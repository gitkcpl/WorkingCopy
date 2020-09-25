CREATE TABLE [dbo].[CompPara]
(
	[Id] INT IDENTITY , 
    [CompId] INT NULL, 
    [ParaId] INT NULL, 
    [ParaValue] VARCHAR(200) NULL, 
    [Remark] VARCHAR(50) NULL,
	[RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid(), 
    CONSTRAINT [PK_CompPara] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_CompPara] UNIQUE CLUSTERED ([Id]), 
    CONSTRAINT [FK_CompPara_Company] FOREIGN KEY ([CompId]) REFERENCES [Company]([Id]), 
    CONSTRAINT [FK_CompPara_SysPara] FOREIGN KEY ([ParaId]) REFERENCES [SysPara]([Id]), 
)
