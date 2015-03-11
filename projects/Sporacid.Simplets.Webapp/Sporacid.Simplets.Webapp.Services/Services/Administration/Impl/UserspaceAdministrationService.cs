namespace Sporacid.Simplets.Webapp.Services.Services.Administration.Impl
{
    using System;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security.Authorization;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Core.Security.Ldap;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Resources.Exceptions;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/administration-profil")]
    public class UserspaceAdministrationService : BaseSecureService, IUserspaceAdministrationService
    {
        private readonly ILdapSearcher ldapSearcher;
        private readonly IRepository<Int32, Profil> profilRepository;

        public UserspaceAdministrationService(ILdapSearcher ldapSearcher, IRepository<Int32, Profil> profilRepository)
        {
            this.profilRepository = profilRepository;
            this.ldapSearcher = ldapSearcher;
        }

        /// <summary>
        /// Creates the base profil for agiven universal code.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <returns>The id of the newly created profil entity.</returns>
        public Int32 CreateBaseProfil(String codeUniversel)
        {
            // Cannot add the profil twice.
            if (this.profilRepository.Has(profil => profil.CodeUniversel == codeUniversel))
            {
                throw new NotAuthorizedException(String.Format(ExceptionStrings.Services_Security_ProfilDuplicate, codeUniversel));
            }

            // Poke ldap for nom, prenom and courriel properties.
            var ldapUser = this.ldapSearcher.SearchForUser(SearchBy.SamAccountName, codeUniversel);

            // Create a base profile entity.
            var profilEntity = new Profil
            {
                ProfilAvance = new ProfilAvance
                {
                    Courriel = ldapUser.Email
                },
                CodeUniversel = codeUniversel,
                Nom = ldapUser.LastName,
                Prenom = ldapUser.FirstName,
                Public = true,
                Actif = true,
            };

            // Add the base profil.
            this.profilRepository.Add(profilEntity);
            return profilEntity.Id;
        }
    }
}