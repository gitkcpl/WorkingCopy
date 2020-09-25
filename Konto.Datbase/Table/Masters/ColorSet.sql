CREATE TABLE [dbo].[ColorSet]
(
	[Id] INT IDENTITY NOT NULL ,
	[ItemId] INT NOT NULL, 
    [ColorId] INT NULL, 
	[Rate] DECIMAL(18, 2) NOT NULL DEFAULT 0,
	[MinQty] DECIMAL(18, 2) NOT NULL DEFAULT 0,
    [Remark] VARCHAR(MAX) NULL, 
    [Extra1] VARCHAR(50) NULL, 
    [Extra2] VARCHAR(50) NULL,
	[IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreateDate] DATETIME2 NULL, 
    [ModifyDate] DATETIME2 NULL, 
    [CreateUser] VARCHAR(50) NULL, 
    [ModifyUser] VARCHAR(50) NULL, 
    [IpAddress] VARCHAR(100) NULL, 
    [RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid(), 

)
