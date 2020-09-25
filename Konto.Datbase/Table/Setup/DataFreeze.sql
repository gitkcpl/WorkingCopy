CREATE TABLE [dbo].[data_freeze](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FromDate] [int] NULL,
	[ToDate] [int] NULL,
	[VoucherTypeID] [bigint] NULL,
	[Freeze] [bit] NULL,
	[Pass] [varchar](10) NULL,
	[CompanyID] [int] NULL,
	[ModifyDate] [datetime2](7) NULL,
	[ModifyUser] [varchar](50) NULL,

PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


