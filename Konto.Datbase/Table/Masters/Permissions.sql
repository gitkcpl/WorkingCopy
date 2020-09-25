CREATE TABLE [dbo].[Permissions]
(
	[Id] INT IDENTITY NOT NULL,
	[PermissionDescription] VARCHAR(100), 
    [RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid(),
	[ModuleId] INT NOT NULL, 
    [PermissionType] VARCHAR(50) NULL, 
    [PermissionTypeId] INT NULL, 
    CONSTRAINT [PK_Permissions] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_Permissions_Id] UNIQUE CLUSTERED ([Id]), 
)
