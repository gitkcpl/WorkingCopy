CREATE TABLE [dbo].[UserMaster]
(
	[Id] INT IDENTITY NOT NULL, 
    [UserName] VARCHAR(50) NULL, 
    [UserPass] VARCHAR(50) NULL, 
    [CreateDate] DATETIME2 NULL, 
    [ModifyDate] DATETIME2 NULL, 
    [LastLoginDate] DATETIME2 NULL, 
    [RoleId] INT NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [IsDeleted] BIT NOT NULL default 0, 
    [EmpId] INT NULL, 
	[IpAddress] varchar(200) null,
	 [CreateUser] VARCHAR(150) NULL, 
	  [ModifyUser] VARCHAR(150) NULL, 
    [RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid()
	CONSTRAINT [PK_UserMaster] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_UserMaster_Id] UNIQUE CLUSTERED ([Id]), 
    CONSTRAINT [FK_UserMaster_Roles] FOREIGN KEY ([RoleId]) REFERENCES [Roles]([Id])
)
