namespace Sporacid.Simplets.Webapp.Core.Security.Ldap
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface ILdapSearcher
    {
        /// <summary>
        /// Search for a ldap user.
        /// </summary>
        /// <param name="searchBy">The search clause.</param>
        /// <param name="value">The value of the clause.</param>
        /// <returns>The ldap user.</returns>
        ILdapUser SearchForUser(SearchBy searchBy, String value);
    }
}