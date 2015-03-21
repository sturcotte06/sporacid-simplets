namespace Sporacid.Simplets.Webapp.Core.Security.Database.Repositories.Impl
{
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Core.Repositories.Impl;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class SecurityGenericRepository<TEntityId, TEntity> :
        GenericRepository<TEntityId, TEntity>, ISecurityRepository<TEntityId, TEntity> where TEntity : class, IHasId<TEntityId>
    {
        public SecurityGenericRepository(SecurityDataContext dataContext) : base(dataContext)
        {
        }
    }
}