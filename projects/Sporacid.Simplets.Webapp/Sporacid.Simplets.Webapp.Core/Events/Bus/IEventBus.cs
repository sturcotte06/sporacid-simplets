namespace Sporacid.Simplets.Webapp.Core.Events.Bus
{
    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IEventBus<in TEvent> where TEvent : IEvent
    {
        /// <summary>
        /// Publishes an event on the event bus.
        /// This action is guaranteed to be thread-safe.
        /// </summary>
        /// <param name="publisher">The publisher of the event.</param>
        /// <param name="event">The event.</param>
        void Publish(IEventPublisher publisher, TEvent @event);

        /// <summary>
        /// Subscribes to an event type.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        void Subscribe(IEventSubscriber subscriber);
    }
}