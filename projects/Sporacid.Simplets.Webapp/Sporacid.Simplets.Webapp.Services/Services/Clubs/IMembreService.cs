namespace Sporacid.Simplets.Webapp.Services.Services.Clubs
{
    using System;
    using System.Collections.Generic;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Membres")]
    [Contextual("clubName")]
    public interface IMembreService
    {
        /// <summary>
        /// Get all membre entities from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The fournisseur entities.</returns>
        [RequiredClaims(Claims.ReadAll)]
        IEnumerable<WithId<Int32, MembreDto>> GetAll(String clubName, UInt32? skip, UInt32? take);

        /// <summary>
        /// Get all membre entities in the given group from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="groupeId">The groupe id.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The fournisseur entities.</returns>
        [RequiredClaims(Claims.ReadAll)]
        IEnumerable<WithId<Int32, MembreDto>> GetAllInGroupe(String clubName, Int32 groupeId, UInt32? skip, UInt32? take);

        /// <summary>
        /// Get a membre entity from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="membreId">The membre id.</param>
        /// <returns>The membre entity.</returns>
        [RequiredClaims(Claims.Read)]
        MembreDto Get(String clubName, Int32 membreId);

        /// <summary>
        /// Creates a membre in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="membre">The membre.</param>
        /// <returns>The created membre id.</returns>
        [RequiredClaims(Claims.Create)]
        Int32 Create(String clubName, MembreDto membre);

        /// <summary>
        /// Udates a membre in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="membreId">The membre id.</param>
        /// <param name="membre">The membre.</param>
        [RequiredClaims(Claims.Update)]
        void Update(String clubName, Int32 membreId, MembreDto membre);

        /// <summary>
        /// Deletes a membre from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="membreId">The membre id.</param>
        [RequiredClaims(Claims.Delete)]
        void Delete(String clubName, Int32 membreId);
    }
}