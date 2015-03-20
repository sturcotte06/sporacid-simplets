namespace Sporacid.Simplets.Webapp.Services.Modules
{
    using System.Linq;
    using System.Reflection;
    using Autofac;
    using Autofac.Integration.WebApi;
    using Sporacid.Simplets.Webapp.Services.Services;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Exception;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Exception.Impl;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Security.Impl;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Validation.Impl;
    using Sporacid.Simplets.Webapp.Tools.Reflection;
    using Module = Autofac.Module;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class FilterModule : Module
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
            // Authentication filter registrations.
            builder.RegisterType<AuthenticationFilter>().AsSelf();
            builder.Register(c => c.Resolve<AuthenticationFilter>())
                .AsWebApiAuthenticationFilterFor<BaseSecureService>()
                .InstancePerRequest();

            // Custom authentication header registrations.
            builder.RegisterType<TokenHeaderFilter>().AsSelf();
            builder.Register(c => c.Resolve<TokenHeaderFilter>())
                .AsWebApiActionFilterFor<BaseSecureService>()
                .SingleInstance();

            // Authorization filter registrations.
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterType<ClaimsByActionDictionary>().AsSelf()
                .WithParameter(TypedParameter.From(assembly))
                .WithParameter(TypedParameter.From(ReflectionExtensions.GetChildrenNamespaces(assembly, "Sporacid.Simplets.Webapp.Services.Services")))
                .SingleInstance();
            builder.RegisterType<AuthorizationFilter>().AsSelf();
            builder.Register(c => c.Resolve<AuthorizationFilter>())
                .AsWebApiAuthorizationFilterFor<BaseSecureService>()
                .InstancePerRequest();

            // Exception filter registrations.
            builder.RegisterType<ExceptionMap>().As<IExceptionMap>()
                .SingleInstance();
            builder.RegisterType<ExceptionFilter>().AsSelf();
            builder.Register(c => c.Resolve<ExceptionFilter>())
                .AsWebApiExceptionFilterFor<BaseService>()
                .SingleInstance();

            // Validation filter registrations.
            builder.RegisterType<ValidationFilter>().AsSelf();
            builder.Register(c => c.Resolve<ValidationFilter>())
                .AsWebApiActionFilterFor<BaseService>()
                .SingleInstance();
        }
    }
}