namespace Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Exception.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Repositories;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security.Authentication;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security.Authorization;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class ExceptionMap : Dictionary<Type, HttpStatusCode>, IExceptionMap
    {
        public ExceptionMap()
        {
            // {Type.GetType("System.Diagnostics.Contracts.__ContractsRuntime.ContractException"), HttpStatusCode.BadRequest};
            this.Add(typeof (ArgumentException), HttpStatusCode.BadRequest);
            this.Add(typeof (ArgumentNullException), HttpStatusCode.BadRequest);
            this.Add(typeof (ArgumentOutOfRangeException), HttpStatusCode.BadRequest);
            this.Add(typeof (SecurityException), HttpStatusCode.Unauthorized);
            this.Add(typeof (WrongCredentialsException), HttpStatusCode.Unauthorized);
            this.Add(typeof (NotAuthorizedException), HttpStatusCode.Forbidden);
            this.Add(typeof (EntityNotFoundException<>), HttpStatusCode.NotFound);
            this.Add(typeof (NotSupportedException), HttpStatusCode.NotImplemented);
        }

        /// <summary>
        /// Maps an exception to an http status code.
        /// </summary>
        /// <typeparam name="TException">Type of the exception.</typeparam>
        /// <returns>The http status code for this exception type.</returns>
        public HttpStatusCode Map<TException>() where TException : Exception
        {
            return this.Map(typeof (TException));
        }

        /// <summary>
        /// Maps an exception to an http status code.
        /// </summary>
        /// <param name="exceptionType">Type of the exception.</param>
        /// <returns>The http status code for this exception type.</returns>
        public HttpStatusCode Map(Type exceptionType)
        {
            if (exceptionType.IsGenericType)
            {
                exceptionType = exceptionType.GetGenericTypeDefinition();
            }

            HttpStatusCode httpStatusCode;
            return this.TryGetValue(exceptionType, out httpStatusCode)
                ? httpStatusCode
                : HttpStatusCode.InternalServerError;
        }
    }
}