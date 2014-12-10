namespace Sporacid.Simplets.Webapp.Tools.Threading
{
    using System;

    /// <summary>
    /// Sealed class that represents a null class.
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    [Serializable]
    public sealed class Void
    {
        /// <summary>
        /// The value for Void.
        /// </summary>
        public static readonly Void Value = new Void();

        /// <summary>
        /// Private constructor.
        /// </summary>
        private Void()
        {
        }
    }
}