namespace Sporacid.Simplets.Webapp.Tools.Collections.Caches.Policies.Invalidation
{
    using System;
    using Sporacid.Simplets.Webapp.Tools.Threading.Timers;

    /// <summary>
    /// </summary>
    /// <typeparam name="TKey">Type of the key.</typeparam>
    /// <typeparam name="TValue">Type of the value.</typeparam>
    public class TimeBasedInvalidationPolicy<TKey, TValue> : ICacheInvalidationPolicy<TKey>
    {
        private readonly ICache<TKey, TValue> cacheRef;
        private readonly TimeSpan validitySpan;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="cacheRef">The cache reference, on which policies are applied.</param>
        /// <param name="validitySpan">The timespan of validity.</param>
        public TimeBasedInvalidationPolicy(ICache<TKey, TValue> cacheRef, TimeSpan validitySpan)
        {
            this.cacheRef = cacheRef;
            this.validitySpan = validitySpan;
        }

        /// <summary>
        /// Applies the caching policy on the given cache key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        public void ApplyInvalidationPolicy(TKey key)
        {
            // After validity span, remove the cached value.
            TimeoutTimer.StartNew(this.validitySpan, (sender, args) => this.cacheRef.Remove(key));
        }
    }
}