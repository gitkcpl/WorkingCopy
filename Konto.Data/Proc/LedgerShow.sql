IF object_id('[dbo].[LedgerShow]') IS NULL 
EXEC ('CREATE PROC [dbo].[LedgerShow] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[LedgerShow]
	@companyid BIGINT=0,
	@partyid BIGINT=0,
	@fromdate INT = 0,
	@todate INT = 0,
	@year INT = 0,
	@reportid INT=0,
    @party VARCHAR(1) = 'N',
	@acgroup VARCHAR(1) = 'N'
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	DECLARE @finstartdate INT,
            @startyear INT;

    SELECT @finstartdate = FromDate
    FROM dbo.FinYear
    WHERE ID = @year;


    SET @startyear =
    (
        SELECT TOP 1 ID FROM dbo.FinYear ORDER BY FromDate ASC
    );
	
	SET NOCOUNT ON;
	
		SELECT b.CompanyID,
           b.AccId,
           b.Particular,
           b.DebitAmt,
           b.CreditAmt,
           b.OpBalance,
           b.FinYear,
           b.FinMonth,
           b.AccId,
           CASE WHEN  b.Diff >0 THEN FORMAT(b.Diff,'N') + ' Dr' ELSE FORMAT(-1*b.Diff,'N') + ' Cr' END Diff ,
           b.Amount,
            CASE WHEN  b.Amount >0 THEN FORMAT(b.Amount,'N') + ' Dr' ELSE FORMAT(-1*b.Amount,'N') + ' Cr' END BalanceAmt ,
           b.AccName FROM(

	SELECT A.CompanyId,
	       A.AccountID AccId,
	       A.Particular,
	       ISNULL(SUM(Debit), 0) [DebitAmt],
	       ISNULL(SUM(Credit), 0) [CreditAmt],
	       ISNULL(SUM(A.Balance), 0)[OpBalance],
	       A.FinYear,
	       A.FinMonth,
		   SUM(SUM(ISNULL(a.Debit,0))-SUM(ISNULL(a.Credit,0))) OVER (PARTITION BY A.AccountID) Diff   ,
		   SUM(SUM(ISNULL(a.Debit,0))-SUM(ISNULL(a.Credit,0))) OVER (PARTITION BY A.AccountID ORDER BY a.FinYear,a.FinMonth) [Amount],
		  A.AccName
	FROM   (
         
	           SELECT op.CompId CompanyID,
	                  op.AccId AccountID,
	                  'Op Balance'[Particular],
	                  op.OpDebit [Debit],
	                  op.OpCredit [Credit],
	                  0 [Balance],
	                  0[FinYear],
	                  0[FinMonth],
					  0[Amount],ac.AccName
	           FROM   dbo.AccBal op
			    INNER JOIN acc AS Ac  ON  op.AccID = Ac.Id
	            INNER JOIN acgroup AS Acg  ON  Acg.Id = op.GroupId
	                                        
	           WHERE  (
			        
	                      CompID = @companyid
						  AND op.YearId = @startyear
	                  )  

					   AND
					   ((
                  @party = 'N'
                  AND
                  (
                      @partyid = 0
                      OR op.AccId = @partyid
                  )
              )
              OR (EXISTS
    (
        SELECT 1
        FROM dbo.ReportPara rs
        WHERE rs.ReportId = @reportid
              AND op.AccId = rs.ParameterValue
              AND rs.ParameterName = 'party'
    )))
	 AND
          (
              @acgroup = 'N'
              OR EXISTS
    (
        SELECT 1
        FROM dbo.ReportPara rs
        WHERE rs.ReportId = @reportid
              AND acg.Id = rs.ParameterValue
              AND rs.ParameterName = 'acgroup'
    ))

	           UNION ALL

	           -- op From Transaction
	           SELECT  l.CompanyId ,
	                  l.AccountID,
	                  'Op Balance',
	                  ISNULL(SUM(l.Debit), 0),
	                  ISNULL(SUM(l.Credit), 0),
	                  0,
	                  0[FinYear],
	                  0[FinMonth],
					  ISNULL(SUM(l.Amount),0),ac.AccName
	           FROM   dbo.LedgerTrans l
			        INNER JOIN acc AS ac  ON  l.AccountID = ac.Id
					LEFT OUTER JOIN dbo.AccBal acb ON acb.AccId =l.AccountId
              AND L.CompanyId = acb.CompId AND l.yearid = acb.yearid
	                INNER JOIN acgroup AS acg ON  acg.Id = acb.GroupId
	           WHERE  l.CompanyID = @companyid AND l.yearid=@year AND l.IsDeleted = 0
	                 
	                  AND ((L.VoucherDate < @fromdate AND (acg.Nature ='ASSETS' OR acg.Nature = 'LIABILITIES'))
					  OR (L.VoucherDate < @fromdate AND L.VoucherDate>=@finstartdate)
					  )
					  AND
					   ((
                  @party = 'N'
                  AND
                  (
                      @partyid = 0
                      OR L.AccountId = @partyid
                  )
              )
              OR (EXISTS
    (
        SELECT 1
        FROM dbo.ReportPara rs
        WHERE rs.ReportId = @reportid
              AND L.AccountId = rs.ParameterValue
              AND rs.ParameterName = 'party'
    )))
	 AND
          (
              @acgroup = 'N'
              OR EXISTS
    (
        SELECT 1
        FROM dbo.ReportPara rs
        WHERE rs.ReportId = @reportid
              AND acg.Id = rs.ParameterValue
              AND rs.ParameterName = 'acgroup'
    ))
	                  
	           GROUP BY
	                  l.CompanyId,
	                  l.AccountId,ac.AccName


	           -- Closing From Transaction

	           UNION ALL

	           SELECT  l.CompanyID,
	                  l.AccountID,
	                  (
							dbo.keyMonthName(MONTH(CONVERT(Date,Convert(varchar(8),l.VoucherDate),112))) + ' - ' + CAST(YEAR(CONVERT(Date,Convert(varchar(8),l.VoucherDate),112)) AS VARCHAR(4))
						   
	                  ),
	                  ISNULL(SUM(l.Debit), 0),
	                  ISNULL(SUM(l.Credit), 0),
	                  0,
	                  YEAR(CONVERT(Date,Convert(varchar(8),l.VoucherDate),112)),
	                  MONTH(CONVERT(Date,Convert(varchar(8),l.VoucherDate),112)),
					  ISNULL(SUM(l.Amount),0),ac.AccName
	           FROM   dbo.LedgerTrans l
			   LEFT OUTER JOIN dbo.Acc ac ON ac.Id = l.AccountId
              	LEFT OUTER JOIN dbo.AccBal ab ON ab.AccId =l.AccountId
              AND L.CompanyId = ab.CompId AND l.yearid = ab.yearid
			       WHERE  l.CompanyID = @companyid AND l.yearid=@year and l.IsDeleted = 0
	                --  AND l.AccountID = @partyid
	                  AND l.VoucherDate BETWEEN @fromdate AND @todate
					 
					  AND
					   ((
                  @party = 'N'
                  AND
                  (
                      @partyid = 0
                      OR L.AccountId = @partyid
                  )
              )
              OR (EXISTS
    (
        SELECT 1
        FROM dbo.ReportPara rs
        WHERE rs.ReportId = @reportid
              AND L.AccountId = rs.ParameterValue
              AND rs.ParameterName = 'party'
    )))
	 AND
          (
              @acgroup = 'N'
              OR EXISTS
    (
        SELECT 1
        FROM dbo.ReportPara rs
        WHERE rs.ReportId = @reportid
              AND ab.GroupId = rs.ParameterValue
              AND rs.ParameterName = 'acgroup'
    ))

	           GROUP BY
	                   YEAR(CONVERT(Date,Convert(varchar(8),l.VoucherDate),112)),
	                  MONTH(CONVERT(Date,Convert(varchar(8),l.VoucherDate),112)),
	                  L.CompanyId,
	                  l.AccountId,ac.AccName
	       )A
	GROUP BY
	       A.CompanyID,
	       A.AccountID,
	       A.Particular,
	       A.FinYear,
	       A.FinMonth,A.AccName
	
		   )b
		   ORDER BY
		 b.AccName,
	       b.FinYear,
	       b.FinMonth

END
GO

