namespace Sporacid.Simplets.Webapp.Tools.Collections.Caches.Policies.Locking
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface ICacheLockingPolicy<TKey, TValue> : ICachePolicy<TKey, TValue>
    {
        /// <summary>
        /// Applies the locking policy to acquire a read lock on the key.
        /// This method will never timeout.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <returns>Whether the lock was acquired.</returns>
        void AcquireReadLock(TKey key);

        /// <summary>
        /// Applies the locking policy to acquire a read lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="timeout">Timespan before timeout. If timeout is reached, the method will throw.</param>
        /// <returns>Whether the lock was acquired.</returns>
        void AcquireReadLock(TKey key, TimeSpan timeout);

        /// <summary>
        /// Applies the locking policy to try to acquire a read lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="timeout">Timespan before timeout. If timeout is reached, the method will throw.</param>
        /// <returns>Whether the lock was acquired.</returns>
        bool TryAcquireReadLock(TKey key, TimeSpan timeout);

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
        /// Acquires a read lock, does an action, then releases the lock.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="do">The action to take with the lock.</param>
        /// <param name="timeout">Timespan before acquire lock timeout.</param>
        void WithReadLockDo(TKey key, Action @do, TimeSpan timeout);

        /// <summary>
        /// Applies the locking policy to acquire a write lock on the key.
        /// This method will never timeout.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <returns>Whether the lock was acquired.</returns>
        void AcquireWriteLock(TKey key);

        /// <summary>
        /// Applies the locking policy to acquire a write lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="timeout">Timespan before timeout. If timeout is reached, the method will throw.</param>
        /// <returns>Whether the lock was acquired.</returns>
        void AcquireWriteLock(TKey key, TimeSpan timeout);

        /// <summary>
        /// Applies the locking policy to try to acquire a write lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="timeout">Timespan before timeout. If timeout is reached, the method will throw.</param>
        /// <returns>Whether the lock was acquired.</returns>
        bool TryAcquireWriteLock(TKey key, TimeSpan timeout);

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

        /// <summary>
        /// Acquires a write lock, does an action, then releases the lock.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="do">The action to take with the lock.</param>
        /// <param name="timeout">Timespan before acquire lock timeout.</param>
        void WithWriteLockDo(TKey key, Action @do, TimeSpan timeout);
    }
}