namespace Sporacid.Simplets.Webapp.Tools.Collections.Caches.Policies.Locking
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// </summary>
    /// <typeparam name="TKey">Type of the key.</typeparam>
    /// <typeparam name="TValue">Type of the value.</typeparam>
    public class SharedReadExclusiveWriteLockingPolicy<TKey, TValue> : ICacheLockingPolicy<TKey>
    {
        private const int ArbitraryTimeout = 10000;
        private readonly Dictionary<TKey, CacheKeyCurrentLocks> cacheLocks = new Dictionary<TKey, CacheKeyCurrentLocks>();
        private readonly ICache<TKey, TValue> cacheRef;
        private readonly object @lock = new object();
        private readonly int sharedReadCount;

        public SharedReadExclusiveWriteLockingPolicy(ICache<TKey, TValue> cacheRef, int sharedReadCount)
        {
            this.cacheRef = cacheRef;
            this.sharedReadCount = sharedReadCount;
        }

        /// <summary>
        /// Applies the locking policy to acquire a read lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <returns>Whether the lock was acquired.</returns>
        public bool AcquireReadLock(TKey key)
        {
            CacheKeyCurrentLocks cacheKeyLocks;
            lock (this.@lock)
            {
                if (!this.cacheLocks.TryGetValue(key, out cacheKeyLocks))
                {
                    cacheKeyLocks = new CacheKeyCurrentLocks(this.sharedReadCount);
                    this.cacheLocks.Add(key, cacheKeyLocks);
                }
            }

            return cacheKeyLocks.SharedReadsSemaphore.WaitOne(ArbitraryTimeout);
        }

        /// <summary>
        /// Applies the locking policy to release the read lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        public void ReleaseReadLock(TKey key)
        {
            CacheKeyCurrentLocks cacheKeyLocks;
            lock (this.@lock)
            {
                if (!this.cacheLocks.TryGetValue(key, out cacheKeyLocks))
                {
                    throw new InvalidOperationException("Unable to release read lock for key. No lock was ever acquired.");
                }
            }

            cacheKeyLocks.SharedReadsSemaphore.Release();
        }

        /// <summary>
        /// Acquires a read lock, does an action, then releases the lock.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="do">The action to take with the lock.</param>
        public void WithReadLockDo(TKey key, Action @do)
        {
            this.AcquireReadLock(key);

            try
            {
                @do();
            }
            finally
            {
                this.ReleaseReadLock(key);
            }
        }

        /// <summary>
        /// Applies the locking policy to acquire a write lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <returns>Whether the lock was acquired.</returns>
        public bool AcquireWriteLock(TKey key)
        {
            CacheKeyCurrentLocks cacheKeyLocks;
            lock (this.@lock)
            {
                if (!this.cacheLocks.TryGetValue(key, out cacheKeyLocks))
                {
                    cacheKeyLocks = new CacheKeyCurrentLocks(this.sharedReadCount);
                    this.cacheLocks.Add(key, cacheKeyLocks);
                }
            }

            cacheKeyLocks.ExclusiveLockRequested.Wait();
            return cacheKeyLocks.ExclusiveWriteMutex.WaitOne(ArbitraryTimeout);
        }

        /// <summary>
        /// Applies the locking policy to release the write lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        public void ReleaseWriteLock(TKey key)
        {
            CacheKeyCurrentLocks cacheKeyLocks;
            lock (this.@lock)
            {
                if (!this.cacheLocks.TryGetValue(key, out cacheKeyLocks))
                {
                    throw new InvalidOperationException("Unable to release write lock for key. No write lock was ever acquired.");
                }
            }

            cacheKeyLocks.ExclusiveWriteMutex.ReleaseMutex();
        }

        /// <summary>
        /// Acquires a write lock, does an action, then releases the lock.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="do">The action to take with the lock.</param>
        public void WithWriteLockDo(TKey key, Action @do)
        {
            this.AcquireWriteLock(key);

            try
            {
                @do();
            }
            finally
            {
                this.ReleaseWriteLock(key);
            }
        }

        /// <summary>
        /// </summary>
        private class CacheKeyCurrentLocks
        {
            public CacheKeyCurrentLocks(int sharedReadCount)
            {
                this.SharedReadsSemaphore = new Semaphore(sharedReadCount, sharedReadCount);
                this.ExclusiveWriteMutex = new Mutex();
                this.ExclusiveLockRequested = new ManualResetEventSlim();
            }

            public Semaphore SharedReadsSemaphore { get; private set; }
            public Mutex ExclusiveWriteMutex { get; private set; }
            public ManualResetEventSlim ExclusiveLockRequested { get; private set; }
        }
    }
}