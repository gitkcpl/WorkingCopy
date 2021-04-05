IF object_id('[dbo].[StockDetails]') IS NULL 
EXEC ('CREATE PROC [dbo].[StockDetails] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[StockDetails]
  	@CompanyId  INT,
	@YearId INT = 0,
	@ItemId INT = 0,
	@FromDate  INT = 20180401,
	@ToDate INT = 20190331, 
	@Branchid INT = 0,
	@DivId INT = 0,
	@item VARCHAR(1)='Y'

AS
IF @item ='Y'
BEGIN
SELECT * FROM  (
SELECT pd.ProductName AS Product,
 'Opening Balance' AS VoucherName,
 'OP' AS VoucherNo,
  ISNULL(CONVERT(DATE, CONVERT(VARCHAR(8), @FromDate), 112), '') TransDate ,
 'OP' AS ChallanNo,
 'OP' AS BillNo,
 '-' AS Particular,
 ISNULL(st.PurPcs,0) + pb.OpNos AS InwPcs,
 ISNULL(st.PurQty,0) + pb.OpQty AS InwQty,
 ISNULL(st.SalePcs,0) AS OutPcs,
 ISNULL(st.SaleQty,0) AS OutQty,
 ISNULL(st.Qty,0) AS Qty,
 ISNULL(st.Pcs,0) AS Pcs,
 NEWID() RefId,
 NEWID() MasterRefId,0 VTypeId
 FROM dbo.Product pd
 LEFT OUTER JOIN (SELECT st.ItemId, SUM(st.IssueNos) AS SalePcs, SUM(st.IssueQty) AS SaleQty, 
 SUM(st.RcptNos) AS PurPcs, SUM(st.RcptQty) AS PurQty, SUM(st.Qty) AS Qty , 
 SUM(st.Pcs) AS Pcs 
FROM dbo.StockTrans st WHERE st.IsDeleted = 0 AND st.IsActive = 1 
AND (@DivId=0 or st.DivId = @DivId) AND (@Branchid=0 or st.BranchId = @Branchid) AND 
st.CompanyId = @CompanyId  AND st.VoucherDate < @FromDate GROUP BY st.ItemId ) st ON  st.ItemId = pd.Id
LEFT OUTER JOIN dbo.ProductBal pb ON pb.ProductId = pd.Id AND pb.CompanyId=@CompanyId AND pb.YearId=@YearId
WHERE pd.IsActive = 1 AND pd.IsDeleted = 0  AND  pd.Id = @ItemId

UNION ALL
SELECT	pd.ProductName AS Product, 
		v.VoucherName, 
		st.VoucherNo, 
		ISNULL(CONVERT(DATE, CONVERT(VARCHAR(8), st.VoucherDate), 112), '') TransDate ,
		ch.ChallanNo, 
		CASE WHEN v.VTypeId=12 THEN st.VoucherNo ELSE st.BillNo END BillNo ,
		ISNULL(ac.AccName,'Production') AS Particular, 
		st.RcptNos AS InwPcs, 
		st.RcptQty AS InwQty, 
		st.IssueNos AS OutPcs,
		st.IssueQty AS OutQty,
		st.Qty,
		ISNULL(st.Pcs,0) AS Pcs,
		st.RefId,st.MasterRefId,v.VTypeId
FROM dbo.StockTrans st
LEFT OUTER JOIN dbo.Product pd ON pd.Id = st.ItemId
LEFT OUTER JOIN dbo.Voucher v ON v.Id = st.VoucherId
LEFT OUTER JOIN dbo.Challan ch ON ch.Id = st.Id
LEFT OUTER JOIN dbo.Acc ac ON ac.Id = st.AccountId
WHERE st.IsActive = 1 AND st.IsDeleted = 0 AND 
(@Branchid=0 or st.BranchId = @Branchid)  AND st.CompanyId = @CompanyId  
AND (@DivId=0 OR st.DivId=@DivId)
AND st.VoucherDate BETWEEN @FromDate AND @ToDate AND  pd.Id = @ItemId  ) x 
ORDER BY x.TransDate

END
ELSE
	BEGIN
		
		SELECT * FROM  (
SELECT pd.ProductName AS Design,
 'Opening Balance' AS VoucherName,
 'OP' AS VoucherNo,
  ISNULL(CONVERT(DATE, CONVERT(VARCHAR(8), @FromDate), 112), '') TransDate ,
 'OP' AS ChallanNo,
 'OP' AS BillNo,
 '-' AS Particular,
 ISNULL(st.PurPcs,0) + pb.OpNos AS InwPcs,
 ISNULL(st.PurQty,0) + pb.OpQty AS InwQty,
 ISNULL(st.SalePcs,0) AS OutPcs,
 ISNULL(st.SaleQty,0) AS OutQty,
 ISNULL(st.Qty,0) AS Qty,
 ISNULL(st.Pcs,0) AS Pcs,
 NEWID() RefId,
 NEWID() MasterRefId,0 VTypeId
 FROM dbo.Product pd
 LEFT OUTER JOIN (SELECT st.DesignId, SUM(st.IssueNos) AS SalePcs, SUM(st.IssueQty) AS SaleQty, 
 SUM(st.RcptNos) AS PurPcs, SUM(st.RcptQty) AS PurQty, SUM(st.Qty) AS Qty , 
 SUM(st.Pcs) AS Pcs 
FROM dbo.StockTrans st WHERE st.IsDeleted = 0 AND st.IsActive = 1 
AND ISNULL(st.DesignId,0)<> 0
AND (@DivId=0 or st.DivId = @DivId) AND (@Branchid=0 or st.BranchId = @Branchid) AND 
st.CompanyId = @CompanyId  AND st.VoucherDate < @FromDate GROUP BY st.DesignId ) st ON  st.DesignId = pd.Id
LEFT OUTER JOIN dbo.ProductBal pb ON pb.ProductId = pd.Id AND pb.CompanyId=@CompanyId AND pb.YearId=@YearId
WHERE pd.IsActive = 1 AND pd.IsDeleted = 0  AND  pd.Id = @ItemId

UNION ALL
SELECT	pd.ProductName AS Design, 
		v.VoucherName, 
		st.VoucherNo, 
		ISNULL(CONVERT(DATE, CONVERT(VARCHAR(8), st.VoucherDate), 112), '') TransDate ,
		ch.ChallanNo, 
		CASE WHEN v.VTypeId=12 THEN st.VoucherNo ELSE st.BillNo END BillNo ,
		ISNULL(ac.AccName,'Production') AS Particular, 
		st.RcptNos AS InwPcs, 
		st.RcptQty AS InwQty, 
		st.IssueNos AS OutPcs,
		st.IssueQty AS OutQty,
		st.Qty,
		ISNULL(st.Pcs,0) AS Pcs,
		st.RefId,st.MasterRefId,v.VTypeId
FROM dbo.StockTrans st
LEFT OUTER JOIN dbo.Product pd ON pd.Id = st.DesignId
LEFT OUTER JOIN dbo.Voucher v ON v.Id = st.VoucherId
LEFT OUTER JOIN dbo.Challan ch ON ch.Id = st.Id
LEFT OUTER JOIN dbo.Acc ac ON ac.Id = st.AccountId
WHERE st.IsActive = 1 AND st.IsDeleted = 0 AND ISNULL(st.DesignId,0)<>0 and
(@Branchid=0 or st.BranchId = @Branchid)  AND st.CompanyId = @CompanyId  
AND (@DivId=0 OR st.DivId=@DivId)
AND st.VoucherDate BETWEEN @FromDate AND @ToDate AND  pd.Id = @ItemId  ) x 
ORDER BY x.TransDate

END


GO

