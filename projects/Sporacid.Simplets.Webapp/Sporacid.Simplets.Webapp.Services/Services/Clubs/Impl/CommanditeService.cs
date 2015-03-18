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
    [RoutePrefix(BasePath + "/{clubName:alpha}/commanditaire/{commanditaireId:int}/commandite")]
    public class CommanditeService : BaseSecureService, ICommanditeService
    {
        private readonly IRepository<Int32, Commanditaire> commanditaireRepository;
        private readonly IRepository<Int32, Commandite> commanditeRepository;

        public CommanditeService(IRepository<Int32, Commanditaire> commanditaireRepository, IRepository<Int32, Commandite> commanditeRepository)
        {
            this.commanditaireRepository = commanditaireRepository;
            this.commanditeRepository = commanditeRepository;
        }

        /// <summary>
        /// Get all commandite from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditaireId">The commanditaire id.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The commandite.</returns>
        [HttpGet, Route("")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Medium)]
        public IEnumerable<WithId<Int32, CommanditeDto>> GetAll(String clubName, Int32 commanditaireId, [FromUri] UInt32? skip = null, [FromUri] UInt32? take = null)
        {
            return this.commanditeRepository
                .GetAll(commandite => clubName == commandite.Commanditaire.Club.Nom)
                .OrderBy(commandite => commandite.Commanditaire.Nom)
                .OptionalSkipTake(skip, take)
                .MapAllWithIds<Commandite, CommanditeDto>();
        }

        /// <summary>
        /// Get a commandite from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditaireId">The commanditaire id.</param>
        /// <param name="commanditeId">The commandite id.</param>
        /// <returns>The commandite.</returns>
        [HttpGet, Route("{commanditeId:int}")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Medium)]
        public CommanditeDto Get(String clubName, Int32 commanditaireId, Int32 commanditeId)
        {
            return this.commanditeRepository
                .GetUnique(commandite => commandite.Commanditaire.Club.Nom == clubName && commandite.Commanditaire.Id == commanditaireId && commandite.Id == commanditeId)
                .MapTo<Commandite, CommanditeDto>();
        }

        /// <summary>
        /// Creates a commandite in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditaireId">The commanditaire id.</param>
        /// <param name="commandite">The commandite.</param>
        /// <returns>The created commandite id.</returns>
        [HttpPost, Route("")]
        [InvalidateCacheOutput("GetAll")]
        public Int32 Create(String clubName, Int32 commanditaireId, CommanditeDto commandite)
        {
            // Somewhat trash call to make sure the commandite is in this context. 
            this.commanditaireRepository
                .GetUnique(commanditaire => commanditaire.Id == commanditaireId && clubName == commanditaire.Club.Nom);

            // Make sure the commandite is created in this context.
            var commanditeEntity = commandite.MapTo<CommanditeDto, Commandite>();
            commanditeEntity.CommanditaireId = commanditaireId;

            this.commanditeRepository.Add(commanditeEntity);
            return commanditeEntity.Id;
        }

        /// <summary>
        /// Udates a commandite in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditaireId">The commanditaire id.</param>
        /// <param name="commanditeId">The commandite id.</param>
        /// <param name="commandite">The commandite.</param>
        [HttpPut, Route("{commanditeId:int}")]
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetAll")]
        public void Update(String clubName, Int32 commanditaireId, Int32 commanditeId, CommanditeDto commandite)
        {
            var commanditeEntity = this.commanditeRepository
                .GetUnique(commandite2 => commandite2.Commanditaire.Club.Nom == clubName && commandite2.Commanditaire.Id == commanditaireId && commandite2.Id == commanditeId)
                .MapFrom(commandite);
            this.commanditeRepository.Update(commanditeEntity);
        }

        /// <summary>
        /// Deletes a commandite from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditaireId">The commanditaire id.</param>
        /// <param name="commanditeId">The commandite id.</param>
        [HttpDelete, Route("{commanditeId:int}")]
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetAll")]
        public void Delete(String clubName, Int32 commanditaireId, Int32 commanditeId)
        {
            // Somewhat trash call to make sure the commandite is in this context. 
            var commanditeEntity = this.commanditeRepository
                .GetUnique(commandite => commandite.Commanditaire.Club.Nom == clubName && commandite.Commanditaire.Id == commanditaireId && commandite.Id == commanditeId);
            this.commanditeRepository.Delete(commanditeEntity);
        }
    }
}