namespace Sporacid.Simplets.Webapp.Core.Exceptions.Repositories
{
    using System;
    using Sporacid.Simplets.Webapp.Core.Resources.Exceptions;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class EntityNotFoundException<TEntity> : RepositoryException
    {
        public EntityNotFoundException()
            : base(String.Format(ExceptionStrings.Core_Repository_EntityNotFound_Name, typeof (TEntity).Name))
        {
        }

        public EntityNotFoundException(object entityId)
            : base(String.Format(ExceptionStrings.Core_Repository_EntityNotFound_NameAndId, typeof (TEntity), entityId))
        {
        }
    }
}