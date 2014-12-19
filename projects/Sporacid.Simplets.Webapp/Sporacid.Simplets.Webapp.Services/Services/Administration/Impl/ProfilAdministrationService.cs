namespace Sporacid.Simplets.Webapp.Services.Services.Administration.Impl
{
    using System;
    using System.Data.Linq.SqlClient;
    using System.Web.Http;
    using AutoMapper;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Userspace;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/administration-profil")]
    public class ProfilAdministrationService : BaseService, IProfilAdministrationService
    {
        private readonly IPrincipalAdministrationService principalService;
        private readonly IRepository<Int32, Profil> profilRepository;

        public ProfilAdministrationService(IPrincipalAdministrationService principalService, IRepository<Int32, Profil> profilRepository)
        {
            this.profilRepository = profilRepository;
            this.principalService = principalService;
        }

        /// <summary>
        /// Adds a profil entity into the system.
        /// This should never be called and only exists for test purposes.
        /// </summary>
        /// <param name="profil">The profil entity.</param>
        /// <returns>The id of the newly created profil entity.</returns>
        [HttpPost]
        [Route("")]
        public int AddProfil(ProfilDto profil)
        {
            var profilEntity = Mapper.Map<ProfilDto, Profil>(profil);
            this.profilRepository.Add(profilEntity);

            // Create the principal for this membre.
            this.principalService.CreatePrincipal(profilEntity.CodeUniversel);
            return profilEntity.Id;
        }

        /// <summary>
        /// Updates a profil entity from the system.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="profil">The profil entity.</param>
        [HttpPut]
        [Route("{codeUniversel}")]
        public void UpdateProfil(String codeUniversel, ProfilDto profil)
        {
            var profilEntity = this.profilRepository.GetUnique(p => SqlMethods.Like(codeUniversel, p.CodeUniversel));
            profilEntity = Mapper.Map(profil, profilEntity);
            this.profilRepository.Update(profilEntity);
        }

        /// <summary>
        /// Gets a member entity from the system.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the user.</param>
        /// <returns>The member entity.</returns>
        [HttpGet]
        [Route("{codeUniversel}")]
        public ProfilDto GeProfil(String codeUniversel)
        {
            var profilEntity = this.profilRepository.GetUnique(p => SqlMethods.Like(codeUniversel, p.CodeUniversel));
            return Mapper.Map<Profil, ProfilDto>(profilEntity);
        }
    }
}