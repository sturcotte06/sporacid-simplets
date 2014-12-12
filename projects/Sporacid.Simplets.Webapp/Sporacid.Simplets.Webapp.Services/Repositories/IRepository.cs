namespace Sporacid.Simplets.Webapp.Services.Repositories
{
    using System;
    using Sporacid.Simplets.Webapp.Services.LinqToSql;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IRepository : IDisposable
    {
        /// <summary>
        /// The linq to sql data context.
        /// </summary>
        DatabaseDataContext DataContext { get; }

        /// <summary>
        /// The commit behaviour.
        /// </summary>
        CommitBehaviour CommitBehaviour { get; }

        /// <summary>
        /// Commit all pending changes.
        /// </summary>
        void CommitAll();
    }
}