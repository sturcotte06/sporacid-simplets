namespace Sporacid.Simplets.Webapp.App.Events.Impl
{
    using Sporacid.Simplets.Webapp.Tools.Collections.Concurrent;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class EventBus<TEvent, TEventArgs> : IEventBus<TEvent, TEventArgs> where TEvent : IEvent<TEventArgs>
    {
        private readonly IBlockingQueue<> 

        /// <summary>
        /// Publishes an event into the bus. 
        /// All subscribers to this event will be notified asynchronously.
        /// </summary>
        /// <param name="publisher">The sender of the event.</param>
        /// <param name="eventArgs">The event arguments.</param>
        public void Publish(IEventPublisher<TEvent, TEventArgs> publisher, TEventArgs eventArgs)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Subscribes to all events occuring in this event bus.
        /// Handlers of the subscriber will be invoked asynchronously.
        /// </summary>
        /// <param name="subscriber">The subscriber to the event.</param>
        public void Subscribe(IEventSubscriber<TEvent, TEventArgs> subscriber)
        {
            throw new System.NotImplementedException();
        }
    }
}