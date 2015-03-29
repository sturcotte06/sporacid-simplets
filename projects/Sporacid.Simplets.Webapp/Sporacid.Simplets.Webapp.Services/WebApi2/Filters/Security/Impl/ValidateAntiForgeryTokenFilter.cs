namespace Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Security.Impl
{
    //using System;
    //using System.Collections.Generic;
    //using System.Linq;
    //using System.Net.Http;
    //using System.Text;
    //using System.Threading;
    //using System.Threading.Tasks;
    //using System.Web;
    //using System.Web.Helpers;
    //using System.Web.Http.Controllers;
    //using System.Web.Mvc;
    //using Sporacid.Simplets.Webapp.Core.Exceptions.Security;
    //using IAuthorizationFilter = System.Web.Http.Filters.IAuthorizationFilter;

    ///// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    ///// <version>1.9.0</version>
    //public class ValidateAntiForgeryTokenFilter : IAuthorizationFilter
    //{
    //    private const String AntiForgeryTokenHeader = "Anti-Forgery-Token";
    //    private static readonly Encoding Encoding;

    //    static ValidateAntiForgeryTokenFilter()
    //    {
    //        var encoding = Encoding.ASCII;
    //        Encoding = (Encoding) encoding.Clone();
    //        Encoding.DecoderFallback = DecoderFallback.ExceptionFallback;
    //    }

    //    /// <summary>
    //    /// Gets or sets a value indicating whether more than one instance of the indicated attribute can be specified for a single
    //    /// program element.
    //    /// </summary>
    //    /// <returns>
    //    /// true if more than one instance is allowed to be specified; otherwise, false. The default is false.
    //    /// </returns>
    //    public bool AllowMultiple
    //    {
    //        get { return false; }
    //    }

    //    /// <summary>
    //    /// Executes the authorization filter to synchronize.
    //    /// </summary>
    //    /// <returns>
    //    /// The authorization filter to synchronize.
    //    /// </returns>
    //    /// <param name="actionContext">The action context.</param>
    //    /// <param name="cancellationToken">The cancellation token associated with the filter.</param>
    //    /// <param name="continuation">The continuation.</param>
    //    public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken,
    //        Func<Task<HttpResponseMessage>> continuation)
    //    {
    //        var request = actionContext.Request;
    //        IEnumerable<String> csrfTokens;

    //        if (!request.Headers.TryGetValues(AntiForgeryTokenHeader, out csrfTokens))
    //        {
    //            throw new SecurityException("Impossible to validate anti-forgery token. Such token is required. Default header name: Anti-Forgery-Token.");
    //        }

    //        // Take the first token. Don't know why we would have more than one.
    //        var csrfToken = csrfTokens.FirstOrDefault();
    //        if (String.IsNullOrEmpty(csrfToken))
    //        {
    //            throw new SecurityException("Impossible to validate anti-forgery token. None were supplied.");
    //        }

    //        csrfToken = Encoding.GetString(Convert.FromBase64String(csrfToken));
    //        var csrfTokenSplit = csrfToken.Split(':');
    //        if (csrfTokenSplit.Length != 2)
    //        {
    //            throw new SecurityException("Impossible to validate anti-forgery token. None were supplied.");
    //        }

    //        var csrfFormToken = csrfTokenSplit[0];
    //        var csrfCookieToken = csrfTokenSplit[1];

    //        try
    //        {
    //            // Validate the old csrf token.
    //            AntiForgery.Validate(csrfCookieToken, csrfFormToken);
    //        }
    //        catch (HttpAntiForgeryException ex)
    //        {
    //            throw new SecurityException("Token was not validated. Possible cross-site request detected.", ex);
    //        }

    //        // Validated. Create a new csrf token in the response for this user's next request.
    //        String newCsrfFormToken, newCsrfCookieToken;
    //        AntiForgery.GetTokens(csrfCookieToken, out newCsrfCookieToken, out newCsrfFormToken);
    //        var newCsrfToken = Convert.ToBase64String(
    //            Encoding.GetBytes(String.Format("{0}:{1}", newCsrfFormToken, newCsrfCookieToken)));
    //        HttpContext.Current.Response.Headers.Add(AntiForgeryTokenHeader, newCsrfToken);

    //        return continuation();
    //    }
    //}
}