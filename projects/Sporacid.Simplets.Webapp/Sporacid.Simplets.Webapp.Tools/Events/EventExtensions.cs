namespace Sporacid.Simplets.Webapp.Tools.Events
{
    using System;

    /// <summary>
    /// Extension method library for event related objects.
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    public static class EventExtensions
    {
        /// <summary>
        /// Safely invoke and event handler.
        /// </summary>
        /// <typeparam name="TArgs">The type of the event args.</typeparam>
        /// <param name="eventHandler">The event handler delegate.</param>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="eventArgs">The event args.</param>
        public static void Raise<TArgs>(this EventHandler<TArgs> eventHandler, object sender, TArgs eventArgs) where TArgs : System.EventArgs
        {
            if (eventHandler != null)
            {
                eventHandler(sender, eventArgs);
            }
        }
    }
}