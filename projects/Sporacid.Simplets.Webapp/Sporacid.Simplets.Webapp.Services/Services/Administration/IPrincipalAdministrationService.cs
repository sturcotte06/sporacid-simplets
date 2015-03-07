namespace Sporacid.Simplets.Webapp.Services.Services.Administration
{
    using System;
    using System.Diagnostics.Contracts;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Resources.Contracts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Administration")]
    [FixedContext(SecurityConfig.SystemContext)]
    [ContractClass(typeof (PrincipalAdministrationServiceContract))]
    public interface IPrincipalAdministrationService
    {
        /// <summary>
        /// Returns whether the principal exists.
        /// </summary>
        /// <param name="identity">The principal's identity.</param>
        /// <returns>Ehether the principal exists.</returns>
        [RequiredClaims(Claims.Admin | Claims.Read)]
        bool PrincipalExists(String identity);

        /// <summary>
        /// Creates a principal in the system.
        /// </summary>
        /// <param name="identity">The principal's identity.</param>
        /// <returns>The created principal id.</returns>
        [RequiredClaims(Claims.Admin | Claims.Create)]
        Int32 CreatePrincipal(String identity);
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IPrincipalAdministrationService))]
    internal abstract class PrincipalAdministrationServiceContract : IPrincipalAdministrationService
    {
        /// <summary>
        /// Returns whether the principal exists.
        /// </summary>
        /// <param name="identity">The principal's identity.</param>
        /// <returns>Ehether the principal exists.</returns>
        public Boolean PrincipalExists(String identity)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(identity), ContractStrings.PrincipalAdministrationService_PrincipalExists_RequiresIdentity);

            // Dummy return.
            return default(Boolean);
        }

        /// <summary>
        /// Creates a principal in the system.
        /// </summary>
        /// <param name="identity">The principal's identity.</param>
        /// <returns>The created principal id.</returns>
        public Int32 CreatePrincipal(String identity)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(identity), ContractStrings.PrincipalAdministrationService_CreatePrincipal_RequiresIdentity);

            // Postconditions.
            Contract.Ensures(Contract.Result<Int32>() > 0, ContractStrings.PrincipalAdministrationService_CreatePrincipal_EnsuresPositivePrincipalId);

            // Dummy return.
            return default(Int32);
        }
    }
}