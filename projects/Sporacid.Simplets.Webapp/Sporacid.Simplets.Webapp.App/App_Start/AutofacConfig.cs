namespace Sporacid.Simplets.Webapp.App
{
    using System.Reflection;
    using System.Web.Mvc;
    using Autofac;
    using Autofac.Integration.Mvc;
    using Sporacid.Simplets.Webapp.App.Filters;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public static class AutofacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            // Register all controllers.
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // Register all filters.
            builder.RegisterFilterProvider();
            builder.Register(c => new LocalizationFilterAttribute())
                .AsActionFilterFor<Controller>()
                .InstancePerRequest();
            builder.Register(c => new HandleErrorAttribute())
                .AsExceptionFilterFor<Controller>()
                .InstancePerRequest();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}