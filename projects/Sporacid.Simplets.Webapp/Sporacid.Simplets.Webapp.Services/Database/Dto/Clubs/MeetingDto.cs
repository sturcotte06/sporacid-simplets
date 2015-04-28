namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Sporacid.Simplets.Webapp.Services.Resources.Validation;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class MeetingDto
    {
        [Range(1, Int32.MaxValue,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "MeetingDto_ConvocateurId_Range")]
        public Int32? ConvocateurId { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "MeetingDto_DateDebut_Required")]
        public DateTime DateDebut { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "MeetingDto_DateFin_Required")]
        public DateTime DateFin { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "MeetingDto_Commentaire_Required")]
        [StringLength(250,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "MeetingDto_Commentaire_StringLength")]
        public String Commentaire { get; set; }
    }
}