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

    partial class ProfilAvance : IHasId<Int32>
    {
        public int Id { get { return ProfilId; } }
    }

    partial class Allergie : IHasId<Int32>
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
}