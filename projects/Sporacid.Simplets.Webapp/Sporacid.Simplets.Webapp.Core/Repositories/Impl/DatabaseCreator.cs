namespace Sporacid.Simplets.Webapp.Core.Repositories.Impl
{
    using System.Data.Linq;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class DatabaseCreator : IDatabaseCreator
    {
        private readonly DataContext dataContext;

        public DatabaseCreator(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        /// <summary>
        /// Creates the database.
        /// </summary>
        public void CreateDatabase()
        {
            if (!this.dataContext.DatabaseExists())
                this.dataContext.CreateDatabase();
            this.Commit();
        }

        /// <summary>
        /// Drops the database.
        /// </summary>
        public void DropDatabase()
        {
            if (this.dataContext.DatabaseExists())
                this.dataContext.DeleteDatabase();
            this.Commit();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.dataContext.Dispose();
        }

        /// <summary>
        /// Commits all pending changes.
        /// </summary>
        private void Commit()
        {
            this.dataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);
        }
    }
}