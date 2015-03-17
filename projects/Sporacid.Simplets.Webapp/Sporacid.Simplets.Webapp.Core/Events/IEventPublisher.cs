namespace Sporacid.Simplets.Webapp.App.Events
{
    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IEventPublisher<TEvent, in TEventArgs> where TEvent : IEvent<TEventArgs>
    {
        /// <summary>
        /// Publishes an event in the given event bus.
        /// </summary>
        /// <param name="eventArgs">The event args of the event.</param>
        void Publish(TEventArgs eventArgs);
    }
}