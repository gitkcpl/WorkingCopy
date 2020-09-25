CREATE TABLE [dbo].[PImage]
(
	[Id] INT NOT NULL IDENTITY,
	[ProductId] INT NOT NULL DEFAULT 1,
	[Category] varchar(1) NULL ,
    [ImagePath] VARCHAR(500) NULL,
    [Extra1] VARCHAR(100) NULL,
    [Extra2] VARCHAR(50) NULL,
	[IsActive] BIT NOT NULL DEFAULT 1,
    [IsDeleted] BIT NOT NULL DEFAULT 0,
    [CreateDate] DATETIME2 NULL,
    [ModifyDate] DATETIME2 NULL,
    [CreateUser] VARCHAR(50) NULL,
    [ModifyUser] VARCHAR(50) NULL,
    [IpAddress] VARCHAR(100) NULL,
    [RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid(),
    CONSTRAINT [PK_PImage] PRIMARY KEY NONCLUSTERED ([RowId]),
    CONSTRAINT [AK_PImage_Id] UNIQUE CLUSTERED ([Id])
)