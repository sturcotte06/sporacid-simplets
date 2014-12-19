namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Dbo;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class FournisseurDto
    {
        [Required]
        [StringLength(250)]
        public String Nom { get; set; }

        [Required]
        public AdresseDto Adresse { get; set; }

        [Required]
        public ContactDto Contact { get; set; }
    }
}