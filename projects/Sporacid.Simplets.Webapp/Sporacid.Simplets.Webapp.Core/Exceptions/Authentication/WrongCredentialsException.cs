namespace Sporacid.Simplets.Webapp.Core.Exceptions.Authentication
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class WrongCredentialsException : SecurityException
    {
        public WrongCredentialsException() : base("The given credentials does not exist.")
        {
        }
    }
}