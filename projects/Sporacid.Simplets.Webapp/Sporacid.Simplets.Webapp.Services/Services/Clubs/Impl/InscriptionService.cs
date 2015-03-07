namespace Sporacid.Simplets.Webapp.Services.Services.Clubs.Impl
{
    using System;
    using System.Data.Linq.SqlClient;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Services.Administration;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/{clubName:alpha}/inscription")]
    public class InscriptionService : BaseService, IInscriptionService
    {
        private readonly IRepository<Int32, Club> clubRepository;
        private readonly IContextAdministrationService contextAdministrationService;
        private readonly IRepository<Int32, Membre> membreRepository;

        public InscriptionService(IContextAdministrationService contextAdministrationService, IRepository<Int32, Club> clubRepository,
            IRepository<Int32, Membre> membreRepository)
        {
            this.contextAdministrationService = contextAdministrationService;
            this.clubRepository = clubRepository;
            this.membreRepository = membreRepository;
        }

        /// <summary>
        /// Subscribes a member entity to a club entity.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="codeUniversel">The id of the member entity.</param>
        [HttpPost, Route("{codeUniversel}")]
        public void SubscribeToClub(String clubName, String codeUniversel)
        {
            // Todo if inactif -> reactiver
            var defaultRole = SecurityConfig.Role.Noob.ToString();
            var clubEntity = this.clubRepository.GetUnique(club => SqlMethods.Like(club.Nom, clubName));
            clubEntity.Membres.Add(new Membre
            {
                Club = clubEntity,
                Titre = defaultRole,
                CodeUniversel = codeUniversel,
                DateDebut = DateTime.UtcNow,
                Actif = true
            });

            // Set the most basic rights on the club context for this principal.
            this.contextAdministrationService.BindRoleToPrincipal(clubName, defaultRole, codeUniversel);
            this.clubRepository.Update(clubEntity);
        }

        /// <summary>
        /// Unsubscribes a member entity from a club entity.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="codeUniversel">The universal code that represents the user.</param>
        [HttpDelete, Route("{membreId:int}")]
        public void UnsubscribeFromClub(String clubName, String codeUniversel)
        {
            var membreEntity = this.membreRepository
                .GetUnique(membre => SqlMethods.Like(membre.CodeUniversel, codeUniversel) && SqlMethods.Like(membre.Club.Nom, clubName));

            // Do not really delete the membre entity. Just set it to inactive.
            membreEntity.Actif = false;
            membreEntity.DateFin = new DateTime();

            this.membreRepository.Update(membreEntity);
        }
    }
}