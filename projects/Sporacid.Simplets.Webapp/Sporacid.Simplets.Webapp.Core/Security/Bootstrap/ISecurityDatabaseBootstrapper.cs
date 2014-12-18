namespace Sporacid.Simplets.Webapp.Core.Security.Bootstrap
{
    using System.Reflection;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
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
}