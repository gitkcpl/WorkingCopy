﻿CREATE TABLE [dbo].[Company]
(
	[Id] INT IDENTITY NOT NULL , 
    [CompName] VARCHAR(75) NULL, 
    [PrintName] VARCHAR(75) NULL, 
    [SortName] VARCHAR(15) NULL, 
    [Address1] VARCHAR(100) NULL, 
    [Address2] VARCHAR(100) NULL, 
    [CityId] INT NULL, 
    [Pincode] VARCHAR(15) NULL, 
    [StateId] INT NULL, 
    [FAddress1] VARCHAR(100) NULL, 
    [FAddress2] VARCHAR(100) NULL, 
    [FCityId] INT NULL, 
    [FPincode] VARCHAR(15) NULL, 
    [FStateId] INT NULL, 
    [Mobile] VARCHAR(10) NULL, 
    [Phone] VARCHAR(50) NULL, 
    [Email] VARCHAR(50) NULL, 
    [Website] VARCHAR(50) NULL, 
    [Para] VARCHAR(25) NULL, 
    [GstIn] VARCHAR(20) NULL, 
    [PanNo] VARCHAR(20) NULL, 
    [AadharNo] VARCHAR(20) NULL, 
    [TdsAcNo] VARCHAR(50) NULL,
	[Remark] VARCHAR(MAX) NULL,
	[AcNo] VARCHAR(25) NULL,
	[BankName] VARCHAR(50),
	[HolyWorld] VARCHAR(500),
	[IfsCode] VARCHAR(50),
	[Insurance] VARCHAR(100),
	[SendFrom] VARCHAR(50),
	[SendPass] VARCHAR(50),
	[LogoPath] VARCHAR(MAX) NULL, 
	[Extra1] VARCHAR(MAX) NULL, 
    [Extra2] VARCHAR(50) NULL,
	[IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreateDate] DATETIME2 NULL, 
    [ModifyDate] DATETIME2 NULL, 
    [CreateUser] VARCHAR(50) NULL, 
    [ModifyUser] VARCHAR(50) NULL, 
    [IpAddress] VARCHAR(100) NULL, 
    [RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid(), 
    [NobId] INT NULL, 
    [EmailPass] VARCHAR(50) NULL, 
    [PromotionalAPI] VARCHAR(5000) NULL, 
    [TransactionalAPI] VARCHAR(500) NULL, 
    CONSTRAINT [PK_Comp] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_Comp_Id] UNIQUE CLUSTERED ([Id]), 
)