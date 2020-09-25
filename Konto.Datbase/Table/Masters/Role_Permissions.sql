CREATE TABLE [dbo].[Role_Permissions]
(
	[Id] INT IDENTITY NOT NULL, 
    [RoleId] INT NOT NULL, 
    [PermissionId] INT NOT NULL, 
    [RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid(),
	[PermissionType] VARCHAR(50) NULL, 
    CONSTRAINT [PK_Role_Permissions] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_Roles_Permissions] UNIQUE CLUSTERED ([Id]), 
    CONSTRAINT [FK_Role_Permissions_Permissions] FOREIGN KEY ([PermissionId]) REFERENCES [Permissions]([Id]), 
    CONSTRAINT [FK_Role_Permissions_Role] FOREIGN KEY ([RoleId]) REFERENCES [Roles]([Id]) ON DELETE CASCADE, 
)
