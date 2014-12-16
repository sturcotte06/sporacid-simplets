using Sporacid.Simplets.Webapp.Services;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof (AutoMapperConfig), "InitializeAutoMapper")]

namespace Sporacid.Simplets.Webapp.Services
{
    using AutoMapper;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class AutoMapperConfig
    {
        /// <summary>
        /// Initializes AutoMapper mappings and configration.
        /// </summary>
        public static void InitializeAutoMapper()
        {
            // Configure AutoMapping.
            Mapper.Initialize(config =>
            {
                config.SourceMemberNamingConvention = new PascalCaseNamingConvention();
                config.DestinationMemberNamingConvention = new PascalCaseNamingConvention();
            });

            // Create all mappings.
            Mapper.CreateMap<Membre, MembreDto>().ReverseMap();
            Mapper.CreateMap<Concentration, ConcentrationDto>().ReverseMap();

            // Assert that we have not screwed up.
            Mapper.AssertConfigurationIsValid();
        }
    }
}