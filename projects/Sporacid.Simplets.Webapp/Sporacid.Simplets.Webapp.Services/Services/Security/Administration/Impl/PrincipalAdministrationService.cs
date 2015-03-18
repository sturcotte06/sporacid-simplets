namespace Sporacid.Simplets.Webapp.Services.Services.Security.Administration.Impl
{
    using System;
    using System.Web;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Core.Exceptions;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Repositories;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security.Authorization;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Core.Security.Database;
    using Sporacid.Simplets.Webapp.Services.Resources.Exceptions;
    using Sporacid.Simplets.Webapp.Services.Services.Userspace.Administration;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class PrincipalAdministrationService : BaseSecureService, IPrincipalAdministrationService
    {
        private readonly IContextAdministrationService contextAdministrationService;
        private readonly IRepository<Int32, Principal> principalRepository;
        private readonly IProfilAdministrationService profilAdministrationService;

        public PrincipalAdministrationService(IContextAdministrationService contextAdministrationService, IProfilAdministrationService profilAdministrationService,
            IRepository<Int32, Principal> principalRepository)
        {
            this.contextAdministrationService = contextAdministrationService;
            this.profilAdministrationService = profilAdministrationService;
            this.principalRepository = principalRepository;
        }

        /// <summary>
        /// Get whether the principal exists.
        /// </summary>
        /// <param name="identity">The principal's identity.</param>
        /// <exception cref="RepositoryException">
        /// If something unexpected occurs while trying to get whether the principal exists.
        /// </exception>
        /// <exception cref="CoreException">
        /// If something unexpected occurs.
        /// </exception>
        /// <returns>Whether the principal exists.</returns>
        public Boolean Exists(String identity)
        {
            return this.principalRepository.Has(principal => principal.Identity == identity);
        }

        /// <summary>
        /// Creates a principal in the system.
        /// Creating a principal will creates its base profile, its personnal security context and will give all rights
        /// to the principal on its context.
        /// </summary>
        /// <param name="identity">The principal's identity.</param>
        /// <exception cref="NotAuthorizedException">
        /// If the principal already exists.
        /// </exception>
        /// <exception cref="RepositoryException">
        /// If something unexpected occurs while creating the principal.
        /// </exception>
        /// <exception cref="CoreException">
        /// If something unexpected occurs.
        /// </exception>
        /// <returns>The created principal id.</returns>
        public Int32 Create(String identity)
        {
            // Cannot add the same context twice.
            if (this.Exists(identity))
            {
                throw new NotAuthorizedException(String.Format(ExceptionStrings.Services_Security_PrincipalDuplicate, identity));
            }

            // Add the new principal.
            var principalEntity = new Principal {Identity = identity};
            this.principalRepository.Add(principalEntity);

            // Create the new personnal context of this principal. The principal has full access over its context.
            this.contextAdministrationService.Create(principalEntity.Identity, principalEntity.Identity);
            this.contextAdministrationService.RemoveAllClaimsFromPrincipal(principalEntity.Identity, principalEntity.Identity);
            this.contextAdministrationService.BindRoleToPrincipal(principalEntity.Identity, SecurityConfig.Role.Administrateur.ToString(), principalEntity.Identity);

            // Create the base profil for the new principal.
            this.profilAdministrationService.CreateBaseProfil(identity);
            return principalEntity.Id;
        }
    }
}