namespace Sporacid.Simplets.Webapp.Services.Services.Clubs
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;
    using Sporacid.Simplets.Webapp.Services.Resources.Contracts;
    using Sporacid.Simplets.Webapp.Tools.Strings;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Fournisseurs")]
    [Contextual("clubName")]
    [ContractClass(typeof (FournisseurServiceContract))]
    public interface IFournisseurService
    {
        /// <summary>
        /// Get all fournisseurs entities from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The fournisseur entities.</returns>
        [RequiredClaims(Claims.ReadAll)]
        IEnumerable<WithId<Int32, FournisseurDto>> GetAll(String clubName, UInt32? skip, UInt32? take);

        /// <summary>
        /// Get a fournisseur entity from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="fournisseurId">The fournisseur id.</param>
        /// <returns>The fournisseur entity.</returns>
        [RequiredClaims(Claims.Read)]
        FournisseurDto Get(String clubName, Int32 fournisseurId);

        /// <summary>
        /// Creates a fournisseur in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="fournisseur">The fournisseur.</param>
        /// <returns>The created fournisseur id.</returns>
        [RequiredClaims(Claims.Create)]
        Int32 Create(String clubName, FournisseurDto fournisseur);

        /// <summary>
        /// Udates a fournisseur in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="fournisseurId">The fournisseur id.</param>
        /// <param name="fournisseur">The fournisseur.</param>
        [RequiredClaims(Claims.Update)]
        void Update(String clubName, Int32 fournisseurId, FournisseurDto fournisseur);

        /// <summary>
        /// Deletes a fournisseur from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="fournisseurId">The fournisseur id.</param>
        [RequiredClaims(Claims.Delete)]
        void Delete(String clubName, Int32 fournisseurId);
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IFournisseurService))]
    internal abstract class FournisseurServiceContract : IFournisseurService
    {
        /// <summary>
        /// Get all fournisseurs from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The fournisseur entities.</returns>
        public IEnumerable<WithId<Int32, FournisseurDto>> GetAll(String clubName, UInt32? skip, UInt32? take)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.FournisseurService_GetAll_RequiresClubName);
            Contract.Requires(take == null || take > 0, ContractStrings.FournisseurService_GetAll_RequiresUndefinedOrPositiveTake);

            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<WithId<Int32, FournisseurDto>>>() != null,
                ContractStrings.FournisseurService_GetAll_EnsuresNonNullFournisseurs);

            // Dummy return.
            return default(IEnumerable<WithId<Int32, FournisseurDto>>);
        }

        /// <summary>
        /// Get a fournisseur from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="fournisseurId">The fournisseur id.</param>
        /// <returns>The fournisseur.</returns>
        public FournisseurDto Get(String clubName, Int32 fournisseurId)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.FournisseurService_Get_RequiresClubName);
            Contract.Requires(fournisseurId > 0, ContractStrings.FournisseurService_Get_RequiresPositiveFournisseurId);

            // Postconditions.
            Contract.Ensures(Contract.Result<FournisseurDto>() != null,
                ContractStrings.FournisseurService_Get_EnsuresNonNullFournisseur);

            // Dummy return.
            return default(FournisseurDto);
        }

        /// <summary>
        /// Creates a fournisseur in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="fournisseur">The fournisseur.</param>
        /// <returns>The created fournisseur id.</returns>
        public Int32 Create(String clubName, FournisseurDto fournisseur)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.FournisseurService_Create_RequiresClubName);
            Contract.Requires(fournisseur != null, ContractStrings.FournisseurService_Create_RequiresFournisseur);

            // Postconditions.
            Contract.Ensures(Contract.Result<Int32>() > 0, ContractStrings.FournisseurService_Create_EnsuresPositiveFournisseurId);

            // Dummy return.
            return default(Int32);
        }

        /// <summary>
        /// Udates a fournisseur in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="fournisseurId">The fournisseur id.</param>
        /// <param name="fournisseur">The fournisseur.</param>
        public void Update(String clubName, Int32 fournisseurId, FournisseurDto fournisseur)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.FournisseurService_Update_RequiresClubName);
            Contract.Requires(fournisseurId > 0, ContractStrings.FournisseurService_Update_RequiresPositiveFournisseurId);
            Contract.Requires(fournisseur != null, ContractStrings.FournisseurService_Update_RequiresFournisseur);
        }

        /// <summary>
        /// Deletes a fournisseur from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="fournisseurId">The fournisseur id.</param>
        public void Delete(String clubName, Int32 fournisseurId)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.FournisseurService_Delete_RequiresClubName);
            Contract.Requires(fournisseurId > 0, ContractStrings.FournisseurService_Delete_RequiresPositiveFournisseurId);
        }
    }
}