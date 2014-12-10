namespace Sporacid.Simplets.Webapp.Tools.Threading.Pooling
{
    using System;

    /// <summary>
    /// Structure for a work item.
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    internal class WorkItem : IDisposable
    {
        /// <summary>
        /// Delegate for the method to be called for this work item.
        /// </summary>
        /// <returns>An arbitrary object.</returns>
        public delegate object WorkItemComputation();
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options">The work item options.</param>
        /// <param name="computation">The work item computation.</param>
        /// <param name="result">The work item priority.</param>
        public WorkItem(WorkItemOptions options, WorkItemComputation computation, WorkItemResult result)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            if (computation == null)
            {
                throw new ArgumentNullException("computation");
            }

            if (result == null)
            {
                throw new ArgumentNullException("result");
            }

            this.Options = options;
            this.Computation = computation;
            this.AsyncResult = result;
        }

        /// <summary>
        /// The work item options.
        /// </summary>
        public WorkItemOptions Options { get; private set; }

        /// <summary>
        /// The work asynchronous result.
        /// </summary>
        public WorkItemResult AsyncResult { get; private set; }

        /// <summary>
        /// The work item computation to execute.
        /// </summary>
        public WorkItemComputation Computation { get; private set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Computation = null;
            this.AsyncResult = null;
        }
    }
}