namespace Sporacid.Simplets.Webapp.Services
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Core.Security.Bootstrap;
    using Sporacid.Simplets.Webapp.Services.Services;
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

        private const Claims ReadOnly = Claims.Read | Claims.ReadAll;
        private const Claims ReadWriteOnly = ReadOnly | Claims.Create | Claims.CreateAll | Claims.Update | Claims.UpdateAll | Claims.Delete | Claims.DeleteAll;
        private const Claims All = ReadWriteOnly | Claims.Admin;

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
                ReflectionExtensions.GetChildrenNamespaces(assembly, typeof (IService).Namespace).ToArray());

            // Bootstrap the user roles of the application.
            roleBootstrapper
                .BindClaims(All)
                .ToModules(allModules)
                .BootstrapTo(Role.Administrateur.ToString());
            roleBootstrapper
                .BindClaims(ReadWriteOnly)
                .ToModules(allModules)
                .BootstrapTo(Role.Collaborateur.ToString());
            roleBootstrapper
                .BindClaims(ReadOnly)
                .ToModules(allModules)
                .BootstrapTo(Role.Lecteur.ToString());
        }
    }
}