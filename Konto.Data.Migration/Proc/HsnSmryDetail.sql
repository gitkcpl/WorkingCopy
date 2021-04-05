IF object_id('[dbo].[HsnSmryDetail]') IS NULL 
EXEC ('CREATE PROC [dbo].[HsnSmryDetail] AS SELECT 1 AS Id') 
GO

ALTER  PROCEDURE [dbo].[HsnSmryDetail]
    @CompanyId INT =1,
	@FromDate  INT,
	@ToDate INT, 
	@YearId int,
	@HsnCode varchar(200),
	@UnitId INT,
	@groupid INT=0
	 
AS
BEGIN

SELECT p.id	BillId,ac.GstIn, vc.VoucherName,p.VoucherNo, p.VDate VoucherDate,
ISNULL(p.BillNo,p.VoucherNo) BillNo,ac.AccName Party,pd.ProductCode,pd.ProductName,isnull(pt.HsnCode,pd.HsnCode) HsnCode,

CASE WHEN pt.IgstPer = 0 THEN pt.CgstPer + pt.SgstPer ELSE pt.IgstPer END  AS GSTRate,
ISNULL(pt.Rate,0) Rate,
isnull(pt.Qty,0)  TotalQty,
pt.TdsAmt,
pt.Total GrossAmount,
pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst TaxableValue,
pt.Igst IgstAmt,
pt.Cgst CgstAmt,
pt.Sgst SgstAmt,
pt.CgstPer,
pt.SgstPer,
pt.IgstPer,
pt.NetTotal TotalAmount
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.ID
LEFT OUTER JOIN dbo.Acc ac ON ac.Id = p.AccId
LEFT OUTER JOIN dbo.Product pd ON pd.Id = pt.ProductId
WHERE ((vc.VTypeId = 12) OR (vc.VTypeId=24 and ISNULL(p.Extra1,'X')='SALE' AND isnull(p.BillType,'X')='DEBIT NOTE'))  
 AND pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND
p.CompId = @CompanyId  AND p.VoucherDate BETWEEN @FromDate AND @ToDate 
AND pt.UomId=@UnitId AND ISNULL(pt.HsnCode,pd.HsnCode)=@HsnCode
AND ISNULL(pd.GroupId,0) = @groupid 
UNION ALL

SELECT  p.id	BillId,ac.GstIn,vc.VoucherName,p.VoucherNo, p.VDate VoucherDate,
ISNULL(p.BillNo,p.VoucherNo) BillNo,ac.AccName Party,pd.ProductCode,pd.ProductName,isnull(pt.HsnCode,pd.HsnCode) HsnCode,
CASE WHEN pt.IgstPer = 0 THEN pt.CgstPer + pt.SgstPer ELSE pt.IgstPer END  AS GSTRate,
ISNULL(pt.Rate,0) Rate,
ISNULL(-1*pt.Qty,0)  TotalQty,
-1* (pt.tdsamt) TdsAmt,
-1* (pt.Total) GrossAmount,
-1* (pt.NetTotal- pt.Cgst-pt.Sgst -pt.Igst) TaxableValue,
-1* pt.Igst IgstAmt,
-1* pt.Cgst CgstAmt,
-1* pt.Sgst SgstAmt,
pt.CgstPer,
pt.SgstPer,
pt.IgstPer,
-1* pt.NetTotal TotalAmount
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.Id
LEFT OUTER JOIN dbo.Acc ac ON ac.Id = p.AccId
LEFT OUTER JOIN dbo.Product pd ON pd.Id = pt.ProductId
WHERE  ((vc.VTypeId = 19) OR (vc.VTypeId=24 and ISNULL(p.Extra1,'X')='SALE' AND isnull(p.BillType,'X')='CREDIT NOTE')) 
AND pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND
pt.UomId=@UnitId AND ISNULL(pt.HsnCode,pd.HsnCode)=@HsnCode
AND ISNULL(pd.GroupId,0) = @groupid and
 p.CompId = @CompanyId AND p.VoucherDate BETWEEN @FromDate AND @ToDate


END
GO