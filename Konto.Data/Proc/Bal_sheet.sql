IF object_id('[dbo].[Bal_sheet]') IS NULL 
EXEC ('CREATE PROC [dbo].[Bal_sheet] AS SELECT 1 AS Id') 
GO 

ALTER PROC [dbo].[Bal_sheet]
    @fromdate INT = 20190401,
    @todate INT = 20200331,
    @companyid INT =1,
    @yearid INT = 2,
	@summary VARCHAR(1)='Y'
AS
BEGIN

    DECLARE @finstartdate INT,
        @startyear INT,
		@prevstartdate INT,
		@prevenddate INT,
		@preyearid int

    SET NOCOUNT ON;

    SELECT @finstartdate = FromDate
    FROM dbo.FinYear
    WHERE ID = @yearid;
    --PRINT @finstartdate

	SELECT TOP 1 @prevstartdate = fromdate,@prevenddate=@todate,@yearid = @preyearid FROM dbo.FinYear
	WHERE FromDate < @finstartdate ORDER BY FromDate DESC
    
	IF @summary='Y'
	BEGIN
    
	IF EXISTS(SELECT 1 FROM sys.objects WHERE name='tmp_bal' AND type_desc='USER_TABLE')
	 DROP TABLE tmp_bal;

	SELECT * INTO tmp_bal
	FROM (
    SELECT ag.BlSort,ag.GroupName Acgroup,
           ag.Nature,
           ag.Id AcgroupId,
           ABS(SUM(ISNULL(l.Bal, 0) + ISNULL(op.OpBal, 0) + ISNULL(lp.Bal, 0))) Diff,
           SUM(ISNULL(l.Bal, 0) + ISNULL(op.OpBal, 0) + ISNULL(lp.Bal, 0)) Bal
		   
    FROM dbo.acc ac
    INNER JOIN
					(
					SELECT oc.AccId AccountID, oc.OpBal,OC.GroupId
					FROM dbo.AccBal oc
					WHERE oc.CompID = @companyid AND oc.YearID = @yearid
				) op ON ac.Id = op.AccountID
    LEFT OUTER JOIN
				(
				SELECT lp.AccountID, SUM(lp.Debit) - SUM(lp.Credit) AS Bal
				FROM dbo.LedgerTrans lp
                INNER JOIN dbo.acc a1 ON a1.Id = lp.AccountID
				LEFT OUTER JOIN dbo.acgroup ag ON ag.Id = a1.GroupId
				WHERE lp.IsDeleted = 0 AND ISNULL(lp.OpBill, 'N') = 'N' AND lp.VoucherDate >=@finstartdate and lp.VoucherDate < @fromdate AND ag.Nature ='BL'
                  AND lp.CompanyID = @companyid --AND lp.referenceaccountid <> 0
				GROUP BY lp.AccountID
				) lp ON lp.AccountID = ac.Id
    LEFT OUTER JOIN dbo.acgroup ag ON ag.Id = op.GroupId
    LEFT OUTER JOIN
					(
					SELECT ld.AccountID, SUM(ld.Debit - ld.Credit) Bal
					FROM dbo.LedgerTrans ld
					WHERE ld.IsDeleted = 0 AND ld.CompanyID = @companyid AND ld.VoucherDate BETWEEN @fromdate AND @todate
					AND ISNULL(ld.OpBill, 'N') = 'N' 
					GROUP BY ld.AccountID
					) l ON l.AccountID = op.AccountID
    GROUP BY ag.GroupName, ag.Nature, ag.Id, ag.BlSort
    HAVING SUM(ISNULL(l.Bal, 0) + ISNULL(op.OpBal,0) + ISNULL(lp.Bal,0) ) <> 0
	)bl

	--- genrate data for balasheet summmary
	

	SELECT * FROM (
	SELECT ISNULL(t1.BlSort,t2.BlSort) BlSort,t1.AcgroupId LGroupId, t1.Acgroup LGroup,t1.Nature L,t1.Bal LBal,t1.Diff LDiff
	,t2.AcgroupId RGroupId, t2.Acgroup RGroup,t2.Nature RNature,t2.Bal RBal,t2.Diff RDiff,'TR' AS BalType FROM(

	SELECT BlSort,AcgroupId,Diff, AcGroup,Nature,bal,ROW_NUMBER() OVER (ORDER BY AcGroup) row1 FROM dbo.tmp_bal
	WHERE nature IN ('TRADING EXPENSE','TRADING INCOME') and  bal > 0
	)t1
	FULL OUTER JOIN (
	SELECT BlSort,AcgroupId,Diff, AcGroup,Nature,bal,ROW_NUMBER() OVER (ORDER BY AcGroup) row2 FROM dbo.tmp_bal
	WHERE nature in ('TRADING EXPENSE','TRADING INCOME') AND bal <0 )t2 ON t1.row1 = t2.row2
	
	UNION ALL
    
	SELECT ISNULL(t1.BlSort,t2.BlSort) BlSort,t1.AcgroupId LGroupId, t1.Acgroup LGroup,t1.Nature L,t1.Bal LBal,t1.Diff LDiff
	,t2.AcgroupId RightGroupId, t2.Acgroup RightGroup,t2.Nature RNature,t2.Bal RBal,t2.Diff RDiff,'PL' AS BalType FROM(

	SELECT BlSort,AcgroupId,Diff, AcGroup,Nature,bal,ROW_NUMBER() OVER (ORDER BY AcGroup) row1 FROM dbo.tmp_bal
	WHERE nature IN ( 'EXPENSE','INCOME') AND Bal >0
	)t1
	FULL OUTER JOIN (
	SELECT BlSort,AcgroupId,Diff, AcGroup,Nature,bal,ROW_NUMBER() OVER (ORDER BY AcGroup) row2 FROM dbo.tmp_bal
	WHERE nature IN ('EXPENSE', 'INCOME' ) AND bal <0)t2 ON t1.row1 = t2.row2
	UNION all
	SELECT ISNULL(t1.BlSort,t2.BlSort) BlSort,t1.AcgroupId LGroupId, t1.Acgroup LGroup,t1.Nature L,t1.Bal LBal,t1.Diff LDiff
	,t2.AcgroupId RightGroupId, t2.Acgroup RightGroup,t2.Nature RNature,t2.Bal RBal,t2.Diff RDiff,'BL' AS BalType FROM(

	SELECT BlSort,AcgroupId,Diff, AcGroup,Nature,bal,ROW_NUMBER() OVER (ORDER BY AcGroup) row1 FROM dbo.tmp_bal
	WHERE nature  IN ( 'LIABILITIES','ASSETS') AND bal < 0
	)t1
	FULL OUTER JOIN (
	SELECT BlSort,AcgroupId,Diff, AcGroup,Nature,bal,ROW_NUMBER() OVER (ORDER BY AcGroup) row2 FROM dbo.tmp_bal
	WHERE Nature IN  ( 'LIABILITIES','ASSETS') and bal > 0 )t2 ON t1.row1 = t2.row2 ) x ORDER BY x.BlSort


	END
    ELSE
    BEGIN

	SELECT ag.BlSort,ag.GroupName Acgroup,
           ag.Nature,
           ag.Id AcgroupId,ac.AccName AccountName,
           ABS(SUM(ISNULL(l.Bal, 0) + ISNULL(op.OpBal, 0) + ISNULL(lp.Bal, 0))) Diff,
           SUM(ISNULL(l.Bal, 0) + ISNULL(op.OpBal, 0) + ISNULL(lp.Bal, 0)) Bal,
		   CASE WHEN ag.Nature='TRADING EXPENSE' OR ag.Nature = 'TRADING INCOME' THEN 0
		   WHEN ag.Nature = 'EXPENSE' OR ag.Nature = 'INCOME' THEN 1
		   WHEN ag.Nature = 'LIABILITIES' OR ag.Nature = 'ASSETS' THEN 3
		   ELSE 
		   4 END AS TransType,
			CASE   WHEN ag.Nature = 'TRADING EXPENSE' OR ag.Nature = 'TRADING INCOME' OR ag.Nature = 'EXPENSE' OR ag.Nature = 'INCOME' THEN 0	
		   ELSE 
		   1 END AS TransType1,
		   op.[Audit],
		   ac.Id AcId,
		   ISNULL(pre.Diff ,0) PreDiff,ISNULL(pre.Bal,0) PreBal
    FROM dbo.acc ac
    INNER JOIN
					(
						SELECT oc.AccId AccountID, oc.OpBal,oc.Audit,oc.GroupId
						FROM dbo.AccBal oc
						WHERE oc.CompID = @companyid AND oc.YearID = @yearid
					) op ON ac.Id = op.AccountID
    LEFT OUTER JOIN
					 (
						SELECT lp.AccountID,SUM(lp.Debit) - SUM(lp.Credit) AS Bal
						FROM dbo.LedgerTrans lp
						INNER JOIN dbo.acc a1 ON a1.Id = lp.AccountID
						LEFT OUTER JOIN dbo.acgroup ag ON ag.Id = a1.GroupId
						WHERE lp.IsDeleted = 0 AND ISNULL(lp.OpBill, 'N') = 'N' AND (ag.Nature='LIABILITIES' OR ag.Nature= 'ASSETS') and lp.VoucherDate < @fromdate  AND lp.voucherdate> =@finstartdate
						AND lp.CompanyID = @companyid --AND lp.referenceaccountid <> 0
						GROUP BY lp.AccountID
					) lp ON lp.AccountID = ac.Id
    LEFT OUTER JOIN dbo.acgroup ag ON ag.Id = op.GroupId
    LEFT OUTER JOIN
					(
						SELECT ld.AccountID, SUM(ld.Debit - ld.Credit) Bal
						FROM dbo.LedgerTrans ld INNER JOIN dbo.acc a1 ON a1.Id = ld.AccountID
						LEFT OUTER JOIN dbo.acgroup ag ON ag.Id = A1.GroupId
						WHERE ld.IsDeleted = 0 AND ld.CompanyID = @companyid AND ld.VoucherDate BETWEEN @fromdate AND @todate 
						AND ISNULL(ld.OpBill, 'N') = 'N' 
						GROUP BY ld.AccountID
					) l ON l.AccountID = op.AccountID
					LEFT OUTER JOIN (
					
					SELECT 
           ABS(SUM(ISNULL(l.Bal, 0) + ISNULL(op.OpBal, 0) )) Diff,
           SUM(ISNULL(l.Bal, 0) + ISNULL(op.OpBal, 0) ) Bal,
		   ac.Id AcId
    FROM dbo.acc ac
   INNER JOIN
					(
						SELECT oc.AccId AccountID, oc.OpBal,oc.Audit,oc.GroupId
						FROM dbo.AccBal oc
						WHERE oc.CompID = @companyid AND oc.YearID = @preyearid
					) op ON ac.Id = op.AccountID
   
    LEFT OUTER JOIN dbo.acgroup ag ON ag.Id = op.GroupId
    LEFT OUTER JOIN
					(
						SELECT ld.AccountID, SUM(ld.Debit - ld.Credit) Bal
						FROM dbo.LedgerTrans ld INNER JOIN dbo.acc a1 ON a1.Id = ld.AccountID
						LEFT OUTER JOIN dbo.acgroup ag ON ag.Id = A1.GroupId
						WHERE ld.IsDeleted = 0 AND ld.CompanyID = @companyid AND ld.VoucherDate BETWEEN @prevstartdate AND @prevenddate
						AND ISNULL(ld.OpBill, 'N') = 'N' 
						GROUP BY ld.AccountID
					) l ON l.AccountID = op.AccountID
    GROUP BY ag.GroupName, ag.Id,ac.AccName,ag.Nature,op.Audit,ac.Id, ag.BlSort
    HAVING SUM(ISNULL(l.Bal, 0) + ISNULL(op.OpBal,0) ) <> 0
	)pre ON pre.AcId = ac.Id

    GROUP BY ag.GroupName, ag.Id,ac.AccName,ag.Nature,op.Audit,ac.Id,pre.Diff,pre.Bal, ag.BlSort
    HAVING SUM(ISNULL(l.Bal, 0) + ISNULL(op.OpBal,0) + ISNULL(lp.Bal,0) ) <> 0
	ORDER BY ag.BlSort,ag.Nature,ag.GroupName,ac.AccName

	
    END
END;
GO

