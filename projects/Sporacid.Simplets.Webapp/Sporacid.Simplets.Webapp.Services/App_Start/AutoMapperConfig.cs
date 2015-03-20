namespace Sporacid.Simplets.Webapp.Services
{
    using System.Web.Http;
    using AutoMapper;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Dbo;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Userspace;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class AutoMapperConfig
    {
        /// <summary>
        /// Initializes AutoMapper mappings and configration.
        /// </summary>
        public static void Register(HttpConfiguration cfg)
        {
            // Configure AutoMapping.
            Mapper.Initialize(config =>
            {
                config.SourceMemberNamingConvention = new PascalCaseNamingConvention();
                config.DestinationMemberNamingConvention = new PascalCaseNamingConvention();
            });

            // Create all mappings.
            // Clubs dtos.
            Mapper.CreateMap<Club, ClubDto>()
                .IgnoreUnmappedProperties().ReverseMap();
            Mapper.CreateMap<Commanditaire, CommanditaireDto>()
                .IgnoreUnmappedProperties().ReverseMap();
            Mapper.CreateMap<Commandite, CommanditeDto>()
                .IgnoreUnmappedProperties().ReverseMap();
            Mapper.CreateMap<Suivie, SuivieDto>()
                .IgnoreUnmappedProperties().ReverseMap();
            Mapper.CreateMap<Evenement, EvenementDto>()
                .IgnoreUnmappedProperties().ReverseMap();
            Mapper.CreateMap<Fournisseur, FournisseurDto>()
                .IgnoreUnmappedProperties().ReverseMap();
            Mapper.CreateMap<Item, ItemDto>()
                .IgnoreUnmappedProperties().ReverseMap();
            Mapper.CreateMap<Membre, MembreDto>()
                .IgnoreUnmappedProperties().ReverseMap();
            Mapper.CreateMap<StatutSuivie, StatutSuivieDto>()
                .IgnoreUnmappedProperties().ReverseMap();
            Mapper.CreateMap<Meeting, MeetingDto>()
                .IgnoreUnmappedProperties().ReverseMap();
            Mapper.CreateMap<Groupe, GroupeDto>()
                .IgnoreUnmappedProperties().ReverseMap();
            Mapper.CreateMap<TypeCommanditaire, TypeCommanditaireDto>()
                .IgnoreUnmappedProperties().ReverseMap();
            Mapper.CreateMap<TypeCommandite, TypeCommanditeDto>()
                .IgnoreUnmappedProperties().ReverseMap();
            Mapper.CreateMap<TypeFournisseur, TypeFournisseurDto>()
                .IgnoreUnmappedProperties().ReverseMap();
            // Dbo dtos.
            Mapper.CreateMap<Adresse, AdresseDto>()
                .IgnoreUnmappedProperties().ReverseMap();
            Mapper.CreateMap<Concentration, ConcentrationDto>()
                .IgnoreUnmappedProperties().ReverseMap();
            Mapper.CreateMap<Contact, ContactDto>()
                .IgnoreUnmappedProperties().ReverseMap();
            Mapper.CreateMap<TypeContact, TypeContactDto>()
                .IgnoreUnmappedProperties().ReverseMap();
            Mapper.CreateMap<Unite, UniteDto>()
                .IgnoreUnmappedProperties().ReverseMap();
            // Userspace dtos.
            Mapper.CreateMap<Antecedent, AntecedentDto>()
                .IgnoreUnmappedProperties().ReverseMap();
            Mapper.CreateMap<TypeAntecedent, TypeAntecedentDto>()
                .IgnoreUnmappedProperties().ReverseMap();
            Mapper.CreateMap<Formation, FormationDto>()
                .IgnoreUnmappedProperties().ReverseMap();
            Mapper.CreateMap<Preference, PreferenceDto>()
                .IgnoreUnmappedProperties().ReverseMap();
            Mapper.CreateMap<ProfilAvance, ProfilAvanceDto>()
                .IgnoreUnmappedProperties().ReverseMap();
            Mapper.CreateMap<Profil, ProfilDto>()
                .IgnoreUnmappedProperties().ReverseMap();

            // Assert that we have not screwed up.
            Mapper.AssertConfigurationIsValid();
        }
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    internal static class AutoMapperExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreUnmappedProperties<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var typeMap = Mapper.FindTypeMapFor<TSource, TDestination>();
            if (typeMap != null)
            {
                foreach (var unmappedPropertyName in typeMap.GetUnmappedPropertyNames())
                {
                    expression.ForMember(unmappedPropertyName, opt => opt.Ignore());
                }
            }

            return expression;
        }
    }
}