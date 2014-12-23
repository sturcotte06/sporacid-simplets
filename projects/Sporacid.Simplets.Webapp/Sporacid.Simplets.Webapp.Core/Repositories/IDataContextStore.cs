namespace Sporacid.Simplets.Webapp.Core.Repositories
{
    using System;
    using System.Data.Linq;
    using PostSharp.Patterns.Contracts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IDataContextStore : IDisposable
    {
        /// <summary>
        /// The data context. It should not be disposed by users of this interface.
        /// </summary>
        [Required]
        DataContext DataContext { get; }
    }
}