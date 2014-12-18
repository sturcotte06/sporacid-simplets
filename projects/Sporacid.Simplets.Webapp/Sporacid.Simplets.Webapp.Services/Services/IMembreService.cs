namespace Sporacid.Simplets.Webapp.Services.Services
{
    using System;
    using PostSharp.Patterns.Contracts;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Membre")]
    [FixedContext("MembreAdministration")]
    public interface IMembreService
    {
        /// <summary>
        /// Adds a member entity into the system.
        /// </summary>
        /// <param name="membre">The member entity.</param>
        /// <returns>The id of the newly created membre entity.</returns>
        [RequiredClaims(Claims.Create)]
        Int32 Add([Required] MembreDto membre);

        /// <summary>
        /// Updates a member entity from the system.
        /// </summary>
        /// <param name="membre">The member entity.</param>
        [RequiredClaims(Claims.Update)]
        void Update([Required] MembreDto membre);

        /// <summary>
        /// Deletes a member entity from the system.
        /// </summary>
        /// <param name="membreId">The id of the member entity.</param>
        [RequiredClaims(Claims.Delete)]
        void Delete([Positive] int membreId);

        /// <summary>
        /// Gets a member entity from the system.
        /// </summary>
        /// <param name="membreId">The id of the member entity.</param>
        /// <returns>The member entity.</returns>
        [RequiredClaims(Claims.Read)]
        MembreDto Get([Positive] int membreId);
    }
}