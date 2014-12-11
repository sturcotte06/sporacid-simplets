namespace Sporacid.Simplets.Webapp.Tools.Collections.Caches.Exceptions
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class CachingException : SystemException
    {
        public CachingException(String message)
            : base(message)
        {
        }

        public CachingException(String message, Exception cause)
            : base(message, cause)
        {
        }
    }
}