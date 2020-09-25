CREATE TABLE [dbo].[Area]
(
	[Id] INT NOT NULL IDENTITY,
	[AreaName] VARCHAR(50) NULL, 
    [CityId] INT NOT NULL DEFAULT 1, 
    [PinCode] VARCHAR(10) NULL, 
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
    CONSTRAINT [PK_Area] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_Area_Id] UNIQUE CLUSTERED ([Id]), 
    CONSTRAINT [FK_Area_City] FOREIGN KEY ([CityId]) REFERENCES [City]([Id]) on delete set default
)
