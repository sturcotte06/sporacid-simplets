USE [SIMPLETS]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

-- CREATE SCHEMA [security]
-- GO

/****** Object:  Table [security].[Resources]  Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [security].[Resources](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ModuleId] [int] NOT NULL,
	[Name] [varchar](20) NOT NULL,
	[BaseUrl] [varchar](50) NOT NULL
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [security].[Claims]  Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [security].[Claims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](20) NOT NULL
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [security].[ResourcesRequiredClaims]  Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [security].[ResourcesRequiredClaims](
	[ResourceId] [int] NOT NULL,
	[ClaimId] [int] NOT NULL,
PRIMARY KEY CLUSTERED
(
	[ResourceId] ASC,
	[ClaimId] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [security].[PrincipalsResourcesClaims]  Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [security].[PrincipalsResourcesClaims](
	[PrincipalId] [int] NOT NULL,
	[ResourceId] [int] NOT NULL,
	[ClaimId] [int] NOT NULL,
PRIMARY KEY CLUSTERED
(
	[PrincipalId] ASC,
	[ResourceId] ASC,
	[ClaimId] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [security].[Principals] Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [security].[Principals](
	[Id] [int] IDENTITY(1, 1) NOT NULL,
	[Identity] [varchar](25) NOT NULL,
	[Version] [rowversion] NOT NULL,
PRIMARY KEY CLUSTERED
(
	[Id] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [security].[AuthenticationAudit]  Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [security].[PrincipalAudit](
	[Id] [bigint] IDENTITY(1, 1) NOT NULL,
	[PrincipalId] [int] NOT NULL,
	[IpAddress] [varchar](15) NOT NULL,
	[Date] [datetime] NOT NULL
PRIMARY KEY CLUSTERED
(
	[Id] ASC
)) ON [PRIMARY]
GO

CREATE TABLE [security].[Modules](
	[Id] [int] IDENTITY(1, 1) NOT NULL,
	[Name] [varchar](20) NOT NULL
PRIMARY KEY CLUSTERED
(
	[Id] ASC
)) ON [PRIMARY]
GO

CREATE TABLE [security].[RoleTemplates](
	[Id] [int] IDENTITY(1, 1) NOT NULL,
	[Name] [varchar](20) NOT NULL
PRIMARY KEY CLUSTERED
(
	[Id] ASC
)) ON [PRIMARY]
GO

CREATE TABLE [security].[RoleTemplatesModulesClaims](
	[RoleTemplateId] [int] NOT NULL,
	[ModuleId] [int] NOT NULL,
	[ClaimId] [int] NOT NULL
PRIMARY KEY CLUSTERED
(
	[RoleTemplateId] ASC,
	[ModuleId] ASC,
	[ClaimId] ASC
)) ON [PRIMARY]
GO


/****** Foreign keys ******/
ALTER TABLE [security].[ResourcesRequiredClaims]  WITH CHECK ADD CONSTRAINT [FKResourcesRequiredClaimsResources] FOREIGN KEY([ResourceId])
REFERENCES [security].[Resources] ([Id])
GO
ALTER TABLE [security].[ResourcesRequiredClaims] CHECK CONSTRAINT [FKResourcesRequiredClaimsResources]
GO

ALTER TABLE [security].[ResourcesRequiredClaims]  WITH CHECK ADD CONSTRAINT [FKResourcesRequiredClaimsClaims] FOREIGN KEY([ClaimId])
REFERENCES [security].[Claims] ([Id])
GO
ALTER TABLE [security].[ResourcesRequiredClaims] CHECK CONSTRAINT [FKResourcesRequiredClaimsClaims]
GO

ALTER TABLE [security].[PrincipalsResourcesClaims]  WITH CHECK ADD CONSTRAINT [FKPrincipalsResourcesClaimsPrincipals] FOREIGN KEY([PrincipalId])
REFERENCES [security].[Principals] ([Id])
GO
ALTER TABLE [security].[PrincipalsResourcesClaims] CHECK CONSTRAINT [FKPrincipalsResourcesClaimsPrincipals]
GO

ALTER TABLE [security].[PrincipalsResourcesClaims]  WITH CHECK ADD CONSTRAINT [FKPrincipalsResourcesClaimsResources] FOREIGN KEY([ResourceId])
REFERENCES [security].[Resources] ([Id])
GO
ALTER TABLE [security].[PrincipalsResourcesClaims] CHECK CONSTRAINT [FKPrincipalsResourcesClaimsResources]
GO

ALTER TABLE [security].[PrincipalsResourcesClaims]  WITH CHECK ADD CONSTRAINT [FKPrincipalsResourcesClaimsClaims] FOREIGN KEY([ClaimId])
REFERENCES [security].[Claims] ([Id])
GO
ALTER TABLE [security].[PrincipalsResourcesClaims] CHECK CONSTRAINT [FKPrincipalsResourcesClaimsClaims]
GO

ALTER TABLE [security].[PrincipalAudit]  WITH CHECK ADD CONSTRAINT [FKPrincipalAuditPrincipals] FOREIGN KEY([PrincipalId])
REFERENCES [security].[Principals] ([Id])
GO
ALTER TABLE [security].[PrincipalAudit] CHECK CONSTRAINT [FKPrincipalAuditPrincipals]
GO

ALTER TABLE [security].[Resources]  WITH CHECK ADD CONSTRAINT [FKResourcesModules] FOREIGN KEY([ModuleId])
REFERENCES [security].[Modules] ([Id])
GO
ALTER TABLE [security].[Resources] CHECK CONSTRAINT [FKResourcesModules]
GO

ALTER TABLE [security].[RoleTemplatesModulesClaims]  WITH CHECK ADD CONSTRAINT [FKRoleTemplatesModulesRoleTemplates] FOREIGN KEY([RoleTemplateId])
REFERENCES [security].[RoleTemplates] ([Id])
GO
ALTER TABLE [security].[RoleTemplatesModulesClaims] CHECK CONSTRAINT [FKRoleTemplatesModulesRoleTemplates]
GO

ALTER TABLE [security].[RoleTemplatesModulesClaims]  WITH CHECK ADD CONSTRAINT [FKRoleTemplatesModulesModules] FOREIGN KEY([ModuleId])
REFERENCES [security].[Modules] ([Id])
GO
ALTER TABLE [security].[RoleTemplatesModulesClaims] CHECK CONSTRAINT [FKRoleTemplatesModulesModules]
GO

ALTER TABLE [security].[RoleTemplatesModulesClaims]  WITH CHECK ADD CONSTRAINT [FKRoleTemplatesModulesModulesClaims] FOREIGN KEY([ClaimId])
REFERENCES [security].[Claims] ([Id])
GO
ALTER TABLE [security].[RoleTemplatesModulesClaims] CHECK CONSTRAINT [FKRoleTemplatesModulesModulesClaims]
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_PADDING OFF
GO