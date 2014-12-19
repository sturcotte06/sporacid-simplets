namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class StatutSuivieDto
    {
        [Required]
        [StringLength(50)]
        public String Code { get; set; }

        [Required]
        [StringLength(150)]
        public String Description { get; set; }
    }
}