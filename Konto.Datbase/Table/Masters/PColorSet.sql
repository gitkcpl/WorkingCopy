CREATE TABLE [dbo].[PColorSet]
(
	[Id] INT NOT NULL , 
    [ProductId] INT NOT NULL, 
    [ColorId] INT NOT NULL, 
    [Rate] NUMERIC(18, 3) NOT NULL, 
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
    CONSTRAINT [PK_PColorSet] PRIMARY KEY NONCLUSTERED ([RowId]),
    CONSTRAINT [AK_PColorSet_Id] UNIQUE CLUSTERED ([Id]), 
    CONSTRAINT [FK_PColorSet_Product] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id]),
	CONSTRAINT [FK_PColorSet_Color] FOREIGN KEY ([ColorId]) REFERENCES [Color]([Id])
)
