namespace Sporacid.Simplets.Webapp.Services.Services.Clubs.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Data.Linq.SqlClient;
    using System.Linq;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;
    using WebApi.OutputCache.V2;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/{clubName:alpha}/meeting")]
    public class MeetingService : BaseService, IMeetingService
    {
        private readonly IRepository<Int32, Club> clubRepository;
        private readonly IRepository<Int32, Meeting> meetingRepository;

        public MeetingService(IRepository<Int32, Club> clubRepository, IRepository<Int32, Meeting> meetingRepository)
        {
            this.clubRepository = clubRepository;
            this.meetingRepository = meetingRepository;
        }

        /// <summary>
        /// Get all meeting entities from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The meeting entities.</returns>
        [HttpGet, Route("")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Medium)]
        public IEnumerable<WithId<Int32, MeetingDto>> GetAll(String clubName, [FromUri] UInt32? skip = null, [FromUri] UInt32? take = null)
        {
            return this.meetingRepository
                .GetAll(meeting => SqlMethods.Like(clubName, meeting.Club.Nom))
                .OrderByDescending(meeting => meeting.DateDebut)
                .OptionalSkipTake(skip, take)
                .MapAllWithIds<Meeting, MeetingDto>();
        }

        /// <summary>
        /// Get a meeting entity from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="meetingId">The meeting id.</param>
        /// <returns>The meeting entity.</returns>
        [HttpGet, Route("{meetingId:int}")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Medium)]
        public MeetingDto Get(String clubName, Int32 meetingId)
        {
            return this.meetingRepository
                .GetUnique(meeting => SqlMethods.Like(meeting.Club.Nom, clubName) && meeting.Id == meetingId)
                .MapTo<Meeting, MeetingDto>();
        }

        /// <summary>
        /// Creates a meeting entity in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="meeting">The meeting entity.</param>
        /// <returns>The created meeting id.</returns>
        [HttpPost, Route("")]
        [InvalidateCacheOutput("GetAll")]
        public Int32 Create(String clubName, MeetingDto meeting)
        {
            var clubEntity = this.clubRepository.GetUnique(club => SqlMethods.Like(clubName, club.Nom));
            var meetingEntity = meeting.MapTo<MeetingDto, Meeting>();

            // Make sure the commandite is created in this context.
            meetingEntity.ClubId = clubEntity.Id;

            this.meetingRepository.Add(meetingEntity);
            return meetingEntity.Id;
        }

        /// <summary>
        /// Updates a meeting entity in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="meetingId">The commandite id.</param>
        /// <param name="meeting">The meeting entity.</param>
        [HttpPut, Route("{meetingId:int}")]
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetAll")]
        public void Update(String clubName, Int32 meetingId, MeetingDto meeting)
        {
            var meetingEntity = this.meetingRepository
                .GetUnique(meeting2 => SqlMethods.Like(meeting2.Club.Nom, clubName) && meeting2.Id == meetingId)
                .MapFrom(meeting);
            this.meetingRepository.Update(meetingEntity);
        }

        /// <summary>
        /// Deletes a meeting entity from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="meetingId">The meeting id.</param>
        [HttpDelete, Route("{meetingId:int}")]
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetAll")]
        public void Delete(String clubName, Int32 meetingId)
        {
            // Somewhat trash call to make sure the meeting is in this context. 
            var meetingEntity = this.meetingRepository
                .GetUnique(meeting => SqlMethods.Like(clubName, meeting.Club.Nom) && meeting.Id == meetingId);
            this.meetingRepository.Delete(meetingEntity);
        }
    }
}