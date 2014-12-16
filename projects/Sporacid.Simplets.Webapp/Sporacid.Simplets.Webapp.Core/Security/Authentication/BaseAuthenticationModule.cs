namespace Sporacid.Simplets.Webapp.Core.Security.Authentication
{
    using System.Collections.Generic;
    using Sporacid.Simplets.Webapp.Core.Exceptions;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Authentication;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public abstract class BaseAuthenticationModule : IAuthenticationModule
    {
        private readonly List<IAuthenticationObserver> observers = new List<IAuthenticationObserver>();

        /// <summary>
        /// Add an observer to the list of authentication observers.
        /// </summary>
        /// <param name="observer">The observer to add.</param>
        public void AddObserver(IAuthenticationObserver observer)
        {
            this.observers.Add(observer);
        }

        /// <summary>
        /// Removes an observer from the list of authentication observers.
        /// </summary>
        /// <param name="observer">The observer to remove.</param>
        public void RemoveObserver(IAuthenticationObserver observer)
        {
            this.observers.Remove(observer);
        }

        /// <summary>
        /// Notify all observers of an authentication.
        /// </summary>
        /// <param name="tokenAndPrincipal">The token and principals of the newly authenticated user.</param>
        public void NotifyAuthentication(ITokenAndPrincipal tokenAndPrincipal)
        {
            this.observers.ForEach(o => o.Update(tokenAndPrincipal));
        }

        /// <summary>
        /// Authenticate a user agaisnt a membership repository.
        /// If the authentication fails, an exception will be raised.
        /// </summary>
        /// <param name="credentials">The credentials of the user.</param>
        /// <exception cref="SecurityException" />
        /// <exception cref="WrongCredentialsException">If user does not exist or the password does not match.</exception>
        public abstract ITokenAndPrincipal Authenticate(ICredentials credentials);

        /// <summary>
        /// Whether the authentication scheme is supported.
        /// </summary>
        /// <param name="scheme">The authentication scheme.</param>
        /// <returns> Whether the authentication scheme is supported.</returns>
        public abstract bool IsSupported(AuthenticationScheme scheme);

        /// <summary>
        /// The supported authentication schemes, as flags.
        /// </summary>
        /// <returns>The supported authentication schemes.</returns>
        public abstract AuthenticationScheme[] Supports();
    }
}