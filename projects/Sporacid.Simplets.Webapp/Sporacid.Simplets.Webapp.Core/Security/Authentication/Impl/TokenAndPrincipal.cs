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

        /// <summary>
        /// Determines whether the current principal belongs to the specified role.
        /// </summary>
        /// <returns>
        /// true if the current principal is a member of the specified role; otherwise, false.
        /// </returns>
        /// <param name="role">The name of the role for which to check membership. </param>
        public bool IsInRole(string role)
        {
            return this.Principal.IsInRole(role);
        }

        /// <summary>
        /// Gets the identity of the current principal.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Security.Principal.IIdentity" /> object associated with the current principal.
        /// </returns>
        public IIdentity Identity
        {
            get { return this.Principal.Identity; }
        }
    }
}