namespace Sporacid.Simplets.Webapp.Tools.Threading.Pooling
{
    using System;

    /// <summary>
    /// Interface for all thread pools.
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    public interface IThreadPool : IDisposable
    {
        /// <summary>
        /// The thread pool's configuration.
        /// </summary>
        ThreadPoolConfiguration Configuration { get; }

        /// <summary>
        /// Whether the thread pool is shutdown or not.
        /// </summary>
        bool IsShutdown { get; }

        /// <summary>
        /// Whether the thread pool is started or not.
        /// </summary>
        bool IsStarted { get; }

        /// <summary>
        /// Whether the thread pool is idle or not.
        /// </summary>
        bool IsIdle { get; }

        /// <summary>
        /// Starts the thread pool with the given configuration.
        /// </summary>
        void Start();

        /// <summary>
        /// Shutdowns the thread pool. The call will block until all threads are finished.
        /// </summary>
        void Shutdown();

        /// <summary>
        /// Shutdowns the thread pool.
        /// If the shutdown takes more than the number of ms specified, false will be returned,
        /// and the thread pool won't be shutdown.
        /// </summary>
        /// <param name="timeoutInMilliseconds">The number of milliseconds before timeout.</param>
        /// <returns>Whether the thread pool was shutdown or not.</returns>
        bool Shutdown(int timeoutInMilliseconds);

        /// <summary>
        /// Shutdowns the thread pool.
        /// If the shutdown takes more than the number of ms specified, all threads will be aborted.
        /// </summary>
        /// <param name="timeoutInMilliseconds">The number of milliseconds before timeout.</param>
        void ForceShutdown(int timeoutInMilliseconds);

        /// <summary>
        /// Blocks the current thread until the thread pool is done with all work items.
        /// </summary>
        void WaitForIdle();

        /// <summary>
        /// Blocks the current thread until the thread pool is done with all work items.
        /// If the thread pool takes more than the number of ms specified to be idle, false will be returned.
        /// </summary>
        /// <param name="timeoutInMilliseconds">The number of milliseconds before timeout.</param>
        /// <returns>Whether the thread pool was idle or not.</returns>
        bool WaitForIdle(int timeoutInMilliseconds);

        /// <summary>
        /// Queues a work item that takes no parameter and returns nothing into the thread pool.
        /// </summary>
        /// <param name="work">The delegate that represent the work to do.</param>
        /// <param name="options">The options of the work.</param>
        /// <returns>The work item asynchronous result object.</returns>
        WorkItemResult<Threading.Void> QueueWorkItem(Work work, WorkItemOptions<Threading.Void> options);

        /// <summary>
        /// Queues a work item that takes no parameter into the thread pool.
        /// </summary>
        /// <typeparam name="TReturn">The return type of the work item.</typeparam>
        /// <param name="work">The delegate that represent the work to do.</param>
        /// <param name="options">The options of the work.</param>
        /// <returns>The work item asynchronous result object.</returns>
        WorkItemResult<TReturn> QueueWorkItem<TReturn>(Work<TReturn> work, WorkItemOptions<TReturn> options);

        /// <summary>
        /// Queues a work item that takes a single parameter into the thread pool.
        /// </summary>
        /// <typeparam name="TParam1">The first parameter type.</typeparam>
        /// <typeparam name="TReturn">The return type of the work item.</typeparam>
        /// <param name="work">The delegate that represent the work to do.</param>
        /// <param name="param1">The first parameter value.</param>
        /// <param name="options">The options of the work.</param>
        /// <returns>The work item asynchronous result object.</returns>
        WorkItemResult<TReturn> QueueWorkItem<TParam1, TReturn>(Work<TParam1, TReturn> work, TParam1 param1, WorkItemOptions<TReturn> options);

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
        WorkItemResult<TReturn> QueueWorkItem<TParam1, TParam2, TReturn>(Work<TParam1, TParam2, TReturn> work, TParam1 param1, TParam2 param2, WorkItemOptions<TReturn> options);

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
        WorkItemResult<TReturn> QueueWorkItem<TParam1, TParam2, TParam3, TReturn>(Work<TParam1, TParam2, TParam3, TReturn> work, TParam1 param1, TParam2 param2, TParam3 param3,
            WorkItemOptions<TReturn> options);

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
        WorkItemResult<TReturn> QueueWorkItem<TParam1, TParam2, TParam3, TParam4, TReturn>(Work<TParam1, TParam2, TParam3, TParam4, TReturn> work, TParam1 param1, TParam2 param2,
            TParam3 param3,
            TParam4 param4, WorkItemOptions<TReturn> options);
    }

    /// <summary>
    /// Delegate for a work load that takes no parameter and returns nothing.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the work load.</param>
    public delegate void Work(WorkItemCancellationToken cancellationToken);

    /// <summary>
    /// Delegate for a work load that takes no parameter.
    /// </summary>
    /// <typeparam name="TReturn">The return type of the work load.</typeparam>
    /// <param name="cancellationToken">The cancellation token to cancel the work load.</param>
    /// <returns>The work load result.</returns>
    public delegate TReturn Work<out TReturn>(WorkItemCancellationToken cancellationToken);

    /// <summary>
    /// Delegate for a work load that takes a single parameter.
    /// </summary>
    /// <typeparam name="TParam1">The first parameter type.</typeparam>
    /// <typeparam name="TReturn">The return type of the work load.</typeparam>
    /// <param name="cancellationToken">The cancellation token to cancel the work load.</param>
    /// <param name="param1">The first parameter value.</param>
    /// <returns>The work load result.</returns>
    public delegate TReturn Work<in TParam1, out TReturn>(WorkItemCancellationToken cancellationToken, TParam1 param1);

    /// <summary>
    /// Delegate for a work load that takes a two parameters.
    /// </summary>
    /// <typeparam name="TParam1">The first parameter type.</typeparam>
    /// <typeparam name="TParam2">The second parameter type.</typeparam>
    /// <typeparam name="TReturn">The return type of the work load.</typeparam>
    /// <param name="cancellationToken">The cancellation token to cancel the work load.</param>
    /// <param name="param1">The first parameter value.</param>
    /// <param name="param2">The second parameter value.</param>
    /// <returns>The work load result.</returns>
    public delegate TReturn Work<in TParam1, in TParam2, out TReturn>(WorkItemCancellationToken cancellationToken, TParam1 param1, TParam2 param2);

    /// <summary>
    /// Delegate for a work load that takes a three parameters.
    /// </summary>
    /// <typeparam name="TParam1">The first parameter type.</typeparam>
    /// <typeparam name="TParam2">The second parameter type.</typeparam>
    /// <typeparam name="TParam3">The third parameter type.</typeparam>
    /// <typeparam name="TReturn">The return type of the work load.</typeparam>
    /// <param name="cancellationToken">The cancellation token to cancel the work load.</param>
    /// <param name="param1">The first parameter value.</param>
    /// <param name="param2">The second parameter value.</param>
    /// <param name="param3">The third parameter value.</param>
    /// <returns>The work load result.</returns>
    public delegate TReturn Work<in TParam1, in TParam2, in TParam3, out TReturn>(WorkItemCancellationToken cancellationToken, TParam1 param1, TParam2 param2, TParam3 param3);

    /// <summary>
    /// Delegate for a work load that takes a four parameters.
    /// </summary>
    /// <typeparam name="TParam1">The first parameter type.</typeparam>
    /// <typeparam name="TParam2">The second parameter type.</typeparam>
    /// <typeparam name="TParam3">The third parameter type.</typeparam>
    /// <typeparam name="TParam4">The fourth parameter type.</typeparam>
    /// <typeparam name="TReturn">The return type of the work load.</typeparam>
    /// <param name="cancellationToken">The cancellation token to cancel the work load.</param>
    /// <param name="param1">The first parameter value.</param>
    /// <param name="param2">The second parameter value.</param>
    /// <param name="param3">The third parameter value.</param>
    /// <param name="param4">The fourth parameter value.</param>
    /// <returns>The work load result.</returns>
    public delegate TReturn Work<in TParam1, in TParam2, in TParam3, in TParam4, out TReturn>(
        WorkItemCancellationToken cancellationToken, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4);
}