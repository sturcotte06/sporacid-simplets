namespace Sporacid.Simplets.Webapp.Core.Security.Authentication.Impl
{
    using System;
    using System.Security.Principal;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class Principal : IPrincipal
    {
        private readonly AuthorizationLevel authorizationLevel;

        internal Principal(string name, AuthenticationScheme authenticationScheme, AuthorizationLevel authorizationLevel)
        {
            this.authorizationLevel = authorizationLevel;
            this.Identity = new PrincipalIdentity(name, authenticationScheme);
        }

        /// <summary>
        /// Determines whether the current principal belongs to the specified role.
        /// </summary>
        /// <returns>
        /// true if the current principal is a member of the specified role; otherwise, false.
        /// </returns>
        /// <param name="role">The name of the role for which to check membership. </param>
        public bool IsInRole(string role)
        {
            AuthorizationLevel authorizationLevel2;
            if (!Enum.TryParse(role, out authorizationLevel2))
            {
                return false;
            }

            return this.authorizationLevel == authorizationLevel2;
        }

        /// <summary>
        /// Gets the identity of the current principal.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Security.Principal.IIdentity" /> object associated with the current principal.
        /// </returns>
        public IIdentity Identity { get; private set; }
    }
}