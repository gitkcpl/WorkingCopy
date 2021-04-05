IF object_id('[dbo].[HsnSummary]') IS NULL 
EXEC ('CREATE PROC [dbo].[HsnSummary] AS SELECT 1 AS Id') 
GO

ALTER  PROCEDURE [dbo].[HsnSummary]
    @CompanyId INT =1,
	@FromDate  INT,
	@ToDate INT 
AS
BEGIN

SELECT ISNULL(x.HsnCode,'NA') HsnCode,x.Description,ISNULL(x.UQC,'NA') UQC,isnull( x.UomId,0)UomId, 
SUM(x.TotalQty) AS TotalQty,
SUM(x.BillAmt) AS BillAmount,
SUM(x.TaxableValue) AS TaxableValue,
SUM(x.IgstAmt) AS IgstAmt,
SUM(x.CgstAmt) AS CgstAmt,
SUM(x.SgstAmt) AS SgstAmt,
SUM(x.Cess) AS Cess,GroupId
 FROM (
SELECT ISNULL(p.HsnCode,bt.HsnCode) HsnCode, ISNULL(pg.GroupName,'NA') AS [Description], u.GSTUnit AS UQC,  bt.UomId,
isnull(sum(case when u.RateOn='Q' or vc.VTypeId=24 then ISNULL(bt.Qty,0) else isnull(bt.Pcs,0) end),0) AS TotalQty,
SUM(bt.NetTotal) AS BillAmt,
SUM(bt.NetTotal) - SUM(bt.Sgst) - SUM(bt.Cgst) - SUM(bt.Igst) AS TaxableValue, 
SUM(bt.Igst) AS IgstAmt,
SUM(bt.Cgst) AS CgstAmt,
SUM(bt.Sgst) AS SgstAmt,
SUM(bt.Cess) AS Cess,ISNULL(p.GroupId,'0')GroupId
FROM dbo.BillMain bm
LEFT OUTER JOIN dbo.BillTrans bt ON bt.BillId = bm.Id 
LEFT OUTER JOIN dbo.Product p ON p.Id = bt.ProductId
LEFT OUTER JOIN dbo.Uom u ON u.Id = bt.UomId
LEFT OUTER JOIN dbo.PGroup pg ON pg.Id  = p.GroupId
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = bm.VoucherId
WHERE bm.CompId = @CompanyId AND ((vc.VTypeId = 12) OR(vc.VTypeId=24 AND ISNULL(bm.Extra1,'X')='SALE' AND ISNULL(bm.BillType,'X')='DEBIT NOTE'))
 AND bm.IsActive =1 AND bm.IsDeleted = 0 AND bt.IsDeleted=0 AND (bm.VoucherDate between @FromDate and @ToDate)
GROUP BY  p.HsnCode,bt.HsnCode, pg.GroupName, u.GSTUnit, bt.UomId,p.GroupId
UNION ALL
SELECT ISNULL(p.HsnCode,bt.HsnCode), ISNULL(pg.GroupName,'NA') AS PDescription,u.GSTUnit AS UQC,  bt.UomId,
-1* isnull(sum(case when u.RateOn='Q' or vc.VTypeId=24 then ISNULL(bt.Qty,0) else isnull(bt.Pcs,0) end),0) AS TotalQty,
-1*SUM(bt.NetTotal) AS BillAmt,
-1*(SUM(bt.NetTotal -bt.Sgst-bt.Cgst- bt.Igst)) AS TaxableValue, 
-1*SUM(bt.Igst) AS IgstAmt,
-1*SUM(bt.Cgst) AS CgstAmt,
-1*SUM(bt.Sgst) AS SgstAmt,
-1*SUM(bt.Cess) AS Cess,ISNULL(p.GroupId,'0')GroupId
FROM dbo.BillMain bm
LEFT OUTER JOIN dbo.BillTrans bt ON bt.BillId = bm.Id 
LEFT OUTER JOIN dbo.Product p ON p.Id = bt.ProductId
LEFT OUTER JOIN dbo.Uom u ON u.Id = bt.UomId
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = bm.VoucherId
LEFT OUTER JOIN dbo.PGroup pg ON pg.Id  = p.GroupId
WHERE bm.CompId = @CompanyId and ((vc.VTypeId = 19) OR (vc.VTypeId=24 AND ISNULL(bm.Extra1,'X')='SALE' AND ISNULL(bm.BillType,'X')='CREDIT NOTE')) and
bm.IsActive = 1 AND bm.IsDeleted = 0  AND bt.IsDeleted=0 AND (bm.VoucherDate between @FromDate and @ToDate)
GROUP BY  p.HsnCode,bt.HsnCode,pg.GroupName, u.GSTUnit, bt.UomId,p.GroupId
) x 
GROUP BY x.HsnCode, x.Description, x.UQC, x.UomId,x.GroupId

END
GO

