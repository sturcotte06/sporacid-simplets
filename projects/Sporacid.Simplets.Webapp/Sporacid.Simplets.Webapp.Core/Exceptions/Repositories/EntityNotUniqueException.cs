namespace Sporacid.Simplets.Webapp.Core.Exceptions.Repositories
{
    using System;
    using Sporacid.Simplets.Webapp.Core.Resources.Exceptions;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class EntityNotUniqueException : RepositoryException
    {
        public EntityNotUniqueException()
            : base(ExceptionStrings.Core_Exceptions_Repository_EntityNotUnique)
        {
        }
    }
}