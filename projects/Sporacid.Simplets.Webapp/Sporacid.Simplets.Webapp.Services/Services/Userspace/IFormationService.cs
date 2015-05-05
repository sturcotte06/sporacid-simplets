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
    [Module("Formations")]
    [Contextual("codeUniversel")]
    [ContractClass(typeof (FormationServiceContract))]
    public interface IFormationService : IService
    {
        /// <summary>
        /// Gets all formation entities from the user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The formation entities.</returns>
        [RequiredClaims(Claims.ReadAll)]
        IEnumerable<WithId<Int32, FormationDto>> GetAll(String codeUniversel, UInt32? skip, UInt32? take);

        /// <summary>
        /// Gets the formation entity from  the user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="formationId">The formation id.</param>
        /// <returns>The formation entity.</returns>
        [RequiredClaims(Claims.Read)]
        FormationDto Get(String codeUniversel, Int32 formationId);

        /// <summary>
        /// Creates a formation entity in a user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="formation">The formation.</param>
        /// <returns>The created formation entity id.</returns>
        [RequiredClaims(Claims.Create)]
        Int32 Create(String codeUniversel, FormationDto formation);

        /// <summary>
        /// Updates the formation entity in a user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="formationId">The formation id.</param>
        /// <param name="formation">The formation.</param>
        [RequiredClaims(Claims.Update)]
        void Update(String codeUniversel, Int32 formationId, FormationDto formation);

        /// <summary>
        /// Deletes a formation entity from a user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="formationId">The formation id.</param>
        [RequiredClaims(Claims.Delete)]
        void Delete(String codeUniversel, Int32 formationId);
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IFormationService))]
    internal abstract class FormationServiceContract : IFormationService
    {
        public IEnumerable<WithId<Int32, FormationDto>> GetAll(String codeUniversel, UInt32? skip, UInt32? take)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.FormationService_GetAll_RequiresCodeUniversel);
            Contract.Requires(take == null || take > 0, ContractStrings.FormationService_GetAll_RequiresUndefinedOrPositiveTake);

            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<WithId<Int32, FormationDto>>>() != null, ContractStrings.FormationService_GetAll_EnsuresNonNullFormations);

            // Dummy return.
            return default(IEnumerable<WithId<Int32, FormationDto>>);
        }

        public FormationDto Get(String codeUniversel, Int32 formationId)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.FormationService_Get_RequiresCodeUniversel);
            Contract.Requires(formationId > 0, ContractStrings.FormationService_Get_RequiresPositiveFormationId);

            // Postconditions.
            Contract.Ensures(Contract.Result<FormationDto>() != null, ContractStrings.FormationService_Get_EnsuresNonNullFormation);

            // Dummy return.
            return default(FormationDto);
        }

        public Int32 Create(String codeUniversel, FormationDto formation)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.FormationService_Create_RequiresCodeUniversel);
            Contract.Requires(formation != null, ContractStrings.FormationService_Create_RequiresFormation);

            // Postconditions.
            Contract.Ensures(Contract.Result<Int32>() > 0, ContractStrings.FormationService_Create_EnsuresPositiveFormationId);

            // Dummy return.
            return default(Int32);
        }

        public void Update(String codeUniversel, Int32 formationId, FormationDto formation)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.FormationService_Update_RequiresCodeUniversel);
            Contract.Requires(formationId > 0, ContractStrings.FormationService_Update_RequiresPositiveFormationId);
            Contract.Requires(formation != null, ContractStrings.FormationService_Update_RequiresFormation);
        }

        public void Delete(String codeUniversel, Int32 formationId)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.FormationService_Delete_RequiresCodeUniversel);
            Contract.Requires(formationId > 0, ContractStrings.FormationService_Delete_RequiresPositiveFormationId);
        }
    }
}