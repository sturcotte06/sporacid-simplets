namespace Sporacid.Simplets.Webapp.Services.Events.Subscribers
{
    using Sporacid.Simplets.Webapp.Core.Events;
    using Sporacid.Simplets.Webapp.Core.Security.Events;
    using Sporacid.Simplets.Webapp.Services.Services.Security.Administration;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class OnFirstAuthenticationCreatePrincipal : IEventSubscriber<PrincipalAuthenticated, PrincipalAuthenticatedEventArgs>
    {
        private readonly IPrincipalAdministrationService principalAdministrationService;
        private readonly IContextAdministrationService contextAdministrationService;

        public OnFirstAuthenticationCreatePrincipal(
            IPrincipalAdministrationService principalAdministrationService,
            IContextAdministrationService contextAdministrationService)
        {
            this.principalAdministrationService = principalAdministrationService;
            this.contextAdministrationService = contextAdministrationService;
        }

        /// <summary>
        /// Handles the event.
        /// This method will be called asynchronously when events of the generic type occur.
        /// </summary>
        /// <param name="event">The event that occured.</param>
        public void Handle(PrincipalAuthenticated @event)
        {
            // Check if the user is logged in for the first time.
            var principal = @event.EventArgs.Principal;
            var identity = principal.Identity.Name;

            // If not, no-op.
            if (this.principalAdministrationService.Exists(identity))
                return;

            // User logged in for first time. Create its principal.
            this.principalAdministrationService.Create(identity);

            // Merge readonly role on the system context with the current claims of the user.
            this.contextAdministrationService.MergeClaimsOfPrincipalWithRole(SecurityConfig.SystemContext, SecurityConfig.Role.Lecteur.ToString(), identity);
        }
    }
}