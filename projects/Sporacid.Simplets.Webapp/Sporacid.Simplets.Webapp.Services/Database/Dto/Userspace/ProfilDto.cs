namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Userspace
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Sporacid.Simplets.Webapp.Services.Resources.Validation;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class ProfilDto
    {
        [Range(1, Int32.MaxValue,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ProfilDto_ConcentrationId_Range")]
        public Int32? ConcentrationId { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ProfilDto_Nom_Required")]
        [StringLength(50,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ProfilDto_Nom_StringLength")]
        public string Nom { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ProfilDto_Prenom_StringLength")]
        [StringLength(50,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ProfilDto_Prenom_Required")]
        public string Prenom { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ProfilDto_Actif_Required")]
        public bool Actif { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ProfilDto_Public_Required")]
        public bool Public { get; set; }

        // public byte[] Avatar { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ProfilDto_ProfilAvance_Required")]
        public ProfilAvanceDto ProfilAvance { get; set; }
    }
}