namespace Sporacid.Simplets.Webapp.Services.Installers
{
    using System;
    using Autofac;
    using Sporacid.Simplets.Webapp.Tools.Collections.Caches;
    using Sporacid.Simplets.Webapp.Tools.Collections.Caches.Policies;
    using Sporacid.Simplets.Webapp.Tools.Collections.Caches.Policies.Invalidation;
    using Sporacid.Simplets.Webapp.Tools.Collections.Caches.Policies.Locking;
    using Sporacid.Simplets.Webapp.Tools.Collections.Pooling;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class ToolsModule : Module
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
            // Specific cache configurations registrations.
            builder.RegisterGeneric(typeof (ConfigurableCache<,>)).As(typeof (ICache<,>))
                .SingleInstance();
            builder.RegisterGeneric(typeof (ReaderWriterLockingPolicy<,>)).As(typeof (ICachePolicy<,>));
            builder.RegisterGeneric(typeof (TimeBasedInvalidationPolicy<,>)).As(typeof (ICachePolicy<,>))
                .WithParameter(TypedParameter.From(TimeSpan.FromHours(6)));


            // const String tokenCachePolicies = "tokenCachePolicies";
            // builder.RegisterCollection(tokenCachePolicies, typeof(ICachePolicy<IToken, ITokenAndPrincipal>));
            // builder.RegisterType<ReaderWriterLockingPolicy<IToken, ITokenAndPrincipal>>().As<ICachePolicy<IToken, ITokenAndPrincipal>>()
            //     .MemberOf(tokenCachePolicies);
            // builder.RegisterType<TimeBasedInvalidationPolicy<IToken, ITokenAndPrincipal>>().As<ICachePolicy<IToken, ITokenAndPrincipal>>()
            //     .WithParameter(TypedParameter.From(TimeSpan.FromHours(6)))
            //     .MemberOf(tokenCachePolicies);
            // builder.RegisterType<ConfigurableCache<IToken, ITokenAndPrincipal>>().As<ICache<IToken, ITokenAndPrincipal>>()
            //     .SingleInstance()
            //     .WithParameter(ResolvedParameter.ForNamed<IEnumerable<ICachePolicy<IToken, ITokenAndPrincipal>>>(tokenCachePolicies));

            // Other registrations.
            builder.RegisterGeneric(typeof (ObjectPool<>)).As(typeof (IObjectPool<>));
        }
    }
}