namespace Sporacid.Simplets.Webapp.Tools.Collections.Concurrent
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// Based on Marc Gravell implementation (http://stackoverflow.com/questions/530211/creating-a-blocking-queuet-in-net/530228#530228).
    /// Blocking queue implementation that blocks producer threads if the queue has maxSize elements and
    /// blocks consumer threads if the queue is empty and not closed.
    /// </summary>
    /// <typeparam name="T">Type of the queue.</typeparam>
    public class BlockingBoundedQueue<T> : IBlockingQueue<T>
    {
        /// <summary>
        /// The lock to access the queue.
        /// </summary>
        private readonly object @lock = new object();

        /// <summary>
        /// The max size for this queue. If this size is reached, producer threads are blocked until
        /// an item has been consumed.
        /// </summary>
        private readonly int maxSize;

        /// <summary>
        /// The actual queue, implemented as a linked list for easy insertion.
        /// </summary>
        private readonly LinkedList<T> queue = new LinkedList<T>();

        /// <summary>
        /// Whether Close() has been called on this object or not.
        /// This flag serves to unblocks consumer thread waiting on elements to be queued.
        /// If the queue is empty and closed, then consumer thread can safely exit.
        /// </summary>
        private volatile bool isClosed;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="maxSize">The max size of the bounded queue.</param>
        public BlockingBoundedQueue(int maxSize)
        {
            if (maxSize < 1)
            {
                throw new ArgumentOutOfRangeException("maxSize");
            }

            this.maxSize = maxSize;
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
        /// Enqueues an object at the end of the queue.
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
                while (this.queue.Count >= this.maxSize)
                {
                    Monitor.Wait(this.@lock);
                }

                // Add the item to the end of the queue
                this.queue.AddLast(item);

                if (this.queue.Count == 1)
                {
                    // wake up any blocked dequeue
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
                        value = default(T);
                        return false;
                    }

                    Monitor.Wait(this.@lock);
                }

                // Assign the out param to the first item in the list.
                value = this.queue.First.Value;

                // Remove it from the queue.
                this.queue.RemoveFirst();

                if (this.queue.Count == this.maxSize - 1)
                {
                    // wake up any blocked enqueue
                    Monitor.PulseAll(this.@lock);
                }

                return true;
            }
        }
    }
}