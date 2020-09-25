
IF object_id('[dbo].[gstr2_Exempt]') IS NULL 
EXEC ('CREATE PROC [dbo].[gstr2_Exempt] AS SELECT 1 AS Id') 
GO

ALTER PROC [dbo].[gstr2_Exempt]
    @fromdate INT = 20190401 ,
    @todate INT = 20200331 ,
    @companyid INT = 1 ,
	@TransTypeId INT  
AS

    BEGIN
    
SELECT x.SupplyType PDescription, ISNULL(SUM(x.IGSTAmt),0) AS IGSTAmt, ISNULL(SUM(x.CGSTAmt),0) AS CGSTAmt, ISNULL(SUM(x.SGSTAmt),0) AS SGSTAmt, ISNULL(SUM(x.BillAmount),0) AS BillAmount FROM (
SELECT 
'Inter-State supplies' [SupplyType],
SUM(CASE WHEN a.VatTds = 'CMP' THEN pt.NetTotal ELSE 0 END ) AS IGSTAmt,
SUM(CASE WHEN tx.TaxName = 'Nil Rated' THEN pt.NetTotal ELSE 0 END ) AS CGSTAmt,
SUM(CASE WHEN tx.TaxName = 'Non GST' THEN pt.NetTotal ELSE 0 END ) AS SGSTAmt,
SUM(CASE WHEN tx.TaxName <> 'Non GST' AND tx.TaxName <> 'Nil Rated' AND pt.Igst+pt.Cgst+pt.Sgst=0 THEN pt.NetTotal ELSE 0 END ) AS BillAmount

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
WHERE vc.VTypeId = @TransTypeId AND ( (a.VatTds ='CMP' OR tx.TaxName='Nil Rated' OR tx.TaxName='Non GST')) AND 
(
pt.Igst > 0) AND st.GstCode <> ISNULL(cms.GstCode,24) AND 
p.CompID =@companyid  AND p.VoucherDate BETWEEN @fromdate AND @todate

UNION ALL
SELECT 
'Intra-State supplies' [SupplyType],
SUM(CASE WHEN a.VatTds = 'CMP' THEN pt.NetTotal ELSE 0 END ) AS CMPValue,
SUM(CASE WHEN tx.TaxName = 'Nil Rated' THEN pt.NetTotal ELSE 0 END ) AS NilValue,
SUM(CASE WHEN tx.TaxName = 'Non GST' THEN pt.NetTotal ELSE 0 END ) AS NonValue,
SUM(CASE WHEN tx.TaxName <> 'Non GST' AND tx.TaxName <> 'Nil Rated' AND pt.Igst+pt.Cgst+pt.Sgst=0 THEN pt.NetTotal ELSE 0 END ) AS ExempValue

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
WHERE vc.VTypeId = @TransTypeId  AND ( (a.VatTds ='CMP' OR tx.TaxName='Nil Rated' OR tx.TaxName='Non GST')) AND 
(
pt.Igst = 0) AND st.GstCode = ISNULL(cms.GstCode,24) AND 
p.CompID =@companyid  AND p.VoucherDate BETWEEN @fromdate AND @todate ) x GROUP BY  x.[SupplyType]

	
    END
GO

