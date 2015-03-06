namespace Sporacid.Simplets.Webapp.Core.Exceptions.Security.Authorization
{
    using System;
    using Sporacid.Simplets.Webapp.Core.Resources.Exceptions;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class NotAuthorizedException : SecurityException
    {
        public NotAuthorizedException()
            : base(ExceptionStrings.Core_Security_Unauthorized)
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