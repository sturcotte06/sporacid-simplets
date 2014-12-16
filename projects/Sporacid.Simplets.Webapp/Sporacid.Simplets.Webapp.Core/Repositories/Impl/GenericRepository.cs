namespace Sporacid.Simplets.Webapp.Core.Repositories.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Repositories;
    using Sporacid.Simplets.Webapp.Tools.Collections;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class GenericRepository<TEntityId, TEntity> : IRepository<TEntityId, TEntity>
        where TEntity : class, IHasId<TEntityId>
    {
        private volatile bool isDisposed;

        public GenericRepository(DataContext dataContext)
        {
            this.DataContext = dataContext;
            this.Table = this.DataContext.GetTable<TEntity>();
        }

        /// <summary>
        /// The table object from which the repository pulls data.
        /// </summary>
        private ITable<TEntity> Table { get; set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this.isDisposed) throw new ObjectDisposedException(this.GetType().FullName);

            // Make sure we do not lose any changes. 
            // This is intended to be called regardless of commit behaviour.
            this.Commit();

            this.DataContext.Dispose();
            this.isDisposed = true;
        }

        /// <summary>
        /// The linq to sql data context.
        /// </summary>
        public DataContext DataContext { get; private set; }

        /// <summary>
        /// Returns all entities.
        /// </summary>
        /// <returns>All entities.</returns>
        public IQueryable<TEntity> GetAll()
        {
            return this.Table;
        }

        /// <summary>
        /// Returns all entities matching the predicate.
        /// </summary>
        /// <param name="whereClause">Predicate to use as a where clause.</param>
        /// <returns>All entities matching the predicate.</returns>
        public IQueryable<TEntity> GetAll(Predicate<TEntity> whereClause)
        {
            return this.Table.Where(e => whereClause(e));
        }

        /// <summary>
        /// Returns the entity with the given id.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        /// <returns>The entity with the given id.</returns>
        public TEntity Get(TEntityId entityId)
        {
            var entity = this.Table.SingleOrDefault(e => e.Id.Equals(entityId));
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
        public TEntity GetUnique(Predicate<TEntity> whereClause)
        {
            var entity =  this.GetAll(whereClause).SingleOrDefault();
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
            this.Table.InsertOnSubmit(entity);
            this.Commit();
        }

        /// <summary>
        /// Adds all entities to the system.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>The ids of the new entities.</returns>
        public void AddAll(IEnumerable<TEntity> entities)
        {
            entities.ForEach(this.Add);
        }

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(TEntity entity)
        {
            this.Table.DeleteOnSubmit(entity);
            this.Commit();
        }

        /// <summary>
        /// Deletes all entities.
        /// </summary>
        public void DeleteAll()
        {
            this.Table.ForEach(this.Delete);
        }

        /// <summary>
        /// Deletes all entities matching the predicate.
        /// </summary>
        /// <param name="whereClause">Predicate to use as a where clause.</param>
        public void DeleteAll(Predicate<TEntity> whereClause)
        {
            this.Table.Where(e => whereClause(e))
                .ForEach(this.Delete);
        }

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(TEntity entity)
        {
            // this.Table.Attach(entity);
            this.Commit();
        }

        /// <summary>
        /// Updates all entities
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void UpdateAll(IEnumerable<TEntity> entities)
        {
            entities.ForEach(this.Update);
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
            this.DataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);
        }
    }
}