namespace Sporacid.Simplets.Webapp.Services.Services.Clubs.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;
    using Sporacid.Simplets.Webapp.Services.Database.Repositories;
    using WebApi.OutputCache.V2;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/club")]
    public class ClubController : BaseSecureService, IClubService
    {
        private readonly IEntityRepository<Int32, Club> clubRepository;

        public ClubController(IEntityRepository<Int32, Club> clubRepository)
        {
            this.clubRepository = clubRepository;
        }

        /// <summary>
        /// Gets all club entities to which the current user is subscribed, from the system.
        /// </summary>
        /// <returns>All club entities subscribed to.</returns>
        [HttpGet, Route("subscribed-to")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.VeryLong)]
        public IEnumerable<WithId<Int32, ClubDto>> GetClubsSubscribedTo()
        {
            var identity = HttpContext.Current.User.Identity.Name;
            return this.clubRepository
                .GetAll(club => club.Membres.Any(membre => membre.CodeUniversel == identity))
                .MapAllWithIds<Club, ClubDto>();
        }
    }
}