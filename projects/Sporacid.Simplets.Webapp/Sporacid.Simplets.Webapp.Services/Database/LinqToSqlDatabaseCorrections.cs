namespace Sporacid.Simplets.Webapp.Services.Database
{
    using System;
    using Sporacid.Simplets.Webapp.Core.Repositories;

    /// <summary>
    /// Diregard the name of this file.
    /// This file should be used to store linq to sql corrections to the generated dbml.
    /// </summary>
    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    partial class Adresse : IHasId<Int32>
    {
    }

    partial class Club : IHasId<Int32>
    {
    }

    partial class Membre : IHasId<Int32>
    {
    }

    partial class Commandite : IHasId<Int32>
    {
    }

    partial class Concentration : IHasId<Int32>
    {
    }

    partial class Contact : IHasId<Int32>
    {
    }

    partial class Evenement : IHasId<Int32>
    {
    }

    partial class Fournisseur : IHasId<Int32>
    {
    }

    partial class Item : IHasId<Int32>
    {
    }

    partial class TypeContact : IHasId<Int32>
    {
    }

    partial class StatutSuivie : IHasId<Int32>
    {
    }

    partial class Unite : IHasId<Int32>
    {
    }

    partial class Profil : IHasId<Int32>
    {
    }

    partial class Antecedent : IHasId<Int32>
    {
    }

    partial class Formation : IHasId<Int32>
    {
    }

    partial class Preference : IHasId<Int32>
    {
    }

    partial class Suivie : IHasId<Int32>
    {
    }

    partial class Groupe : IHasId<Int32>
    {
    }

    partial class Meeting : IHasId<Int32>
    {
    }

    partial class TypeCommanditaire : IHasId<Int32>
    {
    }

    partial class TypeCommandite : IHasId<Int32>
    {
    }

    partial class TypeFournisseur : IHasId<Int32>
    {
    }

    partial class TypeAntecedent : IHasId<Int32>
    {
    }

    partial class Commanditaire : IHasId<Int32>
    {
    }

    partial class GroupeMembre : IHasId<GroupeMembreId>
    {
        private GroupeMembreId id;
        public GroupeMembreId Id
        {
            get
            {
                return id ?? (id = new GroupeMembreId
                {
                    GroupeId = this.GroupeId,
                    MembreId = this.MembreId
                });
            }
        }
    }

    public class GroupeMembreId
    {
        public Int32 GroupeId { get; set; }
        public Int32 MembreId { get; set; }
    }
}