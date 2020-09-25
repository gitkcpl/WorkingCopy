CREATE TABLE [dbo].[Catalog]
(
	[Id] INT IDENTITY NOT NULL ,
	[CatalogNo] VARCHAR(50) NULL, 
    [CatalogName] VARCHAR(500) NULL, 
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
    CONSTRAINT [PK_Catalog] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_Catalog] UNIQUE CLUSTERED ([Id]), 
)