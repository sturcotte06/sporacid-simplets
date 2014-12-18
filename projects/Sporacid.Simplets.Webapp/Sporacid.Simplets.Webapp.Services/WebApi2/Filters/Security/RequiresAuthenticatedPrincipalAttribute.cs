namespace Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Security
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class RequiresAuthenticatedPrincipalAttribute : System.Attribute
    {
    }
}