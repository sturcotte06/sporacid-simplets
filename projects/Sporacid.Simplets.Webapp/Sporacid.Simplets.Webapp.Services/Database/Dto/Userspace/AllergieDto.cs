namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Userspace
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Sporacid.Simplets.Webapp.Services.Resources.Validation;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class AllergieDto
    {
        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "AllergieDto_Nom_Required")]
        [StringLength(50,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "AllergieDto_Nom_StringLength")]
        public String Nom { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "AllergieDto_Description_Required")]
        [StringLength(150,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "AllergieDto_Description_StringLength")]
        public String Description { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "AllergieDto_Public_Required")]
        public Boolean Public { get; set; }
    }
}