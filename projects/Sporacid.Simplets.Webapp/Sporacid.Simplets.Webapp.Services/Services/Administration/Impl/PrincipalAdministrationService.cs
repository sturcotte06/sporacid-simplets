namespace Sporacid.Simplets.Webapp.Services.Services.Administration.Impl
{
    using System;
    using System.Web;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security.Authorization;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Core.Security.Database;
    using Sporacid.Simplets.Webapp.Services.Resources.Exceptions;

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
        public Boolean PrincipalExists(String identity)
        {
            return this.principalRepository.Has(principal => principal.Identity == identity);
        }

        /// <summary>
        /// Creates a principal in the system.
        /// </summary>
        /// <param name="identity">The principal's identity.</param>
        /// <returns>The created principal id.</returns>
        public Int32 CreatePrincipal(String identity)
        {
            // Cannot add the same context twice.
            if (this.PrincipalExists(identity))
            {
                throw new NotAuthorizedException(String.Format(ExceptionStrings.Services_Security_PrincipalDuplicate, identity));
            }

            // Add the new principal.
            var principalEntity = new Principal {Identity = identity};
            this.principalRepository.Add(principalEntity);

            // Create the new personnal context of this principal. The principal has full access over its context.
            this.contextAdministrationService.CreateContext(principalEntity.Identity);
            this.contextAdministrationService.RemoveAllClaimsFromPrincipal(principalEntity.Identity, HttpContext.Current.User.Identity.Name);
            this.contextAdministrationService.BindRoleToPrincipal(principalEntity.Identity, SecurityConfig.Role.Administrateur.ToString(), principalEntity.Identity);
            
            // Create the base profil for the new principal.
            this.profilAdministrationService.CreateBaseProfil(identity);
            return principalEntity.Id;
        }
    }
}