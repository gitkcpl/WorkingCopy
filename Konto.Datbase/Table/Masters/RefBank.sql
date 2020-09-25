CREATE TABLE [dbo].[RefBank]
(
	[Id] INT IDENTITY NOT NULL , 
    [BankName] VARCHAR(50) NULL, 
    [Address] VARCHAR(200) NULL,
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
    CONSTRAINT [PK_RefBank] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_RefBank_Id] UNIQUE CLUSTERED ([Id]), 
)
