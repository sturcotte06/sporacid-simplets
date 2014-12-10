namespace Sporacid.Simplets.Webapp.Tools.Threading.Timers
{
    using System;
    using System.Collections.Generic;
    using Sporacid.Simplets.Webapp.Tools.Events;

    /// <summary>
    /// Interface for all timers.
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    public interface ITimer : IDisposable
    {
        /// <summary>
        /// Whether Start() was called on this timer or not.
        /// </summary>
        bool IsStarted { get; }

        /// <summary>
        /// Whether Start(), then Stop() were called on this timer or not.
        /// The timer is not considered stopped if it was never started.
        /// </summary>
        bool IsStopped { get; }

        /// <summary>
        /// The time span before timeout occurs.
        /// </summary>
        TimeSpan TimeSpanBeforeTimeout { get; }

        /// <summary>
        /// Event triggered when the timer's time is reached.
        /// </summary>
        event TimeReachedEventHandler TimeReached;

        /// <summary>
        /// Starts the timer.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the timer. A stopped timer cannot be restarted.
        /// </summary>
        void Stop();
    }

    /// <summary>
    /// Event handler for the TimeReached event of a Timer.
    /// </summary>
    /// <param name="sender">The timer that triggered the event.</param>
    /// <param name="eventArgs">The event args that contains the event context.</param>
    public delegate void TimeReachedEventHandler(object sender, GenericEventArgs<IEnumerable<object>> eventArgs);
}