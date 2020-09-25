IF object_id('[dbo].[ReconsileList]') IS NULL 
EXEC ('CREATE PROC [dbo].[ReconsileList] AS SELECT 1 AS Id') 
GO

ALTER  PROCEDURE [dbo].[ReconsileList]
	 @FromDate int,
	 @ToDate int,
	 @AccId int,
	 @CompId int,
	 @OpBal int=0
AS
BEGIN
select l.Id
,ISNULL(CONVERT(Date,Convert(varchar(8),l.VoucherDate),112),'')  as VoucherDate,
l.VoucherNo,l.RefAccountId,Bac.AccName as Particular,l.Debit,l.Credit,l.ChqNo,l.TransCode 
 
,CONVERT(Date,Convert(varchar(8),bt.BankDate),112) BankDate,CASE WHEN l.Debit>0 THEN l.Debit ELSE (-1)* L.Credit END as Amount,
ac.AccName as BankName 
from LedgerTrans l 
left outer join Acc Bac on Bac.Id=l.AccountId
left outer join Acc ac on ac.Id=l.RefAccountId
left outer join BillTrans bt on bt.RowId=l.TransCode
where l.CompanyId=@CompId  
and ((l.VoucherDate between @FromDate and @ToDate) or(l.VoucherDate<@FromDate and bt.BankDate is null))
and l.RefAccountId=@AccId
and (@OpBal=0 or (bt.BankDate is not null))
order by l.VoucherDate

END
Go
