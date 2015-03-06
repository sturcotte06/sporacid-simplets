namespace Sporacid.Simplets.Webapp.Services.Services.Administration.Impl
{
    using System;
    using System.Data.Linq.SqlClient;
    using System.Web.Http;
    using AutoMapper;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security.Authorization;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;
    using Sporacid.Simplets.Webapp.Services.Resources.Exceptions;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/administration")]
    public class SystemAdministrationService : BaseService, ISystemAdministrationService
    {
        private readonly IRepository<Int32, Club> clubRepository;
        private readonly IContextAdministrationService contextService;

        public SystemAdministrationService(IContextAdministrationService contextService, IRepository<Int32, Club> clubRepository)
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
        public int CreateClub(ClubDto club)
        {
            // Cannot add the same club twice.
            if (this.clubRepository.Has(c => SqlMethods.Like(c.Nom, club.Nom)))
            {
                throw new NotAuthorizedException(String.Format(ExceptionStrings.Services_Security_ClubDuplicate, club.Nom));
            }

            // Add the new club entity.
            var clubEntity = Mapper.Map<ClubDto, Club>(club);
            this.clubRepository.Add(clubEntity);

            // Add a new context for the club.
            this.contextService.CreateContext(clubEntity.Nom);
            return clubEntity.Id;
        }
    }
}