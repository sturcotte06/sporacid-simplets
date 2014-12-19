namespace Sporacid.Simplets.Webapp.Core.Exceptions.Authentication
{
    using System;
    using Sporacid.Simplets.Webapp.Core.Resources.Exceptions;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class WrongCredentialsException : SecurityException
    {
        public WrongCredentialsException() : base(ExceptionStrings.Core_Exceptions_Security_WrongCredentials)
        {

        }
    }
}