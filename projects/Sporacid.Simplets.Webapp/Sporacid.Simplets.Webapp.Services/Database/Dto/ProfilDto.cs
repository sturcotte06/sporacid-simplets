namespace Sporacid.Simplets.Webapp.Services.Database.Dto
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class ProfilDto
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Nom { get; set; }

        [Required]
        [StringLength(50)]
        public string Prenom { get; set; }

        [Required]
        [StringLength(250)]
        [RegularExpression(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$")]
        public string Courriel { get; set; }

        [Required]
        [RegularExpression("[0-9]{10}")]
        public string Telephone { get; set; }
    }
}