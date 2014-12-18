namespace Sporacid.Simplets.Webapp.Services.Services.Impl
{
    using System;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Core.Security.Database;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix("api/v1")]
    public class PrincipalService : BaseService, IPrincipalService
    {
        private readonly IContextAdministrationService contextAdministrationService;
        private readonly IRepository<Int32, Principal> principalRepository;

        public PrincipalService(IContextAdministrationService contextAdministrationService, IRepository<Int32, Principal> principalRepository)
        {
            this.contextAdministrationService = contextAdministrationService;
            this.principalRepository = principalRepository;
        }

        /// <summary>
        /// Creates a principal in the system.
        /// </summary>
        /// <param name="identity">The principal's identity.</param>
        public Int32 CreatePrincipal(String identity)
        {
            // Add the new principal.
            var principalEntity = new Principal {Identity = identity};
            this.principalRepository.Add(principalEntity);

            // Create the new personnal context of this principal. The principal has full access over its context.
            this.contextAdministrationService.CreateContext(principalEntity.Identity);
            this.contextAdministrationService.BindRoleToPrincipal(principalEntity.Identity, SecurityConfig.Role.Administrateur.ToString(), principalEntity.Identity);
            return principalEntity.Id;
        }
    }
}