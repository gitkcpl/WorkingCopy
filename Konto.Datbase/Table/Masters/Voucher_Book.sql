CREATE TABLE [dbo].[Voucher_Book]
(
	[Id] INT NOT NULL IDENTITY , 
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
    CONSTRAINT [PK_Voucher_Book] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_Voucher_Book_Id] UNIQUE CLUSTERED ([Id]), 
    CONSTRAINT [FK_Vocher_book] FOREIGN KEY (VoucherTypeId) REFERENCES [VoucherType]([Id]), 
    CONSTRAINT [FK_Vocher_book_acgroup] FOREIGN KEY ([GroupId]) REFERENCES [AcGroup]([Id]), 
)
