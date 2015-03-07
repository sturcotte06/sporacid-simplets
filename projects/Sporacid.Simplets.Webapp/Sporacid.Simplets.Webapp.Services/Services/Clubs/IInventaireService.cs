namespace Sporacid.Simplets.Webapp.Services.Services.Clubs
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;
    using Sporacid.Simplets.Webapp.Services.Resources.Contracts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Inventaire")]
    [Contextual("clubName")]
    [ContractClass(typeof (InventaireServiceContract))]
    public interface IInventaireService
    {
        /// <summary>
        /// Get all item entities from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The item entities.</returns>
        [RequiredClaims(Claims.ReadAll)]
        IEnumerable<WithId<Int32, ItemDto>> GetAll(String clubName, UInt32? skip, UInt32? take);

        /// <summary>
        /// Get an item entity from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="itemId">The item entity id.</param>
        /// <returns>The item entity.</returns>
        [RequiredClaims(Claims.Read)]
        ItemDto Get(String clubName, Int32 itemId);

        /// <summary>
        /// Creates an item entity in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="item">The item entity.</param>
        /// <returns>The created item entity id.</returns>
        [RequiredClaims(Claims.Create)]
        Int32 Create(String clubName, ItemDto item);

        /// <summary>
        /// Updates an item entity in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="itemId">The item entity id.</param>
        /// <param name="item">The item entity.</param>
        [RequiredClaims(Claims.Update)]
        void Update(String clubName, Int32 itemId, ItemDto item);

        /// <summary>
        /// Deletes an item entity from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="itemId">The item entity id.</param>
        [RequiredClaims(Claims.Delete)]
        void Delete(String clubName, Int32 itemId);
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IInventaireService))]
    internal abstract class InventaireServiceContract : IInventaireService
    {
        /// <summary>
        /// Get all item entities from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The item entities.</returns>
        public IEnumerable<WithId<Int32, ItemDto>> GetAll(String clubName, UInt32? skip, UInt32? take)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.InventaireService_GetAll_RequiresClubName);
            Contract.Requires(take == null || take > 0, ContractStrings.InventaireService_GetAll_RequiresUndefinedOrPositiveTake);

            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<WithId<Int32, ItemDto>>>() != null,
                ContractStrings.InventaireService_GetAll_EnsuresNonNullItems);

            // Dummy return.
            return default(IEnumerable<WithId<Int32, ItemDto>>);
        }

        /// <summary>
        /// Get an item entity from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="itemId">The item entity id.</param>
        /// <returns>The item entity.</returns>
        public ItemDto Get(String clubName, Int32 itemId)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.InventaireService_Get_RequiresClubName);
            Contract.Requires(itemId > 0, ContractStrings.InventaireService_Get_RequiresPositiveItemId);

            // Postconditions.
            Contract.Ensures(Contract.Result<ItemDto>() != null, ContractStrings.InventaireService_Get_EnsuresNonNullItem);

            // Dummy return.
            return default(ItemDto);
        }

        /// <summary>
        /// Creates an item entity in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="item">The item entity.</param>
        /// <returns>The created item entity id.</returns>
        public Int32 Create(String clubName, ItemDto item)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.InventaireService_Create_RequiresClubName);
            Contract.Requires(item != null, ContractStrings.InventaireService_Create_RequiresItem);

            // Postconditions.
            Contract.Ensures(Contract.Result<Int32>() > 0, ContractStrings.InventaireService_Get_EnsuresPositiveItemId);

            // Dummy return.
            return default(Int32);
        }

        /// <summary>
        /// Updates an item entity in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="itemId">The item entity id.</param>
        /// <param name="item">The item entity.</param>
        public void Update(String clubName, Int32 itemId, ItemDto item)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.InventaireService_Update_RequiresClubName);
            Contract.Requires(itemId > 0, ContractStrings.InventaireService_Update_RequiresPositiveItemId);
            Contract.Requires(item != null, ContractStrings.InventaireService_Update_RequiresItem);
        }

        /// <summary>
        /// Deletes an item entity from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="itemId">The item entity id.</param>
        public void Delete(String clubName, Int32 itemId)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.InventaireService_Delete_RequiresClubName);
            Contract.Requires(itemId > 0, ContractStrings.InventaireService_Delete_RequiresPositiveItemId);
        }
    }
}