namespace Sporacid.Simplets.Webapp.Tools.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Sporacid.Simplets.Webapp.Tools.Collections;
    using Sporacid.Simplets.Webapp.Tools.Factories.Exceptions;

    /// <summary>
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    public class Factory<TObject> : IFactory<TObject> where TObject : class
    {
        /// <summary>
        /// </summary>
        private ConstructorInfo constructor;

        /// <summary>
        /// </summary>
        private List<Func<object>> constructorParameterEvaluators;

        /// <summary>
        /// 
        /// </summary>
        private List<KeyValuePair<String, Func<object>>> propertyEvaluators;

        /// <summary>
        /// Whether Configure() was called on this object or not.
        /// </summary>
        private volatile bool isConfigured;

        /// <summary>
        /// Whether Dispose() was called on this object or not.
        /// </summary>
        private volatile bool isDisposed;

        /// <summary>
        /// Configure the factory object using the supplied factory configuration.
        /// </summary>
        /// <typeparam name="TFactoryConfiguration">The factory configuration type.</typeparam>
        /// <param name="factoryConfiguration">The factory configuration object.</param>
        public void Configure<TFactoryConfiguration>(TFactoryConfiguration factoryConfiguration)
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (this.isConfigured)
            {
                throw new FactoryConfigurationException("The factory is already configured.");
            }

            if (factoryConfiguration == null)
            {
                throw new ArgumentNullException("factoryConfiguration");
            }

            var tObjectType = typeof (TObject);
            var tFactoryConfigurationType = factoryConfiguration.GetType();

            // Get all properties of the configuration object that have the constructor argument attribute.
            var configurationProperties = tFactoryConfigurationType.GetProperties()
                .Where(property => property.GetCustomAttribute<ConstructorArgumentAttribute>() != null)
                .OrderBy(property => property.GetCustomAttribute<ConstructorArgumentAttribute>().Index)
                .ToArray();

            // Resolve which constructor should be called. The constructor that reflect the most
            // the configuration object will be taken.
            var objectConstructors = tObjectType.GetConstructors().Where(ctor => ctor.GetParameters().Any());

            // Default best constructor is the parameterless constructor (or null if it does not exist).
            this.constructor = tObjectType.GetConstructors().FirstOrDefault(ctor => ctor.GetParameters().None());

            var bestRatio = 0f;
            foreach (var objectConstructor in objectConstructors)
            {
                var iProperty = 0;
                var objectConstructorParameters = objectConstructor.GetParameters();
                foreach (var objectConstructorParameter in objectConstructorParameters)
                {
                    if (iProperty >= configurationProperties.Length)
                    {
                        // We have more parameters defined in the config than in this ctor,
                        // and we have no more ctor parameter to test against.
                        break;
                    }

                    var configurationPropertyType = configurationProperties[iProperty].PropertyType;
                    if (objectConstructorParameter.ParameterType.IsAssignableFrom(configurationPropertyType))
                    {
                        // The current ctor parameter has the same type as the current config property type.
                        iProperty++;

                        // Check if we're at the end of the ctor parameter list.
                        if (objectConstructorParameters.Length == iProperty)
                        {
                            // Check if the ratio of satisfied parameters on total number available in config
                            // is better than the last ratio.
                            var ratio = (float) iProperty/configurationProperties.Count();
                            if (ratio > bestRatio)
                            {
                                // If so, we have a better candidate for being the right ctor.
                                this.constructor = objectConstructor;
                                bestRatio = ratio;

                                break;
                            }
                        }
                    }
                }
            }

            if (this.constructor == null)
            {
                // We were unable to find a callable constructor.
                throw new FactoryConfigurationException("Impossible to find a suitable constructor.");
            }

            // Prepare lambda to evaluate every parameter value.
            var constructorParameterCount = this.constructor.GetParameters().Count();
            this.constructorParameterEvaluators = new List<Func<object>>(constructorParameterCount);
            for (var iParam = 0; iParam < constructorParameterCount; iParam++)
            {
                // Copy the value of iParam because of closure.
                var iDummy = iParam;

                this.constructorParameterEvaluators.Add(() => configurationProperties[iDummy].GetValue(factoryConfiguration));
            }
            
            // Get all properties of the configuration object that have the constructor argument attribute.
            configurationProperties = tFactoryConfigurationType.GetProperties()
                .Where(property => property.GetCustomAttribute<PropertyAttribute>() != null)
                .ToArray();

            foreach (var configurationProperty in configurationProperties)
            {
                var propertyAttr = (PropertyAttribute) (configurationProperty.GetCustomAttributes(typeof (PropertyAttribute)).First());
                var property = tObjectType.GetProperties().FirstOrDefault(p => (propertyAttr.Name ?? configurationProperty.Name) == p.Name);

                if (property == null)
                {
                    // Error in configuration, but has no side effect.
                    continue;
                }

                // Copy the value of configurationProperty because of closure.
                var dummyConfigurationProperty = configurationProperty;
                this.propertyEvaluators.Add(new KeyValuePair<string, Func<object>>(property.Name, () => dummyConfigurationProperty.GetValue(factoryConfiguration)));
            }

            // The factory has been configured successfully.
            this.isConfigured = true;
        }

        /// <summary>
        /// Creates a new instance of the object.
        /// </summary>
        /// <returns>A new instance of TObject.</returns>
        public TObject Create()
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (!this.isConfigured)
            {
                throw new FactoryConfigurationException("The factory is not configured.");
            }

            var iParameter = 0;
            var parameters = new object[this.constructorParameterEvaluators.Count()];

            // Evaluate the configuration values at the present moment.
            foreach (var constructorParameterEvaluator in this.constructorParameterEvaluators)
            {
                parameters[iParameter++] = constructorParameterEvaluator();
            }

            try
            {
                // Invoke the constructor with the parameter.
                return (TObject) this.constructor.Invoke(parameters);
            }
            catch (Exception ex)
            {
                throw new ObjectCreationException(ex);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with
        /// freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.isDisposed = true;
            this.constructor = null;
            this.constructorParameterEvaluators = null;
        }
    }
}