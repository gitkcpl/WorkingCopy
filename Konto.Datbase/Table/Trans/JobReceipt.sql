CREATE TABLE [dbo].[JobReceipt]
(
	[Id] INT NOT NULL IDENTITY , 
    [RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid(), 
    [ChallanId] INT NULL, 
    [RefId] INT NULL, 
    [RefTransId] INT NULL, 
    [VoucherId] INT NULL, 
	 [ProductId] INT NULL, 
	  [ColorId] INT NULL, 
    [PendingQty] numeric(18,2) NULL, 
    [PendingPcs] numeric(18,2) NULL, 
	[IssueQty] numeric(18,2) NULL, 
    [IssuePcs] numeric(18,2) NULL, 
	[Qty] numeric(18,2) NULL, 
    [Pcs] numeric(18,2) NULL, 
    [Remark] varchar(max) NULL, 
	[IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreateDate] DATETIME2 NULL, 
    [ModifyDate] DATETIME2 NULL, 
    [CreateUser] VARCHAR(50) NULL, 
    [ModifyUser] VARCHAR(50) NULL, 
    [IpAddress] VARCHAR(100) NULL, 


    [IsClear] BIT NULL Default 0, 
    CONSTRAINT [PK_JR] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_JR_Id] UNIQUE CLUSTERED ([Id]),
	 CONSTRAINT [FK_JobR_Challan] FOREIGN KEY ([ChallanId]) REFERENCES [Challan]([Id])  
	 )