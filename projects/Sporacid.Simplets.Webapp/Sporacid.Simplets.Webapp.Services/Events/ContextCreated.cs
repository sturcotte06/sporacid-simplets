namespace Sporacid.Simplets.Webapp.Services.Events
{
    using System;
    using Sporacid.Simplets.Webapp.Core.Events;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class ContextCreated : IEvent
    {
        public ContextCreated(Int32 contextId)
        {
            this.ContextId = contextId;
        }

        /// <summary>
        /// The created context id.
        /// </summary>
        public Int32 ContextId { get; private set; }
    }
}