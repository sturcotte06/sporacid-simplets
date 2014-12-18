namespace Sporacid.Simplets.Webapp.Services.Services.Impl
{
    using System;
    using System.Web.Http;
    using AutoMapper;
    using Sporacid.Simplets.Webapp.Core.Exceptions;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix("api/v1/membre")]
    public class MembreService : BaseService, IMembreService
    {
        private readonly IRepository<Int32, Membre> membreRepository;
        private readonly IPrincipalService principalService;

        public MembreService(IPrincipalService principalService, IRepository<Int32, Membre> membreRepository)
        {
            this.membreRepository = membreRepository;
            this.principalService = principalService;
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

            this.principalService.Create(membreEntity.CodeUniversel);
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