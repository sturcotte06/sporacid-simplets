namespace Sporacid.Simplets.Webapp.Core.Security.Authorization
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IResource
    {
        /// <summary>
        /// The value of the resource, as a string.
        /// Normally returns ToString() implementation.
        /// </summary>
        String Value { get; }
    }
}