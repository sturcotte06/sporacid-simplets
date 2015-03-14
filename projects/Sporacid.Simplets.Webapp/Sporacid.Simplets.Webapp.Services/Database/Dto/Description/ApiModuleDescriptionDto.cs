namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Description
{
    using System;
    using System.Collections.Generic;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class ApiModuleDescriptionDto
    {
        public String Name { get; set; }
        public IEnumerable<ApiMethodDescriptionDto> Methods { get; set; }
    }
}