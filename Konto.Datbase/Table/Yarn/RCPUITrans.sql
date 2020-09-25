CREATE TABLE [dbo].[RCPUITrans]
(
	[Id] INT Identity NOT NULL, 
	[RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid(), 
    [ProductId] INT NULL, 
    [ColorPer] NUMERIC(18, 4) NULL, 
    [ColorKgs] NUMERIC(18, 2) NULL, 
    [RCPUIId] INT NULL,
		  [Remark] NVARCHAR(MAX) NULL, 
	[IsActive] BIT NOT NULL DEFAULT 1, 
	[IsClose] BIT NOT NULL DEFAULT 0, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreateDate] DATETIME2 NULL, 
    [ModifyDate] DATETIME2 NULL, 
    [CreateUser] VARCHAR(50) NULL, 
    [ModifyUser] VARCHAR(50) NULL, 
    [IpAddress] VARCHAR(100) NULL, 
	[ColorCategory] Varchar(50) null
	CONSTRAINT [PK_ColorRTranId] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_ColorTran_Id] UNIQUE CLUSTERED ([Id])
)
