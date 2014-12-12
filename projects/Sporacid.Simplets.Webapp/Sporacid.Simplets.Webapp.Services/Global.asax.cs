namespace Sporacid.Simplets.Webapp.Services
{
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Http.Dispatcher;
    using System.Web.Mvc;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Resolvers;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            // RouteConfig.RegisterRoutes(RouteTable.Routes);

            var config = GlobalConfiguration.Configuration;

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;

            // Configure the json output.
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ssZ";
            config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Configure service type resolve.
            config.Services.Replace(typeof (IHttpControllerTypeResolver), new ServiceHttpControllerTypeResolver());
            var suffix = typeof (DefaultHttpControllerSelector).GetField("ControllerSuffix", BindingFlags.Static | BindingFlags.Public);
            if (suffix != null)
            {
                suffix.SetValue(null, "Service");
            }

            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}