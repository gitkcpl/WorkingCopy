CREATE TABLE [dbo].[JobCardTrans](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JobCardId] [int] NULL,
	[Description] [nvarchar](50) NULL,
	[ColorPer] [decimal](18, 2) NULL,
	[ConsumeQty] [decimal](18, 2) NULL,
	[Rate] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL,
	[ColorCategory] [varchar](200) NULL,
	[ItemId] [int] NULL,
	[Per] [numeric](6, 2) NULL,
	[ItemType] [varchar](200) NULL,
	[Ply] [int] NULL,
	[LotNo] [varchar](50) NULL,
	[Remark] [varchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetime2](7) NULL,
	[ModifyDate] [datetime2](7) NULL,
	[CreateUser] [varchar](50) NULL,
	[ModifyUser] [varchar](50) NULL,
	[IpAddress] [varchar](100) NULL,
	[RowId] [uniqueidentifier] NOT NULL,
	[RefId] [int] NOT NULL,
 [designId] INT NULL, 
    [DesignName] VARCHAR(MAX) NULL, 
    [Meter] NUMERIC(18, 2) NOT NULL DEFAULT 0, 
    [GMeter] NUMERIC(18, 2) NOT NULL DEFAULT 0, 
    [ProductName] VARCHAR(200) NULL, 
 
    CONSTRAINT [PK_JobCardTrans] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[JobCardTrans] ADD  CONSTRAINT [DF_JobCardTrans_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[JobCardTrans] ADD  CONSTRAINT [DF_JobCardTrans_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO

ALTER TABLE [dbo].[JobCardTrans]  WITH CHECK ADD  CONSTRAINT [FK_JobCardTrans_JobCard] FOREIGN KEY([JobCardId])
REFERENCES [dbo].[JobCard] ([Id])
GO

ALTER TABLE [dbo].[JobCardTrans] CHECK CONSTRAINT [FK_JobCardTrans_JobCard]
GO

