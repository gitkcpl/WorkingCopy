CREATE TABLE [dbo].[Voucher_Party]
(
	[Id] INT IDENTITY NOT NULL , 
    [VoucherTypeId] INT NULL, 
    [GroupId] INT NULL, 
    [Remark] VARCHAR(50) NULL,
	[IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreateDate] DATETIME2 NULL, 
    [ModifyDate] DATETIME2 NULL, 
    [CreateUser] VARCHAR(50) NULL, 
    [ModifyUser] VARCHAR(50) NULL, 
    [IpAddress] VARCHAR(100) NULL, 
    [RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid(), 
    CONSTRAINT [PK_Voucher_Party] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_Vouucher_Party_Id] UNIQUE CLUSTERED ([Id]), 
    CONSTRAINT [FK_Voucher_Party] FOREIGN KEY (VoucherTypeId) REFERENCES [VoucherType]([Id]), 
    CONSTRAINT [FK_Voucher_Party_acgroup] FOREIGN KEY ([GroupId]) REFERENCES [AcGroup]([Id]), 
)
