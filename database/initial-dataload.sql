USE [SIMPLETS]
GO

-- INSERT INTO [dbo].[Clubs] VALUES ('PRECI','Programme de Regroupement Étudiant pour la Coopération Internationale', DEFAULT);
-- INSERT INTO [dbo].[Clubs] VALUES ('LiETS','Ligue d`Improvisation de l`ETS', DEFAULT);
-- INSERT INTO [dbo].[Clubs] VALUES ('Evolution','Véhicule à faible consommation', DEFAULT);
-- INSERT INTO [dbo].[Clubs] VALUES ('Baja','Véhicule amphibie tout terrain', DEFAULT);

INSERT INTO [dbo].[Concentrations] VALUES ('CTN','Génie de la construction');
INSERT INTO [dbo].[Concentrations] VALUES ('ELE','Génie électrique');
INSERT INTO [dbo].[Concentrations] VALUES ('GOL','Génie des opérations et de la logistique');
INSERT INTO [dbo].[Concentrations] VALUES ('GPA','Génie de production automatisée');
INSERT INTO [dbo].[Concentrations] VALUES ('LOG','Génie logiciel');
INSERT INTO [dbo].[Concentrations] VALUES ('MEC','Génie mécanique');
INSERT INTO [dbo].[Concentrations] VALUES ('TI','Génie des technologies de l`information');

-- INSERT INTO [dbo].[Membres] VALUES (1,'AJ00689','Olsen','Patrick','nibh.Aliquam@tristiquesenectus.co.uk','3716562099',1, DEFAULT);
-- INSERT INTO [dbo].[Membres] VALUES (2,'AZ92060','Melendez','Moses','Donec.consectetuer.mauris@mattisvelit.edu','1881189845',1, DEFAULT);
-- INSERT INTO [dbo].[Membres] VALUES (3,'AZ35441','Cote','Giacomo','velit.Cras.lorem@duiSuspendisseac.ca','6534742324',1, DEFAULT);
-- INSERT INTO [dbo].[Membres] VALUES (4,'AX85616','Ford','Jade','sem.ut.dolor@nonbibendum.co.uk','8522879201',1, DEFAULT);
-- INSERT INTO [dbo].[Membres] VALUES (5,'AJ32898','Mccarty','Chandler','in.dolor@atpretiumaliquet.org','3055537869',1, DEFAULT);

INSERT INTO [dbo].[Unites] VALUES ('Mètre','SI');
INSERT INTO [dbo].[Unites] VALUES ('Centimètre','SI');
INSERT INTO [dbo].[Unites] VALUES ('Kilomètre','SI');
INSERT INTO [dbo].[Unites] VALUES ('Gramme','SI');
INSERT INTO [dbo].[Unites] VALUES ('Kilogramme','SI');

-- INSERT INTO [dbo].[Items] VALUES (1,'urna justo faucibus lectus, a',1,3,1,15, DEFAULT);
-- INSERT INTO [dbo].[Items] VALUES (2,'vehicula aliquet libero. Integer in',2,4,1,14, DEFAULT);
-- INSERT INTO [dbo].[Items] VALUES (3,'in lobortis tellus justo sit',3,5,1,21, DEFAULT);
-- INSERT INTO [dbo].[Items] VALUES (4,'at, velit. Pellentesque ultricies dignissim',4,10,1,14, DEFAULT);
-- 
-- INSERT INTO [dbo].[Adresses] VALUES (1,'P.O. Box 733, 4867 Pellentesque. Av.',1,'Greenwich','26231', DEFAULT);
-- INSERT INTO [dbo].[Adresses] VALUES (2,'6206 Suspendisse Avenue',2,'Breton','22078', DEFAULT);
-- INSERT INTO [dbo].[Adresses] VALUES (3,'P.O. Box 932, 5843 Donec Road',3,'Borgerhout','21679', DEFAULT);
-- INSERT INTO [dbo].[Adresses] VALUES (4,'8448 Mollis. St.',4,'Anchorage','5048', DEFAULT);
-- INSERT INTO [dbo].[Adresses] VALUES (5,'P.O. Box 585, 7672 Enim. Avenue',5,'Cargovil','0729LE', DEFAULT);
-- INSERT INTO [dbo].[Adresses] VALUES (6,'928-6538 Cursus. Avenue',6,'Attimis','76747', DEFAULT);
-- INSERT INTO [dbo].[Adresses] VALUES (7,'388-2939 Enim Rd.',7,'Stokkem','56673-407', DEFAULT);
-- INSERT INTO [dbo].[Adresses] VALUES (8,'566-6018 Lorem St.',8,'Barbania','7329WQ', DEFAULT);
-- INSERT INTO [dbo].[Adresses] VALUES (9,'1311 Torquent Av.',9,'Merbes-Sainte-Marie','H0H 4M9', DEFAULT);
-- INSERT INTO [dbo].[Adresses] VALUES (10,'Ap #318-7322 Vivamus St.',10,'Serskamp','97859-876', DEFAULT);
-- 
-- INSERT INTO [dbo].[Allergies] VALUES ('nibh. Phasellus nulla. Integer vulputate,', DEFAULT);
-- INSERT INTO [dbo].[Allergies] VALUES ('scelerisque sed, sapien. Nunc pulvinar', DEFAULT);
-- INSERT INTO [dbo].[Allergies] VALUES ('id nunc interdum feugiat. Sed', DEFAULT);
-- INSERT INTO [dbo].[Allergies] VALUES ('per inceptos hymenaeos. Mauris ut', DEFAULT);
-- INSERT INTO [dbo].[Allergies] VALUES ('Aliquam auctor, velit eget laoreet', DEFAULT);
-- INSERT INTO [dbo].[Allergies] VALUES ('tellus sem mollis dui, in', DEFAULT);
-- 
-- INSERT INTO [dbo].[Fournisseurs] VALUES (1,'Bibendum Fermentum Company','Gilbert, Sean Q.','(805) 561-7970','cubilia.Curae.Phasellus@cubilia.org', DEFAULT);
-- INSERT INTO [dbo].[Fournisseurs] VALUES (2,'Non Corporation','Yates, Cailin E.','(574) 670-8278','Curae.Phasellus@aliquamenimnec.edu', DEFAULT);
-- INSERT INTO [dbo].[Fournisseurs] VALUES (3,'Imperdiet Corp.','Jennings, Brynn P.','(541) 442-7761','Etiam.laoreet.libero@dignissimMaecenas.net', DEFAULT);
-- INSERT INTO [dbo].[Fournisseurs] VALUES (4,'Adipiscing Enim Corporation','Hickman, Wade M.','(268) 523-2179','eget@montesnasceturridiculus.edu', DEFAULT);
-- INSERT INTO [dbo].[Fournisseurs] VALUES (5,'Urna Nunc Quis Company','Douglas, Dustin M.','(318) 662-4800','Phasellus.in.felis@pellentesquemassalobortis.com', DEFAULT);

-- INSERT INTO [dbo].[Commandites] VALUES (1,1,1,908,'ut, pellentesque eget, dictum placerat,', DEFAULT);
-- INSERT INTO [dbo].[Commandites] VALUES (2,2,2,9686,'posuere vulputate, lacus. Cras interdum.', DEFAULT);
-- INSERT INTO [dbo].[Commandites] VALUES (3,3,3,1628,'feugiat placerat velit. Quisque varius.', DEFAULT);
-- INSERT INTO [dbo].[Commandites] VALUES (4,4,4,5702,'molestie dapibus ligula. Aliquam erat', DEFAULT);

INSERT INTO [dbo].[TypesContact] VALUES ('Mère');
INSERT INTO [dbo].[TypesContact] VALUES ('Père');
INSERT INTO [dbo].[TypesContact] VALUES ('Enfant');
INSERT INTO [dbo].[TypesContact] VALUES ('Ami');
INSERT INTO [dbo].[TypesContact] VALUES ('Client');
INSERT INTO [dbo].[TypesContact] VALUES ('Autre');

-- INSERT INTO [dbo].[ContactsUrgence] VALUES (1,1,'Hoover','Zephania','1-772-913-2130', DEFAULT);
-- INSERT INTO [dbo].[ContactsUrgence] VALUES (2,1,'Gibson','Wynne','1-727-710-9565', DEFAULT);
-- INSERT INTO [dbo].[ContactsUrgence] VALUES (3,1,'Ryan','Athena','1-462-616-1288', DEFAULT);
-- INSERT INTO [dbo].[ContactsUrgence] VALUES (1,1,'Harper','Nissim','1-702-829-5337', DEFAULT);

-- INSERT INTO [dbo].[ContactsUrgenceMembres] VALUES (1,1, DEFAULT);
-- INSERT INTO [dbo].[ContactUrgenceMembres] VALUES (2,2, DEFAULT);
-- INSERT INTO [dbo].[ContactUrgenceMembres] VALUES (3,3, DEFAULT);
-- INSERT INTO [dbo].[ContactUrgenceMembres] VALUES (4,4, DEFAULT);

-- INSERT INTO [dbo].[Evenements] VALUES (1,'Événement1','mauris, aliquam eu, accumsan sed,','2012-05-05','2013-09-17', DEFAULT);
-- INSERT INTO [dbo].[Evenements] VALUES (2,'Événement2','In lorem. Donec elementum, lorem','2014-02-24','2014-09-25', DEFAULT);
-- INSERT INTO [dbo].[Evenements] VALUES (3,'Événement3','lorem. Donec elementum, lorem ut','2010-09-23','2014-06-04', DEFAULT);
-- INSERT INTO [dbo].[Evenements] VALUES (4,'Événement4','nulla. Integer urna. Vivamus molestie','2015-12-14','2013-09-30', DEFAULT);

-- INSERT INTO [dbo].[Formations] VALUES ('ATELIER', 'Peux entrer dans l''atelier et utiliser les outils de bases', DEFAULT);
-- INSERT INTO [dbo].[Formations] VALUES ('SOUDURE', 'Peux utiliser les machines à souder', DEFAULT);
-- INSERT INTO [dbo].[Formations] VALUES ('ELECTRIQUE', 'Peux faire des montage électrique complexe', DEFAULT);
-- INSERT INTO [dbo].[Formations] VALUES ('RAPPEL_ESCALADE', 'Peux assurer quelqu''un à l''escalade', DEFAULT);
-- 
-- INSERT INTO [dbo].[FournisseursItems] VALUES (1,1,'CODE1', DEFAULT);
-- INSERT INTO [dbo].[FournisseursItems] VALUES (2,2,'CODE2', DEFAULT);
-- INSERT INTO [dbo].[FournisseursItems] VALUES (3,3,'CODE3', DEFAULT);
-- INSERT INTO [dbo].[FournisseursItems] VALUES (4,4,'CODE4', DEFAULT);

INSERT INTO [clubs].[StatutsSuivie] VALUES ('OUVERT', 'Ouvert');
INSERT INTO [clubs].[StatutsSuivie] VALUES ('PROSPECT', 'Prospect');
INSERT INTO [clubs].[StatutsSuivie] VALUES ('CONFIRME', 'Confirmé');
INSERT INTO [clubs].[StatutsSuivie] VALUES ('FERME', 'Fermé');

-- INSERT INTO [dbo].[Suivies] VALUES (1,1,1,'2012-10-03','erat, in conserectagiluca dsauhifds fgrakfoh fdeuihtur massa. Restibilum', DEFAULT);
-- INSERT INTO [dbo].[Suivies] VALUES (2,2,2,'2013-11-09','erat, in consectetuer ipsum nunc id enim. Curabitur massa. Vestibulum', DEFAULT);
-- INSERT INTO [dbo].[Suivies] VALUES (3,3,3,'2014-12-08','eleifend non, dapibus rutrum, justo. Praesent luctus. Curabitur egestas nunc', DEFAULT);
-- INSERT INTO [dbo].[Suivies] VALUES (4,4,4,'2014-02-09','congue. In scelerisque scelerisque dui. Suspendisse ac metus vitae velit', DEFAULT);

-- INSERT INTO [dbo].[MembresAllergies] VALUES (1,1, DEFAULT);
-- INSERT INTO [dbo].[MembresAllergies] VALUES (1,2, DEFAULT);
