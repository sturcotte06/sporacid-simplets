namespace Sporacid.Simplets.Webapp.Services.Services.Clubs.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;
    using Sporacid.Simplets.Webapp.Services.Database.Repositories;
    using Sporacid.Simplets.Webapp.Services.Services.Security.Administration;
    using Sporacid.Simplets.Webapp.Services.Services.Userspace;
    using WebApi.OutputCache.V2;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/{clubName}/membre")]
    public class MembreController : BaseSecureService, IMembreService
    {
        private readonly IEntityRepository<Int32, Club> clubRepository;
        private readonly IContextAdministrationService contextAdministrationService;
        private readonly IEntityRepository<Int32, Membre> membreRepository;
        private readonly IPrincipalAdministrationService principalAdministrationService;
        private readonly IProfilService profilService;

        public MembreController(IContextAdministrationService contextAdministrationService, IPrincipalAdministrationService principalAdministrationService,
            IProfilService profilService, IEntityRepository<Int32, Club> clubRepository, IEntityRepository<Int32, Membre> membreRepository)
        {
            this.contextAdministrationService = contextAdministrationService;
            this.principalAdministrationService = principalAdministrationService;
            this.clubRepository = clubRepository;
            this.membreRepository = membreRepository;
            this.profilService = profilService;
        }

        /// <summary>
        /// Return all inscriton of a club entity.
        /// </summary>
        /// <param name="clubName">The id of the club entity.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        [HttpGet, Route("")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Medium)]
        public IEnumerable<WithId<Int32, MembreDto>> GetAll(String clubName, [FromUri] UInt32? skip = null, [FromUri] UInt32? take = null)
        {
            // Kind of an hack. The universal code is required to obtain the public profil entity.
            // We need to store a temporary dto that has this code.
            var tempMembres = this.membreRepository
                .GetAll(membre => membre.Club.Nom == clubName)
                .OptionalSkipTake(skip, take)
                .Select(membreEntity => new
                {
                    membreEntity.CodeUniversel,
                    Membre = membreEntity.MapWithId<Int32, Membre, MembreDto>()
                })
                .ToList();

            // Then for each of the temporary dtos, get the public profil.
            tempMembres.ForEach(tempMembre => tempMembre.Membre.Entity.ProfilPublic = this.profilService.GetPublic(tempMembre.CodeUniversel));

            // Only return the membre dto.
            return tempMembres.Select(tempMembre => tempMembre.Membre);
        }

        /// <summary>
        /// Get all membre entities in the given group from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="groupeId">The groupe id.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The fournisseur entities.</returns>
        [HttpGet, Route("in/{groupeId:int}")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Medium)]
        public IEnumerable<WithId<Int32, MembreDto>> GetAllFromGroupe(String clubName, Int32 groupeId, [FromUri] UInt32? skip = null, UInt32? take = null)
        {
            return this.membreRepository
                .GetAll(membre => membre.Club.Nom == clubName && membre.GroupeMembres.Any(gp => gp.GroupeId == groupeId))
                .OptionalSkipTake(skip, take)
                .MapAllWithIds<Membre, MembreDto>();
        }

        /// <summary>
        /// Get a membre entity from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="membreId">The membre id.</param>
        /// <returns>The membre entity.</returns>
        [HttpGet, Route("{membreId:int}")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Medium, ClientTimeSpan = (Int32) CacheDuration.Medium)]
        public MembreDto Get(String clubName, Int32 membreId)
        {
            return this.membreRepository
                .GetUnique(membre => membre.Club.Nom == clubName && membre.Id == membreId)
                .MapTo<Membre, MembreDto>();
        }

        /// <summary>
        /// Subscribes a member entity to a club entity.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="codeUniversel">The id of the member entity.</param>
        [HttpPost, Route("subscribe/{codeUniversel}")]
        public void SubscribeToClub(String clubName, String codeUniversel)
        {
            if (!this.principalAdministrationService.Exists(codeUniversel))
            {
                // User never logged in. Create its principal and base profil.
                this.principalAdministrationService.Create(codeUniversel);
            }

            var defaultRole = SecurityConfig.Role.Lecteur.ToString();
            var clubEntity = this.clubRepository.GetUnique(club => club.Nom == clubName);
            var membreEntity = clubEntity.Membres.SingleOrDefault(membre => membre.CodeUniversel == codeUniversel);

            if (membreEntity != null)
            {
                // Membre entity already exist. Reactivate it.
                membreEntity.Actif = true;
                membreEntity.DateFin = null;
            }
            else
            {
                // Membre entity does not exist. Create it.
                clubEntity.Membres.Add(new Membre
                {
                    Club = clubEntity,
                    Titre = defaultRole,
                    CodeUniversel = codeUniversel,
                    DateDebut = DateTime.UtcNow,
                    Actif = true
                });
            }

            // Merge the previous claims of the principal on the club with the most basic claims.
            this.contextAdministrationService.MergeClaimsOfPrincipalWithRole(clubName, defaultRole, codeUniversel);
            this.clubRepository.Update(clubEntity);
        }

        /// <summary>
        /// Unsubscribes a member entity from a club entity.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="codeUniversel">The universal code that represents the user.</param>
        [HttpDelete, Route("unsubscribe/{codeUniversel}")]
        public void UnsubscribeFromClub(String clubName, String codeUniversel)
        {
            var membreEntity = this.membreRepository
                .GetUnique(membre => membre.CodeUniversel == codeUniversel && membre.Club.Nom == clubName);

            // Do not really delete the membre entity. Just set it to inactive.
            membreEntity.Actif = false;
            membreEntity.DateFin = DateTime.UtcNow;

            // Remove all claims of the principal on the club.
            this.contextAdministrationService.RemoveAllClaimsFromPrincipal(clubName, codeUniversel);
            this.membreRepository.Update(membreEntity);
        }
    }
}