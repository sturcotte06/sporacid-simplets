namespace Sporacid.Simplets.Webapp.Services.Services.Administration.Impl
{
    using System;
    using System.Data.Linq.SqlClient;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Core.Security.Database;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/principal")]
    public class PrincipalAdministrationService : BaseService, IPrincipalAdministrationService
    {
        private readonly IContextAdministrationService contextAdministrationService;
        private readonly IRepository<Int32, Principal> principalRepository;
        private readonly IUserspaceAdministrationService profilAdministrationService;

        public PrincipalAdministrationService(IContextAdministrationService contextAdministrationService, IUserspaceAdministrationService profilAdministrationService,
            IRepository<Int32, Principal> principalRepository)
        {
            this.contextAdministrationService = contextAdministrationService;
            this.profilAdministrationService = profilAdministrationService;
            this.principalRepository = principalRepository;
        }

        /// <summary>
        /// Returns whether the principal exists.
        /// </summary>
        /// <param name="identity">The principal's identity.</param>
        /// <returns>Ehether the principal exists.</returns>
        public bool PrincipalExists(String identity)
        {
            return this.principalRepository.Has(p => SqlMethods.Like(identity, p.Identity));
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

            // Create the base profil for the new principal.
            this.profilAdministrationService.CreateBaseProfil(identity);

            return principalEntity.Id;
        }
    }
}