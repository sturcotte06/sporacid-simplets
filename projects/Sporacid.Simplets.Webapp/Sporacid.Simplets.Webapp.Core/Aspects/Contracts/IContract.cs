namespace Sporacid.Simplets.Webapp.Core.Aspects.Contracts
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IContract
    {
        /// <summary>
        /// The error message. Will be overriden if ErrorMessageResourceName is set.
        /// </summary>
        String ErrorMessage { get; }

        /// <summary>
        /// The error message resource name.
        /// If set, the resource manager will resolve the value into the ErrorMessage property.
        /// </summary>
        String ErrorMessageResourceName { get; }
    }
}