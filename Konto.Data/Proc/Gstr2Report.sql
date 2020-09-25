IF object_id('[dbo].[Gstr2Report]') IS NULL 
EXEC ('CREATE PROC [dbo].[Gstr2Report] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[Gstr2Report]
    @CompanyId INT ,
	@TransTypeId  INT ,
	@TransTypeId1 INT = 0,
	--@TransTypeId2 INT = 0,
	@FromDate  INT ,
	@ToDate INT,
	@yearid INT,

	@BillType VARCHAR(25) = 'DEBIT NOTE' ,
	@Billag VARCHAR(25) = "SALE"

AS
BEGIN
SELECT ac.VatTds AS Type,ac.GstIn,ac.AccName , 
		   bm.BillNo VNo,
		   isnull(bm.RcdDate,CONVERT(Date,Convert(varchar(8),bm.VoucherDate),112)) Date,
           bm.VoucherNo AS VoucherNo,
           ISNULL(CONVERT(Date,Convert(varchar(8),bm.VoucherDate),112),'')  VoucherDate,

 bm.BillType AS URType, ac.AccName AS Account ,
ISNULL(bkk.AccName,bk.AccName) AS BookName, 
vc.VoucherName, 
bm.BillType,
bm.SpecialNotes AS Reason,
bt.HsnCode, 
 CASE WHEN st.StateName IS NULL THEN stm.GstCode + '-' + stm.StateName ELSE st.GstCode + '-' + st.StateName END StateName,
SUM(bt.NetTotal) - SUM(bt.Sgst) - SUM(bt.Cgst) - SUM(bt.Igst) AS TaxableValue, 
bt.CgstPer , 
SUM(bt.Cgst) AS CgstAmt, 
bt.SgstPer, 
SUM(bt.Sgst) AS SgstAmt, 
bt.IgstPer, 
SUM(bt.Igst) AS IgstAmt,
SUM(bt.NetTotal) AS BillAmount, 
SUM(bt.Cess) AS Cess, 
CASE WHEN ISNULL(bm.BillType,'CREDIT NOTE') = 'DEBIT NOTE' THEN 'D' ELSE 'C' END  AS NoteType , 
CASE WHEN bt.IgstPer = 0 THEN bt.CgstPer + bt.SgstPer ELSE bt.IgstPer END  AS GSTRate,
CASE WHEN bm.BillType = 'B2B Purchase' THEN bm.Rcm ELSE 'N' END  AS Rcm, bm.PortCode,
bm.Rcm AS RevChg,
bm.Itc,
vc.VTypeId, CASE WHEN ISNULL(bm.BillType,'x')='Sale from Bonded WH' THEN 'Y' ELSE 'N' END BondedWH
FROM dbo.BillMain bm
LEFT OUTER JOIN dbo.BillTrans bt ON bt.BillId = bm.Id 
LEFT OUTER JOIN dbo.Acc ac ON ac.Id = bm.AccId
LEFT OUTER JOIN dbo.Acc bk ON bk.Id = bm.BookAcId
LEFT OUTER JOIN dbo.Acc bkk ON bkk.Id = bt.ToAccId
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = bm.VoucherId
LEFT OUTER JOIN dbo.AccBal adr ON adr.AccId = ac.Id
LEFT OUTER JOIN dbo.City ct ON ct.Id = adr.CityId
LEFT OUTER JOIN dbo.State st ON st.Id = ct.StateId
LEFT OUTER JOIN dbo.state stm ON stm.Id = bm.StateId
WHERE bm.CompId = @CompanyId AND bm.IsActive = 1 AND bm.IsDeleted = 0 
AND adr.CompId=@CompanyId AND adr.YearId=@yearid and (bm.VoucherDate between @FromDate and @ToDate) 
AND (vc.VTypeId = @TransTypeId OR vc.VTypeId = @TransTypeId1) AND (ISNULL(bm.Extra1,'PURCHASE')='PURCHASE')

GROUP BY bm.id, ac.VatTds,ac.GstIn, ac.AccName, bm.VoucherNo,bm.VoucherDate ,bk.AccName,bkk.AccName, vc.VoucherName, bm.BillType, bm.SpecialNotes,bt.HsnCode,
bt.CgstPer, bt.SgstPer, bt.IgstPer,st.StateName, bm.BillNo, bm.Rcm, bm.RcdDate , bm.PortCode, bm.Rcm, bm.Itc,st.GstCode,stm.StateName,stm.GstCode,vc.VTypeId

ORDER BY bm.VoucherDate,bm.Id
END
GO

