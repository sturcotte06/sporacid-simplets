namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Userspace
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Sporacid.Simplets.Webapp.Services.Resources.Validation;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class PreferenceDto
    {
        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "PreferenceDto_Name_Required")]
        [StringLength(50,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "PreferenceDto_Name_StringLength")]
        public String Name { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "PreferenceDto_Value_Required")]
        [StringLength(150,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "PreferenceDto_Value_StringLength")]
        public String Value { get; set; }
    }
}