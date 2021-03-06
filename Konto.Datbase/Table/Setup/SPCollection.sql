﻿CREATE TABLE [dbo].[SPCollection]
(
	[Id] INT IDENTITY , 
    [Name] VARCHAR(200) NOT NULL, 
    [Section] VARCHAR(200) NOT NULL, 
    [Remark] VARCHAR(500) NULL,
	[RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid(), 
    CONSTRAINT [PK_SPCollection] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_SPCollection] UNIQUE CLUSTERED ([Id]), 
 
)
