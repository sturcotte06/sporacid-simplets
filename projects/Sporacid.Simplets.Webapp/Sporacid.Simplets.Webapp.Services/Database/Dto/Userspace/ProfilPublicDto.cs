namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Userspace
{
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Dbo;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class ProfilPublicDto
    {
        public String Nom { get; set; }
        public String Prenom { get; set; }
        public Boolean Actif { get; set; }
        public Binary Avatar { get; set; }
        public ProfilAvanceDto ProfilAvance { get; set; }
        public IEnumerable<FormationDto> Formations { get; set; }
        public IEnumerable<AntecedentDto> Antecedents { get; set; }
        public IEnumerable<ContactDto> Contacts { get; set; }
    }
}