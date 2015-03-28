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
            Capitaine,
            Noob
        }

        /// <summary>
        /// Fixed context for system administration.
        /// </summary>
        public const String SystemContext = "Systeme";

        private const Claims AllClaims = (Claims) 511;
        private const Claims ReadOnlyClaims = (Claims) 192;
        private const Claims ModifyClaims = (Claims) 207;
        private const Claims FullModifyClaims = (Claims) 255;

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
                .BindClaims(AllClaims)
                .ToModules(allModules)
                .BootstrapTo(Role.Administrateur.ToString());
            roleBootstrapper
                .BindClaims(ReadOnlyClaims)
                .ToModules("Enumerations", "Default")
                .BindClaims(AllClaims)
                .ToModules("Membres")
                .BindClaims(FullModifyClaims)
                .ToModules("Commanditaires")
                .BootstrapTo(Role.Capitaine.ToString());
            roleBootstrapper
                .BindClaims(ReadOnlyClaims)
                .ToModules(allModules)
                .BootstrapTo(Role.Noob.ToString());
        }
    }
}