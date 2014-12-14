namespace Sporacid.Simplets.Webapp.Core.Exceptions.Repositories
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class EntityNotFoundException<TEntity> : RepositoryException
    {
        public EntityNotFoundException()
            : base(String.Format("The entity of type {0} cannot be found.", typeof (TEntity).Name))
        {
        }

        public EntityNotFoundException(object entityId)
            : base(String.Format("The entity of type {0} with id {1} cannot be found.", typeof (TEntity), entityId))
        {
        }
    }
}