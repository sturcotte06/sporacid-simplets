namespace Sporacid.Simplets.Webapp.Services
{
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Http.Dispatcher;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Resolvers;

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
        }
    }
}