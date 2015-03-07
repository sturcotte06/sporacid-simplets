namespace Sporacid.Simplets.Webapp.Services.Services.Clubs
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;
    using Sporacid.Simplets.Webapp.Services.Resources.Contracts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Meetings")]
    [Contextual("clubName")]
    [ContractClass(typeof (MeetingServiceContract))]
    public interface IMeetingService
    {
        /// <summary>
        /// Get all meeting entities from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The meeting entities.</returns>
        [RequiredClaims(Claims.ReadAll)]
        IEnumerable<WithId<Int32, MeetingDto>> GetAll(String clubName, UInt32? skip, UInt32? take);

        /// <summary>
        /// Get a meeting entity from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="meetingId">The meeting id.</param>
        /// <returns>The meeting entity.</returns>
        [RequiredClaims(Claims.Read)]
        MeetingDto Get(String clubName, Int32 meetingId);

        /// <summary>
        /// Creates a meeting entity in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="meeting">The meeting entity.</param>
        /// <returns>The created meeting id.</returns>
        [RequiredClaims(Claims.Create)]
        Int32 Create(String clubName, MeetingDto meeting);

        /// <summary>
        /// Updates a meeting entity in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="meetingId">The commandite id.</param>
        /// <param name="meeting">The meeting entity.</param>
        [RequiredClaims(Claims.Update)]
        void Update(String clubName, Int32 meetingId, MeetingDto meeting);

        /// <summary>
        /// Deletes a meeting entity from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="meetingId">The meeting id.</param>
        [RequiredClaims(Claims.Delete)]
        void Delete(String clubName, Int32 meetingId);
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IMeetingService))]
    internal abstract class MeetingServiceContract : IMeetingService
    {
        /// <summary>
        /// Get all meeting entities from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The meeting entities.</returns>
        public IEnumerable<WithId<Int32, MeetingDto>> GetAll(String clubName, UInt32? skip, UInt32? take)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.MeetingService_GetAll_RequiresClubName);
            Contract.Requires(take == null || take > 0, ContractStrings.MeetingService_GetAll_RequiresUndefinedOrPositiveTake);

            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<WithId<Int32, MeetingDto>>>() != null,
                ContractStrings.MeetingService_GetAll_EnsuresNonNullMeetings);

            // Dummy return.
            return default(IEnumerable<WithId<Int32, MeetingDto>>);
        }

        /// <summary>
        /// Get a meeting entity from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="meetingId">The meeting id.</param>
        /// <returns>The meeting entity.</returns>
        public MeetingDto Get(String clubName, Int32 meetingId)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.MeetingService_Get_RequiresClubName);
            Contract.Requires(meetingId > 0, ContractStrings.MeetingService_Get_RequiresPositiveMeetingId);

            // Postconditions.
            Contract.Ensures(Contract.Result<MeetingDto>() != null, ContractStrings.MeetingService_Get_EnsuresNonNullMeeting);

            // Dummy return.
            return default(MeetingDto);
        }

        /// <summary>
        /// Creates a meeting entity in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="meeting">The meeting entity.</param>
        /// <returns>The created meeting id.</returns>
        public Int32 Create(String clubName, MeetingDto meeting)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.MeetingService_Create_RequiresClubName);
            Contract.Requires(meeting != null, ContractStrings.MeetingService_Create_RequiresMeeting);

            // Postconditions.
            Contract.Ensures(Contract.Result<Int32>() > 0, ContractStrings.MeetingService_Get_EnsuresPositiveMeetingId);

            // Dummy return.
            return default(Int32);
        }

        /// <summary>
        /// Updates a meeting entity in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="meetingId">The commandite id.</param>
        /// <param name="meeting">The meeting entity.</param>
        public void Update(String clubName, Int32 meetingId, MeetingDto meeting)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.MeetingService_Update_RequiresClubName);
            Contract.Requires(meetingId > 0, ContractStrings.MeetingService_Update_RequiresPositiveMeetingId);
            Contract.Requires(meeting != null, ContractStrings.MeetingService_Update_RequiresMeeting);
        }

        /// <summary>
        /// Deletes a meeting entity from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="meetingId">The meeting id.</param>
        public void Delete(String clubName, Int32 meetingId)
        {
            // Preconditions.
            Contract.Requires(!String.IsNullOrEmpty(clubName), ContractStrings.MeetingService_Delete_RequiresClubName);
            Contract.Requires(meetingId > 0, ContractStrings.MeetingService_Delete_RequiresPositiveMeetingId);
        }
    }
}