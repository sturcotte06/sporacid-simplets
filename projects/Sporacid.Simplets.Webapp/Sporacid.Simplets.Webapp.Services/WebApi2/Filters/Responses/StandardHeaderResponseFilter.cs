namespace Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Responses
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;
    using Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Security;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class StandardHeaderResponseFilter : IActionFilter
    {
        private static readonly Encoding Encoding;

        static StandardHeaderResponseFilter()
        {
            var encoding = Encoding.ASCII;
            Encoding = (Encoding) encoding.Clone();
            Encoding.DecoderFallback = DecoderFallback.ExceptionFallback;
        }

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
            // var response = HttpContext.Current.Response;
            // 
            // // Add token authorization informations.
            // var token = AuthenticationFilter.RequestToken;
            // var base64Token = Convert.ToBase64String(Encoding.GetBytes(AuthenticationFilter.RequestToken.Key));
            // response.Headers.Add("Authorization-Token", base64Token);
            // response.Headers.Add("Authorization-Token-Emitted-At", token.EmittedAt.ToString("G"));
            // response.Headers.Add("Authorization-Token-Valid-For", token.ValidFor.ToString());

            return continuation.Invoke();
        }
    }
}