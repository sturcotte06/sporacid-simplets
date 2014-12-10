namespace Sporacid.Simplets.Webapp.Tools.Collections.Caches
{
    using System;
    using System.Collections.Generic;
    using Sporacid.Simplets.Webapp.Tools.Collections.Caches.Policies.Invalidation;
    using Sporacid.Simplets.Webapp.Tools.Collections.Caches.Policies.Locking;

    /// <summary>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class ConfigurableCache<TKey, TValue> : ICache<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> cache = new Dictionary<TKey, TValue>();
        private readonly ICacheInvalidationPolicy<TKey> invalidationPolicy;
        private readonly object @lock = new object();
        private readonly ICacheLockingPolicy<TKey> lockingPolicy;

        /// <summary>
        /// </summary>
        /// <param name="invalidationPolicy"></param>
        /// <param name="lockingPolicy"></param>
        public ConfigurableCache(ICacheInvalidationPolicy<TKey> invalidationPolicy, ICacheLockingPolicy<TKey> lockingPolicy)
        {
            this.invalidationPolicy = invalidationPolicy;
            this.lockingPolicy = lockingPolicy;
        }

        /// <summary>
        /// Returns whether an object is cached for the given key.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <returns>Whether an object is cached for the given key.</returns>
        public bool Has(TKey key)
        {
            lock (@lock)
            {
                return cache.ContainsKey(key);
            }
        }

        /// <summary>
        /// Cache an object into the cache.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <param name="value">The object to cache.</param>
        public void Put(TKey key, TValue value)
        {
            lock (@lock)
            {
                if (cache.ContainsKey(key))
                {
                    throw new InvalidOperationException("Cache key already exists.");
                }

                cache.Add(key, value);
            }
        }

        /// <summary>
        /// Takes an action on a cached value.
        /// An exclusive lock will be acquired while the action is taken.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <param name="do">The action to take</param>
        public void WithLockedValueDo(TKey key, Action<TValue> @do)
        {
            lock (@lock)
            {
                lockingPolicy.WithReadLockDo(key, () =>
                {
                    TValue value;
                    if (!cache.TryGetValue(key, out value))
                    {
                        throw new InvalidOperationException("Couldn't retrieve cached value for key.");
                    }

                    @do(value);
                });
            }
        }

        /// <summary>
        /// Remove the cached object for the given key.
        /// </summary>
        /// <param name="key">The key object.</param>
        public void Remove(TKey key)
        {
            lock (@lock)
            {
                lockingPolicy.WithWriteLockDo(key, () =>
                {
                    TValue value;
                    if (!cache.TryGetValue(key, out value))
                    {

                    }

                    // @do(value);
                });
            }
        }
    }
}