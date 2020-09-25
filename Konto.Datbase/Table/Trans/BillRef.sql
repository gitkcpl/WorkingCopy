

CREATE TABLE [dbo].[BillRef](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RowId] [uniqueidentifier] NULL,
	[CompanyId] [int] NULL,
	[YearId] [int] NULL,
	[BillId] [int] NULL,
	[BillVoucherId] [int] NULL,
	[BillNo] [nvarchar](50) NULL,
	[VoucherNo] [nvarchar](50) NULL,
	[VoucherDate] [int] NULL,
	[BillTransId] [int] NULL,
	[GrossAmt] [decimal](18, 2) DEFAULT 0,
	[BillAmt] [decimal](18, 2) DEFAULT 0,
	[TdsAmt] [decimal](18, 2) DEFAULT 0 ,
	[TcsAmt] [decimal](18, 2) DEFAULT 0,
	[RetAmt] [decimal](18, 2) DEFAULT 0 ,
	[AdjustAmt] [decimal](18, 2) DEFAULT 0 ,
	[AccountId] [int] NULL,
	[Remarks] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[ModifyUser] [nvarchar](50) NULL,
	[IPAddress] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifyDate] [datetime] NULL,
	[RefType] [varchar](50) NULL,
 [ItemId] INT NULL, 
    [BranchId] INT NULL DEFAULT 1, 
    [AgentId] INT NULL, 
    [TotalQty] DECIMAL(18, 2) DEFAULT 0, 
    CONSTRAINT [PK_BillRef] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


GO


GO


GO

ALTER TABLE [dbo].[BillRef] ADD  CONSTRAINT [DF_BillRef_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[BillRef] ADD  CONSTRAINT [DF_BillRef_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO

ALTER TABLE [dbo].[BillRef] ADD  CONSTRAINT [DF_BillRef_CreateUser]  DEFAULT ((0)) FOR [CreateUser]
GO

ALTER TABLE [dbo].[BillRef] ADD  CONSTRAINT [DF_BillRef_ModifyUser]  DEFAULT ((0)) FOR [ModifyUser]
GO

ALTER TABLE [dbo].[BillRef] ADD  CONSTRAINT [DF_BillRef_CreateDate]  DEFAULT (NULL) FOR [CreateDate]
GO

ALTER TABLE [dbo].[BillRef] ADD  CONSTRAINT [DF_BillRef_ModifyDate]  DEFAULT (NULL) FOR [ModifyDate]
GO


