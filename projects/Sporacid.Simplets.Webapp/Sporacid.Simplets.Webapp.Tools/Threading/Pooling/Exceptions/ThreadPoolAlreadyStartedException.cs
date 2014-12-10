namespace Sporacid.Simplets.Webapp.Tools.Threading.Pooling.Exceptions
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    public class ThreadPoolAlreadyStartedException : InvalidOperationException
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ThreadPoolAlreadyStartedException() : base("The thread pool was already started.")
        {
        }
    }
}