namespace Sporacid.Simplets.Webapp.Core.Security.Authorization.Impl
{
    using System;
    using System.Data.Linq.SqlClient;
    using System.Linq;
    using System.Security.Principal;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Authorization;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Tools.Enumerations;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class AuthorizationModule : IAuthorizationModule
    {
        private readonly IRepository<Int32, Database.Context> contextRepository;
        private readonly IRepository<Int32, Database.Module> moduleRepository;
        private readonly IRepository<Int32, Database.Principal> principalRepository;

        public AuthorizationModule(IRepository<Int32, Database.Principal> principalRepository,
            IRepository<Int32, Database.Context> contextRepository, IRepository<Int32, Database.Module> moduleRepository)
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
        public IPrincipal Authorize(IPrincipal principal, Claims claims, string module, params string[] contexts)
        {
            if (claims == Claims.None)
            {
                // Do not bother to authorize. User has no claims and is trying to take a meaningless action.
                return principal;
            }

            var principalEntity = this.principalRepository.GetUnique(p => SqlMethods.Like(p.Identity, principal.Identity.Name));
            var moduleEntity = this.moduleRepository.GetUnique(m => SqlMethods.Like(m.Name, module));
            var contextEntities = this.contextRepository.GetAll(c => contexts.Contains(c.Name));

            foreach (var contextEntity in contextEntities)
            {
                var principalClaimsOnContextAndModule = principalEntity.PrincipalModuleContextClaims
                    .Where(pmcc => pmcc.ContextId == contextEntity.Id && pmcc.ModuleId == moduleEntity.Id)
                    .Select(prc => prc.Claim)
                    .ToArray();


                foreach (var claim in claims.GetFlags().Cast<Claims>().Cast<int>())
                {
                    if (principalClaimsOnContextAndModule.All(pccm => pccm.Value != claim))
                    {
                        // The principal does not have the required claim.
                        throw new NotAuthorizedException();
                    }
                }
            }

            return principal;
        }
    }
}