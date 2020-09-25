CREATE TABLE [dbo].[ReportPara]
(
	[RowId] [uniqueidentifier] NOT NULL DEFAULT NewSequentialid(),
	[ReportId] [int] NULL,
	[ParameterName] [varchar](50) NULL,
	[ParameterValue] [int] NULL,
	[CreateDate] [datetime2](7) NULL,
	[ModifyDate] [datetime2](7) NULL,
	[CreateUser] [varchar](50) NULL,
	[ModifyUser] [varchar](50) NULL,
	[IPAddress] [varchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Id] INT NOT NULL IDENTITY ,
	 CONSTRAINT [PK_ReportPara] PRIMARY KEY NONCLUSTERED ([RowId]), 
    CONSTRAINT [AK_ReportPara] UNIQUE CLUSTERED ([Id]), 
)