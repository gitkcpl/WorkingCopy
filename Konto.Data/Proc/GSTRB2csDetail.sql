IF object_id('[dbo].[GSTRB2csDetail]') IS NULL 
EXEC ('CREATE PROC [dbo].[GSTRB2csDetail] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[GSTRB2csDetail]
    @CompanyId INT =1,
	@FromDate  INT,
	@ToDate INT, 
	@YearId INT,
	@State VarChar(200)
	 
AS
BEGIN

SELECT ac.GSTIn,vc.VoucherName,bm.VoucherNo, bm.VDate VoucherDate, CASE WHEN ac.VatTds='ECOMP' THEN 'E' ELSE 'OE' END [Type],
CASE WHEN bms.StateName IS NULL THEN st.GstCode + '-' + st.StateName ELSE bms.GstCode + '-' + bms.StateName  END StateName,
CASE WHEN ac.VatTds='ECOM' THEN  ac.GstIn ELSE '' END GstIn,
ISNULL(bm.BillNo,bm.VoucherNo) BillNo,ac.AccName Party,pd.ProductCode,pd.ProductName,isnull(bt.HsnCode,pd.HsnCode) HsnCode,
CASE WHEN bt.IgstPer = 0 THEN bt.CgstPer + bt.SgstPer ELSE bt.IgstPer END  AS GSTRate,
ISNULL(bt.Rate,0) Rate,
isnull(bt.Qty,0)  TotalQty,
bt.TdsAmt,
bt.Total GrossAmount,
bt.NetTotal - bt.Sgst - bt.Cgst - bt.Igst AS TaxableValue, 
bt.Igst IgstAmt,
bt.Cgst CgstAmt,
bt.Sgst SgstAmt,
bt.NetTotal TotalAmount,
bt.Cess AS Cess 
,case when bm.RefId>=0 or bm.RefId is not null 
	then 1 
	else 0 end as IsRevice
FROM dbo.BillMain bm
LEFT OUTER JOIN dbo.BillTrans bt ON bt.BillId = bm.Id 
LEFT OUTER JOIN dbo.Product pd ON pd.Id = bt.ProductId 
LEFT OUTER JOIN dbo.Acc ac ON ac.Id = bm.AccId
LEFT OUTER JOIN dbo.Acc bk ON bk.Id = bm.BookAcId
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = bm.VoucherId
LEFT OUTER JOIN dbo.AccBal adr ON adr.AccId = ac.Id
LEFT OUTER JOIN dbo.City ct ON ct.Id = adr.CityId
LEFT OUTER JOIN dbo.State st ON st.Id = ct.StateId
LEFT OUTER JOIN dbo.State bms ON bms.Id = bm.StateId
WHERE bm.CompId = @CompanyId AND ((vc.VTypeId = 12) OR (vc.VTypeId=24 AND isnull(bm.BillType,'X')='DEBIT NOTE' AND ISNULL(bm.Extra1,'X')='SALE'))
AND adr.CompId=@CompanyId AND adr.YearId=@yearid and st.StateName = @State 
 AND bm.IsDeleted = 0 AND bt.IsDeleted=0 AND (bm.VoucherDate between @FromDate and @ToDate)
AND ((bm.TotalAmount <= 250000 and bm.TotalAmount >= 0))  AND (ac.VatTds NOT IN ('REG','CMP'))


UNION ALL 

SELECT ac.GSTIn,vc.VoucherName,bm.VoucherNo, bm.VDate VoucherDate, CASE WHEN ac.VatTds='ECOMP' THEN 'E' ELSE 'OE' END [Type],
CASE WHEN bms.StateName IS NULL THEN st.GstCode + '-' + st.StateName ELSE bms.GstCode + '-' + bms.StateName  END StateName,
CASE WHEN ac.VatTds='ECOM' THEN  ac.GstIn ELSE '' END GstIn,
ISNULL(bm.BillNo,bm.VoucherNo) BillNo,ac.AccName Party,pd.ProductCode,pd.ProductName,isnull(bt.HsnCode,pd.HsnCode) HsnCode,
CASE WHEN bt.IgstPer = 0 THEN bt.CgstPer + bt.SgstPer ELSE bt.IgstPer END  AS GSTRate,
ISNULL(bt.Rate,0) Rate,
isnull(bt.Qty,0)  TotalQty,
-1* bt.TdsAmt,
-1* bt.Total GrossAmount,
-1*(bt.NetTotal - bt.Sgst - bt.Cgst - bt.Igst) AS TaxableValue, 
-1* bt.Igst IgstAmt,
-1* bt.Cgst CgstAmt,
-1* bt.Sgst SgstAmt,
bt.NetTotal TotalAmount,
-1*bt.Cess AS Cess 
,case when bm.RefId>=0 or bm.RefId is not null 
	then 1 
	else 0 end as IsRevice
FROM dbo.BillMain bm
LEFT OUTER JOIN dbo.BillTrans bt ON bt.BillId = bm.Id 
LEFT OUTER JOIN dbo.Product pd ON pd.Id = bt.ProductId 
LEFT OUTER JOIN dbo.Acc ac ON ac.Id = bm.AccId
LEFT OUTER JOIN dbo.Acc bk ON bk.Id = bm.BookAcId
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = bm.VoucherId
LEFT OUTER JOIN dbo.AccBal adr ON adr.AccId = ac.Id
LEFT OUTER JOIN dbo.City ct ON ct.Id = adr.CityId
LEFT OUTER JOIN dbo.State st ON st.Id = ct.StateId
LEFT OUTER JOIN dbo.State bms ON bms.Id = bm.StateId
WHERE bm.CompId = @CompanyId AND ((vc.VTypeId = 19) OR (vc.VTypeId=24 AND isnull(bm.BillType,'X')='CREDIT NOTE' AND ISNULL(bm.Extra1,'X')='SALE'))
 AND bt.IsDeleted=0 AND bm.IsDeleted = 0 AND (bm.VoucherDate between @FromDate and @ToDate)
 AND adr.CompId=@CompanyId AND adr.YearId=@yearid and st.StateName = @State 
AND  ((bm.TotalAmount <= 250000 and bm.TotalAmount >= 0))  AND (ac.VatTds NOT IN ('REG','CMP'))

END
GO

