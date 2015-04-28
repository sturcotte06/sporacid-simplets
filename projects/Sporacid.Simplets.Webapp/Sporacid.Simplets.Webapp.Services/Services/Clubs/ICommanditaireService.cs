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
    [Module("Commanditaires")]
    [Contextual("clubName")]
    [ContractClass(typeof (CommanditaireServiceContract))]
    internal interface ICommanditaireService : IService
    {
        /// <summary>
        /// Get all commanditaire entities from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The commanditaires entities.</returns>
        [RequiredClaims(Claims.ReadAll)]
        IEnumerable<WithId<Int32, CommanditaireDto>> GetAll(String clubName, UInt32? skip, UInt32? take);

        /// <summary>
        /// Get a commanditaire entity from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditaireId">The commanditaire id.</param>
        /// <returns>The meeting entity.</returns>
        [RequiredClaims(Claims.Read)]
        CommanditaireDto Get(String clubName, Int32 commanditaireId);

        /// <summary>
        /// Creates a commanditaire entity in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditaire">The commanditaire entity.</param>
        /// <returns>The created commanditaire id.</returns>
        [RequiredClaims(Claims.Create)]
        Int32 Create(String clubName, CommanditaireDto commanditaire);

        /// <summary>
        /// Updates a commanditaire entity in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditaireId">The commanditaire id.</param>
        /// <param name="commanditaire">The commanditaire entity.</param>
        [RequiredClaims(Claims.Update)]
        void Update(String clubName, Int32 commanditaireId, CommanditaireDto commanditaire);

        /// <summary>
        /// Deletes a commanditaire entity from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditaireId">The commanditaire id.</param>
        [RequiredClaims(Claims.Delete)]
        void Delete(String clubName, Int32 commanditaireId);
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (ICommanditaireService))]
    internal abstract class CommanditaireServiceContract : ICommanditaireService
    {
        public IEnumerable<WithId<Int32, CommanditaireDto>> GetAll(String clubName, UInt32? skip, UInt32? take)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.CommanditaireService_GetAll_RequiresClubName);
            Contract.Requires(take == null || take > 0, ContractStrings.CommanditaireService_GetAll_RequiresUndefinedOrPositiveTake);

            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<WithId<Int32, CommanditaireDto>>>() != null,
                ContractStrings.CommanditaireService_GetAll_EnsuresNonNullCommanditaires);

            // Dummy return.
            return default(IEnumerable<WithId<Int32, CommanditaireDto>>);
        }

        public CommanditaireDto Get(String clubName, Int32 commanditaireId)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.CommanditaireService_Get_RequiresClubName);
            Contract.Requires(commanditaireId > 0, ContractStrings.CommanditaireService_Get_RequiresPositiveCommanditaireId);

            // Postconditions.
            Contract.Ensures(Contract.Result<CommanditaireDto>() != null, ContractStrings.CommanditaireService_Get_EnsuresNonNullCommanditaire);

            // Dummy return.
            return default(CommanditaireDto);
        }

        public Int32 Create(String clubName, CommanditaireDto commanditaire)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.CommanditaireService_Create_RequiresClubName);
            Contract.Requires(commanditaire != null, ContractStrings.CommanditaireService_Create_RequiresCommanditaire);

            // Postconditions.
            Contract.Ensures(Contract.Result<Int32>() > 0, ContractStrings.CommanditaireService_Get_EnsuresPositiveCommanditaireId);

            // Dummy return.
            return default(Int32);
        }

        public void Update(String clubName, Int32 commanditaireId, CommanditaireDto commanditaire)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.CommanditaireService_Update_RequiresClubName);
            Contract.Requires(commanditaireId > 0, ContractStrings.CommanditaireService_Update_RequiresPositiveCommanditaireId);
            Contract.Requires(commanditaire != null, ContractStrings.CommanditaireService_Update_RequiresCommanditaire);
        }

        public void Delete(String clubName, Int32 commanditaireId)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.CommanditaireService_Delete_RequiresClubName);
            Contract.Requires(commanditaireId > 0, ContractStrings.MeetingService_Delete_RequiresPositiveCommanditaireId);
        }
    }
}