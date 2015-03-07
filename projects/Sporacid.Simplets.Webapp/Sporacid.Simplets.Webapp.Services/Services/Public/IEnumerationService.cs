namespace Sporacid.Simplets.Webapp.Services.Services.Public
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Dbo;
    using Sporacid.Simplets.Webapp.Services.Resources.Contracts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Enumerations")]
    [FixedContext(SecurityConfig.SystemContext)]
    [ContractClass(typeof (EnumerationServiceContract))]
    public interface IEnumerationService
    {
        /// <summary>
        /// Returns all type contact entities from the system.
        /// </summary>
        /// <returns>Enumeration of all type contact entities.</returns>
        [RequiredClaims(Claims.None)]
        IEnumerable<WithId<Int32, TypeContactDto>> GetAllTypesContacts();

        /// <summary>
        /// Returns all statut suivie entities from the system.
        /// </summary>
        /// <returns>Enumeration of all statuts suivie entities.</returns>
        [RequiredClaims(Claims.None)]
        IEnumerable<WithId<Int32, StatutSuivieDto>> GetAllStatutsSuivie();

        /// <summary>
        /// Returns all concentration entities from the system.
        /// </summary>
        /// <returns>Enumeration of all concentration entities.</returns>
        [RequiredClaims(Claims.None)]
        IEnumerable<WithId<Int32, ConcentrationDto>> GetAllConcentrations();

        /// <summary>
        /// Returns all unite entities from the system.
        /// </summary>
        /// <returns>Enumeration of all unite entities.</returns>
        [RequiredClaims(Claims.None)]
        IEnumerable<WithId<Int32, UniteDto>> GetAllUnites();
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IEnumerationService))]
    internal abstract class EnumerationServiceContract : IEnumerationService
    {
        /// <summary>
        /// Returns all type contact entities from the system.
        /// </summary>
        /// <returns>Enumeration of all type contact entities.</returns>
        public IEnumerable<WithId<Int32, TypeContactDto>> GetAllTypesContacts()
        {
            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<WithId<Int32, TypeContactDto>>>() != null, ContractStrings.EnumerationService_GetAllTypesContacts_EnsuresNonNullTypesContact);

            // Dummy return.
            return default(IEnumerable<WithId<Int32, TypeContactDto>>);
        }

        /// <summary>
        /// Returns all statut suivie entities from the system.
        /// </summary>
        /// <returns>Enumeration of all statuts suivie entities.</returns>
        public IEnumerable<WithId<Int32, StatutSuivieDto>> GetAllStatutsSuivie()
        {
            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<WithId<Int32, StatutSuivieDto>>>() != null, ContractStrings.EnumerationService_GetAllStatutsSuivie_EnsuresNonNullStatusSuivie);

            // Dummy return.
            return default(IEnumerable<WithId<Int32, StatutSuivieDto>>);
        }

        /// <summary>
        /// Returns all concentration entities from the system.
        /// </summary>
        /// <returns>Enumeration of all concentration entities.</returns>
        public IEnumerable<WithId<Int32, ConcentrationDto>> GetAllConcentrations()
        {
            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<WithId<Int32, ConcentrationDto>>>() != null, ContractStrings.EnumerationService_GetAllConcentrations_EnsuresNonNullConcentrations);

            // Dummy return.
            return default(IEnumerable<WithId<Int32, ConcentrationDto>>);
        }

        /// <summary>
        /// Returns all unite entities from the system.
        /// </summary>
        /// <returns>Enumeration of all unite entities.</returns>
        public IEnumerable<WithId<Int32, UniteDto>> GetAllUnites()
        {
            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<WithId<Int32, UniteDto>>>() != null, ContractStrings.EnumerationService_GetAllUnites_EnsuresNonNullUnites);

            // Dummy return.
            return default(IEnumerable<WithId<Int32, UniteDto>>);
        }
    }
}