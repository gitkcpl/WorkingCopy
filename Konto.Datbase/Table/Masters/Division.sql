CREATE TABLE [dbo].[Division]
(
	[Id] INT IDENTITY NOT NULL , 
    [DivisionName] VARCHAR(50) NULL, 
	[BranchId]	INT NULL,
    [Remark] VARCHAR(200) NULL,
	[IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreateDate] DATETIME2 NULL, 
    [ModifyDate] DATETIME2 NULL, 
    [CreateUser] VARCHAR(50) NULL, 
    [ModifyUser] VARCHAR(50) NULL, 
    [IpAddress] VARCHAR(100) NULL, 
    [RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid(), 
    CONSTRAINT [PK_division] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_division_Id] UNIQUE CLUSTERED ([Id]), 
    CONSTRAINT [FK_Division_Branch] FOREIGN KEY ([BranchId]) REFERENCES [Branch]([Id]), 
)
