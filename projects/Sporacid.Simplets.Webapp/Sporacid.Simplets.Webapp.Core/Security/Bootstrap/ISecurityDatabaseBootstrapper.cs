namespace Sporacid.Simplets.Webapp.Core.Security.Bootstrap
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Reflection;
    using Sporacid.Simplets.Webapp.Core.Resources.Contracts;
    using Sporacid.Simplets.Webapp.Tools.Strings;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClass(typeof(SecurityDatabaseBootstrapperContract))]
    public interface ISecurityDatabaseBootstrapper
    {
        /// <summary>
        /// Bootstraps the security database.
        /// This operation is idempotent, which means multiple calls to this method will give the same result.
        /// However if a new endpoint is configured, the delta between previous bootstrap and current bootstrap will
        /// be committed to the security repository.
        /// </summary>
        /// <param name="assembly">Assembly to scan for endpoints.</param>
        /// <param name="endpointsNamespaces">Namespaces to scan for endpoints.</param>
        void Bootstrap(Assembly assembly, params string[] endpointsNamespaces);
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof(ISecurityDatabaseBootstrapper))]
    internal abstract class SecurityDatabaseBootstrapperContract : ISecurityDatabaseBootstrapper
    {
        /// <summary>
        /// Bootstraps the security database.
        /// This operation is idempotent, which means multiple calls to this method will give the same result.
        /// However if a new endpoint is configured, the delta between previous bootstrap and current bootstrap will
        /// be committed to the security repository.
        /// </summary>
        /// <param name="assembly">Assembly to scan for endpoints.</param>
        /// <param name="endpointsNamespaces">Namespaces to scan for endpoints.</param>
        public void Bootstrap(Assembly assembly, params String[] endpointsNamespaces)
        {
            // Preconditions.
            Contract.Requires(assembly != null, ContractStrings.SecurityDatabaseBootstrapper_Bootstrap_RequiresAssembly);
            Contract.Requires(endpointsNamespaces != null && endpointsNamespaces.Length > 0 &&
                Contract.ForAll(endpointsNamespaces, endpointsNamespace => !String.IsNullOrEmpty(endpointsNamespace)),
                ContractStrings.SecurityDatabaseBootstrapper_Bootstrap_RequiresEndpointNamespaces);
        }
    }
}