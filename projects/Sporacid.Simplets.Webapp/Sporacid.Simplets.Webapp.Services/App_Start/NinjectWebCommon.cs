namespace Sporacid.Simplets.Webapp.Services
{
    using System;
    using System.Configuration;
    using System.Data.Linq;
    using System.Web;
    using System.Web.Http.Filters;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.WebApi.FilterBindingSyntax;
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
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Services;
    using Sporacid.Simplets.Webapp.Services.Services.Impl;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.ExceptionHandling;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Responses;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Security;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Security.Credentials;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Security.Credentials.Impl;
    using Sporacid.Simplets.Webapp.Tools.Collections.Caches;
    using Sporacid.Simplets.Webapp.Tools.Collections.Caches.Policies;
    using Sporacid.Simplets.Webapp.Tools.Collections.Caches.Policies.Invalidation;
    using Sporacid.Simplets.Webapp.Tools.Collections.Caches.Policies.Locking;
    using Sporacid.Simplets.Webapp.Tools.Collections.Pooling;
    using Sporacid.Simplets.Webapp.Tools.Factories;
    using Sporacid.Simplets.Webapp.Tools.Threading.Pooling;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof (OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof (NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            RegisterToolProject(kernel);
            RegisterCoreProject(kernel);
            RegisterServiceProject(kernel);
            RegisterServiceProjectFilters(kernel);
        }

        /// <summary>
        /// Register all bindings for the core project.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterCoreProject(IKernel kernel)
        {
            // Repositories configuration.
            // Bind all (beside exceptions) repositories in request scope, because linq to sql data
            // contexts should live for a single unit of work.
            kernel.Bind(typeof (IRepository<,>)).To(typeof (GenericRepository<,>))
                .WhenAnyAncestorMatches(ctx =>
                    !typeof (ISecurityDatabaseBootstrapper).IsAssignableFrom((ctx.Binding).Service) &&
                    !typeof (IRoleBootstrapper).IsAssignableFrom((ctx.Binding).Service))
                .InRequestScope()
                .OnDeactivation(repository => ((IDisposable) repository).Dispose());
            // Bind repositories used in the bootstrapping of security database in thread scope,
            // because it's not running as a web request.
            kernel.Bind(typeof (IRepository<,>)).To(typeof (GenericRepository<,>))
                .WhenAnyAncestorMatches(ctx =>
                    typeof (ISecurityDatabaseBootstrapper).IsAssignableFrom((ctx.Binding).Service) ||
                    typeof (IRoleBootstrapper).IsAssignableFrom((ctx.Binding).Service))
                .InThreadScope()
                .OnDeactivation(repository => ((IDisposable) repository).Dispose());

            // Data context stores configuration
            kernel.Bind<IDataContextStore>().To<DataContextStore>()
                .When(request => request.ParentRequest.Service.GetGenericArguments()[1].Namespace == "Sporacid.Simplets.Webapp.Core.Security.Database")
                .WithConstructorArgument(typeof (SecurityDataContext));
            kernel.Bind<IDataContextStore>().To<DataContextStore>()
                .When(request => request.ParentRequest.Service.GetGenericArguments()[1].Namespace == "Sporacid.Simplets.Webapp.Services.Database")
                .WithConstructorArgument(typeof (DatabaseDataContext));

            // Security database boostrap configuration.
            kernel.Bind<ISecurityDatabaseBootstrapper>().To<SecurityDatabaseBootstrapper>();
            kernel.Bind<IRoleBootstrapper>().To<RoleBootstrapper>();
            kernel.Bind<IDatabaseCreator>().To<DatabaseCreator>();

            // Cache configurations.
            kernel.Bind(typeof (ICache<,>)).To(typeof (ConfigurableCache<,>))
                .InSingletonScope();

            // Security modules configuration.
            kernel.Bind<IAuthenticationModule>().To<KerberosAuthenticationModule>()
                .InSingletonScope()
                .WithConstructorArgument("ENS.AD.ETSMTL.CA");
            kernel.Bind<IAuthenticationModule>().To<TokenAuthenticationModule>()
                .InSingletonScope()
            .OnActivation(tokenAuthenticationModule => 
                kernel.Get<KerberosAuthenticationModule>().AddObserver(tokenAuthenticationModule));
            kernel.Bind<IAuthorizationModule>().To<AuthorizationModule>()
                .InSingletonScope();
            kernel.Bind<ITokenFactory>().To<AuthenticationTokenFactory>()
                .InSingletonScope()
                .WithConstructorArgument(TimeSpan.FromHours(6))
                .WithConstructorArgument((uint) 64);
        }

        /// <summary>
        /// Register all bindings for the tools project.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterToolProject(IKernel kernel)
        {
            kernel.Bind<IThreadPool>().To<ThreadPool>();
            kernel.Bind(typeof (IFactory<>)).To(typeof (Factory<>));
            kernel.Bind(typeof (IObjectPool<>)).To(typeof (ObjectPool<>));
            kernel.Bind(typeof (ICachePolicy<,>)).To(typeof (ExclusiveLockingPolicy<,>));
            kernel.Bind(typeof (ICachePolicy<,>)).To(typeof (TimeBasedInvalidationPolicy<,>))
                .WithConstructorArgument(TimeSpan.FromHours(6));
        }

        /// <summary>
        /// Register all bindings for the services project.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServiceProject(IKernel kernel)
        {
            // Services configuration.
            kernel.Bind<IAdministrationService>().To<AdministrationService>();
            kernel.Bind<IAnonymousService>().To<AnonymousService>();
            kernel.Bind<IContextAdministrationService>().To<ContextAdministrationService>();
            kernel.Bind<IEnumerationService>().To<EnumerationService>();
            kernel.Bind<IMembreService>().To<MembreService>();
            kernel.Bind<IPrincipalService>().To<PrincipalService>();
            kernel.Bind<ISubscriptionService>().To<SubscriptionService>();

            // Data context
            kernel.Bind<DataContext>().To<SecurityDataContext>()
                .WhenInjectedInto(
                    typeof (IRepository<Int32, Core.Security.Database.RoleTemplate>),
                    typeof (IRepository<Int32, Core.Security.Database.Context>),
                    typeof (IRepository<Int32, Core.Security.Database.Principal>),
                    typeof (IRepository<Int32, Core.Security.Database.Module>))
                .WithConstructorArgument(ConfigurationManager.ConnectionStrings["SIMPLETSConnectionString"].ConnectionString);
            kernel.Bind<DataContext>().To<DatabaseDataContext>()
                .WithConstructorArgument(ConfigurationManager.ConnectionStrings["SIMPLETSConnectionString"].ConnectionString);

            kernel.Bind<ICredentialsExtractor>().To<KerberosCredentialsExtractor>();
            kernel.Bind<ICredentialsExtractor>().To<TokenCredentialsExtractor>();
        }

        /// <summary>
        /// Register all web api filters for the services project.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServiceProjectFilters(IKernel kernel)
        {
            kernel.BindHttpFilter<AuthenticationFilter>(FilterScope.Controller)
                .WhenControllerHas<AuthenticatedAndAuthorizedAttribute>();
            kernel.BindHttpFilter<AuthenticationFilter>(FilterScope.Action)
                .WhenActionMethodHas<AuthenticatedAndAuthorizedAttribute>();

            kernel.BindHttpFilter<AuthorizationFilter>(FilterScope.Controller)
                .WhenControllerHas<AuthenticatedAndAuthorizedAttribute>();
            kernel.BindHttpFilter<AuthorizationFilter>(FilterScope.Action)
                .WhenActionMethodHas<AuthenticatedAndAuthorizedAttribute>();

            kernel.BindHttpFilter<HandleExceptionAsHttpResponseFilter>(FilterScope.Controller)
                .WhenControllerHas<HandlesExceptionAttribute>();
            kernel.BindHttpFilter<HandleExceptionAsHttpResponseFilter>(FilterScope.Action)
                .WhenActionMethodHas<HandlesExceptionAttribute>();

            kernel.BindHttpFilter<StandardHeaderResponseFilter>(FilterScope.Controller);
        }
    }
}
