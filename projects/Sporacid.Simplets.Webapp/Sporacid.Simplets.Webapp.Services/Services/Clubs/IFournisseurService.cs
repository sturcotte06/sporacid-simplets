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
    [Module("Fournisseurs")]
    [Contextual("clubName")]
    public interface IFournisseurService
    {
        /// <summary>
        /// Get all fournisseurs from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <returns>The fournisseur entities.</returns>
        [RequiredClaims(Claims.ReadAll)]
        IEnumerable<WithId<Int32, FournisseurDto>> GetAll([Required] String clubName);

        /// <summary>
        /// Get a fournisseur from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="fournisseurId">The fournisseur id.</param>
        /// <returns>The fournisseur.</returns>
        [RequiredClaims(Claims.Read)]
        FournisseurDto Get([Required] String clubName, [Positive] Int32 fournisseurId);

        /// <summary>
        /// Creates a fournisseur in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="fournisseur">The fournisseur.</param>
        /// <returns>The created fournisseur id.</returns>
        [RequiredClaims(Claims.Create)]
        Int32 Create([Required] String clubName, [Required] FournisseurDto fournisseur);

        /// <summary>
        /// Udates a fournisseur in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="fournisseurId">The fournisseur id.</param>
        /// <param name="fournisseur">The fournisseur.</param>
        [RequiredClaims(Claims.Update)]
        void Update([Required] String clubName, [Positive] Int32 fournisseurId, [Required] FournisseurDto fournisseur);

        /// <summary>
        /// Deletes a fournisseur from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="fournisseurId">The fournisseur id.</param>
        [RequiredClaims(Claims.Delete)]
        void Delete([Required] String clubName, [Positive] Int32 fournisseurId);
    }
}