namespace Sporacid.Simplets.Webapp.Core.Repositories
{
    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IHasId<out TId>
    {
        /// <summary>
        /// Id of the entity.
        /// </summary>
        TId Id { get; }
    }
}