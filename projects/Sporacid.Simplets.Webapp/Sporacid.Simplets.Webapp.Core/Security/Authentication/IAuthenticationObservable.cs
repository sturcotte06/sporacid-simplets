namespace Sporacid.Simplets.Webapp.Core.Security.Authentication
{
    using System.Diagnostics.Contracts;
    using Sporacid.Simplets.Webapp.Core.Resources.Contracts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClass(typeof (AuthenticationObservableContract))]
    public interface IAuthenticationObservable
    {
        /// <summary>
        /// Add an observer to the list of authentication observers.
        /// </summary>
        /// <param name="observer">The observer to add.</param>
        void AddObserver(IAuthenticationObserver observer);

        /// <summary>
        /// Removes an observer from the list of authentication observers.
        /// </summary>
        /// <param name="observer">The observer to remove.</param>
        void RemoveObserver(IAuthenticationObserver observer);

        /// <summary>
        /// Notify all observers of an authentication.
        /// </summary>
        /// <param name="tokenAndPrincipal">The token and principals of the newly authenticated user.</param>
        void NotifyAuthentication(ITokenAndPrincipal tokenAndPrincipal);
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IAuthenticationObservable))]
    internal abstract class AuthenticationObservableContract : IAuthenticationObservable
    {
        /// <summary>
        /// Add an observer to the list of authentication observers.
        /// </summary>
        /// <param name="observer">The observer to add.</param>
        public void AddObserver(IAuthenticationObserver observer)
        {
            // Preconditions.
            Contract.Requires(observer != null, ContractStrings.AuthenticationObservable_AddObserver_RequiresObserver);
        }

        /// <summary>
        /// Removes an observer from the list of authentication observers.
        /// </summary>
        /// <param name="observer">The observer to remove.</param>
        public void RemoveObserver(IAuthenticationObserver observer)
        {
            // Preconditions.
            Contract.Requires(observer != null, ContractStrings.AuthenticationObservable_RemoveObserver_RequiresObserver);
        }

        /// <summary>
        /// Notify all observers of an authentication.
        /// </summary>
        /// <param name="tokenAndPrincipal">The token and principals of the newly authenticated user.</param>
        public void NotifyAuthentication(ITokenAndPrincipal tokenAndPrincipal)
        {
            // Preconditions.
            Contract.Requires(tokenAndPrincipal != null, ContractStrings.AuthenticationObservable_NotifyAuthentication_RequiresTokenAndPrincipal);
            Contract.Requires(tokenAndPrincipal.Token != null, ContractStrings.AuthenticationObservable_NotifyAuthentication_RequiresToken);
            Contract.Requires(tokenAndPrincipal.Principal != null, ContractStrings.AuthenticationObservable_NotifyAuthentication_RequiresPrincipal);
        }
    }
}