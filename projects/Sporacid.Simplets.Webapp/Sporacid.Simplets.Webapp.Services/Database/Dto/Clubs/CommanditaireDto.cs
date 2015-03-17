namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Dbo;
    using Sporacid.Simplets.Webapp.Services.Resources.Validation;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class CommanditaireDto
    {
        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "CommanditaireDto_TypeCommanditaireId_Required")]
        [Range(1, Int32.MaxValue,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "CommanditaireDto_TypeCommanditaireId_Range")]
        public Int32 TypeCommanditaireId { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "CommanditaireDto_Adresse_Required")]
        public AdresseDto Adresse { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "CommanditaireDto_Contact_Required")]
        public ContactDto Contact { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "CommanditaireDto_Nom_Required")]
        [StringLength(100,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "CommanditaireDto_Nom_StringLength")]
        public String Nom { get; set; }

        [StringLength(250,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "CommanditaireDto_Commentaire_StringLength")]
        public String Commentaire { get; set; }
    }
}