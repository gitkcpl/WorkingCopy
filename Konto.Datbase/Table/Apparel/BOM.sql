CREATE TABLE [dbo].[BOM](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DivisionId] [int] NULL,
	[VoucherId] [int] NULL,
	[VoucherNo] [varchar](50) NULL,
	[VoucherDate] [int] NULL,
	[Description] [varchar](50) NULL,
	[ProductId] [int] NULL,
	[TargetQty] [decimal](18, 2) NULL,
	[Remark] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetime2](7) NULL,
	[ModifyDate] [datetime2](7) NULL,
	[CreateUser] [varchar](50) NULL,
	[ModifyUser] [varchar](50) NULL,
	[IpAddress] [varchar](100) NULL,
	[RowId] [uniqueidentifier] NOT NULL,
	[CompId] [int] NULL,
	[YearId] [int] NULL,
	[BranchId] [int] NULL,
 CONSTRAINT [PK_BOM] PRIMARY KEY CLUSTERED 
(
	[RowId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[BOM] ADD  CONSTRAINT [DF_BOM_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[BOM] ADD  CONSTRAINT [DF_BOM_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO

ALTER TABLE [dbo].[BOM] ADD  CONSTRAINT [DF_BOM_RowId]  DEFAULT (newsequentialid()) FOR [RowId]
GO

ALTER TABLE [dbo].[BOM]  WITH CHECK ADD  CONSTRAINT [FK_BOM_Division] FOREIGN KEY([DivisionId])
REFERENCES [dbo].[Division] ([Id])
GO

ALTER TABLE [dbo].[BOM] CHECK CONSTRAINT [FK_BOM_Division]
GO

ALTER TABLE [dbo].[BOM]  WITH CHECK ADD  CONSTRAINT [FK_BOM_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO

ALTER TABLE [dbo].[BOM] CHECK CONSTRAINT [FK_BOM_Product]
GO

ALTER TABLE [dbo].[BOM]  WITH CHECK ADD  CONSTRAINT [FK_BOM_Voucher] FOREIGN KEY([VoucherId])
REFERENCES [dbo].[Voucher] ([Id])
GO

ALTER TABLE [dbo].[BOM] CHECK CONSTRAINT [FK_BOM_Voucher]
GO

