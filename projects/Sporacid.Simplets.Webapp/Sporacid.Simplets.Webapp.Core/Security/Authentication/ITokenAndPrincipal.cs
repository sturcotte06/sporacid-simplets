namespace Sporacid.Simplets.Webapp.Core.Security.Authentication
{
    using System.Security.Principal;
    using Sporacid.Simplets.Webapp.Core.Security.Token;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface ITokenAndPrincipal
    {
        /// <summary>
        /// Authentication token for this principal.
        /// </summary>
        IToken Token { get; }

        /// <summary>
        /// The principal.
        /// </summary>
        IPrincipal Principal { get; }
    }
}