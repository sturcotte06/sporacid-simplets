namespace Sporacid.Simplets.Webapp.Core.Exceptions
{
    using System;

    [Serializable]
    public class SecurityException : SystemException
    {
        public SecurityException(String message)
            : base(message)
        {
        }

        public SecurityException(String message, Exception cause) : base(message, cause)
        {
        }
    }
}