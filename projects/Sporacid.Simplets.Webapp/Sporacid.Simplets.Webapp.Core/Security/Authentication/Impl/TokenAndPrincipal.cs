namespace Sporacid.Simplets.Webapp.Core.Security.Authentication.Impl
{
    using System.Security.Principal;
    using Sporacid.Simplets.Webapp.Core.Security.Authentication.Tokens;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class TokenAndPrincipal : ITokenAndPrincipal
    {
        internal TokenAndPrincipal(IToken token, IPrincipal principal)
        {
            this.Token = token;
            this.Principal = principal;
        }

        /// <summary>
        /// Authentication token for this principal.
        /// </summary>
        public IToken Token { get; private set; }

        /// <summary>
        /// The principal.
        /// </summary>
        public IPrincipal Principal { get; private set; }
    }
}