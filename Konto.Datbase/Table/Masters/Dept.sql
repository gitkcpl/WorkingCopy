CREATE TABLE [dbo].[Dept]
(
	[Id] INT IDENTITY NOT NULL , 
    [DeptName] VARCHAR(50) NULL,
	[Remark] VARCHAR(200),
	[Extra1] VARCHAR(50),
	[Extra2] VARCHAR(50),
	[IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreateDate] DATETIME2 NULL, 
    [ModifyDate] DATETIME2 NULL, 
    [CreateUser] VARCHAR(50) NULL, 
    [ModifyUser] VARCHAR(50) NULL, 
    [IpAddress] VARCHAR(100) NULL, 
    [RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid(), 
    CONSTRAINT [PK_Dept] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_Dept_Id] UNIQUE CLUSTERED ([Id]), 
)
