namespace Sporacid.Simplets.Webapp.Services.Events
{
    using System;
    using Sporacid.Simplets.Webapp.Core.Events;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class ClubCreated : IEvent
    {
        public ClubCreated(Int32 clubId, String clubName)
        {
            this.ClubId = clubId;
            this.ClubName = clubName;
        }

        /// <summary>
        /// The created context id.
        /// </summary>
        public Int32 ClubId { get; private set; }

        /// <summary>
        /// The created context id.
        /// </summary>
        public String ClubName { get; private set; }
    }
}