CREATE TABLE [dbo].[Grade]
(
	[Id] INT IDENTITY NOT NULL , 
    [GradeName] VARCHAR(50) NULL, 
    [Remark] VARCHAR(50) NULL,
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
    [RefGradeId] INT NULL, 
	
    [StartWt] DECIMAL(18, 3) NOT NULL DEFAULT 0, 
    [EndWt] DECIMAL(18, 3) NOT NULL DEFAULT 0, 
    [RateDiff] DECIMAL(18, 2) NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_Grade] PRIMARY KEY NONCLUSTERED ([RowId]), 
	CONSTRAINT [AK_Grade_Id] UNIQUE CLUSTERED ([Id]), 
)
