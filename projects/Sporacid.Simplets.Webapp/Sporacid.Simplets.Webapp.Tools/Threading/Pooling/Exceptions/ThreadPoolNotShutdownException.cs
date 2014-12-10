namespace Sporacid.Simplets.Webapp.Tools.Threading.Pooling.Exceptions
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    public class ThreadPoolNotShutdownException : InvalidOperationException
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ThreadPoolNotShutdownException()
            : base("The thread pool was not shutdown. Cannot proceed until Shutdown() is called successfully.")
        {
        }
    }
}