namespace Sporacid.Simplets.Webapp.Services.Services.Clubs.Administration.Impl
{
    using System;
    using System.Web;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Core.Events;
    using Sporacid.Simplets.Webapp.Core.Exceptions;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Repositories;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Security.Authorization;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;
    using Sporacid.Simplets.Webapp.Services.Events;
    using Sporacid.Simplets.Webapp.Services.Resources.Exceptions;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/administration")]
    public class ClubAdministrationController : BaseSecureService, IClubAdministrationService, IEventPublisher<ClubCreated, ClubCreatedEventArgs>
    {
        private readonly IEventBus<ClubCreated, ClubCreatedEventArgs> clubCreatedEventBus;
        private readonly IRepository<Int32, Club> clubRepository;

        public ClubAdministrationController(IRepository<Int32, Club> clubRepository, IEventBus<ClubCreated, ClubCreatedEventArgs> clubCreatedEventBus)
        {
            this.clubCreatedEventBus = clubCreatedEventBus;
            this.clubRepository = clubRepository;
        }

        /// <summary>
        /// Creates a club entity into the system.
        /// Creating a club will creates its security context and will give all rights on the context to the principal creating the
        /// club.
        /// </summary>
        /// <param name="club">The club entity.</param>
        /// <exception cref="NotAuthorizedException">
        /// If the security context already exists.
        /// </exception>
        /// <exception cref="RepositoryException">
        /// If something unexpected occurs while creating the context.
        /// </exception>
        /// <exception cref="CoreException">
        /// If something unexpected occurs.
        /// </exception>
        /// <returns>The id of the created club entity.</returns>
        [HttpPost, Route("club")]
        public Int32 CreateClub(ClubDto club)
        {
            // Cannot add the same club twice.
            if (this.clubRepository.Has(club2 => club2.Nom == club.Nom))
            {
                throw new NotAuthorizedException(String.Format(ExceptionStrings.Services_Security_ClubDuplicate, club.Nom));
            }

            // Add the new club entity.
            var clubEntity = club.MapTo<ClubDto, Club>();
            this.clubRepository.Add(clubEntity);

            // Publish a club created event.
            this.Publish(new ClubCreatedEventArgs(clubEntity.Nom, HttpContext.Current.User.Identity.Name));

            // Add a new security context for the club.
            // this.contextService.Create(clubEntity.Nom, identity);
            return clubEntity.Id;
        }

        /// <summary>
        /// Publishes an event in the given event bus.
        /// </summary>
        /// <param name="eventArgs">The event args of the event.</param>
        public void Publish(ClubCreatedEventArgs eventArgs)
        {
            this.clubCreatedEventBus.Publish(this, eventArgs);
        }
    }
}