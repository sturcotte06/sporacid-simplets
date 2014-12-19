namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class ClubDto
    {
        [Required]
        [StringLength(50)]
        public String Nom { get; set; }

        [Required]
        [StringLength(250)]
        public String Description { get; set; }
    }
}