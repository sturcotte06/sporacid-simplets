namespace Sporacid.Simplets.Webapp.Core.Security.Ldap
{
    using System;
    using System.Diagnostics.Contracts;
    using Sporacid.Simplets.Webapp.Core.Resources.Contracts;
    using Sporacid.Simplets.Webapp.Tools.Strings;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClass(typeof (LdapSearcherContract))]
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

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (ILdapSearcher))]
    internal abstract class LdapSearcherContract : ILdapSearcher
    {
        /// <summary>
        /// Search for a ldap user.
        /// </summary>
        /// <param name="searchBy">The search clause.</param>
        /// <param name="value">The value of the clause.</param>
        /// <returns>The ldap user.</returns>
        public ILdapUser SearchForUser(SearchBy searchBy, String value)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(value), ContractStrings.LdapSearcher_SearchForUser_RequiresValue);

            // Postconditions.
            Contract.Ensures(Contract.Result<ILdapUser>() != null, ContractStrings.LdapSearcher_SearchForUser_EnsuresNonNullLdapUser);

            // Dummy return.
            return default(ILdapUser);
        }
    }
}