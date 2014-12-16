namespace Sporacid.Simplets.Webapp.Core.Security.Authorization
{
    using System.Security.Principal;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IAuthorizationModule
    {
        /// <summary>
        /// Authorizes an authenticated session in the given context. If the session, and its associated user, does
        /// not have the required authorization level, an exception will be raised.
        /// </summary>
        /// <param name="principal">The principal of an authenticated user.</param>
        /// <param name="resource">The resource the user tries to access.</param>
        IPrincipal Authorize(IPrincipal principal, IResource resource);
    }
}