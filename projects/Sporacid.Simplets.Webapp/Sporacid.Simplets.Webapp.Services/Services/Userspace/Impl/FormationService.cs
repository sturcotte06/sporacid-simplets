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
    [RoutePrefix(BasePath + "/{codeUniversel}/formation")]
    public class FormationController : BaseSecureService, IFormationService
    {
        private readonly IEntityRepository<Int32, Profil> profilRepository;
        private readonly IEntityRepository<Int32, Formation> formationRepository;

        public FormationController(
            IEntityRepository<Int32, Profil> profilRepository,
            IEntityRepository<Int32, Formation> formationRepository)
        {
            this.profilRepository = profilRepository;
            this.formationRepository = formationRepository;
        }

        /// <summary>
        /// Gets all formation entities from the user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The formation entities.</returns>
        [HttpGet, Route("")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Medium)]
        public IEnumerable<WithId<Int32, FormationDto>> GetAll(String codeUniversel, [FromUri] UInt32? skip = null, [FromUri] UInt32? take = null)
        {
            return this.formationRepository
                .GetAll(formation => formation.Profil.CodeUniversel == codeUniversel)
                .OptionalSkipTake(skip, take)
                .MapAllWithIds<Formation, FormationDto>();
        }

        /// <summary>
        /// Gets the formation entity from  the user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="formationId">The formation id.</param>
        /// <returns>The formation entity.</returns>
        [HttpGet, Route("{formationId:int}")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Medium)]
        public FormationDto Get(String codeUniversel, Int32 formationId)
        {
            return this.formationRepository
                .GetUnique(formation => formation.Profil.CodeUniversel == codeUniversel && formation.Id == formationId)
                .MapTo<Formation, FormationDto>();
        }

        /// <summary>
        /// Creates a formation entity in a user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="formation">The formation.</param>
        /// <returns>The created formation entity id.</returns>
        [HttpPost, Route("")]
        [InvalidateCacheOutput("GetAll")]
        public Int32 Create(String codeUniversel, FormationDto formation)
        {
            var profilEntity = this.profilRepository.GetUnique(profil => profil.CodeUniversel == codeUniversel);
            var formationEntity = formation.MapTo<FormationDto, Formation>();

            // Make sure the preference is created in this user context.
            formationEntity.ProfilId = profilEntity.Id;

            this.formationRepository.Add(formationEntity);
            return formationEntity.Id;
        }

        /// <summary>
        /// Updates the formation entity in a user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="formationId">The formation id.</param>
        /// <param name="formation">The formation.</param>
        [HttpPut, Route("{formationId:int}")]
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetAll")]
        public void Update(String codeUniversel, Int32 formationId, FormationDto formation)
        {
            var formationEntity = this.formationRepository
                .GetUnique(formation2 => formation2.Profil.CodeUniversel == codeUniversel && formation2.Id == formationId)
                .MapFrom(formation);
            this.formationRepository.Update(formationEntity);
        }

        /// <summary>
        /// Deletes a formation entity from a user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="formationId">The formation id.</param>
        [HttpDelete, Route("{formationId:int}")]
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetAll")]
        public void Delete(String codeUniversel, Int32 formationId)
        {
            // Somewhat trash call to make sure the antecedent is in this context. 
            var formationEntity = this.formationRepository
                .GetUnique(formation => formation.Profil.CodeUniversel == codeUniversel && formation.Id == formationId);
            this.formationRepository.Delete(formationEntity);
        }
    }
}