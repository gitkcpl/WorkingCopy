CREATE TABLE [dbo].[ReportType]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RowId] [uniqueidentifier] NOT NULL DEFAULT NewSequentialid(),
	[ReportName] [varchar](100) NULL,
	[ReportTypes] [varchar](500) NULL,
	[VoucherTypeId] [int] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[Remarks] [varchar](500) NULL,
	[FileName] [varchar](500) NULL,
	[CreateDate] [datetime2](7) NULL,
	[ModifyDate] [datetime2](7) NULL,
	[CreateUser] [varchar](50) NULL,
	[ModifyUser] [varchar](50) NULL,
	[IPAddress] [varchar](50) NULL,
	[SpName] VARCHAR(500) NULL, 
    CONSTRAINT [PK_ReportType] PRIMARY KEY CLUSTERED ([Id])
)