using Sporacid.Simplets.Webapp.Services;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof (NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof (NinjectWebCommon), "Stop")]

namespace Sporacid.Simplets.Webapp.Services
{
    using System;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Http.Filters;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.WebApi.FilterBindingSyntax;
    using Sporacid.Simplets.Webapp.Core.Security.Authentication;
    using Sporacid.Simplets.Webapp.Core.Security.Authentication.Impl;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization.Impl;
    using Sporacid.Simplets.Webapp.Core.Security.Token;
    using Sporacid.Simplets.Webapp.Core.Security.Token.Factories;
    using Sporacid.Simplets.Webapp.Core.Security.Token.Factories.Impl;
    using Sporacid.Simplets.Webapp.Services.Services;
    using Sporacid.Simplets.Webapp.Services.Services.Impl;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Authentication;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Authorization;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.ExceptionHandling;
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
            // Tools project bindings.
            kernel.Bind<IThreadPool>().To<ThreadPool>();
            kernel.Bind(typeof (IFactory<>)).To(typeof (Factory<>));
            kernel.Bind(typeof (IObjectPool<>)).To(typeof (ObjectPool<>));
            kernel.Bind(typeof (ICacheLockingPolicy<,>)).To(typeof (ExclusiveLockingPolicy<,>));
            kernel.Bind(typeof (ICacheInvalidationPolicy<,>)).To(typeof (TimeBasedInvalidationPolicy<,>))
                .WithConstructorArgument(TimeSpan.FromHours(6));
            kernel.Bind(typeof (ICache<IToken, IPrincipal>)).To(typeof (ConfigurableCache<IToken, IPrincipal>))
                .WithConstructorArgument(new ICachePolicy<IToken, IPrincipal>[]
                {
                    kernel.Get<ICacheLockingPolicy<IToken, IPrincipal>>(),
                    kernel.Get<ICacheInvalidationPolicy<IToken, IPrincipal>>(),
                });

            // Core project bindings.
            kernel.Bind<IAuthenticationModule>().To<KerberosAuthenticationModule>()
                .WithConstructorArgument("ENS.AD.ETSMTL.CA");
            kernel.Bind<IAuthorizationModule>().To<AuthorizationModule>();
            kernel.Bind<ITokenFactory>().To<AuthenticationTokenFactory>()
                .WithConstructorArgument(TimeSpan.FromHours(6))
                .WithConstructorArgument((uint) 64);
            kernel.Bind<IAuthenticationModule[]>().ToMethod<IAuthenticationModule[]>(ctx => new IAuthenticationModule[]
            {
                kernel.Get<KerberosAuthenticationModule>(),
                kernel.Get<TokenAuthenticationModule>()
            });

            // Services project bindings.
            kernel.Bind<IMembreService>().To<MembreService>();

            // Filter bindings.
            kernel.BindHttpFilter<AuthenticationFilter>(FilterScope.Controller)
                .WhenControllerHas<AuthenticatedAttribute>();
            kernel.BindHttpFilter<AuthenticationFilter>(FilterScope.Action)
                .WhenActionMethodHas<AuthenticatedAttribute>();

            kernel.BindHttpFilter<ContextualAuthorizationFilter>(FilterScope.Controller)
                .WhenControllerHas<AuthorizedAttribute>();
            kernel.BindHttpFilter<ContextualAuthorizationFilter>(FilterScope.Action)
                .WhenActionMethodHas<AuthorizedAttribute>();

            kernel.BindHttpFilter<HandleExceptionAsHttpResponseFilter>(FilterScope.Controller)
                .WhenControllerHas<HandlesExceptionAttribute>();
            kernel.BindHttpFilter<HandleExceptionAsHttpResponseFilter>(FilterScope.Action)
                .WhenActionMethodHas<HandlesExceptionAttribute>();
        }
    }
}