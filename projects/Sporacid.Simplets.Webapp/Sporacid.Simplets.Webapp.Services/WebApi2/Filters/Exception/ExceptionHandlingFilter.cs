namespace Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Exception
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http.Filters;
    using Sporacid.Simplets.Webapp.Core.Exceptions;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Repositories;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security.Authentication;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security.Authorization;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class ExceptionHandlingFilter : IExceptionFilter
    {
        public ExceptionHandlingFilter()
        {
            this.Mappings = new Dictionary<Type, HttpStatusCode>
            {
                // {Type.GetType("System.Diagnostics.Contracts.__ContractsRuntime.ContractException"), HttpStatusCode.BadRequest},
                {typeof (ArgumentException), HttpStatusCode.BadRequest},
                {typeof (ArgumentNullException), HttpStatusCode.BadRequest},
                {typeof (ArgumentOutOfRangeException), HttpStatusCode.BadRequest},
                {typeof (SecurityException), HttpStatusCode.Unauthorized},
                {typeof (WrongCredentialsException), HttpStatusCode.Unauthorized},
                {typeof (NotAuthorizedException), HttpStatusCode.Forbidden},
                {typeof (EntityNotFoundException<>), HttpStatusCode.NotFound},
                {typeof (NotSupportedException), HttpStatusCode.NotImplemented},
            };
        }

        /// <summary>
        /// Mapping of exception types and their corresponding http code.
        /// </summary>
        private IDictionary<Type, HttpStatusCode> Mappings { get; set; }

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
        /// Executes an asynchronous exception filter.
        /// </summary>
        /// <returns>
        /// An asynchronous exception filter.
        /// </returns>
        /// <param name="actionExecutedContext">The action executed context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            if (actionExecutedContext.Exception == null)
            {
                return Task.FromResult(0);
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
            if (exceptionType.IsGenericType)
            {
                exceptionType = exceptionType.GetGenericTypeDefinition();
            }

            HttpStatusCode httpStatusCode;
            actionExecutedContext.Response = request.CreateErrorResponse(
                this.Mappings.TryGetValue(exceptionType, out httpStatusCode) ? httpStatusCode : HttpStatusCode.InternalServerError, exception);

            return Task.FromResult(0);
        }
    }
}