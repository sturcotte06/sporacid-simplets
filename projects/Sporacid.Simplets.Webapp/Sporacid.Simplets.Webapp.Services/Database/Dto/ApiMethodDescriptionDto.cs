namespace Sporacid.Simplets.Webapp.Services.Database.Dto
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class ApiMethodDescriptionDto
    {
        public String HttpMethod { get; set; }
        public String Route { get; set; }
        public String Documentation { get; set; }
        public IEnumerable<ApiMethodParameterDescriptionDto> ParameterDescriptions { get; set; }
    }

    [Serializable]
    public class ApiMethodParameterDescriptionDto
    {
        public String Name { get; set; }
        public String Documentation { get; set; }
        public String ParameterType { get; set; }
        public Boolean IsOptional { get; set; }
    }
}