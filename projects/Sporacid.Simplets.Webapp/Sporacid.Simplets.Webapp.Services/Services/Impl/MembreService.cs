namespace Sporacid.Simplets.Webapp.Services.Services.Impl
{
    using System;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Core.Aspects.Logging;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Trace]
    [RoutePrefix("api/v1/membre")]
    public class MembreService : BaseService, IMembreService
    {
        /// <summary>
        /// </summary>
        /// <param name="clubId"></param>
        /// <param name="membre"></param>
        [Route("")]
        [HttpPost]
        public void Add(int clubId, object membre)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="membreId"></param>
        /// <returns></returns>
        // [Authorize]
        [Route("{membreId:int}")]
        [HttpGet]
        public object Get(int membreId)
        {
            throw new ArgumentException();
        }
    }
}