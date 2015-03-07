namespace Sporacid.Simplets.Webapp.Tools.Collections.Caches.Policies.Locking
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Sporacid.Simplets.Webapp.Tools.Collections.Caches.Exceptions;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class ReaderWriterLockingPolicy<TKey, TValue> : BaseLockingPolicy<TKey, TValue>
    {
        private readonly object @lock = new object();
        private readonly Dictionary<TKey, ReaderWriterLock> lockCache = new Dictionary<TKey, ReaderWriterLock>();
        
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
            ReaderWriterLock readerWriterLock;
            lock (this.@lock)
            {
                if (!this.lockCache.TryGetValue(key, out readerWriterLock))
                {
                    readerWriterLock = new ReaderWriterLock();
                    this.lockCache.Add(key, readerWriterLock);
                }
            }

            try
            {
                readerWriterLock.AcquireReaderLock(timeout);
            }
            catch (ApplicationException ex)
            {
                throw new CachingException("Couldn't acquire read lock.", ex);
            }
        }

        /// <summary>
        /// Applies the locking policy to try to acquire a read lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="timeout">Timespan before timeout. If timeout is reached, the method will throw.</param>
        /// <returns>Whether the lock was acquired.</returns>
        public override bool TryAcquireReadLock(TKey key, TimeSpan timeout)
        {
            ReaderWriterLock readerWriterLock;
            lock (this.@lock)
            {
                if (!this.lockCache.TryGetValue(key, out readerWriterLock))
                {
                    readerWriterLock = new ReaderWriterLock();
                    this.lockCache.Add(key, readerWriterLock);
                }
            }

            try
            {
                readerWriterLock.AcquireReaderLock(timeout);
            }
            catch (ApplicationException)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Applies the locking policy to release the read lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        public override void ReleaseReadLock(TKey key)
        {
            ReaderWriterLock readerWriterLock;
            lock (this.@lock)
            {
                if (!this.lockCache.TryGetValue(key, out readerWriterLock))
                {
                    throw new CachingException("Unable to release read lock for key. No lock was ever acquired.");
                }
            }

            try
            {
                readerWriterLock.ReleaseReaderLock();
            }
            catch (ApplicationException ex)
            {
                throw new CachingException("Unable to release read lock for key. No lock was ever acquired.", ex);
            }
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
            ReaderWriterLock readerWriterLock;
            lock (this.@lock)
            {
                if (!this.lockCache.TryGetValue(key, out readerWriterLock))
                {
                    readerWriterLock = new ReaderWriterLock();
                    this.lockCache.Add(key, readerWriterLock);
                }
            }

            try
            {
                readerWriterLock.AcquireWriterLock(timeout);
            }
            catch (ApplicationException ex)
            {
                throw new CachingException("Couldn't acquire write lock.", ex);
            }
        }

        /// <summary>
        /// Applies the locking policy to try to acquire a write lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="timeout">Timespan before timeout. If timeout is reached, the method will throw.</param>
        /// <returns>Whether the lock was acquired.</returns>
        public override bool TryAcquireWriteLock(TKey key, TimeSpan timeout)
        {
            ReaderWriterLock readerWriterLock;
            lock (this.@lock)
            {
                if (!this.lockCache.TryGetValue(key, out readerWriterLock))
                {
                    readerWriterLock = new ReaderWriterLock();
                    this.lockCache.Add(key, readerWriterLock);
                }
            }

            try
            {
                readerWriterLock.AcquireWriterLock(timeout);
            }
            catch (ApplicationException)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Applies the locking policy to release the write lock on the key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        public override void ReleaseWriteLock(TKey key)
        {
            ReaderWriterLock readerWriterLock;
            lock (this.@lock)
            {
                if (!this.lockCache.TryGetValue(key, out readerWriterLock))
                {
                    throw new CachingException("Unable to release write lock for key. No lock was ever acquired.");
                }
            }

            try
            {
                readerWriterLock.ReleaseReaderLock();
            }
            catch (ApplicationException ex)
            {
                throw new CachingException("Unable to release write lock for key. No lock was ever acquired.", ex);
            }
        }
    }
}