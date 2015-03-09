namespace Sporacid.Simplets.Webapp.Services.Services.Userspace.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Userspace;
    using WebApi.OutputCache.V2;

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
        [HttpGet, Route("")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.VeryLong)]
        public ProfilDto Get(String codeUniversel)
        {
            return this.profilRepository
                .GetUnique(profil => codeUniversel == profil.CodeUniversel)
                .MapTo<Profil, ProfilDto>();
        }

        /// <summary>
        /// Updates the profil object in the system.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="profil">The profil.</param>
        [HttpPut, Route("")]
        [InvalidateCacheOutput("Get")]
        public void Update(String codeUniversel, ProfilDto profil)
        {
            var profilEntity = this.profilRepository
                .GetUnique(profil2 => codeUniversel == profil2.CodeUniversel)
                .MapFrom(profil);
            this.profilRepository.Update(profilEntity);
        }

        /// <summary>
        /// Gets all club entities from the system.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <returns>All club entities subscribed to.</returns>
        [HttpGet, Route("clubs")]
        public IEnumerable<WithId<Int32, ClubDto>> GetClubsSubscribedTo(String codeUniversel)
        {
            return this.clubRepository
                .GetAll(club => club.Membres.Any(membre => membre.CodeUniversel == codeUniversel))
                .MapAllWithIds<Club, ClubDto>();
        }
    }
}