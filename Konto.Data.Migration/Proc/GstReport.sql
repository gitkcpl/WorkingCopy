IF object_id('[dbo].[GstReport]') IS NULL 
EXEC ('CREATE PROC [dbo].[GstReport] AS SELECT 1 AS Id') 
GO
ALTER  PROCEDURE [dbo].[GstReport]
    @CompanyId INT ,
	@FromDate  INT ,
	@ToDate INT,
	@yearid int 
AS
BEGIN
SELECT ISNULL(ac.VatTds,'NA') AS Type,isnull(ac.GstIn,'')GstIn,ac.AccName , 
			revice.OrgBillDate,revice.OrgBillNo,
		   bm.BillNo InvoiceNo,
		  -- ISNULL(CONVERT(Date,Convert(varchar(8),brr.VoucherDate),112),'') InvoiceDate,
		   ISNULL(bm.RcdDate,GETDATE()) AS InvoiceDate,
           bm.VoucherNo AS VoucherNo,
           ISNULL(CONVERT(Date,Convert(varchar(8),bm.VoucherDate),112),'')  VoucherDate,

 bm.BillType AS URType, ac.AccName AS Account ,
ISNULL(bkk.AccName,bk.AccName) AS BookName, 
vc.VoucherName, 
bm.BillType,
bm.SpecialNotes AS Reason,
 CASE WHEN stm.StateName IS NULL THEN st.GstCode + '-' + st.StateName ELSE stm.GstCode + '-' + stm.StateName END StateName,
isnull(SUM(bt.NetTotal),0) - isnull(SUM(bt.Sgst),0) - isnull(SUM(bt.Cgst),0) - isnull(SUM(bt.Igst),0) AS TaxableValue, 
isnull(bt.CgstPer ,0)CgstPer  , 
isnull(SUM(bt.Cgst),0) AS CgstAmt, 
isnull(bt.SgstPer,0)SgstPer, 
isnull(SUM(bt.Sgst),0) AS SgstAmt, 
isnull(bt.IgstPer,0)IgstPer, 
isnull(SUM(bt.Igst) ,0)AS IgstAmt,
bm.TotalAmount- ISNULL(bm.RoundOff,0) AS BillAmount, 
isnull(SUM(bt.Cess),0) AS Cess, 
CASE WHEN ISNULL(bm.BillType,'CREDIT NOTE') = 'DEBIT NOTE' THEN 'D' ELSE 'C' END  AS NoteType , 
CASE WHEN bt.IgstPer = 0 THEN isnull(bt.CgstPer,0) + isnull(bt.SgstPer,0) ELSE isnull(bt.IgstPer,0) END  AS GSTRate,
CASE WHEN bm.BillType = 'B2B Purchase' THEN bm.Rcm ELSE 'N' END  AS Rcm, bm.PortCode,
bm.Rcm AS RevChg,
bm.Itc,
vc.VTypeId, CASE WHEN ISNULL(bm.BillType,'x')='Sale from Bonded WH' THEN 'Y' ELSE 'N' END BondedWH
,case when bm.RefId>=0 or bm.RefId is not null 
	then 1 
	else 0 end as IsRevice,Revice.OrgStateName,bm.Id
FROM dbo.BillMain bm
LEFT OUTER JOIN dbo.BillTrans bt ON bt.BillId = bm.Id 
LEFT OUTER JOIN( select rev.Id OrgBillId,rev.VoucherNo OrgBillNo,
				convert(date,convert(varchar(8), rev.VoucherDate),112) OrgBillDate,
				st.StateName as OrgStateName
				from BillMain rev 
				LEFT OUTER JOIN dbo.Acc ac ON ac.Id = rev.AccId
LEFT OUTER JOIN dbo.AccBal adr ON adr.AccId = ac.Id
LEFT OUTER JOIN dbo.City ct ON ct.Id = adr.CityId
LEFT OUTER JOIN dbo.State st ON st.Id = ct.StateId
WHERE rev.IsActive=1 AND rev.IsDeleted=0
				--where rev.IsActive =1 and rev.IsDeleted=0
		)Revice on Revice.OrgBillId=bm.RefId

LEFT OUTER JOIN dbo.Acc ac ON ac.Id = bm.AccId
LEFT OUTER JOIN dbo.Acc bk ON bk.Id = bm.BookAcId
LEFT OUTER JOIN dbo.Acc bkk ON bkk.Id = bt.ToAccId
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = bm.VoucherId
LEFT OUTER JOIN dbo.AccBal adr ON adr.AccId = ac.Id
LEFT OUTER JOIN dbo.City ct ON ct.Id = adr.CityId
LEFT OUTER JOIN dbo.State st ON st.Id = ct.StateId
LEFT OUTER JOIN dbo.state stm ON stm.Id = bm.StateId
WHERE bm.CompId = @CompanyId AND bm.IsActive = 1 AND bm.IsDeleted = 0 AND bt.IsDeleted=0
AND adr.CompId=@CompanyId AND adr.YearId=@yearid
and (bm.VoucherDate between @FromDate and @ToDate)
AND (vc.VTypeId IN (12,19) OR  (vc.VTypeId=24 and ISNULL(bm.Extra1,'NA')='SALE'))
AND (ISNULL(ac.VatTds,'NA') IN ('REG','CMP') OR bm.TotalAmount >= 250000
or isnull(bm.BillType,'XX') NOT IN ('Regular','XX') )
--and ac.GstIn is not null

GROUP BY bm.id, ac.VatTds,ac.GstIn, ac.AccName, bm.VoucherNo,bm.VoucherDate ,
bk.AccName,bkk.AccName, vc.VoucherName, bm.BillType, bm.SpecialNotes,
bt.CgstPer, bt.SgstPer, bt.IgstPer,st.StateName, bm.BillNo, bm.Rcm, bm.RcdDate ,
bm.PortCode, bm.Rcm, bm.Itc,st.GstCode,stm.StateName,stm.GstCode,vc.VTypeId,
Revice.OrgBillDate ,Revice.OrgBillNo, bm.RefId,Revice.OrgStateName, bm.TotalAmount,
bm.RoundOff
ORDER BY bm.VoucherDate,bm.Id

END


GO

