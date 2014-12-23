namespace Sporacid.Simplets.Webapp.Core.Security.Authorization.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Data.Linq.SqlClient;
    using System.Linq;
    using System.Security.Principal;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Authorization;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Repositories;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Core.Resources.Exceptions;
    using Sporacid.Simplets.Webapp.Core.Security.Database;
    using Sporacid.Simplets.Webapp.Tools;
    using Sporacid.Simplets.Webapp.Tools.Collections;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class AuthorizationModule : IAuthorizationModule
    {
        private readonly IRepository<Int32, Context> contextRepository;
        private readonly IRepository<Int32, Module> moduleRepository;
        private readonly IRepository<Int32, Principal> principalRepository;

        public AuthorizationModule(IRepository<Int32, Principal> principalRepository,
            IRepository<Int32, Context> contextRepository, IRepository<Int32, Module> moduleRepository)
        {
            this.principalRepository = principalRepository;
            this.contextRepository = contextRepository;
            this.moduleRepository = moduleRepository;
        }

        /// <summary>
        /// Authorizes an authenticated principal for the given module and contexts. If the user is not what
        /// he claims to be, an authorization exception will be raised.
        /// </summary>
        /// <param name="principal">The principal of an authenticated user.</param>
        /// <param name="claims">What the user claims to be authorized to do on the module and context.</param>
        /// <param name="module">The module name which the user tries to access.</param>
        /// <param name="contexts">The context name which the user tries to access.</param>
        public void Authorize(IPrincipal principal, Claims claims, string module, params String[] contexts)
        {
            if (claims == Claims.None)
            {
                // Do not bother to authorize. 
                // User is trying to take a meaningless action that requires no claim.
                return;
            }

            // Get the principal. If this throws, it's probably because the principal does not exist. Cannot authorize.
            var principalEntity = Snippets.TryCatch<Principal, RepositoryException>(() =>
                this.principalRepository.GetUnique(p => SqlMethods.Like(p.Identity, principal.Identity.Name)),
                ex => { throw new NotAuthorizedException(ExceptionStrings.Core_Exceptions_Security_PrincipalDoesNotExist); });

            // Get the module. If this throws, it's probably because the module does not exist. Cannot authorize.
            var moduleEntity = Snippets.TryCatch<Module, RepositoryException>(() =>
                this.moduleRepository.GetUnique(m => SqlMethods.Like(m.Name, module)),
                ex => { throw new NotAuthorizedException(ExceptionStrings.Core_Exceptions_Security_ModuleDoesNotExist); });

            // Get the module. If this throws, it's probably because the module does not exist. Cannot authorize.
            var contextEntities = this.contextRepository.GetAll(c => contexts.Contains(c.Name));

            // Check if we found all contexts. If one is missing, cannot authorize.
            if (contextEntities.Count() != contexts.Count())
            {
                throw new NotAuthorizedException(ExceptionStrings.Core_Exceptions_Security_ContextsDoNotAllExist);
            }

            contextEntities.ForEach(contextEntity =>
            {
                // Check if there is a PrincipalModuleContextClaims entity for this context, this principal and this module.
                var principalClaimsOnContextAndModuleEntity = principalEntity.PrincipalModuleContextClaims
                    .SingleOrDefault(pmcc => pmcc.ContextId == contextEntity.Id && pmcc.ModuleId == moduleEntity.Id);

                if (principalClaimsOnContextAndModuleEntity == null)
                {
                    // The principal is not subscribed to this context or this module.
                    throw new NotAuthorizedException(ExceptionStrings.Core_Exceptions_Security_UnauthorizedModuleContextsAccess);
                }

                // Get claims flag from the entity.
                var principalClaimsOnContextAndModule = (Claims) principalClaimsOnContextAndModuleEntity.Claims;
                if ((principalClaimsOnContextAndModule & claims) != claims)
                {
                    // User does not have every required claims.
                    throw new NotAuthorizedException(ExceptionStrings.Core_Exceptions_Security_PrincipalModuleContextsClaimsInsufficient);
                }
            });
        }
    }
}