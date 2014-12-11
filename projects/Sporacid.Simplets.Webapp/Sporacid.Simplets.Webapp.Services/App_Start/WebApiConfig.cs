namespace Sporacid.Simplets.Webapp.Services
{
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Authentication;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            // // Convention-based routing.
            // config.Routes.MapHttpRoute(
            //     name: "SimpletsRoutingConvention",
            //     routeTemplate: "api/v1/{controller}");
        }
    }
}