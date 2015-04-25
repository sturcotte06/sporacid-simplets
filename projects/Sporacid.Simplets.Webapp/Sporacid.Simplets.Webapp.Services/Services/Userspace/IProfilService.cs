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
    [Module("Profils")]
    [Contextual("codeUniversel")]
    [ContractClass(typeof (ProfilServiceContract))]
    public interface IProfilService : IService
    {
        /// <summary>
        /// Gets the profil entity from the system.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <returns>The profil.</returns>
        [RequiredClaims(Claims.Admin | Claims.Read)]
        ProfilDto Get(String codeUniversel);

        /// <summary>
        /// Gets the public profil entity from the system.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <returns>The public profil.</returns>
        [RequiredClaims(Claims.None)]
        ProfilPublicDto GetPublic(String codeUniversel);

        /// <summary>
        /// Gets the preferences of the profil entity from the system.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <returns>The profil's preferences.</returns>
        [RequiredClaims(Claims.Read | Claims.ReadAll)]
        IEnumerable<WithId<Int32, PreferenceDto>> GetPreferences(String codeUniversel);

        /// <summary>
        /// Updates the profil entity in the system.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="profil">The profil.</param>
        [RequiredClaims(Claims.Admin | Claims.Update)]
        void Update(String codeUniversel, ProfilDto profil);
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IProfilService))]
    internal abstract class ProfilServiceContract : IProfilService
    {
        public ProfilDto Get(String codeUniversel)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.ProfilService_Get_RequiresCodeUniversel);

            // Postconditions.
            Contract.Ensures(Contract.Result<ProfilDto>() != null, ContractStrings.ProfilService_Get_EnsuresNonNullProfil);

            // Dummy return.
            return default(ProfilDto);
        }

        public ProfilPublicDto GetPublic(String codeUniversel)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.ProfilService_GetPublic_RequiresCodeUniversel);

            // Postconditions.
            Contract.Ensures(Contract.Result<ProfilPublicDto>() != null, ContractStrings.ProfilService_GetPublic_EnsuresNonNullPublicProfil);

            // Dummy return.
            return default(ProfilPublicDto);
        }

        public IEnumerable<WithId<Int32, PreferenceDto>> GetPreferences(String codeUniversel)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.ProfilService_GetPreferences_RequiresCodeUniversel);

            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<WithId<Int32, PreferenceDto>>>() != null, ContractStrings.ProfilService_GetPreferences_EnsuresNonNullPreferences);

            // Dummy return.
            return default(IEnumerable<WithId<Int32, PreferenceDto>>);
        }

        public void Update(String codeUniversel, ProfilDto profil)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.ProfilService_Update_RequiresCodeUniversel);
            Contract.Requires(profil != null, ContractStrings.ProfilService_Update_RequiresProfil);
        }
    }
}