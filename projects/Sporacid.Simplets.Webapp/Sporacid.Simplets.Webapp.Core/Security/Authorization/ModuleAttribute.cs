namespace Sporacid.Simplets.Webapp.Core.Security.Authorization
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ModuleAttribute : Attribute
    {
        public ModuleAttribute(String name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Name of the module.
        /// </summary>
        public String Name { get; private set; }
    }
}