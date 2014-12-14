namespace Sporacid.Simplets.Webapp.Core.Security.Authentication
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface ICredentials
    {
        /// <summary>
        /// The username of the credentials' owner.
        /// </summary>
        [Required]
        String Username { get; set; }

        /// <summary>
        /// The password of the credentials' owner.
        /// </summary>
        [Required]
        String Password { get; set; }
    }
}