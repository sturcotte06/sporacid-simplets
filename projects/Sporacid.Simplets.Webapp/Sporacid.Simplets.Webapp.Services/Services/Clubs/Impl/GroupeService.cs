namespace Sporacid.Simplets.Webapp.Services.Services.Clubs.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;
    using WebApi.OutputCache.V2;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/{clubName:alpha}/groupe")]
    public class GroupeService : BaseSecureService, IGroupeService
    {
        private readonly IRepository<Int32, Club> clubRepository;
        private readonly IRepository<GroupeMembreId, GroupeMembre> groupeMembreRepository;
        private readonly IRepository<Int32, Groupe> groupeRepository;

        public GroupeService(IRepository<Int32, Groupe> groupeRepository, IRepository<Int32, Club> clubRepository,
            IRepository<GroupeMembreId, GroupeMembre> groupeMembreRepository)
        {
            this.groupeRepository = groupeRepository;
            this.clubRepository = clubRepository;
            this.groupeMembreRepository = groupeMembreRepository;
        }

        /// <summary>
        /// Get all groupe entities from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The groupe entities.</returns>
        [HttpGet, Route("")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Medium)]
        public IEnumerable<WithId<int, GroupeDto>> GetAll(String clubName, [FromUri] uint? skip = null, [FromUri] uint? take = null)
        {
            return this.groupeRepository
                .GetAll(groupe => groupe.Club.Nom == clubName)
                .OptionalSkipTake(skip, take)
                .MapAllWithIds<Groupe, GroupeDto>();
        }

        /// <summary>
        /// Get a groupe entity from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="groupeId">The groupe id.</param>
        /// <returns>The groupe entity.</returns>
        [HttpGet, Route("{groupeId:int}")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Medium)]
        public GroupeDto Get(String clubName, int groupeId)
        {
            return this.groupeRepository
                .GetUnique(groupe => groupe.Club.Nom == clubName && groupe.Id == groupeId)
                .MapTo<Groupe, GroupeDto>();
        }

        /// <summary>
        /// Creates a groupe in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="groupe">The groupe.</param>
        /// <returns>The created groupe id.</returns>
        [HttpPost, Route("")]
        [InvalidateCacheOutput("GetAll")]
        public int Create(String clubName, GroupeDto groupe)
        {
            var clubEntity = this.clubRepository.GetUnique(club => clubName == club.Nom);
            var groupeEntity = groupe.MapTo<GroupeDto, Groupe>();

            // Make sure the groupe is created in this context.
            groupeEntity.ClubId = clubEntity.Id;

            this.groupeRepository.Add(groupeEntity);
            return groupeEntity.Id;
        }

        /// <summary>
        /// Adds all membres to a groupe from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="groupeId">The groupe id.</param>
        /// <param name="membreIds">The enumeration of group ids.</param>
        [HttpPost, Route("{groupeId:int}/membre")]
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetAll"), InvalidateCacheOutput("GetAllInGroupe", typeof (MembreService))]
        public void AddAllMembreToGroupe(String clubName, int groupeId, IEnumerable<int> membreIds)
        {
            this.groupeMembreRepository
                .DeleteAll(gp => gp.GroupeId == groupeId && membreIds.Contains(gp.MembreId));
        }

        /// <summary>
        /// Deletes all membres from a groupe from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="groupeId">The groupe id.</param>
        /// <param name="membreIds">The enumeration of group ids.</param>
        [HttpDelete, Route("{groupeId:int}/membre")]
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetAll"), InvalidateCacheOutput("GetAllInGroupe", typeof (MembreService))]
        public void DeleteAllMembreToGroupe(String clubName, int groupeId, IEnumerable<int> membreIds)
        {
            var groupeMembreEntities = membreIds.Select(membreId => new GroupeMembre {GroupeId = groupeId, MembreId = membreId});
            this.groupeMembreRepository.AddAll(groupeMembreEntities);
        }

        /// <summary>
        /// Udates a groupe in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="groupeId">The groupe id.</param>
        /// <param name="groupe">The groupe.</param>
        [HttpPut, Route("{groupeId:int}")]
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetAll")]
        public void Update(String clubName, int groupeId, GroupeDto groupe)
        {
            var groupeEntity = this.groupeRepository
                .GetUnique(groupe2 => groupe2.Club.Nom == clubName && groupe2.Id == groupeId)
                .MapFrom(groupe);
            this.groupeRepository.Update(groupeEntity);
        }

        /// <summary>
        /// Deletes a groupe from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="groupeId">The groupe id.</param>
        [HttpDelete, Route("{groupeId:int}")]
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetAll"), InvalidateCacheOutput("GetAllInGroupe", typeof (MembreService))]
        public void Delete(String clubName, int groupeId)
        {
            // Somewhat trash call to make sure the groupe is in this context. 
            var groupeEntity = this.groupeRepository
                .GetUnique(groupe => clubName == groupe.Club.Nom && groupe.Id == groupeId);
            this.groupeRepository.Delete(groupeEntity);
        }
    }
}