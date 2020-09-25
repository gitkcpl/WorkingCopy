CREATE TABLE [dbo].[ListPage]
(
	[Id] INT  NOT NULL, 
    [Descr] VARCHAR(200) NULL, 
    [SpName] VARCHAR(50) NULL, 
	[LayoutFile] VARCHAR(200) NULL,
    [VTypeId] INT NULL, 
	[GroupCol] VARCHAR(200),
	[SumCol] VARCHAR(200),
    [Extra1] VARCHAR(50) NULL, 
    [Extra2] VARCHAR(50) NULL, 
    [CommandName] VARCHAR(200) NULL, 
    CONSTRAINT [PK_ListPage] PRIMARY KEY ([Id]) 
)