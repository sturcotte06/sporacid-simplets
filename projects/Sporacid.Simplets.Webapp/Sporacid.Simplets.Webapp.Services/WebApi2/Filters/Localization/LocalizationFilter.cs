namespace Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Localization
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class LocalizationFilter : IActionFilter
    {
        /// <summary>
        /// Gets or sets a value indicating whether more than one instance of the indicated attribute can be specified for a single
        /// program element.
        /// </summary>
        /// <returns>
        /// true if more than one instance is allowed to be specified; otherwise, false. The default is false.
        /// </returns>
        public bool AllowMultiple { get; private set; }

        /// <summary>
        /// Executes the filter action asynchronously.
        /// </summary>
        /// <returns>
        /// The newly created task for this operation.
        /// </returns>
        /// <param name="actionContext">The action context.</param>
        /// <param name="cancellationToken">The cancellation token assigned for this task.</param>
        /// <param name="continuation">The delegate function to continue after the action method is invoked.</param>
        public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            var request = actionContext.Request;

            // Get the culture header if it exists.
            var cultureHeader = request.Headers.AcceptLanguage.FirstOrDefault();
            if (cultureHeader != null)
            {
                // Set specific culture requested by client.
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureHeader.Value);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureHeader.Value);
            }

            // TODO Should we follow http rfc and throw http 406 if we cannot serve the request in the good culture?

            return continuation.Invoke();
        }
    }
}