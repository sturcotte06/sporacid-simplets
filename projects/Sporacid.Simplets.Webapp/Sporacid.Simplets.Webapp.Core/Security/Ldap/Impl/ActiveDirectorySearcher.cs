namespace Sporacid.Simplets.Webapp.Core.Security.Ldap.Impl
{
    using System;
    using System.DirectoryServices.AccountManagement;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class ActiveDirectorySearcher : ILdapSearcher
    {
        private readonly String activeDirectyDomainName;

        public ActiveDirectorySearcher(String activeDirectyDomainName)
        {
            this.activeDirectyDomainName = activeDirectyDomainName;
        }

        /// <summary>
        /// Search for a ldap user.
        /// </summary>
        /// <param name="searchBy">The search clause.</param>
        /// <param name="value">The value of the clause.</param>
        /// <returns></returns>
        public ILdapUser SearchForUser(SearchBy searchBy, String value)
        {
            // TODO change my hardcoded credentials for the authenticated user's
            using (var context = new PrincipalContext(ContextType.Domain, this.activeDirectyDomainName, "AJ50440", "AOPbgtZXD666"))
            {
                // find user by display name
                var user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, value);
                if (user == null)
                {
                    throw new SecurityException(String.Format("User {0} does not exist in active directory.", value));
                }

                return new ActiveDirectoryUser
                {
                    Username = user.SamAccountName,
                    FirstName = user.GivenName,
                    LastName = user.Surname,
                    Email = user.EmailAddress,
                };
            }
        }
    }
}