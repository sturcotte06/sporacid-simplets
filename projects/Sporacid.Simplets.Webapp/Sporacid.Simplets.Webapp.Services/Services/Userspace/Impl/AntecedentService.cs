namespace Sporacid.Simplets.Webapp.Services.Services.Userspace.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Userspace;
    using Sporacid.Simplets.Webapp.Services.Database.Repositories;
    using WebApi.OutputCache.V2;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/{codeUniversel}/antecedent")]
    public class AntecedentController : BaseSecureService, IAntecedentService
    {
        private readonly IEntityRepository<Int32, Profil> profilRepository;
        private readonly IEntityRepository<Int32, Antecedent> antecedentRepository;

        public AntecedentController(
            IEntityRepository<Int32, Profil> profilRepository,
            IEntityRepository<Int32, Antecedent> antecedentRepository)
        {
            this.profilRepository = profilRepository;
            this.antecedentRepository = antecedentRepository;
        }

        /// <summary>
        /// Gets all antecedent entities from the user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The antecedent entities.</returns>
        [HttpGet, Route("")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Medium)]
        public IEnumerable<WithId<Int32, AntecedentDto>> GetAll(String codeUniversel, [FromUri] UInt32? skip = null, [FromUri] UInt32? take = null)
        {
            return this.antecedentRepository
                .GetAll(antecedent => antecedent.Profil.CodeUniversel == codeUniversel)
                .OptionalSkipTake(skip, take)
                .MapAllWithIds<Antecedent, AntecedentDto>();
        }

        /// <summary>
        /// Gets the antecedent entity from  the user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="antecedentId">The antecedent id.</param>
        /// <returns>The antecedent entity.</returns>
        [HttpGet, Route("{antecedentId:int}")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Medium)]
        public AntecedentDto Get(String codeUniversel, Int32 antecedentId)
        {
            return this.antecedentRepository
                .GetUnique(antecedent => antecedent.Profil.CodeUniversel == codeUniversel && antecedent.Id == antecedentId)
                .MapTo<Antecedent, AntecedentDto>();
        }

        /// <summary>
        /// Creates a antecedent entity in a user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="antecedent">The antecedent.</param>
        /// <returns>The created antecedent entity id.</returns>
        [HttpPost, Route("")]
        [InvalidateCacheOutput("GetAll")]
        public Int32 Create(String codeUniversel, AntecedentDto antecedent)
        {
            var profilEntity = this.profilRepository.GetUnique(profil => profil.CodeUniversel == codeUniversel);
            var antecedentEntity = antecedent.MapTo<AntecedentDto, Antecedent>();

            // Make sure the preference is created in this user context.
            antecedentEntity.ProfilId = profilEntity.Id;

            this.antecedentRepository.Add(antecedentEntity);
            return antecedentEntity.Id;
        }

        /// <summary>
        /// Updates the antecedent entity in a user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="antecedentId">The antecedent id.</param>
        /// <param name="antecedent">The antecedent.</param>
        [HttpPut, Route("{antecedentId:int}")]
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetAll")]
        public void Update(String codeUniversel, Int32 antecedentId, AntecedentDto antecedent)
        {
            var antecedentEntity = this.antecedentRepository
                .GetUnique(antecedent2 => antecedent2.Profil.CodeUniversel == codeUniversel && antecedent2.Id == antecedentId)
                .MapFrom(antecedent);
            this.antecedentRepository.Update(antecedentEntity);
        }

        /// <summary>
        /// Deletes a antecedent entity from a user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="antecedentId">The antecedent id.</param>
        [HttpDelete, Route("{antecedentId:int}")]
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetAll")]
        public void Delete(String codeUniversel, Int32 antecedentId)
        {
            // Somewhat trash call to make sure the antecedent is in this context. 
            var antecedentEntity = this.antecedentRepository
                .GetUnique(antecedent => antecedent.Profil.CodeUniversel == codeUniversel && antecedent.Id == antecedentId);
            this.antecedentRepository.Delete(antecedentEntity);
        }
    }
}