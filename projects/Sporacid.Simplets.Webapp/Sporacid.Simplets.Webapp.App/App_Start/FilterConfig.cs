using Sporacid.Simplets.Webapp.App.Filters;
using System.Web;
using System.Web.Mvc;

namespace Sporacid.Simplets.Webapp.App
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            // Register global filter
            //GlobalFilters.Filters.Add(new LocalizationFilterAttribute());
            //GlobalFilters.Filters.Add(new HandleErrorAttribute());
            //RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}