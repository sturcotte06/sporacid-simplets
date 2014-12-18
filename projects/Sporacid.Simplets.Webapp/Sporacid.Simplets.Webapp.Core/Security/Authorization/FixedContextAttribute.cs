namespace Sporacid.Simplets.Webapp.Core.Security.Authorization
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class FixedContextAttribute : Attribute
    {
        public FixedContextAttribute(String name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Name of the resource.
        /// </summary>
        public String Name { get; private set; }
    }
}