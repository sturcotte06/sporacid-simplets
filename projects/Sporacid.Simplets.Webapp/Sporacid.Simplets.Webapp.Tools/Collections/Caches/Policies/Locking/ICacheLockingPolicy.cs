namespace Sporacid.Simplets.Webapp.Tools.Collections.Caches.Policies.Locking
{
    using System;

    public interface ICacheLockingPolicy<in TKey>
    {
        /// <summary>
        /// Applies the locking policy to acquire a read lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <returns>Whether the lock was acquired.</returns>
        bool AcquireReadLock(TKey key);

        /// <summary>
        /// Applies the locking policy to release the read lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        void ReleaseReadLock(TKey key);

        /// <summary>
        /// Acquires a read lock, does an action, then releases the lock.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="do">The action to take with the lock.</param>
        void WithReadLockDo(TKey key, Action @do);

        /// <summary>
        /// Applies the locking policy to acquire a write lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <returns>Whether the lock was acquired.</returns>
        bool AcquireWriteLock(TKey key);

        /// <summary>
        /// Applies the locking policy to release the write lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        void ReleaseWriteLock(TKey key);

        /// <summary>
        /// Acquires a write lock, does an action, then releases the lock.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="do">The action to take with the lock.</param>
        void WithWriteLockDo(TKey key, Action @do);
    }
}