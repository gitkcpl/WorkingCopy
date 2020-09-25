
IF object_id('[dbo].[BOMList]') IS NULL 
EXEC ('CREATE PROC [dbo].[BOMList] AS SELECT 1 AS Id') 
GO 

ALTER  PROCEDURE [dbo].[BOMList]	
@compId int,
@branchId int,
@yearId int,
@voucherType int
AS
BEGIN
	select b.Id,b.VoucherNo,b.VoucherId,Cast(b.TargetQty as numeric(18,2)) as Qty, p.ProductName,b.Remark,
	CONVERT(Date,Convert(varchar(8),b.VoucherDate),112)as VoucherDate,b.IsActive,b.Description
	 from BOM b 
	 left outer join Product p on p.Id = b.ProductId
	left outer join Voucher v on v.Id = b.VoucherId
	where b.CompId = @compId and b.YearId = @yearId and b.BranchId = @branchId
	and b.IsActive = 1 and b.IsDeleted = 0 and v.VTypeId = @voucherType
END
GO
