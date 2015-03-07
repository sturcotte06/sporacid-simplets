namespace Sporacid.Simplets.Webapp.Services.Services.Clubs.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Data.Linq.SqlClient;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/{clubName:alpha}/commandite/{commanditeId:int}/suivie")]
    public class SuivieService : BaseService, ISuivieService
    {
        private readonly IRepository<Int32, Club> clubRepository;
        private readonly IRepository<Int32, Commandite> commanditeRepository;
        private readonly IRepository<Int32, Suivie> suivieRepository;

        public SuivieService(IRepository<Int32, Club> clubRepository, IRepository<Int32, Commandite> commanditeRepository,
            IRepository<Int32, Suivie> suivieRepository)
        {
            this.clubRepository = clubRepository;
            this.commanditeRepository = commanditeRepository;
            this.suivieRepository = suivieRepository;
        }

        /// <summary>
        /// Gets all suivie from a commandite in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The suivie entities.</returns>
        [HttpGet, Route("")]
        public IEnumerable<WithId<Int32, SuivieDto>> GetAll(String clubName, Int32 commanditeId, [FromUri] UInt32? skip, [FromUri] UInt32? take)
        {
            return this.suivieRepository
                .GetAll(suivie => SqlMethods.Like(clubName, suivie.Commandite.Club.Nom))
                .OptionalSkipTake(skip, take)
                .MapAllWithIds<Suivie, SuivieDto>();
        }

        /// <summary>
        /// Gets a suivie from a commandite in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        /// <param name="suivieId">The suivie id.</param>
        /// <returns>The suivie entity.</returns>
        [HttpGet, Route("{suivieId:int}")]
        public SuivieDto Get(String clubName, Int32 commanditeId, Int32 suivieId)
        {
            return this.suivieRepository
                .GetUnique(suivie => SqlMethods.Like(clubName, suivie.Commandite.Club.Nom) && suivie.CommanditeId == commanditeId && suivie.Id == suivieId)
                .MapTo<Suivie, SuivieDto>();
        }

        /// <summary>
        /// Creates a suivie for commandite in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        /// <param name="suivie">The suivie.</param>
        /// <returns>The created suivie id.</returns>
        [HttpPost, Route("")]
        public Int32 Create(String clubName, Int32 commanditeId, SuivieDto suivie)
        {
            var commanditeEntity = this.commanditeRepository
                .GetUnique(club => SqlMethods.Like(clubName, club.Club.Nom) && club.Id == commanditeId);

            var suivieEntity = suivie.MapTo<SuivieDto, Suivie>();
            commanditeEntity.Suivies.Add(suivieEntity);

            this.commanditeRepository.Update(commanditeEntity);
            return suivieEntity.Id;
        }

        /// <summary>
        /// Updates a suivie for a commandite in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        /// <param name="suivieId">The suivie id.</param>
        /// <param name="suivie">The suivie.</param>
        [HttpPut, Route("{suivieId:int}")]
        public void Update(String clubName, Int32 commanditeId, Int32 suivieId, SuivieDto suivie)
        {
            var suivieEntity = this.suivieRepository
                .GetUnique(suivie2 => SqlMethods.Like(clubName, suivie2.Commandite.Club.Nom) && suivie2.CommanditeId == commanditeId && suivie2.Id == suivieId)
                .MapFrom(suivie);
            this.suivieRepository.Update(suivieEntity);
        }

        /// <summary>
        /// Deletes a suivie from a commandite in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        /// <param name="suivieId">The suivie id.</param>
        [HttpDelete, Route("{suivieId:int}")]
        public void Delete(String clubName, Int32 commanditeId, Int32 suivieId)
        {
            // Somewhat trash call to make sure the suivie is in this context. 
            var suivieEntity = this.suivieRepository
                .GetUnique(suivie => SqlMethods.Like(clubName, suivie.Commandite.Club.Nom) && suivie.CommanditeId == commanditeId && suivie.Id == suivieId);
            this.suivieRepository.Delete(suivieEntity);
        }
    }
}