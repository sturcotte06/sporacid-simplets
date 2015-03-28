namespace Sporacid.Simplets.Webapp.Services.Services.Clubs
{
    using System;
    using System.Diagnostics.Contracts;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Resources.Contracts;
using System.Collections.Generic;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Inscriptions")]
    [Contextual("clubName")]
    [ContractClass(typeof (InscriptionServiceContract))]
    public interface IInscriptionService : IService
    {
        /// <summary>
        /// Subscribes a member entity to a club entity.
        /// </summary>
        /// <param name="clubName">The id of the club entity.</param>
        /// <param name="codeUniversel">The universal code that represents the user.</param>
        [RequiredClaims(Claims.Admin | Claims.Create)]
        void SubscribeToClub(String clubName, String codeUniversel);

        /// <summary>
        /// Unsubscribes a member entity from a club entity.
        /// </summary>
        /// <param name="clubName">The id of the club entity.</param>
        /// <param name="codeUniversel">The universal code that represents the user.</param>
        [RequiredClaims(Claims.Admin | Claims.Delete)]
        void UnsubscribeFromClub(String clubName, String codeUniversel);

        /// <summary>
        /// Return all inscriton of a club entity.
        /// </summary>
        /// <param name="clubName">The id of the club entity.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        [RequiredClaims(Claims.Read | Claims.ReadAll)]
        IEnumerable<dynamic> GetAllInscriptionsFromClub(String clubName, UInt32? skip, UInt32? take);
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IInscriptionService))]
    internal abstract class InscriptionServiceContract : IInscriptionService
    {
        public void SubscribeToClub(String clubName, String codeUniversel)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.InscriptionService_SubscribeToClub_RequiresClubName);
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.InscriptionService_SubscribeToClub_RequiresCodeUniversel);
        }

        public void UnsubscribeFromClub(String clubName, String codeUniversel)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.InscriptionService_UnsubscribeFromClub_RequiresClubName);
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.InscriptionService_UnsubscribeFromClub_RequiresCodeUniversel);
        }

        public IEnumerable<dynamic> GetAllInscriptionsFromClub(String clubName, UInt32? skip, UInt32? take)
        {
            return null;
        }
    }
}