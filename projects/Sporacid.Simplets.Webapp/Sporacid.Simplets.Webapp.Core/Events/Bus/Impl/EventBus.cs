namespace Sporacid.Simplets.Webapp.Core.Events.Bus.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Sporacid.Simplets.Webapp.Tools.Collections.Concurrent;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class EventBus<TEvent> : IEventBus<TEvent>, IDisposable where TEvent : IEvent
    {
        private readonly IBlockingQueue<PublishedEvent> events = new BlockingQueue<PublishedEvent>();
        private readonly ManualResetEventSlim publisherRunning = new ManualResetEventSlim();
        private readonly Task publisherTask;
        private readonly CancellationTokenSource publisherTaskCancelToken = new CancellationTokenSource();
        private readonly ReaderWriterLock @stateLock = new ReaderWriterLock();
        private readonly IList<IEventSubscriber> subscribers = new List<IEventSubscriber>();

        public EventBus()
        {
            this.publisherTask = Task.Factory.StartNew(() =>
            {
                // Run until dispose is called.
                while (!this.publisherTaskCancelToken.IsCancellationRequested)
                {
                    this.publisherRunning.Wait();

                    // Dequeue one event.
                    PublishedEvent publishedEvent;
                    while (this.events.TryDequeue(out publishedEvent))
                    {
                        this.stateLock.AcquireReaderLock(TimeSpan.FromSeconds(30));

                        try
                        {
                            // Trigger all subscribers handlers.
                            foreach (var subscriber in this.subscribers)
                            {
                                subscriber.OnEvent(publishedEvent.Publisher, publishedEvent.Event);
                            }
                        }
                        finally
                        {
                            this.stateLock.ReleaseReaderLock();
                        }
                    }

                    // No more event, sleep until an event is raised.
                    this.publisherRunning.Reset();
                }
            }, this.publisherTaskCancelToken.Token);
            this.publisherTask.Start();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.publisherTaskCancelToken.Cancel();
            this.publisherTask.Dispose();
        }

        /// <summary>
        /// Publishes an event on the event bus.
        /// This action is guaranteed to be thread-safe.
        /// </summary>
        /// <param name="publisher">The publisher of the event.</param>
        /// <param name="event">The event.</param>
        public void Publish(IEventPublisher publisher, TEvent @event)
        {
            this.stateLock.AcquireReaderLock(TimeSpan.FromSeconds(30));

            try
            {
                this.events.Enqueue(new PublishedEvent(publisher, @event));
                this.publisherRunning.Set();
            }
            finally
            {
                this.stateLock.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// Subscribes to an event type.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        public void Subscribe(IEventSubscriber subscriber)
        {
            this.stateLock.AcquireWriterLock(TimeSpan.FromSeconds(30));

            try
            {
                this.subscribers.Add(subscriber);
            }
            finally
            {
                this.stateLock.ReleaseWriterLock();
            }
        }

        /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
        /// <version>1.9.0</version>
        private class PublishedEvent
        {
            public PublishedEvent(IEventPublisher publisher, TEvent @event)
            {
                this.Event = @event;
                this.Publisher = publisher;
            }

            public IEventPublisher Publisher { get; private set; }
            public TEvent Event { get; private set; }
        }
    }
}