IF object_id('[dbo].[GstReportBtocs]') IS NULL 
EXEC ('CREATE PROC [dbo].[GstReportBtocs] AS SELECT 1 AS Id') 
GO

ALTER  PROCEDURE [dbo].[GstReportBtocs]
    @CompanyId INT ,
	@yearid INT,
	@FromDate  INT,
	@ToDate INT 
	
AS
BEGIN
SELECT x.[Type],
x.StateName,x.GstIn ,x.GSTRate, 
x.IsRevice,
SUM(x.TaxableValue)  AS TaxableValue,
SUM(x.Cess) AS Cess
 FROM (
SELECT CASE WHEN ac.VatTds='ECOMP' THEN 'E' ELSE 'OE' END [Type],
CASE WHEN st.StateName IS NULL THEN bms.GstCode + '-' + bms.StateName ELSE st.GstCode + '-' + st.StateName END StateName,
CASE WHEN ac.VatTds='ECOM' THEN  ac.GstIn ELSE '' END GstIn,
CASE WHEN bt.IgstPer = 0 THEN bt.CgstPer + bt.SgstPer ELSE bt.IgstPer END  AS GSTRate,
SUM(bt.NetTotal) - SUM(bt.Sgst) - SUM(bt.Cgst) - SUM(bt.Igst) AS TaxableValue, 
SUM(bt.Cess) AS Cess 
,case when bm.RefId>=0 or bm.RefId is not null 
	then 1 
	else 0 end as IsRevice
FROM dbo.BillMain bm
LEFT OUTER JOIN dbo.BillTrans bt ON bt.BillId = bm.Id 
LEFT OUTER JOIN dbo.Acc ac ON ac.Id = bm.AccId
LEFT OUTER JOIN dbo.Acc bk ON bk.Id = bm.BookAcId
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = bm.VoucherId
LEFT OUTER JOIN dbo.AccBal adr ON adr.AccId = ac.Id
LEFT OUTER JOIN dbo.City ct ON ct.Id = adr.CityId
LEFT OUTER JOIN dbo.State st ON st.Id = ct.StateId
LEFT OUTER JOIN dbo.State bms ON bms.Id = bm.StateId
WHERE bm.CompId = @CompanyId AND ((vc.VTypeId = 12) OR (vc.VTypeId=24 AND isnull(bm.BillType,'X')='DEBIT NOTE' AND ISNULL(bm.Extra1,'X')='SALE'))
AND adr.CompId=@CompanyId AND adr.YearId=@yearid
 AND bm.IsDeleted = 0 AND bt.IsDeleted=0 AND (bm.VoucherDate between @FromDate and @ToDate)
AND ((bm.TotalAmount <= 250000 and bm.TotalAmount >= 0))  AND (ac.VatTds NOT IN ('REG','CMP'))

GROUP BY bt.CgstPer, bt.SgstPer, bt.IgstPer,st.StateName,ac.VatTds, ac.GstIn,st.GstCode,bms.StateName,bms.GstCode ,bm.RefId
UNION ALL 

 SELECT CASE WHEN ac.VatTds='ECOMP' THEN 'E' ELSE 'OE' END [Type],
CASE WHEN st.StateName IS NULL THEN bms.GstCode + '-' + bms.StateName ELSE st.GstCode + '-' + st.StateName END StateName,
CASE WHEN ac.VatTds='ECOM' THEN  ac.GstIn ELSE '' END GstIn,
CASE WHEN bt.IgstPer = 0 THEN bt.CgstPer + bt.SgstPer ELSE bt.IgstPer END  AS GSTRate,
-1*(SUM(bt.NetTotal) - SUM(bt.Sgst) - SUM(bt.Cgst) - SUM(bt.Igst)) AS TaxableValue, 
-1*SUM(bt.Cess) AS Cess 
,case when bm.RefId>=0 or bm.RefId is not null 
	then 1 
	else 0 end as IsRevice
FROM dbo.BillMain bm
LEFT OUTER JOIN dbo.BillTrans bt ON bt.BillId = bm.Id 
LEFT OUTER JOIN dbo.Acc ac ON ac.Id = bm.AccId
LEFT OUTER JOIN dbo.Acc bk ON bk.Id = bm.BookAcId
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = bm.VoucherId
LEFT OUTER JOIN dbo.AccBal adr ON adr.AccId = ac.Id
LEFT OUTER JOIN dbo.City ct ON ct.Id = adr.CityId
LEFT OUTER JOIN dbo.State st ON st.Id = ct.StateId
LEFT OUTER JOIN dbo.State bms ON bms.Id = bm.StateId
WHERE bm.CompId = @CompanyId AND ((vc.VTypeId = 19) OR (vc.VTypeId=24 AND isnull(bm.BillType,'X')='CREDIT NOTE' AND ISNULL(bm.Extra1,'X')='SALE'))
 AND bt.IsDeleted=0 AND bm.IsDeleted = 0 AND (bm.VoucherDate between @FromDate and @ToDate)
 AND adr.CompId=@CompanyId AND adr.YearId=@yearid
AND  ((bm.TotalAmount <= 250000 and bm.TotalAmount >= 0))  AND (ac.VatTds NOT IN ('REG','CMP'))
GROUP BY bt.CgstPer, bt.SgstPer, bt.IgstPer,st.StateName, ac.VatTds, ac.GstIn,st.GstCode,bms.StateName,bms.GstCode,bm.RefId
 
) x
GROUP BY x.StateName, x.GSTRate, x.Type, x.GstIn,x.IsRevice

END
GO
