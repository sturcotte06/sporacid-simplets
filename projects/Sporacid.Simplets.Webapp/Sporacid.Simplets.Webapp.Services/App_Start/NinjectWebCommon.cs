using Sporacid.Simplets.Webapp.Services;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof (NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof (NinjectWebCommon), "Stop")]

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
    using Sporacid.Simplets.Webapp.Core.Security.Database;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Services;
    using Sporacid.Simplets.Webapp.Services.Services.Impl;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.ExceptionHandling;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Responses;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Security;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Security.Credentials;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Security.Credentials.Impl;
    using Sporacid.Simplets.Webapp.Tools.Collections;
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
            kernel.Bind(typeof (IRepository<,>)).To(typeof (GenericRepository<,>))
                .InSingletonScope();
            kernel.Bind(typeof (ICache<,>)).To(typeof (ConfigurableCache<,>))
                .InSingletonScope();
            kernel.Bind<IAuthenticationModule>().To<KerberosAuthenticationModule>()
                .InSingletonScope()
                .WithConstructorArgument("ENS.AD.ETSMTL.CA");
            kernel.Bind<IAuthenticationModule>().To<TokenAuthenticationModule>()
                .InSingletonScope();
                // .OnActivation(tokenAuthenticationModule =>
                // {
                //     var authenticationObservables = kernel.Get<IAuthenticationObservable[]>();
                // 
                //     authenticationObservables.ForEach(authenticationObservable =>
                //     {
                //         if (authenticationObservable != tokenAuthenticationModule)
                //             authenticationObservable.AddObserver(tokenAuthenticationModule);
                //     });
                // });
            kernel.Bind<IAuthorizationModule>().To<AuthorizationModule>()
                .InSingletonScope()
               /*.WithConstructorArgument(
                   new SecurityDataContext(ConfigurationManager.ConnectionStrings["SIMPLETSConnectionString"].ConnectionString))*/;
            kernel.Bind<ITokenFactory>().To<AuthenticationTokenFactory>()
                .InSingletonScope()
                .WithConstructorArgument(TimeSpan.FromHours(6))
                .WithConstructorArgument((uint) 64);

            // kernel.Bind<IAuthenticationObservable>().To<IAuthenticationModule>();
            // kernel.Bind<IAuthenticationObservable>().To<IAuthenticationModule>();
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
            kernel.Bind<IMembreService>().To<MembreService>();
            kernel.Bind<ISubscriptionService>().To<SubscriptionService>();
            kernel.Bind<DataContext>().To<DatabaseDataContext>()
                .WithConstructorArgument(ConfigurationManager.ConnectionStrings["SIMPLETSConnectionString"].ConnectionString);
            //kernel.Bind<DataContext>().To<SecurityDataContext>()
            //    .WhenInjectedExactlyInto<IAuthorizationModule>()
            //    .WithConstructorArgument(ConfigurationManager.ConnectionStrings["SIMPLETSConnectionString"].ConnectionString);
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