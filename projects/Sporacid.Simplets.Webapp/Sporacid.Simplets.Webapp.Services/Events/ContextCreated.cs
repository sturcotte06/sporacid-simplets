namespace Sporacid.Simplets.Webapp.Services.Events
{
    using System;
    using Sporacid.Simplets.Webapp.Core.Events.Impl;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class ContextCreated : Event<ContextCreatedEventArgs>
    {
        public ContextCreated(object sender, ContextCreatedEventArgs eventArgs)
            : base(sender, eventArgs)
        {
        }
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class ContextCreatedEventArgs
    {
        public ContextCreatedEventArgs(String context, String owner)
        {
            this.Context = context;
            this.Owner = owner;
        }

        /// <summary>
        /// The created context name.
        /// </summary>
        public String Context { get; private set; }

        /// <summary>
        /// The owner of the context.
        /// </summary>
        public String Owner { get; private set; }
    }
}