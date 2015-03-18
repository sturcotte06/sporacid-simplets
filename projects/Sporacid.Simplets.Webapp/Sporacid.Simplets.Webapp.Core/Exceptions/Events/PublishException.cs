namespace Sporacid.Simplets.Webapp.Core.Exceptions.Events
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class PublishException : EventException
    {
        public PublishException(string message) : base(message)
        {
        }

        public PublishException(string message, Exception cause) : base(message, cause)
        {
        }
    }
}