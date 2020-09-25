
CREATE TABLE [dbo].[BtoB](
	[RefCode] [uniqueidentifier] NOT NULL,
	[Amount] [money] NOT NULL,
	[BillClear] [varchar](1) NULL,
	[TransType] [varchar](15) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RowId] [uniqueidentifier] NULL,
	[BillVoucherId] [int] NULL,
	[BillId] [int] NULL,
	[BillTransId] [int] NULL,
	[BillNo] [nvarchar](50) NULL,
	[RefId] [int] NULL,
	[RefTransId] [int] NULL,
	[RefVoucherId] [int] NULL,
	[Adlp1] [numeric](6, 2) NULL,
	[Adla1] [numeric](18, 2) NULL,
	[Adlp2] [numeric](6, 2) NULL,
	[Adla2] [numeric](18, 2) NULL,
	[Adlp3] [numeric](6, 2) NULL,
	[Adla3] [numeric](18, 2) NULL,
	[Adlp4] [numeric](6, 2) NULL,
	[Adla4] [numeric](18, 2) NULL,
	[Adlp5] [numeric](6, 2) NULL,
	[Adla5] [numeric](18, 2) NULL,
	[Adlp6] [numeric](6, 2) NULL,
	[Adla6] [numeric](18, 2) NULL,
	[Adlp7] [numeric](6, 2) NULL,
	[Adla7] [numeric](18, 2) NULL,
	[Adlp8] [numeric](6, 2) NULL,
	[Adla8] [numeric](18, 2) NULL,
	[Adlp9] [numeric](6, 2) NULL,
	[Adla9] [numeric](18, 2) NULL,
	[Adlp10] [numeric](6, 2) NULL,
	[Adla10] [numeric](18, 2) NULL,
	[CreateDate] [datetime] NULL,
	[ModifyDate] [datetime] NULL,
	[IPAddress] [varchar](50) NULL,
	[CreateUser] [nchar](50) NULL,
	[ModifyUser] [nchar](50) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
 CONSTRAINT [PK_BtoB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[BtoB] ADD  CONSTRAINT [DF_BtoB_Amount]  DEFAULT ((0)) FOR [Amount]
GO

ALTER TABLE [dbo].[BtoB] ADD  CONSTRAINT [DF_BtoB_BillClear]  DEFAULT ('N') FOR [BillClear]
GO

