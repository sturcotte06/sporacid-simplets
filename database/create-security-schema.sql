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

IF (NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'security')) 
BEGIN
    EXEC ('CREATE SCHEMA [security] AUTHORIZATION [dbo]')
END

/****** Object:  Table [security].[Modules]  Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [security].[Modules](
	[Id] [int] IDENTITY(1, 1) NOT NULL,
	[Name] [varchar](20) NOT NULL
PRIMARY KEY CLUSTERED
(
	[Id] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [security].[Claims]  Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [security].[Claims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](20) NOT NULL,
	[Value] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX IXClaimValue ON [security].[Claims] ([Value])
GO

/****** Object:  Table [security].[Contexts]  Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [security].[Contexts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [security].[PrincipalsModulesContextsClaims]  Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [security].[PrincipalsModulesContextsClaims](
	[PrincipalId] [int] NOT NULL,
	[ModuleId] [int] NOT NULL,
	[ContextId] [int] NOT NULL,
	[Claims] [int] NOT NULL,
PRIMARY KEY CLUSTERED
(
	[PrincipalId] ASC,
	[ModuleId] ASC,
	[ContextId] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [security].[Principals] Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [security].[Principals](
	[Id] [int] IDENTITY(1, 1) NOT NULL,
	[Identity] [varchar](25) NOT NULL,
PRIMARY KEY CLUSTERED
(
	[Id] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [security].[PrincipalsAudits]  Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [security].[PrincipalsAudits](
	[Id] [bigint] IDENTITY(1, 1) NOT NULL,
	[PrincipalId] [int] NOT NULL,
	[IpAddress] [varchar](15) NOT NULL,
	[Date] [datetime] NOT NULL
PRIMARY KEY CLUSTERED
(
	[Id] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [security].[RolesTemplates]  Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [security].[RolesTemplates](
	[Id] [int] IDENTITY(1, 1) NOT NULL,
	[Name] [varchar](20) NOT NULL
PRIMARY KEY CLUSTERED
(
	[Id] ASC
)) ON [PRIMARY]
GO

/****** Object:  Table [security].[RolesTemplatesModulesClaims]  Script Date: 12/12/2014 2:40:31 PM ******/
CREATE TABLE [security].[RolesTemplatesModulesClaims](
	[RoleTemplateId] [int] NOT NULL,
	[ModuleId] [int] NOT NULL,
	[Claims] [int] NOT NULL
PRIMARY KEY CLUSTERED
(
	[RoleTemplateId] ASC,
	[ModuleId] ASC
)) ON [PRIMARY]
GO


/****** Foreign keys ******/
ALTER TABLE [security].[PrincipalsModulesContextsClaims]  WITH CHECK ADD CONSTRAINT [FKPrincipalsModulesContextsClaimsPrincipals] FOREIGN KEY([PrincipalId])
REFERENCES [security].[Principals] ([Id])
GO
ALTER TABLE [security].[PrincipalsModulesContextsClaims] CHECK CONSTRAINT [FKPrincipalsModulesContextsClaimsPrincipals]
GO

ALTER TABLE [security].[PrincipalsModulesContextsClaims]  WITH CHECK ADD CONSTRAINT [FKPrincipalsModulesContextsModules] FOREIGN KEY([ModuleId])
REFERENCES [security].[Modules] ([Id])
GO
ALTER TABLE [security].[PrincipalsModulesContextsClaims] CHECK CONSTRAINT [FKPrincipalsModulesContextsModules]
GO

ALTER TABLE [security].[PrincipalsModulesContextsClaims]  WITH CHECK ADD CONSTRAINT [FKPrincipalsModulesContextsClaimsContexts] FOREIGN KEY([ContextId])
REFERENCES [security].[Contexts] ([Id])
GO
ALTER TABLE [security].[PrincipalsModulesContextsClaims] CHECK CONSTRAINT [FKPrincipalsModulesContextsClaimsContexts]
GO

ALTER TABLE [security].[PrincipalsAudits]  WITH CHECK ADD CONSTRAINT [FKPrincipalsAuditsPrincipals] FOREIGN KEY([PrincipalId])
REFERENCES [security].[Principals] ([Id])
GO
ALTER TABLE [security].[PrincipalsAudits] CHECK CONSTRAINT [FKPrincipalsAuditsPrincipals]
GO

ALTER TABLE [security].[RolesTemplatesModulesClaims]  WITH CHECK ADD CONSTRAINT [FKRolesTemplatesModulesRoleTemplates] FOREIGN KEY([RoleTemplateId])
REFERENCES [security].[RolesTemplates] ([Id])
GO
ALTER TABLE [security].[RolesTemplatesModulesClaims] CHECK CONSTRAINT [FKRolesTemplatesModulesRoleTemplates]
GO

ALTER TABLE [security].[RolesTemplatesModulesClaims]  WITH CHECK ADD CONSTRAINT [FKRolesTemplatesModulesModules] FOREIGN KEY([ModuleId])
REFERENCES [security].[Modules] ([Id])
GO
ALTER TABLE [security].[RolesTemplatesModulesClaims] CHECK CONSTRAINT [FKRolesTemplatesModulesModules]
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_PADDING OFF
GO