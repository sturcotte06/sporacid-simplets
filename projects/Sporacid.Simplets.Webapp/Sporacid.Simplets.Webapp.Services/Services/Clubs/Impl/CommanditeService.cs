namespace Sporacid.Simplets.Webapp.Services.Services.Clubs.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;
    using WebApi.OutputCache.V2;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/{clubName:alpha}/commandite")]
    public class CommanditeService : BaseSecureService, ICommanditeService
    {
        private readonly IRepository<Int32, Club> clubRepository;
        private readonly IRepository<Int32, Commandite> commanditeRepository;

        public CommanditeService(IRepository<Int32, Commandite> commanditeRepository, IRepository<Int32, Club> clubRepository)
        {
            this.commanditeRepository = commanditeRepository;
            this.clubRepository = clubRepository;
        }

        /// <summary>
        /// Get all commandite from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The commandite.</returns>
        [HttpGet, Route("")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Medium)]
        public IEnumerable<WithId<Int32, CommanditeDto>> GetAll(String clubName, [FromUri] UInt32? skip = null, [FromUri] UInt32? take = null)
        {
            return this.commanditeRepository
                .GetAll(commandite => clubName == commandite.Club.Nom)
                .OptionalSkipTake(skip, take)
                .MapAllWithIds<Commandite, CommanditeDto>();
        }

        /// <summary>
        /// Get a commandite from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        /// <returns>The commandite.</returns>
        [HttpGet, Route("{commanditeId:int}")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Medium)]
        public CommanditeDto Get(String clubName, Int32 commanditeId)
        {
            return this.commanditeRepository
                .GetUnique(commandite => commandite.Club.Nom == clubName && commandite.Id == commanditeId)
                .MapTo<Commandite, CommanditeDto>();
        }

        /// <summary>
        /// Creates a commandite in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commandite">The commandite.</param>
        /// <returns>The created commandite id.</returns>
        [HttpPost, Route("")]
        [InvalidateCacheOutput("GetAll")]
        public Int32 Create(String clubName, CommanditeDto commandite)
        {
            var clubEntity = this.clubRepository.GetUnique(club => clubName == club.Nom);
            var commanditeEntity = commandite.MapTo<CommanditeDto, Commandite>();

            // Make sure the commandite is created in this context.
            commanditeEntity.ClubId = clubEntity.Id;

            this.commanditeRepository.Add(commanditeEntity);
            return commanditeEntity.Id;
        }

        /// <summary>
        /// Udates a commandite in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        /// <param name="commandite">The commandite.</param>
        [HttpPut, Route("{commanditeId:int}")]
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetAll")]
        public void Update(String clubName, Int32 commanditeId, CommanditeDto commandite)
        {
            var commanditeEntity = this.commanditeRepository
                .GetUnique(commandite2 => commandite2.Club.Nom == clubName && commandite2.Id == commanditeId)
                .MapFrom(commandite);
            this.commanditeRepository.Update(commanditeEntity);
        }

        /// <summary>
        /// Deletes a commandite from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        [HttpDelete, Route("{commanditeId:int}")]
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetAll")]
        public void Delete(String clubName, Int32 commanditeId)
        {
            // Somewhat trash call to make sure the commandite is in this context. 
            var commanditeEntity = this.commanditeRepository
                .GetUnique(commandite => commandite.Club.Nom == clubName && commandite.Id == commanditeId);
            this.commanditeRepository.Delete(commanditeEntity);
        }
    }
}