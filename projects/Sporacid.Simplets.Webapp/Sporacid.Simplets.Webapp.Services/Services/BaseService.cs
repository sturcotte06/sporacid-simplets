namespace Sporacid.Simplets.Webapp.Services.Services
{
    using System;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Exception;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [HandlesException]
    public abstract class BaseService : ApiController
    {
        /// <summary>
        /// The services base path.
        /// </summary>
        protected const String BasePath = "api/v1";

        /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
        /// <version>1.9.0</version>
        protected enum CacheDuration : int
        {
            None = 0,
            Shortest = 30,
            Short = 60,
            Medium = 5*60,
            Long = 60*60,
            VeryLong = 24*60*60,
            Maximum = Int32.MaxValue
        }
    }
}