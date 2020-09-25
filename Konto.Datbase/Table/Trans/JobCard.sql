CREATE TABLE [dbo].[JobCard](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VoucherId] [int] NULL,
	[VoucherNo] [varchar](50) NULL,
	[VoucherDate] [int] NULL,
	[DivId] [int] NULL,
	[Type] [varchar](50) NULL,
	[MachineId] [int] NULL,
	[RPUIId] [int] NULL,
	[DyeingType] [varchar](50) NULL,
	[CarrierNo] [varchar](50) NULL,
	[ProgramNo] [varchar](50) NULL,
	[OrderId] [int] NULL,
	[AccountId] [int] NULL,
	[ColorId] [int] NULL,
	[ProductId] [int] NULL,
	[OrderQty] [decimal](18, 4) NULL,
	[GrossWt] [decimal](18, 2) NULL,
	[CarrierWt] [decimal](18, 2) NULL,
	[NoOfCones] [int] NULL,
	[SpringWt] [decimal](18, 2) NULL,
	[SpringId] [int] NULL,
	[GrayItemId] [int] NULL,
	[LotNo] [varchar](50) NULL,
	[GradeId] [int] NULL,
	[BatchId] [int] NULL,
	[Qty] [decimal](18, 2) NULL,
	[ChallanNo] [varchar](50) NULL,
	[Rate] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL,
	[Remark] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetime2](7) NULL,
	[ModifyDate] [datetime2](7) NULL,
	[CreateUser] [varchar](50) NULL,
	[ModifyUser] [varchar](50) NULL,
	[IpAddress] [varchar](100) NULL,
	[RowId] [uniqueidentifier] NOT NULL,
	[VDate] [datetime] NOT NULL,
	[OrdDate] [datetime] NULL,
	[CompanyId] [int] NULL,
	[OrderNo] VARCHAR(200) NULL, 
 [CompId] INT NULL, 
    [YearId] INT NULL, 
    [BranchId] INT NULL, 
    CONSTRAINT [PK_JobCard] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[JobCard] ADD  CONSTRAINT [DF_JobCard_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[JobCard] ADD  CONSTRAINT [DF_JobCard_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO

