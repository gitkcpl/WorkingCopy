CREATE TABLE [dbo].[Voucher_Item]
(
	[Id] INT IDENTITY NOT NULL ,
	[VoucherTypeId] INT NULL, 
    [PTypeId] INT NULL, 
    [Remark] VARCHAR(50) NULL,
	[IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreateDate] DATETIME2 NULL, 
    [ModifyDate] DATETIME2 NULL, 
    [CreateUser] VARCHAR(50) NULL, 
    [ModifyUser] VARCHAR(50) NULL, 
    [IpAddress] VARCHAR(100) NULL, 
    [RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid(), 
    CONSTRAINT [PK_Voucher_Item] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_Voucher_Item_Id] UNIQUE CLUSTERED ([Id]), 
    CONSTRAINT [FK_Vocher_Item] FOREIGN KEY (VoucherTypeId) REFERENCES [VoucherType]([Id]), 
    CONSTRAINT [FK_Vocher_Item_ProductType] FOREIGN KEY ([PTypeId]) REFERENCES [ProductType]([Id]), 
)
