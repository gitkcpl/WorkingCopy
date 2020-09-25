CREATE TABLE [dbo].[Prod_Weft]
(
	[Id] INT NOT NULL IDENTITY,
  [ProdId] INT NULL, 
   [ProductId] INT NULL, 
   [Denier] numeric(18,2) NULL, 
   [PI] numeric(18,2) NULL, 
   [Qty] numeric(18,2) NULL,
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
    [WeftId] INT NULL, 
    CONSTRAINT [PK_Prod_weft_Emp] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_Prod_Emp_weft_Id] UNIQUE CLUSTERED ([Id])
)