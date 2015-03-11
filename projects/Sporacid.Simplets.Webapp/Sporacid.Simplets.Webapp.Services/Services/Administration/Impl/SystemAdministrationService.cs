﻿namespace Sporacid.Simplets.Webapp.Services.Services.Administration.Impl
{
    using System;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security.Authorization;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;
    using Sporacid.Simplets.Webapp.Services.Resources.Exceptions;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/administration")]
    public class SystemAdministrationService : BaseSecureService, ISystemAdministrationService
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
        [HttpPost, Route("club")]
        public Int32 CreateClub(ClubDto club)
        {
            // Cannot add the same club twice.
            if (this.clubRepository.Has(club2 => club2.Nom == club.Nom))
            {
                throw new NotAuthorizedException(String.Format(ExceptionStrings.Services_Security_ClubDuplicate, club.Nom));
            }

            // Add the new club entity.
            var clubEntity = club.MapTo<ClubDto, Club>();
            this.clubRepository.Add(clubEntity);

            // Add a new security context for the club.
            this.contextService.CreateContext(clubEntity.Nom);
            return clubEntity.Id;
        }
    }
}