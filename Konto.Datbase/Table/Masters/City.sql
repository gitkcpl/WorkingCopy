CREATE TABLE [dbo].[City]
(
	[Id] INT NOT NULL IDENTITY,
	[CityName] VARCHAR(50) NULL, 
    [StateId] INT NULL DEFAULT 1, 
	[StdCode] varchar(50),
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
    CONSTRAINT [PK_City] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_City_Id] UNIQUE CLUSTERED ([Id]), 
    CONSTRAINT [FK_City_State] FOREIGN KEY ([StateId]) REFERENCES [State]([Id]) on delete set default
)
