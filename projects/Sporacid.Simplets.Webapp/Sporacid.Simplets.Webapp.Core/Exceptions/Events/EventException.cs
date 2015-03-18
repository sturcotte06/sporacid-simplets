namespace Sporacid.Simplets.Webapp.Core.Exceptions.Events
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class EventException : CoreException
    {
        public EventException(string message) : base(message)
        {
        }

        public EventException(string message, Exception cause) : base(message, cause)
        {
        }
    }
}