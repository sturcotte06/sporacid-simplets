namespace Sporacid.Simplets.Webapp.Core.Repositories.Impl
{
    using System;
    using System.Data.Linq;
    using Ninject;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Repositories;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class DataContextStore : IDataContextStore
    {
        public DataContextStore(IKernel kernel, Type dataContextType)
        {
            if (!typeof (DataContext).IsAssignableFrom(dataContextType))
            {
                throw new RepositoryException(String.Format("{0} is not a DataContext type.", dataContextType));
            }
            
            // No data context. Create a new one.
            this.DataContext = kernel.Get(dataContextType) as DataContext;

            if (this.DataContext == null)
            {
                throw new RepositoryException(String.Format("{0} is not a DataContext type.", dataContextType));
            }
        }

        /// <summary>
        /// The data context. It should not be disposed by users of this interface.
        /// </summary>
        public DataContext DataContext { get; private set; }
    }
}