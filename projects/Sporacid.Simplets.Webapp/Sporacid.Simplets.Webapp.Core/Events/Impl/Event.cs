﻿namespace Sporacid.Simplets.Webapp.App.Events.Impl
{
    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class Event<TEventArgs> : IEvent<TEventArgs>
    {
        public Event(object sender, TEventArgs eventArgs)
        {
            this.Sender = sender;
            this.EventArgs = eventArgs;
            this.Handled = false;
        }

        /// <summary>
        /// The object that sent the event.
        /// </summary>
        public object Sender { get; private set; }

        /// <summary>
        /// The event arguments. Contains a list of info
        /// </summary>
        public TEventArgs EventArgs { get; private set; }

        /// <summary>
        /// Sets whether the event has been handled or not.
        /// </summary>
        public bool Handled { get; set; }
    }
}