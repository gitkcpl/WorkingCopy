CREATE TABLE [dbo].[AccBank]
(
	[Id] INT IDENTITY NOT NULL ,
	[AccId] INT,
	[BankName]   VARCHAR (50)  NULL,
    [BranchName] VARCHAR (50)  NULL,
    [Address]    VARCHAR (100) NULL,
    [IfsCode]   VARCHAR (15)  NULL,
    [AccountNo]  VARCHAR (50)  NULL,
	[RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid(), 
    [IsDeleted] BIT NULL, 
    [ModifyUser] VARCHAR(50) NULL, 
    CONSTRAINT [PK_AccBank] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_AccBank_Id] UNIQUE CLUSTERED ([Id]), 
	CONSTRAINT [FK_AccBank_Acc] FOREIGN KEY ([AccId]) REFERENCES [Acc]([Id]) ON DELETE CASCADE, 
)
