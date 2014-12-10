namespace Sporacid.Simplets.Webapp.Tools.Threading.Pooling
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Sporacid.Simplets.Webapp.Tools.Collections.Concurrent;
    using Sporacid.Simplets.Webapp.Tools.Threading.Pooling.Exceptions;

    /// <summary>
    /// IThreadPool implementation.
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    public class ThreadPool : IThreadPool
    {
        /// <summary>
        /// Empty object array.
        /// </summary>
        private static readonly object[] EmptyParams = {};

        /// <summary>
        /// The thread pool configuration.
        /// </summary>
        private readonly ThreadPoolConfiguration configuration;

        /// <summary>
        /// The priority queue for all work items.
        /// </summary>
        private readonly BlockingPriorityQueue<WorkItem> workItemQueue;

        /// <summary>
        /// The list of available workers.
        /// </summary>
        private readonly List<ThreadPoolWorker> workers;

        /// <summary>
        /// Whether Dispose() was called on this object or not.
        /// </summary>
        private volatile bool isDisposed;

        /// <summary>
        /// Whether the thread pool is shutdown or not.
        /// </summary>
        private volatile bool isShutdown;

        /// <summary>
        /// Whether Shutdown() or ForceShutdown() was called on this object or not.
        /// If this flag is set to true and IsShutdown is false, then Shutdown() was called,
        /// but failed. If the thread pool is in this state, ForceShutdown() should be called to prevent
        /// thread leaks.
        /// </summary>
        private volatile bool isShutdownRequested;

        /// <summary>
        /// Whether the thread pool is started or not.
        /// </summary>
        private volatile bool isStarted;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="configuration">The thread pool configuration.</param>
        public ThreadPool(ThreadPoolConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            this.configuration = configuration;

            this.workers = new List<ThreadPoolWorker>();
            this.workItemQueue = new BlockingPriorityQueue<WorkItem>((item1, item2) => item2.Options.Priority - item1.Options.Priority);

            if (configuration.AutomaticStart)
            {
                // Flagged as automatic start. Start the thread pool.
                this.Start();
            }
        }

        /// <summary>
        /// Whether the thread pool is shutdown or not.
        /// </summary>
        public bool IsShutdown
        {
            get { return this.isShutdown; }
        }

        /// <summary>
        /// Whether the thread pool is started or not.
        /// </summary>
        public bool IsStarted
        {
            get { return this.isStarted; }
        }

        /// <summary>
        /// Whether the thread pool is idle or not.
        /// </summary>
        public bool IsIdle
        {
            get { return this.workers.All(w => w.IsIdle); }
        }

        /// <summary>
        /// Performs application-defined tasks associated with 
        /// freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (!this.IsShutdown)
            {
                throw new ThreadPoolNotShutdownException();
            }

            this.isDisposed = true;

            // Make sure all threads are stopped before clearing their references.
            const int arbitraryTimeout = 2500;
            this.workers.ForEach(worker =>
            {
                if (!worker.IsStopped)
                {
                    if (!worker.Stop(arbitraryTimeout))
                    {
                        throw new WaitException(
                            "Couldn't stop the thread pool's worker in a timely manner. All workers should already have been stopped, and this is a fatal exception. Threads WILL leak.");
                    }
                }

                // Dispose of the worker's resources.
                worker.Dispose();
            });

            // Clear all workers references.
            this.workers.Clear();

            // Cancel all work items that have not been executed and dispose of them.
            WorkItem workItem;
            while (this.workItemQueue.TryDequeue(out workItem))
            {
                workItem.AsyncResult.Cancel();
                workItem.Dispose();
            }
        }

        /// <summary>
        /// Starts the thread pool with the given configuration.
        /// </summary>
        public void Start()
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (this.IsStarted)
            {
                throw new ThreadPoolAlreadyStartedException();
            }

            for (var iThread = 0; iThread < this.configuration.ThreadCount; iThread++)
            {
                // Initialize a new worker
                var worker = new ThreadPoolWorker(String.Format("{0} - {1}", this.configuration.ThreadNamePrefix, iThread), this.workItemQueue);

                // Start it right now.
                worker.Start();

                // Add it to the list of workers so we can track it.
                this.workers.Add(worker);
            }

            this.isStarted = true;
        }

        /// <summary>
        /// Shutdowns the thread pool. The call will block until all threads are finished.
        /// </summary>
        public void Shutdown()
        {
            if (!this.Shutdown(InfiniteTimeout.Value))
            {
                // No timeout specified, so when returning from this method, we expect
                // the thread pool to have shutdown. If not, throw an exception.
                throw new WaitException();
            }
        }

        /// <summary>
        /// Shutdowns the thread pool.
        /// If the shutdown takes more than the number of ms specified, false will be returned,
        /// and the thread pool won't be shutdown.
        /// </summary>
        /// <param name="timeoutInMilliseconds">The number of milliseconds before timeout.</param>
        /// <returns>Whether the thread pool was shutdown or not.</returns>
        public bool Shutdown(int timeoutInMilliseconds)
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (this.IsShutdown)
            {
                throw new ThreadPoolAlreadyShutdownException();
            }

            if (timeoutInMilliseconds < 0 && timeoutInMilliseconds != InfiniteTimeout.Value)
            {
                throw new ArgumentOutOfRangeException("timeoutInMilliseconds");
            }

            // Flag shutdown requested.
            this.isShutdownRequested = true;

            // Close the queue to unblock all waiting threads.
            this.workItemQueue.Close();

            if (timeoutInMilliseconds == InfiniteTimeout.Value)
            {
                // Stop all workers without timeout.
                this.workers.ForEach(w => w.Stop());
            }
            else
            {
                // Start a watch for the timeout.
                var watch = Stopwatch.StartNew();

                // Stop all workers with the specified timeout
                foreach (var worker in this.workers)
                {
                    int remainingMilliseconds;
                    do
                    {
                        // Calculate the remaining milliseconds until timeout.
                        remainingMilliseconds = (int) (timeoutInMilliseconds - watch.ElapsedMilliseconds);
                        if (remainingMilliseconds > 0)
                        {
                            // Timeout not yet reached.
                            continue;
                        }

                        // Timeout reached.
                        watch.Stop();
                        return false;
                    } // Stop the worker with a timeout equal to the number of remaining milliseconds.
                    while (!worker.Stop(remainingMilliseconds));
                }

                watch.Stop();
            }

            // Cancel all work items and dispose of them.
            WorkItem workItem;
            while (this.workItemQueue.TryDequeue(out workItem))
            {
                workItem.AsyncResult.Cancel();
                workItem.Dispose();
            }

            // Shutdown completed successfully.
            this.isShutdown = true;

            return true;
        }

        /// <summary>
        /// Shutdowns the thread pool.
        /// If the shutdown takes more than the number of ms specified, all threads will be aborted.
        /// </summary>
        /// <param name="timeoutInMilliseconds">The number of milliseconds before timeout.</param>
        public void ForceShutdown(int timeoutInMilliseconds)
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (this.IsShutdown)
            {
                throw new ThreadPoolAlreadyShutdownException();
            }

            if (timeoutInMilliseconds < 0 && timeoutInMilliseconds != InfiniteTimeout.Value)
            {
                throw new ArgumentOutOfRangeException("timeoutInMilliseconds");
            }

            // Flag shutdown requested.
            this.isShutdownRequested = true;

            // Close the queue to unblock all waiting threads.
            this.workItemQueue.Close();

            if (timeoutInMilliseconds == InfiniteTimeout.Value)
            {
                // Stop all workers without timeout.
                this.workers.ForEach(w => w.Stop());
            }
            else
            {
                // Start a watch for the timeout.
                var watch = Stopwatch.StartNew();

                // Stop all workers with the specified timeout
                var aggressive = false;
                foreach (var worker in this.workers)
                {
                    if (!aggressive)
                    {
                        int remainingMilliseconds;
                        do
                        {
                            // Calculate the remaining milliseconds until timeout.
                            remainingMilliseconds = (int) (timeoutInMilliseconds - watch.ElapsedMilliseconds);
                            if (remainingMilliseconds > 0)
                            {
                                // Timeout not yet reached.
                                continue;
                            }

                            // Timeout reached. Flag the shutdown to be aggressive.
                            // Break this loop as we're not trying to stop workers cleanly anymore.
                            aggressive = true;
                            break;
                        } // Stop the worker with a timeout equal to the number of remaining milliseconds.
                        while (!worker.Stop(remainingMilliseconds));
                    }

                    if (aggressive && !worker.IsStopped)
                    {
                        // Timeout reached. Start being aggressive.
                        worker.Kill();
                    }
                }

                watch.Stop();
            }

            // Cancel all work items and dispose of them.
            WorkItem workItem;
            while (this.workItemQueue.TryDequeue(out workItem))
            {
                workItem.AsyncResult.Cancel();
                workItem.Dispose();
            }

            // Shutdown completed successfully.
            this.isShutdown = true;
        }

        /// <summary>
        /// Blocks the current thread until the thread pool is done with all work items.
        /// </summary>
        /// <remarks>
        /// I am a 100% positive that there is a race condition. A thread could be queuing work items
        /// while waiting for the pool to be idle. Race condition :
        ///     1) Thread A calls WaitForIdle()
        ///     2) Thread Pool checks worker 1 : it is idle.
        ///     3) Thread Pool checks worker 2 : it is idle.
        ///     4) Thread B queues a work item; worker 1 wakes up and does the work.
        ///     5) Thread Pool checks worker 3 : it is idle.
        ///     6) Thread A unblocks because all worker were idle, but worker 1 is now doing a work item.
        /// The workaround could be to have an event set when waiting on idle. Threads queuing work item would have to wait.
        /// However, this does not fix the problem because when WaitForIdle() unblocks, it did assert that the pool was idle at that moment,
        /// but as soon as the method ends, workers will start working again.
        /// </remarks>
        public void WaitForIdle()
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (this.IsShutdown || this.isShutdownRequested)
            {
                throw new ThreadPoolAlreadyShutdownException();
            }

            // Wait for all worker threads to be idle.
            this.workers.ForEach(w => w.WaitForIdle());
        }

        /// <summary>
        /// Blocks the current thread until the thread pool is done with all work items.
        /// If the thread pool takes more than the number of ms specified to be idle, false will be returned. 
        /// </summary>
        /// <param name="timeoutInMilliseconds">The number of milliseconds before timeout.</param>
        /// <returns>Whether the thread pool was idle or not.</returns>
        public bool WaitForIdle(int timeoutInMilliseconds)
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (this.IsShutdown || this.isShutdownRequested)
            {
                throw new ThreadPoolAlreadyShutdownException();
            }

            // Start a watch for the timeout.
            var watch = Stopwatch.StartNew();

            foreach (var worker in this.workers)
            {
                var remainingMilliseconds = (int) (timeoutInMilliseconds - watch.ElapsedMilliseconds);
                if (remainingMilliseconds <= 0)
                {
                    return false;
                }

                worker.WaitForIdle(remainingMilliseconds);
            }

            return true;
        }

        /// <summary>
        /// Queues a work item that takes no parameter and returns nothing into the thread pool.
        /// </summary>
        /// <param name="work">The delegate that represent the work to do.</param>
        /// <param name="options">The options of the work.</param>
        /// <returns>The work item asynchronous result object.</returns>
        public WorkItemResult<Threading.Void> QueueWorkItem(Work work, WorkItemOptions<Threading.Void> options)
        {
            return this.QueueWorkItem((cancelToken, @params) => Snippets.DoThenReturn(() => work(cancelToken), () => Threading.Void.Value), EmptyParams, options);
        }

        /// <summary>
        /// Queues a work item that takes no parameter into the thread pool.
        /// </summary>
        /// <typeparam name="TReturn">The return type of the work item.</typeparam>
        /// <param name="work">The delegate that represent the work to do.</param>
        /// <param name="options">The options of the work.</param>
        /// <returns>The work item asynchronous result object.</returns>
        public WorkItemResult<TReturn> QueueWorkItem<TReturn>(Work<TReturn> work, WorkItemOptions<TReturn> options)
        {
            return this.QueueWorkItem((cancelToken, @params) => work(cancelToken), EmptyParams, options);
        }

        /// <summary>
        /// Queues a work item that takes a single parameter into the thread pool.
        /// </summary>
        /// <typeparam name="TParam1">The first parameter type.</typeparam>
        /// <typeparam name="TReturn">The return type of the work item.</typeparam>
        /// <param name="work">The delegate that represent the work to do.</param>
        /// <param name="param1">The first parameter value.</param>
        /// <param name="options">The options of the work.</param>
        /// <returns>The work item asynchronous result object.</returns>
        public WorkItemResult<TReturn> QueueWorkItem<TParam1, TReturn>(Work<TParam1, TReturn> work, TParam1 param1, WorkItemOptions<TReturn> options)
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (this.IsShutdown || this.isShutdownRequested)
            {
                throw new ThreadPoolAlreadyShutdownException();
            }

            // Create all required objects.
            var cancelToken = new WorkItemCancellationToken();
            var result = new WorkItemResult<TReturn>(cancelToken);

            // Bind the callbacks on the good events.
            if (options.OnWorkItemComputed != null) 
                result.OnResultComputed += (sender, args) => options.OnWorkItemComputed((TReturn) args.Data);
            if (options.OnWorkItemFailure != null) 
                result.OnResultFailure += (sender, args) => options.OnWorkItemFailure(args.Data);
            if (options.OnWorkItemCancelled != null) 
                result.OnResultCancelled += (sender, args) => options.OnWorkItemCancelled();

            // Create the work item.
            var workItem = new WorkItem(options, () => work(cancelToken, param1), result);

            // Queue the work item.
            this.workItemQueue.Enqueue(workItem);

            // Return the async result back to the user.
            // It has no thread assigned yet, and waiting on this object will cause 
            // the current thread to wait for:
            //      1) A thread to be assigned.
            //      2) The result to be computed.
            return result;
        }

        /// <summary>
        /// Queues a work item that takes two parameters into the thread pool.
        /// </summary>
        /// <typeparam name="TParam1">The first parameter type.</typeparam>
        /// <typeparam name="TParam2">The second parameter type.</typeparam>
        /// <typeparam name="TReturn">The return type of the work item.</typeparam>
        /// <param name="work">The delegate that represent the work to do.</param>
        /// <param name="param1">The first parameter value.</param>
        /// <param name="param2">The second parameter value.</param>
        /// <param name="options">The options of the work.</param>
        /// <returns>The work item asynchronous result object.</returns>
        public WorkItemResult<TReturn> QueueWorkItem<TParam1, TParam2, TReturn>(Work<TParam1, TParam2, TReturn> work, TParam1 param1, TParam2 param2, WorkItemOptions<TReturn> options)
        {
            return this.QueueWorkItem((cancelToken, @params) => work(cancelToken, (TParam1) @params[0], (TParam2) @params[1]), new object[] {param1, param2}, options);
        }

        /// <summary>
        /// Queues a work item that takes three parameters into the thread pool.
        /// </summary>
        /// <typeparam name="TParam1">The first parameter type.</typeparam>
        /// <typeparam name="TParam2">The second parameter type.</typeparam>
        /// <typeparam name="TParam3">The third parameter type.</typeparam>
        /// <typeparam name="TReturn">The return type of the work item.</typeparam>
        /// <param name="work">The delegate that represent the work to do.</param>
        /// <param name="param1">The first parameter value.</param>
        /// <param name="param2">The second parameter value.</param>
        /// <param name="param3">The third parameter value.</param>
        /// <param name="options">The options of the work.</param>
        /// <returns>The work item asynchronous result object.</returns>
        public WorkItemResult<TReturn> QueueWorkItem<TParam1, TParam2, TParam3, TReturn>(Work<TParam1, TParam2, TParam3, TReturn> work, TParam1 param1, TParam2 param2, TParam3 param3,
            WorkItemOptions<TReturn> options)
        {
            return this.QueueWorkItem((cancelToken, @params) => work(cancelToken, (TParam1) @params[0], (TParam2) @params[1], (TParam3) @params[2]), new object[] {param1, param2, param3}, options);
        }

        /// <summary>
        /// Queues a work item that takes four parameters into the thread pool.
        /// </summary>
        /// <typeparam name="TParam1">The first parameter type.</typeparam>
        /// <typeparam name="TParam2">The second parameter type.</typeparam>
        /// <typeparam name="TParam3">The third parameter type.</typeparam>
        /// <typeparam name="TParam4">The fourth parameter type.</typeparam>
        /// <typeparam name="TReturn">The return type of the work item.</typeparam>
        /// <param name="work">The delegate that represent the work to do.</param>
        /// <param name="param1">The first parameter value.</param>
        /// <param name="param2">The second parameter value.</param>
        /// <param name="param3">The third parameter value.</param>
        /// <param name="param4">The fourth parameter value.</param>
        /// <param name="options">The options of the work.</param>
        /// <returns>The work item asynchronous result object.</returns>
        public WorkItemResult<TReturn> QueueWorkItem<TParam1, TParam2, TParam3, TParam4, TReturn>(Work<TParam1, TParam2, TParam3, TParam4, TReturn> work, TParam1 param1, TParam2 param2, TParam3 param3,
            TParam4 param4, WorkItemOptions<TReturn> options)
        {
            return this.QueueWorkItem((cancelToken, @params) => work(cancelToken, (TParam1) @params[0], (TParam2) @params[1], (TParam3) @params[2], (TParam4) @params[3]),
                new object[] {param1, param2, param3, param4}, options);
        }
    }
}