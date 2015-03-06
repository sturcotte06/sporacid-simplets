namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Dbo
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Sporacid.Simplets.Webapp.Services.Resources.Validation;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class UniteDto
    {
        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "UniteDto_Code_Required")]
        [StringLength(10,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "UniteDto_Code_StringLength")]
        public String Code { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "UniteDto_Systeme_Required")]
        [StringLength(10,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "UniteDto_Systeme_StringLength")]
        public String Systeme { get; set; }
    }
}