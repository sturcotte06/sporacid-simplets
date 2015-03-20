namespace Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Exception
{
    using System;
    using System.Net;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IExceptionMap
    {
        /// <summary>
        /// Maps an exception to an http status code.
        /// </summary>
        /// <typeparam name="TException">Type of the exception.</typeparam>
        /// <returns>The http status code for this exception type.</returns>
        HttpStatusCode Map<TException>() where TException : Exception;

        /// <summary>
        /// Maps an exception to an http status code.
        /// </summary>
        /// <param name="exceptionType">Type of the exception.</param>
        /// <returns>The http status code for this exception type.</returns>
        HttpStatusCode Map(Type exceptionType);
    }
}