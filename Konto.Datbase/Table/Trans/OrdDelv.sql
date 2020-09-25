CREATE TABLE [dbo].[OrdDelv]
(
[Id] INT IDENTITY NOT NULL , 
	[OrdId] [int] NULL,
	[AccId] [int] NULL,
	[OrdDelvDate] [datetime2](7) NULL,
	[Qty] [numeric](18, 2) NULL,
	[Remark] [varchar](max) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreateDate] [datetime2](7) NULL,
	[ModifyDate] [datetime2](7) NULL,
	[CreateUser] [varchar](50) NULL,
	[ModifyUser] [varchar](50) NULL,
	[IpAddress] [varchar](100) NULL,
  [RowId] UNIQUEIDENTIFIER NOT NULL DEFAULT NewSequentialid(), 
    CONSTRAINT [PK_OrdDelv] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_OrdDelv_Id] UNIQUE CLUSTERED ([Id]), 
)
