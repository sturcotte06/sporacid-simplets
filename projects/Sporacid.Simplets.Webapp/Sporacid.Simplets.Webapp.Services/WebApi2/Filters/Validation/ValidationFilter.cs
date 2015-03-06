namespace Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Validation
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class ValidationFilter : IActionFilter
    {
        /// <summary>
        /// Gets or sets a value indicating whether more than one instance of the indicated attribute can be specified for a single
        /// program element.
        /// </summary>
        /// <returns>
        /// true if more than one instance is allowed to be specified; otherwise, false. The default is false.
        /// </returns>
        public bool AllowMultiple
        {
            get { return false; }
        }

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
            if (actionContext.ModelState.IsValid)
            {
                return continuation();
            }

            // Cannot continue. Return a bad request response with the model state errors.
            return Task.FromResult(actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState));
        }
    }
}