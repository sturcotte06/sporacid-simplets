namespace Sporacid.Simplets.Webapp.Services
{
    using System;
    using System.Globalization;
    using System.Web.Http;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class SerializationConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var jsonFormatterSettings = config.Formatters.JsonFormatter.SerializerSettings;

            jsonFormatterSettings.Formatting = Formatting.Indented;
            jsonFormatterSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ssZ";
            jsonFormatterSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            jsonFormatterSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonFormatterSettings.Converters.Add(new StringEnumConverter());
            jsonFormatterSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class PascalCasePropertyNamesContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            var camelCase = base.ResolvePropertyName(propertyName);
            return camelCase.Remove(0, 1).Insert(0, Char.ToUpper(camelCase[0]).ToString(CultureInfo.CurrentCulture));
        }
    }
}