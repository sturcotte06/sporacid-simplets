namespace Sporacid.Simplets.Webapp.Services.Database.Dto
{
    using System;
    using PostSharp.Patterns.Contracts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class ClubDto
    {
        [Required]
        public String Nom { get; set; }
        [Required]
        public String Description { get; set; }
    }
}