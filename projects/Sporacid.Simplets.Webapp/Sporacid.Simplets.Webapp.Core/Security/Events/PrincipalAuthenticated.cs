namespace Sporacid.Simplets.Webapp.Core.Security.Events
{
    using System.Security.Principal;
    using Sporacid.Simplets.Webapp.Core.Events.Impl;
    using Sporacid.Simplets.Webapp.Core.Security.Authentication.Tokens;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class PrincipalAuthenticated : Event<PrincipalAuthenticatedEventArgs>
    {
        public PrincipalAuthenticated(object sender, PrincipalAuthenticatedEventArgs eventArgs) : base(sender, eventArgs)
        {
        }
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class PrincipalAuthenticatedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public PrincipalAuthenticatedEventArgs(IPrincipal principal, IToken token)
        {
            this.Principal = principal;
            this.Token = token;
        }

        /// <summary>
        /// The principal that was authenticated.
        /// </summary>
        public IPrincipal Principal { get; private set; }

        /// <summary>
        /// The authentication token generated for this principal.
        /// </summary>
        public IToken Token { get; private set; }
    }
}