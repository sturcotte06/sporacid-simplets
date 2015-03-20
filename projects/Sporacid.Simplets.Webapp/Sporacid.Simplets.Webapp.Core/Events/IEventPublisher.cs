namespace Sporacid.Simplets.Webapp.Core.Events
{
    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IEventPublisher<TEvent, in TEventArgs> where TEvent : IEvent<TEventArgs>
    {
    }
}