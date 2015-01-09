namespace Sporacid.Simplets.Webapp.Core.Events.Bus
{

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IEventSubscriber : IEventSubscriber<IEvent>
    {
        
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IEventSubscriber<TEvent> where TEvent : IEvent
    {
        /// <summary>
        /// The action to take when the event occurs.
        /// </summary>
        /// <param name="publisher">The publisher that raised the event.</param>
        /// <param name="event">The event.</param>
        void OnEvent(IEventPublisher<TEvent> publisher, TEvent @event);
    }
}