namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Description
{
    using System;
    using System.Collections.Generic;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class ApiEntityDescriptionDto
    {
        public String EntityName { get; set; }
        public IEnumerable<ApiEntityPropertyDescriptionDto> Properties { get; set; }
    }
}