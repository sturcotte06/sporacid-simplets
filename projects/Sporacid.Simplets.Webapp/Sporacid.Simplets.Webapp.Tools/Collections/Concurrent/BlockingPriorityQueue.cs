namespace Sporacid.Simplets.Webapp.Tools.Collections.Concurrent
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// Blocking queue implementation that enqueue objects based on a priority
    /// comparer. Lowest priorities objects will always be inserted last, and highest
    /// priorities will always be inserted earlier in the queue.
    /// </summary>
    /// <typeparam name="T">Type of the queue.</typeparam>
    public class BlockingPriorityQueue<T> : IBlockingQueue<T>
    {
        /// <summary>
        /// The priority comparer to know where to enqueue objects.
        /// </summary>
        private readonly IComparer<T> comparer;

        /// <summary>
        /// The lock to access the queue.
        /// </summary>
        private readonly object @lock = new object();

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
        /// <param name="comparer">The priority comparer to know where to enqueue objects.</param>
        public BlockingPriorityQueue(Func<T, T, int> comparer)
            : this(new LambdaComparer<T>(comparer))
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="comparer">The priority comparer to know where to enqueue objects.</param>
        public BlockingPriorityQueue(IComparer<T> comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }

            this.comparer = comparer;
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

            var addFirst = false;
            lock (this.@lock)
            {
                // Find at which node to insert the item in the queue.
                // Because this is a priority queue, we want to insert the item
                // after all items with greater or equal priority.
                var currentNode = this.queue.First;
                while (currentNode != null)
                {
                    if (this.comparer.Compare(item, currentNode.Value) > 0)
                    {
                        // Item to enqueue has a greater priority than current item.
                        currentNode = currentNode.Previous;

                        // If previous node is null, then insertion should be before the current first node.
                        addFirst = currentNode == null;

                        break;
                    }

                    // Move to the next node
                    currentNode = currentNode.Next;
                }

                if (currentNode == null && addFirst)
                {
                    // Checked the first item and there's no item with priority that high, enqueue first.
                    this.queue.AddFirst(item);
                }
                else if (currentNode == null)
                {
                    // Iterated through whole list, didn't find an item with priority that low, enqueue last.
                    this.queue.AddLast(item);
                }
                else
                {
                    // Found at which node to insert.
                    this.queue.AddAfter(currentNode, item);
                }

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

        /// <summary>
        /// IComparer implementation that takes a lambda to compare two objects against each other.
        /// </summary>
        /// <typeparam name="TCompare">The type to compare.</typeparam>
        /// <author>Simon Turcotte-Langevin</author>
        private class LambdaComparer<TCompare> : IComparer<TCompare>
        {
            /// <summary>
            /// The lambda expression that allows comparison of TCompare instances.
            /// </summary>
            private readonly Func<TCompare, TCompare, int> comparerLambda;

            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="comparerLambda">The lambda expression that allows comparison of TCompare instances.</param>
            public LambdaComparer(Func<TCompare, TCompare, int> comparerLambda)
            {
                this.comparerLambda = comparerLambda;
            }

            /// <summary>
            /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
            /// </summary>
            /// <returns>
            /// A signed integer that indicates the relative values of <paramref name="x"/> and <paramref name="y"/>, as shown in the following table.Value Meaning Less than zero<paramref name="x"/> is less than <paramref name="y"/>.Zero<paramref name="x"/> equals <paramref name="y"/>.Greater than zero<paramref name="x"/> is greater than <paramref name="y"/>.
            /// </returns>
            /// <param name="x">The first object to compare.</param><param name="y">The second object to compare.</param>
            public int Compare(TCompare x, TCompare y)
            {
                return this.comparerLambda(x, y);
            }
        }
    }
}