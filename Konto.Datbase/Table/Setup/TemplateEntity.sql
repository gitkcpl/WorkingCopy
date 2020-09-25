CREATE TABLE [dbo].[TemplateEntity]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [EntityName] VARCHAR(50) NULL, 
    [IsActive] BIT NOT NULL DEFAULT 0 
)
