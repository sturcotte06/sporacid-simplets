namespace Sporacid.Simplets.Webapp.Core.Events.Impl
{
    using System;
    using System.Collections.Generic;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Events;
    using Sporacid.Simplets.Webapp.Tools;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class SyncTransientEventBus<TEvent, TEventArgs> : IEventBus<TEvent, TEventArgs> where TEvent : IEvent<TEventArgs>
    {
        private readonly IEnumerable<IEventSubscriber<TEvent, TEventArgs>> subscribers;

        public SyncTransientEventBus(IEnumerable<IEventSubscriber<TEvent, TEventArgs>> subscribers)
        {
            this.subscribers = subscribers;
        }

        /// <summary>
        /// Publishes an event into the bus.
        /// All subscribers to this event will be notified asynchronously.
        /// </summary>
        /// <param name="publisher">The sender of the event.</param>
        /// <param name="eventArgs">The event arguments.</param>
        public void Publish(IEventPublisher<TEvent, TEventArgs> publisher, TEventArgs eventArgs)
        {
            // Create a new instance of the event.
            var @event = default(TEvent);
            Snippets.TryCatch<Exception>(
                () => @event = (TEvent) Activator.CreateInstance(typeof (TEvent), publisher, eventArgs),
                ex => { throw new PublishException("TODO", ex); });

            // Notify each subscribers until the event is handled.
            foreach (var subscriber in this.subscribers)
            {
                subscriber.Handle(@event);
                if (@event.Handled)
                {
                    // Event is handled, do not notify other subscribers.
                    break;
                }
            }
        }

        /// <summary>
        /// Subscribes to all events occuring in this event bus.
        /// Handlers of the subscriber will be invoked asynchronously.
        /// </summary>
        /// <param name="subscriber">The subscriber to the event.</param>
        public void Subscribe(IEventSubscriber<TEvent, TEventArgs> subscriber)
        {
            // Not implemented for real. This is a transient implementation. 
            // Subscribers are sent through the constructor and exist only for a couple of events.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }
    }
}