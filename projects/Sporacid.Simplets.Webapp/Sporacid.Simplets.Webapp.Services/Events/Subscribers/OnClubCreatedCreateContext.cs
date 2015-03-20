namespace Sporacid.Simplets.Webapp.Services.Events.Subscribers
{
    using Sporacid.Simplets.Webapp.Core.Events;
    using Sporacid.Simplets.Webapp.Services.Services.Security.Administration;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class OnClubCreatedCreateContext : IEventSubscriber<ClubCreated, ClubCreatedEventArgs>
    {
        private readonly IContextAdministrationService contextAdministrationService;

        public OnClubCreatedCreateContext(IContextAdministrationService contextAdministrationService)
        {
            this.contextAdministrationService = contextAdministrationService;
        }

        /// <summary>
        /// Handles the event.
        /// This method will be called asynchronously when events of the generic type occur.
        /// </summary>
        /// <param name="event">The event that occured.</param>
        public void Handle(ClubCreated @event)
        {
            // Create the security context for the club.
            this.contextAdministrationService.Create(@event.EventArgs.ClubName, @event.EventArgs.Owner);
        }
    }
}