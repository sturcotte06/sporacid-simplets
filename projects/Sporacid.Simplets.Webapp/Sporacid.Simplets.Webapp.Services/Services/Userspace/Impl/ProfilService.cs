namespace Sporacid.Simplets.Webapp.Services.Services.Userspace.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Data.Linq.SqlClient;
    using System.Linq;
    using System.Web.Http;
    using AutoMapper;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Userspace;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/{codeUniversel}/profil")]
    public class ProfilService : BaseService, IProfilService
    {
        private readonly IRepository<Int32, Club> clubRepository;
        private readonly IRepository<Int32, Profil> profilRepository;

        public ProfilService(IRepository<Int32, Profil> profilRepository, IRepository<Int32, Club> clubRepository)
        {
            this.profilRepository = profilRepository;
            this.clubRepository = clubRepository;
        }

        /// <summary>
        /// Gets the profil object from the system.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <returns>The profil.</returns>
        [HttpGet]
        [Route("")]
        public ProfilDto GetProfil(String codeUniversel)
        {
            var profilEntity = this.profilRepository.GetUnique(m => SqlMethods.Like(codeUniversel, m.CodeUniversel));
            return Mapper.Map<Profil, ProfilDto>(profilEntity);
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

        /// <summary>
        /// Gets all club entities from the system.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <returns>All club entities subscribed to.</returns>
        [HttpGet]
        [Route("clubs")]
        public IEnumerable<WithId<Int32, ClubDto>> GetClubsSubscribedTo(String codeUniversel)
        {
            var clubEntities = this.clubRepository.GetAll(
                c => c.Membres.Any(m => SqlMethods.Like(m.CodeUniversel, codeUniversel)));
            var a = clubEntities.Select(c => new WithId<Int32, ClubDto>(c.Id, Mapper.Map<Club, ClubDto>(c)));

            return clubEntities.Select(c => new WithId<Int32, ClubDto>(c.Id, Mapper.Map<Club, ClubDto>(c)));
        }
    }
}