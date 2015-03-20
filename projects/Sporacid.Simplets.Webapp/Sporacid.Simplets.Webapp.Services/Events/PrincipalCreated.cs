namespace Sporacid.Simplets.Webapp.Services.Events
{
    using System;
    using Sporacid.Simplets.Webapp.Core.Events.Impl;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class PrincipalCreated : Event<PrincipalCreatedEventArgs>
    {
        public PrincipalCreated(object sender, PrincipalCreatedEventArgs eventArgs) : base(sender, eventArgs)
        {
        }
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class PrincipalCreatedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public PrincipalCreatedEventArgs(string identity)
        {
            this.Identity = identity;
        }

        /// <summary>
        /// The identity of the created principal.
        /// </summary>
        public String Identity { get; private set; }
    }
}