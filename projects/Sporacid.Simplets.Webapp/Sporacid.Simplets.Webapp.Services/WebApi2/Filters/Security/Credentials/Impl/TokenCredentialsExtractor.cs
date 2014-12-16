namespace Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Security.Credentials.Impl
{
    using System;
    using System.Text;
    using Sporacid.Simplets.Webapp.Core.Security.Authentication;
    using Sporacid.Simplets.Webapp.Core.Security.Authentication.Impl;
    using Sporacid.Simplets.Webapp.Tools.Strings;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class TokenCredentialsExtractor : ICredentialsExtractor
    {
        private static readonly AuthenticationScheme[] SupportedSchemes = {AuthenticationScheme.Token};
        private static readonly Encoding Encoding;

        static TokenCredentialsExtractor()
        {
            var encoding = Encoding.ASCII;
            Encoding = (Encoding) encoding.Clone();
            Encoding.DecoderFallback = DecoderFallback.ExceptionFallback;
        }

        /// <summary>
        /// Extract credentials from
        /// </summary>
        /// <param name="credentialsParameter"></param>
        /// <returns></returns>
        public ICredentials Extract(string credentialsParameter)
        {
            var credentialBytes = Convert.FromBase64String(credentialsParameter);
            var token = Encoding.GetString(credentialBytes);
            if (token.IsNullOrEmpty())
            {
                // There were no credentials.
                return null;
            }

            return new Credentials(token, null);
        }

        /// <summary>
        /// Whether the authentication scheme is supported.
        /// </summary>
        /// <param name="scheme">The authentication scheme.</param>
        /// <returns> Whether the authentication scheme is supported.</returns>
        public bool IsSupported(AuthenticationScheme scheme)
        {
            return scheme == AuthenticationScheme.Token;
        }

        /// <summary>
        /// The supported authentication schemes, as flags.
        /// </summary>
        /// <returns>The supported authentication schemes.</returns>
        public AuthenticationScheme[] Supports()
        {
            return SupportedSchemes;
        }
    }
}