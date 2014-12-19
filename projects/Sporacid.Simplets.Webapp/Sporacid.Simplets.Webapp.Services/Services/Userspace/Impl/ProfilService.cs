namespace Sporacid.Simplets.Webapp.Services.Services.Userspace.Impl
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
    [RoutePrefix(BasePath + "/{codeUniversel}/profil")]
    public class ProfilService : BaseService, IProfilService
    {
        private readonly IRepository<Int32, Profil> profilRepository;

        public ProfilService(IRepository<Int32, Profil> profilRepository)
        {
            this.profilRepository = profilRepository;
        }

        /// <summary>
        /// Updates the profil object in the system.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="profil">The profil.</param>
        [HttpPut]
        [Route("")]
        public void UpdateProfil(String codeUniversel, ProfilDto profil)
        {
            var profilEntity = this.profilRepository.GetUnique(m => SqlMethods.Like(codeUniversel, m.CodeUniversel));
            Mapper.Map(profil, profilEntity);
            this.profilRepository.Update(profilEntity);
        }
    }
}