namespace Sporacid.Simplets.Webapp.Services
{
    using System;
    using System.Reflection;
    using Ninject;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Core.Security.Bootstrap;

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

        private static readonly String[] AllModules =
        {
            "ContextAdministration",
            "ProfilAdministration",
            "ClubAdministration",
            "PrincipalAdministration",
            "Club",
            "Commandites",
            "Commanditaires",
            "Fournisseurs",
            "Inventaire",
            "Default",
            "Enumerations",
            "Profils",
            "Inscriptions"
        };

        private static void BootstrapSecurityContext()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var kernel = NinjectWebCommon.Bootstrapper.Kernel;

            // Bootstrap the security database.
            kernel.Get<ISecurityDatabaseBootstrapper>()
                .Bootstrap(assembly,
                    "Sporacid.Simplets.Webapp.Services.Services.Security",
                    "Sporacid.Simplets.Webapp.Services.Services.Security.Administration",
                    "Sporacid.Simplets.Webapp.Services.Services.Clubs",
                    "Sporacid.Simplets.Webapp.Services.Services.Clubs.Administration",
                    "Sporacid.Simplets.Webapp.Services.Services.Public",
                    "Sporacid.Simplets.Webapp.Services.Services.Public.Administration",
                    "Sporacid.Simplets.Webapp.Services.Services.Userspace",
                    "Sporacid.Simplets.Webapp.Services.Services.Userspace.Administration");

            // Bootstrap the user roles of the application.
            var roleBootstrapper = kernel.Get<IRoleBootstrapper>();

            roleBootstrapper
                .BindClaims(AllClaims)
                .ToModules(AllModules)
                .BootstrapTo(Role.Administrateur.ToString());
            roleBootstrapper
                .BindClaims(ReadOnlyClaims)
                .ToModules("Enumerations", "Default")
                .BindClaims(AllClaims)
                .ToModules("Inscriptions")
                .BindClaims(FullModifyClaims)
                .ToModules("Administration")
                .BootstrapTo(Role.Capitaine.ToString());
            roleBootstrapper
                .BindClaims(ReadOnlyClaims)
                .ToModules("Enumerations", "Default")
                .BootstrapTo(Role.Noob.ToString());
        }
    }
}