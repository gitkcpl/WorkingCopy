CREATE TABLE [dbo].[Roles]
(
	[Id] INT IDENTITY NOT NULL , 
    [RoleName] VARCHAR(50) NULL, 
    [IsSysAdmin] BIT NOT NULL, 
    [RoleDescription] VARCHAR(MAX) NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreateDate] DATETIME2 NULL, 
    [ModifyDate] DATETIME2 NULL, 
    [CreateUser] VARCHAR(50) NULL, 
    [ModifyUser] VARCHAR(50) NULL, 
    [IpAddress] VARCHAR(100) NULL, 
    [RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid(),
	CONSTRAINT [PK_Roles] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_Roles_Id] UNIQUE CLUSTERED ([Id]), 
)
