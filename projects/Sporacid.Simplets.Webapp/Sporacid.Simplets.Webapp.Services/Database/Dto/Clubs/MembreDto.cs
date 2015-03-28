namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Userspace;
    using Sporacid.Simplets.Webapp.Services.Resources.Validation;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class MembreDto
    {
        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "MembreDto_Titre_Required")]
        [StringLength(50,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "MembreDto_Titre_StringLength")]
        public String Titre { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "MembreDto_DateDebut_Required")]
        public DateTime DateDebut { get; set; }

        public DateTime DateFin { get; set; }

        [Required(ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "MembreDto_Actif_Required")]
        public Boolean Actif { get; set; }

        public ProfilPublicDto ProfilPublic { get; set; }
    }
}