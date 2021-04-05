IF object_id('[dbo].[GstReportBtoc]') IS NULL 
EXEC ('CREATE PROC [dbo].[GstReportBtoc] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[GstReportBtoc]
    @CompanyId INT ,
	@TransTypeId  INT ,
	@FromDate  INT,
	@ToDate INT 

AS
BEGIN
SELECT ac.VatTds Type,bm.VoucherNo AS VNo, bm.VDate AS Date,  
SUM(bt.NetTotal) AS BillAmount,
st.StateName ,
CASE WHEN bm.BillType = 'Sale from Bonded WH' THEN 'Y' ELSE 'N' END  AS BondedWH, 
CASE WHEN bt.IgstPer = 0 THEN bt.CgstPer + bt.SgstPer ELSE bt.IgstPer END  AS GSTRate,
SUM(bt.NetTotal) - SUM(bt.Sgst) - SUM(bt.Cgst) - SUM(bt.Igst) AS TaxableValue, 
SUM(bt.Cess) AS Cess, 
bt.CgstPer , 
SUM(bt.Cgst) AS CgstAmt, 
bt.SgstPer, 
SUM(bt.Sgst) AS SgstAmt, 
bt.IgstPer, 
SUM(bt.Igst) AS IgstAmt

FROM dbo.BillMain bm
LEFT OUTER JOIN dbo.BillTrans bt ON bt.BillId = bm.Id 
LEFT OUTER JOIN dbo.Acc ac ON ac.Id = bm.AccId
LEFT OUTER JOIN dbo.Acc bk ON bk.Id = bm.BookAcId
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = bm.VoucherId
LEFT OUTER JOIN dbo.AccBal adr ON adr.AccId = ac.Id
LEFT OUTER JOIN dbo.City ct ON ct.Id = adr.CityId
LEFT OUTER JOIN dbo.State st ON st.Id = ct.StateId
WHERE bm.CompId = @CompanyId AND vc.VTypeId = @TransTypeId AND bm.IsActive = 1 AND bm.IsDeleted = 0 AND (bm.VoucherDate between @FromDate and @ToDate)
GROUP BY  ac.VatTds,bm.VoucherNo,bm.VDate ,bt.CgstPer, bt.SgstPer, bt.IgstPer,st.StateName, bm.BillType

END
GO

