using IKernel = Ninject.IKernel;
using ResolutionExtensions = Ninject.ResolutionExtensions;

namespace Sporacid.Simplets.Webapp.Services.Tests.IntegrationTests.Database
{
    using System;
    using System.Linq;
    using NUnit.Framework;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Services.Database;
    using Sporacid.Simplets.Webapp.Services.Tests.IntegrationTests.Ninject;
    using Sporacid.Simplets.Webapp.Tools.Collections;

    [TestFixture]
    public class ClubsSchemaIntegrationTests
    {
        private const int TestSize = 10;
        private readonly IKernel kernel = IntegrationTestModule.GetKernel();

        private void Test_Create_Clubs()
        {
            var clubRepository = ResolutionExtensions.Get<IRepository<Int32, Club>>(this.kernel);
            for (var iClub = 0; iClub < TestSize; iClub++)
            {
                var club = new Club()
                {
                    Nom = String.Format("Club #{0}", iClub + 1),
                    Description = String.Format("Description du club #{0}", iClub + 1)
                };

                Assert.DoesNotThrow(() => clubRepository.Add(club));
                Assert.IsTrue(clubRepository.Has(club));
            }
        }

        private void Test_Remove_One_Club()
        {
            var clubRepository = ResolutionExtensions.Get<IRepository<Int32, Club>>(this.kernel);
            var clubId = clubRepository.GetAll().First().Id;
            Assert.DoesNotThrow(() => clubRepository.Delete(clubId));
        }

        private void Test_Update_One_Club()
        {
            var clubRepository = ResolutionExtensions.Get<IRepository<Int32, Club>>(this.kernel);
            var club = clubRepository.GetAll().First();
            var newNom = club.Nom + " (Update)";
            club.Nom = newNom;
            Assert.DoesNotThrow(() => clubRepository.Update(club));
            Assert.AreEqual(clubRepository.Get(club.Id).Nom, newNom);
        }

        private void Test_Add_Membres()
        {
            var clubRepository = ResolutionExtensions.Get<IRepository<Int32, Club>>(this.kernel);
            clubRepository.GetAll().ForEach(club =>
            {
                for (var iMembre = 0; iMembre < TestSize; iMembre++)
                {
                    var membre = new Membre()
                    {
                        ClubId = club.Id,
                        CodeUniversel = String.Format("AJ{0:00000}", iMembre + 1),
                        DateDebut = DateTime.Now,
                        Titre = "Membre",
                        Actif = true
                    };

                    var membreRepository = ResolutionExtensions.Get<IRepository<Int32, Membre>>(this.kernel);
                    Assert.DoesNotThrow(() => membreRepository.Add(membre));
                    Assert.IsTrue(membreRepository.Has(membre));

                    var clubRepository2 = ResolutionExtensions.Get<IRepository<Int32, Club>>(this.kernel);
                    Assert.IsTrue(clubRepository2.Has(club2 => club2.Membres.Any(membre2 => membre2.Id == membre.Id)));
                }
            });
        }

        private void Test_Add_Items()
        {
            var uniteRepository = ResolutionExtensions.Get<IRepository<Int32, Unite>>(this.kernel);
            var clubRepository = ResolutionExtensions.Get<IRepository<Int32, Club>>(this.kernel);
            clubRepository.GetAll().ForEach(club =>
            {
                for (var iItem = 0; iItem < TestSize; iItem++)
                {
                    var item = new Item
                    {
                        ClubId = club.Id,
                        UniteId = uniteRepository.GetAll().First().Id,
                        Code = String.Format("IT{0:0000}", iItem + 1),
                        Quantite = 10,
                        QuantiteMin = 5,
                        QuantiteMax = 15,
                        Description = String.Format("Item description #{0}", iItem + 1)
                    };

                    var itemRepository = ResolutionExtensions.Get<IRepository<Int32, Item>>(this.kernel);
                    Assert.DoesNotThrow(() => itemRepository.Add(item));
                    Assert.IsTrue(itemRepository.Has(item));

                    var clubRepository2 = ResolutionExtensions.Get<IRepository<Int32, Club>>(this.kernel);
                    Assert.IsTrue(clubRepository2.Has(club2 => club2.Items.Any(item2 => item2.Id == item.Id)));
                }
            });
        }

        private void Test_Add_Fournisseurs()
        {
            var typeContactRepository = ResolutionExtensions.Get<IRepository<Int32, TypeContact>>(this.kernel);
            var itemRepository = ResolutionExtensions.Get<IRepository<Int32, Fournisseur>>(this.kernel);
            var clubRepository = ResolutionExtensions.Get<IRepository<Int32, Club>>(this.kernel);
            clubRepository.GetAll().ForEach(club =>
            {
                for (var iFournisseur = 0; iFournisseur < TestSize; iFournisseur++)
                {
                    var fournisseur = new Fournisseur
                    {
                        ClubId = club.Id,
                        Nom = String.Format("Fournisseur #{0}", iFournisseur + 1),
                        Adresse = new Adresse
                        {
                            NoCivique = (iFournisseur + 1)*10,
                            Rue = String.Format("Rue #{0}", iFournisseur + 1),
                            Ville = String.Format("Ville #{0}", iFournisseur + 1),
                            CodePostal = String.Format("A{0}A{0}A{0}", iFournisseur + 1),
                        },
                        Contact = new Contact
                        {
                            TypeContactId = typeContactRepository.GetAll().First().Id,
                            Prenom = String.Format("Prenom #{0}", iFournisseur + 1),
                            Nom = String.Format("Nom #{0}", iFournisseur + 1),
                            Telephone = String.Format("{0}{0}{0}{0}{0}{0}{0}{0}{0}{0}", (iFournisseur%10)),
                            Courriel = String.Format("email_{0}@domain.com", iFournisseur + 1),
                        }
                    };

                    var fournisseurRepository = ResolutionExtensions.Get<IRepository<Int32, Fournisseur>>(this.kernel);
                    Assert.DoesNotThrow(() => fournisseurRepository.Add(fournisseur));
                    Assert.IsTrue(fournisseurRepository.Has(fournisseur));

                    var clubRepository2 = ResolutionExtensions.Get<IRepository<Int32, Club>>(this.kernel);
                    Assert.IsTrue(clubRepository2.Has(club2 => club2.Fournisseurs.Any(fournisseur2 => fournisseur2.Id == fournisseur.Id)));
                }
            });
        }

        private void Test_Add_Commandites()
        {
            var itemRepository = ResolutionExtensions.Get<IRepository<Int32, Fournisseur>>(this.kernel);
            var fournisseurRepository = ResolutionExtensions.Get<IRepository<Int32, Fournisseur>>(this.kernel);
            var clubRepository = ResolutionExtensions.Get<IRepository<Int32, Club>>(this.kernel);
            clubRepository.GetAll().ForEach(club =>
            {
                for (var iCommandite = 0; iCommandite < TestSize; iCommandite++)
                {
                    var commandite = new Commandite
                    {
                        ClubId = club.Id,
                        FournisseurId = fournisseurRepository.GetAll().First().Id,
                        ItemId = itemRepository.GetAll().First().Id,
                        Nature = String.Format("Nature {0}", iCommandite + 1),
                        Valeur = 10*(iCommandite + 1)
                    };

                    var commanditeRepository = ResolutionExtensions.Get<IRepository<Int32, Commandite>>(this.kernel);
                    Assert.DoesNotThrow(() => commanditeRepository.Add(commandite));
                    Assert.IsTrue(commanditeRepository.Has(commandite));

                    var clubRepository2 = ResolutionExtensions.Get<IRepository<Int32, Club>>(this.kernel);
                    Assert.IsTrue(clubRepository2.Has(club2 => club2.Commandites.Any(commandite2 => commandite2.Id == commandite.Id)));
                }
            });
        }

        private void Test_Add_Meetings()
        {
        }

        private void Test_Add_Suivies()
        {
        }

        [Test]
        public void Execute_All_Tests()
        {
            this.Test_Create_Clubs();
            this.Test_Add_Membres();
            this.Test_Add_Items();
            this.Test_Add_Fournisseurs();
            this.Test_Add_Commandites();
            this.Test_Add_Suivies();
            this.Test_Add_Meetings();

            // this.Test_Remove_One_Club();
            // this.Test_Update_One_Club();
        }

        [TearDown]
        public void Destroy()
        {
            kernel.Dispose();
        }
    }
}