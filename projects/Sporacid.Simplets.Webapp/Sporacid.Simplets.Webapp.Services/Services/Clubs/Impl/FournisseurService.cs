namespace Sporacid.Simplets.Webapp.Services.Services.Clubs.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;
    using Sporacid.Simplets.Webapp.Services.Database.Repositories;
    using WebApi.OutputCache.V2;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/{clubName}/fournisseur")]
    public class FournisseurController : BaseSecureService, IFournisseurService
    {
        private readonly IEntityRepository<Int32, Club> clubRepository;
        private readonly IEntityRepository<Int32, Fournisseur> fournisseurRepository;

        public FournisseurController(IEntityRepository<Int32, Fournisseur> fournisseurRepository, IEntityRepository<Int32, Club> clubRepository)
        {
            this.fournisseurRepository = fournisseurRepository;
            this.clubRepository = clubRepository;
        }

        /// <summary>
        /// Get all fournisseurs entities from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The fournisseur entities.</returns>
        [HttpGet, Route("")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Medium)]
        public IEnumerable<WithId<Int32, FournisseurDto>> GetAll(String clubName, [FromUri] UInt32? skip = null, [FromUri] UInt32? take = null)
        {
            return this.fournisseurRepository
                .GetAll(fournisseur => fournisseur.Club.Nom == clubName)
                .OptionalSkipTake(skip, take)
                .MapAllWithIds<Fournisseur, FournisseurDto>();
        }

        /// <summary>
        /// Get a fournisseur from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="fournisseurId">The fournisseur id.</param>
        /// <returns>The fournisseur.</returns>
        [HttpGet, Route("{fournisseurId:int}")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Medium)]
        public FournisseurDto Get(String clubName, Int32 fournisseurId)
        {
            return this.fournisseurRepository
                .GetUnique(fournisseur => fournisseur.Club.Nom == clubName && fournisseur.Id == fournisseurId)
                .MapTo<Fournisseur, FournisseurDto>();
        }

        /// <summary>
        /// Creates a fournisseur in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="fournisseur">The fournisseur.</param>
        /// <returns>The created fournisseur id.</returns>
        [HttpPost, Route("")]
        [InvalidateCacheOutput("GetAll")]
        public Int32 Create(String clubName, FournisseurDto fournisseur)
        {
            var clubEntity = this.clubRepository.GetUnique(club => clubName == club.Nom);
            var fournisseurEntity = fournisseur.MapTo<FournisseurDto, Fournisseur>();

            // Make sure the fournisseur is created in this context.
            fournisseurEntity.ClubId = clubEntity.Id;

            this.fournisseurRepository.Add(fournisseurEntity);
            return fournisseurEntity.Id;
        }

        /// <summary>
        /// Udates a fournisseur in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="fournisseurId">The fournisseur id.</param>
        /// <param name="fournisseur">The fournisseur.</param>
        [HttpPut, Route("{fournisseurId:int}")]
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetAll")]
        public void Update(String clubName, Int32 fournisseurId, FournisseurDto fournisseur)
        {
            var fournisseurEntity = this.fournisseurRepository
                .GetUnique(fournisseur2 => fournisseur2.Club.Nom == clubName && fournisseur2.Id == fournisseurId)
                .MapFrom(fournisseur);
            this.fournisseurRepository.Update(fournisseurEntity);
        }

        /// <summary>
        /// Deletes a fournisseur from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="fournisseurId">The fournisseur id.</param>
        [HttpDelete, Route("{fournisseurId:int}")]
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetAll")]
        public void Delete(String clubName, Int32 fournisseurId)
        {
            // Somewhat trash call to make sure the fournisseur is in this context. 
            var fournisseurEntity = this.fournisseurRepository
                .GetUnique(fournisseur => clubName == fournisseur.Club.Nom && fournisseur.Id == fournisseurId);
            this.fournisseurRepository.Delete(fournisseurEntity);
        }
    }
}