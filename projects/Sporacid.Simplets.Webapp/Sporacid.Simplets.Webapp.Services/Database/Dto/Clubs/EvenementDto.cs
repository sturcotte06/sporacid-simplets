namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Sporacid.Simplets.Webapp.Services.Resources.Validation;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class EvenementDto
    {
        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "EvenementDto_Nom_Required")]
        [StringLength(50,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "EvenementDto_Nom_StringLength")]
        public String Nom { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "EvenementDto_Description_Required")]
        [StringLength(250,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "EvenementDto_Description_StringLength")]
        public String Description { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "EvenementDto_DateDebut_Required")]
        public DateTime DateDebut { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "EvenementDto_DateFin_Required")]
        public DateTime DateFin { get; set; }
    }
}