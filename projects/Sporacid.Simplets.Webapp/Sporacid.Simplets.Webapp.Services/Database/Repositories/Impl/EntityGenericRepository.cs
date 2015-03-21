namespace Sporacid.Simplets.Webapp.Services.Database.Repositories.Impl
{
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Core.Repositories.Impl;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class EntityGenericRepository<TEntityId, TEntity> : GenericRepository<TEntityId, TEntity>, IEntityRepository<TEntityId, TEntity>
        where TEntity : class, IHasId<TEntityId>
    {
        public EntityGenericRepository(DatabaseDataContext dataContext)
            : base(dataContext)
        {
        }
    }
}