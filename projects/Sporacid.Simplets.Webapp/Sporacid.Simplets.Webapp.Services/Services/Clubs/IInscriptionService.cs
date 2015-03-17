namespace Sporacid.Simplets.Webapp.Services.Services.Clubs
{
    using System;
    using System.Diagnostics.Contracts;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Resources.Contracts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Inscriptions")]
    [Contextual("clubName")]
    [ContractClass(typeof (InscriptionServiceContract))]
    public interface IInscriptionService
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
    }
}