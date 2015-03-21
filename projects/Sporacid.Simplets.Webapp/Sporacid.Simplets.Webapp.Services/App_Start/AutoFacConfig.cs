namespace Sporacid.Simplets.Webapp.Services
{
    using System.Net.Http;
    using System.Reflection;
    using System.Web;
    using System.Web.Http;
    using Autofac;
    using Autofac.Integration.WebApi;
    using Sporacid.Simplets.Webapp.Services.Installers;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class AutoFacConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            
            // Register all modules.
            builder.RegisterModule<ToolsModule>();
            builder.RegisterModule<CoreModule>();
            builder.RegisterModule<ServicesModule>();
            builder.RegisterModule<FilterModule>();
            
            // Let the dependency injector filters.
            builder.RegisterWebApiFilterProvider(config);

            // Let the dependency injector manage controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Build the container and set it in the application config.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}