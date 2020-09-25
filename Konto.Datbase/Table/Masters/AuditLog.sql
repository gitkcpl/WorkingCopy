CREATE TABLE [dbo].[AuditLog]
(
	[Id] BIGINT NOT NULL IDENTITY , 
    [EntityName ] VARCHAR(50) NULL, 
    [PropertyName] VARCHAR(50) NULL, 
    [PrimaryKeyValue] INT NULL, 
    [OldValue] VARCHAR(MAX) NULL, 
    [NewValue] VARCHAR(MAX) NULL, 
    [DateChanged] DATETIME2 NULL, 
    [UserName] VARCHAR(50) NULL, 
    [EntryMode] VARCHAR(50) NULL, 
    [MenuId] INT NULL, 
    CONSTRAINT [PK_AuditLog] PRIMARY KEY ([Id])
)
