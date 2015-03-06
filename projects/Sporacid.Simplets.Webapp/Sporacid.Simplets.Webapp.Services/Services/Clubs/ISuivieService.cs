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
    [Module("SuiviesCommandites")]
    [Contextual("clubName")]
    [ContractClass(typeof (SuivieServiceContract))]
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

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (ISuivieService))]
    internal abstract class SuivieServiceContract : ISuivieService
    {
        /// <summary>
        /// Gets all suivie from a commandite in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        /// <returns>The suivie entities.</returns>
        public IEnumerable<WithId<Int32, SuivieDto>> GetAll(String clubName, Int32 commanditeId)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.SuivieService_GetAll_RequiresClubName);
            Contract.Requires(commanditeId > 0, ContractStrings.SuivieService_GetAll_RequiresPositiveCommanditeId);

            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<WithId<Int32, SuivieDto>>>() != null,
                ContractStrings.SuivieService_GetAll_EnsuresNonNullSuivies);

            // Dummy return.
            return default(IEnumerable<WithId<Int32, SuivieDto>>);
        }

        /// <summary>
        /// Gets a suivie from a commandite in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        /// <param name="suivieId">The suivie id.</param>
        /// <returns>The suivie entity.</returns>
        public SuivieDto Get(String clubName, Int32 commanditeId, Int32 suivieId)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.SuivieService_Get_RequiresClubName);
            Contract.Requires(commanditeId > 0, ContractStrings.SuivieService_Get_RequiresPositiveCommanditeId);
            Contract.Requires(suivieId > 0, ContractStrings.SuivieService_Get_RequiresPositiveSuivieId);

            // Postconditions.
            Contract.Ensures(Contract.Result<SuivieDto>() != null, ContractStrings.SuivieService_Get_EnsuresNonNullSuivie);

            // Dummy return.
            return default(SuivieDto);
        }

        /// <summary>
        /// Creates a suivie for commandite in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        /// <param name="suivie">The suivie.</param>
        /// <returns>The created suivie id.</returns>
        public Int32 Create(String clubName, Int32 commanditeId, SuivieDto suivie)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.SuivieService_Create_RequiresClubName);
            Contract.Requires(commanditeId > 0, ContractStrings.SuivieService_Create_RequiresPositiveCommanditeId);
            Contract.Requires(suivie != null, ContractStrings.SuivieService_Create_RequiresSuivie);

            // Postconditions.
            Contract.Ensures(Contract.Result<Int32>() > 0, ContractStrings.SuivieService_Create_EnsuresPositiveSuivieId);

            // Dummy return.
            return default(Int32);
        }

        /// <summary>
        /// Updates a suivie for a commandite in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        /// <param name="suivieId">The suivie id.</param>
        /// <param name="suivie">The suivie.</param>
        public void Update(String clubName, Int32 commanditeId, Int32 suivieId, SuivieDto suivie)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.SuivieService_Update_RequiresClubName);
            Contract.Requires(commanditeId > 0, ContractStrings.SuivieService_Update_RequiresPositiveCommanditeId);
            Contract.Requires(suivieId > 0, ContractStrings.SuivieService_Update_RequiresPositiveSuivieId);
            Contract.Requires(suivie != null, ContractStrings.SuivieService_Update_RequiresSuivie);
        }

        /// <summary>
        /// Deletes a suivie from a commandite in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="commanditeId">The commandite id.</param>
        /// <param name="suivieId">The suivie id.</param>
        public void Delete(String clubName, Int32 commanditeId, Int32 suivieId)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.SuivieService_Delete_RequiresClubName);
            Contract.Requires(commanditeId > 0, ContractStrings.SuivieService_Delete_RequiresPositiveCommanditeId);
            Contract.Requires(suivieId > 0, ContractStrings.SuivieService_Delete_RequiresPositiveSuivieId);
        }
    }
}