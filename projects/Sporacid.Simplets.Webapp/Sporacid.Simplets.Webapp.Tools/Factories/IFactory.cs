namespace Sporacid.Simplets.Webapp.Tools.Factories
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <author>Simon Turcotte-Langevin</author>
    public interface IFactory<out TObject> : IDisposable where TObject : class
    {
        /// <summary>
        /// Configure the factory object using the supplied factory configuration.
        /// </summary>
        /// <typeparam name="TFactoryConfiguration">The factory configuration type.</typeparam>
        /// <param name="factoryConfiguration">The factory configuration object.</param>
        void Configure<TFactoryConfiguration>(TFactoryConfiguration factoryConfiguration);

        /// <summary>
        /// Creates a new instance of the object.
        /// </summary>
        /// <returns>A new instance of TObject.</returns>
        TObject Create();
    }
}
