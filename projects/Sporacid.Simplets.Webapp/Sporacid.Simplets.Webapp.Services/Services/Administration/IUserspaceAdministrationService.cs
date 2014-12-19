namespace Sporacid.Simplets.Webapp.Services.Services.Administration
{
    using System;
    using PostSharp.Patterns.Contracts;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Administration")]
    [FixedContext("Userspaces")]
    public interface IUserspaceAdministrationService
    {
        /// <summary>
        /// Creates the base profil for agiven universal code.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <returns>The id of the newly created profil entity.</returns>
        [RequiredClaims(Claims.Admin | Claims.Create)]
        Int32 CreateBaseProfil([Required] String codeUniversel);

        // /// <summary>
        // /// Adds a profil entity into the system.
        // /// This should never be called and only exists for test purposes.
        // /// </summary>
        // /// <param name="profil">The profil entity.</param>
        // /// <returns>The id of the newly created profil entity.</returns>
        // [RequiredClaims(Claims.Admin | Claims.Create)]
        // Int32 AddProfil([Required] ProfilDto profil);
        // 
        // /// <summary>
        // /// Updates a profil entity from the system.
        // /// </summary>
        // /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        // /// <param name="profil">The profil entity.</param>
        // [RequiredClaims(Claims.Admin | Claims.Update)]
        // void UpdateProfil([Required] String codeUniversel, [Required] ProfilDto profil);
        // 
        // /// <summary>
        // /// Gets a profil entity from the system.
        // /// </summary>
        // /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        // /// <returns>The profil entity.</returns>
        // [RequiredClaims(Claims.Read)]
        // ProfilDto GeProfil([Required] String codeUniversel);
    }
}