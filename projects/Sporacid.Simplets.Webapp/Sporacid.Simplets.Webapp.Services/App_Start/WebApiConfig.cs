namespace Sporacid.Simplets.Webapp.Services
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Description;
    using System.Web.Http.Dispatcher;
    using System.Web.Http.Tracing;
    using Sporacid.Simplets.Webapp.Services.Services;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Description;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Trace;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            // The convention for this project is NameService instead of NameController.
            config.Services.Replace(typeof (IHttpControllerTypeResolver), new ServiceHttpControllerTypeResolver());
            var suffix = typeof (DefaultHttpControllerSelector).GetField("ControllerSuffix", BindingFlags.Static | BindingFlags.Public);
            if (suffix != null)
            {
                suffix.SetValue(null, "Service");
            }

            // Use log4net for logging
            config.Services.Replace(typeof (ITraceWriter), new Log4NetTraceWriter());
            Trace.AutoFlush = true;

            config.Services.Replace(typeof (IDocumentationProvider),
                new XmlDocumentationProvider(HttpContext.Current.Server.MapPath("~/bin/Sporacid.Simplets.Webapp.Services.xml")));
        }

        /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
        /// <version>1.9.0</version>
        private class ServiceHttpControllerTypeResolver : DefaultHttpControllerTypeResolver
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
}