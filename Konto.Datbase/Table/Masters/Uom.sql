CREATE TABLE [dbo].[Uom]
(
	[Id] INT NOT NULL IDENTITY,
	[UnitCode] VARCHAR(15) NULL,
	[UnitName] VARCHAR(50) NULL, 
    [Nod] INT NOT NULL, 
    [Remark] VARCHAR(100) NULL,
	[RateOn] VARCHAR(1) NOT NULL DEFAULT('N'),
	[GSTUnit] varchar(50),
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
    CONSTRAINT [PK_Uom] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_Uom_Id] UNIQUE CLUSTERED ([Id]), 
    
)
