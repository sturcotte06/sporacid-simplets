namespace Sporacid.Simplets.Webapp.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Core.Objects.DataClasses;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Linq.Expressions;
    using Sporacid.Simplets.Webapp.Core.Resources.Contracts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClass(typeof (RepositoryContract<,>))]
    public interface IRepository<in TEntityId, TEntity> : IDisposable where TEntity : class, IHasId<TEntityId>
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
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, Boolean>> whereClause);

        /// <summary>
        /// Whether the entity with id exists or not.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        /// <returns>Whether the entity with id exists or not</returns>
        Boolean Has(TEntityId entityId);

        /// <summary>
        /// Whether the entity exists or not.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Whether the entity exists or not</returns>
        Boolean Has(TEntity entity);

        /// <summary>
        /// Whether the entity exists or not.
        /// </summary>
        /// <param name="whereClause">Predicate to use as a where clause.</param>
        /// <returns>Whether the entity exists or not</returns>
        Boolean Has(Expression<Func<TEntity, Boolean>> whereClause);

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
        TEntity GetUnique(Expression<Func<TEntity, Boolean>> whereClause);

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
        void DeleteAll(Expression<Func<TEntity, Boolean>> whereClause);

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

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IRepository<,>))]
    internal abstract class RepositoryContract<TEntityId, TEntity> : IRepository<TEntityId, TEntity> where TEntity : class, IHasId<TEntityId>
    {
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// Returns all entities.
        /// </summary>
        /// <returns>All entities.</returns>
        public IQueryable<TEntity> GetAll()
        {
            // Postconditions.
            Contract.Ensures(Contract.Result<IQueryable<TEntity>>() != null, ContractStrings.Repository_GetAll_EnsuresNonNullEntities);
            
            // Dummy return.
            return default(IQueryable<TEntity>);
        }

        /// <summary>
        /// Returns all entities matching the predicate.
        /// </summary>
        /// <param name="whereClause">Predicate to use as a where clause.</param>
        /// <returns>All entities matching the predicate.</returns>
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, Boolean>> whereClause)
        {
            // Preconditions.
            Contract.Requires(whereClause != null, ContractStrings.Repository_GetAll_RequiresWhereClause);

            // Postconditions.
            Contract.Ensures(Contract.Result<IQueryable<TEntity>>() != null, ContractStrings.Repository_GetAll_EnsuresNonNullEntities);

            // Dummy return.
            return default(IQueryable<TEntity>);
        }

        /// <summary>
        /// Whether the entity with id exists or not.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        /// <returns>Whether the entity with id exists or not</returns>
        public Boolean Has(TEntityId entityId)
        {
            // Preconditions.
            Contract.Requires(!entityId.Equals(default(TEntityId)), ContractStrings.Repository_Has_RequiresEntityId);

            // Dummy return.
            return default(Boolean);
        }

        /// <summary>
        /// Whether the entity exists or not.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Whether the entity exists or not</returns>
        public Boolean Has(TEntity entity)
        {
            // Preconditions.
            Contract.Requires(entity != null, ContractStrings.Repository_Has_RequiresEntity);

            // Dummy return.
            return default(Boolean);
        }

        /// <summary>
        /// Whether the entity exists or not.
        /// </summary>
        /// <param name="whereClause">Predicate to use as a where clause.</param>
        /// <returns>Whether the entity exists or not</returns>
        public Boolean Has(Expression<Func<TEntity, Boolean>> whereClause)
        {
            // Preconditions.
            Contract.Requires(whereClause != null, ContractStrings.Repository_Has_RequiresWhereClause);

            // Dummy return.
            return default(Boolean);
        }

        /// <summary>
        /// Returns the entity with the given id.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        /// <returns>The entity with the given id.</returns>
        public TEntity Get(TEntityId entityId)
        {
            // Preconditions.
            Contract.Requires(!entityId.Equals(default(TEntityId)), ContractStrings.Repository_Get_RequiresEntityId);

            // Postconditions.
            Contract.Ensures(Contract.Result<TEntity>() != null, ContractStrings.Repository_Get_EnsuresNonNullEntity);

            // Dummy return.
            return default(TEntity);
        }

        /// <summary>
        /// Returns the entity that matches the predicate.
        /// </summary>
        /// <param name="whereClause">The entity id.</param>
        /// <returns>The entity that matches the given predicate.</returns>
        public TEntity GetUnique(Expression<Func<TEntity, Boolean>> whereClause)
        {
            // Preconditions.
            Contract.Requires(whereClause != null, ContractStrings.Repository_GetUnique_RequiresWhereClause);

            // Postconditions.
            Contract.Ensures(Contract.Result<TEntity>() != null, ContractStrings.Repository_GetUnique_EnsuresNonNullEntity);

            // Dummy return.
            return default(TEntity);
        }

        /// <summary>
        /// Adds an entity to the system.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Add(TEntity entity)
        {
            // Preconditions.
            Contract.Requires(entity != null, ContractStrings.Repository_Add_RequiresEntity);
        }

        /// <summary>
        /// Adds all entities to the system.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void AddAll(IEnumerable<TEntity> entities)
        {
            // Preconditions.
            Contract.Requires(entities != null && Contract.ForAll(entities, entity => entity != null),
                ContractStrings.Repository_AddAll_RequiresEntities);
        }

        /// <summary>
        /// Deletes the entity with the given id.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        public void Delete(TEntityId entityId)
        {
            // Preconditions.
            Contract.Requires(!entityId.Equals(default(TEntityId)), ContractStrings.Repository_Delete_RequiresEntityId);
        }

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(TEntity entity)
        {
            // Preconditions.
            Contract.Requires(entity != null, ContractStrings.Repository_Delete_RequiresEntity);
        }

        /// <summary>
        /// Deletes all entities.
        /// </summary>
        public abstract void DeleteAll();

        /// <summary>
        /// Deletes all entities matching the predicate.
        /// </summary>
        /// <param name="whereClause">Predicate to use as a where clause.</param>
        public void DeleteAll(Expression<Func<TEntity, Boolean>> whereClause)
        {
            // Preconditions.
            Contract.Requires(whereClause != null, ContractStrings.Repository_DeleteAll_RequiresWhereClause);
        }

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(TEntity entity)
        {
            // Preconditions.
            Contract.Requires(entity != null, ContractStrings.Repository_Update_RequiresEntity);
        }

        /// <summary>
        /// Updates all entities
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void UpdateAll(IEnumerable<TEntity> entities)
        {
            // Preconditions.
            Contract.Requires(entities != null && Contract.ForAll(entities, entity => entity != null),
                ContractStrings.Repository_UpdateAll_RequiresEntities);
        }
    }
}