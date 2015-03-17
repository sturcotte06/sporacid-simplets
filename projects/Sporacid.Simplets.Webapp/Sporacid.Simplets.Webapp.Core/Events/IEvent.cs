namespace Sporacid.Simplets.Webapp.App.Events
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IEvent<out TEventArgs>
    {
        /// <summary>
        /// The object that sent the event.
        /// </summary>
        Object Sender { get; }

        /// <summary>
        /// The event arguments. Contains a list of info
        /// </summary>
        TEventArgs EventArgs { get; }

        /// <summary>
        /// Sets whether the event has been handled or not.
        /// </summary>
        Boolean Handled { get; set; }
    }
}