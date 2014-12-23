namespace Sporacid.Simplets.Webapp.Services.Services.Clubs.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Data.Linq.SqlClient;
    using System.Linq;
    using System.Web.Http;
    using AutoMapper;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/{clubName:alpha}/fournisseur")]
    public class FournisseurService : BaseService, IFournisseurService
    {
        private readonly IRepository<Int32, Club> clubRepository;
        private readonly IRepository<Int32, Fournisseur> fournisseurRepository;

        public FournisseurService(IRepository<Int32, Fournisseur> fournisseurRepository, IRepository<Int32, Club> clubRepository)
        {
            this.fournisseurRepository = fournisseurRepository;
            this.clubRepository = clubRepository;
        }

        /// <summary>
        /// Get all fournisseurs from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <returns>The fournisseur entities.</returns>
        [HttpGet]
        [Route("")]
        public IEnumerable<WithId<Int32, FournisseurDto>> GetAll(String clubName)
        {
            var fournisseurEntities = this.fournisseurRepository.GetAll(
                f => SqlMethods.Like(clubName, f.Club.Nom));
            return fournisseurEntities.Select(f =>
                new WithId<Int32, FournisseurDto>(f.Id, Mapper.Map<Fournisseur, FournisseurDto>(f)));
        }

        /// <summary>
        /// Get a fournisseur from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="fournisseurId">The fournisseur id.</param>
        /// <returns>The fournisseur.</returns>
        [HttpGet]
        [Route("{fournisseurId:int}")]
        public FournisseurDto Get(String clubName, Int32 fournisseurId)
        {
            var fournisseurEntity = this.fournisseurRepository.GetUnique(
                f => SqlMethods.Like(f.Club.Nom, clubName) && f.Id == fournisseurId);
            return Mapper.Map<Fournisseur, FournisseurDto>(fournisseurEntity);
        }

        /// <summary>
        /// Creates a fournisseur in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="fournisseur">The fournisseur.</param>
        /// <returns>The created fournisseur id.</returns>
        [HttpPost]
        [Route("")]
        public Int32 Create(String clubName, FournisseurDto fournisseur)
        {
            var clubEntity = this.clubRepository.GetUnique(c => SqlMethods.Like(clubName, c.Nom));
            var fournisseurEntity = Mapper.Map<FournisseurDto, Fournisseur>(fournisseur);

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
        [HttpPut]
        [Route("{fournisseurId:int}")]
        public void Update(String clubName, Int32 fournisseurId, FournisseurDto fournisseur)
        {
            var fournisseurEntity = this.fournisseurRepository.GetUnique(
                f => SqlMethods.Like(f.Club.Nom, clubName) && f.Id == fournisseurId);
            Mapper.Map(fournisseur, fournisseurEntity);
            this.fournisseurRepository.Update(fournisseurEntity);
        }

        /// <summary>
        /// Deletes a fournisseur from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="fournisseurId">The fournisseur id.</param>
        [HttpDelete]
        [Route("{fournisseurId:int}")]
        public void Delete(String clubName, Int32 fournisseurId)
        {
            // Somewhat trash call to make sure the fournisseur is in this context. 
            var fournisseurEntity = this.fournisseurRepository.GetUnique(f =>
                SqlMethods.Like(clubName, f.Club.Nom) && f.Id == fournisseurId);
            this.fournisseurRepository.Delete(fournisseurEntity);
        }
    }
}