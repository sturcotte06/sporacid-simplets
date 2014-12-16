namespace Sporacid.Simplets.Webapp.Services.Services.Impl
{
    using System;
    using System.Web.Http;
    using AutoMapper;
    using Sporacid.Simplets.Webapp.Core.Exceptions;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Core.Security.Database;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix("api/v1/membre")]
    public class MembreService : BaseService, IMembreService
    {
        private readonly IRepository<Int32, Membre> membreRepository;
        private readonly IRepository<Int32, Principal> principalRepository;

        public MembreService(IRepository<Int32, Membre> membreRepository, IRepository<Int32, Principal> principalRepository)
        {
            this.membreRepository = membreRepository;
            this.principalRepository = principalRepository;
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

            // Add the new principal for this membre.
            var principalEntity = new Principal {Identity = membreEntity.CodeUniversel};
            this.principalRepository.Add(principalEntity);

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
            if (membreEntity.CodeUniversel != membre.CodeUniversel)
            {
                throw new SecurityException("Cannot update the field 'CodeUniversel'.");
            }

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
            var membreEntity = this.membreRepository.Get(membreId);
            this.membreRepository.Delete(membreEntity);

            // Remove the principal.
            var principalEntity = this.principalRepository.GetUnique(p =>
                String.Equals(p.Identity, membreEntity.CodeUniversel, StringComparison.CurrentCultureIgnoreCase));
            this.principalRepository.Delete(principalEntity);
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