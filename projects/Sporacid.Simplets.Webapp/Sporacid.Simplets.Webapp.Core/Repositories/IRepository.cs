namespace Sporacid.Simplets.Webapp.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IRepository<in TEntityId, TEntity> : IDisposable where TEntity : IHasId<TEntityId>
    {
        /// <summary>
        /// The linq to sql data context.
        /// </summary>
        DataContext DataContext { get; }
        
        /// <summary>
        /// Returns all entities.
        /// </summary>
        /// <returns>All entities.</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Returns all entities matching the predicate.
        /// </summary>
        /// <param name="whereClause">Predicate to use as a where clause.</param>
        /// <returns>All entities matching the predicate.</returns>
        IQueryable<TEntity> GetAll(Predicate<TEntity> whereClause);

        /// <summary>
        /// Returns the entity with the given id.
        /// </summary>
        /// <param name="entityId">The entity id,</param>
        /// <returns>The entity with the given id.</returns>
        TEntity Get(TEntityId entityId);

        // /// <summary>
        // /// Returns the entity with the given id.
        // /// </summary>
        // /// <param name="entityId">The entity id,</param>
        // /// <returns>The entity with the given id.</returns>
        // TEntity Get<TEntityId>(TEntityId entityId) where TEntityId : ICompositeId;

        /// <summary>
        /// Adds an entity to the system.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Adds all entities to the system.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void AddAll(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes the entity with the given id.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        void Delete(TEntityId entityId);

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes all entities.
        /// </summary>
        void DeleteAll();

        /// <summary>
        /// Deletes all entities matching the predicate.
        /// </summary>
        /// <param name="whereClause">Predicate to use as a where clause.</param>
        void DeleteAll(Predicate<TEntity> whereClause);

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Updates all entities
        /// </summary>
        /// <param name="entities">The entities.</param>
        void UpdateAll(IEnumerable<TEntity> entities);
    }
}