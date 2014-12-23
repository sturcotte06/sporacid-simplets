namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Dbo;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class ItemDto
    {
        [Required]
        public Int32 UniteId { get; set; }

        [Required]
        [StringLength(250)]
        public String Description { get; set; }

        [StringLength(20)]
        public String CodeClub { get; set; }

        [StringLength(20)]
        public Double QuantiteCourante { get; set; }

        [StringLength(20)]
        public Double QuantiteMin { get; set; }

        [StringLength(20)]
        public Double QuantiteMax { get; set; }
    }
}