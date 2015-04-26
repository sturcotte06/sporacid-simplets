namespace Sporacid.Simplets.Webapp.Services.Services.Public.Impl
{
    using System;
    using System.Web.Http;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath)]
    public class AnonymousController : BaseSecureService, IAnonymousService
    {
        public AnonymousController()
        {
            Console.WriteLine("");
        }

        /// <summary>
        /// Dummy method that has no side effect on the system.
        /// However, the request will pass through the api's pipeline; it can therefore be used to test a principal's rights in the
        /// system.
        /// </summary>
        [HttpGet, Route("no-op")]
        public void NoOp()
        {
        }
    }
}