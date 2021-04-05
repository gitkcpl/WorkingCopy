
IF object_id('[dbo].[ledger_audit]') IS NULL 
EXEC ('CREATE PROC [dbo].[ledger_audit] AS SELECT 1 AS Id') 
GO 

ALTER PROC dbo.ledger_audit
@fromdate INT,
@todate INT,
@compid int
AS
BEGIN

	SELECT lt.RefId,v.VoucherName,lt.BillNo,
	CONVERT(Date,Convert(varchar(8),lt.VoucherDate),112) VoucherDate,
	SUM(lt.Debit)Debit,SUM(lt.Credit)Credit,SUM(lt.Debit-lt.Credit) Diff,v.VTypeId FROM dbo.LedgerTrans lt
	LEFT OUTER JOIN dbo.Voucher v ON v.Id =lt.VoucherId
	WHERE isnull(lt.RefAccountId,0)<>0 AND lt.VoucherDate BETWEEN @fromdate AND @todate
	AND lt.CompanyId=@compid
	GROUP BY lt.RefId,v.VoucherName,lt.BillNo,lt.VoucherDate,v.VTypeId
	HAVING SUM(lt.Debit)<> SUM(lt.Credit) 
	


END
GO
