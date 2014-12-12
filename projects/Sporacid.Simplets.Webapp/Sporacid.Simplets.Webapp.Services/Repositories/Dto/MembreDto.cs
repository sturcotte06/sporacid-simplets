namespace Sporacid.Simplets.Webapp.Services.Repositories.Dto
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class MembreDto
    {
        [Required]
        [RegularExpression("[A-Za-z]{2}[0-9]{5}")]
        public string CodeUniversel { get; set; }

        [Required]
        public ConcentrationDto Concentration { get; set; }

        [Required]
        [StringLength(50)]
        public string Nom { get; set; }

        [Required]
        [StringLength(50)]
        public string Prenom { get; set; }

        [Required]
        [StringLength(250)]
        [RegularExpression("[0-9]{10}")]
        public string Courriel { get; set; }

        [Required]
        [RegularExpression("[0-9]{10}")]
        public string Telephone { get; set; }

        [Required]
        public bool Actif { get; set; }
    }
}