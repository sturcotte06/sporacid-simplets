namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Sporacid.Simplets.Webapp.Services.Resources.Validation;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class SuivieDto
    {
        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "SuivieDto_MembreId_Required")]
        [Range(1, Int32.MaxValue,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "SuivieDto_MembreId_Range")]
        public Int32 MembreId { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "SuivieDto_StatutSuivieId_Required")]
        [Range(1, Int32.MaxValue,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "SuivieDto_StatutSuivieId_Range")]
        public Int32 StatutSuivieId { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "SuivieDto_DateSuivie_Required")]
        public DateTime DateSuivie { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "SuivieDto_Commentaire_Required")]
        [StringLength(250,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "SuivieDto_Commentaire_StringLength")]
        public String Commentaire { get; set; }
    }
}