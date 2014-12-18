namespace Sporacid.Simplets.Webapp.Core.Repositories
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IDatabaseCreator : IDisposable
    {
        /// <summary>
        /// Creates the database.
        /// </summary>
        void CreateDatabase();

        /// <summary>
        /// Drops the database.
        /// </summary>
        void DropDatabase();
    }
}