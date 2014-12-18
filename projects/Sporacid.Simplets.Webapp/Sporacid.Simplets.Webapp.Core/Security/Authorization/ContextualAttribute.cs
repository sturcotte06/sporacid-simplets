namespace Sporacid.Simplets.Webapp.Core.Security.Authorization
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ContextualAttribute : Attribute
    {
        public ContextualAttribute() : this("context")
        {
            
        }

        public ContextualAttribute(String contextParameterName)
        {
            this.ContextParameterName = contextParameterName;
        }

        /// <summary>
        /// The name of the parameter context.
        /// </summary>
        public String ContextParameterName { get; private set; }
    }
}