namespace Sporacid.Simplets.Webapp.Services.Services.Clubs
{
    using System;
    using System.Collections.Generic;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Groupes")]
    [Contextual("clubName")]
    public interface IGroupeService
    {
        /// <summary>
        /// Get all groupe entities from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The groupe entities.</returns>
        [RequiredClaims(Claims.ReadAll)]
        IEnumerable<WithId<Int32, GroupeDto>> GetAll(String clubName, UInt32? skip, UInt32? take);

        /// <summary>
        /// Get a groupe entity from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="groupeId">The groupe id.</param>
        /// <returns>The groupe entity.</returns>
        [RequiredClaims(Claims.Read)]
        GroupeDto Get(String clubName, Int32 groupeId);

        /// <summary>
        /// Creates a groupe in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="groupe">The groupe.</param>
        /// <returns>The created groupe id.</returns>
        [RequiredClaims(Claims.Create)]
        Int32 Create(String clubName, GroupeDto groupe);

        /// <summary>
        /// Adds all membres to a groupe from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="groupeId">The groupe id.</param>
        /// <param name="membreIds">The enumeration of group ids.</param>
        [RequiredClaims(Claims.CreateAll)]
        void AddAllMembreToGroupe(String clubName, Int32 groupeId, IEnumerable<Int32> membreIds);

        /// <summary>
        /// Deletes all membres from a groupe from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="groupeId">The groupe id.</param>
        /// <param name="membreIds">The enumeration of group ids.</param>
        [RequiredClaims(Claims.DeleteAll)]
        void DeleteAllMembreToGroupe(String clubName, Int32 groupeId, IEnumerable<Int32> membreIds);

        /// <summary>
        /// Udates a groupe in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="groupeId">The groupe id.</param>
        /// <param name="groupe">The groupe.</param>
        [RequiredClaims(Claims.Update)]
        void Update(String clubName, Int32 groupeId, GroupeDto groupe);

        /// <summary>
        /// Deletes a groupe from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="groupeId">The groupe id.</param>
        [RequiredClaims(Claims.Delete)]
        void Delete(String clubName, Int32 groupeId);
    }
}