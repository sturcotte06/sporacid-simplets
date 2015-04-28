namespace Sporacid.Simplets.Webapp.Services.Events
{
    using System;
    using Sporacid.Simplets.Webapp.Core.Events.Impl;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class ClubCreated : Event<ClubCreatedEventArgs>
    {
        public ClubCreated(Object sender, ClubCreatedEventArgs eventArgs)
            : base(sender, eventArgs)
        {
        }
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class ClubCreatedEventArgs
    {
        public ClubCreatedEventArgs(String clubName, String owner)
        {
            this.ClubName = clubName;
            this.Owner = owner;
        }

        /// <summary>
        /// The created context name.
        /// </summary>
        public String ClubName { get; private set; }

        /// <summary>
        /// The owner of the context.
        /// </summary>
        public String Owner { get; private set; }
    }
}