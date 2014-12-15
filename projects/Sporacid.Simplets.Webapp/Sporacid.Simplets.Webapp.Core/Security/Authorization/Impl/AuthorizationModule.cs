namespace Sporacid.Simplets.Webapp.Core.Security.Authorization.Impl
{
    using System;
    using System.Security.Principal;
    using Sporacid.Simplets.Webapp.Core.Exceptions;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Authorization;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class AuthorizationModule : IAuthorizationModule
    {
        /// <summary>
        /// Authorizes an anonymous user in the given context. If the context does not allow anonymous requests,
        /// an exception will be raised.
        /// </summary>
        /// <typeparam name="TContextModel">The model object of the context.</typeparam>
        /// <param name="context">The context for which the user must be authorized.</param>
        /// <exception cref="SecurityException" />
        /// <exception cref="NotAuthorizedException">If anonymous action cannot be taken.</exception>
        public void AuthorizeAnonymous<TContextModel>(IContext<TContextModel> context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Authorizes an authenticated session in the given context. If the session, and its associated user, does
        /// not have the required authorization level, an exception will be raised.
        /// </summary>
        /// <typeparam name="TContextModel">The model object of the context.</typeparam>
        /// <param name="principal">The principal of the user.</param>
        /// <param name="context">The context for which the user must be authorized.</param>
        /// <exception cref="SecurityException" />
        /// <exception cref="NotAuthorizedException">If user is unauthorized.</exception>
        public void Authorize<TContextModel>(IPrincipal principal, IContext<TContextModel> context)
        {
            throw new NotImplementedException();
        }
    }
}