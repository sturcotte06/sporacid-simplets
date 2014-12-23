namespace Sporacid.Simplets.Webapp.Services.Services.Clubs
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("SuiviesCommandites")]
    [Contextual("clubName")]
    public interface ISuivieService
    {
        /// <summary>
        /// Gets all suivie from a commandite in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        /// <returns>The suivie entities.</returns>
        [RequiredClaims(Claims.ReadAll)]
        IEnumerable<WithId<Int32, SuivieDto>> GetAll(String clubName, Int32 commanditeId);

        /// <summary>
        /// Gets a suivie from a commandite in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        /// <param name="suivieId">The suivie id.</param>
        /// <returns>The suivie entity.</returns>
        [RequiredClaims(Claims.Read)]
        SuivieDto Get(String clubName, Int32 commanditeId, Int32 suivieId);

        /// <summary>
        /// Creates a suivie for commandite in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        /// <param name="suivie">The suivie.</param>
        /// <returns>The created suivie id.</returns>
        [RequiredClaims(Claims.Create)]
        Int32 Create(String clubName, Int32 commanditeId, SuivieDto suivie);

        /// <summary>
        /// Updates a suivie for a commandite in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        /// <param name="suivieId">The suivie id.</param>
        /// <param name="suivie">The suivie.</param>
        [RequiredClaims(Claims.Update)]
        void Update(String clubName, Int32 commanditeId, Int32 suivieId, SuivieDto suivie);

        /// <summary>
        /// Deletes a suivie from a commandite in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        /// <param name="suivieId">The suivie id.</param>
        [RequiredClaims(Claims.Delete)]
        void Delete(String clubName, Int32 commanditeId, Int32 suivieId);
    }
}