namespace Sporacid.Simplets.Webapp.Services.Events.Subscribers
{
    using Sporacid.Simplets.Webapp.Core.Events;
    using Sporacid.Simplets.Webapp.Services.Services.Security.Administration;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class ContextCreatedSubscriber : IEventSubscriber<ContextCreated, ContextCreatedEventArgs>
    {
        private readonly IContextAdministrationService contextAdministrationService;

        public ContextCreatedSubscriber(IEventBus<ContextCreated, ContextCreatedEventArgs> contextCreatedEventBus, IContextAdministrationService contextAdministrationService)
        {
            this.contextAdministrationService = contextAdministrationService;
            contextCreatedEventBus.Subscribe(this);
        }

        /// <summary>
        /// Handles the event.
        /// This method will be called asynchronously when events of the generic type occur.
        /// </summary>
        /// <param name="event">The event that occured.</param>
        public void Handle(ContextCreated @event)
        {
            // Bind administrator role on the owner of the context.
            this.contextAdministrationService.BindRoleToPrincipal(
                @event.EventArgs.Context, SecurityConfig.Role.Administrateur.ToString(), @event.EventArgs.Owner);
        }
    }
}