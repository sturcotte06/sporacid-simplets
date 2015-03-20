namespace Sporacid.Simplets.Webapp.Services
{
    // /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    // /// <version>1.9.0</version>
    // public static class NinjectWebCommon
    // {
    //     public static readonly Bootstrapper Bootstrapper = new Bootstrapper();
    // 
    //     /// <summary>
    //     /// Starts the application
    //     /// </summary>
    //     public static void Start()
    //     {
    //         DynamicModuleUtility.RegisterModule(typeof (OnePerRequestHttpModule));
    //         DynamicModuleUtility.RegisterModule(typeof (NinjectHttpModule));
    //         Bootstrapper.Initialize(CreateKernel);
    //     }
    // 
    //     /// <summary>
    //     /// Stops the application.
    //     /// </summary>
    //     public static void Stop()
    //     {
    //         Bootstrapper.ShutDown();
    //     }
    // 
    //     /// <summary>
    //     /// Creates the kernel that will manage your application.
    //     /// </summary>
    //     /// <returns>The created kernel.</returns>
    //     private static IKernel CreateKernel()
    //     {
    //         var kernel = new StandardKernel();
    //         try
    //         {
    //             kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
    //             kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
    // 
    //             RegisterServices(kernel);
    //             return kernel;
    //         }
    //         catch
    //         {
    //             kernel.Dispose();
    //             throw;
    //         }
    //     }
    // 
    //     /// <summary>
    //     /// Load your modules or register your services here!
    //     /// </summary>
    //     /// <param name="kernel">The kernel.</param>
    //     private static void RegisterServices(IKernel kernel)
    //     {
    //         RegisterDataContext(kernel);
    //         RegisterToolProject(kernel);
    //         RegisterCoreProject(kernel);
    //         RegisterServiceProject(kernel);
    //         RegisterServiceProjectFilters(kernel);
    //     }
    // 
    //     /// <summary>
    //     /// Register all bindings for the core project.
    //     /// </summary>
    //     /// <param name="kernel">The kernel.</param>
    //     private static void RegisterCoreProject(IKernel kernel)
    //     {
    //         // Repositories configuration.
    //         // Bind all (beside exceptions) repositories in request scope, because linq to sql data
    //         // contexts should live for a single unit of work.
    //         kernel.Bind(typeof (IRepository<,>)).To(typeof (GenericRepository<,>))
    //             .InRequestScope()
    //             .OnDeactivation(ctx => ((IDisposable) ctx).Dispose());
    // 
    //         // Security database boostrap configuration.
    //         kernel.Bind<ISecurityDatabaseBootstrapper>().To<SecurityDatabaseBootstrapper>();
    //         kernel.Bind<IRoleBootstrapper>().To<RoleBootstrapper>();
    // 
    //         // Token factory configuration.
    //         kernel.Bind<ITokenFactory>().To<AuthenticationTokenFactory>()
    //             .WithConstructorArgument(TimeSpan.FromHours(6))
    //             .WithConstructorArgument((uint) 64);
    // 
    //         // Ldap configuration.
    //         kernel.Bind<ILdapSearcher>().To<ActiveDirectorySearcher>()
    //             .WithConstructorArgument(ConfigurationManager.AppSettings["ActiveDirectoryDomainName"]);
    // 
    //         // Security modules configuration.
    //         kernel.Bind(typeof (IAuthenticationModule), typeof (KerberosAuthenticationModule)).To<KerberosAuthenticationModule>()
    //             .InRequestScope()
    //             .WithConstructorArgument(ConfigurationManager.AppSettings["ActiveDirectoryDomainName"]);
    //         kernel.Bind(typeof (IAuthenticationModule), typeof (TokenAuthenticationModule)).To<TokenAuthenticationModule>()
    //             .InRequestScope()
    //             .WithConstructorArgument(typeof (IEnumerable<IAuthenticationObservable>), new[] {kernel.Get<KerberosAuthenticationModule>()});
    //         kernel.Bind<IAuthorizationModule>().To<AuthorizationModule>()
    //             .InRequestScope();
    // 
    //         // // Event configuration.
    //         // kernel.Bind(typeof (IBlockingQueue<>)).To(typeof (BlockingQueue<>));
    //         // kernel.Bind(x => x
    //         //     .FromThisAssembly()
    //         //     .SelectAllClasses().InheritedFrom(typeof (IEventSubscriber<,>))
    //         //     .BindAllInterfaces()
    //         //     .Configure(s => s.InSingletonScope()));
    //         // kernel.Bind(typeof (IEventBus<,>)).To(typeof (EventBus<,>))
    //         //     .InSingletonScope()
    //         //     .WithConstructorArgument(typeof (IThreadPool), new ThreadPool(new ThreadPoolConfiguration
    //         //     {
    //         //        AutomaticStart = true,
    //         //        ThreadCount = 1,
    //         //        ThreadNamePrefix = "Event Bus"
    //         //     }));
    // 
    //     }
    // 
    //     /// <summary>
    //     /// Register all linq to sql data contexts.
    //     /// </summary>
    //     /// <param name="kernel">The kernel.</param>
    //     private static void RegisterDataContext(IKernel kernel)
    //     {
    //         kernel.Bind<LinqToSqlLog4netAdapter>().ToSelf()
    //             .InSingletonScope()
    //             .WithConstructorArgument(Level.Info)
    //             .WithConstructorArgument(LogManager.GetLogger("Queries.QueriesLogger"));
    // 
    //         // Security data context configuration.
    //         kernel.Bind<DataContext>().To<SecurityDataContext>()
    //             .When(request => request.ParentRequest.Service.GetGenericArguments()[1].Namespace == "Sporacid.Simplets.Webapp.Core.Security.Database")
    //             .WithConstructorArgument(ConfigurationManager.ConnectionStrings["SIMPLETSConnectionString"].ConnectionString)
    //             .OnActivation(dc => dc.Log = kernel.Get<LinqToSqlLog4netAdapter>());
    // 
    //         // Database data context configuration.
    //         kernel.Bind<DataContext>().To<DatabaseDataContext>()
    //             .When(request => request.ParentRequest.Service.GetGenericArguments()[1].Namespace == "Sporacid.Simplets.Webapp.Services.Database")
    //             .WithConstructorArgument(ConfigurationManager.ConnectionStrings["SIMPLETSConnectionString"].ConnectionString)
    //             .OnActivation(dc => dc.Log = kernel.Get<LinqToSqlLog4netAdapter>());
    //     }
    // 
    //     /// <summary>
    //     /// Register all bindings for the tools project.
    //     /// </summary>
    //     /// <param name="kernel">The kernel.</param>
    //     private static void RegisterToolProject(IKernel kernel)
    //     {
    //         // Cache configurations.
    //         kernel.Bind(typeof (IDictionary<,>)).To(typeof (Dictionary<,>));
    //         kernel.Bind(typeof (ICache<,>)).To(typeof (ConfigurableCache<,>)).InSingletonScope();
    //         kernel.Bind(typeof (ICachePolicy<,>)).To(typeof (ReaderWriterLockingPolicy<,>));
    //         kernel.Bind(typeof (ICachePolicy<,>)).To(typeof (TimeBasedInvalidationPolicy<,>))
    //             .WithConstructorArgument(TimeSpan.FromHours(6));
    // 
    //         kernel.Bind(typeof (IObjectPool<>)).To(typeof (ObjectPool<>));
    //     }
    // 
    //     /// <summary>
    //     /// Register all bindings for the services project.
    //     /// </summary>
    //     /// <param name="kernel">The kernel.</param>
    //     private static void RegisterServiceProject(IKernel kernel)
    //     {
    //         // Services configuration.
    //         kernel.Bind(x => x
    //             .FromThisAssembly()
    //             .SelectAllClasses().InheritedFrom<IService>()
    //             .BindAllInterfaces()
    //             .Configure(service => service.InRequestScope()));
    // 
    //         // Credential extractor configuration.
    //         kernel.Bind(typeof (ICredentialsExtractor), typeof (KerberosCredentialsExtractor)).To<KerberosCredentialsExtractor>();
    //         kernel.Bind(typeof (ICredentialsExtractor), typeof (TokenCredentialsExtractor)).To<TokenCredentialsExtractor>();
    //     }
    // 
    //     /// <summary>
    //     /// Register all web api filters for the services project.
    //     /// </summary>
    //     /// <param name="kernel">The kernel.</param>
    //     private static void RegisterServiceProjectFilters(IKernel kernel)
    //     {
    //         // Bind the authentication filter on services that have the RequiresAuthenticatedPrincipal attribute
    //         kernel.BindHttpFilter<AuthenticationFilter>(FilterScope.Controller)
    //             .WhenControllerHas<RequiresAuthenticatedPrincipalAttribute>();
    // 
    //         // Bind the authorization filter on services that have the RequiresAuthorizedPrincipal attribute
    //         kernel.Bind<ClaimsByActionDictionary>().ToSelf().InSingletonScope()
    //             .WithConstructorArgument(Assembly.GetExecutingAssembly())
    //             .WithConstructorArgument(ReflectionExtensions.GetChildrenNamespaces(Assembly.GetExecutingAssembly(), "Sporacid.Simplets.Webapp.Services.Services").ToArray());
    //         kernel.BindHttpFilter<AuthorizationFilter>(FilterScope.Controller)
    //             .WhenControllerHas<RequiresAuthorizedPrincipalAttribute>();
    // 
    //         // Bind the exception handling filter on services that have the HandlesException attribute
    //         kernel.BindHttpFilter<ExceptionHandlingFilter>(FilterScope.Controller)
    //             .WhenControllerHas<HandlesExceptionAttribute>();
    // 
    //         // Bind the model validation filter.
    //         kernel.BindHttpFilter<ValidationFilter>(FilterScope.Global);
    //     }
    // }
}