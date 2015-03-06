namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Dbo
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Sporacid.Simplets.Webapp.Services.Resources.Validation;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class ContactDto
    {
        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ContactDto_TypeContactId_Required")]
        [Range(1, Int32.MaxValue,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ContactDto_TypeContactId_Range")]
        public Int32 TypeContactId { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ContactDto_Nom_Required")]
        [StringLength(50,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ContactDto_Nom_StringLength")]
        public String Nom { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ContactDto_Prenom_Required")]
        [StringLength(50,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ContactDto_Prenom_StringLength")]
        public String Prenom { get; set; }

        [RegularExpression("[0-9]{10}",
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ContactDto_Telephone_Regex")]
        public String Telephone { get; set; }

        [StringLength(250,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ContactDto_Courriel_StringLength")]
        [RegularExpression(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$",
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ContactDto_Courriel_Regex")]
        public String Courriel { get; set; }
    }
}