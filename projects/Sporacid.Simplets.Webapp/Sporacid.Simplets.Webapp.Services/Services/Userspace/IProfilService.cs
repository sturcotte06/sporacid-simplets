namespace Sporacid.Simplets.Webapp.Services.Services.Userspace
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Userspace;
    using Sporacid.Simplets.Webapp.Services.Resources.Contracts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Profils")]
    [Contextual("codeUniversel")]
    [ContractClass(typeof (ProfilServiceContract))]
    public interface IProfilService
    {
        /// <summary>
        /// Gets the profil entity from the system.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <returns>The profil.</returns>
        [RequiredClaims(Claims.Admin | Claims.Read)]
        ProfilDto Get(String codeUniversel);

        /// <summary>
        /// Updates the profil entity in the system.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="profil">The profil.</param>
        [RequiredClaims(Claims.Admin | Claims.Update)]
        void Update(String codeUniversel, ProfilDto profil);

        /// <summary>
        /// Gets all club entities from the system.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <returns>All club entities subscribed to.</returns>
        [RequiredClaims(Claims.Admin | Claims.Read)]
        IEnumerable<WithId<Int32, ClubDto>> GetClubsSubscribedTo(String codeUniversel);
    }


    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IProfilService))]
    internal abstract class ProfilServiceContract : IProfilService
    {
        /// <summary>
        /// Gets the profil entity from the system.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <returns>The profil.</returns>
        public ProfilDto Get(String codeUniversel)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.ProfilService_Get_RequiresCodeUniversel);

            // Postconditions.
            Contract.Ensures(Contract.Result<ProfilDto>() != null, ContractStrings.ProfilService_Get_EnsuresNonNullProfil);

            // Dummy return.
            return default(ProfilDto);
        }

        /// <summary>
        /// Updates the profil entity in the system.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="profil">The profil.</param>
        public void Update(String codeUniversel, ProfilDto profil)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.ProfilService_Update_RequiresCodeUniversel);
            Contract.Requires(profil != null, ContractStrings.ProfilService_Update_RequiresProfil);
        }

        /// <summary>
        /// Gets all club entities from the system.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <returns>All club entities subscribed to.</returns>
        public IEnumerable<WithId<Int32, ClubDto>> GetClubsSubscribedTo(String codeUniversel)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.ProfilService_GetClubsSubscribedTo_RequiresCodeUniversel);

            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<WithId<Int32, ClubDto>>>() != null, ContractStrings.CommanditeService_GetClubsSubscribedTo_EnsuresNonNullClubs);

            // Dummy return.
            return default(IEnumerable<WithId<Int32, ClubDto>>);
        }
    }
}