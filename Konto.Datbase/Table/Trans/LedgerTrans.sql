CREATE TABLE [dbo].[LedgerTrans](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RefId] [uniqueidentifier] NOT NULL,
	[TransType] [int] NULL,
	[CompanyId] [int] NOT NULL,
	[YearId] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
	[VoucherId] [int] NOT NULL,
	[VoucherDate] [int] NOT NULL,
	[TransDate] [datetime2](7) NOT NULL,
	[BillNo] [varchar](30) NULL,
	[VoucherNo] [varchar](30) NULL,
	[AccountId] [int] NOT NULL,
	[RefAccountId] [int] NULL,
	[Debit] [numeric](18, 2) NOT NULL,
	[Credit] [numeric](18, 2) NOT NULL,
	[BilllAmount] [numeric](18, 2) NOT NULL,
	[ChqDate] [datetime2](7) NULL,
	[ChqNo] [varchar](50) NULL,
	[AgentId] [int] NULL,
	[TableName] [varchar](50) NULL,
	[KeyFldValue] [int] NULL,
	[Narration] [varchar](MAX) NULL,
	[Remark] [varchar](2000) NULL,
	[LrDate] [datetime2](7) NULL,
	[LrNo] [varchar](50) NULL,
	[ReconDate] [int] NULL,
	[Audit] [bit] NULL,
	[AdjustAmount] [numeric](18, 2) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NULL,
	[CreateUser] [nvarchar](50) NULL,
	[ModifyUser] [nvarchar](50) NULL,
	[IPAddress] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifyDate] [datetime] NULL,
	[RowId] [uniqueidentifier] NULL,
	[Amount] [money] NOT NULL,
	[TransCode] [uniqueidentifier] NULL,
	[RetAmount] [numeric](18, 2) NULL,
	[TdsAmount] [numeric](18, 2) NULL,
	[OpBill] [varchar](1) NULL,
 CONSTRAINT [PK_LedgerTrans] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[LedgerTrans] ADD  CONSTRAINT [DF_LedgerTrans_TransType]  DEFAULT ((0)) FOR [TransType]
GO

ALTER TABLE [dbo].[LedgerTrans] ADD  CONSTRAINT [DF_LedgerTrans_CompanyId]  DEFAULT ((0)) FOR [CompanyId]
GO

ALTER TABLE [dbo].[LedgerTrans] ADD  CONSTRAINT [DF_LedgerTrans_YearId]  DEFAULT ((0)) FOR [YearId]
GO

ALTER TABLE [dbo].[LedgerTrans] ADD  CONSTRAINT [DF_LedgerTrans_BranchId]  DEFAULT ((0)) FOR [BranchId]
GO

ALTER TABLE [dbo].[LedgerTrans] ADD  CONSTRAINT [DF_LedgerTrans_VoucherId]  DEFAULT ((0)) FOR [VoucherId]
GO

ALTER TABLE [dbo].[LedgerTrans] ADD  CONSTRAINT [DF_LedgerTrans_AccountId]  DEFAULT ((0)) FOR [AccountId]
GO

ALTER TABLE [dbo].[LedgerTrans] ADD  CONSTRAINT [DF_LedgerTrans_Debit]  DEFAULT ((0)) FOR [Debit]
GO

ALTER TABLE [dbo].[LedgerTrans] ADD  CONSTRAINT [DF_LedgerTrans_Credit]  DEFAULT ((0)) FOR [Credit]
GO

ALTER TABLE [dbo].[LedgerTrans] ADD  CONSTRAINT [DF_LedgerTrans_BilllAmount]  DEFAULT ((0)) FOR [BilllAmount]
GO

ALTER TABLE [dbo].[LedgerTrans] ADD  CONSTRAINT [DF_LedgerTrans_Audit]  DEFAULT ((0)) FOR [Audit]
GO

ALTER TABLE [dbo].[LedgerTrans] ADD  CONSTRAINT [DF_LedgerTrans_AdjustAmount]  DEFAULT ((0)) FOR [AdjustAmount]
GO

ALTER TABLE [dbo].[LedgerTrans] ADD  CONSTRAINT [DF_LedgerTrans_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO

ALTER TABLE [dbo].[LedgerTrans] ADD  CONSTRAINT [DF_LedgerTrans_IsDeleted]  DEFAULT ('N') FOR [IsDeleted]
GO

ALTER TABLE [dbo].[LedgerTrans] ADD  CONSTRAINT [DF_LedgerTrans_CreateUser]  DEFAULT ((0)) FOR [CreateUser]
GO

ALTER TABLE [dbo].[LedgerTrans] ADD  CONSTRAINT [DF_LedgerTrans_ModifyUser]  DEFAULT ((0)) FOR [ModifyUser]
GO

ALTER TABLE [dbo].[LedgerTrans] ADD  CONSTRAINT [DF_LedgerTrans_CreateDate]  DEFAULT (NULL) FOR [CreateDate]
GO

ALTER TABLE [dbo].[LedgerTrans] ADD  CONSTRAINT [DF_LedgerTrans_ModifyDate]  DEFAULT (NULL) FOR [ModifyDate]
GO

ALTER TABLE [dbo].[LedgerTrans] ADD  CONSTRAINT [DF_LedgerTrans_Amount_1]  DEFAULT ((0)) FOR [Amount]
GO

ALTER TABLE [dbo].[LedgerTrans] ADD  CONSTRAINT [DF_LedgerTrans_RetAmount_1]  DEFAULT ((0)) FOR [RetAmount]
GO

ALTER TABLE [dbo].[LedgerTrans] ADD  CONSTRAINT [DF_LedgerTrans_TdsAmount_1]  DEFAULT ((0)) FOR [TdsAmount]
GO

ALTER TABLE [dbo].[LedgerTrans] ADD  CONSTRAINT [DF_LedgerTrans_OpBill]  DEFAULT ('N') FOR [OpBill]
GO

