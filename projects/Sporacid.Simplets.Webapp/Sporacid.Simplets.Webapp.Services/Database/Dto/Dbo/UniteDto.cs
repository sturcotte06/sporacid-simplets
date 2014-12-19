namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Dbo
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class UniteDto
    {
        [Required]
        [StringLength(10)]
        public String Code { get; set; }

        [Required]
        [StringLength(10)]
        public String Systeme { get; set; }
    }
}