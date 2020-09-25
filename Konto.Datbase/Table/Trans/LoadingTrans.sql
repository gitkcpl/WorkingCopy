CREATE TABLE [dbo].[LoadingTrans]
(
	[Id] INT NOT NULL IDENTITY ,  
    [RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid(), 
    [ProdId] INT NOT NULL,
	[LoadingDate] datetime2 null,
	[DivId] int null,
	[MacId] int null,
	[BeamPotion] varchar(50) null,
	[ProdStatus] varchar(50) null,
	[ProductId] int null,
    [Extra1] varchar(50) NULL,  
    [Extra2] varchar(50) NULL,  
    [IsActive] BIT NOT NULL DEFAULT 1, 
	[IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreateDate] DATETIME2 NULL, 
    [ModifyDate] DATETIME2 NULL, 
    [CreateUser] VARCHAR(50) NULL, 
    [ModifyUser] VARCHAR(50) NULL, 
    [IpAddress] VARCHAR(100) NULL,

       CONSTRAINT [PK_Loading_Prod] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_Loading_Prod_Id] UNIQUE CLUSTERED ([Id]) 
)
