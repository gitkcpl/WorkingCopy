﻿CREATE TABLE [dbo].[ErpModule]
(
	[Id] INT NOT NULL, 
    [ParentId] INT NULL, 
    [ModuleDesc] VARCHAR(50) NULL, 
    [OrderIndex] INT NULL, 
    [LinkButton] VARCHAR(50) NULL, 
    [ShortCutKey] VARCHAR(25) NULL, 
    [PackageId] INT NULL, 
    [DefaultReport] VARCHAR(50) NULL, 
    [DefaultLayout] VARCHAR(50) NULL, 
    [TableName] VARCHAR(50) NULL, 
    [AssemblyName] VARCHAR(75) NULL, 
    [MainAssembly] VARCHAR(50) NULL,
	[ListAssembly] VARCHAR(75) NULL,
	[MDI] BIT NULL,
	[Title] VARCHAR(50) NULL,
	[Visible] BIT NOT NULL DEFAULT(1),
	[IconPath] VARCHAR(50) NULL,
	[CheckRight] BIT NULL,
	[VisibleOnDashBoard] BIT NULL,
	[VisibleOnSideBar] BIT NULL,
	[IsSeprator] BIT NOT NULL DEFAULT(0),
	[Offline] bit NOT NULL DEFAULT(1),
	[Extra1] varchar(50) null,
	[Extra2] varchar(50) NULL,
	[IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreateDate] DATETIME2 NULL, 
    [ModifyDate] DATETIME2 NULL, 
    [CreateUser] VARCHAR(50) NULL, 
    [ModifyUser] VARCHAR(50) NULL, 
    [IpAddress] VARCHAR(100) NULL, 
    [RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid(), 
	[ImageIndex] INT NULL, 
    CONSTRAINT [PK_ErpModule] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_ErpModule_Id] UNIQUE CLUSTERED ([Id]), 
)
