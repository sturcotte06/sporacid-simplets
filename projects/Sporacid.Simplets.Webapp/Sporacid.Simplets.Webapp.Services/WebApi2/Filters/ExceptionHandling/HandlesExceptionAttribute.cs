namespace Sporacid.Simplets.Webapp.Services.WebApi2.Filters.ExceptionHandling
{
    using System;
    using System.Web.Http.Filters;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class HandlesExceptionAttribute : ExceptionFilterAttribute
    {
    }
}