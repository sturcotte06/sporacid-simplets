namespace Sporacid.Simplets.Webapp.Core.Security.Authentication.Impl
{
    using System.Security.Principal;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class Principal : IPrincipal
    {
        // private readonly AuthorizationLevel authorizationLevel;

        internal Principal(string name, AuthenticationScheme authenticationScheme)
        {
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
            // AuthorizationLevel authorizationLevel2;
            // if (!Enum.TryParse(role, out authorizationLevel2))
            // {
            //     return false;
            // }
            // 
            // return this.authorizationLevel == authorizationLevel2;
            return false;
        }

        /// <summary>
        /// Gets the identity of the current principal.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Security.Principal.IIdentity" /> object associated with the current principal.
        /// </returns>
        public IIdentity Identity { get; private set; }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object" />.
        /// </returns>
        public override int GetHashCode()
        {
            return this.Identity.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            var identity = obj as IIdentity;
            return identity != null && this.Identity.Equals(obj);
        }
    }
}