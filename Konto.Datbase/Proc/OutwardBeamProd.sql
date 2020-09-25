CREATE  PROCEDURE [dbo].[OutwardBeamProd]
 	@CompanyId int,
	--@VoucherId int,
--	@InwardVId int,
	@ProductId INT=0,
	@IsOk INT,
	@transid int=0,
	@voucherid int=0,
	@ptypeid INT = 0,
	@vtype VARCHAR(50) ='Stock'
AS
BEGIN

IF @vtype ='Stock'
BEGIN
 SELECT p.Id,p.RowId,p.TransId,p.SrNo,c.VoucherNo AS InwardNo,ac.AccName AS Weaver,p.ProductId,p.ProductName YarnName,
        p.GradeId,p.ColorId,p.VoucherId,p.VoucherDate,p.VoucherNo,p.RefId,p.Cops ,p.CopsWt, p.Ply,p.Tops,p.CopsRate,
	    p.BoxWt,p.CartnWt,p.GrossWt,p.TareWt,P.NetWt,
	    ISNULL(p.NetWt,0)+ISNULL(iss.IssueQty,0) Qty,
	    p.DivId,p.CurrQty,p.FinQty,p.IssueRefId,
	    p.IssueRefVoucherId,p.Remark,p.PlyProductId,p.VTypeId, p.LotNo
 FROM dbo.prod  p
 LEFT OUTER JOIN dbo.Challan c ON c.Id = p.RefId AND c.VoucherId  = p.VoucherId
 LEFT OUTER JOIN dbo.Color cl ON cl.Id = p.ColorId
 LEFT OUTER JOIN dbo.Product pd ON pd.Id = p.ProductId
 LEFT OUTER JOIN dbo.Acc ac ON ac.Id = c.AccId
 LEFT OUTER JOIN dbo.voucher v ON v.id = p.VoucherId
 LEFT OUTER JOIN(SELECT pt.ProdId,SUM(pt.Qty) IssueQty FROM dbo.ProdOut pt
 WHERE pt.IsActive=1 AND pt.IsDeleted=0 
 GROUP BY pt.ProdId)Iss ON p.Id = iss.ProdId
 WHERE p.IsActive=1 AND p.IsDeleted=0 and p.IsClose=0 AND p.IsOk = @IsOk
AND (@ProductId =0 or p.ProductId=@ProductId)
AND (@transid =0 OR p.TransId= @transid)
AND (@voucherid=0 OR p.VoucherId=@voucherid)
AND (@ptypeid=0 OR pd.PTypeId=@ptypeid)
AND p.NetWt+ ISNULL(Iss.IssueQty,0) > 0
and p.CompId=@CompanyId 
order by p.Id

END

ELSE IF (@vtype ='Refinish')
BEGIN

SELECT p.Id,p.RowId,p.TransId,p.SrNo,c.VoucherNo AS InwardNo,ac.AccName AS Weaver,p.ProductId,p.ProductName YarnName,
        p.GradeId,p.ColorId,p.VoucherId,p.VoucherDate,p.VoucherNo,p.RefId,p.Cops ,p.CopsWt, p.Ply,p.Tops,p.CopsRate,
	    p.BoxWt,p.CartnWt,p.GrossWt,p.TareWt,P.NetWt,
	    ISNULL(po.Qty,0) Qty,
	    p.DivId,p.CurrQty,p.FinQty,p.IssueRefId,
	    p.IssueRefVoucherId,p.Remark,p.PlyProductId,p.VTypeId, p.LotNo
 FROM dbo.prod  p
 LEFT OUTER JOIN dbo.ProdOut po ON po.ProdId = p.Id
  LEFT OUTER JOIN ( SELECT pt.ProdId,pt.Qty FROM dbo.ProdOut pt 
 WHERE pt.TakaStatus = 'RefIssue' ) x ON x.ProdId = p.Id
 LEFT OUTER JOIN dbo.Challan c ON c.Id = p.RefId AND c.VoucherId  = p.VoucherId
 LEFT OUTER JOIN dbo.Color cl ON cl.Id = p.ColorId
 LEFT OUTER JOIN dbo.Product pd ON pd.Id = p.ProductId
 LEFT OUTER JOIN dbo.Acc ac ON ac.Id = c.AccId
 LEFT OUTER JOIN dbo.voucher v ON v.id = p.VoucherId
 WHERE p.IsActive=1 AND p.IsDeleted=0 and p.IsClose=0 AND po.IsOk = 0
AND (@ProductId =0 or p.ProductId=@ProductId) 
AND (@transid =0 OR p.TransId= @transid)
AND (@voucherid=0 OR p.VoucherId=@voucherid)
AND (@ptypeid=0 OR pd.PTypeId=@ptypeid)
AND po.Qty + ISNULL(x.Qty,0) <> 0
and p.CompId=@CompanyId 
order by p.Id
END

ELSE IF (@vtype ='MillIssue')
BEGIN

 SELECT p.Id,p.RowId,p.TransId,p.SrNo,c.VoucherNo AS InwardNo,ac.AccName AS Weaver,p.ProductId,p.ProductName YarnName,
        p.GradeId,p.ColorId,p.VoucherId,p.VoucherDate,p.VoucherNo,p.RefId,p.Cops ,p.CopsWt, p.Ply,p.Tops,p.CopsRate,
	    p.BoxWt,p.CartnWt,p.GrossWt,p.TareWt,P.NetWt,
	    ISNULL(p.NetWt,0)+ISNULL(iss.IssueQty,0) Qty,
	    p.DivId,p.CurrQty,p.FinQty,p.IssueRefId,
	    p.IssueRefVoucherId,p.Remark,p.PlyProductId,p.VTypeId, p.LotNo
 FROM dbo.prod  p
 LEFT OUTER JOIN dbo.Challan c ON c.Id = p.RefId AND c.VoucherId  = p.VoucherId
 LEFT OUTER JOIN dbo.Color cl ON cl.Id = p.ColorId
 LEFT OUTER JOIN dbo.Product pd ON pd.Id = p.ProductId
 LEFT OUTER JOIN dbo.Acc ac ON ac.Id = c.AccId
 LEFT OUTER JOIN dbo.voucher v ON v.id = p.VoucherId
 LEFT OUTER JOIN(SELECT pt.ProdId,SUM(pt.Qty) IssueQty FROM dbo.ProdOut pt
 WHERE pt.IsActive=1 AND pt.IsDeleted=0 
 GROUP BY pt.ProdId)Iss ON p.Id = iss.ProdId
 WHERE p.IsActive=1 AND p.IsDeleted=0 and p.IsClose=0 AND p.IsOk = @IsOk
AND (@ProductId =0 or p.ProductId=@ProductId)
AND (@transid =0 OR p.TransId= @transid)
AND (@voucherid=0 OR p.VoucherId=@voucherid)
AND (@ptypeid=0 OR pd.PTypeId=@ptypeid)
AND p.NetWt+ ISNULL(Iss.IssueQty,0) > 0
and p.CompId=@CompanyId AND p.ProdStatus = 'Stock'
order by p.Id

END

ELSE 
BEGIN
 SELECT p.Id,p.RowId,p.TransId,p.SrNo,c.VoucherNo AS InwardNo,ac.AccName AS Weaver,p.ProductId,p.ProductName YarnName,
        p.GradeId,p.ColorId,p.VoucherId,p.VoucherDate,p.VoucherNo,p.RefId,p.Cops ,p.CopsWt, p.Ply,p.Tops, p.CopsRate,
	    p.BoxWt,p.CartnWt,p.GrossWt,p.TareWt,P.NetWt,
	    p.NetWt Qty,
	    p.DivId,p.CurrQty,p.FinQty,p.IssueRefId,
	    p.IssueRefVoucherId,p.Remark,p.PlyProductId,p.VTypeId, p.LotNo
 FROM dbo.prod  p
 LEFT OUTER JOIN dbo.Challan c ON c.Id = p.RefId AND c.VoucherId  = p.VoucherId
 LEFT OUTER JOIN dbo.Color cl ON cl.Id = p.ColorId
 LEFT OUTER JOIN dbo.Product pd ON pd.Id = p.ProductId
 LEFT OUTER JOIN dbo.Acc ac ON ac.Id = c.AccId
 LEFT OUTER JOIN dbo.voucher v ON v.id = p.VoucherId
 LEFT OUTER JOIN(SELECT pt.ProdId,SUM(pt.Qty) IssueQty FROM dbo.ProdOut pt
 WHERE pt.IsActive=1 AND pt.IsDeleted=0
 GROUP BY pt.ProdId)Iss ON p.Id = iss.ProdId
 WHERE p.IsActive=1 AND p.IsDeleted=0 and p.IsClose=0 AND p.IsOk = @IsOk
AND (@ProductId =0 or p.ProductId=@ProductId)
AND (@transid =0 OR p.TransId= @transid)
AND (@voucherid=0 OR p.VoucherId=@voucherid)
AND (@ptypeid=0 OR pd.PTypeId=@ptypeid)
AND p.NetWt+ ISNULL(Iss.IssueQty,0) = 0
and p.CompId=@CompanyId 
order by p.Id

END;
END
GO

