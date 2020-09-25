﻿CREATE TABLE [dbo].[Route]
(
	[Id] INT IDENTITY NOT NULL ,
	[RouteCode] VARCHAR(15),
	[RouteName] VARCHAR(50),
	[CityId] INT,
	[AreaId] INT,
	[IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreateDate] DATETIME2 NULL, 
    [ModifyDate] DATETIME2 NULL, 
    [CreateUser] VARCHAR(50) NULL, 
    [ModifyUser] VARCHAR(50) NULL, 
    [IpAddress] VARCHAR(100) NULL, 
    [RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid(), 
    CONSTRAINT [PK_Route] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_Route_Id] UNIQUE CLUSTERED ([Id]), 
    CONSTRAINT [FK_Route_Area] FOREIGN KEY ([AreaId]) REFERENCES [Area]([Id]), 
	CONSTRAINT [FK_Route_City] FOREIGN KEY ([CityId]) REFERENCES [City]([Id]),
)
