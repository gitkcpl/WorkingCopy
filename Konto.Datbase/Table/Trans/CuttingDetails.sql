CREATE TABLE [dbo].[CuttingDetails]
(
	[Id] INT NOT NULL IDENTITY , 
    [RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid(), 
    [DesignId] INT NULL, 
	[CompId] INT NULL, 
    [YearId] INT NULL, 
    [VoucherId] INT NULL, 
    [TransId] INT NULL, 
	[Pcs] NUMERIC(18, 4) NULL DEFAULT 0, 
  	[Cut] NUMERIC(6, 2) NULL DEFAULT 0, 
	[Mtr] NUMERIC(18, 4) NULL DEFAULT 0, 
	[Remark] NVARCHAR(MAX) NULL, 
	[IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreateDate] DATETIME2 NULL, 
    [ModifyDate] DATETIME2 NULL, 
    [CreateUser] VARCHAR(50) NULL, 
    [ModifyUser] VARCHAR(50) NULL, 
    [IpAddress] VARCHAR(100) NULL, 
    CONSTRAINT [PK_CuttingDetails] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_CuttingDetails_Id] UNIQUE CLUSTERED ([Id]) 
)