namespace Sporacid.Simplets.Webapp.Services.Services.Administration
{
    using System;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Administration")]
    [FixedContext("Systeme")]
    public interface IPrincipalAdministrationService
    {
        /// <summary>
        /// Creates a principal in the system.
        /// </summary>
        /// <param name="identity">The principal's identity.</param>
        [RequiredClaims(Claims.Admin | Claims.Create)]
        Int32 CreatePrincipal(String identity);
    }
}