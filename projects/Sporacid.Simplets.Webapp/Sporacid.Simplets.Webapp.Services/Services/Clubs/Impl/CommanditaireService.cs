namespace Sporacid.Simplets.Webapp.Services.Services.Clubs.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;
    using Sporacid.Simplets.Webapp.Services.Database.Repositories;
    using WebApi.OutputCache.V2;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/{clubName:alpha}/commanditaire")]
    public class CommanditaireController : BaseSecureService, ICommanditaireService
    {
        private readonly IEntityRepository<Int32, Club> clubRepository;
        private readonly IEntityRepository<Int32, Commanditaire> commanditaireRepository;

        public CommanditaireController(IEntityRepository<Int32, Commanditaire> commanditaireRepository, IEntityRepository<Int32, Club> clubRepository)
        {
            this.commanditaireRepository = commanditaireRepository;
            this.clubRepository = clubRepository;
        }

        /// <summary>
        /// Get all commanditaires entities from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The commanditaires entities.</returns>
        [HttpGet, Route("")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Medium)]
        public IEnumerable<WithId<Int32, CommanditaireDto>> GetAll(String clubName, [FromUri] UInt32? skip = null, [FromUri] UInt32? take = null)
        {
            return this.commanditaireRepository
                .GetAll(commanditaire => commanditaire.Club.Nom == clubName)
                .OptionalSkipTake(skip, take)
                .MapAllWithIds<Commanditaire, CommanditaireDto>();
        }

        /// <summary>
        /// Get a commanditaire entity from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditaireId">The commanditaire id.</param>
        /// <returns>The meeting entity.</returns>
        [HttpGet, Route("{commanditaireId:int}")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Medium)]
        public CommanditaireDto Get(String clubName, Int32 commanditaireId)
        {
            return this.commanditaireRepository
                .GetUnique(commanditaire => commanditaire.Club.Nom == clubName && commanditaire.Id == commanditaireId)
                .MapTo<Commanditaire, CommanditaireDto>();
        }

        /// <summary>
        /// Creates a commanditaire entity in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditaire">The commanditaire entity.</param>
        /// <returns>The created commanditaire id.</returns>
        [HttpPost, Route("")]
        [InvalidateCacheOutput("GetAll")]
        public Int32 Create(String clubName, CommanditaireDto commanditaire)
        {
            var clubEntity = this.clubRepository.GetUnique(club => clubName == club.Nom);
            var commanditaireEntity = commanditaire.MapTo<CommanditaireDto, Commanditaire>();

            // Make sure the commanditaire is created in this context.
            commanditaireEntity.ClubId = clubEntity.Id;

            this.commanditaireRepository.Add(commanditaireEntity);
            return commanditaireEntity.Id;
        }

        /// <summary>
        /// Updates a commanditaire entity in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditaireId">The commanditaire id.</param>
        /// <param name="commanditaire">The commanditaire entity.</param>
        [HttpPut, Route("{commanditaireId:int}")]
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetAll")]
        public void Update(String clubName, Int32 commanditaireId, CommanditaireDto commanditaire)
        {
            var commanditaireEntity = this.commanditaireRepository
                .GetUnique(commanditaire2 => commanditaire2.Club.Nom == clubName && commanditaire2.Id == commanditaireId)
                .MapFrom(commanditaire);
            this.commanditaireRepository.Update(commanditaireEntity);
        }

        /// <summary>
        /// Deletes a commanditaire entity from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditaireId">The commanditaire id.</param>
        [HttpDelete, Route("{commanditaireId:int}")]
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetAll")]
        public void Delete(String clubName, Int32 commanditaireId)
        {
            // Somewhat trash call to make sure the commanditaire is in this context. 
            var commanditaireEntity = this.commanditaireRepository
                .GetUnique(commanditaire => clubName == commanditaire.Club.Nom && commanditaire.Id == commanditaireId);
            this.commanditaireRepository.Delete(commanditaireEntity);
        }
    }
}