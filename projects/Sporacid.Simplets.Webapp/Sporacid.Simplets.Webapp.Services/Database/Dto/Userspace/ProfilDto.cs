namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Userspace
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class ProfilDto
    {
        public Int32? ConcentrationId { get; set; }

        [Required]
        [StringLength(50)]
        public string Nom { get; set; }

        [StringLength(50)]
        public string Prenom { get; set; }

        [Required]
        public bool Actif { get; set; }

        [Required]
        public bool Public { get; set; }

        // public byte[] Avatar { get; set; }

        [Required]
        public ProfilAvanceDto ProfilAvance { get; set; }

        // public FormationDto[] Formations { get; set; }
        // public PreferenceDto[] Preferences { get; set; }
        // public AllergieDto[] Allergies { get; set; }
        // public ContactDto[] ContactsUrgence { get; set; }
    }
}