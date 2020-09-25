CREATE TABLE [dbo].[PFormula]
(
 	[Id] INT NOT NULL IDENTITY,
	[ProductId] int NOT NULL DEFAULT 1, 
    [DescType] INT NULL,
    [Qty] numeric(18,2) NULL, 
	[cut] numeric(18,2) NULL, 
	[ColorId] INT NULL,
	[Rate] numeric(18,2) NULL, 
	[Total] numeric(18,2) NULL, 
	[Remark] varchar(500) NULL, 
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
    CONSTRAINT [PK_PFormula] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_PFormula_Id] UNIQUE CLUSTERED ([Id])
)