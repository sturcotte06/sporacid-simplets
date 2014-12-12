namespace Sporacid.Simplets.Webapp.Core.Security.Authentication.Impl
{
    using System;
    using System.Security.Principal;
    using Sporacid.Simplets.Webapp.Core.Models.Contexts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class Principal : IPrincipal
    {
        private readonly AuthorizationLevel authorizationLevel;

        internal Principal(string name, AuthenticationScheme authenticationScheme, AuthorizationLevel authorizationLevel)
        {
            this.authorizationLevel = authorizationLevel;
            this.Identity = new Identity(name, authenticationScheme);
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

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class Identity : IIdentity
    {
        internal Identity(string name, AuthenticationScheme authenticationScheme)
        {
            this.Name = name;
            this.AuthenticationType = authenticationScheme.ToString();
            this.IsAuthenticated = true;
        }

        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        /// <returns>
        /// The name of the user on whose behalf the code is running.
        /// </returns>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the type of authentication used.
        /// </summary>
        /// <returns>
        /// The type of authentication used to identify the user.
        /// </returns>
        public string AuthenticationType { get; private set; }

        /// <summary>
        /// Gets a value that indicates whether the user has been authenticated.
        /// </summary>
        /// <returns>
        /// true if the user was authenticated; otherwise, false.
        /// </returns>
        public bool IsAuthenticated { get; private set; }
    }
}