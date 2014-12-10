namespace Sporacid.Simplets.Webapp.Tools.Threading.Pooling
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using Sporacid.Simplets.Webapp.Tools.Collections.Concurrent;
    using Sporacid.Simplets.Webapp.Tools.Strings;
    using ThreadState = System.Threading.ThreadState;

    /// <summary>
    /// Thread pool worker that can dequeue work items and process them.
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    internal class ThreadPoolWorker : IDisposable
    {
        /// <summary>
        /// The event that tells if this worker is idle or not.
        /// </summary>
        private readonly ManualResetEventSlim isIdleEvent;

        /// <summary>
        /// The priority queue for all work items.
        /// </summary>
        private readonly IBlockingQueue<WorkItem> workItemQueue;

        /// <summary>
        /// The name of this worker.
        /// </summary>
        private readonly String workerName;

        /// <summary>
        /// Whether Dispose() was called on this object or not.
        /// </summary>
        private volatile bool isDisposed;

        /// <summary>
        /// Whether this worker is stopped or not.
        /// </summary>
        private volatile bool isStopped;

        /// <summary>
        /// Whether Stop() was called on this object or not.
        /// </summary>
        private volatile bool isStopping;

        /// <summary>
        /// The actual thread that this worker runs on.
        /// </summary>
        private Thread thread;

        /// <summary>
        /// Whether this worker is idle or not.
        /// </summary>
        public bool IsIdle
        {
            get
            {
                // Worker is idle if the work item queue is empty and this worker is in wait state.
                // This mean the worker is currently waiting on work items to be queued.
                return this.isIdleEvent.IsSet && ((this.thread.ThreadState & ThreadState.WaitSleepJoin) != 0) && this.workItemQueue.Count == 0;
            }
        }

        /// <summary>
        /// Whether this worker is stopped or not.
        /// </summary>
        public bool IsStopped
        {
            get { return this.isStopped; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="workerName">The name of this worker.</param>
        /// <param name="workItemQueue">The priority queue for all work items.</param>
        public ThreadPoolWorker(string workerName, IBlockingQueue<WorkItem> workItemQueue)
        {
            if (workerName.IsNullOrEmpty())
            {
                throw new ArgumentNullException("workerName");
            }

            if (workItemQueue == null)
            {
                throw new ArgumentNullException("workItemQueue");
            }

            this.workItemQueue = workItemQueue;
            this.workerName = workerName;

            this.isIdleEvent = new ManualResetEventSlim(true);
            this.thread = new Thread(this.ProcessWorkItems) {Name = workerName};
            this.isStopped = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with 
        /// freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (!this.isStopped)
            {
                throw new InvalidOperationException("Worker must be stopped before disposing of it.");
            }

            this.isDisposed = true;

            if (this.isIdleEvent != null)
                this.isIdleEvent.Dispose();
        }

        /// <summary>
        /// Starts the worker.
        /// </summary>
        public void Start()
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (!this.isStopped)
            {
                throw new InvalidOperationException("Worker is already started.");
            }

            // Start the actual thread.
            this.isStopped = false;
            this.thread.Start();
        }

        /// <summary>
        /// Stops the worker.
        /// Make sure the work item queue is closed before calling this method, 
        /// as this will deadlock if the queue is still opened.
        /// </summary>
        public void Stop()
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (this.isStopped)
            {
                throw new InvalidOperationException("Worker is already stopped.");
            }

            // Wait for the thread to end.
            this.isStopping = true;
            this.thread.Join();
            this.isStopped = true;
        }

        /// <summary>
        /// Stops the worker.
        /// Make sure the work item queue is closed before calling this method, 
        /// as this will return false if the queue is still opened.
        /// </summary>
        /// <param name="timeoutInMilliseconds">The number of milliseconds before timeout.</param>
        /// <returns>Whether the worker was stopped or not.</returns>
        public bool Stop(int timeoutInMilliseconds)
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (this.isStopped)
            {
                throw new InvalidOperationException("Worker is already stopped.");
            }

            // Wait for the thread to end.
            this.isStopping = true;
            if (!this.thread.Join(timeoutInMilliseconds))
            {
                // Couldn't join in a timely manner.
                return false;
            }

            this.isStopped = true;
            return true;
        }

        /// <summary>
        /// Waits until the worker is idle.
        /// </summary>
        public void WaitForIdle()
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (this.isStopping || this.isStopped)
            {
                throw new InvalidOperationException("Worker is stopping or stopped.");
            }

            // Idle is kinda tough to implement. The IsIdle flag tells us if the worker is really idle.
            // The is isIdleEvent tells us if the worker is doing a work item or not.
            // Wait on the isIdle event until the IsIdle flag becomes true.
            while (!this.IsIdle)
            {
                this.isIdleEvent.Wait();
            }
        }

        /// <summary>
        /// Waits until the worker is idle.
        /// f the worker takes more than the number of ms specified to be idle, false will be returned. 
        /// </summary>
        /// <param name="timeoutInMilliseconds">The number of milliseconds before timeout.</param>
        /// <returns>Whether the worker was idle or not.</returns>
        public bool WaitForIdle(int timeoutInMilliseconds)
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (this.isStopped)
            {
                throw new InvalidOperationException("Worker is stopped.");
            }

            // Start a watch for the timeout.
            var watch = Stopwatch.StartNew();

            // Idle is kinda tough to implement. The IsIdle flag tells us if the worker is really idle.
            // The is isIdleEvent tells us if the worker is doing a work item or not.
            // Wait on the isIdle event until the IsIdle flag becomes true, or until timeout.
            while (!this.IsIdle)
            {
                var remainingMilliseconds = (int) (timeoutInMilliseconds - watch.ElapsedMilliseconds);
                if (remainingMilliseconds <= 0)
                {
                    // Timeout reached.
                    return false;
                }

                this.isIdleEvent.Wait(remainingMilliseconds);
            }

            return true;
        }

        /// <summary>
        /// Kill this worker.
        /// This should not be called in a common scenario and should be used only to 
        /// prevent resource leak.
        /// </summary>
        public void Kill()
        {
            // Abort the thread and silence any exceptions.
            Snippets.TryCatch(() => this.thread.Abort(), ex => { /* Silence! */ });

            this.isStopped = true;

            // Recreate the thread in the eventuality that Start() is called again.
            this.thread = new Thread(this.ProcessWorkItems) {Name = this.workerName};
        }

        /// <summary>
        /// The routine for the worker thread,
        /// Thread will wait for work items to be available, and will then proceed with them.
        /// This routine will be executed until the priority queue is closed.
        /// </summary>
        private void ProcessWorkItems()
        {
            while (!this.isStopping)
            {
                // Get a work item from the queue
                WorkItem workItem;
                if (!this.workItemQueue.TryDequeue(out workItem))
                {
                    // No more item in the queue, and the queue is closing.
                    // We're done processing work items.
                    break;
                }

                var asyncResult = workItem.AsyncResult;
                if (asyncResult.IsCancelled)
                {
                    // Work item was cancelled.
                    // workItem.AsyncResult.Exception = new OperationCanceledException();
                    continue;
                }

                try
                {
                    // We're now doing work.
                    this.isIdleEvent.Reset();

                    // Signal that the work item result is being computed.
                    asyncResult.BeginComputation();

                    // Invoke the work item work.
                    asyncResult.Result = workItem.Computation();
                }
                catch (Exception ex)
                {
                    asyncResult.Exception = ex;
                }
                finally
                {
                    // Flag the result as computed.
                    asyncResult.EndComputation();

                    // Done with work.
                    this.isIdleEvent.Set();
                }
            }
        }
    }
}