namespace Sporacid.Simplets.Webapp.Core.Security.Authentication
{
    using System;
    using System.Diagnostics.Contracts;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security.Authentication;
    using Sporacid.Simplets.Webapp.Core.Resources.Contracts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClass(typeof (AuthenticationModuleContract))]
    public interface IAuthenticationModule : IAuthenticationObservable
    {
        /// <summary>
        /// Authenticate a user agaisnt a membership repository.
        /// If the authentication fails, an exception will be raised.
        /// </summary>
        /// <param name="credentials">The credentials of the user.</param>
        /// <exception cref="SecurityException" />
        /// <exception cref="WrongCredentialsException">If user does not exist or the password does not match.</exception>
        ITokenAndPrincipal Authenticate(ICredentials credentials);

        /// <summary>
        /// Whether the authentication scheme is supported.
        /// </summary>
        /// <param name="scheme">The authentication scheme.</param>
        /// <returns> Whether the authentication scheme is supported.</returns>
        bool IsSupported(AuthenticationScheme scheme);

        /// <summary>
        /// The supported authentication schemes, as flags.
        /// </summary>
        /// <returns>The supported authentication schemes.</returns>
        AuthenticationScheme[] Supports();
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IAuthenticationModule))]
    internal abstract class AuthenticationModuleContract : IAuthenticationModule
    {
        /// <summary>
        /// Add an observer to the list of authentication observers.
        /// </summary>
        /// <param name="observer">The observer to add.</param>
        public abstract void AddObserver(IAuthenticationObserver observer);

        /// <summary>
        /// Removes an observer from the list of authentication observers.
        /// </summary>
        /// <param name="observer">The observer to remove.</param>
        public abstract void RemoveObserver(IAuthenticationObserver observer);

        /// <summary>
        /// Notify all observers of an authentication.
        /// </summary>
        /// <param name="tokenAndPrincipal">The token and principals of the newly authenticated user.</param>
        public abstract void NotifyAuthentication(ITokenAndPrincipal tokenAndPrincipal);

        /// <summary>
        /// Authenticate a user agaisnt a membership repository.
        /// If the authentication fails, an exception will be raised.
        /// </summary>
        /// <param name="credentials">The credentials of the user.</param>
        /// <exception cref="SecurityException" />
        /// <exception cref="WrongCredentialsException">If user does not exist or the password does not match.</exception>
        public ITokenAndPrincipal Authenticate(ICredentials credentials)
        {
            // Preconditions.
            Contract.Requires(credentials != null, ContractStrings.AuthenticationModule_Authenticate_RequiresCredentials);
            Contract.Requires(credentials.Identity != null, ContractStrings.AuthenticationModule_Authenticate_RequiresIdentity);

            // Postconditions.
            Contract.Ensures(Contract.Result<ITokenAndPrincipal>() != null, ContractStrings.AuthenticationModule_Authenticate_EnsuresNonNullTokenAndPrincipal);
            Contract.Ensures(Contract.Result<ITokenAndPrincipal>().Token != null, ContractStrings.AuthenticationModule_Authenticate_EnsuresNonNullToken);
            Contract.Ensures(Contract.Result<ITokenAndPrincipal>().Principal != null, ContractStrings.AuthenticationModule_Authenticate_EnsuresNonNullPrincipal);

            // Dummy return.
            return default(ITokenAndPrincipal);
        }

        /// <summary>
        /// Whether the authentication scheme is supported.
        /// </summary>
        /// <param name="scheme">The authentication scheme.</param>
        /// <returns> Whether the authentication scheme is supported.</returns>
        public Boolean IsSupported(AuthenticationScheme scheme)
        {
            // Dummy return.
            return default(Boolean);
        }

        /// <summary>
        /// The supported authentication schemes, as flags.
        /// </summary>
        /// <returns>The supported authentication schemes.</returns>
        public AuthenticationScheme[] Supports()
        {
            // Dummy return.
            return default(AuthenticationScheme[]);
        }
    }
}