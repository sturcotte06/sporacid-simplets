namespace Sporacid.Simplets.Webapp.Core.Events
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IEventBus<TEvent, TEventArgs> : IDisposable where TEvent : IEvent<TEventArgs>
    {
        /// <summary>
        /// Publishes an event into the bus.
        /// All subscribers to this event will be notified asynchronously.
        /// </summary>
        /// <param name="publisher">The sender of the event.</param>
        /// <param name="eventArgs">The event arguments.</param>
        void Publish(IEventPublisher<TEvent, TEventArgs> publisher, TEventArgs eventArgs);

        /// <summary>
        /// Subscribes to all events occuring in this event bus.
        /// Handlers of the subscriber will be invoked asynchronously.
        /// </summary>
        /// <param name="subscriber">The subscriber to the event.</param>
        void Subscribe(IEventSubscriber<TEvent, TEventArgs> subscriber);
    }
}