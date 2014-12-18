namespace Sporacid.Simplets.Webapp.Services.Services.Impl
{
    using System;
    using System.Data.Linq.SqlClient;
    using System.Web.Http;
    using AutoMapper;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Authorization;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix("api/v1/administration")]
    public class AdministrationService : BaseService, IAdministrationService
    {
        private readonly IRepository<Int32, Club> clubRepository;
        private readonly IContextAdministrationService contextService;

        public AdministrationService(IContextAdministrationService contextService, IRepository<Int32, Club> clubRepository)
        {
            this.contextService = contextService;
            this.clubRepository = clubRepository;
        }

        /// <summary>
        /// Adds a club entity into the system.
        /// All resources available for the club will be added to the security sub-system.
        /// </summary>
        /// <param name="club">The club entity.</param>
        /// <returns>The id of the newly created club entity.</returns>
        [HttpPost]
        [Route("club")]
        public int AddClub(ClubDto club)
        {
            // Cannot add the same club twice.
            if (this.clubRepository.GetUnique(c => SqlMethods.Like(c.Nom, club.Nom)) != null)
            {
                throw new NotAuthorizedException(String.Format("Club '{0}' already exists and duplicates are not allowed.", club.Nom));
            }

            // Add the new club entity.
            var clubEntity = Mapper.Map<ClubDto, Club>(club);
            this.clubRepository.Add(clubEntity);

            // Add a new context for the club.
            this.contextService.Create(clubEntity.Nom);
            return clubEntity.Id;
        }
    }
}