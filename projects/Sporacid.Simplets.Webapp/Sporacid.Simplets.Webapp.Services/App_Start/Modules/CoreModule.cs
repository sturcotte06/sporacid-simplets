namespace Sporacid.Simplets.Webapp.Services.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Linq;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using Autofac;
    using Autofac.Builder;
    using Autofac.Core;
    using Sporacid.Simplets.Webapp.Core.Events;
    using Sporacid.Simplets.Webapp.Core.Events.Impl;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Core.Repositories.Impl;
    using Sporacid.Simplets.Webapp.Core.Security.Authentication;
    using Sporacid.Simplets.Webapp.Core.Security.Authentication.Impl;
    using Sporacid.Simplets.Webapp.Core.Security.Authentication.Tokens.Factories;
    using Sporacid.Simplets.Webapp.Core.Security.Authentication.Tokens.Factories.Impl;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization.Impl;
    using Sporacid.Simplets.Webapp.Core.Security.Bootstrap;
    using Sporacid.Simplets.Webapp.Core.Security.Bootstrap.Impl;
    using Sporacid.Simplets.Webapp.Core.Security.Database;
    using Sporacid.Simplets.Webapp.Core.Security.Events.Subscribers;
    using Sporacid.Simplets.Webapp.Core.Security.Ldap;
    using Sporacid.Simplets.Webapp.Core.Security.Ldap.Impl;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Services;
    using Module = Autofac.Module;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class CoreModule : Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        /// <param name="builder">
        /// The builder through which components can be
        /// registered.
        /// </param>
        protected override void Load(ContainerBuilder builder)
        {
            // Linq2sql data context registrations.
            var connectionString = ConfigurationManager.ConnectionStrings["SIMPLETSConnectionString"].ConnectionString;
            builder.RegisterType<DatabaseDataContext>().AsSelf()
                .WithParameter(TypedParameter.From(connectionString));
            builder.RegisterType<SecurityDataContext>().AsSelf()
                .WithParameter(TypedParameter.From(connectionString));

            // Repositories registrations.
            builder.RegisterGeneric(typeof (GenericRepository<,>)).As(typeof (IRepository<,>))
                .OnPreparing(args =>
                {
                    var firstService = args.Component.Services.FirstOrDefault() as TypedService;
                    if (firstService != null)
                    {
                        // Repositories second generic is the entity type.
                        var entityType = firstService.ServiceType.GetGenericArguments()[1];
                        var entityNamespace = (entityType ?? typeof (object)).Namespace ?? "";
                        var newParams = new List<Parameter>(args.Parameters);
                        if (entityNamespace.StartsWith("Sporacid.Simplets.Webapp.Services"))
                        {
                            newParams.Add(new TypedParameter(typeof (DataContext), args.Context.Resolve<DatabaseDataContext>()));
                        }
                        else if (entityNamespace.Contains("Sporacid.Simplets.Webapp.Core"))
                        {
                            newParams.Add(new TypedParameter(typeof (DataContext), args.Context.Resolve<SecurityDataContext>()));
                        }

                        args.Parameters = newParams;
                    }
                });

            // Security database boostrap registrations.
            builder.RegisterType<SecurityDatabaseBootstrapper>().As<ISecurityDatabaseBootstrapper>();
            builder.RegisterType<RoleBootstrapper>().As<IRoleBootstrapper>();

            // Security registrations.
            builder.RegisterType<AuthenticationTokenFactory>().As<ITokenFactory>()
                .WithParameter(TypedParameter.From(TimeSpan.FromHours(6)))
                .WithParameter(TypedParameter.From((uint) 64));
            builder.RegisterType<ActiveDirectorySearcher>().As<ILdapSearcher>()
                .WithParameter(TypedParameter.From(ConfigurationManager.AppSettings["ActiveDirectoryDomainName"]));
            builder.RegisterType<AuthorizationModule>().As<IAuthorizationModule>();

            // Specific authentication registrations. 
            // It's more complicated because of observables.
            builder.RegisterType<KerberosAuthenticationModule>().As<IAuthenticationModule>()
                .WithParameter(TypedParameter.From(ConfigurationManager.AppSettings["ActiveDirectoryDomainName"]));
            builder.RegisterType<TokenAuthenticationModule>().As<IAuthenticationModule>();


            // Register event bus registrations.
            builder.RegisterGeneric(typeof (SyncTransientEventBus<,>)).As(typeof (IEventBus<,>));

            // Event subscribers registrations.
            builder.RegisterAssemblyTypes(typeof(IService).Assembly, typeof(IAuthenticationModule).Assembly)
                .Where(type => type.GetInterfaces().Any(@interface => @interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof (IEventSubscriber<,>)))
                .AsImplementedInterfaces();
        }
    }
}