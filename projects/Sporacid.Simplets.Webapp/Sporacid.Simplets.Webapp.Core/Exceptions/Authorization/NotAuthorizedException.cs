namespace Sporacid.Simplets.Webapp.Core.Exceptions.Authorization
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class NotAuthorizedException : SecurityException
    {
        public NotAuthorizedException() : base("The action cannot be authorized.")
        {
        }

        public NotAuthorizedException(String message)
            : base(message)
        {
        }

        public NotAuthorizedException(String message, Exception cause)
            : base(message, cause)
        {
        }
    }
}