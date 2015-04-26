namespace Sporacid.Simplets.Webapp.Services.Services.Userspace.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Dbo;
    using Sporacid.Simplets.Webapp.Services.Database.Repositories;
    using WebApi.OutputCache.V2;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/{codeUniversel}/contact-urgence")]
    public class ContactUrgenceController : BaseSecureService, IContactUrgenceService
    {
        private readonly IEntityRepository<Int32, Profil> profilRepository;
        private readonly IEntityRepository<Int32, Contact> contactRepository;
        private readonly IEntityRepository<ContactUrgenceId, ContactUrgence> contactUrgenceRepository;

        public ContactUrgenceController(
            IEntityRepository<Int32, Profil> profilRepository,
            IEntityRepository<Int32, Contact> contactRepository,
            IEntityRepository<ContactUrgenceId, ContactUrgence> contactUrgenceRepository)
        {
            this.profilRepository = profilRepository;
            this.contactRepository = contactRepository;
            this.contactUrgenceRepository = contactUrgenceRepository;
        }

        /// <summary>
        /// Gets all contact urgence entities from the user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The contact urgence entities.</returns>
        [HttpGet, Route("")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Medium)]
        public IEnumerable<WithId<Int32, ContactDto>> GetAll(String codeUniversel, [FromUri] UInt32? skip = null, [FromUri] UInt32? take = null)
        {
            return this.contactUrgenceRepository
                .GetAll(contactUrgence => contactUrgence.Profil.CodeUniversel == codeUniversel)
                .Select(contactUrgence => contactUrgence.Contact)
                .OptionalSkipTake(skip, take)
                .MapAllWithIds<Contact, ContactDto>();
        }

        /// <summary>
        /// Gets the contact urgence entity from  the user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="contactId">The contact urgence id.</param>
        /// <returns>The contact urgence entity.</returns>
        [HttpGet, Route("{contactId:int}")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Medium)]
        public ContactDto Get(String codeUniversel, Int32 contactId)
        {
            return this.contactUrgenceRepository
                .GetUnique(contactUrgence => contactUrgence.Profil.CodeUniversel == codeUniversel && contactUrgence.Contact.Id == contactId)
                .Contact
                .MapTo<Contact, ContactDto>();
        }

        /// <summary>
        /// Creates a contact urgence entity in a user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="contact">The contact urgence.</param>
        /// <returns>The created contact urgence entity id.</returns>
        [HttpPost, Route("")]
        [InvalidateCacheOutput("GetAll")]
        public Int32 Create(String codeUniversel, ContactDto contact)
        {
            var profilEntity = this.profilRepository.GetUnique(profil => profil.CodeUniversel == codeUniversel);
            var contactEntity = contact.MapTo<ContactDto, Contact>();

            // Add the contact entity first.
            this.contactRepository.Add(contactEntity);

            // Then create the relationship with the profil.
            var contactUrgenceEntity = new ContactUrgence {ContactId = contactEntity.Id, ProfilId = profilEntity.Id};
            this.contactUrgenceRepository.Add(contactUrgenceEntity);

            return contactEntity.Id;
        }

        /// <summary>
        /// Updates the contact urgence entity in a user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="contactId">The contact urgence id.</param>
        /// <param name="contact">The contact urgence.</param>
        [HttpPut, Route("{contactId:int}")]
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetAll")]
        public void Update(String codeUniversel, Int32 contactId, ContactDto contact)
        {
            var contactEntity = this.contactUrgenceRepository
                .GetUnique(contactUrgence => contactUrgence.Profil.CodeUniversel == codeUniversel && contactUrgence.Contact.Id == contactId)
                .Contact
                .MapFrom(contact);
            this.contactRepository.Update(contactEntity);
        }

        /// <summary>
        /// Deletes a contact urgence entity from a user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="contactId">The contact urgence id.</param>
        [HttpDelete, Route("{contactId:int}")]
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetAll")]
        public void Delete(String codeUniversel, Int32 contactId)
        {
            var contactUrgenceEntity = this.contactUrgenceRepository
                .GetUnique(contactUrgence => contactUrgence.Profil.CodeUniversel == codeUniversel && contactUrgence.Contact.Id == contactId);
            this.contactRepository.Delete(contactUrgenceEntity.Contact);
        }
    }
}