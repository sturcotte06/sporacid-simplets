namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class SuivieDto
    {
        [Required]
        public Int32 MembreId { get; set; }

        [Required]
        public Int32 StatutSuivieId { get; set; }

        //[Required]
        //public MembreDto Membre { get; set; }

        //[Required]
        //public StatutSuivieDto StatutSuivie { get; set; }

        [Required]
        public DateTime DateSuivie { get; set; }

        [Required]
        [StringLength(250)]
        public String Commentaire { get; set; }
    }
}