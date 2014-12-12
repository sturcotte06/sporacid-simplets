namespace Sporacid.Simplets.Webapp.Services.Repositories
{
    using Sporacid.Simplets.Webapp.Services.LinqToSql;
    using Sporacid.Simplets.Webapp.Services.Repositories.Dto;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IMembreRepository
    {
        Membre Get(int membreId);
        void Add(MembreDto membre);
        void Delete(int membreId);
        void Update(int membreId, MembreDto membre);
    }
}