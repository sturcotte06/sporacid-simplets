namespace Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Authentication
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AuthenticatedAttribute : Attribute
    {
    }
}