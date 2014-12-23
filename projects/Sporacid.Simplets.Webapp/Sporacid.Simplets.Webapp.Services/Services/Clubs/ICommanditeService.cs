namespace Sporacid.Simplets.Webapp.Services.Services.Clubs
{
    using System;
    using System.Collections.Generic;
    using PostSharp.Patterns.Contracts;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Commandites")]
    [Contextual("clubName")]
    public interface ICommanditeService
    {
        /// <summary>
        /// Get all commandite from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <returns>The commandite.</returns>
        [RequiredClaims(Claims.ReadAll)]
        IEnumerable<WithId<Int32, CommanditeDto>> GetAll([Required] String clubName);

        /// <summary>
        /// Get a commandite from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        /// <returns>The commandite.</returns>
        [RequiredClaims(Claims.Read)]
        CommanditeDto Get([Required] String clubName, [Positive] Int32 commanditeId);

        /// <summary>
        /// Creates a commandite in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commandite">The commandite.</param>
        /// <returns>The created commandite id.</returns>
        [RequiredClaims(Claims.Create)]
        Int32 Create([Required] String clubName, [Required] CommanditeDto commandite);

        /// <summary>
        /// Udates a commandite in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        /// <param name="commandite">The commandite.</param>
        [RequiredClaims(Claims.Update)]
        void Update([Required] String clubName, [Positive] Int32 commanditeId, [Required] CommanditeDto commandite);

        /// <summary>
        /// Deletes a commandite from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        [RequiredClaims(Claims.Delete)]
        void Delete([Required] String clubName, [Positive] Int32 commanditeId);
    }
}