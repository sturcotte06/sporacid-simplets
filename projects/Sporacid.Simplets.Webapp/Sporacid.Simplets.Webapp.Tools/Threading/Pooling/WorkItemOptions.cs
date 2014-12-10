namespace Sporacid.Simplets.Webapp.Tools.Threading.Pooling
{
    using System;

    /// <summary>
    /// Structure for a work item's options.
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    public class WorkItemOptions<TReturn> : WorkItemOptions
    {
        /// <summary>
        /// The handler when the work item is done computing.
        /// This method won't be called if an exception occurs;
        /// OnWorkItemException will be called instead.
        /// </summary>
        public new WorkItemComputedHandler<TReturn> OnWorkItemComputed { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="priority">The priority of the work item.</param>
        public WorkItemOptions(Priority priority = Priority.Normal) : base(priority)
        {
        }
    }

    /// <summary>
    /// Structure for a work item's options.
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    public class WorkItemOptions
    {
        /// <summary>
        /// The priority of the work item.
        /// </summary>
        public Priority Priority { get; set; }

        /// <summary>
        /// The handler when the work item is done computing.
        /// This method won't be called if an exception occurs;
        /// OnWorkItemException will be called instead.
        /// </summary>
        public WorkItemComputedHandler<object> OnWorkItemComputed { get; set; }

        /// <summary>
        /// The handler when the work item fails with an exception.
        /// This method won't be called if no exception occurs.
        /// </summary>
        public WorkItemFailedHandler OnWorkItemFailure { get; set; }

        /// <summary>
        /// The handler when the work item is cancelled.
        /// </summary>
        public WorkItemCancelledHandler OnWorkItemCancelled { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="priority">The priority of the work item.</param>
        public WorkItemOptions(Priority priority = Priority.Normal)
        {
            this.Priority = priority;
        }
    }

    /// <summary>
    /// Handler for the on computed event of the work item.
    /// </summary>
    /// <typeparam name="TReturn">The return type of the work item.</typeparam>
    /// <param name="return">The return value of the work item.</param>
    public delegate void WorkItemComputedHandler<in TReturn>(TReturn @return);

    /// <summary>
    /// Handler for the on failure event of the work item.
    /// </summary>
    /// <param name="exception">The work item's generated exception.</param>
    public delegate void WorkItemFailedHandler(Exception exception);

    /// <summary>
    /// Handler for the on cancel event of the work item.
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    public delegate void WorkItemCancelledHandler();
}