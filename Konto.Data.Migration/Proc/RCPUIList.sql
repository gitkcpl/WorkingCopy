IF object_id('[dbo].[RCPUIList]') IS NULL 
EXEC ('CREATE PROC [dbo].[RCPUIList] AS SELECT 1 AS Id') 
GO


ALTER PROCEDURE [dbo].[RCPUIList]	
@compId int,
@branchId int,
@yearId int,
@voucherType int
AS
BEGIN
	select r.Id,r.VoucherNo,r.VoucherId,Cast(r.Qty as numeric(18,2)) as Qty, p.ProductName,c.ColorName,r.Remark,
	CONVERT(Date,Convert(varchar(8),r.VoucherDate),112) as VoucherDate,r.IsActive
	,r.Description
	 from RCPUI r left outer join Product p on p.Id = r.ProductId
	left outer join Color c on c.Id = r.ColorId 
	left outer join Voucher v on v.Id = r.VoucherId
	where r.CompId = @compId and r.YearId = @yearId and r.BranchId = @branchId
	and r.IsActive = 1 and r.IsDeleted = 0 and v.VTypeId = @voucherType
END
GO



