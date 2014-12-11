namespace Sporacid.Simplets.Webapp.Tools.Collections.Caches.Policies.Locking
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Sporacid.Simplets.Webapp.Tools.Collections.Caches.Exceptions;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class ExclusiveLockingPolicy<TKey, TValue> : BaseLockingPolicy<TKey, TValue>
    {
        private readonly object @lock = new object();
        private readonly Dictionary<TKey, Mutex> lockCache = new Dictionary<TKey, Mutex>();

        /// <summary>
        /// Acquires the exclusive lock.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="timeout">Timespan before timeout. If timeout is reached, the method will throw.</param>
        /// <returns>Whether the lock was acquired.</returns>
        private bool TryAcquireExclusiveLock(TKey key, TimeSpan timeout)
        {
            Mutex mutex;
            lock (this.@lock)
            {
                if (!this.lockCache.TryGetValue(key, out mutex))
                {
                    mutex = new Mutex();
                    this.lockCache.Add(key, mutex);
                }
            }

            return mutex.WaitOne(timeout);
        }

        /// <summary>
        /// Acquires the exclusive lock.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="timeout">Timespan before timeout. If timeout is reached, the method will throw.</param>
        /// <returns>Whether the lock was acquired.</returns>
        private void AcquireExclusiveLock(TKey key, TimeSpan timeout)
        {
            if (!this.TryAcquireExclusiveLock(key, timeout))
            {
                throw new CachingException("Couldn't acquire exclusive lock.");
            }
        }

        /// <summary>
        /// Releases the exclusive lock.
        /// </summary>
        /// <param name="key">The cache key.</param>
        private void ReleaseExclusiveLock(TKey key)
        {
            Mutex mutex;
            lock (this.@lock)
            {
                if (!this.lockCache.TryGetValue(key, out mutex))
                {
                    throw new InvalidOperationException("Unable to release exclusive lock for key. No lock was ever acquired.");
                }
            }

            mutex.ReleaseMutex();
        }

        /// <summary>
        /// Applies the locking policy to acquire a read lock on the key.
        /// This method will never timeout.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <returns>Whether the lock was acquired.</returns>
        public override void AcquireReadLock(TKey key)
        {
            this.AcquireReadLock(key, DefaultTimeout);
        }

        /// <summary>
        /// Applies the locking policy to acquire a read lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="timeout">Timespan before timeout. If timeout is reached, the method will throw.</param>
        /// <returns>Whether the lock was acquired.</returns>
        public override void AcquireReadLock(TKey key, TimeSpan timeout)
        {
            this.AcquireExclusiveLock(key, timeout);
        }

        /// <summary>
        /// Applies the locking policy to try to acquire a read lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="timeout">Timespan before timeout. If timeout is reached, the method will throw.</param>
        /// <returns>Whether the lock was acquired.</returns>
        public override bool TryAcquireReadLock(TKey key, TimeSpan timeout)
        {
            return this.TryAcquireExclusiveLock(key, timeout);
        }


        /// <summary>
        /// Applies the locking policy to release the read lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        public override void ReleaseReadLock(TKey key)
        {
            this.ReleaseExclusiveLock(key);
        }

        /// <summary>
        /// Applies the locking policy to acquire a write lock on the key.
        /// This method will never timeout.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <returns>Whether the lock was acquired.</returns>
        public override void AcquireWriteLock(TKey key)
        {
            this.AcquireWriteLock(key, DefaultTimeout);
        }

        /// <summary>
        /// Applies the locking policy to acquire a write lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="timeout">Timespan before timeout. If timeout is reached, the method will throw.</param>
        /// <returns>Whether the lock was acquired.</returns>
        public override void AcquireWriteLock(TKey key, TimeSpan timeout)
        {
            this.AcquireExclusiveLock(key, timeout);
        }

        /// <summary>
        /// Applies the locking policy to try to acquire a write lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="timeout">Timespan before timeout. If timeout is reached, the method will throw.</param>
        /// <returns>Whether the lock was acquired.</returns>
        public override bool TryAcquireWriteLock(TKey key, TimeSpan timeout)
        {
            return this.TryAcquireExclusiveLock(key, timeout);
        }


        /// <summary>
        /// Applies the locking policy to release the write lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        public override void ReleaseWriteLock(TKey key)
        {
            this.ReleaseExclusiveLock(key);
        }
    }
}