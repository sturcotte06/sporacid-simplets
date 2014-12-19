namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Userspace
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class ProfilAvanceDto
    {
        [StringLength(12)]
        public String CodePermanent { get; set; }

        public DateTime? DateNaissance { get; set; }
        
        [RegularExpression("[0-9]{10}")]
        public String Telephone { get; set; }

        [StringLength(250)]
        [RegularExpression(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$")]
        public String Courriel { get; set; }

        [Required]
        public bool Public { get; set; }
    }
}