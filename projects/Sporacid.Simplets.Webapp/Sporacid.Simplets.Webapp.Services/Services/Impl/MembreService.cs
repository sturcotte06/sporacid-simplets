namespace Sporacid.Simplets.Webapp.Services.Services.Impl
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using AutoMapper;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.LinqToSql;
    using Sporacid.Simplets.Webapp.Services.Models.Dto;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix("api/v1/membre")]
    public class MembreService : BaseService, IMembreService
    {
        private readonly IRepository<Int32, Club> clubRepository;
        private readonly IRepository<Int32, Membre> membreRepository;
        private readonly IRepository<Int32, MembreClub> membresClubRepository;

        public MembreService(IRepository<Int32, Membre> membreRepository, IRepository<Int32, Club> clubRepository, IRepository<Int32, MembreClub> membresClubRepository)
        {
            this.membreRepository = membreRepository;
            this.clubRepository = clubRepository;
            this.membresClubRepository = membresClubRepository;
        }

        /// <summary>
        /// Subscribes a member entity to a club entity.
        /// </summary>
        /// <param name="clubId">The id of the club entity.</param>
        /// <param name="membreId">The id of the member entity.</param>
        [HttpPost]
        [Route("{membreId:int}/subscribe-to/{clubId:int}")]
        public void SubscribeToClub(int clubId, int membreId)
        {
            var membreEntity = this.membreRepository.Get(membreId);
            var clubEntity = this.clubRepository.Get(clubId);

            membreEntity.MembreClubs.Add(new MembreClub
            {
                Membre = membreEntity,
                Club = clubEntity,
                DateDebut = DateTime.UtcNow
            });

            this.membreRepository.Update(membreEntity);
        }

        /// <summary>
        /// Unsubscribes a member entity from a club entity.
        /// </summary>
        /// <param name="clubId">The id of the club entity.</param>
        /// <param name="membreId">The id of the member entity.</param>
        [HttpDelete]
        [Route("{membreId:int}/unsubscribe-from/{clubId:int}")]
        public void UnsubscribeFromClub(int clubId, int membreId)
        {
            var membreEntity = this.membreRepository.Get(membreId);
            var membreClubEntity = membreEntity.MembreClubs
                .FirstOrDefault(mc => mc.ClubId == clubId && mc.MembreId == membreId);
            this.membresClubRepository.Delete(membreClubEntity);
        }

        /// <summary>
        /// Adds a member entity into the system.
        /// </summary>
        /// <param name="membre">The member entity.</param>
        [HttpPost]
        [Route("")]
        public Int32 Add(MembreDto membre)
        {
            var membreEntity = Mapper.Map<MembreDto, Membre>(membre);
            this.membreRepository.Add(membreEntity);

            return membreEntity.Id;
        }

        /// <summary>
        /// Updates a member entity from the system.
        /// </summary>
        /// <param name="membre">The member entity.</param>
        [HttpPut]
        [Route("")]
        public void Update(MembreDto membre)
        {
            var membreEntity = this.membreRepository.Get(membre.Id);
            membreEntity = Mapper.Map(membre, membreEntity);
            this.membreRepository.Update(membreEntity);
        }

        /// <summary>
        /// Deletes a member entity from the system.
        /// </summary>
        /// <param name="membreId">The id of the member entity.</param>
        [HttpDelete]
        [Route("{membreId:int}")]
        public void Delete(int membreId)
        {
            this.membreRepository.Delete(membreId);
        }

        /// <summary>
        /// Gets a member entity from the system.
        /// </summary>
        /// <param name="membreId">The id of the member entity.</param>
        /// <returns>The member entity.</returns>
        [HttpGet]
        [Route("{membreId:int}")]
        public MembreDto Get(int membreId)
        {
            var membreEntity = this.membreRepository.Get(membreId);
            return Mapper.Map<Membre, MembreDto>(membreEntity);
        }
    }
}