CREATE PROC [dbo].[PendingLotSp]
@companyid INT=1,
@yearid INT=1
AS
SELECT  ch.VoucherNo IssueNo, ch.VoucherDate ChlnDate, ct.Pcs, CAST(ISNULL(ct.Qty,0) AS NUMERIC(18,2)) AS  Meter,ct.LotNo, ac.AccName MillName, pd.ProductName Quality,ct.Id BalId
FROM    dbo.Challan ch
        LEFT OUTER JOIN dbo.ChallanTrans ct ON ct.ChallanId = ch.Id
        LEFT OUTER JOIN dbo.Acc ac ON ac.Id = ch.AccId
		LEFT OUTER JOIN dbo.Product pd ON pd.Id = ct.ProductId
		LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = ch.VoucherId
        WHERE ct.LotNo IS NULL AND ch.IsDeleted = 0 AND ct.IsDeleted = 0 AND vc.VTypeId = 37 AND  ch.CompId=@companyid AND ch.YearId=@yearid
        ORDER BY ch.VoucherNo


GO

