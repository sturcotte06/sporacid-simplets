namespace Sporacid.Simplets.Webapp.Tools.Threading.Pooling.Exceptions
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    public class WaitException : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public WaitException() : this((Exception)null)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="innerException">This exception's cause.</param>
        public WaitException(Exception innerException)
            : base("There was an error while waiting for the result to be computed.", innerException)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        public WaitException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        /// <param name="innerException">This exception's cause.</param>
        public WaitException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}