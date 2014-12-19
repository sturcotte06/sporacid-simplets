namespace Sporacid.Simplets.Webapp.Services.Services.Public
{
    using System.Collections.Generic;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Dbo;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Enumerations")]
    [FixedContext("Systeme")]
    public interface IEnumerationService
    {
        /// <summary>
        /// Returns all type contact entities from the system.
        /// </summary>
        /// <returns>Enumeration of all type contact entities.</returns>
        [RequiredClaims(Claims.None)]
        IEnumerable<TypeContactDto> GetAllTypesContacts();

        /// <summary>
        /// Returns all statut suivie entities from the system.
        /// </summary>
        /// <returns>Enumeration of all statuts suivie entities.</returns>
        [RequiredClaims(Claims.None)]
        IEnumerable<StatutSuivieDto> GetAllStatutsSuivie();

        /// <summary>
        /// Returns all concentration entities from the system.
        /// </summary>
        /// <returns>Enumeration of all concentration entities.</returns>
        [RequiredClaims(Claims.None)]
        IEnumerable<ConcentrationDto> GetAllConcentrations();
    }
}