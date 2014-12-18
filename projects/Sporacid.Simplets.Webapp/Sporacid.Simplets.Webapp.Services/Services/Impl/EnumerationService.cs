namespace Sporacid.Simplets.Webapp.Services.Services.Impl
{
    using System.Collections.Generic;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/enumerations")]
    public class EnumerationService : BaseService, IEnumerationService
    {
        /// <summary>
        /// Returns all club entities from the system.
        /// </summary>
        /// <returns>Enumeration of all club entities.</returns>
        public IEnumerable<ClubDto> GetAllClubs()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Returns all allergie entities from the system.
        /// </summary>
        /// <returns>Enumeration of all allergie entities.</returns>
        public IEnumerable<AllergieDto> GetAllAllergies()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Returns all lien parente entities from the system.
        /// </summary>
        /// <returns>Enumeration of all lien parente entities.</returns>
        public IEnumerable<LienParenteDto> GetAllLiensParentes()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Returns all statut suivie entities from the system.
        /// </summary>
        /// <returns>Enumeration of all statuts suivie entities.</returns>
        public IEnumerable<StatutSuivieDto> GetAllStatutsSuivie()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Returns all concentration entities from the system.
        /// </summary>
        /// <returns>Enumeration of all concentration entities.</returns>
        public IEnumerable<ConcentrationDto> GetAllConcentrations()
        {
            throw new System.NotImplementedException();
        }
    }
}