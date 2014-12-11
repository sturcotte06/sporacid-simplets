namespace Sporacid.Simplets.Webapp.Tools.Collections.Caches.Policies.Locking
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public abstract class BaseLockingPolicy<TKey, TValue> : BasePolicy<TKey, TValue>, ICacheLockingPolicy<TKey, TValue>
    {
        protected static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(30);

        /// <summary>
        /// Applies the locking policy to acquire a read lock on the key.
        /// This method will never timeout.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <returns>Whether the lock was acquired.</returns>
        public abstract void AcquireReadLock(TKey key);

        /// <summary>
        /// Applies the locking policy to acquire a read lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="timeout">Timespan before timeout. If timeout is reached, the method will throw.</param>
        /// <returns>Whether the lock was acquired.</returns>
        public abstract void AcquireReadLock(TKey key, TimeSpan timeout);

        /// <summary>
        /// Applies the locking policy to try to acquire a read lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="timeout">Timespan before timeout. If timeout is reached, the method will throw.</param>
        /// <returns>Whether the lock was acquired.</returns>
        public abstract bool TryAcquireReadLock(TKey key, TimeSpan timeout);

        /// <summary>
        /// Applies the locking policy to release the read lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        public abstract void ReleaseReadLock(TKey key);

        /// <summary>
        /// Acquires a read lock, does an action, then releases the lock.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="do">The action to take with the lock.</param>
        public void WithReadLockDo(TKey key, Action @do)
        {
            this.WithReadLockDo(key, @do, TimeSpan.FromSeconds(30));
        }

        /// <summary>
        /// Acquires a read lock, does an action, then releases the lock.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="do">The action to take with the lock.</param>
        /// <param name="timeout">Timespan before acquire lock timeout.</param>
        public void WithReadLockDo(TKey key, Action @do, TimeSpan timeout)
        {
            this.AcquireReadLock(key, timeout);

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
        /// This method will never timeout.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <returns>Whether the lock was acquired.</returns>
        public abstract void AcquireWriteLock(TKey key);

        /// <summary>
        /// Applies the locking policy to acquire a write lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="timeout">Timespan before timeout. If timeout is reached, the method will throw.</param>
        /// <returns>Whether the lock was acquired.</returns>
        public abstract void AcquireWriteLock(TKey key, TimeSpan timeout);

        /// <summary>
        /// Applies the locking policy to try to acquire a write lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="timeout">Timespan before timeout. If timeout is reached, the method will throw.</param>
        /// <returns>Whether the lock was acquired.</returns>
        public abstract bool TryAcquireWriteLock(TKey key, TimeSpan timeout);

        /// <summary>
        /// Applies the locking policy to release the write lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        public abstract void ReleaseWriteLock(TKey key);

        /// <summary>
        /// Acquires a write lock, does an action, then releases the lock.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="do">The action to take with the lock.</param>
        /// <returns>Whether a lock could be acquired and the action could be done.</returns>
        public void WithWriteLockDo(TKey key, Action @do)
        {
            this.WithWriteLockDo(key, @do, TimeSpan.FromSeconds(30));
        }

        /// <summary>
        /// Acquires a write lock, does an action, then releases the lock.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="do">The action to take with the lock.</param>
        /// <param name="timeout">Timespan before acquire lock timeout.</param>
        public void WithWriteLockDo(TKey key, Action @do, TimeSpan timeout)
        {
            this.AcquireWriteLock(key, timeout);

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
        /// What to do before the Has() method of the cache.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <returns>Whether an object is cached for the given key.</returns>
        public override void BeforeHas(TKey key)
        {
            base.BeforeHas(key);
            this.AcquireReadLock(key);
        }

        /// <summary>
        /// What to do after the Has() method of the cache.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <returns>Whether an object is cached for the given key.</returns>
        public override void AfterHas(TKey key)
        {
            base.AfterHas(key);
            this.ReleaseReadLock(key);
        }

        /// <summary>
        /// What to do before the Put() method of the cache.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <param name="value">The object to cache.</param>
        public override void BeforePut(TKey key, TValue value)
        {
            base.BeforePut(key, value);
            this.AcquireWriteLock(key);
        }

        /// <summary>
        /// What to do after the Put() method of the cache.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <param name="value">The object to cache.</param>
        public override void AfterPut(TKey key, TValue value)
        {
            base.AfterPut(key, value);
            this.ReleaseWriteLock(key);
        }

        /// <summary>
        /// What to do before the WithValueDo() method of the cache.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <param name="do">The action to take</param>
        public override void BeforeWithValueDo(TKey key, Action<TValue> @do)
        {
            base.BeforeWithValueDo(key, @do);
            this.AcquireReadLock(key);
        }

        /// <summary>
        /// What to do after the WithValueDo() method of the cache.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <param name="do">The action to take</param>
        public override void AfterWithValueDo(TKey key, Action<TValue> @do)
        {
            base.AfterWithValueDo(key, @do);
            this.ReleaseReadLock(key);
        }

        /// <summary>
        /// What to do before the Remove() method of the cache.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <returns>Whether the removal was successful.</returns>
        public override bool BeforeRemove(TKey key)
        {
            var successul = base.BeforeRemove(key);
            this.AcquireWriteLock(key);
            return successul;
        }

        /// <summary>
        /// What to do after the Remove() method of the cache.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <returns>Whether the removal was successful.</returns>
        public override bool AfterRemove(TKey key)
        {
            var successul = base.AfterRemove(key);
            this.ReleaseWriteLock(key);
            return successul;
        }
    }
}