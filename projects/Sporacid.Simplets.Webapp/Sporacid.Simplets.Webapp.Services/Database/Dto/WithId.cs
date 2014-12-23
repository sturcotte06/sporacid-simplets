namespace Sporacid.Simplets.Webapp.Services.Database.Dto
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class WithId<TDtoId, TDto>
    {
        public WithId(TDtoId id, TDto dto)
        {
            this.Id = id;
            this.Entity = dto;
        }

        /// <summary>
        /// The dto id.
        /// </summary>
        public TDtoId Id { get; private set; }

        /// <summary>
        /// The dto objec.
        /// </summary>
        public TDto Entity { get; private set; }
    }
}