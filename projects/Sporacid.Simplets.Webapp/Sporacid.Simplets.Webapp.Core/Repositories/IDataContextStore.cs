namespace Sporacid.Simplets.Webapp.Core.Repositories
{
    using System.Data.Linq;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IDataContextStore
    {
        /// <summary>
        /// The data context. It should not be disposed by users of this interface.
        /// </summary>
        DataContext DataContext { get; }
    }
}