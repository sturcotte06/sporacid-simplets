namespace Sporacid.Simplets.Webapp.Core.Security.Authentication
{
    using System.Diagnostics.Contracts;
    using Sporacid.Simplets.Webapp.Core.Resources.Contracts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClass(typeof (AuthenticationObserverContract))]
    public interface IAuthenticationObserver
    {
        /// <summary>
        /// Updates the observer.
        /// </summary>
        /// <param name="tokenAndPrincipal">The token and principals of the newly authenticated user.</param>
        void Update(ITokenAndPrincipal tokenAndPrincipal);
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IAuthenticationObserver))]
    internal abstract class AuthenticationObserverContract : IAuthenticationObserver
    {
        /// <summary>
        /// Updates the observer.
        /// </summary>
        /// <param name="tokenAndPrincipal">The token and principals of the newly authenticated user.</param>
        public void Update(ITokenAndPrincipal tokenAndPrincipal)
        {
            // Preconditions.
            Contract.Requires(tokenAndPrincipal != null, ContractStrings.AuthenticationObserver_Update_RequiresTokenAndPrincipal);
            Contract.Requires(tokenAndPrincipal.Token != null, ContractStrings.AuthenticationObserver_Update_RequiresToken);
            Contract.Requires(tokenAndPrincipal.Principal != null, ContractStrings.AuthenticationObserver_Update_RequiresPrincipal);
        }
    }
}