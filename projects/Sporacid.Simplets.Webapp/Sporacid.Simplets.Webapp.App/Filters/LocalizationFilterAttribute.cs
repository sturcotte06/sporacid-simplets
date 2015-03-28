using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Sporacid.Simplets.Webapp.Tools.Threading;

namespace Sporacid.Simplets.Webapp.App.Filters
{
    public class LocalizationFilterAttribute: Attribute,IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var cultureHeader = filterContext.HttpContext.Request.Headers["AcceptLanguage"];
            Thread.CurrentThread.ToCulture(cultureHeader != null ? cultureHeader : ConfigurationManager.AppSettings["DefaultLanguage"]);
        }
    }
}