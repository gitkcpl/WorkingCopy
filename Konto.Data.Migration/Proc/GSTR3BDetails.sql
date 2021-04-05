

IF object_id('[dbo].[GSTR3BDetails]') IS NULL 
EXEC ('CREATE PROC [dbo].[GSTR3BDetails] AS SELECT 1 AS Id') 
GO


ALTER PROCEDURE [dbo].[GSTR3BDetails]
    @CompanyId INT =1,
	@FromDate  INT,
	@ToDate INT, 
	@YearId INT =0,
	@Type VARCHAR(1),
	@StateName varchar(50)=NULL,
	@gstin VARCHAR(15) ='NA',
	@taxrate DECIMAL =0
	 
AS
IF @Type ='A' --( a) Outward taxable supplies (other than zero rated, nil rated and exempted)
BEGIN 

SELECT p.id	BillId,ac.GstIn, vc.VoucherName,p.VoucherNo, p.VDate VoucherDate,'Outward Supplies' [SupplyType],
ISNULL(p.BillNo,p.VoucherNo) BillNo,ac.AccName Party,pd.ProductCode,pd.ProductName,isnull(pt.HsnCode,pd.HsnCode) HsnCode,
'Outward taxable supplies[Registeres]' [Nature of Supplies],
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
WHERE ((vc.VTypeId = 12) OR (vc.VTypeId=24 and ISNULL(p.Extra1,'X')='SALE' AND isnull(p.BillType,'X')='DEBIT NOTE'))  AND (
pt.Cgst > 0 OR pt.Igst > 0 OR pt.Sgst > 0) AND pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND
p.CompId = @CompanyId  AND p.VoucherDate BETWEEN @FromDate AND @ToDate 

UNION ALL

SELECT  p.id	BillId,ac.GstIn,vc.VoucherName,p.VoucherNo, p.VDate VoucherDate,'Outward Supplies' [SupplyType],
ISNULL(p.BillNo,p.VoucherNo) BillNo,ac.AccName Party,pd.ProductCode,pd.ProductName,isnull(pt.HsnCode,pd.HsnCode) HsnCode,
'Outward taxable supplies[Registeres]' [Nature of Supplies],
CASE WHEN pt.IgstPer = 0 THEN pt.CgstPer + pt.SgstPer ELSE pt.IgstPer END  AS GSTRate,
ISNULL(pt.Rate,0) Rate,
ISNULL(pt.Qty,0)  TotalQty,
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
WHERE  ((vc.VTypeId = 19) OR (vc.VTypeId=24 and ISNULL(p.Extra1,'X')='SALE' AND isnull(p.BillType,'X')='CREDIT NOTE')) AND (
						pt.Cgst > 0 OR pt.Sgst > 0 OR pt.Igst > 0) AND pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND
						p.CompId = @CompanyId AND p.VoucherDate BETWEEN @FromDate AND @ToDate

END

ELSE IF (@Type ='B') --- (b) Outward Taxable  supplies  (zero rated )
BEGIN

SELECT  p.id BillId,ac.GstIn,vc.VoucherName,p.VoucherNo, p.VDate VoucherDate,'Outward Supplies' [SupplyType],
ISNULL(p.BillNo,p.VoucherNo) BillNo,ac.AccName Party,i.ProductCode,i.ProductName,isnull(pt.HsnCode,i.HsnCode) HsnCode,
'Outward taxable supplies (zero rated)' [Nature of Supplies],
CASE WHEN pt.IgstPer = 0 THEN pt.CgstPer + pt.SgstPer ELSE pt.IgstPer END  AS GSTRate,
ISNULL(pt.Rate,0) Rate,
ISNULL(pt.Qty,0)  TotalQty,
pt.tdsamt TdsAmt,
pt.Total GrossAmount,
(pt.NetTotal-pt.Cgst-PT.Sgst-pt.Igst) TaxableValue,
pt.Igst IgstAmt,
pt.Cgst CgstAmt,
pt.Sgst SgstAmt,
pt.CgstPer,
pt.SgstPer,
pt.IgstPer,
pt.NetTotal TotalAmount
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.Id
LEFT OUTER JOIN dbo.Acc ac ON ac.Id = p.AccId
LEFT OUTER JOIN dbo.Product i ON i.Id = pt.ProductId
LEFT OUTER JOIN dbo.TaxMaster tx ON tx.Id = i.TaxId
WHERE ((vc.VTypeId = 12) OR (vc.VTypeId=24 and ISNULL(p.Extra1,'X')='SALE' AND isnull(p.BillType,'X')='DEBIT NOTE'))
 AND (tx.Id=8) AND pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND
	  p.CompID=@CompanyId AND p.VoucherDate BETWEEN @FromDate AND @ToDate

UNION ALL

SELECT  p.id BillId,ac.GstIn,vc.VoucherName,p.VoucherNo, p.VDate VoucherDate,'Outward Supplies' [SupplyType],
ISNULL(p.BillNo,p.VoucherNo) BillNo,ac.AccName Party,i.ProductCode,i.ProductName,isnull(pt.HsnCode,i.HsnCode) HsnCode,
'Outward taxable supplies (zero rated)' [Nature of Supplies],
CASE WHEN pt.IgstPer = 0 THEN pt.CgstPer + pt.SgstPer ELSE pt.IgstPer END  AS GSTRate,
ISNULL(pt.Rate,0) Rate,
ISNULL(pt.Qty,0)  TotalQty,
-1* (pt.tdsamt) TdsAmt,
-1* (pt.Total) GrossAmount,
-1* (pt.NetTotal-pt.Cgst -pt.Sgst -pt.Igst) TaxableValue,
-1* (pt.Igst) IgstAmt,
-1* (pt.Cgst) CgstAmt,
-1* (pt.Sgst) SgstAmt,
pt.CgstPer,
pt.SgstPer,
pt.IgstPer,
-1* pt.NetTotal TotalAmount
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.Id
LEFT OUTER JOIN dbo.Acc ac ON ac.Id = p.AccId
LEFT OUTER JOIN dbo.Product i ON i.Id = pt.ProductId
LEFT OUTER JOIN dbo.TaxMaster tx ON tx.Id = i.TaxId
WHERE ((vc.VTypeId = 19) OR(vc.VTypeId=24 and ISNULL(p.Extra1,'X')='SALE' AND isnull(p.BillType,'X')='CREDIT NOTE')) AND  tx.Id=8 AND
p.CompID =@CompanyId  AND p.VoucherDate BETWEEN @FromDate AND @ToDate and pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1


END

ELSE IF (@Type ='C') ---(c) Other Outward Taxable  supplies (Nil rated, exempted)
BEGIN

SELECT  p.id BillId,ac.GstIn,vc.VoucherName,p.VoucherNo, p.VDate VoucherDate,'Outward Supplies' [SupplyType],
ISNULL(p.BillNo,p.VoucherNo) BillNo,ac.AccName Party,i.ProductCode,i.ProductName,isnull(pt.HsnCode,i.HsnCode) HsnCode,
'Outward taxable supplies (Exempted)' [Nature of Supplies],
CASE WHEN pt.IgstPer = 0 THEN pt.CgstPer + pt.SgstPer ELSE pt.IgstPer END  AS GSTRate,
ISNULL(pt.Rate,0) Rate,
ISNULL(pt.Qty,0)  TotalQty,
pt.tdsamt TdsAmt,
pt.Total GrossAmount,
(pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst) TaxableValue,
pt.Igst IgstAmt,
pt.Cgst CgstAmt,
pt.Sgst SgstAmt,
pt.CgstPer,
pt.SgstPer,
pt.IgstPer,
pt.NetTotal TotalAmount
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.Id
LEFT OUTER JOIN dbo.Acc ac ON ac.Id = p.AccId
LEFT OUTER JOIN dbo.Product i ON i.Id = pt.ProductId
LEFT OUTER JOIN dbo.TaxMaster tx ON tx.Id = i.TaxId
WHERE ((vc.VTypeId = 12) AND (vc.VTypeId=24 AND ISNULL(p.Extra1,'X')='SALE' AND isnull(p.BillType,'X')='DEBIT NOTE')) and  tx.Id=6 AND
	  pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND  p.CompID =@CompanyId  AND p.VoucherDate BETWEEN @FromDate AND @ToDate

UNION ALL

SELECT  p.id BillId,ac.GstIn,vc.VoucherName,p.VoucherNo, p.VDate VoucherDate,'Outward Supplies' [SupplyType],
ISNULL(p.BillNo,p.VoucherNo) BillNo,ac.AccName Party,i.ProductCode,i.ProductName,isnull(pt.HsnCode,i.HsnCode) HsnCode,
'Outward taxable supplies (Exempted)' [Nature of Supplies], 
CASE WHEN pt.IgstPer = 0 THEN pt.CgstPer + pt.SgstPer ELSE pt.IgstPer END  AS GSTRate,
ISNULL(pt.Rate,0) Rate,
ISNULL(pt.Qty,0)  TotalQty,
-1* (pt.tdsamt) TdsAmt,
-1* (pt.Total) GrossAmount,
-1* (pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst)TaxableValue,
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
LEFT OUTER JOIN dbo.Product i ON i.Id = pt.ProductId
LEFT OUTER JOIN dbo.Acc ac ON ac.Id = p.AccId
LEFT OUTER JOIN dbo.TaxMaster tx ON tx.Id = i.TaxId
WHERE ((vc.VTypeId = 19) OR (vc.VTypeId=24 and ISNULL(p.Extra1,'X')='SALE' AND isnull(p.BillType,'X')='CREDIT NOTE')) AND  tx.Id=6 AND
	  pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND  p.CompID =@CompanyId  AND p.VoucherDate BETWEEN @FromDate AND @ToDate

END

ELSE IF (@Type ='D') --(d) Inward supplies (liable to reverse charge)
BEGIN

SELECT  p.id BillId,ac.GstIn,vc.VoucherName,p.VoucherNo, p.VDate VoucherDate,'Outward Supplies' [SupplyType],
ISNULL(p.BillNo,p.VoucherNo) BillNo,ac.AccName Party,pd.ProductCode,pd.ProductName,isnull(pt.HsnCode,pd.HsnCode) HsnCode,
'Inward supplies (liable to reverse charge)' [Nature of Supplies],
CASE WHEN pt.IgstPer = 0 THEN pt.CgstPer + pt.SgstPer ELSE pt.IgstPer END  AS GSTRate,
 ISNULL(pt.Rate,0) Rate,
ISNULL(pt.Qty,0)  TotalQty,
-1* (pt.tdsamt) TdsAmt,
-1* (pt.Total) GrossAmount,
(pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst) TaxableValue,
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
WHERE (vc.VTypeId = 13 AND ISNULL(p.Rcm,'X') = 'YES') OR (vc.VTypeId = 23 AND ISNULL(p.Rcm,'X') = 'YES')  AND
 (
pt.Cgst > 0 OR pt.Sgst > 0 OR pt.Igst > 0) AND 
pt.IsDeleted=0 AND p.IsDeleted=0 AND  p.CompID=@CompanyId  AND p.VoucherDate BETWEEN @FromDate AND @ToDate

UNION ALL

SELECT  p.id BillId,ac.GstIn,vc.VoucherName,p.VoucherNo, p.VDate VoucherDate,'Outward Supplies' [SupplyType],
ISNULL(p.BillNo,p.VoucherNo) BillNo,ac.AccName Party,pd.ProductCode,pd.ProductName,isnull(pt.HsnCode,pd.HsnCode) HsnCode,
'Inward supplies (liable to reverse charge)' [Nature of Supplies],
CASE WHEN pt.IgstPer = 0 THEN pt.CgstPer + pt.SgstPer ELSE pt.IgstPer END  AS GSTRate,
 ISNULL(pt.Rate,0) Rate,
ISNULL(pt.Qty,0)  TotalQty,
-1* (pt.tdsamt) TdsAmt,
-1* (pt.Total) GrossAmount,
-1*(pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst)TaxableValue,
-1*(pt.Igst) IgstAmt,
-1*(pt.Cgst) CgstAmt,
-1*(pt.Sgst) SgstAmt,
pt.CgstPer,
pt.SgstPer,
pt.IgstPer,
-1* pt.NetTotal TotalAmount
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.ID
LEFT OUTER JOIN dbo.Acc ac ON ac.Id = p.AccId
LEFT OUTER JOIN dbo.Product pd ON pd.Id = pt.ProductId
WHERE (vc.VTypeId = 48 AND ISNULL(p.Rcm,'X') = 'YES') OR (vc.VTypeId = 18 AND ISNULL(p.Rcm,'X') = 'YES')  AND
 (
pt.Cgst > 0 OR pt.Sgst > 0 OR pt.Igst > 0) AND pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND
p.CompID=@CompanyId  AND p.VoucherDate BETWEEN @FromDate AND @ToDate

END

ELSE IF (@Type ='E') --4.1 import of goods
BEGIN

SELECT p.id BillId, ac.GstIn,vc.VoucherName,p.VoucherNo, p.VDate VoucherDate,'Inward Supplies' [SupplyType],
ISNULL(p.BillNo,p.VoucherNo) BillNo,ac.AccName Party,pd.ProductCode,pd.ProductName,isnull(pt.HsnCode,pd.HsnCode) HsnCode,
'Import of goods' [Nature of Supplies],
CASE WHEN pt.IgstPer = 0 THEN pt.CgstPer + pt.SgstPer ELSE pt.IgstPer END  AS GSTRate,
 ISNULL(pt.Rate,0) Rate,
ISNULL(pt.Qty,0)  TotalQty,
(pt.tdsamt) TdsAmt,
(pt.Total) GrossAmount,
(pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst)TaxableValue,
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
WHERE ((vc.VTypeId IN(13,23)) OR (vc.VTypeId=24 and ISNULL(p.Extra1,'X')='PURCHASE' AND isnull(p.BillType,'X')='CREDIT NOTE'))   AND (
pt.Igst > 0 OR pt.Cgst > 0 OR pt.Sgst > 0) AND ISNULL(p.Rcm,'NO')='NO' AND ISNULL(p.BillType,'Regular') ='Import' AND
pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND
p.CompID=@CompanyId  AND p.VoucherDate BETWEEN @FromDate AND @ToDate 

UNION all

SELECT  p.id BillId,a.GstIn,vc.VoucherName,p.VoucherNo, p.VDate VoucherDate,'Inward Supplies' [SupplyType],
ISNULL(p.BillNo,p.VoucherNo) BillNo,a.AccName Party,pd.ProductCode,pd.ProductName,isnull(pt.HsnCode,pd.HsnCode) HsnCode,
'Import of goods' [Nature of Supplies],
CASE WHEN pt.IgstPer = 0 THEN pt.CgstPer + pt.SgstPer ELSE pt.IgstPer END  AS GSTRate,
 ISNULL(pt.Rate,0) Rate,
ISNULL(pt.Qty,0)  TotalQty,
-1* (pt.tdsamt) TdsAmt,
-1* (pt.Total) GrossAmount,
-1* (pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst)TaxableValue,
-1* (pt.Igst) IgstAmt,
-1* (pt.Cgst) CgstAmt,
-1* (pt.Sgst) SgstAmt,
pt.CgstPer,
pt.SgstPer,
pt.IgstPer,
-1* pt.NetTotal TotalAmount
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.acc a ON a.Id = p.AccID
LEFT OUTER JOIN dbo.acc l ON l.Id = p.BookAcId
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.ID
LEFT OUTER JOIN dbo.Product pd ON pd.Id = pt.ProductId
WHERE ((vc.VTypeId in (18,48)) OR (vc.VTypeId=24 AND ISNULL(p.Extra1,'X')='PURCHASE' AND isnull(p.BillType,'X')='DEBIT NOTE'))  AND (
pt.Cgst > 0 OR pt.Sgst > 0 OR pt.Igst > 0) AND a.VatTds = 'REG' AND ISNULL(p.Rcm,'NO')='NO' AND  ISNULL(p.BillType,'Regular') ='Import' and
p.CompID=@CompanyId  AND p.VoucherDate BETWEEN @FromDate AND @ToDate and pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1

END

ELSE IF (@Type ='F') --4.2 import of Services
BEGIN

SELECT p.id BillId, a.GstIn,vc.VoucherName,p.VoucherNo, p.VDate VoucherDate,'Inward Supplies' [SupplyType],
ISNULL(p.BillNo,p.VoucherNo) BillNo,a.AccName Party,pd.ProductCode,pd.ProductName,isnull(pt.HsnCode,pd.HsnCode) HsnCode,
'Import of Services' [Nature of Supplies],
CASE WHEN pt.IgstPer = 0 THEN pt.CgstPer + pt.SgstPer ELSE pt.IgstPer END  AS GSTRate,
 ISNULL(pt.Rate,0) Rate,
ISNULL(pt.Qty,0)  TotalQty,
(pt.tdsamt) TdsAmt,
(pt.Total) GrossAmount,
(pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst)TaxableValue,
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
LEFT OUTER JOIN dbo.acc a ON a.Id = p.AccID
LEFT OUTER JOIN dbo.Product pd ON pd.Id = pt.ProductId
WHERE ((vc.VTypeId IN(13,23)) OR (vc.VTypeId=24 and ISNULL(p.Extra1,'X')='PURCHASE' AND isnull(p.BillType,'X')='CREDIT NOTE'))   AND (
pt.Igst > 0 OR pt.Cgst > 0 OR pt.Sgst > 0) AND ISNULL(p.Rcm,'NO')='NO' AND ISNULL(p.BillType,'Regular') ='Import' AND ISNULL(p.Itc,'X')='Input Services' and
pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND  p.CompID=@CompanyId  AND p.VoucherDate BETWEEN @FromDate AND @ToDate

UNION all

SELECT p.id BillId, a.GstIn,vc.VoucherName,p.VoucherNo, p.VDate VoucherDate,'Inward Supplies' [SupplyType],
ISNULL(p.BillNo,p.VoucherNo) BillNo,a.AccName Party,pd.ProductCode,pd.ProductName,isnull(pt.HsnCode,pd.HsnCode) HsnCode,
'Import of Services' [Nature of Supplies],
CASE WHEN pt.IgstPer = 0 THEN pt.CgstPer + pt.SgstPer ELSE pt.IgstPer END  AS GSTRate,
 ISNULL(pt.Rate,0) Rate,
ISNULL(pt.Qty,0)  TotalQty,
-1* (pt.tdsamt) TdsAmt,
-1* (pt.Total) GrossAmount,
-1* (pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst)TaxableValue,
-1* pt.Igst IgstAmt,
-1* pt.Cgst CgstAmt,
-1* pt.Sgst SgstAmt,
pt.CgstPer,
pt.SgstPer,
pt.IgstPer,
-1* pt.NetTotal TotalAmount
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.acc a ON a.Id = p.AccID
LEFT OUTER JOIN dbo.acc l ON l.Id = p.BookAcId
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.ID
LEFT OUTER JOIN dbo.Product pd ON pd.Id = pt.ProductId
WHERE ((vc.VTypeId in (18,48)) OR (vc.VTypeId=24 AND ISNULL(p.Extra1,'X')='PURCHASE' AND isnull(p.BillType,'X')='DEBIT NOTE'))  AND (
pt.Cgst > 0 OR pt.Sgst > 0 OR pt.Igst > 0) AND a.VatTds = 'REG' AND ISNULL(p.Rcm,'NO')='NO' AND  ISNULL(p.BillType,'Regular') ='Import' AND ISNULL(p.Itc,'X')='Input Services' and
pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND  p.CompID=@CompanyId  AND p.VoucherDate BETWEEN @FromDate AND @ToDate

END

ELSE IF (@Type ='G')  -- (5) All other ITC
BEGIN
SELECT  p.id BillId,a.GstIn,vc.VoucherName,p.VoucherNo, p.VDate VoucherDate,'Inward Supplies' [SupplyType],
ISNULL(p.BillNo,p.VoucherNo) BillNo,a.AccName Party,pd.ProductCode,pd.ProductName,isnull(pt.HsnCode,pd.HsnCode) HsnCode,
'Inward taxable supplies' [Nature of Supplies],
CASE WHEN pt.IgstPer = 0 THEN pt.CgstPer + pt.SgstPer ELSE pt.IgstPer END  AS GSTRate,
 ISNULL(pt.Rate,0) Rate,
ISNULL(pt.Qty,0)  TotalQty,
(pt.tdsamt) TdsAmt,
(pt.Total) GrossAmount,
(pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst)TaxableValue,
(pt.Igst) IgstAmt,
(pt.Cgst) CgstAmt,
(pt.Sgst) SgstAmt,
pt.CgstPer,
pt.SgstPer,
pt.IgstPer,
pt.NetTotal TotalAmount
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.ID
LEFT OUTER JOIN dbo.acc a ON a.Id = p.AccID
LEFT OUTER JOIN dbo.Product pd ON pd.Id = pt.ProductId
WHERE ((vc.VTypeId IN(13,23,36,40,41) AND ISNULL(p.BillType,'Regular') ='Regular') OR (vc.VTypeId=24 and ISNULL(p.Extra1,'X')='PURCHASE' AND isnull(p.BillType,'X')='CREDIT NOTE'))   AND (
pt.Igst > 0 OR pt.Cgst > 0 OR pt.Sgst > 0)   AND ISNULL(p.Rcm,'NO')='NO'  and
pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND  p.CompID=@CompanyId  AND p.VoucherDate BETWEEN @FromDate AND @ToDate

UNION all

SELECT  p.id BillId,a.GstIn,vc.VoucherName,p.VoucherNo, p.VDate VoucherDate,'Inward Supplies' [SupplyType],
ISNULL(p.BillNo,p.VoucherNo) BillNo,a.AccName Party,pd.ProductCode,pd.ProductName,isnull(pt.HsnCode,pd.HsnCode) HsnCode,
'Inward taxable supplies' [Nature of Supplies],
CASE WHEN pt.IgstPer = 0 THEN pt.CgstPer + pt.SgstPer ELSE pt.IgstPer END  AS GSTRate,
ISNULL(pt.Rate,0) Rate,
ISNULL(pt.Qty,0)  TotalQty,
-1* (pt.tdsamt) TdsAmt,
-1* (pt.Total) GrossAmount,
-1* (pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst)TaxableValue,
-1* pt.Igst IgstAmt,
-1* pt.Cgst CgstAmt,
-1* pt.Sgst SgstAmt,
pt.CgstPer,
pt.SgstPer,
pt.IgstPer,
-1* pt.NetTotal TotalAmount
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.acc a ON a.Id = p.AccID
LEFT OUTER JOIN dbo.acc l ON l.Id = p.BookAcId
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.ID
LEFT OUTER JOIN dbo.Product pd ON pd.Id = pt.ProductId
WHERE ((vc.VTypeId in (18,48) AND  ISNULL(p.BillType,'Regular') ='Regular') OR (vc.VTypeId=24 AND ISNULL(p.Extra1,'X')='PURCHASE' AND isnull(p.BillType,'X')='DEBIT NOTE'))  AND (
pt.Cgst > 0 OR pt.Sgst > 0 OR pt.Igst > 0)  AND ISNULL(p.Rcm,'NO')='NO'  and
pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND p.CompID=@CompanyId  AND p.VoucherDate BETWEEN @FromDate AND @ToDate

END

ELSE IF (@Type ='H') --From a supplier under composition scheme, Exempt and Nil rated supply
BEGIN

SELECT  p.id BillId,a.GstIn,vc.VoucherName,p.VoucherNo, p.VDate VoucherDate,'Inward Supplies' [SupplyType],
ISNULL(p.BillNo,p.VoucherNo) BillNo,a.AccName Party,i.ProductCode,i.ProductName,isnull(pt.HsnCode,i.HsnCode) HsnCode,
'From a supplier under composition scheme, Exempt and Nil rated supply' [Nature of Supplies],
CASE WHEN pt.IgstPer = 0 THEN pt.CgstPer + pt.SgstPer ELSE pt.IgstPer END  AS GSTRate,
ISNULL(pt.Rate,0) Rate,
ISNULL(pt.Qty,0)  TotalQty,
pt.tdsamt TdsAmt,
pt.Total GrossAmount,
pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst TaxableValue,
pt.Igst IgstAmt,
pt.Cgst CgstAmt,
pt.Sgst SgstAmt,
pt.CgstPer,
pt.SgstPer,
pt.IgstPer,
pt.NetTotal TotalAmount,
CASE WHEN st.GstCode <> ISNULL(cms.GstCode,24) then pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst ELSE 0 END  AS IgstTaxable,
CASE WHEN st.GstCode = ISNULL(cms.GstCode,24) then pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst ELSE 0 END  AS CgstTaxable
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.acc a ON a.Id = p.AccID
LEFT OUTER JOIN dbo.AccBal acb ON acb.AccId = a.Id
LEFT OUTER JOIN dbo.city ct ON ct.Id = acb.CityId
LEFT OUTER JOIN dbo.state st ON st.Id = ct.StateId
LEFT OUTER JOIN dbo.company cm ON cm.Id = p.CompID
LEFT OUTER JOIN dbo.State cms ON cms.ID = cm.StateId
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.ID
LEFT OUTER JOIN dbo.Product i ON i.Id = pt.ProductId
LEFT OUTER JOIN dbo.TaxMaster tx ON tx.Id = i.TaxId
WHERE vc.VTypeId = 13  AND ( (a.VatTds ='CMP' OR tx.id =6)) AND pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND
p.CompID =@CompanyId AND acb.compid= @companyid AND acb.yearid=@yearid  AND p.VoucherDate BETWEEN @FromDate AND @ToDate

UNION all
SELECT  p.id BillId,a.GstIn,vc.VoucherName,p.VoucherNo, p.VDate VoucherDate,'Inward Supplies' [SupplyType],
'From a supplier under composition scheme, Exempt and Nil rated supply' [Nature of Supplies],
ISNULL(p.BillNo,p.VoucherNo) BillNo,a.AccName Party,i.ProductCode,i.ProductName,isnull(pt.HsnCode,i.HsnCode) HsnCode,
CASE WHEN pt.IgstPer = 0 THEN pt.CgstPer + pt.SgstPer ELSE pt.IgstPer END  AS GSTRate,
ISNULL(pt.Rate,0) Rate,
ISNULL(pt.Qty,0)  TotalQty,
-1* (pt.tdsamt) TdsAmt,
-1* (pt.Total) GrossAmount,
-1* (pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst) TaxableValue,
-1* (pt.Igst) IgstAmt,
-1* (pt.Cgst) CgstAmt,
-1* (pt.Sgst) SgstAmt,
pt.CgstPer,
pt.SgstPer,
pt.IgstPer,
-1* (pt.NetTotal) TotalAmount,
-1* (CASE WHEN st.GstCode <> ISNULL(cms.GstCode,24) THEN pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst ELSE 0 END ) AS IgstTaxable,
-1* (CASE WHEN st.GstCode = ISNULL(cms.GstCode,24) THEN pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst ELSE 0 END ) AS CgstTaxable
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.acc a ON a.Id = p.AccID
LEFT OUTER JOIN dbo.acc l ON l.Id = p.BookAcId
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.ID
LEFT OUTER JOIN dbo.Product i ON i.Id = pt.ProductId
LEFT OUTER JOIN dbo.TaxMaster tx ON tx.Id = i.TaxId
LEFT OUTER JOIN dbo.AccBal acb ON acb.AccId = a.Id
LEFT OUTER JOIN dbo.city ct ON ct.Id = acb.CityId
LEFT OUTER JOIN dbo.state st ON st.Id = ct.StateId
LEFT OUTER JOIN dbo.company cm ON cm.Id = p.CompID
LEFT OUTER JOIN dbo.State cms ON cms.ID = cm.StateId
WHERE vc.VTypeId = 18  AND ( (a.VatTds ='CMP' OR tx.Id=6)) AND pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND
p.CompID =@CompanyId AND acb.CompId=@CompanyId and acb.YearId =@YearId AND p.VoucherDate BETWEEN @FromDate AND @ToDate

END

ELSE IF (@Type ='I') --From a unregistered person
BEGIN

SELECT p.id BillId, a.GstIn,vc.VoucherName,p.VoucherNo, p.VDate VoucherDate,'Inward Supplies' [SupplyType],
ISNULL(p.BillNo,p.VoucherNo) BillNo,a.AccName Party,i.ProductCode,i.ProductName,isnull(pt.HsnCode,i.HsnCode) HsnCode,
'Non GST supply' [Nature of Supplies],
CASE WHEN pt.IgstPer = 0 THEN pt.CgstPer + pt.SgstPer ELSE pt.IgstPer END  AS GSTRate,
ISNULL(pt.Rate,0) Rate,
ISNULL(pt.Qty,0)  TotalQty,
pt.tdsamt TdsAmt,
pt.Total GrossAmount,
(pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst) TaxableValue,
pt.Igst IgstAmt,
pt.Cgst CgstAmt,
pt.Sgst SgstAmt,
pt.CgstPer,
pt.SgstPer,
pt.IgstPer,
(CASE WHEN st.GstCode <> ISNULL(cms.GstCode,24) THEN pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst ELSE 0 END ) AS IgstTaxable,
(CASE WHEN st.GstCode = ISNULL(cms.GstCode,24) THEN pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst ELSE 0 END ) AS CgstTaxable
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.acc a ON a.Id = p.AccID
LEFT OUTER JOIN dbo.acc l ON l.Id = p.BookAcId
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.ID
LEFT OUTER JOIN dbo.Product i ON i.Id = pt.ProductId
LEFT OUTER JOIN dbo.TaxMaster tx ON tx.Id = i.TaxId
LEFT OUTER JOIN dbo.AccBal acb ON acb.AccId = a.Id
LEFT OUTER JOIN dbo.city ct ON ct.Id = acb.CityId
LEFT OUTER JOIN dbo.state st ON st.Id = ct.StateId
LEFT OUTER JOIN dbo.company cm ON cm.Id = p.CompID
LEFT OUTER JOIN dbo.State cms ON cms.ID = cm.StateId
WHERE vc.VTypeId = 13  AND (tx.Id=7) AND pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND
p.CompID =@CompanyId AND acb.CompId=@CompanyId and acb.YearId =@YearId AND p.VoucherDate BETWEEN @FromDate AND @ToDate

UNION all

SELECT  p.id BillId,a.GstIn,vc.VoucherName,p.VoucherNo, p.VDate VoucherDate,'Inward Supplies' [SupplyType],
ISNULL(p.BillNo,p.VoucherNo) BillNo,a.AccName Party,i.ProductCode,i.ProductName,isnull(pt.HsnCode,i.HsnCode) HsnCode,
'Non GST supply' [Nature of Supplies],
CASE WHEN pt.IgstPer = 0 THEN pt.CgstPer + pt.SgstPer ELSE pt.IgstPer END  AS GSTRate,
ISNULL(pt.Rate,0) Rate,
ISNULL(pt.Qty,0)  TotalQty,
-1* (pt.tdsamt) TdsAmt,
-1* (pt.Total) GrossAmount,
-1* (pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst)TaxableValue,
-1* (pt.Igst) IgstAmt,
-1* (pt.Cgst) CgstAmt,
-1* (pt.Sgst) SgstAmt,
pt.CgstPer,
pt.SgstPer,
pt.IgstPer,
-1* (CASE WHEN st.Id <> cms.Id THEN pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst ELSE 0 END ) AS IgstTaxable,
-1* (CASE WHEN st.Id = cms.Id THEN pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst ELSE 0 END ) AS CgstTaxable
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.acc a ON a.Id = p.AccID
LEFT OUTER JOIN dbo.acc l ON l.Id = p.BookAcId
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.ID
LEFT OUTER JOIN dbo.Product i ON i.Id = pt.ProductId
LEFT OUTER JOIN dbo.TaxMaster tx ON tx.Id = i.TaxId
LEFT OUTER JOIN dbo.AccBal acb ON acb.AccId = a.Id
LEFT OUTER JOIN dbo.city ct ON ct.Id = acb.CityId
LEFT OUTER JOIN dbo.state st ON st.Id = ct.StateId
LEFT OUTER JOIN dbo.company cm ON cm.Id = p.CompID
LEFT OUTER JOIN dbo.State cms ON cms.ID = cm.StateId
WHERE vc.VTypeId = 18  AND ( tx.Id=7) AND pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND
p.CompID =@CompanyId AND acb.CompId=@CompanyId and acb.YearId =@YearId AND p.VoucherDate BETWEEN @FromDate AND @ToDate

END
ELSE IF (@Type ='J') --'State wise detail'Supplies made to Unregister Person 
BEGIN

SELECT p.id BillId, a.GstIn,vc.VoucherName,p.VoucherNo, p.VDate VoucherDate,'Outward Supplies [Inter-State]' [SupplyType],
ISNULL(p.BillNo,p.VoucherNo) BillNo,a.AccName Party,i.ProductCode,i.ProductName,isnull(pt.HsnCode,i.HsnCode) HsnCode,
'Supplies made to Unregister Person' [Nature of Supplies],
CASE WHEN pt.IgstPer = 0 THEN pt.CgstPer + pt.SgstPer ELSE pt.IgstPer END  AS GSTRate,
ISNULL(pt.Rate,0) Rate,
ISNULL(pt.Qty,0)  TotalQty,
pt.tdsamt TdsAmt,
pt.Total GrossAmount,
pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst TaxableValue,
pt.Igst IgstAmt,
pt.Cgst CgstAmt,
pt.Sgst SgstAmt,
pt.CgstPer,
pt.SgstPer,
pt.IgstPer,
pt.NetTotal TotalAmount,
CASE WHEN st.GstCode <> ISNULL(cms.GstCode,24) then pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst ELSE 0 END  AS IgstTaxable,
CASE WHEN st.GstCode = ISNULL(cms.GstCode,24) then pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst ELSE 0 END  AS CgstTaxable
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.acc a ON a.Id = p.AccID
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.Id
LEFT OUTER JOIN dbo.Product i ON i.Id = pt.ProductId
LEFT OUTER JOIN dbo.AccBal acb ON acb.AccId = a.Id
LEFT OUTER JOIN dbo.City ct ON ct.Id = acb.CityId
LEFT OUTER JOIN dbo.[State] st ON st.Id = ct.StateId
LEFT OUTER JOIN dbo.[State] ste ON ste.Id = p.StateId
LEFT OUTER JOIN dbo.company cm ON cm.ID = p.CompID
LEFT OUTER JOIN dbo.[State] cms ON cms.Id = cm.StateId
WHERE ((vc.VTypeId = 12) OR (vc.VTypeId=24 and ISNULL(p.Extra1,'X')='SALES' AND ISNULL(p.BillType,'X') ='DEBIT NOTE')) AND
p.CompID=@CompanyId AND acb.CompId=@CompanyId and acb.YearId = @YearId AND p.VoucherDate BETWEEN @FromDate AND @ToDate and
pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 
AND (@taxrate=0 OR pt.SgstPer+pt.CgstPer+pt.IgstPer=@taxrate) 
AND ste.StateName = @StateName AND (@gstin='NA' OR a.GstIn=@gstin)
and (a.VatTds='URD' OR a.VatTds='ECOM' or a.VatTds = 'CMP')
--AND cm.StateId <> ste.Id

 
UNION all
SELECT  p.id BillId,a.GstIn,vc.VoucherName,p.VoucherNo, p.VDate VoucherDate,'Outward Supplies [Inter-State]' [SupplyType],
'Supplies made to Unregister Person' [Nature of Supplies],
ISNULL(p.BillNo,p.VoucherNo) BillNo,a.AccName Party,i.ProductCode,i.ProductName,isnull(pt.HsnCode,i.HsnCode) HsnCode,
CASE WHEN pt.IgstPer = 0 THEN pt.CgstPer + pt.SgstPer ELSE pt.IgstPer END  AS GSTRate,
ISNULL(pt.Rate,0) Rate,
ISNULL(pt.Qty,0)  TotalQty,
-1* (pt.tdsamt) TdsAmt,
-1* (pt.Total) GrossAmount,
-1* (pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst) TaxableValue,
-1* (pt.Igst) IgstAmt,
-1* (pt.Cgst) CgstAmt,
-1* (pt.Sgst) SgstAmt,
pt.CgstPer,
pt.SgstPer,
pt.IgstPer,
-1* (pt.NetTotal) TotalAmount,
-1* (CASE WHEN st.GstCode <> ISNULL(cms.GstCode,24) THEN pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst ELSE 0 END ) AS IgstTaxable,
-1* (CASE WHEN st.GstCode = ISNULL(cms.GstCode,24) THEN pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst ELSE 0 END ) AS CgstTaxable

FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.acc a ON a.Id = p.AccID
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.Id
LEFT OUTER JOIN dbo.AccBal acb ON acb.AccId = a.Id
LEFT OUTER JOIN dbo.City ct ON ct.Id = acb.CityId
LEFT OUTER JOIN dbo.[State] st ON st.Id = ct.StateId
LEFT OUTER JOIN dbo.company cm ON cm.ID = p.CompID
LEFT OUTER JOIN dbo.[State] ste ON ste.Id = p.StateId
LEFT OUTER JOIN dbo.[State] cms ON cms.Id = cm.StateId
LEFT OUTER JOIN dbo.Product i ON i.Id = pt.ProductId
WHERE ((vc.VTypeId = 19) OR (vc.VTypeId=24 and ISNULL(p.Extra1,'X')='SALE' AND isnull(p.BillType,'X')='CREDIT NOTE')) 
 -- AND cm.StateId <> ste.Id 
  AND (@taxrate=0 OR pt.SgstPer+pt.CgstPer+pt.IgstPer=@taxrate) AND
p.CompID=@CompanyId AND acb.CompId=@CompanyId AND pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND
acb.YearId = @YearId AND p.VoucherDate BETWEEN @FromDate AND @ToDate
AND ste.StateName = @StateName AND (@gstin='NA' OR a.GstIn=@gstin)
and (a.VatTds='URD' OR a.VatTds='ECOM' or a.VatTds = 'CMP' )



 
END

GO


