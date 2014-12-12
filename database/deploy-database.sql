--ALTER database [SIMPLETS] set offline with ROLLBACK IMMEDIATE
--GO

--DROP database [SIMPLETS]
--GO

--CREATE DATABASE [SIMPLETS]
--GO
USE [SIMPLETS]
GO

/****** Object:  Table [dbo].[Adresses]    Script Date: 12/12/2014 2:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Adresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NoCivique] [int] NOT NULL,
	[Rue] [varchar](64) COLLATE French_CI_AS NOT NULL,
	[Appartement] [varchar](10) COLLATE French_CI_AS NULL,
	[Ville] [varchar](128) COLLATE French_CI_AS NOT NULL,
	[CodePostal] [varchar](16) COLLATE French_CI_AS NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Allergies]    Script Date: 12/12/2014 2:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Allergies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](128) COLLATE French_CI_AS NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Audits]    Script Date: 12/12/2014 2:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Audits](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CodeUniversel] [varchar](50) COLLATE French_CI_AS NOT NULL,
	[AdresseIp] [varchar](255) COLLATE French_CI_AS NULL,
	[Message] [varchar](255) COLLATE French_CI_AS NULL,
	[DateAudit] [datetime] NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Clubs]    Script Date: 12/12/2014 2:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Clubs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [varchar](50) COLLATE French_CI_AS NOT NULL,
	[Description] [varchar](255) COLLATE French_CI_AS NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Commandites]    Script Date: 12/12/2014 2:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Commandites](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FournisseurId] [int] NOT NULL,
	[ItemId] [int] NOT NULL,
	[ClubId] [int] NOT NULL,
	[Valeur] [numeric](6, 2) NOT NULL,
	[Nature] [varchar](64) COLLATE French_CI_AS NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Concentrations]    Script Date: 12/12/2014 2:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Concentrations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Acronyme] [varchar](10) COLLATE French_CI_AS NOT NULL,
	[Description] [varchar](150) COLLATE French_CI_AS NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Contacts_Urgence]    Script Date: 12/12/2014 2:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ContactsUrgence](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MembreId] [int] NOT NULL,
	[LienParenteId] [int] NOT NULL,
	[Nom] [varchar](50) COLLATE French_CI_AS NOT NULL,
	[Prenom] [varchar](50) COLLATE French_CI_AS NOT NULL,
	[Telephone] [varchar](16) COLLATE French_CI_AS NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Evenements]    Script Date: 12/12/2014 2:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Evenements](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClubId] [int] NOT NULL,
	[Nom] [varchar](50) COLLATE French_CI_AS NOT NULL,
	[Description] [varchar](255) COLLATE French_CI_AS NOT NULL,
	[DateDebut] [datetime] NOT NULL,
	[DateFin] [datetime] NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Formations]    Script Date: 12/12/2014 2:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Formations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titre] [varchar](50) COLLATE French_CI_AS NULL,
	[Description] [varchar](150) COLLATE French_CI_AS NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Fournisseurs]    Script Date: 12/12/2014 2:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Fournisseurs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AdresseId] [int] NOT NULL,
	[Nom] [varchar](255) COLLATE French_CI_AS NOT NULL,
	[Contact] [varchar](64) COLLATE French_CI_AS NOT NULL,
	[Telephone] [varchar](24) COLLATE French_CI_AS NOT NULL,
	[Fax] [varchar](24) COLLATE French_CI_AS NULL,
	[Courriel] [varchar](255) COLLATE French_CI_AS NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Fournisseurs_Items]    Script Date: 12/12/2014 2:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FournisseursItems](
	[FournisseurId] [int] NOT NULL,
	[ItemId] [int] NOT NULL,
	[CodeFournisseur] [varchar](20) NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FournisseurId] ASC,
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Items]    Script Date: 12/12/2014 2:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Items](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UnitId] [int] NOT NULL,
	[Description] [varchar](255) COLLATE French_CI_AS NOT NULL,
	[CodeClub] [varchar](32) COLLATE French_CI_AS NULL,
	[QuantiteCourante] [numeric](6, 3) NOT NULL,
	[QunatiteMin] [numeric](6, 3) NULL,
	[QuantiteMax] [numeric](6, 3) NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Liens_Parente]    Script Date: 12/12/2014 2:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LiensParente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) COLLATE French_CI_AS NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Membres]    Script Date: 12/12/2014 2:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Membres](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConcentrationId] [int] NULL,
	[Nom] [varchar](64) COLLATE French_CI_AS NOT NULL,
	[Prenom] [varchar](64) COLLATE French_CI_AS NOT NULL,
	[Courriel] [varchar](255) COLLATE French_CI_AS NOT NULL,
	[CodeUniversel] [varchar](10) COLLATE French_CI_AS NOT NULL,
	[Actif] [bit] NOT NULL,
	[Telephone] [varchar](16) COLLATE French_CI_AS NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Membres_Allergies]    Script Date: 12/12/2014 2:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MembresAllergies](
	[MembreId] [int] NOT NULL,
	[AllergieId] [int] NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MembreId] ASC,
	[AllergieId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Membres_Clubs]    Script Date: 12/12/2014 2:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MembresClubs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ClubId] [int] NOT NULL,
	[MembreId] [int] NOT NULL,
	[DateDebut] [datetime] NOT NULL,
	[DateFin] [datetime] NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Membres_Clubs_Roles]    Script Date: 12/12/2014 2:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MembreClubRoles](
	[MembreClubId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MembreClubId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Membres_Formations]    Script Date: 12/12/2014 2:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Membres_Preferences]    Script Date: 12/12/2014 2:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MembrePreferences](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MembreId] [int] NOT NULL,
	[PreferenceKey] [varchar](50) COLLATE French_CI_AS NOT NULL,
	[PreferenceValue] [varchar](150) COLLATE French_CI_AS NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 12/12/2014 2:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [varchar](50) COLLATE French_CI_AS NOT NULL,
	[Description] [varchar](255) COLLATE French_CI_AS NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Suivie_Statuts]    Script Date: 12/12/2014 2:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StatutsSuivie](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) COLLATE French_CI_AS NOT NULL,
	[Description] [varchar](150) COLLATE French_CI_AS NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Suivies]    Script Date: 12/12/2014 2:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Suivies](
	[CommanditeId] [int] NOT NULL,
	[MembreId] [int] NOT NULL,
	[StatutSuivieId] [int] NOT NULL,
	[DateSuivie] [datetime] NOT NULL,
	[Commentaire] [varchar](255) COLLATE French_CI_AS NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CommanditeId] ASC,
	[MembreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Units]    Script Date: 12/12/2014 2:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Unites](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](8) COLLATE French_CI_AS NOT NULL,
	[Systeme] [varchar](12) COLLATE French_CI_AS NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Commandites]  WITH CHECK ADD  CONSTRAINT [FKCommandite304970] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Items] ([id])
GO
ALTER TABLE [dbo].[Commandites] CHECK CONSTRAINT [FKCommandite304970]
GO

ALTER TABLE [dbo].[Commandites]  WITH CHECK ADD  CONSTRAINT [FKCommandite426027] FOREIGN KEY([Clubs_id])
REFERENCES [dbo].[Clubs] ([id])
GO
ALTER TABLE [dbo].[Commandites] CHECK CONSTRAINT [FKCommandite426027]
GO

ALTER TABLE [dbo].[Commandites]  WITH CHECK ADD  CONSTRAINT [FKCommandite968053] FOREIGN KEY([Fournisseurs_id])
REFERENCES [dbo].[Fournisseurs] ([id])
GO
ALTER TABLE [dbo].[Commandites] CHECK CONSTRAINT [FKCommandite968053]
GO

ALTER TABLE [dbo].[Contacts_Urgence]  WITH CHECK ADD  CONSTRAINT [FKContacts_U164890] FOREIGN KEY([liens_parente_id])
REFERENCES [dbo].[Liens_Parente] ([id])
GO
ALTER TABLE [dbo].[Contacts_Urgence] CHECK CONSTRAINT [FKContacts_U164890]
GO

ALTER TABLE [dbo].[Contacts_Urgence]  WITH CHECK ADD  CONSTRAINT [FKContacts_U687324] FOREIGN KEY([membres_id])
REFERENCES [dbo].[Membres] ([id])
GO
ALTER TABLE [dbo].[Contacts_Urgence] CHECK CONSTRAINT [FKContacts_U687324]
GO

ALTER TABLE [dbo].[Evenements]  WITH CHECK ADD  CONSTRAINT [FKEvenements917475] FOREIGN KEY([Clubs_id])
REFERENCES [dbo].[Clubs] ([id])
GO
ALTER TABLE [dbo].[Evenements] CHECK CONSTRAINT [FKEvenements917475]
GO

ALTER TABLE [dbo].[Fournisseurs]  WITH CHECK ADD  CONSTRAINT [FKFournisseu604575] FOREIGN KEY([Adresses_id])
REFERENCES [dbo].[Adresses] ([id])
GO
ALTER TABLE [dbo].[Fournisseurs] CHECK CONSTRAINT [FKFournisseu604575]
GO

ALTER TABLE [dbo].[Fournisseurs_Items]  WITH CHECK ADD  CONSTRAINT [FKFournisseu24556] FOREIGN KEY([Items_id])
REFERENCES [dbo].[Items] ([id])
GO
ALTER TABLE [dbo].[Fournisseurs_Items] CHECK CONSTRAINT [FKFournisseu24556]
GO

ALTER TABLE [dbo].[Fournisseurs_Items]  WITH CHECK ADD  CONSTRAINT [FKFournisseu723122] FOREIGN KEY([Fournisseurs_id])
REFERENCES [dbo].[Fournisseurs] ([id])
GO
ALTER TABLE [dbo].[Fournisseurs_Items] CHECK CONSTRAINT [FKFournisseu723122]
GO

ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FKItems252341] FOREIGN KEY([Units_id])
REFERENCES [dbo].[Units] ([id])
GO
ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FKItems252341]
GO

ALTER TABLE [dbo].[Membres]  WITH CHECK ADD  CONSTRAINT [FKMembres495106] FOREIGN KEY([Concentrations_id])
REFERENCES [dbo].[Concentrations] ([id])
GO
ALTER TABLE [dbo].[Membres] CHECK CONSTRAINT [FKMembres495106]
GO

ALTER TABLE [dbo].[Membres_Allergies]  WITH CHECK ADD  CONSTRAINT [FKMembres_Al548936] FOREIGN KEY([Membres_id])
REFERENCES [dbo].[Membres] ([id])
GO
ALTER TABLE [dbo].[Membres_Allergies] CHECK CONSTRAINT [FKMembres_Al548936]
GO

ALTER TABLE [dbo].[Membres_Allergies]  WITH CHECK ADD  CONSTRAINT [FKMembres_Al895678] FOREIGN KEY([Allergies_id])
REFERENCES [dbo].[Allergies] ([id])
GO
ALTER TABLE [dbo].[Membres_Allergies] CHECK CONSTRAINT [FKMembres_Al895678]
GO

ALTER TABLE [dbo].[Membres_Clubs]  WITH CHECK ADD  CONSTRAINT [FKMembres_Cl269114] FOREIGN KEY([Clubs_id])
REFERENCES [dbo].[Clubs] ([id])
GO
ALTER TABLE [dbo].[Membres_Clubs] CHECK CONSTRAINT [FKMembres_Cl269114]
GO

ALTER TABLE [dbo].[Membres_Clubs]  WITH CHECK ADD  CONSTRAINT [FKMembres_Cl936196] FOREIGN KEY([Membres_id])
REFERENCES [dbo].[Membres] ([id])
GO
ALTER TABLE [dbo].[Membres_Clubs] CHECK CONSTRAINT [FKMembres_Cl936196]
GO

ALTER TABLE [dbo].[Membres_Clubs_Roles]  WITH CHECK ADD  CONSTRAINT [FKMembres_Cl153481] FOREIGN KEY([Membres_Clubs_id])
REFERENCES [dbo].[Membres_Clubs] ([id])
GO
ALTER TABLE [dbo].[Membres_Clubs_Roles] CHECK CONSTRAINT [FKMembres_Cl153481]
GO

ALTER TABLE [dbo].[Membres_Clubs_Roles]  WITH CHECK ADD  CONSTRAINT [FKMembres_Cl391451] FOREIGN KEY([Roles_id])
REFERENCES [dbo].[Roles] ([id])
GO
ALTER TABLE [dbo].[Membres_Clubs_Roles] CHECK CONSTRAINT [FKMembres_Cl391451]
GO

ALTER TABLE [dbo].[Membres_Formations]  WITH CHECK ADD  CONSTRAINT [FKMembres_Fo495691] FOREIGN KEY([Membres_id])
REFERENCES [dbo].[Membres] ([id])
GO
ALTER TABLE [dbo].[Membres_Formations] CHECK CONSTRAINT [FKMembres_Fo495691]
GO

ALTER TABLE [dbo].[Membres_Formations]  WITH CHECK ADD  CONSTRAINT [FKMembres_Fo906962] FOREIGN KEY([Formations_id])
REFERENCES [dbo].[Formations] ([id])
GO
ALTER TABLE [dbo].[Membres_Formations] CHECK CONSTRAINT [FKMembres_Fo906962]
GO

ALTER TABLE [dbo].[Membres_Preferences]  WITH CHECK ADD  CONSTRAINT [FKMembres_Pr229875] FOREIGN KEY([membres_id])
REFERENCES [dbo].[Membres] ([id])
GO
ALTER TABLE [dbo].[Membres_Preferences] CHECK CONSTRAINT [FKMembres_Pr229875]
GO

ALTER TABLE [dbo].[Suivies]  WITH CHECK ADD  CONSTRAINT [FKSuivies436259] FOREIGN KEY([Membres_id])
REFERENCES [dbo].[Membres] ([id])
GO
ALTER TABLE [dbo].[Suivies] CHECK CONSTRAINT [FKSuivies436259]
GO

ALTER TABLE [dbo].[Suivies]  WITH CHECK ADD  CONSTRAINT [FKSuivies572159] FOREIGN KEY([Commandites_id])
REFERENCES [dbo].[Commandites] ([id])
GO
ALTER TABLE [dbo].[Suivies] CHECK CONSTRAINT [FKSuivies572159]
GO

ALTER TABLE [dbo].[Suivies]  WITH CHECK ADD  CONSTRAINT [FKSuivies746520] FOREIGN KEY([Suivie_Statuts_id])
REFERENCES [dbo].[Suivie_Statuts] ([id])
GO
ALTER TABLE [dbo].[Suivies] CHECK CONSTRAINT [FKSuivies746520]
GO
