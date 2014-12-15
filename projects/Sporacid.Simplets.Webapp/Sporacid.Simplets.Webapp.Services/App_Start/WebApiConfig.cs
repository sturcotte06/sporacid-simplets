namespace Sporacid.Simplets.Webapp.Services
{
    using System.Diagnostics;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Http.Dispatcher;
    using System.Web.Http.Tracing;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Resolvers;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Trace;

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
            config.Services.Replace(typeof(ITraceWriter), new Log4NetTraceWriter());
            Trace.AutoFlush = true;
        }
    }
}