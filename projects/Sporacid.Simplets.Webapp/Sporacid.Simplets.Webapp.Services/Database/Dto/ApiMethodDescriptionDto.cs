﻿namespace Sporacid.Simplets.Webapp.Services.Database.Dto
{
    using System;
    using System.Collections.Generic;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class ApiMethodDescriptionDto
    {
        public String HttpMethod { get; set; }
        public String Route { get; set; }
        public String Documentation { get; set; }
        public IEnumerable<ApiMethodParameterDescriptionDto> ParameterDescriptions { get; set; }
    }
}