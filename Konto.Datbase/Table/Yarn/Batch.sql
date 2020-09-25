﻿CREATE TABLE [dbo].[Batch]
(
	[Id] INT Identity NOT NULL, 
    [RowId] UNIQUEIDENTIFIER NOT NULL, 
    [CompId] INT NULL, 
    [DivId] INT NULL,
	[VoucherId] INT NULL, 
    [VoucherNo] VARCHAR(25) NULL, 
    [VoucherDate] INT NULL,  
    [ItemId] INT NULL, 
    [YearId] INT NULL, 
    [BranchId] INT NULL, 
    [ShadeId] INT NULL, 
	[Cross_Section] VARCHAR(50) NULL,
    [Remark] VARCHAR(MAX) NULL,
	[IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreateDate] DATETIME2 NULL, 
    [ModifyDate] DATETIME2 NULL, 
    [CreateUser] VARCHAR(50) NULL, 
    [ModifyUser] VARCHAR(50) NULL, 
    [IpAddress] VARCHAR(100) NULL,  
    CONSTRAINT [PK_Batch] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_Batch_Id] UNIQUE CLUSTERED ([Id]) 
)