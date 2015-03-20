namespace Sporacid.Simplets.Webapp.Core.Security.Events.Subscribers
{
    using System.Security.Principal;
    using Sporacid.Simplets.Webapp.Core.Events;
    using Sporacid.Simplets.Webapp.Core.Security.Authentication.Tokens;
    using Sporacid.Simplets.Webapp.Tools.Collections.Caches;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class OnPrincipalAuthenticatedCacheToken : IEventSubscriber<PrincipalAuthenticated, PrincipalAuthenticatedEventArgs>
    {
        // private readonly HttpResponseBase httpResponse;
        private readonly ICache<IPrincipal, IToken> principalCache;
        private readonly ICache<IToken, IPrincipal> tokenCache;

        public OnPrincipalAuthenticatedCacheToken(ICache<IToken, IPrincipal> tokenCache, ICache<IPrincipal, IToken> principalCache)
        {
            this.principalCache = principalCache;
            this.tokenCache = tokenCache;
        }

        /// <summary>
        /// Handles the event.
        /// This method will be called asynchronously when events of the generic type occur.
        /// </summary>
        /// <param name="event">The event that occured.</param>
        public void Handle(PrincipalAuthenticated @event)
        {
            var principal = @event.EventArgs.Principal;
            var token = @event.EventArgs.Token;

            // If the principal previously had a token, invalidate it, and recache the new token.
            IToken staleToken = null;
            if (this.principalCache.TryWithValueDo(principal, tok => staleToken = tok))
            {
                // Uncache the old token and unbind the token and the principal.
                this.tokenCache.Remove(staleToken);
                this.principalCache.Remove(principal);
            }

            // Cache the new token and bind it to the principal.
            this.principalCache.Put(principal, token);
            this.tokenCache.Put(token, principal);
        }
    }
}