namespace Sporacid.Simplets.Webapp.Services.Services.Userspace.Administration.Impl
{
    using System;
    using Sporacid.Simplets.Webapp.Core.Exceptions;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Repositories;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security.Authorization;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Core.Security.Ldap;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Resources.Exceptions;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class ProfilAdministrationController : BaseSecureService, IProfilAdministrationService
    {
        private readonly ILdapSearcher ldapSearcher;
        private readonly IRepository<Int32, Profil> profilRepository;

        public ProfilAdministrationController(ILdapSearcher ldapSearcher, IRepository<Int32, Profil> profilRepository)
        {
            this.profilRepository = profilRepository;
            this.ldapSearcher = ldapSearcher;
        }

        /// <summary>
        /// Creates the base profil entity for a given principal's identity.
        /// Every available informations for the principal will be extracted and included in the profil entity.
        /// </summary>
        /// <param name="identity">The principal's identity.</param>
        /// <exception cref="NotAuthorizedException">
        /// If the profil entity already exists.
        /// </exception>
        /// <exception cref="RepositoryException">
        /// If something unexpected occurs while creating the base profil entity.
        /// </exception>
        /// <exception cref="CoreException">
        /// If something unexpected occurs.
        /// </exception>
        /// <returns>The id of the created profil entity.</returns>
        public Int32 CreateBaseProfil(String identity)
        {
            // Cannot add the profil twice.
            if (this.profilRepository.Has(profil => profil.CodeUniversel == identity))
            {
                throw new NotAuthorizedException(String.Format(ExceptionStrings.Services_Security_ProfilDuplicate, identity));
            }

            // Poke ldap for nom, prenom and courriel properties.
            var ldapUser = this.ldapSearcher.SearchForUser(SearchBy.SamAccountName, identity);

            // Create a base profile entity.
            var profilEntity = new Profil
            {
                ProfilAvance = new ProfilAvance
                {
                    Courriel = ldapUser.Email
                },
                CodeUniversel = identity,
                Nom = ldapUser.LastName,
                Prenom = ldapUser.FirstName,
                DateCreation = DateTime.Now,
                Public = true,
                Actif = true,
            };

            // Add the base profil.
            this.profilRepository.Add(profilEntity);
            return profilEntity.Id;
        }
    }
}