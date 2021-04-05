IF object_id('[dbo].[TakaCuttingList]') IS NULL 
EXEC ('CREATE PROC [dbo].[TakaCuttingList] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[TakaCuttingList]
 @VTypeId int,
 @CompanyId int,
 @YearId int,
 @FromDate int,
 @ToDate int,
 @Deleted INT = 0,
 @BranchId int
AS
BEGIN
	SELECT  pout.Id, c.ColorName ColorName, pout.Remark PoNo,
	pro.ProductName ProductName, pd.NetWt Qty,  
	CONVERT(date,CONVERT(varchar(8),pd.voucherdate),112) VoucherDate, 
	pout.VoucherNo VoucherNo,pd.VoucherNo TakaNo,pout.VoucherId
	from ProdOut pout
    left outer join  Prod pd on pout.ProdId =pd.Id 
    left outer join Voucher v  on pout.VoucherId =v.Id
    left outer join Product pro on pd.ProductId =pro.Id
    left outer join Color c on pd.ColorId =c.Id
    where v.VTypeId = @VTypeId
	and pd.BranchId=@BranchId
	
    and pd.IsActive=1 and pd.IsDeleted=0
	and pout.IsActive=1 and pout.IsDeleted=@Deleted
    order by pout.Id desc
	
END
Go


