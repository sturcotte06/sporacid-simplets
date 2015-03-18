namespace Sporacid.Simplets.Webapp.Core.Events
{
    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IEventSubscriber<in TEvent, in TEventArgs> where TEvent : IEvent<TEventArgs>
    {
        /// <summary>
        /// Handles the event.
        /// This method will be called asynchronously when events of the generic type occur.
        /// </summary>
        /// <param name="event">The event that occured.</param>
        void Handle(TEvent @event);
    }
}