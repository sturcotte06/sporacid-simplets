namespace Sporacid.Simplets.Webapp.Tools.Collections.Caches.Policies.Invalidation
{
    public interface ICacheInvalidationPolicy<in TKey>
    {
        /// <summary>
        /// Applies the caching policy on the given cache key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        void ApplyInvalidationPolicy(TKey key);
    }
}