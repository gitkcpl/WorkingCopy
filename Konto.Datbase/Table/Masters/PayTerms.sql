CREATE TABLE [dbo].[PayTerms]
(
	[Id] INT IDENTITY NOT NULL , 
    [PayDescr] VARCHAR(200) NOT NULL,
	[Days] INT NOT NULL,
	[Extra1] VARCHAR(100) NULL, 
    [Extra2] VARCHAR(50) NULL,
	[IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreateDate] DATETIME2 NULL, 
    [ModifyDate] DATETIME2 NULL, 
    [CreateUser] VARCHAR(50) NULL, 
    [ModifyUser] VARCHAR(50) NULL, 
    [IpAddress] VARCHAR(100) NULL, 
    [RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid(), 
    CONSTRAINT [PK_PayTerms] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_PayTerms_Id] UNIQUE CLUSTERED ([Id]), 
)
