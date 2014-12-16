namespace Sporacid.Simplets.Webapp.Services.Database.Dto
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class ConcentrationDto
    {
        [Required]
        [StringLength(10)]
        public string Acronyme { get; set; }

        [Required]
        [StringLength(150)]
        public string Description { get; set; }
    }
}