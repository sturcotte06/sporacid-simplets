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

IF (NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'userspace')) 
BEGIN
    EXEC ('CREATE SCHEMA [userspace] AUTHORIZATION [dbo]')
END

/****** Object:  Table [userspace].[Profils]  Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [userspace].[Profils](
	[Id] [int] IDENTITY(1, 1) NOT NULL,
	[ConcentrationId] [int] NULL,
	[CodeUniversel] [varchar](10) NULL,
	[Nom] [varchar](50) NULL,
	[Prenom] [varchar](50) NULL,
	[Avatar] [varbinary](4000) NULL,
	[Xp] [int] NOT NULL,
	[Actif] [bit] NOT NULL,
	[Public] [bit] NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED
(
	[Id] ASC
)) ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX UIXProfilsCodeUniversel ON [userspace].[Profils] ([CodeUniversel])
GO

CREATE TABLE [userspace].[XpTable](
	[Level] [int] NOT NULL,
	[RequiredXp] [int] NOT NULL
PRIMARY KEY CLUSTERED
(
	[Level] ASC
)) ON [PRIMARY]
GO

CREATE TABLE [userspace].[ProfilsAvances](
	[ProfilId] [int] NOT NULL,
	[CodePermanent] [varchar](12) NULL,
	[DateNaissance] [datetime] NULL,
	[Courriel] [varchar](250) NULL,
	[Telephone] [varchar](20) NULL,
	[Public] [bit] NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED
(
	[ProfilId] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Membres_Preferences]    Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [userspace].[Preferences](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProfilId] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Value] [varchar](150) NOT NULL
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX UIXPreferencesProfilId ON [userspace].[Preferences] ([ProfilId])
GO

/****** Object:  Table [userspace].[ProfilsFormations]  Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [userspace].[Formations](
	[Id] [int] IDENTITY(1, 1) NOT NULL,
	[ProfilId] [int] NOT NULL,
	[Titre] [varchar](50) NOT NULL,
	[Description] [varchar](150) NULL,
	[Public] [bit] NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED
(
	[Id] ASC
)) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX UIXFormationsProfilId ON [userspace].[Formations] ([ProfilId])
GO

/****** Object:  Table [userspace].[ProfilsContactsUrgence]  Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [userspace].[ContactsUrgence](
	[ProfilId] [int] NOT NULL,
	[ContactId] [int] NOT NULL,
	[Public] [bit] NOT NULL,
PRIMARY KEY CLUSTERED
(
	[ProfilId] ASC,
	[ContactId] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [userspace].[MembresAllergies]  Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [userspace].[Allergies](
	[Id] [int] IDENTITY(1, 1) NOT NULL,
	[ProfilId] [int] NOT NULL,
	[Nom] [varchar](50) NOT NULL,
	[Description] [varchar](150) NOT NULL,
	[Public] [bit] NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX UIXAllergiesProfilId ON [userspace].[Allergies] ([ProfilId])
GO

/****** Foreign keys ******/
ALTER TABLE [userspace].[Profils]  WITH CHECK ADD CONSTRAINT [FKProfilsConcentrations] FOREIGN KEY([ConcentrationId])
REFERENCES [dbo].[Concentrations] ([Id])
GO
ALTER TABLE [userspace].[Profils] CHECK CONSTRAINT [FKProfilsConcentrations]
GO

ALTER TABLE [userspace].[ProfilsAvances]  WITH CHECK ADD CONSTRAINT [FKProfilsAvancesProfils] FOREIGN KEY([ProfilId])
REFERENCES [userspace].[Profils] ([Id])
GO
ALTER TABLE [userspace].[ProfilsAvances] CHECK CONSTRAINT [FKProfilsAvancesProfils]
GO

ALTER TABLE [userspace].[Preferences]  WITH CHECK ADD CONSTRAINT [FKPreferencesProfils] FOREIGN KEY([ProfilId])
REFERENCES [userspace].[Profils] ([Id])
GO
ALTER TABLE [userspace].[Preferences] CHECK CONSTRAINT [FKPreferencesProfils]
GO

ALTER TABLE [userspace].[Formations]  WITH CHECK ADD CONSTRAINT [FKFormationsProfils] FOREIGN KEY([ProfilId])
REFERENCES [userspace].[Profils] ([Id])
GO
ALTER TABLE [userspace].[Formations] CHECK CONSTRAINT [FKFormationsProfils]
GO

ALTER TABLE [userspace].[ContactsUrgence]  WITH CHECK ADD CONSTRAINT [FKContactsUrgenceProfils] FOREIGN KEY([ProfilId])
REFERENCES [userspace].[Profils] ([Id])
GO
ALTER TABLE [userspace].[ContactsUrgence] CHECK CONSTRAINT [FKContactsUrgenceProfils]
GO

ALTER TABLE [userspace].[ContactsUrgence]  WITH CHECK ADD CONSTRAINT [FKContactsUrgenceContacts] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contacts] ([Id])
GO
ALTER TABLE [userspace].[ContactsUrgence] CHECK CONSTRAINT [FKContactsUrgenceContacts]
GO

ALTER TABLE [userspace].[Allergies]  WITH CHECK ADD CONSTRAINT [FKProfilsAllergies] FOREIGN KEY([ProfilId])
REFERENCES [userspace].[Profils] ([Id])
GO
ALTER TABLE [userspace].[Allergies] CHECK CONSTRAINT [FKProfilsAllergies]
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_PADDING OFF
GO