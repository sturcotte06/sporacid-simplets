namespace Sporacid.Simplets.Webapp.Services.Services.Administration.Impl
{
    using System;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Core.Security.Ldap;
    using Sporacid.Simplets.Webapp.Services.Database;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/administration-profil")]
    public class UserspaceAdministrationService : BaseService, IUserspaceAdministrationService
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
        public Int32 CreateBaseProfil(string codeUniversel)
        {
            // Poke ldap for nom, prenom and courriel properties.
            var ldapUser = this.ldapSearcher.SearchForUser(SearchBy.SamAccountName, codeUniversel);

            // Create a base profile entity.
            var profilEntity = new Profil
            {
                ProfilAvance = new ProfilAvance {Courriel = ldapUser.Email},
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

        // /// <summary>
        // /// Adds a profil entity into the system.
        // /// This should never be called and only exists for test purposes.
        // /// </summary>
        // /// <param name="profil">The profil entity.</param>
        // /// <returns>The id of the newly created profil entity.</returns>
        // [HttpPost]
        // [Route("")]
        // public int AddProfil(ProfilDto profil)
        // {
        //     var profilEntity = Mapper.Map<ProfilDto, Profil>(profil);
        //     this.profilRepository.Add(profilEntity);
        // 
        //     // Create the principal for this membre.
        //     // this.principalService.CreatePrincipal(profilEntity.CodeUniversel);
        //     return profilEntity.Id;
        // }
        // 
        // /// <summary>
        // /// Updates a profil entity from the system.
        // /// </summary>
        // /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        // /// <param name="profil">The profil entity.</param>
        // [HttpPut]
        // [Route("{codeUniversel}")]
        // public void UpdateProfil(String codeUniversel, ProfilDto profil)
        // {
        //     var profilEntity = this.profilRepository.GetUnique(p => SqlMethods.Like(codeUniversel, p.CodeUniversel));
        //     profilEntity = Mapper.Map(profil, profilEntity);
        //     this.profilRepository.Update(profilEntity);
        // }
        // 
        // /// <summary>
        // /// Gets a member entity from the system.
        // /// </summary>
        // /// <param name="codeUniversel">The universal code that represents the user.</param>
        // /// <returns>The member entity.</returns>
        // [HttpGet]
        // [Route("{codeUniversel}")]
        // public ProfilDto GeProfil(String codeUniversel)
        // {
        //     var profilEntity = this.profilRepository.GetUnique(p => SqlMethods.Like(codeUniversel, p.CodeUniversel));
        //     return Mapper.Map<Profil, ProfilDto>(profilEntity);
        // }
    }
}