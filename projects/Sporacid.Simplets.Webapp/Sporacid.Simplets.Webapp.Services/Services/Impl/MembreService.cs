namespace Sporacid.Simplets.Webapp.Services.Services.Impl
{
    using System.Collections.Generic;
    using System.Web.Http;
    using AutoMapper;
    using Sporacid.Simplets.Webapp.Core.Aspects.Logging;
    using Sporacid.Simplets.Webapp.Services.LinqToSql;
    using Sporacid.Simplets.Webapp.Services.Repositories;
    using Sporacid.Simplets.Webapp.Services.Repositories.Dto;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Trace(LoggingLevel.Information)]
    [RoutePrefix("api/v1/membre")]
    public class MembreService : BaseService, IMembreService
    {
        private readonly IMembreRepository membreRepository;
        
        public MembreService(IMembreRepository membreRepository)
        {
            this.membreRepository = membreRepository;
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
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Adds a member entity into the system.
        /// </summary>
        /// <param name="membre">The member entity.</param>
        [HttpPost]
        [Route("")]
        public void Add(MembreDto membre)
        {
            this.membreRepository.Add(membre);
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
            var membre = this.membreRepository.Get(membreId);
            return Mapper.Map<Membre, MembreDto>(membre);
        }

        /// <summary>
        /// Get all member entities subscribed to the club entity.
        /// </summary>
        /// <param name="clubId">The id of the club entity.</param>
        /// <returns>All subscribed member entities.</returns>
        [HttpGet]
        [Route("all-from/{clubId:int}")]
        public IEnumerable<Membre> GetByClub(int clubId)
        {
            throw new System.NotImplementedException();
        }
    }
}