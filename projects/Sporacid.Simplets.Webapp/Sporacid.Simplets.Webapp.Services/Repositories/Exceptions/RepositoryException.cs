namespace Sporacid.Simplets.Webapp.Services.Repositories.Exceptions
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class RepositoryException : SystemException
    {
        public RepositoryException(String message) : base(message)
        {
        }

        public RepositoryException(String message, Exception cause) : base(message, cause)
        {
        }
    }
}