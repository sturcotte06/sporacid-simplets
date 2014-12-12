namespace Sporacid.Simplets.Webapp.Services.Services
{
    using System.Collections.Generic;
    using PostSharp.Patterns.Contracts;
    using Sporacid.Simplets.Webapp.Services.LinqToSql;
    using Sporacid.Simplets.Webapp.Services.Repositories.Dto;

    /// <summary>
    /// Interface for all membre services.
    /// </summary>
    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IMembreService
    {
        /// <summary>
        /// Subscribes a member entity to a club entity.
        /// </summary>
        /// <param name="clubId">The id of the club entity.</param>
        /// <param name="membreId">The id of the member entity.</param>
        void SubscribeToClub([Positive] int clubId, [Positive] int membreId);

        /// <summary>
        /// Unsubscribes a member entity from a club entity.
        /// </summary>
        /// <param name="clubId">The id of the club entity.</param>
        /// <param name="membreId">The id of the member entity.</param>
        void UnsubscribeFromClub([Positive] int clubId, [Positive] int membreId);

        /// <summary>
        /// Adds a member entity into the system.
        /// </summary>
        /// <param name="membre">The member entity.</param>
        void Add([Required] MembreDto membre);

        /// <summary>
        /// Deletes a member entity from the system.
        /// </summary>
        /// <param name="membreId">The id of the member entity.</param>
        void Delete([Positive] int membreId);

        /// <summary>
        /// Gets a member entity from the system.
        /// </summary>
        /// <param name="membreId">The id of the member entity.</param>
        /// <returns>The member entity.</returns>
        MembreDto Get([Positive] int membreId);

        /// <summary>
        /// Get all member entities subscribed to the club entity.
        /// </summary>
        /// <param name="clubId">The id of the club entity.</param>
        /// <returns>All subscribed member entities.</returns>
        IEnumerable<Membre> GetByClub([Positive] int clubId);
    }
}