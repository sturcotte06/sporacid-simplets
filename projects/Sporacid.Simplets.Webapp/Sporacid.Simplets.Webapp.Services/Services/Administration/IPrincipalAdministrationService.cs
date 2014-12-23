namespace Sporacid.Simplets.Webapp.Services.Services.Administration
{
    using System;
    using PostSharp.Patterns.Contracts;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Administration")]
    [FixedContext("Systeme")]
    public interface IPrincipalAdministrationService
    {
        /// <summary>
        /// Returns whether the principal exists.
        /// </summary>
        /// <param name="identity">The principal's identity.</param>
        /// <returns>Ehether the principal exists.</returns>
        [RequiredClaims(Claims.Admin | Claims.Read)]
        bool PrincipalExists([Required] String identity);

        /// <summary>
        /// Creates a principal in the system.
        /// </summary>
        /// <param name="identity">The principal's identity.</param>
        /// <returns>The created principal id.</returns>
        [RequiredClaims(Claims.Admin | Claims.Create)]
        Int32 CreatePrincipal([Required] String identity);
    }
}