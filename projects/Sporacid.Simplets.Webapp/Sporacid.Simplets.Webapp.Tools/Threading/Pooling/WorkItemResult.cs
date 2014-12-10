namespace Sporacid.Simplets.Webapp.Tools.Threading.Pooling
{
    using System;
    using System.Threading;
    using Sporacid.Simplets.Webapp.Tools.Events;
    using Sporacid.Simplets.Webapp.Tools.Threading.Pooling.Exceptions;

    /// <summary>
    /// Structure for an asynchronous work item result.
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    public class WorkItemResult<TReturn> : WorkItemResult
    {
        /// <summary>
        /// The concrete result of the work item.
        /// </summary>
        internal new TReturn Result { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel this work item.</param>
        public WorkItemResult(WorkItemCancellationToken cancellationToken) : base(cancellationToken)
        {
        }

        /// <summary>
        /// Waits for the result to be computed. If the thread generated an exception, 
        /// throw the generated exception. Else, return the result of the computation.
        /// </summary>
        /// <returns>The result of the computation.</returns>
        public new TReturn GetResultOrThrow()
        {
            return (TReturn) base.GetResultOrThrow();
        }
    }

    /// <summary>
    /// Structure for an asynchronous work item result.
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    public class WorkItemResult : IDisposable
    {
        /// <summary>
        /// The cancellation token for the result provider.
        /// </summary>
        private readonly WorkItemCancellationToken cancellationToken;

        /// <summary>
        /// Event for whether computation has been started for this work item or not.
        /// </summary>
        private readonly ManualResetEventSlim computationBegun = new ManualResetEventSlim(false);

        /// <summary>
        /// Event for whether computation has ended for this work item or not.
        /// </summary>
        private readonly ManualResetEventSlim computationEnded = new ManualResetEventSlim(false);

        /// <summary>
        /// Whether Cancel() has been called on this object or not.
        /// </summary>
        private volatile bool isCancelled;

        /// <summary>
        /// Whether Dispose() was called on this object or not.
        /// </summary>
        private volatile bool isDisposed;

        /// <summary>
        /// The concrete result of the work item.
        /// </summary>
        internal object Result { get; set; }

        /// <summary>
        /// The exception of the work item, if something bad happened.
        /// </summary>
        internal Exception Exception { get; set; }

        /// <summary>
        /// Whether Cancel() has been called on this object or not.
        /// </summary>
        public bool IsCancelled
        {
            get { return this.isCancelled; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel this work item.</param>
        public WorkItemResult(WorkItemCancellationToken cancellationToken)
        {
            if (cancellationToken == null)
            {
                throw new ArgumentNullException("cancellationToken");
            }

            this.cancellationToken = cancellationToken;
        }

        /// <summary>
        /// Performs application-defined tasks associated with 
        /// freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.isDisposed = true;

            // Release references, so the garbage collector can reclaim those.
            // Do not dispose of the thread, because it is not managed by this class.
            this.Result = null;
            this.Exception = null;

            if (this.cancellationToken != null)
                this.cancellationToken.Dispose();

            if (this.computationBegun != null)
                this.computationBegun.Dispose();

            if (this.computationEnded != null)
                this.computationEnded.Dispose();
        }

        /// <summary>
        /// Event triggered when the result is finished computing.
        /// </summary>
        internal event EventHandler<GenericEventArgs<object>> OnResultComputed;

        /// <summary>
        /// Event triggered when the result is finished computing, but the computation has failed.
        /// </summary>
        internal event EventHandler<GenericEventArgs<Exception>> OnResultFailure;

        /// <summary>
        /// Event triggered when the result is cancelled.
        /// </summary>
        internal event EventHandler<EventArgs> OnResultCancelled;

        /// <summary>
        /// Signals that the thread assigned to do this work item have started.
        /// </summary>
        internal void BeginComputation()
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (this.computationBegun.IsSet)
            {
                throw new InvalidOperationException("The work item result has already begun computation.");
            }

            // Trigger the event that will unblock threads waiting for the result.
            this.computationBegun.Set();
        }

        /// <summary>
        /// Signals that the thread assigned to do this work item have ended.
        /// </summary>
        internal void EndComputation()
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (this.computationEnded.IsSet)
            {
                throw new InvalidOperationException("The work item result has already ended computation.");
            }

            // Trigger the event that will unblock threads waiting for the result.
            this.computationEnded.Set();

            // Raise the good event whether the computation failed or succeeded.
            if (this.Exception == null)
            {
                var handler = this.OnResultComputed;
                handler.Raise(this, new GenericEventArgs<object>(this.Result));
            }
            else
            {
                var handler = this.OnResultFailure;
                handler.Raise(this, new GenericEventArgs<Exception>(this.Exception));
            }
        }

        /// <summary>
        /// Waits for the result provider to finish computing the result.
        /// If this methods finish successfully, the result will be in this.Result.
        /// </summary>
        internal void WaitForResult()
        {
            if (!this.WaitForResult(InfiniteTimeout.Value))
            {
                // No timeout specified, so when returning from this method, we expect
                // the thread to have joined. If not, throw an exception.
                throw new WaitException();
            }
        }

        /// <summary>
        /// Waits for the result provider to finish computing the result.
        /// If the wait takes more than the number of ms specified, false will be returned and 
        /// the result won't be available.
        /// </summary>
        /// <param name="timeoutInMilliseconds">The number of milliseconds before timeout.</param>
        /// <returns>Whether the result was computed or not.</returns>
        internal bool WaitForResult(int timeoutInMilliseconds)
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (timeoutInMilliseconds < 0 && timeoutInMilliseconds != InfiniteTimeout.Value)
            {
                throw new ArgumentOutOfRangeException("timeoutInMilliseconds");
            }

            if (this.isCancelled)
            {
                throw new OperationCanceledException();
            }

            // Try to join the thread.
            if (timeoutInMilliseconds == InfiniteTimeout.Value)
            {
                // Wait for computation to be done.
                this.computationEnded.Wait();
                return true;
            }

            // Wait for the result provider to finish computing the result.
            return this.computationEnded.Wait(timeoutInMilliseconds);
        }

        /// <summary>
        /// Cancels the work item and blocks the current thread until the worker thread 
        /// is finished.
        /// </summary>
        public void Cancel()
        {
            if (!this.Cancel(InfiniteTimeout.Value))
            {
                // No timeout specified, so when returning from this method, we expect
                // the work item to have been cacnelled. If not, throw an exception.
                throw new WaitException();
            }
        }

        /// <summary>
        /// Cancels the work item and blocks the current thread until the worker thread 
        /// is finished or aborted (if timeout is reached).
        /// </summary>
        /// <param name="timeoutInMilliseconds">The number of milliseconds before timeout.</param>
        public bool Cancel(int timeoutInMilliseconds)
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (this.isCancelled)
            {
                // Idempotence
                return true;
            }

            // Trigger the cancel of the cancellation token.
            this.cancellationToken.Cancel();

            // If any computation have begun
            if (this.computationBegun.IsSet)
            {
                // Wait until computation have ended. We cannot alter the sequential nature of the work load.
                if (timeoutInMilliseconds == InfiniteTimeout.Value)
                {
                    this.computationEnded.Wait();
                }
                else if (!this.computationEnded.Wait(timeoutInMilliseconds))
                {
                    // Timeout reached.
                    return false;
                }
            }

            // Cancelled successfully.
            this.isCancelled = true;

            // Raise the on cancelled event.
            var handler = this.OnResultCancelled;
            handler.Raise(this, EventArgs.Empty);

            return true;
        }

        /// <summary>
        /// Waits for the result to be computed. If the thread generated an exception, 
        /// throw the generated exception. Else, return the result of the computation.
        /// </summary>
        /// <returns>The result of the computation.</returns>
        public virtual object GetResultOrThrow()
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            // Block until the result is computed.
            this.WaitForResult();

            if (this.Exception != null)
            {
                // Exception happened during computation, throw it.
                throw this.Exception;
            }

            // No exception, return the result of the computation.
            return this.Result;
        }
    }
}