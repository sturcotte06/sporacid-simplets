namespace Sporacid.Simplets.Webapp.Services.Installers
{
    using System;
    using System.Reflection;
    using Autofac;
    using Autofac.Builder;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Security.Credentials;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Security.Credentials.Impl;
    using Module = Autofac.Module;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class ServicesModule : Module
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
            // Services registrations.
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(type => type.IsClass && !type.IsAbstract && type.Name.EndsWith("Controller"))
                .AsImplementedInterfaces();

            // Credential extractor registrations.
            const String availableCredentialExtractors = "availableCredentialExtractors";
            builder.RegisterCollection(availableCredentialExtractors, typeof (ICredentialsExtractor));
            builder.RegisterType<KerberosCredentialsExtractor>().As<ICredentialsExtractor>()
                .MemberOf(availableCredentialExtractors);
            builder.RegisterType<TokenCredentialsExtractor>().As<ICredentialsExtractor>()
                .MemberOf(availableCredentialExtractors);
        }
    }
}