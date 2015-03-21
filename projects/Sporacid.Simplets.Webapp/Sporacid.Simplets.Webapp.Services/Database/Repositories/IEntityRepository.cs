namespace Sporacid.Simplets.Webapp.Services.Database.Repositories
{
    using Sporacid.Simplets.Webapp.Core.Repositories;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IEntityRepository<in TEntityId, TEntity> : IRepository<TEntityId, TEntity> where TEntity : class, IHasId<TEntityId>
    {
    }
}