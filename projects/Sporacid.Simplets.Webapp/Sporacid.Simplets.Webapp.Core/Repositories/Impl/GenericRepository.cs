namespace Sporacid.Simplets.Webapp.Core.Repositories.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;
    using System.Linq.Expressions;
    using LinqKit;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Repositories;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class GenericRepository<TEntityId, TEntity> : IRepository<TEntityId, TEntity> where TEntity : class, IHasId<TEntityId>
    {
        private readonly IDataContextStore dataContextStore;

        public GenericRepository(IDataContextStore dataContextStore)
        {
            this.dataContextStore = dataContextStore;
        }

        /// <summary>
        /// Returns all entities.
        /// </summary>
        /// <returns>All entities.</returns>
        public IQueryable<TEntity> GetAll()
        {
            return this.dataContextStore.DataContext.GetTable<TEntity>();
        }

        /// <summary>
        /// Returns all entities matching the predicate.
        /// </summary>
        /// <param name="whereClause">Predicate to use as a where clause.</param>
        /// <returns>All entities matching the predicate.</returns>
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> whereClause)
        {
            return this.dataContextStore.DataContext.GetTable<TEntity>().Where(whereClause);
        }

        /// <summary>
        /// Whether the entity with id exists or not.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        /// <returns>Whether the entity with id exists or not</returns>
        public bool Has(TEntityId entityId)
        {
            return this.dataContextStore.DataContext.GetTable<TEntity>()
                .Any(e => e.Id.Equals(entityId));
        }

        /// <summary>
        /// Whether the entity exists or not.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Whether the entity exists or not</returns>
        public bool Has(TEntity entity)
        {
            return Has(entity.Id);
        }

        /// <summary>
        /// Whether the entity exists or not.
        /// </summary>
        /// <param name="whereClause">Predicate to use as a where clause.</param>
        /// <returns>Whether the entity exists or not</returns>
        public bool Has(Expression<Func<TEntity, bool>> whereClause)
        {
            return this.dataContextStore.DataContext.GetTable<TEntity>()
                .AsExpandable().Any(whereClause);
        }

        /// <summary>
        /// Returns the entity with the given id.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        /// <returns>The entity with the given id.</returns>
        public TEntity Get(TEntityId entityId)
        {
            var entity = this.dataContextStore.DataContext.GetTable<TEntity>().SingleOrDefault(e => e.Id.Equals(entityId));
            if (entity == null)
            {
                throw new EntityNotFoundException<TEntity>(entityId);
            }

            return entity;
        }

        /// <summary>
        /// Returns the entity that matches the predicate.
        /// </summary>
        /// <param name="whereClause">The entity id.</param>
        /// <returns>The entity that matches the given predicate.</returns>
        public TEntity GetUnique(Expression<Func<TEntity, bool>> whereClause)
        {
            var entities = this.GetAll(whereClause);
            if (!entities.Any())
            {
                throw new EntityNotFoundException<TEntity>();
            }

            var entity = entities.SingleOrDefault();
            if (entity == null)
            {
                throw new EntityNotUniqueException();
            }

            return entity;
        }

        /// <summary>
        /// Adds an entity to the system.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The id of the new entity.</returns>
        public void Add(TEntity entity)
        {
            this.dataContextStore.DataContext.GetTable<TEntity>().InsertOnSubmit(entity);
            this.Commit();
        }

        /// <summary>
        /// Adds all entities to the system.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>The ids of the new entities.</returns>
        public void AddAll(IEnumerable<TEntity> entities)
        {
            this.dataContextStore.DataContext.GetTable<TEntity>().InsertAllOnSubmit(entities);
            this.Commit();
        }

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(TEntity entity)
        {
            this.dataContextStore.DataContext.GetTable<TEntity>().DeleteOnSubmit(entity);
            this.Commit();
        }

        /// <summary>
        /// Deletes all entities.
        /// </summary>
        public void DeleteAll()
        {
            var table = this.dataContextStore.DataContext.GetTable<TEntity>();
            this.dataContextStore.DataContext.GetTable<TEntity>().DeleteAllOnSubmit(table);
            this.Commit();
        }

        /// <summary>
        /// Deletes all entities matching the predicate.
        /// </summary>
        /// <param name="whereClause">Predicate to use as a where clause.</param>
        public void DeleteAll(Expression<Func<TEntity, bool>> whereClause)
        {
            var table = this.dataContextStore.DataContext.GetTable<TEntity>();
            table.DeleteAllOnSubmit(table.AsExpandable().Where(whereClause));
            this.Commit();
        }

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(TEntity entity)
        {
            // dataContextStore.DataContext.GetTable<TEntity>().Attach(entity, true);
            this.Commit();
        }

        /// <summary>
        /// Updates all entities
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void UpdateAll(IEnumerable<TEntity> entities)
        {
            // dataContextStore.DataContext.GetTable<TEntity>().AttachAll(entities, true);
            this.Commit();
        }

        /// <summary>
        /// Deletes the entity with the given id.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        public void Delete(TEntityId entityId)
        {
            var entity = this.Get(entityId);
            this.Delete(entity);
        }

        /// <summary>
        /// Commits all pending changes.
        /// </summary>
        private void Commit()
        {
            var commitSucceeded = false;
            while (!commitSucceeded)
            {
                try
                {
                    // Commit.
                    this.dataContextStore.DataContext.SubmitChanges(ConflictMode.ContinueOnConflict);
                    commitSucceeded = true;
                }
                catch (ChangeConflictException)
                {
                    // Handle conflicts.
                    // We'll take the latest values. Let team members handle conflicts between themselves.
                    foreach (var conflict in this.dataContextStore.DataContext.ChangeConflicts)
                    {
                        conflict.Resolve(RefreshMode.KeepCurrentValues);
                    }
                }
            }
        }
    }
}