CREATE TABLE [dbo].[SysPara]
(
	[Id] INT NOT NULL , 
    [Descr] VARCHAR(200) NULL,
	[DefaultValue] Varchar(200),
    [ValueDescr] VARCHAR(200) NULL, 
    [Category] VARCHAR(50) NULL,
	[RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid(), 
    [FreezePass] VARCHAR(200) NULL, 
    CONSTRAINT [PK_SysPara] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_SysPara] UNIQUE CLUSTERED ([Id]), 
)
