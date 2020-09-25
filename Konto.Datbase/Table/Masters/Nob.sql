CREATE TABLE [dbo].[Nob]
(
	[Id] [int] identity NOT NULL,
	[BusinessType] [varchar](100) NULL,
	[Extra1] [varchar](max) NULL,
	[Extra2] [varchar](50) NULL,
	CONSTRAINT [PK_Nob] PRIMARY KEY NONCLUSTERED ([Id]), 
)
