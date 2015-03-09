namespace Sporacid.Simplets.Webapp.Services.Services.Clubs.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs;
    using WebApi.OutputCache.V2;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath + "/{clubName:alpha}/inventaire")]
    public class InventaireService : BaseService, IInventaireService
    {
        private readonly IRepository<Int32, Club> clubRepository;
        private readonly IRepository<Int32, Item> itemRepository;

        public InventaireService(IRepository<Int32, Item> itemRepository, IRepository<Int32, Club> clubRepository)
        {
            this.itemRepository = itemRepository;
            this.clubRepository = clubRepository;
        }

        /// <summary>
        /// Get all item entities from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="skip">Optional parameter. Specifies how many entities to skip.</param>
        /// <param name="take">Optional parameter. Specifies how many entities to take.</param>
        /// <returns>The item entities.</returns>
        [HttpGet, Route("")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Medium)]
        public IEnumerable<WithId<Int32, ItemDto>> GetAll(String clubName, [FromUri] UInt32? skip, [FromUri] UInt32? take)
        {
            return this.itemRepository
                .GetAll(item => clubName == item.Club.Nom)
                .OptionalSkipTake(skip, take)
                .MapAllWithIds<Item, ItemDto>();
        }

        /// <summary>
        /// Get an item entity from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="itemId">The item entity id.</param>
        /// <returns>The item entity.</returns>
        [HttpGet, Route("{itemId:int}")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Medium)]
        public ItemDto Get(String clubName, Int32 itemId)
        {
            return this.itemRepository
                .GetUnique(item => item.Club.Nom == clubName && item.Id == itemId)
                .MapTo<Item, ItemDto>();
        }

        /// <summary>
        /// Creates an item entity in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="item">The item entity.</param>
        /// <returns>The created item entity id.</returns>
        [HttpPost, Route("")]
        [InvalidateCacheOutput("GetAll")]
        public Int32 Create(String clubName, ItemDto item)
        {
            var clubEntity = this.clubRepository.GetUnique(club => clubName == club.Nom);
            var itemEntity = item.MapTo<ItemDto, Item>();

            // Make sure the item is created in this context.
            itemEntity.ClubId = clubEntity.Id;

            this.itemRepository.Add(itemEntity);
            return itemEntity.Id;
        }

        /// <summary>
        /// Updates an item entity in a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="itemId">The item entity id.</param>
        /// <param name="item">The item entity.</param>
        [HttpPut, Route("{itemId:int}")]
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetAll")]
        public void Update(String clubName, Int32 itemId, ItemDto item)
        {
            var itemEntity = this.itemRepository
                .GetUnique(item2 => item2.Club.Nom == clubName && item2.Id == itemId)
                .MapFrom(item);
            this.itemRepository.Update(itemEntity);
        }

        /// <summary>
        /// Deletes an item entity from a club context.
        /// </summary>
        /// <param name="clubName">The unique club name of the club entity.</param>
        /// <param name="itemId">The item entity id.</param>
        [HttpDelete, Route("{itemId:int}")]
        [InvalidateCacheOutput("Get"), InvalidateCacheOutput("GetAll")]
        public void Delete(String clubName, Int32 itemId)
        {
            // Somewhat trash call to make sure the item is in this context. 
            var itemEntity = this.itemRepository
                .GetUnique(item => clubName == item.Club.Nom && item.Id == itemId);
            this.itemRepository.Delete(itemEntity);
        }
    }
}