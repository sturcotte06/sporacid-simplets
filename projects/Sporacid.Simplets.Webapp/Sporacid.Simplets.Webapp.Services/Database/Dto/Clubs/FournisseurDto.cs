namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Dbo;
    using Sporacid.Simplets.Webapp.Services.Resources.Validation;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class FournisseurDto
    {
        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "FournisseurDto_TypeFournisseurId_Required")]
        [Range(1, Int32.MaxValue,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "FournisseurDto_TypeFournisseurId_Range")]
        public Int32 TypeFournisseurId { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "FournisseurDto_Nom_Required")]
        [StringLength(100,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "FournisseurDto_Nom_StringLength")]
        public String Nom { get; set; }

        [StringLength(250,
            ErrorMessageResourceType = typeof(ValidationStrings),
            ErrorMessageResourceName = "FournisseurDto_Commentaire_StringLength")]
        public String Commentaire { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "FournisseurDto_Adresse_Required")]
        public AdresseDto Adresse { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "FournisseurDto_Contact_Required")]
        public ContactDto Contact { get; set; }
    }
}