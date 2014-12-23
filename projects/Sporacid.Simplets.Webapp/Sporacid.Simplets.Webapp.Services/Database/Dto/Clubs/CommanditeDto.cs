namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class CommanditeDto
    {
        public Int32? FournisseurId { get; set; }
        public Int32? ItemId { get; set; }

        [Required]
        [Range(0, 1000000)]
        public Double Valeur { get; set; }

        [Required]
        [StringLength(50)]
        public String Nature { get; set; }
    }
}