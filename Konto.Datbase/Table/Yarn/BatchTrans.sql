CREATE TABLE [dbo].[BatchTrans]
(
	[Id] INT Identity NOT NULL, 
    [RowId] UNIQUEIDENTIFIER NOT NULL, 
    [BatchId] INT NULL, 
    [ItemId] INT NULL, 
    [Per] numeric(6,2) NULL, 
     [ItemType] VARCHAR(50) NULL, 
    [Ply] INT NULL, 
    [LotNo] varchar(50) NULL,  
    [Remark] VARCHAR(MAX) NULL,
	[IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreateDate] DATETIME2 NULL, 
    [ModifyDate] DATETIME2 NULL, 
    [CreateUser] VARCHAR(50) NULL, 
    [ModifyUser] VARCHAR(50) NULL, 
    [IpAddress] VARCHAR(100) NULL, 
    [RefId] INT NULL, 
    [RefTransId] INT NULL, 
    CONSTRAINT [PK_BatchTrans] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_BatchTrans_Id] UNIQUE CLUSTERED ([Id]),
	CONSTRAINT [FK_Batch_trans] FOREIGN KEY ([BatchId]) REFERENCES [Batch]([Id])
)