CREATE TABLE [dbo].[BOMTrans](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[BaseQty] [decimal](18, 2) NULL,
	[UnitId] [int] NULL,
	[RequireQty] [decimal](18, 2) NULL,
	[Stock] [decimal](18, 2) NULL,
	[Rate] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL,
	[ShortQty] [decimal](18, 2) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetime2](7) NULL,
	[ModifyDate] [datetime2](7) NULL,
	[CreateUser] [varchar](50) NULL,
	[ModifyUser] [varchar](50) NULL,
	[IpAddress] [varchar](100) NULL,
	[RowId] [uniqueidentifier] NOT NULL,
 [BOMId] INT NULL, 
    CONSTRAINT [PK_BOMTrans] PRIMARY KEY CLUSTERED 
(
	[RowId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[BOMTrans] ADD  CONSTRAINT [DF_BOMTrans_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[BOMTrans] ADD  CONSTRAINT [DF_BOMTrans_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO

ALTER TABLE [dbo].[BOMTrans] ADD  CONSTRAINT [DF_BOMTrans_RowId]  DEFAULT (newsequentialid()) FOR [RowId]
GO

ALTER TABLE [dbo].[BOMTrans]  WITH CHECK ADD  CONSTRAINT [FK_BOMTrans_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO

ALTER TABLE [dbo].[BOMTrans] CHECK CONSTRAINT [FK_BOMTrans_Product]
GO


