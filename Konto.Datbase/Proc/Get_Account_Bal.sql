CREATE PROCEDURE dbo.Get_Account_Bal
  @todate INT,
  @accid INT,
  @compid INT,
  @yearid int
AS 
  
  DECLARE 
  
  @opbal NUMERIC(18,2),
  @bal NUMERIC(18,2)
  BEGIN
  SELECT @opbal = ISNULL(ab.OpBal,0) FROM AccBal ab WHERE ab.CompId=@compid AND ab.YearId=@yearid AND ab.AccId=@accid
  
  SELECT @bal = isnull(SUM(lt.Debit-lt.Credit),0) FROM LedgerTrans lt WHERE lt.VoucherDate <=@todate 
  AND lt.AccountId=@accid AND lt.YearId=@yearid AND lt.CompanyId=@compid

  SELECT ISNULL(@bal + @opbal,0) AS Amt
 END

