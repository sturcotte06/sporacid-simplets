namespace Sporacid.Simplets.Webapp.Services.Services
{
    using System;
    using System.Linq;
    using AutoMapper;
    using NLog.Time;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Applies an optional skip and take.
        /// If skip and take are null, this is a no-op.
        /// </summary>
        /// <typeparam name="TQueryResult">Type of the query.</typeparam>
        /// <param name="query">The query on which to apply skip and take.</param>
        /// <param name="skip">The amount of element to skip.</param>
        /// <param name="take">The amount of element to take.</param>
        /// <returns>The new query.</returns>
        public static IQueryable<TQueryResult> OptionalSkipTake<TQueryResult>(this IQueryable<TQueryResult> query, UInt32? skip, UInt32? take)
        {
            // If skip and take are defined, apply skip and take operation.
            // Else, just return the query.
            return (skip != null && take != null)
                ? query.Skip((Int32) skip).Take((Int32) take)
                : query;
        }

        /// <summary>
        /// Maps all entities of a source query into a query of destination entity.
        /// </summary>
        /// <typeparam name="TEntitySource">Type of the source entity.</typeparam>
        /// <typeparam name="TEntityDestination">Type of the destination entity.</typeparam>
        /// <param name="sourceEntitiesQuery">The source entities query.</param>
        /// <returns>The query for destination entities.</returns>
        public static IQueryable<TEntityDestination> MapAll<TEntitySource, TEntityDestination>(this IQueryable<TEntitySource> sourceEntitiesQuery)
        {
            return sourceEntitiesQuery
                .Select(sourceEntity => Mapper.Map<TEntitySource, TEntityDestination>(sourceEntity));
        }

        /// <summary>
        /// Maps all entities of a source query to a query of destination entity.
        /// All destination entity will be paired with the source's id.
        /// </summary>
        /// <typeparam name="TEntitySource">Type of the source entity.</typeparam>
        /// <typeparam name="TEntityDestination">Type of the destination entity.</typeparam>
        /// <param name="sourceEntitiesQuery">The source entities query.</param>
        /// <returns>The query for destination entities with their ids.</returns>
        public static IQueryable<WithId<Int32, TEntityDestination>> MapAllWithIds<TEntitySource, TEntityDestination>(this IQueryable<TEntitySource> sourceEntitiesQuery) where TEntitySource : IHasId<Int32>
        {
            return sourceEntitiesQuery.MapAllWithIds<TEntitySource, Int32, TEntityDestination>();
        }

        /// <summary>
        /// Maps all entities of a source query to a query of destination entity.
        /// All destination entity will be paired with the source's id.
        /// </summary>
        /// <typeparam name="TEntitySource">Type of the source entity.</typeparam>
        /// <typeparam name="TEntitySourceId">Type of the source entity id.</typeparam>
        /// <typeparam name="TEntityDestination">Type of the destination entity.</typeparam>
        /// <param name="sourceEntitiesQuery">The source entities query.</param>
        /// <returns>The query for destination entities with their ids.</returns>
        public static IQueryable<WithId<TEntitySourceId, TEntityDestination>> MapAllWithIds<TEntitySource, TEntitySourceId, TEntityDestination>(this IQueryable<TEntitySource> sourceEntitiesQuery) where TEntitySource : IHasId<TEntitySourceId>
        {
            return sourceEntitiesQuery
                .Select(sourceEntity =>
                    new WithId<TEntitySourceId, TEntityDestination>(
                        sourceEntity.Id,
                        Mapper.Map<TEntitySource, TEntityDestination>(sourceEntity)));
        }

        /// <summary>
        // Maps a source entity to a destination entity.
        /// </summary>
        /// <typeparam name="TEntitySource">Type of the source entity.</typeparam>
        /// <typeparam name="TEntityDestination">Type of the destination entity.</typeparam>
        /// <param name="sourceEntity">The source entity.</param>
        /// <returns>The destination entity.</returns>
        public static TEntityDestination MapTo<TEntitySource, TEntityDestination>(this TEntitySource sourceEntity)
        {
            return Mapper.Map<TEntitySource, TEntityDestination>(sourceEntity);
        }

        /// <summary>
        // Maps a source entity to a destination entity.
        /// </summary>
        /// <typeparam name="TEntitySource">Type of the source entity.</typeparam>
        /// <typeparam name="TEntityDestination">Type of the destination entity.</typeparam>
        /// <param name="sourceEntity">The source entity.</param>
        /// <param name="destinationEntity">The destination entity.</param>
        public static void MapInto<TEntitySource, TEntityDestination>(this TEntitySource sourceEntity, TEntityDestination destinationEntity)
        {
            Mapper.Map(sourceEntity, destinationEntity);
        }

        /// <summary>
        // Maps a source entity to a destination entity.
        /// </summary>
        /// <typeparam name="TEntitySource">Type of the source entity.</typeparam>
        /// <typeparam name="TEntityDestination">Type of the destination entity.</typeparam>
        /// <param name="sourceEntity">The source entity.</param>
        /// <param name="destinationEntity">The destination entity.</param>
        /// <returns>The modified destination entity.</returns>
        public static TEntityDestination MapFrom<TEntitySource, TEntityDestination>(this TEntityDestination destinationEntity, TEntitySource sourceEntity)
        {
            Mapper.Map(sourceEntity, destinationEntity);
            return destinationEntity;
        }
    }
}