namespace Sporacid.Simplets.Webapp.Tools.Collections.Pooling
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;

    /// <summary>
    /// Class to express a pool of objects to be acquired by threads
    /// and then released. This technique is faster than locking a single
    /// object to the cost of more memory.
    /// </summary>
    /// <typeparam name="TObject">The object on which we pool</typeparam>
    /// <author>Simon Turcotte-Langevin</author>
    public class ObjectPool<TObject> : IObjectPool<TObject> where TObject : class
    {
        /// <summary>
        /// The default capacity.
        /// </summary>
        private const uint cDefaultCapacity = 100;

        /// <summary>
        /// The timeout, in milliseconds, for acquiring an instance.
        /// </summary>
        private const int cAcquireObjectTimeout = 10000;

        /// <summary>
        /// The semaphore to control the number of instances in the pool.
        /// </summary>
        private readonly SemaphoreSlim semaphore;

        /// <summary>
        /// Whether Dispose() was called on this object or not.
        /// </summary>
        private volatile bool isDisposed;

        /// <summary>
        /// The object manager to get new instances of TObject.
        /// </summary>
        protected IObjectManager<TObject> ObjectManager { get; private set; }

        /// <summary>
        /// The object pool.
        /// </summary>
        protected ConcurrentQueue<TObject> ObjectQueue { get; private set; }

        /// <summary>
        /// Default constructor.
        /// The object pool will have the default object provider
        /// and an initial capacity of 1.
        /// </summary>
        public ObjectPool() : this(1)
        {
        }

        /// <summary>
        /// Constructor.
        /// The object pool will have the default object provider.
        /// </summary>
        /// <param name="initialObjectCount">The number of instances initially available in the pool.</param>
        public ObjectPool(uint initialObjectCount)
            : this(new DefaultObjectManager<TObject>(), initialObjectCount)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="manager">The object manager to get new instances of TObject.</param>
        /// <param name="initialObjectCount">The number of instances initially available in the pool.</param>
        public ObjectPool(IObjectManager<TObject> manager, uint initialObjectCount) : this(manager, initialObjectCount, cDefaultCapacity)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="manager">The object manager to get new instances of TObject.</param>
        /// <param name="initialObjectCount">The number of instances initially available in the pool.</param>
        /// <param name="capacity">The maximum number of object that can be available in the pool.</param>
        public ObjectPool(IObjectManager<TObject> manager, uint initialObjectCount, uint capacity)
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }

            if (capacity == 0)
            {
                throw new ArgumentOutOfRangeException("capacity");
            }

            if (initialObjectCount > capacity)
            {
                throw new ArgumentOutOfRangeException("initialObjectCount");
            }

            this.ObjectManager = manager;
            this.ObjectQueue = new ConcurrentQueue<TObject>();
            this.semaphore = new SemaphoreSlim((int) capacity, (int) capacity);

            for (var i = 0; i < initialObjectCount; i++)
            {
                this.ObjectQueue.Enqueue(manager.GetObject());
            }
        }

        /// <summary>
        /// Do an action with an object from the pool.
        /// The object will be exclusive to the thread calling this method.
        /// </summary>
        /// <param name="action">The action to take with the object</param>
        public void WithObject(Action<TObject> action)
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            TObject obj = null;

            try
            {
                obj = this.AcquireObject();
                action(obj);
            }
            finally
            {
                this.ReleaseObject(obj);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with 
        /// freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <remarks>
        /// This is not thread-safe. Make sure no more threads are currently
        /// accessing the object pool when calling dispose. If some threads
        /// still have an instance, then those object won't be released.
        /// </remarks>
        public virtual void Dispose()
        {
            this.isDisposed = true;

            TObject obj;
            while (this.ObjectQueue.TryDequeue(out obj))
            {
                this.ObjectManager.DisposeObject(obj);
            }
        }

        /// <summary>
        /// Acquire an object from the object pool.
        /// The object will not be in the pool anymore and so
        /// the thread will have an "exclusive lock" on the object.
        /// </summary>
        /// <returns>The acquired object.</returns>
        protected virtual TObject AcquireObject()
        {
            TObject obj;
            if (!this.semaphore.Wait(cAcquireObjectTimeout))
            {
                throw new TimeoutException("Unable to acquire an object from the pool in a timely manner.");
            }

            if (!this.ObjectQueue.TryDequeue(out obj))
            {
                // No available object. Create a new instance.
                obj = this.ObjectManager.GetObject();
            }

            return obj;
        }

        /// <summary>
        /// Releases an object back into the object pool.
        /// </summary>
        /// <param name="obj">The object to release.</param>
        protected virtual void ReleaseObject(TObject obj)
        {
            if (obj == null)
            {
                // Do not put a null into the pool
                return;
            }

            // Clean the object for threads to come
            this.ObjectManager.CleanObject(obj);

            // Put the object back into the pool
            this.ObjectQueue.Enqueue(obj);

            // Release the instance back in the semaphore.
            this.semaphore.Release();
        }
    }
}