namespace Sporacid.Simplets.Webapp.Services.Services.Userspace
{
    using System;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Userspace;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Profils")]
    [Contextual("codeUniversel")]
    public interface IProfilService
    {
        /// <summary>
        /// Gets the profil object from the system.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <returns>The profil.</returns>
        [RequiredClaims(Claims.Admin | Claims.Read)]
        ProfilDto GetProfil(String codeUniversel);

        /// <summary>
        /// Updates the profil object in the system.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="profil">The profil.</param>
        [RequiredClaims(Claims.Admin | Claims.Update)]
        void UpdateProfil(String codeUniversel, ProfilDto profil);
    }
}