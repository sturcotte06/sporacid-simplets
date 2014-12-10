namespace Sporacid.Simplets.Webapp.Core.Exceptions.Authentication
{
    using System;

    [Serializable]
    public class WrongUsernameException : SecurityException
    {
        public WrongUsernameException()
            : base("The username specified by given credentials does not exist.")
        {
        }
    }
}