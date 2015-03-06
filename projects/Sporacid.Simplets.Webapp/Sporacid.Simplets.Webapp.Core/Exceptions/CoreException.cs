namespace Sporacid.Simplets.Webapp.Core.Exceptions
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public abstract class CoreException : SystemException
    {
        protected CoreException(String message) : base(message)
        {
        }

        protected CoreException(String message, Exception cause)
            : base(message, cause)
        {
        }
    }
}