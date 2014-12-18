namespace Sporacid.Simplets.Webapp.Services.Services
{
    using System;
    using PostSharp.Patterns.Contracts;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Subscription")]
    [Contextual("clubName")]
    public interface ISubscriptionService
    {
        /// <summary>
        /// Subscribes a member entity to a club entity.
        /// </summary>
        /// <param name="clubName">The id of the club entity.</param>
        /// <param name="membreId">The id of the member entity.</param>
        [RequiredClaims(Claims.Admin | Claims.Create)]
        void SubscribeToClub([Required] String clubName, [Positive] int membreId);

        /// <summary>
        /// Unsubscribes a member entity from a club entity.
        /// </summary>
        /// <param name="clubName">The id of the club entity.</param>
        /// <param name="membreId">The id of the member entity.</param>
        [RequiredClaims(Claims.Admin | Claims.Delete)]
        void UnsubscribeFromClub([Required] String clubName, [Positive] int membreId);
    }
}