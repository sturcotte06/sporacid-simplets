namespace Sporacid.Simplets.Webapp.Tools.Threading.Pooling
{
    using System;

    /// <summary>
    /// Structure for a cancellation token for a thread pool's work item.
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    public class WorkItemCancellationToken : IDisposable
    {
        /// <summary>
        /// Whether Cancel() was called on this object or not.
        /// </summary>
        private volatile bool isCancellationRequested;

        /// <summary>
        /// Whether Dispose() was called on this object or not.
        /// </summary>
        private volatile bool isDisposed;

        /// <summary>
        /// Whether Cancel() was called on this object or not.
        /// </summary>
        public bool IsCancellationRequested
        {
            get
            {
                // Does not require lock, because it is volatile.
                // This read is guaranteed to be up to date.
                return this.isCancellationRequested;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with 
        /// freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.isDisposed = true;
        }

        /// <summary>
        /// Cancels the work item. The current thread will not block
        /// and the worker thread will eventually be cancelled.
        /// </summary>
        public void Cancel()
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            this.isCancellationRequested = true;
        }
    }
}