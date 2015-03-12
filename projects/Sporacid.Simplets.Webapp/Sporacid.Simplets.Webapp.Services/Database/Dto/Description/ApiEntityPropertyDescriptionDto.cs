namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Description
{
    using System;
    using System.Collections.Generic;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class ApiEntityPropertyDescriptionDto
    {
        public String Name { get; set; }
        public String Type { get; set; }
        public IEnumerable<String> Constraints { get; set; }
    }
}