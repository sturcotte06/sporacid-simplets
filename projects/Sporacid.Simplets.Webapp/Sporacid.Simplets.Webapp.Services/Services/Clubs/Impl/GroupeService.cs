namespace Sporacid.Simplets.Webapp.Services.Services.Clubs.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;
    using Sporacid.Simplets.Webapp.Services.Database.Repositories;
    using WebApi.OutputCache.V2;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/{clubName}/groupe")]
    public class GroupeController : BaseSecureService, IGroupeService
    {
        private readonly IEntityRepository<Int32, Club> clubRepository;
        private readonly IEntityRepository<GroupeMembreId, GroupeMembre> groupeMembreRepository;
        private readonly IEntityRepository<Int32, Groupe> groupeRepository;

        public GroupeController(IEntityRepository<Int32, Groupe> groupeRepository, IEntityRepository<Int32, Club> clubRepository,
                                IEntityRepository<GroupeMembreId, GroupeMembre> groupeMembreRepository)
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
        public IEnumerable<WithId<Int32, GroupeDto>> GetAll(String clubName, [FromUri] UInt32? skip = null, [FromUri] UInt32? take = null)
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
        public GroupeDto Get(String clubName, Int32 groupeId)
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
        public Int32 Create(String clubName, GroupeDto groupe)
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
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetAll"), InvalidateCacheOutput("GetAllInGroupe", typeof (MembreController))]
        public void AddAllMembreToGroupe(String clubName, Int32 groupeId, IEnumerable<Int32> membreIds)
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
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetAll"), InvalidateCacheOutput("GetAllInGroupe", typeof (MembreController))]
        public void DeleteAllMembreToGroupe(String clubName, Int32 groupeId, IEnumerable<Int32> membreIds)
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
        public void Update(String clubName, Int32 groupeId, GroupeDto groupe)
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
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetAll"), InvalidateCacheOutput("GetAllInGroupe", typeof (MembreController))]
        public void Delete(String clubName, Int32 groupeId)
        {
            // Somewhat trash call to make sure the groupe is in this context. 
            var groupeEntity = this.groupeRepository
                .GetUnique(groupe => clubName == groupe.Club.Nom && groupe.Id == groupeId);
            this.groupeRepository.Delete(groupeEntity);
        }
    }
}