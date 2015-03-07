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

IF (NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'clubs')) 
BEGIN
    EXEC ('CREATE SCHEMA [clubs] AUTHORIZATION [dbo]')
END

/****** Object:  Table [dbo].[Clubs]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [clubs].[Clubs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [varchar](50) NOT NULL UNIQUE,
	[Description] [varchar](250) NULL,
	[Logo] [varbinary](4000) NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX UIXClubsNom ON [clubs].[Clubs] ([Nom])
GO

/****** Object:  Table [dbo].[Membres_Clubs]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [clubs].[Membres](
	[Id] [int] IDENTITY(1, 1) NOT NULL,
	[ClubId] [int] NOT NULL,
	[CodeUniversel] [varchar](10) NOT NULL,
	[Titre] [varchar](50) NOT NULL,
	[DateDebut] [datetime] NOT NULL,
	[DateFin] [datetime] NULL,
	[Actif] [bit] NOT NULL
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX UIXMembresCodeUniverselClubId ON [clubs].[Membres] ([ClubId], [CodeUniversel])
GO

/****** Object:  Table [dbo].[Commandites]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [clubs].[Commandites](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClubId] [int] NOT NULL,
	[FournisseurId] [int] NULL,
	[ItemId] [int] NULL,
	[Valeur] [numeric](6, 2) NOT NULL,
	[Nature] [varchar](50) NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX UIXCommanditesClubId ON [clubs].[Commandites] ([ClubId])
GO

/****** Object:  Table [dbo].[Evenements]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [clubs].[Evenements](
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

CREATE NONCLUSTERED INDEX UIXEvenementsClubId ON [clubs].[Evenements] ([ClubId])
GO

/****** Object:  Table [dbo].[Fournisseurs]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [clubs].[Fournisseurs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClubId] [int] NOT NULL,
	[AdresseId] [int] NULL,
	[ContactId] [int] NULL,
	[Nom] [varchar](250) NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX UIXFournisseursClubId ON [clubs].[Fournisseurs] ([ClubId])
GO

/****** Object:  Table [dbo].[Fournisseurs_Items]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [clubs].[FournisseursItems](
	[FournisseurId] [int] NOT NULL,
	[ItemId] [int] NOT NULL,
	[CodeFournisseur] [varchar](20) NOT NULL
PRIMARY KEY CLUSTERED 
(
	[FournisseurId] ASC,
	[ItemId] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Items]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [clubs].[Items](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClubId] [int] NOT NULL,
	[UniteId] [int] NOT NULL,
	[Code] [varchar](20) NULL,
	[Description] [varchar](250) NOT NULL,
	[Quantite] [numeric](6, 3) NOT NULL,
	[QuantiteMin] [numeric](6, 3) NULL,
	[QuantiteMax] [numeric](6, 3) NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX UIXItemsClubId ON [clubs].[Items] ([ClubId])
GO

/****** Object:  Table [dbo].[StatutsSuivie]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [clubs].[StatutsSuivie](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[Description] [varchar](150) NOT NULL
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Suivies]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [clubs].[Suivies](
	[Id] [int] IDENTITY (1, 1) NOT NULL,
	[CommanditeId] [int] NOT NULL,
	[MembreId] [int] NULL,
	[StatutSuivieId] [int] NOT NULL,
	[DateSuivie] [datetime] NOT NULL,
	[Commentaire] [varchar](250) NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX UIXItemsCommanditeId ON [clubs].[Suivies] ([CommanditeId])
GO

/****** Object:  Table [dbo].[Groupes]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [clubs].[Groupes](
	[Id] [int] IDENTITY (1, 1) NOT NULL,
	[ClubId] [int] NOT NULL,
	[Nom] [varchar](50) NOT NULL UNIQUE,
	[Description] [varchar](250) NULL,
	[Version] [rowversion] NOT NULL
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX UIXGroupesClubId ON [clubs].[Groupes] ([ClubId])
GO

/****** Object:  Table [dbo].[GroupesMembres]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [clubs].[GroupesMembres](
	[GroupeId] [int] NOT NULL,
	[MembreId] [int] NOT NULL
PRIMARY KEY CLUSTERED 
(
	[GroupeId] ASC,
	[MembreId] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Meetings]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [clubs].[Meetings](
	[Id] [int] IDENTITY (1, 1) NOT NULL,
	[ClubId] [int] NOT NULL,
	[ConvocateurId] [int] NOT NULL,
	[DateDebut] [datetime] NOT NULL,
	[DateFin] [datetime] NOT NULL,
	[Commentaire] [varchar](250) NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX UIXMeetingsClubId ON [clubs].[Meetings] ([ClubId])
GO

/****** Object:  Table [dbo].[MeetingsMembres]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [clubs].[MeetingsMembres](
	[MeetingId] [int] NOT NULL,
	[MembreId] [int] NOT NULL,
	[PresenceRequise] [bit] NOT NULL
PRIMARY KEY CLUSTERED 
(
	[MeetingId] ASC,
	[MembreId] ASC
)) ON [PRIMARY]
GO

/****** Foreign keys ******/
ALTER TABLE [clubs].[Commandites]  WITH CHECK ADD CONSTRAINT [FKCommanditesItems] FOREIGN KEY([ItemId])
REFERENCES [clubs].[Items] ([Id])
GO
ALTER TABLE [clubs].[Commandites] CHECK CONSTRAINT [FKCommanditesItems]
GO

ALTER TABLE [clubs].[Commandites]  WITH CHECK ADD  CONSTRAINT [FKCommanditesClubs] FOREIGN KEY([ClubId])
REFERENCES [clubs].[Clubs] ([Id])
GO
ALTER TABLE [clubs].[Commandites] CHECK CONSTRAINT [FKCommanditesClubs]
GO

ALTER TABLE [clubs].[Commandites]  WITH CHECK ADD  CONSTRAINT [FKCommanditesFournisseurs] FOREIGN KEY([FournisseurId])
REFERENCES [clubs].[Fournisseurs] ([Id])
GO
ALTER TABLE [clubs].[Commandites] CHECK CONSTRAINT [FKCommanditesFournisseurs]
GO

ALTER TABLE [clubs].[Evenements]  WITH CHECK ADD  CONSTRAINT [FKEvenementsClubs] FOREIGN KEY([ClubId])
REFERENCES [clubs].[Clubs] ([Id])
GO
ALTER TABLE [clubs].[Evenements] CHECK CONSTRAINT [FKEvenementsClubs]
GO

ALTER TABLE [clubs].[Fournisseurs]  WITH CHECK ADD  CONSTRAINT [FKFournisseursAdresses] FOREIGN KEY([AdresseId])
REFERENCES [dbo].[Adresses] ([Id])
GO
ALTER TABLE [clubs].[Fournisseurs] CHECK CONSTRAINT [FKFournisseursAdresses]
GO

ALTER TABLE [clubs].[Fournisseurs]  WITH CHECK ADD  CONSTRAINT [FKFournisseursContacts] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contacts] ([Id])
GO
ALTER TABLE [clubs].[Fournisseurs] CHECK CONSTRAINT [FKFournisseursContacts]
GO

ALTER TABLE [clubs].[Fournisseurs]  WITH CHECK ADD  CONSTRAINT [FKFournisseursClubs] FOREIGN KEY([ClubId])
REFERENCES [clubs].[Clubs] ([Id])
GO
ALTER TABLE [clubs].[Fournisseurs] CHECK CONSTRAINT [FKFournisseursClubs]
GO

ALTER TABLE [clubs].[FournisseursItems]  WITH CHECK ADD  CONSTRAINT [FKFournisseursItemsItems] FOREIGN KEY([ItemId])
REFERENCES [clubs].[Items] ([Id])
GO
ALTER TABLE [clubs].[FournisseursItems] CHECK CONSTRAINT [FKFournisseursItemsItems]
GO

ALTER TABLE [clubs].[FournisseursItems]  WITH CHECK ADD  CONSTRAINT [FKFournisseursItemsFournisseurs] FOREIGN KEY([FournisseurId])
REFERENCES [clubs].[Fournisseurs] ([Id])
GO
ALTER TABLE [clubs].[FournisseursItems] CHECK CONSTRAINT [FKFournisseursItemsFournisseurs]
GO

ALTER TABLE [clubs].[Items]  WITH CHECK ADD  CONSTRAINT [FKItemsUnites] FOREIGN KEY([UniteId])
REFERENCES [dbo].[Unites] ([Id])
GO
ALTER TABLE [clubs].[Items] CHECK CONSTRAINT [FKItemsUnites]
GO

ALTER TABLE [clubs].[Items]  WITH CHECK ADD  CONSTRAINT [FKItemsClubs] FOREIGN KEY([ClubId])
REFERENCES [clubs].[Clubs] ([Id])
GO
ALTER TABLE [clubs].[Items] CHECK CONSTRAINT [FKItemsClubs]
GO

ALTER TABLE [clubs].[Suivies]  WITH CHECK ADD  CONSTRAINT [FKSuiviesMembres] FOREIGN KEY([MembreId])
REFERENCES [clubs].[Membres] ([Id])
GO
ALTER TABLE [clubs].[Suivies] CHECK CONSTRAINT [FKSuiviesMembres]
GO

ALTER TABLE [clubs].[Suivies]  WITH CHECK ADD  CONSTRAINT [FKSuiviesCommandites] FOREIGN KEY([CommanditeId])
REFERENCES [clubs].[Commandites] ([Id])
GO
ALTER TABLE [clubs].[Suivies] CHECK CONSTRAINT [FKSuiviesCommandites]
GO

ALTER TABLE [clubs].[Suivies]  WITH CHECK ADD  CONSTRAINT [FKSuiviesStatutsSuivie] FOREIGN KEY([StatutSuivieId])
REFERENCES [clubs].[StatutsSuivie] ([Id])
GO
ALTER TABLE [clubs].[Suivies] CHECK CONSTRAINT [FKSuiviesStatutsSuivie]
GO

ALTER TABLE [clubs].[Membres]  WITH CHECK ADD  CONSTRAINT [FKMembresClubs] FOREIGN KEY([ClubId])
REFERENCES [clubs].[Clubs] ([Id])
GO
ALTER TABLE [clubs].[Membres] CHECK CONSTRAINT [FKMembresClubs]
GO

ALTER TABLE [clubs].[Groupes]  WITH CHECK ADD CONSTRAINT [FKGroupesClubs] FOREIGN KEY([ClubId])
REFERENCES [clubs].[Clubs] ([Id])
GO
ALTER TABLE [clubs].[Groupes] CHECK CONSTRAINT [FKGroupesClubs]
GO

ALTER TABLE [clubs].[GroupesMembres]  WITH CHECK ADD CONSTRAINT [FKGroupesMembresGroupes] FOREIGN KEY([GroupeId])
REFERENCES [clubs].[Groupes] ([Id])
GO
ALTER TABLE [clubs].[GroupesMembres] CHECK CONSTRAINT [FKGroupesMembresGroupes]
GO

ALTER TABLE [clubs].[GroupesMembres]  WITH CHECK ADD CONSTRAINT [FKGroupesMembresMembres] FOREIGN KEY([MembreId])
REFERENCES [clubs].[Membres] ([Id])
GO
ALTER TABLE [clubs].[GroupesMembres] CHECK CONSTRAINT [FKGroupesMembresMembres]
GO

ALTER TABLE [clubs].[Meetings]  WITH CHECK ADD CONSTRAINT [FKMeetingsClubs] FOREIGN KEY([ClubId])
REFERENCES [clubs].[Clubs] ([Id])
GO
ALTER TABLE [clubs].[Meetings] CHECK CONSTRAINT [FKMeetingsClubs]
GO

ALTER TABLE [clubs].[MeetingsMembres]  WITH CHECK ADD CONSTRAINT [FKMeetingsMembresMeetings] FOREIGN KEY([MeetingId])
REFERENCES [clubs].[Meetings] ([Id])
GO
ALTER TABLE [clubs].[MeetingsMembres] CHECK CONSTRAINT [FKMeetingsMembresMeetings]
GO

ALTER TABLE [clubs].[MeetingsMembres]  WITH CHECK ADD CONSTRAINT [FKMeetingsMembresMembres] FOREIGN KEY([MembreId])
REFERENCES [clubs].[Membres] ([Id])
GO
ALTER TABLE [clubs].[MeetingsMembres] CHECK CONSTRAINT [FKMeetingsMembresMembres]
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_PADDING OFF
GO