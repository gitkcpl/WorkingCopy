﻿CREATE TABLE [dbo].[AccBal]
(
	[Id] INT IDENTITY NOT NULL, 
    [AccId] INT NULL, 
    [GroupId] INT NULL, 
    [AddressId] INT NULL, 
    [Bal] NUMERIC(18, 2) NULL, 
    [OpBal] NUMERIC(18, 2) NULL, 
    [OpDebit] NUMERIC(18, 2) NULL, 
    [OpCredit] NUMERIC(18, 2) NULL, 
    [CompId] INT NULL, 
    [YearId] INT NULL,
	[Share] NUMERIC(8,2),
	[Address1] VARCHAR(200), 
    [Address2] VARCHAR(200) NULL, 
    [CityId] INT NULL DEFAULT 1, 
    [AreaId] INT NULL DEFAULT 1, 
    [PinCode] VARCHAR(15) NULL, 
    [ContactPerson] VARCHAR(75) NULL, 
    [MobileNo] VARCHAR(15) NULL, 
    [Phone] VARCHAR(50) NULL, 
    [Email] VARCHAR(50) NULL, 
    [Website] VARCHAR(50) NULL, 
    [RouteId] INT NULL DEFAULT 1, 
	[Others] VARCHAR(100) NULL, 
	[RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid(), 
    [AccRowId] UNIQUEIDENTIFIER NULL, 
	[ModifyUser] VARCHAR(50) NULL, 
	[Audit] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_AccBal] PRIMARY KEY NONCLUSTERED ([RowId]), 
	CONSTRAINT [AK_AccBal_Id] UNIQUE CLUSTERED ([Id]), 
    CONSTRAINT [FK_AccBal_Company] FOREIGN KEY ([CompId]) REFERENCES [Company]([Id]), 
    CONSTRAINT [FK_AccBal_FinYear] FOREIGN KEY ([YearId]) REFERENCES [FinYear]([Id]), 
    CONSTRAINT [FK_AccBal_Acc] FOREIGN KEY ([AccId]) REFERENCES [Acc]([Id]), 
	CONSTRAINT [FK_AccBal_Route] FOREIGN KEY ([RouteId]) REFERENCES [Route]([Id]) on delete set default,
	CONSTRAINT [FK_AccBal_City] FOREIGN KEY ([CityId]) REFERENCES [City]([Id]) on delete set default,
	CONSTRAINT [FK_AccBal_Area] FOREIGN KEY ([AreaId]) REFERENCES [Area]([Id]) on delete set default, 
    CONSTRAINT [FK_AccBal_AcGroup] FOREIGN KEY ([GroupId]) REFERENCES [AcGroup]([Id]),
)

GO

CREATE INDEX [IX_AccBal_AccId] ON [dbo].[AccBal] ([AccId])
