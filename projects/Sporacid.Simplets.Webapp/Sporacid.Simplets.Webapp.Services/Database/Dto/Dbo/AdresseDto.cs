namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Dbo
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Sporacid.Simplets.Webapp.Services.Resources.Validation;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class AdresseDto
    {
        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "AdresseDto_NoCivique_Required")]
        [Range(1, 100000,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "AdresseDto_NoCivique_Range")]
        public Int32 NoCivique { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "AdresseDto_Rue_Required")]
        [StringLength(50,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "AdresseDto_Rue_StringLength")]
        public String Rue { get; set; }

        [StringLength(10,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "AdresseDto_Appartement_StringLength")]
        public String Appartement { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "AdresseDto_Ville_Required")]
        [StringLength(150,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "AdresseDto_Ville_StringLength")]
        public String Ville { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "AdresseDto_CodePostal_Required")]
        [StringLength(16,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "AdresseDto_CodePostal_StringLength")]
        public String CodePostal { get; set; }
    }
}