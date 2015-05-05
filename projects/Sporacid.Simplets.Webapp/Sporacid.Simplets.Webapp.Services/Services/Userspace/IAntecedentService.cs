namespace Sporacid.Simplets.Webapp.Services.Services.Userspace
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Userspace;
    using Sporacid.Simplets.Webapp.Services.Resources.Contracts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Antecedents")]
    [Contextual("codeUniversel")]
    [ContractClass(typeof (AntecedentServiceContract))]
    public interface IAntecedentService : IService
    {
        /// <summary>
        /// Gets all antecedent entities from the user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The antecedent entities.</returns>
        [RequiredClaims(Claims.ReadAll)]
        IEnumerable<WithId<Int32, AntecedentDto>> GetAll(String codeUniversel, UInt32? skip, UInt32? take);

        /// <summary>
        /// Gets the antecedent entity from  the user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="antecedentId">The antecedent id.</param>
        /// <returns>The antecedent entity.</returns>
        [RequiredClaims(Claims.Read)]
        AntecedentDto Get(String codeUniversel, Int32 antecedentId);

        /// <summary>
        /// Creates a antecedent entity in a user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="antecedent">The antecedent.</param>
        /// <returns>The created antecedent entity id.</returns>
        [RequiredClaims(Claims.Create)]
        Int32 Create(String codeUniversel, AntecedentDto antecedent);

        /// <summary>
        /// Updates the antecedent entity in a user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="antecedentId">The antecedent id.</param>
        /// <param name="antecedent">The antecedent.</param>
        [RequiredClaims(Claims.Update)]
        void Update(String codeUniversel, Int32 antecedentId, AntecedentDto antecedent);

        /// <summary>
        /// Deletes a antecedent entity from a user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="antecedentId">The antecedent id.</param>
        [RequiredClaims(Claims.Delete)]
        void Delete(String codeUniversel, Int32 antecedentId);
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IAntecedentService))]
    internal abstract class AntecedentServiceContract : IAntecedentService
    {
        public IEnumerable<WithId<Int32, AntecedentDto>> GetAll(String codeUniversel, UInt32? skip, UInt32? take)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.AntecedentService_GetAll_RequiresCodeUniversel);
            Contract.Requires(take == null || take > 0, ContractStrings.AntecedentService_GetAll_RequiresUndefinedOrPositiveTake);

            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<WithId<Int32, AntecedentDto>>>() != null, ContractStrings.AntecedentService_GetAll_EnsuresNonNullAntecedents);

            // Dummy return.
            return default(IEnumerable<WithId<Int32, AntecedentDto>>);
        }

        public AntecedentDto Get(String codeUniversel, Int32 antecedentId)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.AntecedentService_Get_RequiresCodeUniversel);
            Contract.Requires(antecedentId > 0, ContractStrings.AntecedentService_Get_RequiresPositiveAntecedentId);

            // Postconditions.
            Contract.Ensures(Contract.Result<AntecedentDto>() != null, ContractStrings.AntecedentService_Get_EnsuresNonNullAntecedent);

            // Dummy return.
            return default(AntecedentDto);
        }

        public Int32 Create(String codeUniversel, AntecedentDto antecedent)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.AntecedentService_Create_RequiresCodeUniversel);
            Contract.Requires(antecedent != null, ContractStrings.AntecedentService_Create_RequiresAntecedent);

            // Postconditions.
            Contract.Ensures(Contract.Result<Int32>() > 0, ContractStrings.AntecedentService_Create_EnsuresPositiveAntecedentId);

            // Dummy return.
            return default(Int32);
        }

        public void Update(String codeUniversel, Int32 antecedentId, AntecedentDto antecedent)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.AntecedentService_Update_RequiresCodeUniversel);
            Contract.Requires(antecedentId > 0, ContractStrings.AntecedentService_Update_RequiresPositiveAntecedentId);
            Contract.Requires(antecedent != null, ContractStrings.AntecedentService_Update_RequiresAntecedent);
        }

        public void Delete(String codeUniversel, Int32 antecedentId)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.AntecedentService_Delete_RequiresCodeUniversel);
            Contract.Requires(antecedentId > 0, ContractStrings.AntecedentService_Delete_RequiresPositiveAntecedentId);
        }
    }
}