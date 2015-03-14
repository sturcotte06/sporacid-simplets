namespace Sporacid.Simplets.Webapp.Services.Database.Dto.Userspace
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Sporacid.Simplets.Webapp.Services.Resources.Validation;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class ProfilAvanceDto
    {
        [StringLength(12,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ProfilAvanceDto_CodePermament_StringLength")]
        public String CodePermanent { get; set; }

        [JsonConverter(typeof(DateConverter))]
        public DateTime? DateNaissance { get; set; }

        [RegularExpression("[0-9]{10}",
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ProfilAvanceDto_Telephone_Regex")]
        public String Telephone { get; set; }

        [StringLength(250,
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ProfilAvanceDto_Courriel_StringLength")]
        [RegularExpression(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$",
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ProfilAvanceDto_Courriel_Regex")]
        public String Courriel { get; set; }

        [Required(
            ErrorMessageResourceType = typeof (ValidationStrings),
            ErrorMessageResourceName = "ProfilAvanceDto_Public_Required")]
        public bool Public { get; set; }
    }

    class DateConverter : IsoDateTimeConverter
    {
        public DateConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd";
        }
    }
}