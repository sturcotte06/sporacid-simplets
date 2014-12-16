USE [SIMPLETS]
GO

-- Drop all tables before creating them.
-- Execute this ~5 times to drop all tables (fk exceptions)
-- EXEC sp_MSforeachtable @command1 = "DROP TABLE ?" 
-- EXEC sp_MSforeachtable @command1 = "DBCC CHECKIDENT (?, reseed, 1)"

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

/****** Object:  Table [dbo].[Adresses]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [dbo].[Adresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NoCivique] [int] NOT NULL,
	[Rue] [varchar](50) NOT NULL,
	[Appartement] [varchar](10) NULL,
	[Ville] [varchar](150) NOT NULL,
	[CodePostal] [varchar](16) NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Allergies]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [dbo].[Allergies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](150) NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[Clubs]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [dbo].[Clubs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [varchar](50) NOT NULL UNIQUE,
	[Description] [varchar](250) NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Commandites]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [dbo].[Commandites](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FournisseurId] [int] NOT NULL,
	[ItemId] [int] NOT NULL,
	[ClubId] [int] NOT NULL,
	[Valeur] [numeric](6, 2) NOT NULL,
	[Nature] [varchar](50) NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Concentrations]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [dbo].[Concentrations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Acronyme] [varchar](10) NOT NULL,
	[Description] [varchar](150) NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Contacts_Urgence]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [dbo].[ContactsUrgence](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MembreId] [int] NOT NULL,
	[LienParenteId] [int] NOT NULL,
	[Nom] [varchar](50) NOT NULL,
	[Prenom] [varchar](50) NOT NULL,
	[Telephone] [varchar](20) NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Evenements]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [dbo].[Evenements](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClubId] [int] NOT NULL,
	[Nom] [varchar](50) NOT NULL,
	[Description] [varchar](250) NOT NULL,
	[DateDebut] [datetime] NOT NULL,
	[DateFin] [datetime] NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Formations]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [dbo].[Formations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titre] [varchar](50) NULL,
	[Description] [varchar](150) NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Fournisseurs]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [dbo].[Fournisseurs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AdresseId] [int] NOT NULL,
	[Nom] [varchar](250) NOT NULL,
	[Contact] [varchar](50) NOT NULL,
	[Telephone] [varchar](20) NOT NULL,
	[Courriel] [varchar](250) NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Fournisseurs_Items]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [dbo].[FournisseursItems](
	[FournisseurId] [int] NOT NULL,
	[ItemId] [int] NOT NULL,
	[CodeFournisseur] [varchar](20) NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FournisseurId] ASC,
	[ItemId] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Items]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [dbo].[Items](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UniteId] [int] NOT NULL,
	[Description] [varchar](250) NOT NULL,
	[CodeClub] [varchar](20) NULL,
	[QuantiteCourante] [numeric](6, 3) NOT NULL,
	[QunatiteMin] [numeric](6, 3) NULL,
	[QuantiteMax] [numeric](6, 3) NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Liens_Parente]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [dbo].[LiensParente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Membres]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [dbo].[Membres](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConcentrationId] [int] NULL,
	[CodeUniversel] [varchar](10) NOT NULL,
	[Nom] [varchar](50) NOT NULL,
	[Prenom] [varchar](50) NOT NULL,
	[Courriel] [varchar](250) NOT NULL,
	[Telephone] [varchar](20) NULL,
	[Actif] [bit] NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Membres_Allergies]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [dbo].[MembresAllergies](
	[MembreId] [int] NOT NULL,
	[AllergieId] [int] NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MembreId] ASC,
	[AllergieId] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Membres_Clubs]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [dbo].[MembresClubs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClubId] [int] NOT NULL,
	[MembreId] [int] NOT NULL,
	[DateDebut] [datetime] NOT NULL,
	[DateFin] [datetime] NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Membres_Clubs_Roles]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [dbo].[MembreClubsRoles](
	[MembreClubId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MembreClubId] ASC,
	[RoleId] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Membres_Formations]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [dbo].[MembresFormations](
	[MembreId] [int] NOT NULL,
	[FormationId] [int] NOT NULL,
	[DateSuivie] [datetime] NOT NULL,
	[DateEcheance] [datetime] NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MembreId] ASC,
	[FormationId] ASC
)) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Membres_Preferences]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [dbo].[MembrePreferences](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MembreId] [int] NOT NULL,
	[PreferenceKey] [varchar](50) NOT NULL,
	[PreferenceValue] [varchar](150) NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Suivie_Statuts]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [dbo].[StatutsSuivie](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[Description] [varchar](150) NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Suivies]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [dbo].[Suivies](
	[CommanditeId] [int] NOT NULL,
	[MembreId] [int] NOT NULL,
	[StatutSuivieId] [int] NOT NULL,
	[DateSuivie] [datetime] NOT NULL,
	[Commentaire] [varchar](250) NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CommanditeId] ASC,
	[MembreId] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Units]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [dbo].[Unites](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](10) NOT NULL,
	[Systeme] [varchar](10) NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

/****** Foreign keys ******/
ALTER TABLE [dbo].[Commandites]  WITH CHECK ADD CONSTRAINT [FKCommanditesItems] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Items] ([Id])
GO
ALTER TABLE [dbo].[Commandites] CHECK CONSTRAINT [FKCommanditesItems]
GO

ALTER TABLE [dbo].[Commandites]  WITH CHECK ADD  CONSTRAINT [FKCommanditesClubs] FOREIGN KEY([ClubId])
REFERENCES [dbo].[Clubs] ([Id])
GO
ALTER TABLE [dbo].[Commandites] CHECK CONSTRAINT [FKCommanditesClubs]
GO

ALTER TABLE [dbo].[Commandites]  WITH CHECK ADD  CONSTRAINT [FKCommanditesFournisseurs] FOREIGN KEY([FournisseurId])
REFERENCES [dbo].[Fournisseurs] ([Id])
GO
ALTER TABLE [dbo].[Commandites] CHECK CONSTRAINT [FKCommanditesFournisseurs]
GO

ALTER TABLE [dbo].[ContactsUrgence]  WITH CHECK ADD  CONSTRAINT [FKContactsLiensParente] FOREIGN KEY([LienParenteId])
REFERENCES [dbo].[LiensParente] ([Id])
GO
ALTER TABLE [dbo].[ContactsUrgence] CHECK CONSTRAINT [FKContactsLiensParente]
GO

ALTER TABLE [dbo].[ContactsUrgence]  WITH CHECK ADD  CONSTRAINT [FKContactsMembres] FOREIGN KEY([MembreId])
REFERENCES [dbo].[Membres] ([Id])
GO
ALTER TABLE [dbo].[ContactsUrgence] CHECK CONSTRAINT [FKContactsMembres]
GO

ALTER TABLE [dbo].[Evenements]  WITH CHECK ADD  CONSTRAINT [FKEvenementsClubs] FOREIGN KEY([ClubId])
REFERENCES [dbo].[Clubs] ([Id])
GO
ALTER TABLE [dbo].[Evenements] CHECK CONSTRAINT [FKEvenementsClubs]
GO

ALTER TABLE [dbo].[Fournisseurs]  WITH CHECK ADD  CONSTRAINT [FKFournisseursAdresses] FOREIGN KEY([AdresseId])
REFERENCES [dbo].[Adresses] ([Id])
GO
ALTER TABLE [dbo].[Fournisseurs] CHECK CONSTRAINT [FKFournisseursAdresses]
GO

ALTER TABLE [dbo].[FournisseursItems]  WITH CHECK ADD  CONSTRAINT [FKFournisseursItemsItems] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Items] ([Id])
GO
ALTER TABLE [dbo].[FournisseursItems] CHECK CONSTRAINT [FKFournisseursItemsItems]
GO

ALTER TABLE [dbo].[FournisseursItems]  WITH CHECK ADD  CONSTRAINT [FKFournisseursItemsFournisseurs] FOREIGN KEY([FournisseurId])
REFERENCES [dbo].[Fournisseurs] ([Id])
GO
ALTER TABLE [dbo].[FournisseursItems] CHECK CONSTRAINT [FKFournisseursItemsFournisseurs]
GO

ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FKItemsUnites] FOREIGN KEY([UniteId])
REFERENCES [dbo].[Unites] ([Id])
GO
ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FKItemsUnites]
GO

ALTER TABLE [dbo].[Membres]  WITH CHECK ADD  CONSTRAINT [FKMembresConcentrations] FOREIGN KEY([ConcentrationId])
REFERENCES [dbo].[Concentrations] ([Id])
GO
ALTER TABLE [dbo].[Membres] CHECK CONSTRAINT [FKMembresConcentrations]
GO

ALTER TABLE [dbo].[MembresAllergies]  WITH CHECK ADD  CONSTRAINT [FKMembresAllergiesMembres] FOREIGN KEY([MembreId])
REFERENCES [dbo].[Membres] ([Id])
GO
ALTER TABLE [dbo].[MembresAllergies] CHECK CONSTRAINT [FKMembresAllergiesMembres]
GO

ALTER TABLE [dbo].[MembresAllergies]  WITH CHECK ADD  CONSTRAINT [FKMembresAllergiesAllergies] FOREIGN KEY([AllergieId])
REFERENCES [dbo].[Allergies] ([Id])
GO
ALTER TABLE [dbo].[MembresAllergies] CHECK CONSTRAINT [FKMembresAllergiesAllergies]
GO

ALTER TABLE [dbo].[MembresClubs]  WITH CHECK ADD  CONSTRAINT [FKMembresClubsClubs] FOREIGN KEY([ClubId])
REFERENCES [dbo].[Clubs] ([Id])
GO
ALTER TABLE [dbo].[MembresClubs] CHECK CONSTRAINT [FKMembresClubsClubs]
GO

ALTER TABLE [dbo].[MembresClubs]  WITH CHECK ADD  CONSTRAINT [FKMembresClubsMembres] FOREIGN KEY([MembreId])
REFERENCES [dbo].[Membres] ([Id])
GO
ALTER TABLE [dbo].[MembresClubs] CHECK CONSTRAINT [FKMembresClubsMembres]
GO

ALTER TABLE [dbo].[MembresFormations]  WITH CHECK ADD  CONSTRAINT [FKMembresFormationsMembres] FOREIGN KEY([MembreId])
REFERENCES [dbo].[Membres] ([Id])
GO
ALTER TABLE [dbo].[MembresFormations] CHECK CONSTRAINT [FKMembresFormationsMembres]
GO

ALTER TABLE [dbo].[MembresFormations]  WITH CHECK ADD  CONSTRAINT [FKMembresFormationsFormations] FOREIGN KEY([FormationId])
REFERENCES [dbo].[Formations] ([Id])
GO
ALTER TABLE [dbo].[MembresFormations] CHECK CONSTRAINT [FKMembresFormationsFormations]
GO

ALTER TABLE [dbo].[MembrePreferences]  WITH CHECK ADD  CONSTRAINT [FKMembrePreferencesMembres] FOREIGN KEY([MembreId])
REFERENCES [dbo].[Membres] ([Id])
GO
ALTER TABLE [dbo].[MembrePreferences] CHECK CONSTRAINT [FKMembrePreferencesMembres]
GO

ALTER TABLE [dbo].[Suivies]  WITH CHECK ADD  CONSTRAINT [FKSuiviesMembres] FOREIGN KEY([MembreId])
REFERENCES [dbo].[Membres] ([Id])
GO
ALTER TABLE [dbo].[Suivies] CHECK CONSTRAINT [FKSuiviesMembres]
GO

ALTER TABLE [dbo].[Suivies]  WITH CHECK ADD  CONSTRAINT [FKSuiviesCommandites] FOREIGN KEY([CommanditeId])
REFERENCES [dbo].[Commandites] ([Id])
GO
ALTER TABLE [dbo].[Suivies] CHECK CONSTRAINT [FKSuiviesCommandites]
GO

ALTER TABLE [dbo].[Suivies]  WITH CHECK ADD  CONSTRAINT [FKSuiviesStatutsSuivie] FOREIGN KEY([StatutSuivieId])
REFERENCES [dbo].[StatutsSuivie] ([Id])
GO
ALTER TABLE [dbo].[Suivies] CHECK CONSTRAINT [FKSuiviesStatutsSuivie]
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_PADDING OFF
GO