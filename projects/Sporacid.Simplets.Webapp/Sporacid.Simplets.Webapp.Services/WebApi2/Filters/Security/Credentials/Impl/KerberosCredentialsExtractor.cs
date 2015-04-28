namespace Sporacid.Simplets.Webapp.Services.WebApi2.Filters.Security.Credentials.Impl
{
    using System;
    using System.Text;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security.Authentication;
    using Sporacid.Simplets.Webapp.Core.Security.Authentication;
    using Sporacid.Simplets.Webapp.Core.Security.Authentication.Impl;
    using Sporacid.Simplets.Webapp.Tools.Strings;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class KerberosCredentialsExtractor : ICredentialsExtractor
    {
        private static readonly AuthenticationScheme[] SupportedSchemes = {AuthenticationScheme.Kerberos};
        private static readonly Encoding Encoding;

        static KerberosCredentialsExtractor()
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
        public ICredentials Extract(String credentialsParameter)
        {
            var credentialBytes = Convert.FromBase64String(credentialsParameter);
            var decodedCredentials = Encoding.GetString(credentialBytes);
            if (decodedCredentials.IsNullOrEmpty())
            {
                // There were no credentials.
                return null;
            }

            var colonIndex = decodedCredentials.IndexOf(':');
            if (colonIndex == -1)
            {
                throw new WrongCredentialsException();
            }

            var username = decodedCredentials.Substring(0, colonIndex);
            var password = decodedCredentials.Substring(colonIndex + 1);
            return new Credentials(username, password);
        }

        /// <summary>
        /// Whether the authentication scheme is supported.
        /// </summary>
        /// <param name="scheme">The authentication scheme.</param>
        /// <returns> Whether the authentication scheme is supported.</returns>
        public Boolean IsSupported(AuthenticationScheme scheme)
        {
            return scheme == AuthenticationScheme.Kerberos;
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