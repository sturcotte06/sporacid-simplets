namespace Sporacid.Simplets.Webapp.Tools.Threading.Pooling
{
    /// <summary>
    /// Structure for the configuration of a thread pool.
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    public class ThreadPoolConfiguration
    {
        /// <summary>
        /// The number of threads available in the thread pool.
        /// </summary>
        public int ThreadCount { get; set; }

        /// <summary>
        /// Whether the thread pool should be started automatically on instantiation or manually.
        /// </summary>
        public bool AutomaticStart { get; set; }

        /// <summary>
        /// The prefix for threads' names in this thread pool.
        /// This prefix will be suffixed by an integral index.
        /// </summary>
        public string ThreadNamePrefix { get; set; }
    }
}