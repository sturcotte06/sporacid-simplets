namespace Sporacid.Simplets.Webapp.Services
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Core.Security.Bootstrap;
    using Sporacid.Simplets.Webapp.Tools.Reflection;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class SecurityConfig
    {
        /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
        /// <version>1.9.0</version>
        public enum Role
        {
            Administrateur,
            Collaborateur,
            Lecteur
        }

        /// <summary>
        /// Fixed context for system administration.
        /// </summary>
        public const String SystemContext = "Systeme";

        public static void Register(HttpConfiguration config)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // Query all modules of application.
            var allModules = assembly.GetTypes().Where(type => type.GetCustomAttribute<ModuleAttribute>() != null)
                .Select(type => type.GetCustomAttribute<ModuleAttribute>().Name).ToArray();

            // Get bootstrappers of security database.
            var securityDatabaseBootstrapper = (ISecurityDatabaseBootstrapper) config.DependencyResolver.GetService(typeof (ISecurityDatabaseBootstrapper));
            var roleBootstrapper = (IRoleBootstrapper) config.DependencyResolver.GetService(typeof (IRoleBootstrapper));

            // Bootstrap the security database.
            securityDatabaseBootstrapper.Bootstrap(assembly,
                ReflectionExtensions.GetChildrenNamespaces(assembly, "Sporacid.Simplets.Webapp.Services.Services").ToArray());

            // Bootstrap the user roles of the application.
            roleBootstrapper
                .BindClaims(Claims.All)
                .ToModules(allModules)
                .BootstrapTo(Role.Administrateur.ToString());
            roleBootstrapper
                .BindClaims(Claims.ReadWriteOnly)
                .ToModules(allModules)
                .BootstrapTo(Role.Collaborateur.ToString());
            roleBootstrapper
                .BindClaims(Claims.ReadOnly)
                .ToModules(allModules)
                .BootstrapTo(Role.Lecteur.ToString());
        }
    }
}