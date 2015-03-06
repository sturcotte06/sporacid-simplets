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
    [Module("Commandites")]
    [Contextual("clubName")]
    [ContractClass(typeof (CommanditeServiceContract))]
    public interface ICommanditeService
    {
        /// <summary>
        /// Get all commandite from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <returns>The commandite.</returns>
        [RequiredClaims(Claims.ReadAll)]
        IEnumerable<WithId<Int32, CommanditeDto>> GetAll(String clubName);

        /// <summary>
        /// Get a commandite from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        /// <returns>The commandite.</returns>
        [RequiredClaims(Claims.Read)]
        CommanditeDto Get(String clubName, Int32 commanditeId);

        /// <summary>
        /// Creates a commandite in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commandite">The commandite.</param>
        /// <returns>The created commandite id.</returns>
        [RequiredClaims(Claims.Create)]
        Int32 Create(String clubName, CommanditeDto commandite);

        /// <summary>
        /// Udates a commandite in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        /// <param name="commandite">The commandite.</param>
        [RequiredClaims(Claims.Update)]
        void Update(String clubName, Int32 commanditeId, CommanditeDto commandite);

        /// <summary>
        /// Deletes a commandite from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        [RequiredClaims(Claims.Delete)]
        void Delete(String clubName, Int32 commanditeId);
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (ICommanditeService))]
    internal abstract class CommanditeServiceContract : ICommanditeService
    {
        /// <summary>
        /// Get all commandite from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <returns>The commandite.</returns>
        public IEnumerable<WithId<Int32, CommanditeDto>> GetAll(String clubName)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.CommanditeService_GetAll_RequiresClubName);

            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<WithId<Int32, CommanditeDto>>>() != null,
                ContractStrings.CommanditeService_GetAll_EnsuresNonNullCommandites);

            // Dummy return.
            return default(IEnumerable<WithId<Int32, CommanditeDto>>);
        }

        /// <summary>
        /// Get a commandite from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        /// <returns>The commandite.</returns>
        public CommanditeDto Get(String clubName, Int32 commanditeId)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.CommanditeService_Get_RequiresClubName);
            Contract.Requires(commanditeId > 0, ContractStrings.CommanditeService_Get_RequiresPositiveCommanditeId);

            // Postconditions.
            Contract.Ensures(Contract.Result<CommanditeDto>() != null, ContractStrings.CommanditeService_Get_EnsuresNonNullCommandite);

            // Dummy return.
            return default(CommanditeDto);
        }

        /// <summary>
        /// Creates a commandite in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commandite">The commandite.</param>
        /// <returns>The created commandite id.</returns>
        public Int32 Create(String clubName, CommanditeDto commandite)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.CommanditeService_Create_RequiresClubName);
            Contract.Requires(commandite != null, ContractStrings.CommanditeService_Create_RequiresCommandite);

            // Postconditions.
            Contract.Ensures(Contract.Result<Int32>() > 0, ContractStrings.CommanditeService_Get_EnsuresPositiveCommanditeId);

            // Dummy return.
            return default(Int32);
        }

        /// <summary>
        /// Udates a commandite in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        /// <param name="commandite">The commandite.</param>
        public void Update(String clubName, Int32 commanditeId, CommanditeDto commandite)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.CommanditeService_Update_RequiresClubName);
            Contract.Requires(commanditeId > 0, ContractStrings.CommanditeService_Update_RequiresPositiveCommanditeId);
            Contract.Requires(commandite != null, ContractStrings.CommanditeService_Update_RequiresCommandite);
        }

        /// <summary>
        /// Deletes a commandite from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        public void Delete(String clubName, Int32 commanditeId)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.CommanditeService_Delete_RequiresClubName);
            Contract.Requires(commanditeId > 0, ContractStrings.CommanditeService_Delete_RequiresPositiveCommanditeId);
        }
    }
}