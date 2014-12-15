namespace Sporacid.Simplets.Webapp.Core.Security.Authentication.Impl
{
    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class Credentials : ICredentials
    {
        public Credentials(string username, string password)
        {
            this.Identity = username;
            this.Password = password;
        }

        /// <summary>
        /// The identity of the credentials' owner.
        /// </summary>
        public string Identity { get; set; }

        /// <summary>
        /// The password of the credentials' owner.
        /// </summary>
        public string Password { get; set; }
    }
}