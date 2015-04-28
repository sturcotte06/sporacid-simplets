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
    [Module("Membres")]
    [Contextual("clubName")]
    [ContractClass(typeof (MembreServiceContract))]
    public interface IMembreService : IService
    {
        /// <summary>
        /// Return all inscriton of a club entity.
        /// </summary>
        /// <param name="clubName">The id of the club entity.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        [RequiredClaims(Claims.Read | Claims.ReadAll)]
        IEnumerable<WithId<Int32, MembreDto>> GetAll(String clubName, UInt32? skip, UInt32? take);

        /// <summary>
        /// Get all membre entities in the given group from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="groupeId">The groupe id.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The fournisseur entities.</returns>
        [RequiredClaims(Claims.Read | Claims.ReadAll)]
        IEnumerable<WithId<Int32, MembreDto>> GetAllFromGroupe(String clubName, Int32 groupeId, UInt32? skip, UInt32? take);

        /// <summary>
        /// Get a membre entity from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="membreId">The membre id.</param>
        /// <returns>The membre entity.</returns>
        [RequiredClaims(Claims.Read)]
        MembreDto Get(String clubName, Int32 membreId);

        /// <summary>
        /// Subscribes a member entity to a club entity.
        /// </summary>
        /// <param name="clubName">The id of the club entity.</param>
        /// <param name="codeUniversel">The universal code that represents the user.</param>
        [RequiredClaims(Claims.Admin | Claims.Create)]
        void SubscribeToClub(String clubName, String codeUniversel);

        /// <summary>
        /// Unsubscribes a member entity from a club entity.
        /// </summary>
        /// <param name="clubName">The id of the club entity.</param>
        /// <param name="codeUniversel">The universal code that represents the user.</param>
        [RequiredClaims(Claims.Admin | Claims.Delete)]
        void UnsubscribeFromClub(String clubName, String codeUniversel);
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IMembreService))]
    internal abstract class MembreServiceContract : IMembreService
    {
        public IEnumerable<WithId<Int32, MembreDto>> GetAll(String clubName, UInt32? skip, UInt32? take)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.MembreService_GetAll_RequiresClubName);
            Contract.Requires(take == null || take > 0, ContractStrings.MembreService_GetAll_RequiresUndefinedOrPositiveTake);

            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<WithId<Int32, MembreDto>>>() != null, ContractStrings.MembreService_GetAll_EnsuresNonNullMembres);

            // Dummy return.
            return default(IEnumerable<WithId<Int32, MembreDto>>);
        }

        public IEnumerable<WithId<Int32, MembreDto>> GetAllFromGroupe(String clubName, Int32 groupeId, UInt32? skip, UInt32? take)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.MembreService_GetAllFromGroupe_RequiresClubName);
            Contract.Requires(take == null || take > 0, ContractStrings.MembreService_GetAllFromGroupe_RequiresUndefinedOrPositiveTake);
            Contract.Requires(groupeId > 0, ContractStrings.MembreService_GetAllFromGroupe_RequiresPositiveGroupeId);

            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<WithId<Int32, MembreDto>>>() != null, ContractStrings.MembreService_GetAllFromGroupe_EnsuresNonNullMembres);

            // Dummy return.
            return default(IEnumerable<WithId<Int32, MembreDto>>);
        }

        public MembreDto Get(String clubName, Int32 membreId)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.MembreService_Get_RequiresClubName);
            Contract.Requires(membreId > 0, ContractStrings.MembreService_Get_RequiresPositiveMembreId);

            // Postconditions.
            Contract.Ensures(Contract.Result<MembreDto>() != null, ContractStrings.MembreService_Get_EnsuresNonNullMembre);

            // Dummy return.
            return default(MembreDto);
        }

        public void SubscribeToClub(String clubName, String codeUniversel)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.MembreService_SubscribeToClub_RequiresClubName);
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.MembreService_SubscribeToClub_RequiresCodeUniversel);
        }

        public void UnsubscribeFromClub(String clubName, String codeUniversel)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.MembreService_UnsubscribeFromClub_RequiresClubName);
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.MembreService_UnsubscribeFromClub_RequiresCodeUniversel);
        }
    }
}