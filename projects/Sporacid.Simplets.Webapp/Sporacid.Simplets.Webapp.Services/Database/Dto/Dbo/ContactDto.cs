namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Dbo
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class ContactDto
    {
        [Required]
        public TypeContactDto TypeContact { get; set; }

        [Required]
        [StringLength(50)]
        public String Nom { get; set; }

        [Required]
        [StringLength(50)]
        public String Prenom { get; set; }

        [Required]
        [RegularExpression("[0-9]{10}")]
        public String Telephone { get; set; }

        [Required]
        [StringLength(250)]
        [RegularExpression(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$")]
        public String Courriel { get; set; }
    }
}