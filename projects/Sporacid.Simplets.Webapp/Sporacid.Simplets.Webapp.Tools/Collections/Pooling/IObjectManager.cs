namespace Sporacid.Simplets.Webapp.Tools.Collections.Pooling
{
    /// <summary>
    /// Interface for all object managers.
    /// </summary>
    /// <typeparam name="TObject">Type of the object to manage.</typeparam>
    /// <author>Simon Turcotte-Langevin</author>
    public interface IObjectManager<TObject> where TObject : class
    {
        /// <summary>
        /// Get a new instance of the TObject class.
        /// </summary>
        /// <returns>A new instance of the TObject class.</returns>
        TObject GetObject();

        /// <summary>
        /// Clean the instance for next threads that will
        /// use it.
        /// </summary>
        /// <param name="instance">The instance to clean.</param>
        void CleanObject(TObject instance);

        /// <summary>
        /// Dispose of the object.
        /// </summary>
        /// <param name="instance">The instance to dispose.</param>
        void DisposeObject(TObject instance);
    }
}