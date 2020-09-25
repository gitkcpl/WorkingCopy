CREATE TABLE [dbo].[dbVersion]
(
	[Id] INT NOT NULL  IDENTITY, 
    [CreateDate] DATETIME2 NULL, 
    [UpgradeDate] DATETIME2 NULL, 
    [Version] VARCHAR(20) NULL, 
    [Remark] VARCHAR(300) NULL, 
    CONSTRAINT [PK_dbVersion] PRIMARY KEY ([Id])
)
