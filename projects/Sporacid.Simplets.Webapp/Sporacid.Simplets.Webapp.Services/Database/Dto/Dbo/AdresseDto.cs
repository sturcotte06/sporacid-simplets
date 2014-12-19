namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Dbo
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class AdresseDto
    {
        [Required]
        public Int32 NoCivique { get; set; }

        [Required]
        [StringLength(50)]
        public String Rue { get; set; }

        [Required]
        [StringLength(10)]
        public String Appartement { get; set; }

        [Required]
        [StringLength(150)]
        public String Ville { get; set; }

        [Required]
        [StringLength(16)]
        public String CodePostal { get; set; }
    }
}