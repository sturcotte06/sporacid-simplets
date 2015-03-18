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
    [Module("Commandites")]
    [Contextual("clubName")]
    [ContractClass(typeof (CommanditeServiceContract))]
    public interface ICommanditeService : IService
    {
        /// <summary>
        /// Get all commandite entities from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditaireId">The commanditaire id.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The commandite entities.</returns>
        [RequiredClaims(Claims.ReadAll)]
        IEnumerable<WithId<Int32, CommanditeDto>> GetAll(String clubName, Int32 commanditaireId, UInt32? skip, UInt32? take);

        /// <summary>
        /// Get a commandite entity from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditaireId">The commanditaire id.</param>
        /// <param name="commanditeId">The commandite entity id.</param>
        /// <returns>The commandite entity.</returns>
        [RequiredClaims(Claims.Read)]
        CommanditeDto Get(String clubName, Int32 commanditaireId, Int32 commanditeId);

        /// <summary>
        /// Creates a commandite entity in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditaireId">The commanditaire id.</param>
        /// <param name="commandite">The commandite entity.</param>
        /// <returns>The created commandite entity id.</returns>
        [RequiredClaims(Claims.Create)]
        Int32 Create(String clubName, Int32 commanditaireId, CommanditeDto commandite);

        /// <summary>
        /// Updates a commandite entity in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditaireId">The commanditaire id.</param>
        /// <param name="commanditeId">The commandite entity id.</param>
        /// <param name="commandite">The commandite entity.</param>
        [RequiredClaims(Claims.Update)]
        void Update(String clubName, Int32 commanditaireId, Int32 commanditeId, CommanditeDto commandite);

        /// <summary>
        /// Deletes a commandite entity from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditaireId">The commanditaire id.</param>
        /// <param name="commanditeId">The commandite entity id.</param>
        [RequiredClaims(Claims.Delete)]
        void Delete(String clubName, Int32 commanditaireId, Int32 commanditeId);
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (ICommanditeService))]
    internal abstract class CommanditeServiceContract : ICommanditeService
    {
        public IEnumerable<WithId<Int32, CommanditeDto>> GetAll(String clubName, Int32 commanditaireId, UInt32? skip, UInt32? take)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.CommanditeService_GetAll_RequiresClubName);
            Contract.Requires(commanditaireId > 0, ContractStrings.CommanditeService_GetAll_RequiresPositiveCommanditaireId);
            Contract.Requires(take == null || take > 0, ContractStrings.CommanditeService_GetAll_RequiresUndefinedOrPositiveTake);

            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<WithId<Int32, CommanditeDto>>>() != null,
                ContractStrings.CommanditeService_GetAll_EnsuresNonNullCommandites);

            // Dummy return.
            return default(IEnumerable<WithId<Int32, CommanditeDto>>);
        }

        public CommanditeDto Get(String clubName, Int32 commanditaireId, Int32 commanditeId)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.CommanditeService_Get_RequiresClubName);
            Contract.Requires(commanditaireId > 0, ContractStrings.CommanditeService_Get_RequiresPositiveCommanditaireId);
            Contract.Requires(commanditeId > 0, ContractStrings.CommanditeService_Get_RequiresPositiveCommanditeId);

            // Postconditions.
            Contract.Ensures(Contract.Result<CommanditeDto>() != null, ContractStrings.CommanditeService_Get_EnsuresNonNullCommandite);

            // Dummy return.
            return default(CommanditeDto);
        }

        public Int32 Create(String clubName, Int32 commanditaireId, CommanditeDto commandite)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.CommanditeService_Create_RequiresClubName);
            Contract.Requires(commanditaireId > 0, ContractStrings.CommanditeService_Create_RequiresPositiveCommanditaireId);
            Contract.Requires(commandite != null, ContractStrings.CommanditeService_Create_RequiresCommandite);

            // Postconditions.
            Contract.Ensures(Contract.Result<Int32>() > 0, ContractStrings.CommanditeService_Get_EnsuresPositiveCommanditeId);

            // Dummy return.
            return default(Int32);
        }

        public void Update(String clubName, Int32 commanditaireId, Int32 commanditeId, CommanditeDto commandite)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.CommanditeService_Update_RequiresClubName);
            Contract.Requires(commanditaireId > 0, ContractStrings.CommanditeService_Update_RequiresPositiveCommanditaireId);
            Contract.Requires(commanditeId > 0, ContractStrings.CommanditeService_Update_RequiresPositiveCommanditeId);
            Contract.Requires(commandite != null, ContractStrings.CommanditeService_Update_RequiresCommandite);
        }

        public void Delete(String clubName, Int32 commanditaireId, Int32 commanditeId)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.CommanditeService_Delete_RequiresClubName);
            Contract.Requires(commanditaireId > 0, ContractStrings.CommanditeService_Delete_RequiresPositiveCommanditaireId);
            Contract.Requires(commanditeId > 0, ContractStrings.CommanditeService_Delete_RequiresPositiveCommanditeId);
        }
    }
}