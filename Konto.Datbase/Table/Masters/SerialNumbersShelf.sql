CREATE TABLE [dbo].[SerialNumbersShelf]
(
	[Id] INT IDENTITY NOT NULL,
	[VoucherId] INT NOT NULL,
	[Serial_Value] INT NOT NULL,
	[Is_Hold] BIt NOT NULL,
	[YearId] INT NOT  NULL,
	[BranchId] INT NOT NULL,
	[CompanyId] INT NOT NULL, 
    CONSTRAINT [PK_SerialNumbersShelf] PRIMARY KEY ([Id])
)
