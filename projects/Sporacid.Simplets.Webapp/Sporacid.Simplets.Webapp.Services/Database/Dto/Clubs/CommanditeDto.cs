namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Sporacid.Simplets.Webapp.Services.Resources.Validation;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class CommanditeDto
    {
        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "CommanditeDto_TypeCommanditeId_Required")]
        [Range(1, Int32.MaxValue,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "CommanditeDto_TypeCommanditeId_Range")]
        public Int32 TypeCommanditeId { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "CommanditeDto_Valeur_Required")]
        [Range(0, 100000000,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "CommanditeDto_Valeur_Range")]
        public Double Valeur { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "CommanditeDto_Recu_Required")]
        public Boolean Recu { get; set; }

        [StringLength(250,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "CommanditeDto_Commentaire_StringLength")]
        public String Commentaire { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "CommanditeDto_Suivies_Required")]
        public IEnumerable<SuivieDto> Suivies { get; set; }
    }
}