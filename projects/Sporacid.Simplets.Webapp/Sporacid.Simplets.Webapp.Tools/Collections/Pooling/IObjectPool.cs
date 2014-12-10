namespace Sporacid.Simplets.Webapp.Tools.Collections.Pooling
{
    using System;

    /// <summary>
    /// Interface for all object pools.
    /// </summary>
    /// <typeparam name="TObject">The object type on which we pool</typeparam>
    /// <author>Simon Turcotte-Langevin</author>
    public interface IObjectPool<out TObject> : IDisposable where TObject : class
    {
        /// <summary>
        /// Do an action with an object from the pool.
        /// The object will be exclusive to the thread calling this method.
        /// </summary>
        /// <param name="action">The action to take with the object</param>
        void WithObject(Action<TObject> action);
    }
}