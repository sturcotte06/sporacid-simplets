namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Description
{
    using System;
    using System.Collections.Generic;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class ApiMethodDescriptionDto
    {
        public String Name { get; set; }
        public String HttpMethod { get; set; }
        public String Route { get; set; }
        public String Documentation { get; set; }
        public Claims RequiredClaims { get; set; }
        public IEnumerable<ApiMethodParameterDescriptionDto> Parameters { get; set; }
        public ApiResponseDescriptionDto Response { get; set; }
    }
}