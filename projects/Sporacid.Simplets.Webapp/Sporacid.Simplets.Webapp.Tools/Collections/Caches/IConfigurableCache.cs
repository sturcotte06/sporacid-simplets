namespace Sporacid.Simplets.Webapp.Tools.Collections.Caches
{
    using Sporacid.Simplets.Webapp.Tools.Collections.Caches.Policies;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IConfigurableCache<TKey, TValue> : ICache<TKey, TValue>
    {
        /// <summary>
        /// Register a policies in the cache. Because policies have very different behaviour, caches implementation
        /// are responsible of using the policy.
        /// </summary>
        /// <param name="policies">The policies to register.</param>
        void RegisterPolicies(params ICachePolicy<TKey, TValue>[] policies);
    }
}