CREATE TABLE [dbo].[SpPara]
(
[Id] INT  NOT NULL IDENTITY, 
    
    [SpName] VARCHAR(200) NULL, 
	[ParaName] VARCHAR(200) NULL, 
	[ParaType] varchar(50) Not Null,
	[DefaultValue] VARCHAR(200) NULL,
    [Extra1] VARCHAR(50) NULL, 
    [Extra2] VARCHAR(50) NULL,
    CONSTRAINT [PK_SpPara] PRIMARY KEY ([Id]) 
	)
