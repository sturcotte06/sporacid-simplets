namespace Sporacid.Simplets.Webapp.App.Filters
{
    using System;
    using System.Configuration;
    using System.Threading;
    using System.Web.Mvc;
    using Sporacid.Simplets.Webapp.Tools.Threading;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class LocalizationFilterAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var cultureHeader = filterContext.HttpContext.Request.Headers["Accept-Language"];
            Thread.CurrentThread.ToCulture(cultureHeader ?? ConfigurationManager.AppSettings["DefaultLanguage"]);
        }
    }
}