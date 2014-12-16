namespace Sporacid.Simplets.Webapp.Services.Services
{
    using System;
    using PostSharp.Patterns.Contracts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface ISubscriptionService
    {
        /// <summary>
        /// Subscribes a member entity to a club entity.
        /// </summary>
        /// <param name="clubId">The id of the club entity.</param>
        /// <param name="membreId">The id of the member entity.</param>
        void SubscribeToClub([Required] String clubName, [Positive] int membreId);

        /// <summary>
        /// Unsubscribes a member entity from a club entity.
        /// </summary>
        /// <param name="clubId">The id of the club entity.</param>
        /// <param name="membreId">The id of the member entity.</param>
        void UnsubscribeFromClub([Required] String clubName, [Positive] int membreId);
    }
}