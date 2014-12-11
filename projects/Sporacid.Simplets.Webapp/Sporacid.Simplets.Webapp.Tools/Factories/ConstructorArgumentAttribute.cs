namespace Sporacid.Simplets.Webapp.Tools.Factories
{
    using System;

    /// <summary>
    /// Attribute for a constructor parameter.
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property)]
    public class ConstructorArgumentAttribute : Attribute
    {
        public ConstructorArgumentAttribute(UInt32 index)
        {
            this.Index = index;
        }

        /// <summary>
        /// The index of the constructor parameter.
        /// </summary>
        public UInt32 Index { get; set; }
    }
}