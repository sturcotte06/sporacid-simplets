INSERT INTO [security].[Claims] VALUES ('Create');
INSERT INTO [security].[Claims] VALUES ('CreateAll');
INSERT INTO [security].[Claims] VALUES ('Update');
INSERT INTO [security].[Claims] VALUES ('UpdateAll');
INSERT INTO [security].[Claims] VALUES ('Delete');
INSERT INTO [security].[Claims] VALUES ('DeleteAll');
INSERT INTO [security].[Claims] VALUES ('Read');
INSERT INTO [security].[Claims] VALUES ('ReadAll');
INSERT INTO [security].[Claims] VALUES ('Admin');

INSERT INTO [security].[Modules] VALUES ('Profil');
INSERT INTO [security].[Modules] VALUES ('Club');
INSERT INTO [security].[Modules] VALUES ('Membre');
INSERT INTO [security].[Modules] VALUES ('Commandite');
INSERT INTO [security].[Modules] VALUES ('Fournisseur');
INSERT INTO [security].[Modules] VALUES ('Inventaire');
INSERT INTO [security].[Modules] VALUES ('Securite');
INSERT INTO [security].[Modules] VALUES ('Enumeration');


INSERT INTO [security].[RoleTemplates] VALUES ('Administrateur');
INSERT INTO [security].[RoleTemplates] VALUES ('SuperUsager');
INSERT INTO [security].[RoleTemplates] VALUES ('Usager');

DECLARE @moduleId int
SET @moduleId = 0

-- Administrateur template
WHILE @moduleId < 8
BEGIN 
	INSERT INTO [security].[RoleTemplatesModulesClaims] VALUES (1, @moduleId, 1)
	INSERT INTO [security].[RoleTemplatesModulesClaims] VALUES (1, @moduleId, 2)
	INSERT INTO [security].[RoleTemplatesModulesClaims] VALUES (1, @moduleId, 3)
	INSERT INTO [security].[RoleTemplatesModulesClaims] VALUES (1, @moduleId, 4)
	INSERT INTO [security].[RoleTemplatesModulesClaims] VALUES (1, @moduleId, 5)
	INSERT INTO [security].[RoleTemplatesModulesClaims] VALUES (1, @moduleId, 6)
	INSERT INTO [security].[RoleTemplatesModulesClaims] VALUES (1, @moduleId, 7)
	INSERT INTO [security].[RoleTemplatesModulesClaims] VALUES (1, @moduleId, 8)
	INSERT INTO [security].[RoleTemplatesModulesClaims] VALUES (1, @moduleId, 9)

	SET @moduleId = @moduleId + 1
END

-- Superuser template
SET @moduleId = 0
WHILE @moduleId < 8
BEGIN 
	INSERT INTO [security].[RoleTemplatesModulesClaims] VALUES (2, @moduleId, 1)
	INSERT INTO [security].[RoleTemplatesModulesClaims] VALUES (2, @moduleId, 2)
	INSERT INTO [security].[RoleTemplatesModulesClaims] VALUES (2, @moduleId, 3)
	INSERT INTO [security].[RoleTemplatesModulesClaims] VALUES (2, @moduleId, 4)
	INSERT INTO [security].[RoleTemplatesModulesClaims] VALUES (2, @moduleId, 5)
	INSERT INTO [security].[RoleTemplatesModulesClaims] VALUES (2, @moduleId, 7)
	INSERT INTO [security].[RoleTemplatesModulesClaims] VALUES (2, @moduleId, 8)

	SET @moduleId = @moduleId + 1
END

-- User template
SET @moduleId = 0
WHILE @moduleId < 8
BEGIN 
	INSERT INTO [security].[RoleTemplatesModulesClaims] VALUES (3, @moduleId, 1)
	INSERT INTO [security].[RoleTemplatesModulesClaims] VALUES (3, @moduleId, 3)
	INSERT INTO [security].[RoleTemplatesModulesClaims] VALUES (3, @moduleId, 7)

	SET @moduleId = @moduleId + 1
END