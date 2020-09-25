CREATE TABLE [dbo].[State]
(
	[Id] INT NOT NULL identity, 
    [StateName] VARCHAR(50) NULL, 
    [CountryId] INT NOT NULL DEFAULT 1, 
    [GstCode] VARCHAR(5) NULL, 
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
    CONSTRAINT [PK_State] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_State_Id] UNIQUE CLUSTERED ([Id]), 
    CONSTRAINT [FK_State_Country] FOREIGN KEY ([CountryId]) REFERENCES [Country]([Id]) on delete set default
)
