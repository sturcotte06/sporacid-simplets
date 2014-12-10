namespace Sporacid.Simplets.Webapp.Tools.Collections.Concurrent
{
    /// <summary>
    /// Interface for all blocking queues for producer/consumer scenarios.
    /// </summary>
    /// <typeparam name="T">Type of the queue.</typeparam>
    /// <author>Simon Turcotte-Langevin</author>
    public interface IBlockingQueue<T>
    {
        /// <summary>
        /// The number of element in the queue.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Whether the queue is empty or not.
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Closes the queue which automatically unblocks all thread waiting on this queue.
        /// </summary>
        void Close();

        /// <summary>
        /// Enqueues an object at the end of the queue.
        /// </summary>
        /// <param name="item">The item to enqueue.</param>
        void Enqueue(T item);

        /// <summary>
        /// Dequeues an object from the queue.
        /// </summary>
        /// <param name="value">The out parameter that will point the to dequeued object.</param>
        /// <returns>Whether the dequeue function did dequeue something or not.</returns>
        bool TryDequeue(out T value);
    }
}