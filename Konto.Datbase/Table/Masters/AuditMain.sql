CREATE TABLE [dbo].[AuditMain]
(
	[Id] BIGINT NOT NULL IDENTITY , 
    [ModuleName ] VARCHAR(50) NULL, 
    [EntryMode] VARCHAR(50) NULL, 
    [PrimaryKeyValue] INT NULL, 
	[RefId] UNIQUEIDENTIFIER NULL , 
    [OldValue] VARCHAR(MAX) NULL, 
    [NewValue] VARCHAR(MAX) NULL, 
    [DateChanged] DATETIME2 NULL, 
    [UserName] VARCHAR(50) NULL, 
    CONSTRAINT [PK_AuditMain] PRIMARY KEY ([Id])
)
