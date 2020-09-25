CREATE TABLE [dbo].[RecpaySetting](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecPay] VARCHAR(50) NOT NULL,
	[Field] VARCHAR(10) NOT NULL ,
	[PlusMinus] VARCHAR(10) NOT NULL,
	[PerCap] [varchar](10) NULL,
	[AmtCap] [varchar](10) NOT NULL,
	[AccountId] [int] NOT NULL DEFAULT 0,
	[RowId] [uniqueidentifier] NOT NULL,
	[CalcOn] [varchar](200) NULL,
	[CreateDate] [datetime2](7) NULL,
	[CreateUser] [varchar](50) NULL,
	[ModifyUser] [varchar](50) NULL,
	[ModifyDate] [datetime2](7) NULL,
	[IpAddress] [varchar](100) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
    [YearId] INT NOT NULL DEFAULT 0, 
    [Drcr] VARCHAR NULL, 
    [TaxId] INT NULL, 
    [HsnCode] VARCHAR(50) NULL, 
    [Remark] VARCHAR(200) NULL, 
    [VoucherId] INT NULL, 
    CONSTRAINT [PK_RecpaySetting] PRIMARY KEY NONCLUSTERED 
(
	[RowId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_RecpaySetting] UNIQUE CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[RecpaySetting] ADD  CONSTRAINT [DF__RecpaySet__RowId__414EAC47]  DEFAULT (newsequentialid()) FOR [RowId]
GO

