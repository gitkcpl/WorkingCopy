﻿CREATE TABLE [dbo].[Process]
(
	[Id] INT IDENTITY NOT NULL , 
    [ProcessName] VARCHAR(200) NOT NULL,  
    [HsnCode] VARCHAR(200) NOT NULL,
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
    [Priority] INT NOT NULL DEFAULT 0, 
    [TaxId] INT NULL, 
    CONSTRAINT [PK_Process] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_Process_Id] UNIQUE CLUSTERED ([Id]), 
)
