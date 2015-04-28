namespace Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Exception.Impl
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http.Filters;
    using Autofac.Integration.WebApi;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class ExceptionFilter : IAutofacExceptionFilter
    {
        private readonly IExceptionMap exceptionMap;

        public ExceptionFilter(IExceptionMap exceptionMap)
        {
            this.exceptionMap = exceptionMap;
        }

        /// <summary>
        /// Called when an exception is thrown.
        /// </summary>
        /// <param name="actionExecutedContext">The context for the action.</param>
        public void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception == null)
            {
                return;
            }

            var request = actionExecutedContext.Request;
            var exception = actionExecutedContext.Exception;

            if (actionExecutedContext.Exception is HttpException)
            {
                var httpException = (HttpException) exception;
                actionExecutedContext.Response = request.CreateErrorResponse((HttpStatusCode) httpException.GetHttpCode(), httpException);
            }

            // Not an Http exception so cannot map directly to a http status code.
            // Do a lookup in the mappings.
            var exceptionType = exception.GetType();
            var httpStatusCode = this.exceptionMap.Map(exceptionType);
#if DEBUG
            actionExecutedContext.Response = request.CreateErrorResponse(httpStatusCode, exception);
#else
            actionExecutedContext.Response = request.CreateResponse(httpStatusCode, CreateErrorObject(exception));
#endif
        }

        /// <summary>
        /// Creates an error object from an exception.
        /// </summary>
        /// <param name="exception">The source exception.</param>
        /// <returns>An error object.</returns>
        private Object CreateErrorObject(Exception exception)
        {
            return new
            {
                exception.Message,
                Cause = exception.InnerException != null
                    ? this.CreateErrorObject(exception.InnerException)
                    : null
            };
        }
    }
}