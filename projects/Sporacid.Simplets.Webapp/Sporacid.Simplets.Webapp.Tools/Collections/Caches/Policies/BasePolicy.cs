namespace Sporacid.Simplets.Webapp.Tools.Collections.Caches.Policies
{
    using System;
    using PostSharp.Patterns.Contracts;
    using Sporacid.Simplets.Webapp.Tools.Collections.Caches.Exceptions;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public abstract class BasePolicy<TKey, TValue> : ICachePolicy<TKey, TValue>
    {
        protected ICache<TKey, TValue> Cache { get; private set; }
        private volatile bool isApplied = false;

        /// <summary>
        /// Applies the policy on the given cache.
        /// </summary>
        /// <param name="cache"></param>
        public void ApplyOn([Required] ICache<TKey, TValue> cache)
        {
            // Policy can be applied.
            this.Cache = cache;
            isApplied = true;
        }

        /// <summary>
        /// What to do before the Has() method of the cache.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <returns>Whether an object is cached for the given key.</returns>
        public virtual void BeforeHas(TKey key)
        {
            if (!isApplied)
            {
                throw new CachingException("Policy was not applied. Cannot proceed with BeforeHas().");
            }
        }

        /// <summary>
        /// What to do after the Has() method of the cache.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <returns>Whether an object is cached for the given key.</returns>
        public virtual void AfterHas(TKey key)
        {
            if (!isApplied)
            {
                throw new CachingException("Policy was not applied. Cannot proceed with AfterHas().");
            }
        }

        /// <summary>
        /// What to do before the Put() method of the cache.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <param name="value">The object to cache.</param>
        public virtual void BeforePut(TKey key, TValue value)
        {
            if (!isApplied)
            {
                throw new CachingException("Policy was not applied. Cannot proceed with BeforePut().");
            }
        }

        /// <summary>
        /// What to do after the Put() method of the cache.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <param name="value">The object to cache.</param>
        public virtual void AfterPut(TKey key, TValue value)
        {
            if (!isApplied)
            {
                throw new CachingException("Policy was not applied. Cannot proceed with AfterPut().");
            }
        }

        /// <summary>
        /// What to do before the WithValueDo() method of the cache.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <param name="do">The action to take</param>
        public virtual void BeforeWithValueDo(TKey key, Action<TValue> @do)
        {
            if (!isApplied)
            {
                throw new CachingException("Policy was not applied. Cannot proceed with BeforeWithValueDo().");
            }
        }

        /// <summary>
        /// What to do after the WithValueDo() method of the cache.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <param name="do">The action to take</param>
        public virtual void AfterWithValueDo(TKey key, Action<TValue> @do)
        {
            if (!isApplied)
            {
                throw new CachingException("Policy was not applied. Cannot proceed with AfterWithValueDo().");
            }
        }

        /// <summary>
        /// What to do before the Remove() method of the cache.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <returns>Whether the removal was successful.</returns>
        public virtual bool BeforeRemove(TKey key)
        {
            if (!isApplied)
            {
                throw new CachingException("Policy was not applied. Cannot proceed with BeforeRemove().");
            }

            return true;
        }

        /// <summary>
        /// What to do after the Remove() method of the cache.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <returns>Whether the removal was successful.</returns>
        public virtual bool AfterRemove(TKey key)
        {
            if (!isApplied)
            {
                throw new CachingException("Policy was not applied. Cannot proceed with AfterRemove().");
            }

            return true;
        }
    }
}