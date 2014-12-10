Insert into clubs values (nextval('clubs_id_seq'), 'PRECI','Programme de Regroupement Étudiant pour la Coopération Internationale');
Insert into clubs values (nextval('clubs_id_seq'), 'LiETS','Ligue d`Improvisation de l`ETS');
Insert into clubs values (nextval('clubs_id_seq'), 'Evolution','Véhicule à faible consommation');
Insert into clubs values (nextval('clubs_id_seq'), 'Baja','Véhicule amphibie tout terrain');
				
INSERT INTO roles(id, nom, description) VALUES (nextval('roles_id_seq'), 'Membre', 'Un membre.');
INSERT INTO roles(id, nom, description) VALUES (nextval('roles_id_seq'), 'Capitaine', 'Un capitaine.');
INSERT INTO roles(id, nom, description) VALUES (nextval('roles_id_seq'), 'Membre', 'Un membre.');
INSERT INTO roles(id, nom, description) VALUES (nextval('roles_id_seq'), 'Auditeur', 'Un auditeur.');

Insert into concentrations values (nextval('concentrations_id_seq'), 'CTN','Génie de la construction');
Insert into concentrations values (nextval('concentrations_id_seq'), 'ELE','Génie électrique');
Insert into concentrations values (nextval('concentrations_id_seq'), 'GOL','Génie des opérations et de la logistique');
Insert into concentrations values (nextval('concentrations_id_seq'), 'GPA','Génie de production automatisée');
Insert into concentrations values (nextval('concentrations_id_seq'), 'LOG','Génie logiciel');
Insert into concentrations values (nextval('concentrations_id_seq'), 'MEC','Génie mécanique');
Insert into concentrations values (nextval('concentrations_id_seq'), 'TI','Génie des technologies de l`information');

INSERT INTO membres (id,concentrations_id,nom,prenom,courriel,code_permanent,code_universel,actif,telephone) VALUES (nextval('membres_id_seq'),1,'Patrick','Olsen','nibh.Aliquam@tristiquesenectus.co.uk','XUYJ98427084','AJ00689',true,'3716562099');
INSERT INTO membres (id,concentrations_id,nom,prenom,courriel,code_permanent,code_universel,actif,telephone) VALUES (nextval('membres_id_seq'),2,'Moses','Melendez','Donec.consectetuer.mauris@mattisvelit.edu','SXGD95781049','AJ32898',true,'1881189845');
INSERT INTO membres (id,concentrations_id,nom,prenom,courriel,code_permanent,code_universel,actif,telephone) VALUES (nextval('membres_id_seq'),3,'Giacomo','Cote','velit.Cras.lorem@duiSuspendisseac.ca','IJKG71845113','AZ92060',true,'6534742324');
INSERT INTO membres (id,concentrations_id,nom,prenom,courriel,code_permanent,code_universel,actif,telephone) VALUES (nextval('membres_id_seq'),4,'Jade','Ford','sem.ut.dolor@nonbibendum.co.uk','GXLM93228319','AZ35441',true,'8522879201');
INSERT INTO membres (id,concentrations_id,nom,prenom,courriel,code_permanent,code_universel,actif,telephone) VALUES (nextval('membres_id_seq'),5,'Chandler','Mccarty','in.dolor@atpretiumaliquet.org','ACGN79164633','AX85616',true,'3055537869');

INSERT INTO units (id,unit_code,systeme) VALUES (nextval('units_id_seq'),'UNIT1','SYSTEME2');
INSERT INTO units (id,unit_code,systeme) VALUES (nextval('units_id_seq'),'UNIT2','SYSTEME3');
INSERT INTO units (id,unit_code,systeme) VALUES (nextval('units_id_seq'),'UNIT3','SYSTEME4');
INSERT INTO units (id,unit_code,systeme) VALUES (nextval('units_id_seq'),'UNIT4','SYSTEME5');
INSERT INTO units (id,unit_code,systeme) VALUES (nextval('units_id_seq'),'UNIT5','SYSTEME6');

INSERT INTO items (id,units_id,description,code_club,qte_courante,qty_min,qty_max) VALUES (nextval('items_id_seq'),1,'urna justo faucibus lectus, a',1,3,1,15);
INSERT INTO items (id,units_id,description,code_club,qte_courante,qty_min,qty_max) VALUES (nextval('items_id_seq'),2,'vehicula aliquet libero. Integer in',2,4,1,14);
INSERT INTO items (id,units_id,description,code_club,qte_courante,qty_min,qty_max) VALUES (nextval('items_id_seq'),3,'in lobortis tellus justo sit',3,5,1,21);
INSERT INTO items (id,units_id,description,code_club,qte_courante,qty_min,qty_max) VALUES (nextval('items_id_seq'),4,'at, velit. Pellentesque ultricies dignissim',4,10,1,14);

INSERT INTO adresses (id,no_civique,rue,appartement,ville,code_postal) VALUES (nextval('adresses_id_seq'),1,'P.O. Box 733, 4867 Pellentesque. Av.',1,'Greenwich','26231');
INSERT INTO adresses (id,no_civique,rue,appartement,ville,code_postal) VALUES (nextval('adresses_id_seq'),2,'6206 Suspendisse Avenue',2,'Breton','22078');
INSERT INTO adresses (id,no_civique,rue,appartement,ville,code_postal) VALUES (nextval('adresses_id_seq'),3,'P.O. Box 932, 5843 Donec Road',3,'Borgerhout','21679');
INSERT INTO adresses (id,no_civique,rue,appartement,ville,code_postal) VALUES (nextval('adresses_id_seq'),4,'8448 Mollis. St.',4,'Anchorage','5048');
INSERT INTO adresses (id,no_civique,rue,appartement,ville,code_postal) VALUES (nextval('adresses_id_seq'),5,'P.O. Box 585, 7672 Enim. Avenue',5,'Cargovil','0729LE');
INSERT INTO adresses (id,no_civique,rue,appartement,ville,code_postal) VALUES (nextval('adresses_id_seq'),6,'928-6538 Cursus. Avenue',6,'Attimis','76747');
INSERT INTO adresses (id,no_civique,rue,appartement,ville,code_postal) VALUES (nextval('adresses_id_seq'),7,'388-2939 Enim Rd.',7,'Stokkem','56673-407');
INSERT INTO adresses (id,no_civique,rue,appartement,ville,code_postal) VALUES (nextval('adresses_id_seq'),8,'566-6018 Lorem St.',8,'Barbania','7329WQ');
INSERT INTO adresses (id,no_civique,rue,appartement,ville,code_postal) VALUES (nextval('adresses_id_seq'),9,'1311 Torquent Av.',9,'Merbes-Sainte-Marie','H0H 4M9');
INSERT INTO adresses (id,no_civique,rue,appartement,ville,code_postal) VALUES (nextval('adresses_id_seq'),10,'Ap #318-7322 Vivamus St.',10,'Serskamp','97859-876');

INSERT INTO allergies (id,description) VALUES (nextval('allergies_id_seq'),'nibh. Phasellus nulla. Integer vulputate,');
INSERT INTO allergies (id,description) VALUES (nextval('allergies_id_seq'),'scelerisque sed, sapien. Nunc pulvinar');
INSERT INTO allergies (id,description) VALUES (nextval('allergies_id_seq'),'id nunc interdum feugiat. Sed');
INSERT INTO allergies (id,description) VALUES (nextval('allergies_id_seq'),'per inceptos hymenaeos. Mauris ut');
INSERT INTO allergies (id,description) VALUES (nextval('allergies_id_seq'),'Aliquam auctor, velit eget laoreet');
INSERT INTO allergies (id,description) VALUES (nextval('allergies_id_seq'),'tellus sem mollis dui, in');

INSERT INTO fournisseurs (id,adresses_id,nom,contact,telephone,fax,courriel) VALUES (nextval('fournisseurs_id_seq'),1,'Bibendum Fermentum Company','Gilbert, Sean Q.','(805) 561-7970','(984) 452-9655','cubilia.Curae.Phasellus@cubilia.org');
INSERT INTO fournisseurs (id,adresses_id,nom,contact,telephone,fax,courriel) VALUES (nextval('fournisseurs_id_seq'),2,'Non Corporation','Yates, Cailin E.','(574) 670-8278','(793) 724-5490','Curae.Phasellus@aliquamenimnec.edu');
INSERT INTO fournisseurs (id,adresses_id,nom,contact,telephone,fax,courriel) VALUES (nextval('fournisseurs_id_seq'),3,'Imperdiet Corp.','Jennings, Brynn P.','(541) 442-7761','(756) 911-2750','Etiam.laoreet.libero@dignissimMaecenas.net');
INSERT INTO fournisseurs (id,adresses_id,nom,contact,telephone,fax,courriel) VALUES (nextval('fournisseurs_id_seq'),4,'Adipiscing Enim Corporation','Hickman, Wade M.','(850) 532-2571','(268) 523-2179','eget@montesnasceturridiculus.edu');
INSERT INTO fournisseurs (id,adresses_id,nom,contact,telephone,fax,courriel) VALUES (nextval('fournisseurs_id_seq'),5,'Urna Nunc Quis Company','Douglas, Dustin M.','(173) 840-7137','(318) 662-4800','Phasellus.in.felis@pellentesquemassalobortis.com');

INSERT INTO commandites (id,fournisseurs_id,items_id,clubs_id,valeur,nature) VALUES (nextval('commandites_id_seq'),1,1,1,908,'ut, pellentesque eget, dictum placerat,');
INSERT INTO commandites (id,fournisseurs_id,items_id,clubs_id,valeur,nature) VALUES (nextval('commandites_id_seq'),2,2,2,9686,'posuere vulputate, lacus. Cras interdum.');
INSERT INTO commandites (id,fournisseurs_id,items_id,clubs_id,valeur,nature) VALUES (nextval('commandites_id_seq'),3,3,3,1628,'feugiat placerat velit. Quisque varius.');
INSERT INTO commandites (id,fournisseurs_id,items_id,clubs_id,valeur,nature) VALUES (nextval('commandites_id_seq'),4,4,4,5702,'molestie dapibus ligula. Aliquam erat');

INSERT INTO liens_parente (id, description) VALUES (nextval('liens_parente_id_seq'), 'Mère');
INSERT INTO liens_parente (id, description) VALUES (nextval('liens_parente_id_seq'), 'Père');
INSERT INTO liens_parente (id, description) VALUES (nextval('liens_parente_id_seq'), 'Enfant');

INSERT INTO contacts_urgence (id,liens_parente_id,nom,prenom,telephone) VALUES (nextval('contacts_urgence_id_seq'),1,'Hoover','Zephania','1-772-913-2130');
INSERT INTO contacts_urgence (id,liens_parente_id,nom,prenom,telephone) VALUES (nextval('contacts_urgence_id_seq'),2,'Gibson','Wynne','1-727-710-9565');
INSERT INTO contacts_urgence (id,liens_parente_id,nom,prenom,telephone) VALUES (nextval('contacts_urgence_id_seq'),3,'Ryan','Athena','1-462-616-1288');
INSERT INTO contacts_urgence (id,liens_parente_id,nom,prenom,telephone) VALUES (nextval('contacts_urgence_id_seq'),1,'Harper','Nissim','1-702-829-5337');

INSERT INTO contacts_urgence_membres (contacts_urgence_id, membres_id) VALUES (1,1);
INSERT INTO contacts_urgence_membres (contacts_urgence_id, membres_id) VALUES (2,2);
INSERT INTO contacts_urgence_membres (contacts_urgence_id, membres_id) VALUES (3,3);
INSERT INTO contacts_urgence_membres (contacts_urgence_id, membres_id) VALUES (4,4);

INSERT INTO evenements (id,clubs_id,nom,description,date_debut,date_fin) VALUES (nextval('evenements_id_seq'),1,'Événement1','mauris, aliquam eu, accumsan sed,','2012-05-05','2013-09-17');
INSERT INTO evenements (id,clubs_id,nom,description,date_debut,date_fin) VALUES (nextval('evenements_id_seq'),2,'Événement2','In lorem. Donec elementum, lorem','2014-02-24','2014-09-25');
INSERT INTO evenements (id,clubs_id,nom,description,date_debut,date_fin) VALUES (nextval('evenements_id_seq'),3,'Événement3','lorem. Donec elementum, lorem ut','2010-09-23','2014-06-04');
INSERT INTO evenements (id,clubs_id,nom,description,date_debut,date_fin) VALUES (nextval('evenements_id_seq'),4,'Événement4','nulla. Integer urna. Vivamus molestie','2015-12-14','2013-09-30');

INSERT INTO formations (id, titre, description) VALUES (nextval('formations_id_seq'), 'ATELIER', 'Peux entrer dans l''atelier et utiliser les outils de bases');
INSERT INTO formations (id, titre, description) VALUES (nextval('formations_id_seq'), 'SOUDURE', 'Peux utiliser les machines à souder');
INSERT INTO formations (id, titre, description) VALUES (nextval('formations_id_seq'), 'ELECTRIQUE', 'Peux faire des montage électrique complexe');
INSERT INTO formations (id, titre, description) VALUES (nextval('formations_id_seq'), 'RAPPEL_ESCALADE', 'Peux assurer quelqu''un à l''escalade');

INSERT INTO fournisseurs_items (fournisseurs_id,items_id,code_fournisseur) VALUES (1,1,'CODE1');
INSERT INTO fournisseurs_items (fournisseurs_id,items_id,code_fournisseur) VALUES (2,2,'CODE2');
INSERT INTO fournisseurs_items (fournisseurs_id,items_id,code_fournisseur) VALUES (3,3,'CODE3');
INSERT INTO fournisseurs_items (fournisseurs_id,items_id,code_fournisseur) VALUES (4,4,'CODE4');

INSERT INTO suivie_statuts (id,code,description) VALUES (nextval('suivie_statuts_id_seq'), 'OUVERT', 'Ouvert');
INSERT INTO suivie_statuts (id,code,description) VALUES (nextval('suivie_statuts_id_seq'), 'PROSPECT', 'Prospect');
INSERT INTO suivie_statuts (id,code,description) VALUES (nextval('suivie_statuts_id_seq'), 'CONFIRME', 'Confirmé');
INSERT INTO suivie_statuts (id,code,description) VALUES (nextval('suivie_statuts_id_seq'), 'FERME', 'Fermé');

INSERT INTO suivies (id,commandites_id,membres_id,suivie_statuts_id,date_suivie,commentaire) VALUES (nextval('suivies_id_seq'),1,1,1,'2012-10-03','erat, in conserectagiluca dsauhifds fgrakfoh fdeuihtur massa. Restibilum');
INSERT INTO suivies (id,commandites_id,membres_id,suivie_statuts_id,date_suivie,commentaire) VALUES (nextval('suivies_id_seq'),2,2,2,'2013-11-09','erat, in consectetuer ipsum nunc id enim. Curabitur massa. Vestibulum');
INSERT INTO suivies (id,commandites_id,membres_id,suivie_statuts_id,date_suivie,commentaire) VALUES (nextval('suivies_id_seq'),3,3,3,'2014-12-08','eleifend non, dapibus rutrum, justo. Praesent luctus. Curabitur egestas nunc');
INSERT INTO suivies (id,commandites_id,membres_id,suivie_statuts_id,date_suivie,commentaire) VALUES (nextval('suivies_id_seq'),4,4,4,'2014-02-09','congue. In scelerisque scelerisque dui. Suspendisse ac metus vitae velit');

INSERT INTO membres_allergies (membres_id, allergies_id) VALUES (1,1);
INSERT INTO membres_allergies (membres_id, allergies_id) VALUES (1,2);
