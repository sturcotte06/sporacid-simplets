namespace Sporacid.Simplets.Webapp.Core.Security.Authentication.Impl
{
    using System;
    using Sporacid.Simplets.Webapp.Core.Exceptions;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Authentication;
    using Sporacid.Simplets.Webapp.Core.Models.Sessions;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class KerberosAuthenticationModule : IAuthenticationModule
    {
        public KerberosAuthenticationModule()
        {
            
        }

        /// <summary>
        /// Authenticate a user agaisnt a membership repository.
        /// If the authentication fails, an exception will be raised.
        /// </summary>
        /// <param name="credentials">The credentials of the user.</param>
        /// <exception cref="SecurityException" />
        /// <exception cref="WrongUsernameException">If user does not exist.</exception>
        /// <exception cref="WrongPasswordException">If the password does not match.</exception>
        public void Authenticate(ICredentials credentials)
        {
            throw new NotImplementedException();
        }
    }
}