namespace Sporacid.Simplets.Webapp.Core.Exceptions.Authentication
{
    using System;

    [Serializable]
    public class WrongPasswordException : SecurityException
    {
        public WrongPasswordException() : base("The password specified by given credentials was erroneous.")
        {
        }
    }
}