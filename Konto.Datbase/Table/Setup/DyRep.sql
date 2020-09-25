CREATE TABLE [dbo].[DyRep]
(
	[Id] INT NOT NULL  IDENTITY, 
    [RepCode] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY NONCLUSTERED, 
    [ReportTypes] VARCHAR(50) NULL, 
    [RepId] INT NULL, 
    [FieldName] VARCHAR(50) NULL, 
    [Show] BIT NOT NULL default(0), 
    [OrderIndex] INT NOT NULL Default(0), 
    [Heading] VARCHAR(50) NULL, 
    [Width] INT NOT NULL DEFAULT 0, 
    [GroupIndex] INT NOT NULL DEFAULT 0, 
    [SortIndex] INT NOT NULL DEFAULT 0, 
    [Summary] BIT NOT NULL DEFAULT 0, 
    [GroupSummary] BIT NOT NULL DEFAULT 0, 
    [SummaryType] VARCHAR(50) NULL, 
    CONSTRAINT [AK_DyRep_Column] UNIQUE CLUSTERED ([Id])
)
