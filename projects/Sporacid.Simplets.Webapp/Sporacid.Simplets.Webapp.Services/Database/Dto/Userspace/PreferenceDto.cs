namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Userspace
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class PreferenceDto
    {
        [Required]
        [StringLength(50)]
        public String Name { get; set; }

        [Required]
        [StringLength(150)]
        public String Value { get; set; }
    }
}