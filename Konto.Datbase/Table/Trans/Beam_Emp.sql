CREATE TABLE [dbo].[Beam_Emp]
(
	[Id] INT NOT NULL IDENTITY ,  
    [RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid(), 
    [BeamId] INT NOT NULL, 
    [ProdId] INT NOT NULL, 
    [EmpId] int NOT NULL,  
    [Extra1] varchar(50) NULL,  
    [Extra2] varchar(50) NULL,  
    [IsActive] BIT NOT NULL DEFAULT 1, 
	[IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreateDate] DATETIME2 NULL, 
    [ModifyDate] DATETIME2 NULL, 
    [CreateUser] VARCHAR(50) NULL, 
    [ModifyUser] VARCHAR(50) NULL, 
    [IpAddress] VARCHAR(100) NULL, 
      
    CONSTRAINT [PK_Beam_Prod] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_Beam_Prod_Id] UNIQUE CLUSTERED ([Id]) 
)