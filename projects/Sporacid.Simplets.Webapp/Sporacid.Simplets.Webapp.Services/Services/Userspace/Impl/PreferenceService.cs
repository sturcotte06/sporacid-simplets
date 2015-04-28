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
    [RoutePrefix(BasePath + "/{codeUniversel}/preference")]
    public class PreferenceController : BaseSecureService, IPreferenceService
    {
        private readonly IEntityRepository<Int32, Profil> profilRepository;
        private readonly IEntityRepository<Int32, Preference> preferenceRepository;

        public PreferenceController(
            IEntityRepository<Int32, Profil> profilRepository,
            IEntityRepository<Int32, Preference> preferenceRepository)
        {
            this.profilRepository = profilRepository;
            this.preferenceRepository = preferenceRepository;
        }

        /// <summary>
        /// Gets all preference entities from the user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The preference entities.</returns>
        [HttpGet, Route("")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Medium)]
        public IEnumerable<WithId<Int32, PreferenceDto>> GetAll(String codeUniversel, [FromUri] UInt32? skip = null, [FromUri] UInt32? take = null)
        {
            return this.preferenceRepository
                .GetAll(preference => preference.Profil.CodeUniversel == codeUniversel)
                .OptionalSkipTake(skip, take)
                .MapAllWithIds<Preference, PreferenceDto>();
        }

        /// <summary>
        /// Gets the preference entity from  the user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="preferenceId">The preferences id.</param>
        /// <returns>The preference entity.</returns>
        [HttpGet, Route("{preferenceId:int}")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Medium)]
        public PreferenceDto Get(String codeUniversel, Int32 preferenceId)
        {
            return this.preferenceRepository
                .GetUnique(preference => preference.Profil.CodeUniversel == codeUniversel && preference.Id == preferenceId)
                .MapTo<Preference, PreferenceDto>();
        }

        /// <summary>
        /// Creates a preference entity in a user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="preference">The preference.</param>
        /// <returns>The created preference entity id.</returns>
        [HttpPost, Route("")]
        [InvalidateCacheOutput("GetAll")]
        public Int32 Create(String codeUniversel, PreferenceDto preference)
        {
            var profilEntity = this.profilRepository.GetUnique(profil => profil.CodeUniversel == codeUniversel);
            var preferenceEntity = preference.MapTo<PreferenceDto, Preference>();

            // Make sure the preference is created in this user context.
            preferenceEntity.ProfilId = profilEntity.Id;

            this.preferenceRepository.Add(preferenceEntity);
            return preferenceEntity.Id;
        }

        /// <summary>
        /// Updates the preference entity in a user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="preferenceId">The preference id.</param>
        /// <param name="preference">The preference.</param>
        [HttpPut, Route("{preferenceId:int}")]
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetAll")]
        public void Update(String codeUniversel, Int32 preferenceId, PreferenceDto preference)
        {
            var preferenceEntity = this.preferenceRepository
                .GetUnique(preference2 => preference2.Profil.CodeUniversel == codeUniversel && preference2.Id == preferenceId)
                .MapFrom(preference);
            this.preferenceRepository.Update(preferenceEntity);
        }

        /// <summary>
        /// Deletes a preference entity from a user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="preferenceId">The preference id.</param>
        [HttpDelete, Route("{preferenceId:int}")]
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetAll")]
        public void Delete(String codeUniversel, Int32 preferenceId)
        {
            // Somewhat trash call to make sure the preference is in this context. 
            var preferenceEntity = this.preferenceRepository
                .GetUnique(preference => preference.Profil.CodeUniversel == codeUniversel && preference.Id == preferenceId);
            this.preferenceRepository.Delete(preferenceEntity);
        }
    }
}