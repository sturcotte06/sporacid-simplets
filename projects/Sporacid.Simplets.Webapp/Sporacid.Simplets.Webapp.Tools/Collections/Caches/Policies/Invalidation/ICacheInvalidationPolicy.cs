namespace Sporacid.Simplets.Webapp.Tools.Collections.Caches.Policies.Invalidation
{
    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface ICacheInvalidationPolicy<TKey, TValue> : ICachePolicy<TKey, TValue>
    {
        /// <summary>
        /// Event when invalidation occurs.
        /// </summary>
        event OnInvalidateHandler<TKey, TValue> OnInvalidate;
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public delegate void OnInvalidateHandler<in TKey, in TValue>(TKey key, TValue value);
}