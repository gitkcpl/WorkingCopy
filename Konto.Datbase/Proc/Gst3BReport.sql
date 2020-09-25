CREATE PROCEDURE [dbo].[Gst3BReport]
    @CompanyId INT ,
	@YearId INT = 0,
	@FromDate  INT ,
	@ToDate INT 

AS
BEGIN

---( a) Outward taxable supplies (other than zero rated, nil rated and exempted)

SELECT sl.SupplyType,sl.[Nature of Supplies] AS TransType,'-' AS StateName,SUM(ISNULL(sl.TaxableValue,0))TaxableValue,
SUM(ISNULL(sl.IGST,0)) IGSTAmt,
SUM(ISNULL(sl.CGST,0)) CGSTAmt,
SUM(ISNULL(sl.SGST,0)) SGSTAmt,
0 UrdTaxable,
0 CmpTaxable,
0 UrdIgst,
0 CmpIgst
 FROM
(
SELECT 'Outward Supplies' [SupplyType],
'Outward taxable supplies[Registeres]' [Nature of Supplies],
SUM(pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst)TaxableValue,
SUM(pt.Igst)IGST,
SUM(pt.Cgst)CGST,
SUM(pt.Sgst)SGST
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.ID
WHERE ((vc.VTypeId = 12) OR (vc.VTypeId=24 and ISNULL(p.Extra1,'X')='SALE' AND isnull(p.BillType,'X')='DEBIT NOTE'))  AND (
pt.Cgst > 0 OR pt.Igst > 0 OR pt.Sgst > 0) AND pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND
p.CompId = @CompanyId  AND p.VoucherDate BETWEEN @FromDate AND @ToDate 

UNION ALL

SELECT 'Outward Supplies' [SupplyType],
'Outward taxable supplies[Registeres]' [Nature of Supplies],
-1* SUM(pt.NetTotal- pt.Cgst-pt.Sgst -pt.Igst )TaxableValue,
-1* SUM(pt.Igst)IGST,
-1* SUM(pt.Cgst)CGST,
-1* SUM(pt.Sgst)SGST
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.Id
WHERE  ((vc.VTypeId = 19) OR (vc.VTypeId=24 and ISNULL(p.Extra1,'X')='SALE' AND isnull(p.BillType,'X')='CREDIT NOTE')) AND (
						pt.Cgst > 0 OR pt.Sgst > 0 OR pt.Igst > 0) AND pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND
						p.CompId = @CompanyId AND p.VoucherDate BETWEEN @FromDate AND @ToDate

)sl
GROUP BY sl.SupplyType,sl.[Nature of Supplies]

UNION ALL

-----( b) Outward taxable supplies (zero rated)

SELECT so.SupplyType,so.[Nature of Supplies],'-' StateName,
SUM(ISNULL(so.TaxableValue,0)),SUM(ISNULL(so.IGST,0)) IGST,SUM(ISNULL(so.CGST,0))CGST,
SUM(ISNULL(So.sgst,0))sgst,
0,
0,
0,
0
 FROM (
SELECT 
'Outward Supplies' [SupplyType],
'Outward taxable supplies (zero rated)' [Nature of Supplies],
SUM(pt.NetTotal-pt.Cgst-PT.Sgst-pt.Igst)TaxableValue,
SUM(pt.Igst)IGST,
SUM(pt.Cgst)CGST,
SUM(pt.Sgst)SGST
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.Id
LEFT OUTER JOIN dbo.Product i ON i.Id = pt.ProductId
LEFT OUTER JOIN dbo.TaxMaster tx ON tx.Id = i.TaxId
WHERE ((vc.VTypeId = 12) OR (vc.VTypeId=24 and ISNULL(p.Extra1,'X')='SALE' AND isnull(p.BillType,'X')='DEBIT NOTE'))
 AND (tx.Id=8) AND pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND
	  p.CompID=@CompanyId AND p.VoucherDate BETWEEN @FromDate AND @ToDate

UNION ALL

SELECT 
'Outward Supplies' [SupplyType],
'Outward taxable supplies (zero rated)' [Nature of Supplies],
-1*SUM(pt.NetTotal-pt.Cgst -pt.Sgst -pt.Igst)TaxableValue,
-1*SUM(pt.Igst)IGST,
-1*SUM(pt.Cgst)CGST,
-1 * SUM(pt.Sgst)SGST
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.Id
LEFT OUTER JOIN dbo.Product i ON i.Id = pt.ProductId
LEFT OUTER JOIN dbo.TaxMaster tx ON tx.Id = i.TaxId
WHERE ((vc.VTypeId = 19) OR(vc.VTypeId=24 and ISNULL(p.Extra1,'X')='SALE' AND isnull(p.BillType,'X')='CREDIT NOTE')) AND  tx.Id=8 AND
p.CompID =@CompanyId  AND p.VoucherDate BETWEEN @FromDate AND @ToDate and pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1

) So
GROUP BY SO.SupplyType,so.[Nature of Supplies]

--- ( c) Other outward supplies, (Nil rated, exempted)

UNION ALL

SELECT  se.SupplyType,se.[Nature of Supplies],'' StateName,
SUM(ISNULL(se.TaxableValue,0)),SUM(ISNULL(se.IGST,0)) IGST,SUM(ISNULL(se.CGST,0))CGST,
SUM(ISNULL(Se.SGST,0))sgst,
0,
0,
0,
0
 FROM(
SELECT 
'Outward Supplies' [SupplyType],
'Outward taxable supplies (Exempted)' [Nature of Supplies],
SUM(pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst)TaxableValue,
SUM(pt.Igst)IGST,
SUM(pt.Cgst)CGST,
SUM(pt.Sgst)SGST
FROM dbo.BillMain p 

LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId

LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.Id
LEFT OUTER JOIN dbo.Product i ON i.Id = pt.ProductId
LEFT OUTER JOIN dbo.TaxMaster tx ON tx.Id = i.TaxId
WHERE ((vc.VTypeId = 12) AND (vc.VTypeId=24 AND ISNULL(p.Extra1,'X')='SALE' AND isnull(p.BillType,'X')='DEBIT NOTE')) and  tx.Id=6 AND
	  pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND  p.CompID =@CompanyId  AND p.VoucherDate BETWEEN @FromDate AND @ToDate

UNION ALL

SELECT 
'Outward Supplies' [SupplyType],
'Outward taxable supplies (Exempted)' [Nature of Supplies], 
-1*SUM(pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst)TaxableValue,
-1*SUM(pt.Igst)IGST,
-1*SUM(pt.Cgst)CGST,
-1 * SUM(pt.Sgst)SGST
FROM dbo.BillMain p 

LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId

LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.Id
LEFT OUTER JOIN dbo.Product i ON i.Id = pt.ProductId
LEFT OUTER JOIN dbo.TaxMaster tx ON tx.Id = i.TaxId
WHERE ((vc.VTypeId = 19) OR (vc.VTypeId=24 and ISNULL(p.Extra1,'X')='SALE' AND isnull(p.BillType,'X')='CREDIT NOTE')) AND  tx.Id=6 AND
	  pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND  p.CompID =@CompanyId  AND p.VoucherDate BETWEEN @FromDate AND @ToDate

)se GROUP BY se.SupplyType,se.[Nature of Supplies]

-------( d) Inward supplies (liable to reverse charge)

UNION ALL
SELECT  rc.SupplyType,rc.[Nature of Supplies],'-' StateName,
SUM(ISNULL(rc.TaxableValue,0)),SUM(ISNULL(rc.IGST,0)) IGST,SUM(ISNULL(rc.CGST,0))CGST,
SUM(ISNULL(rc.sgst,0))sgst,
0,
0,
0,
0 FROM (
SELECT 
'Outward Supplies' [SupplyType],
'Inward supplies (liable to reverse charge)' [Nature of Supplies],
SUM(pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst)TaxableValue,
SUM(pt.Igst)IGST,
SUM(pt.Cgst)CGST,
SUM(pt.Sgst)SGST
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.ID
WHERE (vc.VTypeId = 13 AND ISNULL(p.Rcm,'X') = 'YES') OR (vc.VTypeId = 23 AND ISNULL(p.Rcm,'X') = 'YES')  AND
 (
pt.Cgst > 0 OR pt.Sgst > 0 OR pt.Igst > 0) AND 
pt.IsDeleted=0 AND p.IsDeleted=0 AND  p.CompID=@CompanyId  AND p.VoucherDate BETWEEN @FromDate AND @ToDate
UNION ALL
SELECT 
'Outward Supplies' [SupplyType],
'Inward supplies (liable to reverse charge)' [Nature of Supplies],
-1*SUM(pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst)TaxableValue,
-1*SUM(pt.Igst)IGST,
-1*SUM(pt.Cgst)CGST,
-1*SUM(pt.Sgst)SGST
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.ID
WHERE (vc.VTypeId = 48 AND ISNULL(p.Rcm,'X') = 'YES') OR (vc.VTypeId = 18 AND ISNULL(p.Rcm,'X') = 'YES')  AND
 (
pt.Cgst > 0 OR pt.Sgst > 0 OR pt.Igst > 0) AND pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND
p.CompID=@CompanyId  AND p.VoucherDate BETWEEN @FromDate AND @ToDate

)rc
GROUP BY rc.SupplyType,rc.[Nature of Supplies]

UNION ALL

--Supplies made to Unregister Person

SELECT su.SupplyType,su.[Nature of Supplies],su.StateName ,
SUM(ISNULL(su.IGST,0))IGST,0,0, SUM(ISNULL(su.TaxableValue,0)), SUM(ISNULL(su.URDTaxable,0)), SUM(ISNULL(su.CMPTaxable,0)),
SUM(ISNULL(su.URDIGST,0)), SUM(ISNULL(su.CMPIGST,0)) FROM
(
SELECT 
'Outward Supplies [Inter-State]' [SupplyType],
'Supplies made to Unregister Person' [Nature of Supplies], 
CASE WHEN a.VatTds='ECOM' THEN ste.GstCode + '-' + STE.StateName ELSE st.GstCode + '-' +  st.StateName END StateName,
SUM( pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst)TaxableValue,
SUM(CASE WHEN a.VatTds = 'URD' OR a.VatTds='ECOM' THEN pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst ELSE 0 END ) AS URDTaxable,
SUM(CASE WHEN a.VatTds = 'CMP' THEN pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst ELSE 0 END ) AS CMPTaxable,
SUM(CASE WHEN a.VatTds = 'URD' OR a.VatTds='ECOM' THEN pt.Igst ELSE 0 END ) AS URDIGST,
SUM(CASE WHEN a.VatTds = 'CMP' THEN pt.Igst ELSE 0 END ) AS CMPIGST,
SUM(pt.Igst)IGST,
0 cgst,
0 sgst
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.acc a ON a.Id = p.AccID
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.Id
LEFT OUTER JOIN dbo.AccBal acb ON acb.AccId = a.Id
LEFT OUTER JOIN dbo.City ct ON ct.Id = acb.CityId
LEFT OUTER JOIN dbo.[State] st ON st.Id = ct.StateId
LEFT OUTER JOIN dbo.[State] ste ON ste.Id = p.StateId
LEFT OUTER JOIN dbo.company cm ON cm.ID = p.CompID
WHERE ((vc.VTypeId = 12) OR (vc.VTypeId=24 and ISNULL(p.Extra1,'X')='SALES' AND ISNULL(p.BillType,'X') ='DEBIT NOTE')) AND
p.CompID=@CompanyId AND acb.CompId=@CompanyId and acb.YearId = @YearId AND p.VoucherDate BETWEEN @FromDate AND @ToDate and
pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 
AND a.VatTds <> 'REG'
AND cm.StateId <> st.Id
GROUP BY st.StateName,st.GstCode,ste.StateName,ste.GstCode,a.VatTds

UNION ALL

SELECT 
'Outward Supplies [Inter-State]' [SupplyType],
'Supplies made to Unregister Person' [Nature of Supplies],
 CASE WHEN a.VatTds='ECOM' THEN ste.GstCode + '-' + STE.StateName ELSE st.GstCode + '-' +  st.StateName END StateName,
-1*SUM(pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst)TaxableValue,
-1*SUM(CASE WHEN a.VatTds = 'URD' OR a.VatTds='ECOM' THEN pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst ELSE 0 END ) AS URDTaxable,
-1*SUM(CASE WHEN a.VatTds = 'CMP' THEN pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst ELSE 0 END ) AS CMPTaxable,
-1*SUM(CASE WHEN a.VatTds = 'URD' OR a.VatTds='ECOM' THEN pt.Igst ELSE 0 END ) AS URDIGST,
-1*SUM(CASE WHEN a.VatTds = 'CMP' THEN pt.Igst ELSE 0 END ) AS CMPIGST,
-1*SUM(pt.Igst)IGST,
0,0
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.acc a ON a.Id = p.AccID
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.Id
LEFT OUTER JOIN dbo.AccBal acb ON acb.AccId = a.Id
LEFT OUTER JOIN dbo.City ct ON ct.Id = acb.CityId
LEFT OUTER JOIN dbo.[State] st ON st.Id = ct.StateId
LEFT OUTER JOIN dbo.company cm ON cm.ID = p.CompID
LEFT OUTER JOIN dbo.[State] ste ON ste.Id = p.StateId
WHERE ((vc.VTypeId = 19) OR (vc.VTypeId=24 and ISNULL(p.Extra1,'X')='SALE' AND isnull(p.BillType,'X')='CREDIT NOTE'))   AND cm.StateId <> st.Id  and
p.CompID=@CompanyId AND acb.CompId=@CompanyId AND pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND
acb.YearId = @YearId AND p.VoucherDate BETWEEN @FromDate AND @ToDate
AND a.VatTds <> 'REG'
GROUP BY st.StateName,st.GstCode,ste.StateName,ste.GstCode,a.VatTds
)su
GROUP BY su.SupplyType,su.[Nature of Supplies],su.StateName

UNION ALL

--4.1 import of goods
SELECT oth.[SupplyType],oth.[Nature of Supplies],'-' AS StateName,
SUM(ISNULL(oth.TaxableValue,0)),SUM(ISNULL(oth.IGST,0)),SUM(ISNULL(oth.CGST,0)),SUM(ISNULL(oth.SGST,0)) ,
0,
0,
0,
0
FROM(
SELECT 
'Inward Supplies' [SupplyType],
'Import of goods' [Nature of Supplies],
SUM(pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst)TaxableValue,
SUM(pt.Igst)IGST,
SUM(pt.Cgst)CGST,
SUM(pt.Sgst)SGST
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.ID
WHERE ((vc.VTypeId IN(13,23)) OR (vc.VTypeId=24 and ISNULL(p.Extra1,'X')='PURCHASE' AND isnull(p.BillType,'X')='CREDIT NOTE'))   AND (
pt.Igst > 0 OR pt.Cgst > 0 OR pt.Sgst > 0) AND ISNULL(p.Rcm,'NO')='NO' AND ISNULL(p.BillType,'Regular') ='Import' AND
pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND
p.CompID=@CompanyId  AND p.VoucherDate BETWEEN @FromDate AND @ToDate 

UNION all

SELECT 
'Inward Supplies' [SupplyType],
'Import of goods' [Nature of Supplies],
-1*SUM(pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst)TaxableValue,
-1*SUM(pt.Igst)IGST,
-1*SUM(pt.Cgst)CGST,
-1* SUM(pt.Sgst)SGST
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.acc a ON a.Id = p.AccID
LEFT OUTER JOIN dbo.acc l ON l.Id = p.BookAcId
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.ID
WHERE ((vc.VTypeId in (18,48)) OR (vc.VTypeId=24 AND ISNULL(p.Extra1,'X')='PURCHASE' AND isnull(p.BillType,'X')='DEBIT NOTE'))  AND (
pt.Cgst > 0 OR pt.Sgst > 0 OR pt.Igst > 0) AND a.VatTds = 'REG' AND ISNULL(p.Rcm,'NO')='NO' AND  ISNULL(p.BillType,'Regular') ='Import' and
p.CompID=@CompanyId  AND p.VoucherDate BETWEEN @FromDate AND @ToDate and pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1
)oth
GROUP BY oth.SupplyType,oth.[Nature of Supplies]
UNION ALL

--4.2 import of Services
SELECT oth.[SupplyType],oth.[Nature of Supplies],'-' AS StateName,
SUM(ISNULL(oth.TaxableValue,0)),SUM(ISNULL(oth.IGST,0)),SUM(ISNULL(oth.CGST,0)),SUM(ISNULL(oth.SGST,0)) ,
0,
0,
0,
0
FROM(
SELECT 
'Inward Supplies' [SupplyType],
'Import of Services' [Nature of Supplies],
SUM(pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst)TaxableValue,
SUM(pt.Igst)IGST,
SUM(pt.Cgst)CGST,
SUM(pt.Sgst)SGST
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.ID
WHERE ((vc.VTypeId IN(13,23)) OR (vc.VTypeId=24 and ISNULL(p.Extra1,'X')='PURCHASE' AND isnull(p.BillType,'X')='CREDIT NOTE'))   AND (
pt.Igst > 0 OR pt.Cgst > 0 OR pt.Sgst > 0) AND ISNULL(p.Rcm,'NO')='NO' AND ISNULL(p.BillType,'Regular') ='Import' AND ISNULL(p.Itc,'X')='Input Services' and
pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND  p.CompID=@CompanyId  AND p.VoucherDate BETWEEN @FromDate AND @ToDate

UNION all

SELECT 
'Inward Supplies' [SupplyType],
'Import of Services' [Nature of Supplies],
-1*SUM(pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst)TaxableValue,
-1*SUM(pt.Igst)IGST,
-1*SUM(pt.Cgst)CGST,
-1* SUM(pt.Sgst)SGST
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.acc a ON a.Id = p.AccID
LEFT OUTER JOIN dbo.acc l ON l.Id = p.BookAcId
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.ID
WHERE ((vc.VTypeId in (18,48)) OR (vc.VTypeId=24 AND ISNULL(p.Extra1,'X')='PURCHASE' AND isnull(p.BillType,'X')='DEBIT NOTE'))  AND (
pt.Cgst > 0 OR pt.Sgst > 0 OR pt.Igst > 0) AND a.VatTds = 'REG' AND ISNULL(p.Rcm,'NO')='NO' AND  ISNULL(p.BillType,'Regular') ='Import' AND ISNULL(p.Itc,'X')='Input Services' and
pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND  p.CompID=@CompanyId  AND p.VoucherDate BETWEEN @FromDate AND @ToDate
)oth
GROUP BY oth.SupplyType,oth.[Nature of Supplies]


UNION all

------(5) All other ITC

SELECT oth.[SupplyType],oth.[Nature of Supplies],'-' AS StateName,
SUM(ISNULL(oth.TaxableValue,0)),SUM(ISNULL(oth.IGST,0)),SUM(ISNULL(oth.CGST,0)),SUM(ISNULL(oth.SGST,0)) ,
0,
0,
0,
0
FROM(
SELECT 
'Inward Supplies' [SupplyType],
'Inward taxable supplies' [Nature of Supplies],
SUM(pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst)TaxableValue,
SUM(pt.Igst)IGST,
SUM(pt.Cgst)CGST,
SUM(pt.Sgst)SGST
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.ID
WHERE ((vc.VTypeId IN(13,23)) OR (vc.VTypeId=24 and ISNULL(p.Extra1,'X')='PURCHASE' AND isnull(p.BillType,'X')='CREDIT NOTE'))   AND (
pt.Igst > 0 OR pt.Cgst > 0 OR pt.Sgst > 0) AND ISNULL(p.Rcm,'NO')='NO' AND ISNULL(p.BillType,'Regular') ='Regular' and
pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND  p.CompID=@CompanyId  AND p.VoucherDate BETWEEN @FromDate AND @ToDate

UNION all

SELECT 
'Inward Supplies' [SupplyType],
'Inward taxable supplies' [Nature of Supplies],
-1*SUM(pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst)TaxableValue,
-1*SUM(pt.Igst)IGST,
-1*SUM(pt.Cgst)CGST,
-1* SUM(pt.Sgst)SGST
FROM dbo.BillMain p 
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = p.VoucherId
LEFT OUTER JOIN dbo.acc a ON a.Id = p.AccID
LEFT OUTER JOIN dbo.acc l ON l.Id = p.BookAcId
LEFT OUTER JOIN dbo.BillTrans pt ON pt.BillId = p.ID
WHERE ((vc.VTypeId in (18,48)) OR (vc.VTypeId=24 AND ISNULL(p.Extra1,'X')='PURCHASE' AND isnull(p.BillType,'X')='DEBIT NOTE'))  AND (
pt.Cgst > 0 OR pt.Sgst > 0 OR pt.Igst > 0) AND a.VatTds = 'REG' AND ISNULL(p.Rcm,'NO')='NO' AND  ISNULL(p.BillType,'Regular') ='Regular' and
pt.IsDeleted=0 AND p.IsDeleted=0 AND p.IsActive = 1 AND p.CompID=@CompanyId  AND p.VoucherDate BETWEEN @FromDate AND @ToDate
)oth
GROUP BY oth.SupplyType,oth.[Nature of Supplies]


---From a supplier under composition scheme, Exempt and Nil rated supply

UNION ALL

SELECT oth.[SupplyType],oth.[Nature of Supplies],'-' AS StateName,
SUM(ISNULL(oth.TaxableValue,0)),SUM(isnull(oth.IGST,0)),SUM(ISNULL(oth.CGST,0)),SUM(ISNULL(oth.SGST,0)) ,
SUM(ISNULL(oth.IgstTaxable,0)) AS IgstTaxable,
SUM(ISNULL(oth.CgstTaxable,0)) AS CgstTaxable,
0,
0
FROM(
SELECT 
'Inward Supplies' [SupplyType],
'From a supplier under composition scheme, Exempt and Nil rated supply' [Nature of Supplies],
SUM(pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst)TaxableValue,
SUM(pt.Igst)IGST,
SUM(pt.Cgst)CGST,
SUM(pt.Sgst)SGST,
SUM(CASE WHEN st.GstCode <> ISNULL(cms.GstCode,24) then pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst ELSE 0 END ) AS IgstTaxable,
SUM(CASE WHEN st.GstCode = ISNULL(cms.GstCode,24) then pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst ELSE 0 END ) AS CgstTaxable
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
SELECT 
'Inward Supplies' [SupplyType],
'From a supplier under composition scheme, Exempt and Nil rated supply' [Nature of Supplies],
-1*SUM(pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst)TaxableValue,
-1*SUM(pt.Igst)IGST,
-1*SUM(pt.Cgst)CGST,
-1*SUM(pt.Sgst)SGST,
-1*SUM(CASE WHEN st.GstCode <> ISNULL(cms.GstCode,24) THEN pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst ELSE 0 END ) AS IgstTaxable,
-1*SUM(CASE WHEN st.GstCode = ISNULL(cms.GstCode,24) THEN pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst ELSE 0 END ) AS CgstTaxable
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
--GROUP BY l.AccName,tx.TaxName,st.StateName
)oth
GROUP BY oth.SupplyType,oth.[Nature of Supplies]

UNION ALL

----Non GST supply
SELECT oth.[SupplyType],oth.[Nature of Supplies],'-' AS StateName,
SUM(ISNULL(oth.TaxableValue,0)),SUM(ISNULL(oth.IGST,0)),SUM(ISNULL(oth.CGST,0)),SUM(ISNULL(oth.SGST,0)) ,
SUM(ISNULL(oth.IgstTaxable,0)) AS IgstTaxable,
SUM(ISNULL(oth.CgstTaxable,0)) AS CgstTaxable,
0,
0
FROM(
SELECT 
'Inward Supplies' [SupplyType],
'Non GST supply' [Nature of Supplies],
SUM(pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst)TaxableValue,
SUM(pt.Igst)IGST,
SUM(pt.Cgst)CGST,
SUM(pt.Sgst)SGST,
SUM(CASE WHEN st.GstCode <> ISNULL(cms.GstCode,24) THEN pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst ELSE 0 END ) AS IgstTaxable,
SUM(CASE WHEN st.GstCode = ISNULL(cms.GstCode,24) THEN pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst ELSE 0 END ) AS CgstTaxable
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

SELECT 
'Inward Supplies' [SupplyType],
'Non GST supply' [Nature of Supplies],
-1*SUM(pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst)TaxableValue,
-1*SUM(pt.Igst)IGST,
-1*SUM(pt.Cgst)CGST,
-1*SUM(pt.Sgst)SGST,
-1*SUM(CASE WHEN st.Id <> cms.Id THEN pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst ELSE 0 END ) AS IgstTaxable,
-1*SUM(CASE WHEN st.Id = cms.Id THEN pt.NetTotal-pt.Cgst-pt.Sgst-pt.Igst ELSE 0 END ) AS CgstTaxable
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
)oth
GROUP BY OTH.SupplyType,OTH.[Nature of Supplies]

END
GO

