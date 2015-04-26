namespace Sporacid.Simplets.Webapp.Services.Services.Userspace.Impl
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Dbo;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Userspace;
    using Sporacid.Simplets.Webapp.Services.Database.Repositories;
    using Sporacid.Simplets.Webapp.Services.Services.Clubs.Impl;
    using WebApi.OutputCache.V2;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/{codeUniversel}/profil")]
    public class ProfilController : BaseSecureService, IProfilService
    {
        private readonly IEntityRepository<ContactUrgenceId, ContactUrgence> contactUrgenceRepository;
        private readonly IEntityRepository<Int32, Profil> profilRepository;

        public ProfilController(
            IEntityRepository<Int32, Profil> profilRepository,
            IEntityRepository<ContactUrgenceId, ContactUrgence> contactUrgenceRepository)
        {
            this.profilRepository = profilRepository;
            this.contactUrgenceRepository = contactUrgenceRepository;
        }

        /// <summary>
        /// Gets the profil object from the system.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <returns>The profil.</returns>
        [HttpGet, Route("")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.VeryLong)]
        public ProfilDto Get(String codeUniversel)
        {
            return this.profilRepository
                .GetUnique(profil => codeUniversel == profil.CodeUniversel)
                .MapTo<Profil, ProfilDto>();
        }

        /// <summary>
        /// Updates the profil object in the system.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="profil">The profil.</param>
        [HttpPut, Route("")]
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetPublic"),
         InvalidateCacheOutput("GetAll", typeof (MembreController)),
         InvalidateCacheOutput("Get", typeof (MembreController)),
         InvalidateCacheOutput("GetAllFromGroupe", typeof (MembreController))]
        public void Update(String codeUniversel, ProfilDto profil)
        {
            var profilEntity = this.profilRepository
                .GetUnique(profil2 => codeUniversel == profil2.CodeUniversel)
                .MapFrom(profil);
            this.profilRepository.Update(profilEntity);
        }

        /// <summary>
        /// Gets the public profil entity from the system.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <returns>The public profil.</returns>
        [HttpGet, Route("public")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.VeryLong)]
        public ProfilPublicDto GetPublic(String codeUniversel)
        {
            var profilEntity = this.profilRepository
                .GetUnique(profil => codeUniversel == profil.CodeUniversel);

            // Map the profil to a public profil dto.
            var profilPublic = profilEntity.MapTo<Profil, ProfilPublicDto>();

            // Sets all non-public data to null.
            profilPublic.ProfilAvance = profilPublic.ProfilAvance.Public
                ? profilPublic.ProfilAvance
                : null;
            profilPublic.Formations = profilPublic.Formations.Where(formation => formation.Public);
            profilPublic.Antecedents = profilPublic.Antecedents.Where(antecedent => antecedent.Public);

            // Query the contact differently, because of n to n relationship between contacts and profil.
            profilPublic.Contacts = this.contactUrgenceRepository
                .GetAll(contactUrgence => contactUrgence.Public && contactUrgence.Profil.CodeUniversel == codeUniversel)
                .Select(contactUrgence => contactUrgence.Contact)
                .MapAll<Contact, ContactDto>();

            return profilPublic;
        }
    }
}