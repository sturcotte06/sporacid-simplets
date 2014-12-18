namespace Sporacid.Simplets.Webapp.Services.Services.Impl
{
    using System;
    using System.Data.Linq.SqlClient;
    using System.Web.Http;
    using AutoMapper;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix("api/v1/{context}/profil")]
    public class ProfilService : BaseService, IProfilService
    {
        private readonly IRepository<Int32, Membre> membreRepository;

        public ProfilService(IRepository<Int32, Membre> membreRepository)
        {
            this.membreRepository = membreRepository;
        }

        /// <summary>
        /// Updates the profil object in the system.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="profil">The profil.</param>
        [HttpPut]
        [Route("")]
        public void Update(String context, ProfilDto profil)
        {
            var membreEntity = this.membreRepository.GetUnique(m => SqlMethods.Like(context, m.CodeUniversel));
            Mapper.Map(profil, membreEntity);
            this.membreRepository.Update(membreEntity);
        }
    }
}