namespace Sporacid.Simplets.Webapp.Tools.Factories
{
    using System;

    /// <summary>
    /// Attribute for a constructor parameter.
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyAttribute : Attribute
    {
        /// <summary>
        /// The index of the constructor parameter.
        /// </summary>
        public String Name { get; set; }
    }
}