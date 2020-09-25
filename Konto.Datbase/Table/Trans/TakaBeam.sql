CREATE TABLE [dbo].[TakaBeam]
(
	[Id] INT NOT NULL IDENTITY ,  
    [RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid(), 
    [BeamId] INT NULL, 
    [ProdId] INT NULL, 
    [Per] numeric(6,2) NULL, 
    [Qty] numeric(18,2) NULL, 
    [Mtr] numeric(18,2) NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
	[IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreateDate] DATETIME2 NULL, 
    [ModifyDate] DATETIME2 NULL, 
    [CreateUser] VARCHAR(50) NULL, 
    [ModifyUser] VARCHAR(50) NULL, 
    [IpAddress] VARCHAR(100) NULL, 
      
    CONSTRAINT [PK_TakaBeam] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_TakaBeam_Id] UNIQUE CLUSTERED ([Id]) 
)