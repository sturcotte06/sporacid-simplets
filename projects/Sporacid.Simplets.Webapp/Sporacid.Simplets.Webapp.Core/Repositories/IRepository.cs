namespace Sporacid.Simplets.Webapp.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IRepository<in TEntityId, TEntity> where TEntity : IHasId<TEntityId>
    {
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
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> whereClause);

        /// <summary>
        /// Whether the entity with id exists or not.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        /// <returns>Whether the entity with id exists or not</returns>
        bool Has(TEntityId entityId);

        /// <summary>
        /// Whether the entity exists or not.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Whether the entity exists or not</returns>
        bool Has(TEntity entity);

        /// <summary>
        /// Whether the entity exists or not.
        /// </summary>
        /// <param name="whereClause">Predicate to use as a where clause.</param>
        /// <returns>Whether the entity exists or not</returns>
        bool Has(Expression<Func<TEntity, bool>> whereClause);

        /// <summary>
        /// Returns the entity with the given id.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        /// <returns>The entity with the given id.</returns>
        TEntity Get(TEntityId entityId);

        /// <summary>
        /// Returns the entity that matches the predicate.
        /// </summary>
        /// <param name="whereClause">The entity id.</param>
        /// <returns>The entity that matches the given predicate.</returns>
        TEntity GetUnique(Expression<Func<TEntity, bool>> whereClause);

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
        void DeleteAll(Expression<Func<TEntity, bool>> whereClause);

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