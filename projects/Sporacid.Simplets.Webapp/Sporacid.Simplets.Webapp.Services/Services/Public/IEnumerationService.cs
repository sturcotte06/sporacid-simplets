namespace Sporacid.Simplets.Webapp.Services.Services.Public
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Dbo;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Userspace;
    using Sporacid.Simplets.Webapp.Services.Resources.Contracts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Enumerations")]
    [FixedContext(SecurityConfig.SystemContext)]
    [ContractClass(typeof (EnumerationServiceContract))]
    public interface IEnumerationService : IService
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

        /// <summary>
        /// Returns all commanditaire types from the system.
        /// </summary>
        /// <returns>Enumeration of all commanditaire types.</returns>
        [RequiredClaims(Claims.None)]
        IEnumerable<WithId<Int32, TypeCommanditaireDto>> GetAllTypeCommanditaires();

        /// <summary>
        /// Returns all fournisseur types from the system.
        /// </summary>
        /// <returns>Enumeration of all fournisseur types.</returns>
        [RequiredClaims(Claims.None)]
        IEnumerable<WithId<Int32, TypeFournisseurDto>> GetAllTypeFournisseurs();

        /// <summary>
        /// Returns all antecedent types from the system.
        /// </summary>
        /// <returns>Enumeration of all antecedent types.</returns>
        [RequiredClaims(Claims.None)]
        IEnumerable<WithId<Int32, TypeAntecedentDto>> GetAllTypeAntecedents();
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IEnumerationService))]
    internal abstract class EnumerationServiceContract : IEnumerationService
    {
        public IEnumerable<WithId<Int32, TypeContactDto>> GetAllTypesContacts()
        {
            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<WithId<Int32, TypeContactDto>>>() != null, ContractStrings.EnumerationService_GetAllTypesContacts_EnsuresNonNullTypesContact);

            // Dummy return.
            return default(IEnumerable<WithId<Int32, TypeContactDto>>);
        }

        public IEnumerable<WithId<Int32, StatutSuivieDto>> GetAllStatutsSuivie()
        {
            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<WithId<Int32, StatutSuivieDto>>>() != null, ContractStrings.EnumerationService_GetAllStatutsSuivie_EnsuresNonNullStatusSuivie);

            // Dummy return.
            return default(IEnumerable<WithId<Int32, StatutSuivieDto>>);
        }

        public IEnumerable<WithId<Int32, ConcentrationDto>> GetAllConcentrations()
        {
            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<WithId<Int32, ConcentrationDto>>>() != null, ContractStrings.EnumerationService_GetAllConcentrations_EnsuresNonNullConcentrations);

            // Dummy return.
            return default(IEnumerable<WithId<Int32, ConcentrationDto>>);
        }

        public IEnumerable<WithId<Int32, UniteDto>> GetAllUnites()
        {
            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<WithId<Int32, UniteDto>>>() != null, ContractStrings.EnumerationService_GetAllUnites_EnsuresNonNullUnites);

            // Dummy return.
            return default(IEnumerable<WithId<Int32, UniteDto>>);
        }

        public IEnumerable<WithId<Int32, TypeCommanditaireDto>> GetAllTypeCommanditaires()
        {
            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<WithId<Int32, TypeCommanditaireDto>>>() != null, ContractStrings.EnumerationService_GetAllTypeCommanditaires_EnsuresNonNullTypeCommanditaires);

            // Dummy return.
            return default(IEnumerable<WithId<Int32, TypeCommanditaireDto>>);
        }

        public IEnumerable<WithId<Int32, TypeFournisseurDto>> GetAllTypeFournisseurs()
        {
            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<WithId<Int32, TypeFournisseurDto>>>() != null, ContractStrings.EnumerationService_GetAllTypeFournisseurs_EnsuresNonNullTypeFournisseurs);

            // Dummy return.
            return default(IEnumerable<WithId<Int32, TypeFournisseurDto>>);
        }

        public IEnumerable<WithId<Int32, TypeAntecedentDto>> GetAllTypeAntecedents()
        {
            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<WithId<Int32, TypeAntecedentDto>>>() != null, ContractStrings.EnumerationService_GetAllTypeAntecedents_EnsuresNonNullTypeAntecedents);

            // Dummy return.
            return default(IEnumerable<WithId<Int32, TypeAntecedentDto>>);
        }
    }
}