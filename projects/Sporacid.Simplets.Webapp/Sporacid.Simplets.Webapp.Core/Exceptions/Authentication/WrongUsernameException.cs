namespace Sporacid.Simplets.Webapp.Core.Exceptions.Authentication
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class WrongUsernameException : SecurityException
    {
        public WrongUsernameException() : base("The username specified by given credentials does not exist.")
        {
        }
    }
}