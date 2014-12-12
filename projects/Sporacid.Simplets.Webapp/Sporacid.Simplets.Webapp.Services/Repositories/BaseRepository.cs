namespace Sporacid.Simplets.Webapp.Services.Repositories
{
    using System.Data.Linq;
    using Sporacid.Simplets.Webapp.Services.LinqToSql;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class BaseRepository : IRepository
    {
        protected BaseRepository(DatabaseDataContext dataContext, CommitBehaviour commitBehaviour)
        {
            this.DataContext = dataContext;
            this.CommitBehaviour = commitBehaviour;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.DataContext.Dispose();
        }

        /// <summary>
        /// The linq to sql data context.
        /// </summary>
        public DatabaseDataContext DataContext { get; private set; }

        /// <summary>
        /// The commit behaviour.
        /// </summary>
        public CommitBehaviour CommitBehaviour { get; private set; }

        /// <summary>
        /// Commit all pending changes.
        /// </summary>
        public void CommitAll()
        {
            this.DataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);
        }
    }
}