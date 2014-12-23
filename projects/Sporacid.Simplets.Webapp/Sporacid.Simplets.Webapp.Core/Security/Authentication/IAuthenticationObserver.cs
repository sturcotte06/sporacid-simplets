namespace Sporacid.Simplets.Webapp.Core.Security.Authentication
{
    using PostSharp.Patterns.Contracts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IAuthenticationObserver
    {
        /// <summary>
        /// Updates the observer.
        /// </summary>
        /// <param name="tokenAndPrincipal">The token and principals of the newly authenticated user.</param>
        void Update([Required] ITokenAndPrincipal tokenAndPrincipal);
    }
}