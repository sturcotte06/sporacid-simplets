namespace Sporacid.Simplets.Webapp.Services.Repositories.Impl
{
    using System.Linq;
    using Sporacid.Simplets.Webapp.Services.LinqToSql;
    using Sporacid.Simplets.Webapp.Services.Repositories.Dto;
    using Sporacid.Simplets.Webapp.Services.Repositories.Exceptions;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class MembreRepository : BaseRepository, IMembreRepository
    {
        public MembreRepository(DatabaseDataContext dataContext, CommitBehaviour commitBehaviour) : base(dataContext, commitBehaviour)
        {
        }

        public Membre Get(int membreId)
        {
            var membre = this.DataContext.Membres.FirstOrDefault(m => m.Id == membreId);
            if (membre == null)
            {
                throw new EntityNotFoundException<Membre>(membreId);
            }

            return membre;
        }

        public void Add(MembreDto membreDto)
        {
            var membre = new Membre
            {
                Prenom = membreDto.Prenom,
                Nom = membreDto.Nom,
                Telephone = membreDto.Telephone,
                CodeUniversel = membreDto.CodeUniversel,
                Actif = membreDto.Actif,
            };

            this.DataContext.Membres.InsertOnSubmit(membre);
            if (this.CommitBehaviour == CommitBehaviour.Automatic) this.CommitAll();
        }

        public void Delete(int membreId)
        {
            this.DataContext.Membres.DeleteOnSubmit(this.Get(membreId));
            if (this.CommitBehaviour == CommitBehaviour.Automatic) this.CommitAll();
        }

        public void Update(int membreId, MembreDto membreDto)
        {
            var membre = this.Get(membreId);
            membre.Prenom = membreDto.Prenom;
            membre.Nom = membreDto.Nom;
            membre.Telephone = membreDto.Telephone;
            membre.CodeUniversel = membreDto.CodeUniversel;
            membre.Actif = membreDto.Actif;

            if (this.CommitBehaviour == CommitBehaviour.Automatic) this.CommitAll();
        }
    }
}