namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Sporacid.Simplets.Webapp.Services.Resources.Validation;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class ItemDto
    {
        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ItemDto_UniteId_Required")]
        [Range(1, Int32.MaxValue,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ItemDto_UniteId_Range")]
        public Int32 UniteId { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ItemDto_Description_Required")]
        [StringLength(250,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ItemDto_Description_StringLength")]
        public String Description { get; set; }

        [StringLength(20,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ItemDto_Code_StringLength")]
        public String Code { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ItemDto_Quantite_Required")]
        [Range(1, 1000000,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ItemDto_Quantite_Range")]
        public Double Quantite { get; set; }

        [Range(1, 1000000,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ItemDto_QuantiteMin_Range")]
        public Double QuantiteMin { get; set; }

        [Range(1, 1000000,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ItemDto_QuantiteMax_Range")]
        public Double QuantiteMax { get; set; }
    }
}