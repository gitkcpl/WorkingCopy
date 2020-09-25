CREATE TABLE [dbo].[PSize]
(
	[Id] INT IDENTITY NOT NULL,
	[SizeCode] VARCHAR(15) NULL, 
    [SizeName] VARCHAR(50) NULL, 
    [Remark] VARCHAR(MAX) NULL, 
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
    CONSTRAINT [PK_Size] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_Size] UNIQUE CLUSTERED ([Id]), 
)
