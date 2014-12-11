namespace Sporacid.Simplets.Webapp.Tools.Factories.Exceptions
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    public class FactoryConfigurationException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public FactoryConfigurationException(string message)
            : this(message, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public FactoryConfigurationException(string message, Exception innerException)
            : base("The factory couldn't be configured. " + message, innerException)
        {
        }
    }
}
