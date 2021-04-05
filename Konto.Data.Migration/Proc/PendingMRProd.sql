IF object_id('[dbo].[PendingMRProd]') IS NULL 
EXEC ('CREATE PROC [dbo].[PendingMRProd] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[PendingMRProd]
  @CompanyId int,
	@AccountId INT=0,
	@ProductId INT=0,
	@Status1 varchar(50)=null,
	@Status2 varchar(50)=null,
	@FromDate INT=0,
	@ToDate INT=0,
	@RefId int,
	@TransId INT,
	@itemid int=0


AS
BEGIN


SELECT  po.RefId,po.Id,p.ProdStatus,p.Id as ProdId,(-1 * row_number() OVER(ORDER BY p.Id ASC)) as dId,
	CAST( row_number() OVER(ORDER BY p.Id ASC) AS INT) as SrNo,
	p.ProductId,p.Id ProdId , p.VoucherNo VoucherNo,p.ColorId,p.CompId,
    p.GradeId,p.YearId,
	-1*po.GrayMtr - (ISNULL(x.GrayMtr,0)) AS GrayMtr,
	ISNULL(p.CurrQty,0) CurrQty,
	 0.00 ShMtr ,
	0.00 AS  FinMrt,
	0.00 TP1,0.00 TP2,0.00 TP3,0.00 TP4,0.00 TP5 , x.Qty AS IssQty, (po.Qty* -1) Qty
	FROM dbo.ProdOut po
	LEFT OUTER JOIN dbo.Prod p ON p.Id=po.ProdId
	LEFT OUTER JOIN dbo.Challan c ON c.Id=po.RefId 	
	left outer join dbo.Voucher v on v.Id=po.VoucherId
	LEFT OUTER JOIN (
	SELECT SUM(x.Qty) AS Qty,sum(x.GrayMtr) as GrayMtr, ISNULL(x.ProdId,0) AS ProdId,
	 ct.RefId
	FROM dbo.ProdOut x
	LEFT outer join dbo.Voucher v on v.Id=x.VoucherId
	LEFT OUTER JOIN dbo.Challan ch ON ch.Id = x.RefId
	LEFT OUTER JOIN DBO.ChallanTrans ct on ct.ChallanId = ch.Id
	WHERE 
	x.IsActive=1 and x.IsDeleted=0
  	AND v.VTypeId in( 7,49)
	GROUP BY x.ProdId,ct.RefId,x.VTypeId)x 
	ON x.ProdId = po.ProdId and x.RefId = po.TransId

	
	WHERE (@refid=0 or  po.RefId=@RefId )
	 and(@itemid=0 or po.ProductId=@itemid)

	AND p.IsActive=1 and p.IsDeleted=0 AND po.IsDeleted = 0
	AND c.CompId=@CompanyId	AND(@AccountId=0 or c.AccId=@AccountId)
	AND (@TransId=0 or po.TransId=@TransId)
	AND p.IsOk=1  and abs(po.GrayMtr) -(isnull(x.GrayMtr,0)) > 0


	--SELECT  po.RefId,po.Id,p.ProdStatus,p.Id as ProdId,(-1 * row_number() OVER(ORDER BY p.Id ASC)) as dId,
	--CAST( row_number() OVER(ORDER BY p.Id ASC) AS INT) as SrNo,
	--p.ProductId,p.Id ProdId , p.VoucherNo VoucherNo,p.ColorId,p.CompId,
 --   p.GradeId,p.YearId,
	--CASE   WHEN po.TakaStatus = 'REFISSUE' THEN (po.Qty* -1)  ELSE (x.GrayMtr* -1) END AS GrayMtr,
	--ISNULL(p.CurrQty,0) CurrQty,
	-- 0.00 ShMtr ,
	--0.00 AS  FinMrt,
	--0.00 TP1,0.00 TP2,0.00 TP3,0.00 TP4,0.00 TP5 , x.Qty AS IssQty, (po.Qty* -1) Qty
	--FROM dbo.ProdOut po
	--LEFT OUTER JOIN dbo.Prod p ON p.Id=po.ProdId
	--LEFT OUTER JOIN dbo.Challan c ON c.Id=po.RefId 	
	--left outer join dbo.Voucher v on v.Id=po.VoucherId
	--LEFT OUTER JOIN (
	--SELECT SUM(x.Qty) AS Qty,sum(x.GrayMtr) as GrayMtr, ISNULL(x.ProdId,0) AS ProdId
	--FROM dbo.ProdOut x
	--LEFT outer join dbo.Voucher v on v.Id=x.VoucherId
	--LEFT OUTER JOIN dbo.Challan ch ON ch.Id = x.RefId
	--WHERE -- x.ISOK=1 and 
	--x.IsActive=1 and x.IsDeleted=0
 -- -- AND x.ProdId = 8149
	--AND v.VTypeId NOT IN (43,6)  --AND x.TakaStatus != 'RefIssue'
	--GROUP BY x.ProdId )x ON x.ProdId = po.ProdId
	--WHERE (@refid=0 or  po.RefId=@RefId )
	--AND x.GrayMtr <>0 and(@itemid=0 or po.ProductId=@itemid)
	--AND p.IsActive=1 and p.IsDeleted=0 AND po.IsDeleted = 0
	--AND c.CompId=@CompanyId	AND(@AccountId=0 or c.AccId=@AccountId)
	--AND (@TransId=0 or po.TransId=@TransId)
	--AND p.IsOk=1  
	end
GO

