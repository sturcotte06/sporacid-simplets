namespace Sporacid.Simplets.Webapp.Core.Exceptions.Security
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class SecurityException : CoreException
    {
        public SecurityException(String message) : base(message)
        {
        }

        public SecurityException(String message, Exception cause) : base(message, cause)
        {
        }
    }
}