namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Dbo
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Sporacid.Simplets.Webapp.Services.Resources.Validation;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class ConcentrationDto
    {
        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ConcentrationDto_Acronyme_Required")]
        [StringLength(10,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ConcentrationDto_Acronyme_StringLength")]
        public String Acronyme { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ConcentrationDto_Description_Required")]
        [StringLength(150,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ConcentrationDto_Description_StringLength")]
        public String Description { get; set; }
    }
}