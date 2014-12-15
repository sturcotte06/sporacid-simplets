namespace Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Authentication.Credentials
{
    using Sporacid.Simplets.Webapp.Core.Security.Authentication;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface ICredentialsExtractor
    {
        /// <summary>
        /// Extract credentials from
        /// </summary>
        /// <param name="credentialsParameter"></param>
        /// <returns></returns>
        ICredentials Extract(string credentialsParameter);

        /// <summary>
        /// Whether the authentication scheme is supported.
        /// </summary>
        /// <param name="scheme">The authentication scheme.</param>
        /// <returns> Whether the authentication scheme is supported.</returns>
        bool IsSupported(AuthenticationScheme scheme);

        /// <summary>
        /// The supported authentication schemes, as flags.
        /// </summary>
        /// <returns>The supported authentication schemes.</returns>
        AuthenticationScheme[] Supports();
    }
}