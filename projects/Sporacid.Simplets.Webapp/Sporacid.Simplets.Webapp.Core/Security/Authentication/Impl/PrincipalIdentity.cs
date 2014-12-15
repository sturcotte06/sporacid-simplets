namespace Sporacid.Simplets.Webapp.Core.Security.Authentication.Impl
{
    using System.Security.Principal;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class PrincipalIdentity : IIdentity
    {
        internal PrincipalIdentity(string name, AuthenticationScheme authenticationScheme)
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