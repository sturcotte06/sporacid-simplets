namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Description
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class ApiMethodParameterDescriptionDto
    {
        public String Name { get; set; }
        public String Documentation { get; set; }
        public String ParameterType { get; set; }
        public Boolean IsOptional { get; set; }
    }
}