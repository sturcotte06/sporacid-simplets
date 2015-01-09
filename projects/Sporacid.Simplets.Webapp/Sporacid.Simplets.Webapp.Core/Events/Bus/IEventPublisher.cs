namespace Sporacid.Simplets.Webapp.Core.Events.Bus
{
    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IEventPublisher : IEventPublisher<IEvent>
    {
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IEventPublisher<TEvent>
    {
    }
}