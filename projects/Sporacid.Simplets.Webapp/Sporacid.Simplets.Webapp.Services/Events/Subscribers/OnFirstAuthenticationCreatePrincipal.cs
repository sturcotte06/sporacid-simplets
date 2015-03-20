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

        public OnFirstAuthenticationCreatePrincipal(IPrincipalAdministrationService principalAdministrationService)
        {
            this.principalAdministrationService = principalAdministrationService;
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
            if (!this.principalAdministrationService.Exists(identity))
            {
                // User logged in for first time. Create its principal.
                this.principalAdministrationService.Create(identity);
            }
        }
    }
}