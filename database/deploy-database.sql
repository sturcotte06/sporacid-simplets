INSERT INTO [dbo].[Clubs] VALUES ('PRECI','Programme de Regroupement Étudiant pour la Coopération Internationale');
INSERT INTO [dbo].[Clubs] VALUES ('LiETS','Ligue d`Improvisation de l`ETS');
INSERT INTO [dbo].[Clubs] VALUES ('Evolution','Véhicule à faible consommation');
INSERT INTO [dbo].[Clubs] VALUES ('Baja','Véhicule amphibie tout terrain');
				
INSERT INTO [dbo].[Roles] (nom, description) VALUES ('Membre', 'Un membre.');
INSERT INTO [dbo].[Roles] (nom, description) VALUES ('Capitaine', 'Un capitaine.');
INSERT INTO [dbo].[Roles] (nom, description) VALUES ('Membre', 'Un membre.');
INSERT INTO [dbo].[Roles] (nom, description) VALUES ('Auditeur', 'Un auditeur.');

INSERT INTO [dbo].[Concentrations] VALUES ('CTN','Génie de la construction');
INSERT INTO [dbo].[Concentrations] VALUES ('ELE','Génie électrique');
INSERT INTO [dbo].[Concentrations] VALUES ('GOL','Génie des opérations et de la logistique');
INSERT INTO [dbo].[Concentrations] VALUES ('GPA','Génie de production automatisée');
INSERT INTO [dbo].[Concentrations] VALUES ('LOG','Génie logiciel');
INSERT INTO [dbo].[Concentrations] VALUES ('MEC','Génie mécanique');
INSERT INTO [dbo].[Concentrations] VALUES ('TI','Génie des technologies de l`information');

INSERT INTO [dbo].[Membres] (concentrations_id,nom,prenom,courriel,code_universel,actif,telephone) VALUES (1,'Patrick','Olsen','nibh.Aliquam@tristiquesenectus.co.uk','AJ00689', 1,'3716562099');
INSERT INTO [dbo].[Membres] (concentrations_id,nom,prenom,courriel,code_universel,actif,telephone) VALUES (2,'Moses','Melendez','Donec.consectetuer.mauris@mattisvelit.edu','AJ32898', 1,'1881189845');
INSERT INTO [dbo].[Membres] (concentrations_id,nom,prenom,courriel,code_universel,actif,telephone) VALUES (3,'Giacomo','Cote','velit.Cras.lorem@duiSuspendisseac.ca','AZ92060', 1,'6534742324');
INSERT INTO [dbo].[Membres] (concentrations_id,nom,prenom,courriel,code_universel,actif,telephone) VALUES (4,'Jade','Ford','sem.ut.dolor@nonbibendum.co.uk','AZ35441', 1,'8522879201');
INSERT INTO [dbo].[Membres] (concentrations_id,nom,prenom,courriel,code_universel,actif,telephone) VALUES (5,'Chandler','Mccarty','in.dolor@atpretiumaliquet.org','AX85616', 1,'3055537869');

INSERT INTO [dbo].[units] (unit_code,systeme) VALUES ('UNIT1','SYSTEME2');
INSERT INTO [dbo].[units] (unit_code,systeme) VALUES ('UNIT2','SYSTEME3');
INSERT INTO [dbo].[units] (unit_code,systeme) VALUES ('UNIT3','SYSTEME4');
INSERT INTO [dbo].[units] (unit_code,systeme) VALUES ('UNIT4','SYSTEME5');
INSERT INTO [dbo].[units] (unit_code,systeme) VALUES ('UNIT5','SYSTEME6');

INSERT INTO [dbo].[items] (units_id,description,code_club,qte_courante,qty_min,qty_max) VALUES (1,'urna justo faucibus lectus, a',1,3,1,15);
INSERT INTO [dbo].[items] (units_id,description,code_club,qte_courante,qty_min,qty_max) VALUES (2,'vehicula aliquet libero. Integer in',2,4,1,14);
INSERT INTO [dbo].[items] (units_id,description,code_club,qte_courante,qty_min,qty_max) VALUES (3,'in lobortis tellus justo sit',3,5,1,21);
INSERT INTO [dbo].[items] (units_id,description,code_club,qte_courante,qty_min,qty_max) VALUES (4,'at, velit. Pellentesque ultricies dignissim',4,10,1,14);
			
INSERT INTO [dbo].[adresses] (no_civique,rue,appartement,ville,code_postal) VALUES (1,'P.O. Box 733, 4867 Pellentesque. Av.',1,'Greenwich','26231');
INSERT INTO [dbo].[adresses] (no_civique,rue,appartement,ville,code_postal) VALUES (2,'6206 Suspendisse Avenue',2,'Breton','22078');
INSERT INTO [dbo].[adresses] (no_civique,rue,appartement,ville,code_postal) VALUES (3,'P.O. Box 932, 5843 Donec Road',3,'Borgerhout','21679');
INSERT INTO [dbo].[adresses] (no_civique,rue,appartement,ville,code_postal) VALUES (4,'8448 Mollis. St.',4,'Anchorage','5048');
INSERT INTO [dbo].[adresses] (no_civique,rue,appartement,ville,code_postal) VALUES (5,'P.O. Box 585, 7672 Enim. Avenue',5,'Cargovil','0729LE');
INSERT INTO [dbo].[adresses] (no_civique,rue,appartement,ville,code_postal) VALUES (6,'928-6538 Cursus. Avenue',6,'Attimis','76747');
INSERT INTO [dbo].[adresses] (no_civique,rue,appartement,ville,code_postal) VALUES (7,'388-2939 Enim Rd.',7,'Stokkem','56673-407');
INSERT INTO [dbo].[adresses] (no_civique,rue,appartement,ville,code_postal) VALUES (8,'566-6018 Lorem St.',8,'Barbania','7329WQ');
INSERT INTO [dbo].[adresses] (no_civique,rue,appartement,ville,code_postal) VALUES (9,'1311 Torquent Av.',9,'Merbes-Sainte-Marie','H0H 4M9');
INSERT INTO [dbo].[adresses] (no_civique,rue,appartement,ville,code_postal) VALUES (10,'Ap #318-7322 Vivamus St.',10,'Serskamp','97859-876');

INSERT INTO [dbo].[allergies] (description) VALUES ('nibh. Phasellus nulla. Integer vulputate,');
INSERT INTO [dbo].[allergies] (description) VALUES ('scelerisque sed, sapien. Nunc pulvinar');
INSERT INTO [dbo].[allergies] (description) VALUES ('id nunc interdum feugiat. Sed');
INSERT INTO [dbo].[allergies] (description) VALUES ('per inceptos hymenaeos. Mauris ut');
INSERT INTO [dbo].[allergies] (description) VALUES ('Aliquam auctor, velit eget laoreet');
INSERT INTO [dbo].[allergies] (description) VALUES ('tellus sem mollis dui, in');

INSERT INTO [dbo].[fournisseurs] (adresses_id,nom,contact,telephone,fax,courriel) VALUES (1,'Bibendum Fermentum Company','Gilbert, Sean Q.','(805) 561-7970','(984) 452-9655','cubilia.Curae.Phasellus@cubilia.org');
INSERT INTO [dbo].[fournisseurs] (adresses_id,nom,contact,telephone,fax,courriel) VALUES (2,'Non Corporation','Yates, Cailin E.','(574) 670-8278','(793) 724-5490','Curae.Phasellus@aliquamenimnec.edu');
INSERT INTO [dbo].[fournisseurs] (adresses_id,nom,contact,telephone,fax,courriel) VALUES (3,'Imperdiet Corp.','Jennings, Brynn P.','(541) 442-7761','(756) 911-2750','Etiam.laoreet.libero@dignissimMaecenas.net');
INSERT INTO [dbo].[fournisseurs] (adresses_id,nom,contact,telephone,fax,courriel) VALUES (4,'Adipiscing Enim Corporation','Hickman, Wade M.','(850) 532-2571','(268) 523-2179','eget@montesnasceturridiculus.edu');
INSERT INTO [dbo].[fournisseurs] (adresses_id,nom,contact,telephone,fax,courriel) VALUES (5,'Urna Nunc Quis Company','Douglas, Dustin M.','(173) 840-7137','(318) 662-4800','Phasellus.in.felis@pellentesquemassalobortis.com');

INSERT INTO [dbo].[commandites] (fournisseurs_id,items_id,clubs_id,valeur,nature) VALUES (1,1,1,908,'ut, pellentesque eget, dictum placerat,');
INSERT INTO [dbo].[commandites] (fournisseurs_id,items_id,clubs_id,valeur,nature) VALUES (2,2,2,9686,'posuere vulputate, lacus. Cras interdum.');
INSERT INTO [dbo].[commandites] (fournisseurs_id,items_id,clubs_id,valeur,nature) VALUES (3,3,3,1628,'feugiat placerat velit. Quisque varius.');
INSERT INTO [dbo].[commandites] (fournisseurs_id,items_id,clubs_id,valeur,nature) VALUES (4,4,4,5702,'molestie dapibus ligula. Aliquam erat');

INSERT INTO [dbo].[liens_parente] ( description) VALUES ( 'Mère');
INSERT INTO [dbo].[liens_parente] ( description) VALUES ( 'Père');
INSERT INTO [dbo].[liens_parente] ( description) VALUES ( 'Enfant');

INSERT INTO [dbo].[contacts_urgence] (membres_id,liens_parente_id,nom,prenom,telephone) VALUES (1,1,'Hoover','Zephania','1-772-913-2130');
INSERT INTO [dbo].[contacts_urgence] (membres_id,liens_parente_id,nom,prenom,telephone) VALUES (1,2,'Gibson','Wynne','1-727-710-9565');
INSERT INTO [dbo].[contacts_urgence] (membres_id,liens_parente_id,nom,prenom,telephone) VALUES (1,3,'Ryan','Athena','1-462-616-1288');
INSERT INTO [dbo].[contacts_urgence] (membres_id,liens_parente_id,nom,prenom,telephone) VALUES (1,1,'Harper','Nissim','1-702-829-5337');

INSERT INTO [dbo].[evenements] (clubs_id,nom,description,date_debut,date_fin) VALUES (1,'Événement1','mauris, aliquam eu, accumsan sed,','2012-05-05','2013-09-17');
INSERT INTO [dbo].[evenements] (clubs_id,nom,description,date_debut,date_fin) VALUES (2,'Événement2','In lorem. Donec elementum, lorem','2014-02-24','2014-09-25');
INSERT INTO [dbo].[evenements] (clubs_id,nom,description,date_debut,date_fin) VALUES (3,'Événement3','lorem. Donec elementum, lorem ut','2010-09-23','2014-06-04');
INSERT INTO [dbo].[evenements] (clubs_id,nom,description,date_debut,date_fin) VALUES (4,'Événement4','nulla. Integer urna. Vivamus molestie','2015-12-14','2013-09-30');

INSERT INTO [dbo].[formations] ( titre, description) VALUES ( 'ATELIER', 'Peux entrer dans l''atelier et utiliser les outils de bases');
INSERT INTO [dbo].[formations] ( titre, description) VALUES ( 'SOUDURE', 'Peux utiliser les machines à souder');
INSERT INTO [dbo].[formations] ( titre, description) VALUES ( 'ELECTRIQUE', 'Peux faire des montage électrique complexe');
INSERT INTO [dbo].[formations] ( titre, description) VALUES ( 'RAPPEL_ESCALADE', 'Peux assurer quelqu''un à l''escalade');

INSERT INTO [dbo].[fournisseurs_items] (fournisseurs_id,items_id,code_fournisseur) VALUES (1,1,334);
INSERT INTO [dbo].[fournisseurs_items] (fournisseurs_id,items_id,code_fournisseur) VALUES (2,2,554);
INSERT INTO [dbo].[fournisseurs_items] (fournisseurs_id,items_id,code_fournisseur) VALUES (3,3,773);
INSERT INTO [dbo].[fournisseurs_items] (fournisseurs_id,items_id,code_fournisseur) VALUES (4,4,1111);

INSERT INTO [dbo].[suivie_statuts] (code,description) VALUES ( 'OUVERT', 'Ouvert');
INSERT INTO [dbo].[suivie_statuts] (code,description) VALUES ( 'PROSPECT', 'Prospect');
INSERT INTO [dbo].[suivie_statuts] (code,description) VALUES ( 'CONFIRME', 'Confirmé');
INSERT INTO [dbo].[suivie_statuts] (code,description) VALUES ( 'FERME', 'Fermé');

INSERT INTO [dbo].[suivies] (commandites_id,membres_id,suivie_statuts_id,date_suivie,commentaire) VALUES (1,1,1,'2012-10-03','erat, in conserectagiluca dsauhifds fgrakfoh fdeuihtur massa. Restibilum');
INSERT INTO [dbo].[suivies] (commandites_id,membres_id,suivie_statuts_id,date_suivie,commentaire) VALUES (2,2,2,'2013-11-09','erat, in consectetuer ipsum nunc id enim. Curabitur massa. Vestibulum');
INSERT INTO [dbo].[suivies] (commandites_id,membres_id,suivie_statuts_id,date_suivie,commentaire) VALUES (3,3,3,'2014-12-08','eleifend non, dapibus rutrum, justo. Praesent luctus. Curabitur egestas nunc');
INSERT INTO [dbo].[suivies] (commandites_id,membres_id,suivie_statuts_id,date_suivie,commentaire) VALUES (4,4,4,'2014-02-09','congue. In scelerisque scelerisque dui. Suspendisse ac metus vitae velit');

INSERT INTO [dbo].[membres_allergies] (membres_id, allergies_id) VALUES (1,1);
INSERT INTO [dbo].[membres_allergies] (membres_id, allergies_id) VALUES (1,2);
