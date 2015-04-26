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
    [Module("Preferences")]
    [Contextual("codeUniversel")]
    [ContractClass(typeof (PreferenceServiceContract))]
    public interface IPreferenceService : IService
    {
        /// <summary>
        /// Gets all preference entities from the user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The preference entities.</returns>
        [RequiredClaims(Claims.ReadAll)]
        IEnumerable<WithId<Int32, PreferenceDto>> GetAll(String codeUniversel, UInt32? skip, UInt32? take);

        /// <summary>
        /// Gets the preference entity from  the user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="preferenceId">The preferences id.</param>
        /// <returns>The preference entity.</returns>
        [RequiredClaims(Claims.Read)]
        PreferenceDto Get(String codeUniversel, Int32 preferenceId);

        /// <summary>
        /// Creates a preference entity in a user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="preference">The preference.</param>
        /// <returns>The created preference entity id.</returns>
        [RequiredClaims(Claims.Create)]
        Int32 Create(String codeUniversel, PreferenceDto preference);

        /// <summary>
        /// Updates the preference entity in a user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="preferenceId">The preference id.</param>
        /// <param name="preference">The preference.</param>
        [RequiredClaims(Claims.Update)]
        void Update(String codeUniversel, Int32 preferenceId, PreferenceDto preference);

        /// <summary>
        /// Deletes a preference entity from a user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="preferenceId">The preference id.</param>
        [RequiredClaims(Claims.Delete)]
        void Delete(String codeUniversel, Int32 preferenceId);
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IPreferenceService))]
    internal abstract class PreferenceServiceContract : IPreferenceService
    {
        public IEnumerable<WithId<Int32, PreferenceDto>> GetAll(String codeUniversel, UInt32? skip, UInt32? take)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.PreferenceService_GetAll_RequiresCodeUniversel);
            Contract.Requires(take == null || take > 0, ContractStrings.PreferenceService_GetAll_RequiresUndefinedOrPositiveTake);

            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<WithId<Int32, PreferenceDto>>>() != null, ContractStrings.PreferenceService_GetAll_EnsuresNonNullPreferences);

            // Dummy return.
            return default(IEnumerable<WithId<Int32, PreferenceDto>>);
        }

        public PreferenceDto Get(String codeUniversel, Int32 preferenceId)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.PreferenceService_Get_RequiresCodeUniversel);
            Contract.Requires(preferenceId > 0, ContractStrings.PreferenceService_Get_RequiresPositivePreferenceId);

            // Postconditions.
            Contract.Ensures(Contract.Result<PreferenceDto>() != null, ContractStrings.PreferenceService_Get_EnsuresNonNullPreference);

            // Dummy return.
            return default(PreferenceDto);
        }

        public Int32 Create(String codeUniversel, PreferenceDto preference)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.PreferenceService_Create_RequiresCodeUniversel);
            Contract.Requires(preference != null, ContractStrings.PreferenceService_Create_RequiresPreference);

            // Postconditions.
            Contract.Ensures(Contract.Result<Int32>() > 0, ContractStrings.PreferenceService_Create_EnsuresPositivePreferenceId);

            // Dummy return.
            return default(Int32);
        }

        public void Update(String codeUniversel, Int32 preferenceId, PreferenceDto preference)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.PreferenceService_Update_RequiresCodeUniversel);
            Contract.Requires(preferenceId > 0, ContractStrings.PreferenceService_Update_RequiresPositivePreferenceId);
            Contract.Requires(preference != null, ContractStrings.PreferenceService_Update_RequiresPreference);
        }

        public void Delete(String codeUniversel, Int32 preferenceId)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.PreferenceService_Delete_RequiresCodeUniversel);
            Contract.Requires(preferenceId > 0, ContractStrings.PreferenceService_Delete_RequiresPositivePreferenceId);
        }
    }
}