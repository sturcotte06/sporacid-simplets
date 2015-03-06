namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Sporacid.Simplets.Webapp.Services.Resources.Validation;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class StatutSuivieDto
    {
        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "StatutSuivieDto_Code_Required")]
        [StringLength(50,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "StatutSuivieDto_Code_StringLength")]
        public String Code { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "StatutSuivieDto_Description_Required")]
        [StringLength(150,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "StatutSuivieDto_Description_StringLength")]
        public String Description { get; set; }
    }
}