namespace Sporacid.Simplets.Webapp.Services.Services.Clubs.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Data.Linq.SqlClient;
    using System.Linq;
    using System.Web.Http;
    using AutoMapper;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/{clubName:alpha}/commandite")]
    public class CommanditeService : BaseService, ICommanditeService
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
        /// <returns>The commandite.</returns>
        [HttpGet]
        [Route("")]
        public IEnumerable<WithId<Int32, CommanditeDto>> GetAll(String clubName)
        {
            var commanditeEntities = this.commanditeRepository.GetAll(
                c => SqlMethods.Like(clubName, c.Club.Nom));
            return commanditeEntities.Select(commandite =>
                new WithId<Int32, CommanditeDto>(commandite.Id, Mapper.Map<Commandite, CommanditeDto>(commandite)));
        }

        /// <summary>
        /// Get a commandite from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        /// <returns>The commandite.</returns>
        [HttpGet]
        [Route("{commanditeId:int}")]
        public CommanditeDto Get(String clubName, Int32 commanditeId)
        {
            var commanditeEntity = this.commanditeRepository.GetUnique(
                c => SqlMethods.Like(c.Club.Nom, clubName) && c.Id == commanditeId);
            return Mapper.Map<Commandite, CommanditeDto>(commanditeEntity);
        }

        /// <summary>
        /// Creates a commandite in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commandite">The commandite.</param>
        /// <returns>The created commandite id.</returns>
        [HttpPost]
        [Route("")]
        public Int32 Create(String clubName, CommanditeDto commandite)
        {
            var clubEntity = this.clubRepository.GetUnique(c => SqlMethods.Like(clubName, c.Nom));
            var commanditeEntity = Mapper.Map<CommanditeDto, Commandite>(commandite);

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
        [HttpPut]
        [Route("{commanditeId:int}")]
        public void Update(String clubName, Int32 commanditeId, CommanditeDto commandite)
        {
            var commanditeEntity = this.commanditeRepository.GetUnique(
                c => SqlMethods.Like(c.Club.Nom, clubName) && c.Id == commanditeId);
            Mapper.Map(commandite, commanditeEntity);
            this.commanditeRepository.Update(commanditeEntity);
        }

        /// <summary>
        /// Deletes a commandite from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        [HttpDelete]
        [Route("{commanditeId:int}")]
        public void Delete(String clubName, Int32 commanditeId)
        {
            // Somewhat trash call to make sure the commandite is in this context. 
            var commanditeEntity = this.commanditeRepository.GetUnique(c =>
                SqlMethods.Like(clubName, c.Club.Nom) && c.Id == commanditeId);
            this.commanditeRepository.Delete(commanditeEntity);
        }
    }
}