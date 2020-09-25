CREATE TABLE [dbo].[MachineMaster]
(
	[Id] INT NOT NULL IDENTITY,
	[MachineName] varchar(25) NOT NULL,
	[remark] varchar(max) NULL,
	[CompanyID] int NULL,
	[DivId] int NULL,
	[IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreateDate] DATETIME2 NULL, 
    [ModifyDate] DATETIME2 NULL, 
    [CreateUser] VARCHAR(50) NULL, 
    [ModifyUser] VARCHAR(50) NULL, 
    [IpAddress] VARCHAR(100) NULL, 
    [RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid(), 
    CONSTRAINT [PK_machine] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_Machine_Id] UNIQUE CLUSTERED ([Id])
)