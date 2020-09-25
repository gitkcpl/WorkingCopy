IF object_id('[dbo].[GetBoxTakaByBarcode]') IS NULL 
EXEC ('CREATE PROC [dbo].[GetBoxTakaByBarcode] AS SELECT 1 AS Id') 
GO

ALTER PROC [dbo].[GetBoxTakaByBarcode]
@barcodeno VARCHAR(50),
@compid INT
as
BEGIN
SELECT p.Id,p.RowId,p.TransId,p.SrNo,c.VoucherNo AS InwardNo,ac.AccName AS Weaver,p.CProductId ProductId,pd.ProductName YarnName,
        p.GradeId,isnull(p.ColorId,1) ColorId,p.VoucherId,p.VoucherDate,p.VoucherNo,p.RefId,p.Cops ,p.CopsWt, p.Ply,p.Tops,p.CopsRate,
	    p.BoxWt,p.CartnWt,p.GrossWt,p.TareWt,P.NetWt,
	    ISNULL(p.NetWt,0)+ISNULL(iss.IssueQty,0) Qty,
		ISNULL(p.NetWt,0)+ISNULL(iss.IssueQty,0) OrgQty,
	    p.DivId,p.CurrQty,p.FinQty,p.IssueRefId,
	    p.IssueRefVoucherId,p.Remark,p.PlyProductId,p.VTypeId, p.LotNo,p.Extra1,0 ProdOutId,cl.ColorName
 FROM dbo.prod  p
 LEFT OUTER JOIN dbo.Challan c ON c.Id = p.RefId AND c.VoucherId  = p.VoucherId
 LEFT OUTER JOIN dbo.Color cl ON cl.Id = p.ColorId
 LEFT OUTER JOIN dbo.Product pd ON pd.Id = p.CProductId
 LEFT OUTER JOIN dbo.Acc ac ON ac.Id = c.AccId
 LEFT OUTER JOIN dbo.voucher v ON v.id = p.VoucherId
 LEFT OUTER JOIN(SELECT pt.ProdId,SUM(pt.Qty) IssueQty FROM dbo.ProdOut pt
 WHERE pt.IsActive=1 AND pt.IsDeleted=0 
 GROUP BY pt.ProdId)Iss ON p.Id = iss.ProdId
 WHERE
 ISNULL(p.Extra1,'NA')=@barcodeno and
  p.IsActive=1 AND p.IsDeleted=0 and p.IsClose=0 AND p.IsOk = 1 and p.CompId=@compid 
AND p.NetWt+ ISNULL(Iss.IssueQty,0) > 0

END
 GO
