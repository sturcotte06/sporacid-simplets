namespace Sporacid.Simplets.Webapp.Tools.Threading.Pooling.Exceptions
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    public class ThreadPoolNotStartedException : InvalidOperationException
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ThreadPoolNotStartedException()
            : base("The thread pool was not started. Cannot proceed until Start() is called successfully.")
        {
        }
    }
}