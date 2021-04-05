IF object_id('[dbo].[PendingOJCProd]') IS NULL 
EXEC ('CREATE PROC [dbo].[PendingOJCProd] AS SELECT 1 AS Id') 
GO

ALTER  PROCEDURE [dbo].[PendingOJCProd]
 	@CompanyId int,
 	@IsOk INT =1,
	@transid int=0,
	@voucherid int=0,
	@AccountId int = 0
AS
BEGIN 

 SELECT --Iss.IsOk,
 p.Id ProdId,p.RowId,p.TransId,p.SrNo,c.VoucherNo AS InwardNo,ac.AccName AS Weaver,p.ProductId,cl.ColorName YarnName,
        p.GradeId,p.ColorId,p.VoucherId,p.VoucherDate,p.VoucherNo,p.RefId,p.Cops ,p.CopsWt,
	   p.BoxWt,p.CartnWt,p.GrossWt,p.TareWt,

	 --  ISNULL(p.NetWt,0)+ISNULL(iss.IssueQty,0) Qty,
	   ISNULL(p.NetWt,0)-ISNULL(iss.IssueQty,0) GrayMtr,
	   p.DivId,p.CurrQty,p.FinQty,p.IssueRefId,
	   p.IssueRefVoucherId,p.Remark,p.PlyProductId,p.VTypeId
 FROM prod  p
 LEFT OUTER JOIN dbo.Challan c ON c.Id = p.RefId AND c.VoucherId  = p.VoucherId
 LEFT OUTER JOIN dbo.Color cl ON cl.Id = p.ColorId
 LEFT OUTER JOIN dbo.Acc ac ON ac.Id = c.AccId
 LEFT OUTER JOIN voucher v ON v.id = c.VoucherId
 LEFT OUTER JOIN(SELECT pt.ProdId,SUM(pt.GrayMtr) IssueQty-- ,pt.IsOk
				FROM dbo.ProdOut pt
				WHERE pt.IsActive=1 AND pt.IsDeleted=0
				GROUP BY pt.ProdId--,pt.IsOk
				)Iss ON p.Id = iss.ProdId
where p.IsActive=1 AND p.IsDeleted=0
 --and Iss.IsOk=0
 and p.IsClose=0 AND p.IsOk = @IsOk
AND (@transid =0 OR p.TransId= @transid)
AND (@voucherid=0 OR v.vtypeID=@voucherid)
AND p.NetWt- ISNULL(Iss.IssueQty,0) > 0
and p.CompId=@CompanyId 
and c.ChallanType=2 
--and p.Id not in(select pt.ProdId from prodout pt where pt.IsOk=1 AND pt.IsDeleted =0)
END
GO

