namespace Sporacid.Simplets.Webapp.Core.Security.Authorization.Impl
{
    using System;
    using System.Security.Principal;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Repositories;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security.Authorization;
    using Sporacid.Simplets.Webapp.Core.Resources.Exceptions;
    using Sporacid.Simplets.Webapp.Core.Security.Database;
    using Sporacid.Simplets.Webapp.Core.Security.Database.Repositories;
    using Sporacid.Simplets.Webapp.Tools;
    using Sporacid.Simplets.Webapp.Tools.Collections;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class AuthorizationModule : IAuthorizationModule
    {
        private readonly ISecurityRepository<PrincipalModuleContextClaimsId, PrincipalModuleContextClaims> claimsRepository;

        public AuthorizationModule(ISecurityRepository<PrincipalModuleContextClaimsId, PrincipalModuleContextClaims> claimsRepository)
        {
            this.claimsRepository = claimsRepository;
        }

        /// <summary>
        /// Authorizes an authenticated principal for the given module and contexts. If the user is not what
        /// he claims to be, an authorization exception will be raised.
        /// </summary>
        /// <param name="principal">The principal of an authenticated user.</param>
        /// <param name="claims">What the user claims to be authorized to do on the module and context.</param>
        /// <param name="module">The module name which the user tries to access.</param>
        /// <param name="contexts">The context name which the user tries to access.</param>
        public void Authorize(IPrincipal principal, Claims claims, String module, params String[] contexts)
        {
            if (claims == Claims.None)
            {
                // Do not bother to authorize. 
                // User is trying to take a meaningless action that requires no claim.
                return;
            }

            // Authorize principal on each contexts.
            contexts.ForEach(context =>
            {
                // Get the claims of the principal on this module and context.
                var claimsEntity = Snippets.TryCatch<PrincipalModuleContextClaims, RepositoryException>(() =>
                    this.claimsRepository.GetUnique(pmcc => pmcc.Principal.Identity == principal.Identity.Name && pmcc.Context.Name == context && pmcc.Module.Name == module),
                    ex => { throw new NotAuthorizedException(ExceptionStrings.Core_Security_UnauthorizedModuleContextsAccess, ex); });

                // Get claims flag from the entity.
                var principalClaimsOnContextAndModule = (Claims) claimsEntity.Claims;
                if ((principalClaimsOnContextAndModule & claims) != claims)
                {
                    // User does not have every required claims.
                    throw new NotAuthorizedException(ExceptionStrings.Core_Security_PrincipalModuleContextsClaimsInsufficient);
                }
            });
        }
    }
}