using IKernel = Ninject.IKernel;
using NinjectModule = Ninject.Modules.NinjectModule;
using StandardKernel = Ninject.StandardKernel;

namespace Sporacid.Simplets.Webapp.Services.Tests.IntegrationTests.Ninject
{
    using System;
    using System.Configuration;
    using System.Data.Linq;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Core.Repositories.Impl;
    using Sporacid.Simplets.Webapp.Core.Security.Database;
    using Sporacid.Simplets.Webapp.Services.Database;

    internal class IntegrationTestModule : NinjectModule
    {
        public static IKernel GetKernel()
        {
            return new StandardKernel(new IntegrationTestModule());
        }

        public override void Load()
        {
            this.Bind(typeof(IRepository<,>)).To(typeof(GenericRepository<,>))
                .InTransientScope()
                .OnDeactivation(ctx => ((IDisposable) ctx).Dispose());

            // Data context configuration.
            this.Bind<DataContext>().To<SecurityDataContext>()
                .When(request => request.ParentRequest.Service.GetGenericArguments()[1].Namespace == "Sporacid.Simplets.Webapp.Core.Security.Database")
                .WithConstructorArgument(ConfigurationManager.ConnectionStrings["SIMPLETS_TESTSConnectionString"].ConnectionString);
            this.Bind<DataContext>().To<DatabaseDataContext>()
                .When(request => request.ParentRequest.Service.GetGenericArguments()[1].Namespace == "Sporacid.Simplets.Webapp.Services.Database")
                .WithConstructorArgument(ConfigurationManager.ConnectionStrings["SIMPLETS_TESTSConnectionString"].ConnectionString);
        }
    }
}