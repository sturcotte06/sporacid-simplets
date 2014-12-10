namespace Sporacid.Simplets.Webapp.Tools.Threading.Pooling.Exceptions
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    public class ThreadPoolAlreadyShutdownException : InvalidOperationException
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ThreadPoolAlreadyShutdownException()
            : base("The thread pool was already shutdown.")
        {
        }
    }
}