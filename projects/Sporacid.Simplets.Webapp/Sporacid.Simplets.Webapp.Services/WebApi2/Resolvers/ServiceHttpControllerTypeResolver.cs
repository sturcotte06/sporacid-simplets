namespace Sporacid.Simplets.Webapp.Services.WebApi2.Resolvers
{
    using System;
    using System.Web.Http.Controllers;
    using System.Web.Http.Dispatcher;
    using Sporacid.Simplets.Webapp.Services.Services;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class ServiceHttpControllerTypeResolver : DefaultHttpControllerTypeResolver
    {
        public ServiceHttpControllerTypeResolver()
            : base(IsHttpEndpoint)
        {
        }

        private static bool IsHttpEndpoint(Type t)
        {
            return t != null &&
                   t.IsClass &&
                   t.IsVisible &&
                   !t.IsAbstract &&
                   typeof (BaseService).IsAssignableFrom(t) &&
                   typeof (IHttpController).IsAssignableFrom(t);
        }
    }
}