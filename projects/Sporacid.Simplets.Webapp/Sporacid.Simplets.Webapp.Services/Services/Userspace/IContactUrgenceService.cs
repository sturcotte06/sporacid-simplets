namespace Sporacid.Simplets.Webapp.Services.Services.Userspace
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Dbo;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Userspace;
    using Sporacid.Simplets.Webapp.Services.Resources.Contracts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("ContactsUrgence")]
    [Contextual("codeUniversel")]
    [ContractClass(typeof (ContactUrgenceServiceContract))]
    public interface IContactUrgenceService : IService
    {
        /// <summary>
        /// Gets all contact urgence entities from the user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The contact urgence entities.</returns>
        [RequiredClaims(Claims.ReadAll)]
        IEnumerable<WithId<Int32, ContactDto>> GetAll(String codeUniversel, UInt32? skip, UInt32? take);

        /// <summary>
        /// Gets the contact urgence entity from  the user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="contactId">The contact urgence id.</param>
        /// <returns>The contact urgence entity.</returns>
        [RequiredClaims(Claims.Read)]
        ContactDto Get(String codeUniversel, Int32 contactId);

        /// <summary>
        /// Creates a contact urgence entity in a user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="contact">The contact urgence.</param>
        /// <returns>The created contact urgence entity id.</returns>
        [RequiredClaims(Claims.Create)]
        Int32 Create(String codeUniversel, ContactDto contact);

        /// <summary>
        /// Updates the contact urgence entity in a user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="contactId">The contact urgence id.</param>
        /// <param name="contact">The contact urgence.</param>
        [RequiredClaims(Claims.Update)]
        void Update(String codeUniversel, Int32 contactId, ContactDto contact);

        /// <summary>
        /// Deletes a contact urgence entity from a user context.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <param name="contactId">The contact urgence id.</param>
        [RequiredClaims(Claims.Delete)]
        void Delete(String codeUniversel, Int32 contactId);
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IContactUrgenceService))]
    internal abstract class ContactUrgenceServiceContract : IContactUrgenceService
    {
        public IEnumerable<WithId<Int32, ContactDto>> GetAll(String codeUniversel, UInt32? skip, UInt32? take)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.ContactUrgenceService_GetAll_RequiresCodeUniversel);
            Contract.Requires(take == null || take > 0, ContractStrings.ContactUrgenceService_GetAll_RequiresUndefinedOrPositiveTake);

            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<WithId<Int32, ContactDto>>>() != null, ContractStrings.ContactUrgenceService_GetAll_EnsuresNonNullContactsUrgence);

            // Dummy return.
            return default(IEnumerable<WithId<Int32, ContactDto>>);
        }

        public ContactDto Get(String codeUniversel, Int32 contactId)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.ContactUrgenceService_Get_RequiresCodeUniversel);
            Contract.Requires(contactId > 0, ContractStrings.ContactUrgenceService_Get_RequiresPositiveContactUrgenceId);

            // Postconditions.
            Contract.Ensures(Contract.Result<PreferenceDto>() != null, ContractStrings.ContactUrgenceService_Get_EnsuresNonNullContactUrgence);

            // Dummy return.
            return default(ContactDto);
        }

        public Int32 Create(String codeUniversel, ContactDto contact)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.ContactUrgenceService_Create_RequiresCodeUniversel);
            Contract.Requires(contact != null, ContractStrings.ContactUrgenceService_Create_RequiresContactUrgence);

            // Postconditions.
            Contract.Ensures(Contract.Result<Int32>() > 0, ContractStrings.ContactUrgenceService_Create_EnsuresPositiveContactUrgenceId);

            // Dummy return.
            return default(Int32);
        }

        public void Update(String codeUniversel, Int32 contactId, ContactDto contact)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.ContactUrgenceService_Update_RequiresCodeUniversel);
            Contract.Requires(contactId > 0, ContractStrings.ContactUrgenceService_Update_RequiresPositiveContactUrgenceId);
            Contract.Requires(contact != null, ContractStrings.ContactUrgenceService_Update_RequiresContactUrgence);
        }

        public void Delete(String codeUniversel, Int32 contactId)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(codeUniversel), ContractStrings.ContactUrgenceService_Delete_RequiresCodeUniversel);
            Contract.Requires(contactId > 0, ContractStrings.ContactUrgenceService_Delete_RequiresPositiveContactUrgenceId);
        }
    }
}