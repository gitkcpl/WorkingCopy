CREATE PROCEDURE [dbo].[TrialBalanceReport]
(
      @fromdate INT = 20130401 ,
      @todate INT = 20140331 ,
      @companyid INT = 22 ,
      @year INT = 2 ,
      @closingtb VARCHAR(1) = 'Y' ,
      @groupid INT = 0,
	  @showall VARCHAR(1) ='N',
	  @inclop VARCHAR(1) ='Y'
      
    )
AS
BEGIN
DECLARE @finstartdate INT,@startyear int

SELECT @finstartdate = FromDate FROM dbo.FinYear WHERE ID=@year
--PRINT @finstartdate

--SET @startyear = (SELECT TOP 1 ID FROM dbo.FinYear ORDER BY FromDate ASC)
--print @startyear

    SELECT  ac.Id AS AcId,
	        ag.GroupName GroupName ,
            ac.AccName Party ,
            ac.Id AccountID ,
            ac.GroupId GroupId ,
			CASE WHEN ISNULL(op.OpBal,0) + ISNULL(optr.TranAmt,0) > 0 THEN ISNULL(op.OpBal,0)+ISNULL(optr.TranAmt,0) ELSE 0 END AS OpDebit,
			CASE WHEN ISNULL(op.OpBal,0) + ISNULL(optr.TranAmt,0) < 0 THEN -1*(ISNULL(op.OpBal,0)+ ISNULL(optr.TranAmt,0)) ELSE 0 END AS OpCredit,

			--CASE WHEN ISNULL(tr.TranAmt, 0) > 0 THEN ISNULL(tr.TranAmt, 0) ELSE 0 END AS ClDebit,
			--CASE WHEN ISNULL(tr.TranAmt, 0) < 0 THEN -1*ISNULL(tr.TranAmt, 0) ELSE 0 END AS ClCredit,
				ISNULL(tr.Debit,0) ClDebit,
			ISNULL(tr.Credit,0)  ClCredit,

            CASE WHEN ( ISNULL(tr.TranAmt, 0) + ISNULL(op.OpBal,0) +ISNULL(optr.TranAmt,0) ) > 0
                 THEN ISNULL(tr.TranAmt, 0) + ISNULL(op.OpBal,0) +ISNULL(optr.TranAmt,0)
                 ELSE 0
            END Debit ,
            CASE WHEN ( ISNULL(tr.TranAmt, 0) + ISNULL(op.OpBal,0) +ISNULL(optr.TranAmt,0) ) < 0
                 THEN -1 * ( ISNULL(tr.TranAmt, 0) + ISNULL(op.OpBal,0) +ISNULL(optr.TranAmt,0) )
                 ELSE 0
            END Credit,ISNULL(op.[Audit],0) [Audit]
    FROM    dbo.acc ac
            LEFT OUTER JOIN ( SELECT    l.AccountID ,
                                        SUM(l.Debit) Debit ,
                                        SUM(l.Credit) Credit ,
                                        SUM(l.Debit - l.Credit) TranAmt
                              FROM      dbo.LedgerTrans l
                              WHERE     l.IsDeleted = 0 AND l.VoucherId != 0 AND l.CompanyID = @companyid
                                        AND l.VoucherDate BETWEEN @fromdate AND  @todate 
                                        AND @closingtb='Y' AND ISNULL(l.OpBill,'N')='N' 
                                        GROUP BY l.AccountID
                            ) tr ON tr.AccountID = ac.Id

							LEFT OUTER JOIN ( SELECT    l.AccountID ,
                                        SUM(l.Debit) Debit ,
                                        SUM(l.Credit) Credit ,
                                        SUM(l.Debit - l.Credit) TranAmt
                              FROM      dbo.LedgerTrans l
							  LEFT OUTER JOIN dbo.acc ac ON ac.Id = l.AccountID
							  LEFT OUTER JOIN dbo.acgroup ag ON ag.Id = ac.GroupId
                              WHERE     l.IsDeleted = 0 AND l.VoucherId != 0 AND  l.CompanyId = @companyid
                                        AND 
										((l.VoucherDate < @fromdate AND @closingtb='Y' AND @inclOp='Y'  AND  l.VoucherDate >= @finstartdate))
										AND ISNULL(l.OpBill,'N')='N' 

                                        GROUP BY l.AccountID
                            ) optr ON optr.AccountID = ac.Id
							LEFT OUTER JOIN(SELECT o.CompID,
                                                   o.AccId AccountID,
                                                   o.GroupID,
                                                   o.OpDebit OpBalanceDebit,
                                                   o.OpCredit OpBalanceCredit,
                                                   o.Share SharePer,
                                                   o.YearID,
                                                   o.OpBal,
                                                   o.OpBal Balance,o.[Audit] FROM dbo.AccBal o
												   WHERE (@inclOp ='Y' OR @closingtb='N') AND o.CompID=@companyid AND o.YearID=@year
												   )op
							ON op.AccountID = ac.Id

            LEFT OUTER JOIN dbo.acgroup ag ON ag.Id = ac.GroupId
    WHERE   
     ( ( @groupid = 0
              AND ( ISNULL(tr.TranAmt, 0) + ISNULL(op.OpBal,0) + ISNULL(optr.TranAmt,0)) <> 0
			  AND @showall='N'
            )
            OR ( ac.GroupId = @groupid
                 AND ( ISNULL(tr.Debit, 0) > 0
                       OR ISNULL(tr.Credit, 0) > 0
                       OR ISNULL(op.OpBal,0) <> 0
                     )
               ) OR(@showall='Y' AND @groupid=0 AND (ISNULL(tr.TranAmt, 0)<>0 OR  ISNULL(op.OpBal,0)<>0 OR ISNULL(optr.TranAmt,0)<>0) ) ) 

    ORDER BY ag.TbSort,ag.GroupName ,
            ac.AccName;
			END
GO

