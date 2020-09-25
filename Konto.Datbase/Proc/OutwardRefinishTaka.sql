


CREATE PROCEDURE [dbo].[OutwardRefinishTaka]
 	@CompanyId int,
	@VoucherId int,
	@InwardVId int,
	@ProductId int,
	@FromDate int,
	@ToDate int,
	@IsOk int
AS
BEGIN
  SELECT p.ProdStatus,p.Id,p.RowId,p.TransId,p.SrNo,c.VoucherNo AS InwardNo,ac.AccName AS Weaver,p.ProductId,
        p.GradeId,p.ColorId,p.VoucherId,p.VoucherDate,p.VoucherNo,p.RefId,p.Cops ,p.CopsWt,
	   p.BoxWt,p.CartnWt,p.GrossWt,p.TareWt,p.NetWt,p.DivId,p.CurrQty,p.FinQty,p.IssueRefId,
	   p.IssueRefVoucherId,p.Remark
 FROM dbo.Prod  p
 LEFT OUTER JOIN dbo.ProdOut po ON po.ProdId = p.Id
 LEFT OUTER JOIN dbo.Challan c ON c.Id = p.RefId
 LEFT OUTER JOIN dbo.Acc ac ON ac.Id = c.AccId
 LEFT OUTER JOIN voucher v ON v.id = p.VoucherId
 WHERE ProdStatus='STOCK' AND po.IsOk = 0
 AND p.IsActive=1 AND p.IsDeleted=0 and p.IsClose=0 
AND (v.VTypeId=@VoucherId OR (v.VTypeId=@InwardVId AND p.ProductId=@ProductId))
and p.CompId=@CompanyId and (p.VoucherDate between @FromDate and @ToDate)
END

