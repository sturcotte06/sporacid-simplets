namespace Sporacid.Simplets.Webapp.Services.Services
{
    using System.Reflection;
    using System.Web.Http;
    using log4net;
    using Sporacid.Simplets.Webapp.Core.Aspects.Logging;

    /// <summary>
    /// </summary>
    [Trace]
    public abstract class BaseService : ApiController
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected BaseService()
        {
            this.asdf();
        }

        private void asdf()
        {
            Logger.DebugFormat("Entering: {0}", "asf");
        }
    }
}