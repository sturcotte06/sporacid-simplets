namespace Sporacid.Simplets.Webapp.Tools.Collections.Pooling
{
    using System;

    /// <summary>
    /// Default object manager to use if no managers were specified.
    /// </summary>
    /// <typeparam name="TObject">Type of the object to provide</typeparam>
    public class DefaultObjectManager<TObject> : IObjectManager<TObject>
        where TObject : class
    {
        /// <summary>
        /// Get a new instance of the TObject class.
        /// </summary>
        /// <returns>A new instance of the TObject class</returns>
        public virtual TObject GetObject()
        {
            // Return an instance using the default empty constructor
            return Activator.CreateInstance<TObject>();
        }

        /// <summary>
        /// Clean the instance for next threads that will
        /// use it.
        /// </summary>
        /// <param name="instance">The instance to clean</param>
        public virtual void CleanObject(TObject instance)
        {
            // No-op
        }

        /// <summary>
        /// Dispose of the object.
        /// </summary>
        /// <param name="instance">The instance to dispose</param>
        public virtual void DisposeObject(TObject instance)
        {
            var disposable = instance as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}