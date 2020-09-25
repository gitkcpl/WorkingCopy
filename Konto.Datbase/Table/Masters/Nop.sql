CREATE TABLE [dbo].[Nop]
(
	[Id] INT NOT NULL ,
	[Descr] VARCHAR(300),
	[SecCode] VARCHAR(15), 
    [SecNo] VARCHAR(15) NULL,
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
    CONSTRAINT [PK_Nop] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_Nop_Id] UNIQUE CLUSTERED ([Id]), 
)
