namespace Sporacid.Simplets.Webapp.Services
{
    using System;
    using System.Web.Http;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;

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
            jsonFormatterSettings.ContractResolver = new PascalCasePropertyNamesContractResolver();
        }
    }

    class PascalCasePropertyNamesContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            var pascalCase = base.ResolvePropertyName(propertyName);
            return pascalCase.Remove(0, 1).Insert(0, Char.ToUpper(pascalCase[0]).ToString());
        }
    }
}