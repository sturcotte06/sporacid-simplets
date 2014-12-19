namespace Sporacid.Simplets.Webapp.Services
{
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
        public static void InitializeAutoMapper()
        {
            // Configure AutoMapping.
            Mapper.Initialize(config =>
            {
                config.SourceMemberNamingConvention = new PascalCaseNamingConvention();
                config.DestinationMemberNamingConvention = new PascalCaseNamingConvention();
            });

            // Create all mappings.
            // Clubs dtos.
            Mapper.CreateMap<Club, ClubDto>().ReverseMap();
            Mapper.CreateMap<Commandite, CommanditeDto>().ReverseMap();
            Mapper.CreateMap<Evenement, EvenementDto>().ReverseMap();
            Mapper.CreateMap<Fournisseur, FournisseurDto>().ReverseMap();
            Mapper.CreateMap<Item, ItemDto>().ReverseMap();
            Mapper.CreateMap<Membre, MembreDto>().ReverseMap();
            Mapper.CreateMap<StatutSuivie, StatutSuivieDto>().ReverseMap();
            Mapper.CreateMap<Suivie, SuivieDto>().ReverseMap();
            // Dbo dtos.
            Mapper.CreateMap<Adresse, AdresseDto>().ReverseMap();
            Mapper.CreateMap<Concentration, ConcentrationDto>().ReverseMap();
            Mapper.CreateMap<Contact, ContactDto>().ReverseMap();
            Mapper.CreateMap<TypeContact, TypeContactDto>().ReverseMap();
            Mapper.CreateMap<Unite, UniteDto>().ReverseMap();
            // Userspace dtos.
            Mapper.CreateMap<Allergie, AllergieDto>().ReverseMap();
            Mapper.CreateMap<Formation, FormationDto>().ReverseMap();
            Mapper.CreateMap<Preference, PreferenceDto>().ReverseMap();
            Mapper.CreateMap<ProfilAvance, ProfilAvanceDto>().ReverseMap();
            Mapper.CreateMap<Profil, ProfilDto>().ReverseMap();

            // Assert that we have not screwed up.
            //Mapper.AssertConfigurationIsValid();
        }
    }
}