namespace Sporacid.Simplets.Webapp.Services.Services
{
    using System.Collections.Generic;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Enumerations")]
    [FixedContext("Enumerations")]
    public interface IEnumerationService
    {
        /// <summary>
        /// Returns all club entities from the system.
        /// </summary>
        /// <returns>Enumeration of all club entities.</returns>
        [RequiredClaims(Claims.ReadAll)]
        IEnumerable<ClubDto> GetAllClubs();

        /// <summary>
        /// Returns all allergie entities from the system.
        /// </summary>
        /// <returns>Enumeration of all allergie entities.</returns>
        [RequiredClaims(Claims.ReadAll)]
        IEnumerable<AllergieDto> GetAllAllergies();

        /// <summary>
        /// Returns all lien parente entities from the system.
        /// </summary>
        /// <returns>Enumeration of all lien parente entities.</returns>
        [RequiredClaims(Claims.ReadAll)]
        IEnumerable<LienParenteDto> GetAllLiensParentes();

        /// <summary>
        /// Returns all statut suivie entities from the system.
        /// </summary>
        /// <returns>Enumeration of all statuts suivie entities.</returns>
        [RequiredClaims(Claims.ReadAll)]
        IEnumerable<StatutSuivieDto> GetAllStatutsSuivie();

        /// <summary>
        /// Returns all concentration entities from the system.
        /// </summary>
        /// <returns>Enumeration of all concentration entities.</returns>
        [RequiredClaims(Claims.ReadAll)]
        IEnumerable<ConcentrationDto> GetAllConcentrations();
    }
}