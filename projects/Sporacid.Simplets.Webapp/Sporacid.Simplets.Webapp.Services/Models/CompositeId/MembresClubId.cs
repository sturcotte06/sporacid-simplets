namespace Sporacid.Simplets.Webapp.Services.Models.CompositeId
{
    using System;
    using Sporacid.Simplets.Webapp.Core.Repositories;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class MembresClubId : ICompositeId
    {
        public Int32 MembreId { get; set; }
        public Int32 ClubId { get; set; }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the
        /// current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value
        /// Meaning Less than zero This instance precedes <paramref name="obj" /> in the sort order. Zero This instance occurs in
        /// the same position in the sort order as <paramref name="obj" />. Greater than zero This instance follows
        /// <paramref name="obj" /> in the sort order.
        /// </returns>
        /// <param name="obj">An object to compare with this instance. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="obj" /> is not the same type as this instance. </exception>
        public int CompareTo(object obj)
        {
            var membresClubId = obj as MembresClubId;
            if (membresClubId == null)
            {
                throw new ArgumentException("obj");
            }

            if (this.MembreId == membresClubId.MembreId)
            {
                if (this.ClubId == membresClubId.ClubId)
                {
                    return 0;
                }

                if (this.ClubId < membresClubId.ClubId)
                {
                    return -1;
                }

                return 1;
            }

            if (this.MembreId < membresClubId.MembreId)
            {
                return -1;
            }

            return 1;
        }
    }
}