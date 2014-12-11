namespace Sporacid.Simplets.Webapp.Tools.Collections.Caches.Policies
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface ICachePolicy<TKey, TValue>
    {
        /// <summary>
        /// Applies the policy on the given cache.
        /// </summary>
        /// <param name="cache"></param>
        void ApplyOn(ICache<TKey, TValue> cache);

        /// <summary>
        /// What to do before the Has() method of the cache.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <returns>Whether an object is cached for the given key.</returns>
        void BeforeHas(TKey key);

        /// <summary>
        /// What to do after the Has() method of the cache.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <returns>Whether an object is cached for the given key.</returns>
        void AfterHas(TKey key);

        /// <summary>
        /// What to do before the Put() method of the cache.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <param name="value">The object to cache.</param>
        void BeforePut(TKey key, TValue value);

        /// <summary>
        /// What to do after the Put() method of the cache.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <param name="value">The object to cache.</param>
        void AfterPut(TKey key, TValue value);

        /// <summary>
        /// What to do before the WithValueDo() method of the cache.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <param name="do">The action to take</param>
        void BeforeWithValueDo(TKey key, Action<TValue> @do);

        /// <summary>
        /// What to do after the WithValueDo() method of the cache.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <param name="do">The action to take</param>
        void AfterWithValueDo(TKey key, Action<TValue> @do);

        /// <summary>
        /// What to do before the Remove() method of the cache.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <returns>Whether the removal was successful.</returns>
        bool BeforeRemove(TKey key);

        /// <summary>
        /// What to do after the Remove() method of the cache.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <returns>Whether the removal was successful.</returns>
        bool AfterRemove(TKey key);
    }
}