namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Sporacid.Simplets.Webapp.Services.Resources.Validation;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class CommanditeDto
    {
        [Range(1, Int32.MaxValue,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "CommanditeDto_FournisseurId_Range")]
        public Int32? FournisseurId { get; set; }

        [Range(1, Int32.MaxValue,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "CommanditeDto_ItemId_Range")]
        public Int32? ItemId { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "CommanditeDto_Valeur_Required")]
        [Range(0, 1000000,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "CommanditeDto_Valeur_Range")]
        public Double Valeur { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "CommanditeDto_Nature_Required")]
        [StringLength(50,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "CommanditeDto_Nature_StringLength")]
        public String Nature { get; set; }
    }
}