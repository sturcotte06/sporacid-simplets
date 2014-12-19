namespace Sporacid.Simplets.Webapp.Core.Security.Ldap
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface ILdapUser
    {
        String Username { get; }
        String Email { get; }
        String FirstName { get; }
        String LastName { get; }
    }
}