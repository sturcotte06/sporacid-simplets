namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Description
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class ApiResponseDescriptionDto
    {
        public String Documentation { get; set; }
        public String ResponseType { get; set; }
    }
}