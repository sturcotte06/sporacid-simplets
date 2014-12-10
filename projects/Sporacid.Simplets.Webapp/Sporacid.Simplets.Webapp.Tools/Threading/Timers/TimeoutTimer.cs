namespace Sporacid.Simplets.Webapp.Tools.Threading.Timers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Sporacid.Simplets.Webapp.Tools.Events;

    /// <summary>
    /// Simple ITimer implementation to handle timeouts.
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    public class TimeoutTimer : ITimer
    {
        /// <summary>
        /// The timer thread.
        /// </summary>
        private readonly Thread timerThread;

        /// <summary>
        /// Reset event so we can notify the timer thread when to shut down
        /// </summary>
        private readonly ManualResetEvent wasStoppedEvent;

        /// <summary>
        /// Whether Dispose() was called on this object or not.
        /// </summary>
        private volatile bool isDisposed;

        /// <summary>
        /// Whether Start() was called on this timer or not.
        /// </summary>
        private volatile bool isStarted;

        /// <summary>
        /// Whether Start(), then Stop() were called on this timer or not.
        /// The timer is not considered stopped if it was never started.
        /// </summary>
        private volatile bool isStopped;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="timeSpanBeforeTimeout">The time span before timeout occurs.</param>
        /// <param name="onTimeoutArguments">The arguments that will be passed to the event handlers on timeout.</param>
        public TimeoutTimer(TimeSpan timeSpanBeforeTimeout, params object[] onTimeoutArguments)
        {
            this.TimeSpanBeforeTimeout = timeSpanBeforeTimeout;
            this.wasStoppedEvent = new ManualResetEvent(false);
            this.timerThread = new Thread(() =>
            {
                // Wait until the Stop() method is called, or the wait times out
                if (this.wasStoppedEvent.WaitOne(this.TimeSpanBeforeTimeout))
                {
                    return;
                }

                // WaitOne() has returned false. This means Stop() was most likely not called.
                // Check to be sure.
                if (this.isStopped)
                {
                    return;
                }

                // Timeout occured.
                this.TimeReached(this, new GenericEventArgs<IEnumerable<object>>(onTimeoutArguments));
                this.isStopped = true;
            });
        }

        /// <summary>
        /// Performs application-defined tasks associated with 
        /// freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this.IsStarted && !this.IsStopped)
            {
                // Timer is running.
                throw new InvalidOperationException("Stop the timer before disposing of it.");
            }

            this.isDisposed = true;

            if (this.wasStoppedEvent != null)
                this.wasStoppedEvent.Dispose();
        }

        /// <summary>
        /// Whether Start() was called on this timer or not.
        /// </summary>
        public bool IsStarted
        {
            get { return this.isStarted; }
        }

        /// <summary>
        /// Whether Start(), then Stop() were called on this timer or not.
        /// The timer is not considered stopped if it was never started.
        /// </summary>
        public bool IsStopped
        {
            get { return this.isStopped; }
        }

        /// <summary>
        /// The time span before timeout occurs.
        /// </summary>
        public TimeSpan TimeSpanBeforeTimeout { get; private set; }

        /// <summary>
        /// Event triggered when the timer's time is reached.
        /// </summary>
        public event TimeReachedEventHandler TimeReached;

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void Start()
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (this.isStarted)
            {
                throw new InvalidOperationException("The timer is already started.");
            }

            if (this.isStopped)
            {
                throw new InvalidOperationException("The timer was previously stopped and does not support resume.");
            }

            this.isStarted = true;
            this.timerThread.Start();
        }

        /// <summary>
        /// Stops the timer. A stopped timer cannot be restarted.
        /// </summary>
        public void Stop()
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (!this.isStarted)
            {
                throw new InvalidOperationException("The timer is not started.");
            }

            if (this.isStopped)
            {
                throw new InvalidOperationException("The timer is already stopped.");
            }

            this.isStopped = true;

            // Set the was stopped event so the time thread can join.
            this.wasStoppedEvent.Set();

            // Join the timer thread.
            this.timerThread.Join();
        }

        /// <summary>
        /// Starts a new timer.
        /// </summary>
        /// <param name="timeSpanBeforeTimeout">The time span before timeout occurs.</param>
        /// <param name="onTimeout">The event handler when the timeout occurs.</param>
        /// <param name="onTimeoutArguments">The arguments that will be passed to the event handlers on timeout.</param>
        /// <returns>A running timer that will trigger the onTimeout method when the time span has been reached.</returns>
        public static ITimer StartNew(TimeSpan timeSpanBeforeTimeout, TimeReachedEventHandler onTimeout, params object[] onTimeoutArguments)
        {
            if (onTimeout == null)
            {
                throw new ArgumentNullException("onTimeout");
            }

            if (onTimeoutArguments == null)
            {
                throw new ArgumentNullException("onTimeoutArguments");
            }

            var timer = new TimeoutTimer(timeSpanBeforeTimeout, onTimeoutArguments);
            timer.TimeReached += onTimeout;
            timer.Start();

            return timer;
        }
    }
}