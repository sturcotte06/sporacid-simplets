namespace Sporacid.Simplets.Webapp.Services.Services.Public.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Dbo;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Userspace;
    using Sporacid.Simplets.Webapp.Services.Database.Repositories;
    using WebApi.OutputCache.V2;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/enumeration")]
    public class EnumerationController : BaseService, IEnumerationService
    {
        private readonly IEntityRepository<Int32, Concentration> concentrationRepository;
        private readonly IEntityRepository<Int32, StatutSuivie> statutSuivieRepository;
        private readonly IEntityRepository<Int32, TypeContact> typeContactRepository;
        private readonly IEntityRepository<Int32, Unite> uniteRepository;
        private readonly IEntityRepository<Int32, TypeCommanditaire> typeCommanditaireRepository;
        private readonly IEntityRepository<Int32, TypeAntecedent> typeAntecedentRepository;

        public EnumerationController(
            IEntityRepository<Int32, TypeContact> typeContactRepository,
            IEntityRepository<Int32, Concentration> concentrationRepository,
            IEntityRepository<Int32, StatutSuivie> statutSuivieRepository,
            IEntityRepository<Int32, Unite> uniteRepository,
            IEntityRepository<Int32, TypeCommanditaire> typeCommanditaireRepository,
            IEntityRepository<Int32, TypeAntecedent> typeAntecedentRepository)
        {
            this.typeContactRepository = typeContactRepository;
            this.concentrationRepository = concentrationRepository;
            this.statutSuivieRepository = statutSuivieRepository;
            this.uniteRepository = uniteRepository;
            this.typeCommanditaireRepository = typeCommanditaireRepository;
            this.typeAntecedentRepository = typeAntecedentRepository;
        }

        /// <summary>
        /// Returns all type contact entities from the system.
        /// </summary>
        /// <returns>Enumeration of all type contact entities.</returns>
        [HttpGet, Route("types-contacts")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Maximum, ClientTimeSpan = (Int32) CacheDuration.Maximum)]
        public IEnumerable<WithId<Int32, TypeContactDto>> GetAllTypesContacts()
        {
            return this.typeContactRepository
                .GetAll()
                .MapAllWithIds<TypeContact, TypeContactDto>();
        }

        /// <summary>
        /// Returns all statut suivie entities from the system.
        /// </summary>
        /// <returns>Enumeration of all statuts suivie entities.</returns>
        [HttpGet, Route("statuts-suivies")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Maximum, ClientTimeSpan = (Int32) CacheDuration.Maximum)]
        public IEnumerable<WithId<Int32, StatutSuivieDto>> GetAllStatutsSuivie()
        {
            return this.statutSuivieRepository
                .GetAll()
                .MapAllWithIds<StatutSuivie, StatutSuivieDto>();
        }

        /// <summary>
        /// Returns all concentration entities from the system.
        /// </summary>
        /// <returns>Enumeration of all concentration entities.</returns>
        [HttpGet, Route("concentrations")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Maximum, ClientTimeSpan = (Int32) CacheDuration.Maximum)]
        public IEnumerable<WithId<Int32, ConcentrationDto>> GetAllConcentrations()
        {
            return this.concentrationRepository
                .GetAll()
                .MapAllWithIds<Concentration, ConcentrationDto>();
        }

        /// <summary>
        /// Returns all unite entities from the system.
        /// </summary>
        /// <returns>Enumeration of all unite entities.</returns>
        [HttpGet, Route("unites")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Maximum, ClientTimeSpan = (Int32) CacheDuration.Maximum)]
        public IEnumerable<WithId<Int32, UniteDto>> GetAllUnites()
        {
            return this.uniteRepository
                .GetAll()
                .MapAllWithIds<Unite, UniteDto>();
        }

        /// <summary>
        /// Returns all commanditaire types from the system.
        /// </summary>
        /// <returns>Enumeration of all commanditaire types.</returns>
        [HttpGet, Route("types-commanditaires")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Maximum, ClientTimeSpan = (Int32) CacheDuration.Maximum)]
        public IEnumerable<WithId<Int32, TypeCommanditaireDto>> GetAllTypeCommanditaires()
        {
            return this.typeCommanditaireRepository
                .GetAll()
                .MapAllWithIds<TypeCommanditaire, TypeCommanditaireDto>();
        }

        /// <summary>
        /// Returns all antecedent types from the system.
        /// </summary>
        /// <returns>Enumeration of all antecedent types.</returns>
        [HttpGet, Route("types-antecedents")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Maximum, ClientTimeSpan = (Int32) CacheDuration.Maximum)]
        public IEnumerable<WithId<Int32, TypeAntecedentDto>> GetAllTypeAntecedents()
        {
            return this.typeAntecedentRepository
                .GetAll()
                .MapAllWithIds<TypeAntecedent, TypeAntecedentDto>();
        }
    }
}