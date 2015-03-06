namespace Sporacid.Simplets.Webapp.Core.Security.Bootstrap.Impl
{
    using System;
    using System.Data.Linq.SqlClient;
    using System.Linq;
    using System.Reflection;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Core.Security.Database;
    using Module = Sporacid.Simplets.Webapp.Core.Security.Database.Module;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class SecurityDatabaseBootstrapper : ISecurityDatabaseBootstrapper
    {
        private readonly IRepository<Int32, Claim> claimsRepository;
        private readonly IRepository<Int32, Context> contextRepository;
        private readonly IRepository<Int32, Module> moduleRepository;

        public SecurityDatabaseBootstrapper(IRepository<Int32, Module> moduleRepository, IRepository<Int32, Claim> claimsRepository,
            IRepository<Int32, Context> contextRepository)
        {
            this.moduleRepository = moduleRepository;
            this.claimsRepository = claimsRepository;
            this.contextRepository = contextRepository;
        }

        /// <summary>
        /// Bootstraps the security database.
        /// This operation is idempotent, which means multiple calls to this method will give the same result.
        /// However if a new endpoint is configured, the delta between previous bootstrap and current bootstrap will
        /// be committed to the security repository.
        /// </summary>
        /// <param name="assembly">Assembly to scan for endpoints.</param>
        /// <param name="endpointsNamespaces">Namespaces to scan for endpoints.</param>
        public void Bootstrap(Assembly assembly, params string[] endpointsNamespaces)
        {
            var endpointTypes = from type in assembly.GetTypes()
                where (type.IsClass || type.IsInterface) && endpointsNamespaces.Contains(type.Namespace)
                select type;

            this.Bootstrap(endpointTypes.ToArray());
        }

        /// <summary>
        /// Bootstraps the security database.
        /// This operation is idempotent, which means multiple calls to this method will give the same result.
        /// However if a new endpoint is configured, the delta between previous bootstrap and current bootstrap will
        /// be committed to the security repository.
        /// </summary>
        private void Bootstrap(params Type[] configuredEndpoints)
        {
            // Add a module and the fixed context, if applicable, for each configured endpoints.
            foreach (var configuredEndpoint in configuredEndpoints)
            {
                var moduleAttr = (ModuleAttribute) configuredEndpoint.GetCustomAttributes(typeof (ModuleAttribute), true).FirstOrDefault();
                var fixedContextAttr = (FixedContextAttribute) configuredEndpoint.GetCustomAttributes(typeof (FixedContextAttribute), true).FirstOrDefault();

                if (moduleAttr != null && !this.moduleRepository.Has(m => SqlMethods.Like(m.Name, moduleAttr.Name)))
                {
                    // Add the module.
                    this.moduleRepository.Add(new Module {Name = moduleAttr.Name});
                }

                if (fixedContextAttr != null && !this.contextRepository.Has(m => SqlMethods.Like(m.Name, fixedContextAttr.Name)))
                {
                    // Add the context.
                    this.contextRepository.Add(new Context {Name = fixedContextAttr.Name});
                }
            }

            // Add a claim for each values of the enum.
            foreach (var claim in Enum.GetValues(typeof (Claims)).Cast<Claims>())
            {
                var claimName = claim.ToString();
                var claimValue = (int) claim;
                if (!this.claimsRepository.Has(c => c.Value == claimValue))
                {
                    this.claimsRepository.Add(new Claim {Name = claimName, Value = claimValue});
                }
            }
        }
    }
}