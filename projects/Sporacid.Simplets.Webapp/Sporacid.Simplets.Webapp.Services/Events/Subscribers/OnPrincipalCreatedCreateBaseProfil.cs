namespace Sporacid.Simplets.Webapp.Services.Events.Subscribers
{
    using Sporacid.Simplets.Webapp.Core.Events;
    using Sporacid.Simplets.Webapp.Services.Services.Security.Administration;
    using Sporacid.Simplets.Webapp.Services.Services.Userspace.Administration;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class OnPrincipalCreatedCreateBaseProfil : IEventSubscriber<PrincipalCreated, PrincipalCreatedEventArgs>
    {
        private readonly IContextAdministrationService contextAdministrationService;
        private readonly IProfilAdministrationService profilAdministrationService;

        public OnPrincipalCreatedCreateBaseProfil(IContextAdministrationService contextAdministrationService, IProfilAdministrationService profilAdministrationService)
        {
            this.contextAdministrationService = contextAdministrationService;
            this.profilAdministrationService = profilAdministrationService;
        }

        /// <summary>
        /// Handles the event.
        /// This method will be called asynchronously when events of the generic type occur.
        /// </summary>
        /// <param name="event">The event that occured.</param>
        public void Handle(PrincipalCreated @event)
        {
            var identity = @event.EventArgs.Identity;

            // Create the new personnal context of this principal. The principal has full access over its context.
            this.contextAdministrationService.Create(identity, identity);
            this.contextAdministrationService.RemoveAllClaimsFromPrincipal(identity, identity);
            this.contextAdministrationService.BindRoleToPrincipal(identity, SecurityConfig.Role.Administrateur.ToString(), identity);

            // Create the base profil for the new principal.
            this.profilAdministrationService.CreateBaseProfil(identity);
        }
    }
}