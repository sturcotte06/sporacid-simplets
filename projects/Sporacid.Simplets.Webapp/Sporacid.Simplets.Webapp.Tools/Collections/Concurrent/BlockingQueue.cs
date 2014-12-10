namespace Sporacid.Simplets.Webapp.Tools.Collections.Concurrent
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// Blocking queue implementation.
    /// </summary>
    /// <typeparam name="T">Type of the queue.</typeparam>
    public class BlockingQueue<T> : IBlockingQueue<T>
    {
        /// <summary>
        /// The lock to access the queue.
        /// </summary>
        private readonly object @lock = new object();

        /// <summary>
        /// The actual queue, implemented as a linked list for easy insertion.
        /// </summary>
        private readonly LinkedList<T> queue;

        /// <summary>
        /// Whether Close() has been called on this object or not.
        /// This flag serves to unblocks consumer thread waiting on elements to be queued.
        /// If the queue is empty and closed, then consumer thread can safely exit.
        /// </summary>
        private volatile bool isClosed;

        /// <summary>
        /// Constructor.
        /// </summary>
        public BlockingQueue()
        {
            this.queue = new LinkedList<T>();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="enumerable">Base enumeration from which to create the queue.</param>
        public BlockingQueue(IEnumerable<T> enumerable)
        {
            this.queue = new LinkedList<T>(enumerable);
        }

        /// <summary>
        /// The number of element in the queue.
        /// </summary>
        public int Count
        {
            get
            {
                lock (this.@lock)
                {
                    return this.queue.Count;
                }
            }
        }

        /// <summary>
        /// Whether the queue is empty or not.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                lock (this.@lock)
                {
                    return this.queue.Count == 0;
                }
            }
        }

        /// <summary>
        /// Closes the queue which automatically unblocks all thread waiting on this queue.
        /// </summary>
        public void Close()
        {
            lock (this.@lock)
            {
                this.isClosed = true;
                Monitor.PulseAll(this.@lock);
            }
        }

        /// <summary>
        /// Enqueues an object into the queue. The object will be inserted 
        /// somewhere in the queue based on its priority.
        /// </summary>
        /// <param name="item">The item to enqueue.</param>
        public void Enqueue(T item)
        {
            if (this.isClosed)
            {
                throw new InvalidOperationException("The blocking queue is closed. Enqueuing would cause unwanted behaviours and is prohibited.");
            }

            lock (this.@lock)
            {
                // Add the item at last position.
                this.queue.AddLast(item);

                if (this.queue.Count == 1)
                {
                    // Queue was empty, now we added a new item, so the queue has exactly one element.
                    // Since we have the lock, there can't be more or less than one lement, so wake all threads.
                    Monitor.PulseAll(this.@lock);
                }
            }
        }

        /// <summary>
        /// Dequeues an object from the queue.
        /// </summary>
        /// <param name="value">The out parameter that will point the to dequeued object.</param>
        /// <returns>Whether the dequeue function did dequeue something or not.</returns>
        public bool TryDequeue(out T value)
        {
            lock (this.@lock)
            {
                while (this.queue.Count == 0)
                {
                    if (this.isClosed)
                    {
                        // Queue is closing, do not block thread.
                        value = default(T);
                        return false;
                    }

                    // Wait until an item in the queue is available.
                    Monitor.Wait(this.@lock);
                }

                // Assign the out param to the first item in the list.
                value = this.queue.First.Value;

                // Remove it from the queue.
                this.queue.RemoveFirst();

                return true;
            }
        }
    }
}