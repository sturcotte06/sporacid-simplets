namespace Sporacid.Simplets.Webapp.Tools.Events
{
    using System;

    /// <summary>
    /// Generic event args implementation.
    /// </summary>
    /// <typeparam name="TData">Type of the data associated with the event args.</typeparam>
    [Serializable]
    public class GenericEventArgs<TData> : EventArgs
    {
        /// <summary>
        /// The event args' data.
        /// </summary>
        public TData Data { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">The event args' data.</param>
        public GenericEventArgs(TData data)
        {
            this.Data = data;
        }
    }
}