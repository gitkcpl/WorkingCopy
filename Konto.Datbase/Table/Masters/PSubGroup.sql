CREATE TABLE [dbo].[PSubGroup]
(
	[Id] INT IDENTITY NOT NULL,
	[SubCode] VARCHAR(15) NULL, 
    [SubName] VARCHAR(50) NULL, 
	[PGroupId] INT NOT NULL,
    [Extra1] VARCHAR(50) NULL, 
    [Extra2] VARCHAR(50) NULL,
	[IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreateDate] DATETIME2 NULL, 
    [ModifyDate] DATETIME2 NULL, 
    [CreateUser] VARCHAR(50) NULL, 
    [ModifyUser] VARCHAR(50) NULL, 
    [IpAddress] VARCHAR(100) NULL, 
    [RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid(), 
    CONSTRAINT [PK_PSubGroup] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_PSubGroup] UNIQUE CLUSTERED ([Id]), 
    CONSTRAINT [FK_PSubGroup_PGroup] FOREIGN KEY ([PGroupId]) REFERENCES [PGroup]([Id]) 
)
