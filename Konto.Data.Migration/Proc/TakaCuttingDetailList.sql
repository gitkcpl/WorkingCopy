IF object_id('[dbo].[TakaCuttingDetailList]') IS NULL 
EXEC ('CREATE PROC [dbo].[TakaCuttingDetailList] AS SELECT 1 AS Id') 
GO


ALTER PROCEDURE [dbo].[TakaCuttingDetailList]
 @VTypeId int,
 @CompanyId int,
 @YearId int,
 @FromDate int,
 @ToDate int,
 @BranchId int
AS
BEGIN
	SELECT  pout.Id, c.ColorName ColorName, pout.Remark PoNo,
	pro.ProductName ProductName, pd.NetWt Qty, pd.VoucherDate VDate,
	pout.VoucherNo VoucherNo,pd.VoucherNo as TakaNo,
	trans.VoucherNo as CutTakaNo, trans.NetWt as CutQty,
	trans.TwistType as CutBoxNo,
	tc.ColorName as CutColorName
	from ProdOut pout
    left outer join  Prod pd on pout.ProdId =pd.Id 
    left outer join Voucher v  on pout.VoucherId =v.Id
    left outer join Product pro on pd.ProductId =pro.Id
    left outer join Color c on pd.ColorId =c.Id
	left outer join prod trans on trans.IssueRefId=pd.Id and pd.IssueRefVoucherId =pd.VoucherId
	left outer join Color tc on tc.Id=trans.ColorId
    where v.VTypeId = @VTypeId
    and pd.IsActive=1 and pd.IsDeleted=0
	and pout.IsActive=1 and pout.IsDeleted=0
	and pd.VoucherDate between @FromDate and @ToDate
	and pout.CompId=@CompanyId
	and pd.BranchId=@BranchId
    order by pout.Id desc	
END

Go


