namespace Sporacid.Simplets.Webapp.Core.Events
{
    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public static class EventExtensions
    {
        /// <summary>
        /// Publishes an event in the given event bus.
        /// </summary>
        /// <typeparam name="TEvent">Type of the event.</typeparam>
        /// <typeparam name="TEventArgs">Type of the event args></typeparam>
        /// <param name="eventBus">The event bus on which to publish.</param>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="eventArgs">The event args.</param>
        public static void Publish<TEvent, TEventArgs>(this IEventPublisher<TEvent, TEventArgs> sender, IEventBus<TEvent, TEventArgs> eventBus, TEventArgs eventArgs)
            where TEvent : IEvent<TEventArgs>
        {
            eventBus.Publish(sender, eventArgs);
        }
    }
}