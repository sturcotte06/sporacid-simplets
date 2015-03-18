namespace Sporacid.Simplets.Webapp.Core.Events.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Events;
    using Sporacid.Simplets.Webapp.Tools;
    using Sporacid.Simplets.Webapp.Tools.Collections.Concurrent;
    using Sporacid.Simplets.Webapp.Tools.Threading.Pooling;
    using Void = Sporacid.Simplets.Webapp.Tools.Threading.Void;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class EventBus<TEvent, TEventArgs> : IEventBus<TEvent, TEventArgs> where TEvent : IEvent<TEventArgs>
    {
        private readonly IBlockingQueue<TEvent> eventQueue;
        private readonly ReaderWriterLock @lock = new ReaderWriterLock();
        private readonly IThreadPool subscriberPool;
        private readonly List<IEventSubscriber<TEvent, TEventArgs>> subscribers;

        public EventBus(IBlockingQueue<TEvent> eventQueue, IThreadPool subscriberPool)
        {
            this.eventQueue = eventQueue;
            this.subscriberPool = subscriberPool;
            this.subscribers = new List<IEventSubscriber<TEvent, TEventArgs>>();

            // Create a task foreach available thread in the thread pool.
            // Those tasks will poll the event queue and notify all subscribers every time the event occurs.
            for (var iThread = 0; iThread < this.subscriberPool.Configuration.ThreadCount; iThread++)
            {
                this.subscriberPool.QueueWorkItem<Void>(this.Notify, new WorkItemOptions<Void>());
            }
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

            // Enqueue the event. Could be asynchronous if problematic.
            this.eventQueue.Enqueue(@event);
        }

        /// <summary>
        /// Subscribes to all events occuring in this event bus.
        /// Handlers of the subscriber will be invoked asynchronously.
        /// </summary>
        /// <param name="subscriber">The subscriber to the event.</param>
        public void Subscribe(IEventSubscriber<TEvent, TEventArgs> subscriber)
        {
            this.@lock.AcquireWriterLock(TimeSpan.FromSeconds(30));
            try
            {
                this.subscribers.Add(subscriber);
            }
            finally
            {
                this.@lock.ReleaseWriterLock();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.eventQueue.Close();
            this.subscriberPool.Shutdown();
            this.subscriberPool.Dispose();
        }

        /// <summary>
        /// Dequeues events and notifies subscribers when they occur.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token if </param>
        private Void Notify(WorkItemCancellationToken cancellationToken)
        {
            TEvent @event;
            while (!cancellationToken.IsCancellationRequested && this.eventQueue.TryDequeue(out @event))
            {
                var acquired = false;
                while (!acquired)
                {
                    try
                    {
                        this.@lock.AcquireReaderLock(TimeSpan.FromSeconds(30));
                        acquired = true;
                    }
                    catch (ApplicationException)
                    {
                        // Do nothing and retry. We need that lock.
                    }
                }

                try
                {
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
                finally
                {
                    this.@lock.ReleaseReaderLock();
                }
            }

            return Void.Value;
        }
    }
}