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
	[Actif] [bit] NOT NULL,
	[Public] [bit] NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED
(
	[Id] ASC
)) ON [PRIMARY]
GO

CREATE TABLE [userspace].[ProfilsAvances](
	[ProfilId] [int] NOT NULL,
	[CodePermanent] [varchar](10) NULL,
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
CREATE TABLE [userspace].[ProfilsPreferences](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProfilId] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Value] [varchar](150) NOT NULL
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [userspace].[ProfilsFormations]  Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [userspace].[ProfilsFormations](
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

/****** Object:  Table [userspace].[ProfilsContactsUrgence]  Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [userspace].[ProfilsContactsUrgence](
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
CREATE TABLE [userspace].[ProfilsAllergies](
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

ALTER TABLE [userspace].[ProfilsPreferences]  WITH CHECK ADD CONSTRAINT [FKProfilsPreferencesProfils] FOREIGN KEY([ProfilId])
REFERENCES [userspace].[Profils] ([Id])
GO
ALTER TABLE [userspace].[ProfilsPreferences] CHECK CONSTRAINT [FKProfilsPreferencesProfils]
GO

ALTER TABLE [userspace].[ProfilsFormations]  WITH CHECK ADD CONSTRAINT [FKProfilsFormationsProfils] FOREIGN KEY([ProfilId])
REFERENCES [userspace].[Profils] ([Id])
GO
ALTER TABLE [userspace].[ProfilsFormations] CHECK CONSTRAINT [FKProfilsFormationsProfils]
GO

ALTER TABLE [userspace].[ProfilsContactsUrgence]  WITH CHECK ADD CONSTRAINT [FKProfilsContactsUrgenceProfils] FOREIGN KEY([ProfilId])
REFERENCES [userspace].[Profils] ([Id])
GO
ALTER TABLE [userspace].[ProfilsContactsUrgence] CHECK CONSTRAINT [FKProfilsContactsUrgenceProfils]
GO

ALTER TABLE [userspace].[ProfilsContactsUrgence]  WITH CHECK ADD CONSTRAINT [FKProfilsContactsUrgenceContacts] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contacts] ([Id])
GO
ALTER TABLE [userspace].[ProfilsContactsUrgence] CHECK CONSTRAINT [FKProfilsContactsUrgenceContacts]
GO

ALTER TABLE [userspace].[ProfilsAllergies]  WITH CHECK ADD CONSTRAINT [FKProfilsAllergies] FOREIGN KEY([ProfilId])
REFERENCES [userspace].[Profils] ([Id])
GO
ALTER TABLE [userspace].[ProfilsAllergies] CHECK CONSTRAINT [FKProfilsAllergies]
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_PADDING OFF
GO