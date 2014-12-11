namespace Sporacid.Simplets.Webapp.Tools.Collections.Caches.Policies.Invalidation
{
    using System;
    using Sporacid.Simplets.Webapp.Tools.Threading.Timers;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class TimeBasedInvalidationPolicy<TKey, TValue> : BasePolicy<TKey, TValue>, ICacheInvalidationPolicy<TKey, TValue>
    {
        private readonly TimeSpan validitySpan;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="validitySpan">The timespan of validity.</param>
        public TimeBasedInvalidationPolicy(TimeSpan validitySpan)
        {
            this.validitySpan = validitySpan;
            this.OnInvalidate += (key, value) => this.Cache.Remove(key);
        }

        /// <summary>
        /// What to do after the Put() method of the cache.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <param name="value">The object to cache.</param>
        public override void AfterPut(TKey key, TValue value)
        {
            // After validity span, remove the cached value.
            TimeoutTimer.StartNew(this.validitySpan, (sender, args) => this.OnInvalidate(key, value));
        }

        /// <summary>
        /// Event when invalidation occurs.
        /// </summary>
        public event OnInvalidateHandler<TKey, TValue> OnInvalidate;
    }
}