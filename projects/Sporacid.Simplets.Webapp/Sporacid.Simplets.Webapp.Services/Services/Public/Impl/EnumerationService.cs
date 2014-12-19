﻿namespace Sporacid.Simplets.Webapp.Services.Services.Public.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using AutoMapper;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Dbo;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/enumerations")]
    public class EnumerationService : BaseService, IEnumerationService
    {
        private readonly IRepository<Int32, Concentration> concentrationRepository;
        private readonly IRepository<Int32, StatutSuivie> statutSuivieRepository;
        private readonly IRepository<Int32, TypeContact> typeContactRepository;

        public EnumerationService(IRepository<Int32, TypeContact> typeContactRepository,
            IRepository<Int32, Concentration> concentrationRepository, IRepository<Int32, StatutSuivie> statutSuivieRepository)
        {
            this.typeContactRepository = typeContactRepository;
            this.concentrationRepository = concentrationRepository;
            this.statutSuivieRepository = statutSuivieRepository;
        }

        /// <summary>
        /// Returns all type contact entities from the system.
        /// </summary>
        /// <returns>Enumeration of all type contact entities.</returns>
        [HttpGet]
        [Route("types-contacts")]
        public IEnumerable<TypeContactDto> GetAllTypesContacts()
        {
            var typeContactEntities = this.typeContactRepository.GetAll();
            return typeContactEntities.Select(typeContactEntity =>
                Mapper.Map<TypeContact, TypeContactDto>(typeContactEntity));
        }

        /// <summary>
        /// Returns all statut suivie entities from the system.
        /// </summary>
        /// <returns>Enumeration of all statuts suivie entities.</returns>
        [HttpGet]
        [Route("statuts-suivie")]
        public IEnumerable<StatutSuivieDto> GetAllStatutsSuivie()
        {
            var statutSuivieEntities = this.statutSuivieRepository.GetAll();
            return statutSuivieEntities.Select(statutSuivieEntity =>
                Mapper.Map<StatutSuivie, StatutSuivieDto>(statutSuivieEntity));
        }

        /// <summary>
        /// Returns all concentration entities from the system.
        /// </summary>
        /// <returns>Enumeration of all concentration entities.</returns>
        [HttpGet]
        [Route("concentrations")]
        public IEnumerable<ConcentrationDto> GetAllConcentrations()
        {
            var concentrationEntities = this.concentrationRepository.GetAll();
            return concentrationEntities.Select(concentrationEntity =>
                Mapper.Map<Concentration, ConcentrationDto>(concentrationEntity));
        }
    }
}