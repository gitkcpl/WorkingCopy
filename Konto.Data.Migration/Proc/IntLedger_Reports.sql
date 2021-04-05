IF object_id('[dbo].[IntLedger_Reports]') IS NULL 
EXEC ('CREATE PROC [dbo].[IntLedger_Reports] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[IntLedger_Reports]
   @fromdate INT = 20190401,
    @todate INT = 20200331,
    @companyid INT = 1,
	  @party VARCHAR(1) = 'N',
    @reportid INT = 0,
    @acgroup VARCHAR(1) = 'N',
  @yearid INT = 1,
      @partygroupid INT = 0,
	  @branchid INT = 0,
    @partyid  INT = 0,
    @IntPer INT = 12,
    @Days INT = 12,
    @TotalDays INT = 365 
	as
 
DECLARE @finstartdate INT;
       
SELECT @finstartdate = FromDate
FROM dbo.FinYear
WHERE Id = @yearid;
 
  CREATE TABLE #mydays(vTypeId INT,[Days] INT)
  
  INSERT INTO #mydays SELECT vt.Id,
  CASE WHEN vt.id IN (14,15,16,24) THEN 0 ELSE @Days end FROM VoucherType AS vt 
  
SELECT  CAST(0 AS BIT) IsSelected,xx.TransDate,
       xx.VoucherNo,
       xx.BillNo,
       xx.Voucher,
       xx.Particular,
	   xx.OpBal,
       xx.DebitAmt,
	     DATEDIFF(DAY, DATEADD(DAY, xx.Dys, xx.transdate),convert(date,convert(varchar(8),@todate),112)) Days,
	     xx.CreditAmt,
		 xx.CreditInt,
		 xx.DebitInt,
	  --  (xx.DebitAmt*@IntPer *(DATEDIFF(DAY, DATEADD(DAY, @Days,xx.transdate), convert(date,convert(varchar(8),@todate),112))))/(100 *@TotalDays ) DebitInt,
     
	  --    (xx.CreditAmt*@IntPer*(DATEDIFF(DAY, DATEADD(DAY, @Days,xx.transdate), convert(date,convert(varchar(8),@todate),112))))/(100 *@TotalDays ) CreditInt, 
		 
       CASE
           WHEN xx.Amount > 0 THEN
               FORMAT(xx.Amount, 'N') + ' Dr'
           ELSE
               FORMAT(-1 * xx.Amount, 'N') + ' Cr'
       END BalanceAmt,

	    CASE
           WHEN xx.IntAmt > 0 THEN
               FORMAT(xx.IntAmt , 'N') + ' Dr'
           ELSE
               FORMAT(-1 *( xx.IntAmt), 'N') + ' Cr'
       END IntAmt ,

       CASE
           WHEN xx.Bal > 0 THEN
               FORMAT(xx.Bal, 'N') + ' Dr'
           ELSE
               FORMAT(-1 * xx.Bal, 'N') + ' Cr'
       END Bal,
       CASE
           WHEN xx.Bal > 0 THEN
               FORMAT(xx.Bal
			   +
			   ((xx.Amount *@IntPer*(DATEDIFF(DAY, DATEADD(DAY, xx.Dys, xx.transdate),convert(date,convert(varchar(8),@todate),112))))/(100 *@TotalDays ))
			   , 'N') + ' Dr'
           ELSE
               FORMAT(-1 * (xx.Bal
			   +((xx.Amount *@IntPer*(DATEDIFF(DAY, DATEADD(DAY, xx.Dys, xx.transdate),convert(date,convert(varchar(8),@todate),112))))/(100 *@TotalDays ))
			   ), 'N') + ' Cr'
       END   
	   TotalBal,
	 
       xx.Amount,
       xx.Narration,
       xx.Chequeno,
       xx.AccountName,
       xx.flag,
       xx.Id,
       xx.CompanyId,
       xx.CompanyName,
       xx.ReferenceAccountId,
       xx.VTypeId,
       xx.AccountId,
       xx.Remarks,
       xx.VoucherID,
       xx.AcGroup,
       xx.Address1,
       xx.Address2,
       xx.GstIn,
       xx.PanNo,
       xx.Email,
       xx.MobileNo,
       xx.PinCode,
       xx.AreaName,
       xx.CityName,
       xx.StateName,
       xx.Agent,
       xx.VoucherDate,
	   xx.Audit
FROM
(
    SELECT x.TransDate,
           x.VoucherNo,
           x.BillNo,
           x.Voucher,
           x.Particular,
		   x.OpBal,
           x.DebitAmt,
           x.CreditAmt,
		      (x.DebitAmt*@IntPer *(DATEDIFF(DAY, DATEADD(DAY, x.Dys,x.transdate), convert(date,convert(varchar(8),@todate),112))))/(100 *@TotalDays ) DebitInt,
			  (x.CreditAmt*@IntPer *(DATEDIFF(DAY, DATEADD(DAY, x.Dys,x.transdate), convert(date,convert(varchar(8),@todate),112))))/(100 *@TotalDays ) creditInt,
           SUM(ISNULL(DebitAmt, 0) - ISNULL(CreditAmt, 0)) OVER (PARTITION BY x.AccountId) Bal,
           SUM(ISNULL(DebitAmt, 0) - ISNULL(CreditAmt, 0)) OVER (PARTITION BY x.AccountId ORDER BY  x.flag, x.VoucherDate, x.Id) [Amount],

		     SUM(ISNULL((x.DebitAmt*@IntPer *(DATEDIFF(DAY, DATEADD(DAY, x.Dys,x.transdate), convert(date,convert(varchar(8),@todate),112))))/(100 *@TotalDays ), 0)
			  -
			   ISNULL((x.CreditAmt*@IntPer *(DATEDIFF(DAY, DATEADD(DAY, x.Dys,x.transdate), convert(date,convert(varchar(8),@todate),112))))/(100 *@TotalDays ), 0))
			    OVER (PARTITION BY x.AccountId ORDER BY  x.flag, x.VoucherDate, x.Id) [IntAmt],
			  
           x.Narration,
           x.Chequeno,
           x.AccountName,
           x.flag,
           x.Id,
           x.CompanyId,
           x.CompanyName,
           x.ReferenceAccountId,
           x.VTypeId,
           x.AccountId,
           x.Remarks,
           x.VoucherID,
           x.AcGroup,
           x.Address1,
           x.Address2,
           x.GstIn,
           x.PanNo,
           x.Email,
           x.MobileNo,
           x.PinCode,
           x.AreaName,
           x.CityName,
           x.StateName,
           x.Agent,
           x.VoucherDate,x.KeyFldValue,x.Audit,x.RefRowID,x.Dys
    FROM
    (
        SELECT 0 AS flag,
               op.Id AS Id,
               op.CompId CompanyId,
               c.CompName CompanyName,
               0 ReferenceAccountId,
               'Op Balance' AS Particular,
			   op.OpBal + ISNULL(l.Bal, 0) OpBal,
               CASE
                   WHEN op.OpBal + ISNULL(l.Bal, 0) > 0 THEN
                       op.OpBal + ISNULL(l.Bal, 0)
                   ELSE
                       0
               END AS DebitAmt,
               CASE
                   WHEN op.OpBal + ISNULL(l.Bal, 0) < 0 THEN
                       -1 * (op.OpBal + ISNULL(l.Bal, 0))
                   ELSE
                       0
               END AS CreditAmt,
               'OP' AS VoucherNo,
               'OP' AS BillNo,
               @fromdate VoucherDate,
               'Op' Voucher,
               0 AS VTypeId,
               op.AccId AccountId,
               a.AccName AS AccountName,
               ag.AccName Agent,
               NULL Narration,
               ISNULL(CONVERT(DATE, CONVERT(VARCHAR(8), @fromdate), 112), '') TransDate,
               NULL Remarks,
               NULL Chequeno,
               0 VoucherID,
               acg.GroupName AcGroup,
               op.Address1,
               op.Address2,
               a.GstIn,
               a.PanNo,
               op.Email,
               op.MobileNo,
               op.PinCode,
               ar.AreaName,
               ct.CityName,
               st.StateName,0 KeyFldValue, null as Audit,null as RefRowID,0 AS Dys
        FROM dbo.AccBal op
            LEFT OUTER JOIN
            (
                SELECT ld.AccountId,
                       ld.CompanyId,
                       ld.YearId,
                       ld.BranchId,
                       SUM(ld.Debit) - SUM(ld.Credit) AS Bal
                FROM dbo.LedgerTrans ld
                    LEFT OUTER JOIN dbo.Acc ac
                        ON ac.Id = ld.AccountId
                    LEFT OUTER JOIN dbo.AcGroup ag
                        ON ag.Id = ac.GroupId
                WHERE ld.IsActive = 1
                      AND ld.IsDeleted = 0 AND ld.CompanyId = @companyid
                      AND
                      (
                          
                              ld.VoucherDate < @fromdate  AND ld.VoucherDate >= @finstartdate
                        
                      )
                GROUP BY ld.AccountId,
                         ld.CompanyId,
                         ld.YearId,
                         ld.BranchId
            ) l
                ON op.AccId = l.AccountId
                   AND op.CompId = l.CompanyId
                   AND op.YearId = l.YearId
            LEFT OUTER JOIN dbo.Acc a
                ON a.Id = op.AccId
            LEFT OUTER JOIN dbo.AcGroup acg
                ON acg.Id = a.GroupId
            LEFT OUTER JOIN dbo.Acc ag
                ON ag.Id = a.AgentId
            LEFT OUTER JOIN dbo.Company c
                ON c.Id = op.CompId
            LEFT OUTER JOIN dbo.Area ar
                ON ar.Id = op.AreaId
            LEFT OUTER JOIN dbo.City ct
                ON ct.Id = op.CityId
            LEFT OUTER JOIN dbo.[State] st
                ON st.Id = ct.StateId
			                
        WHERE op.YearId = @yearid
              AND op.CompId = @companyid
              AND
              (
                  @branchid = 0
                  OR l.BranchId = @branchid
              )
             
              AND op.OpBal + ISNULL(l.Bal, 0) <> 0
              AND
              (
                  (
                      @partyid = 0
                      OR op.AccId = @partyid
                  )
				  AND (@party = 'N'
                  OR (EXISTS
        (
            SELECT 1
            FROM dbo.ReportPara rs
            WHERE rs.ReportId = @reportid
                  AND op.AccId = rs.ParameterValue
                  AND rs.ParameterName = 'party'
        )
                     ))
              )
             
              AND
              (
                  @partygroupid = 0
                  OR a.PGroupId = @partygroupid
              )
              AND
              (
                  @acgroup = 'N'
                  OR EXISTS
        (
            SELECT 1
            FROM dbo.ReportPara rs
            WHERE rs.ReportId = @reportid
                  AND a.GroupId = rs.ParameterValue
                  AND rs.ParameterName = 'acgroup'
        )
              )

        UNION ALL

        SELECT 1 AS flag,
               l.Id AS Id,
               l.CompanyId,
               c.CompName CompanyName,
               l.RefAccountId ReferenceAccountID,
               a1.AccName AS Particulars,
			   0 AS OpBal,
               l.Debit DebitAmt,
               l.Credit CreditAmt,
               l.VoucherNo AS VoucherNo,
               ISNULL(l.BillNo, l.VoucherNo) AS BillNo,
               l.VoucherDate,
               v.VoucherName Voucher,
               v.VTypeId AS VTypeId,
               l.AccountId,
               a.AccName AS AccountName,
               ag.AccName Agent,
               l.Narration,
               ISNULL(CONVERT(DATE, CONVERT(VARCHAR(8), l.VoucherDate), 112), '') TransDate,
               l.Remark Remarks,
               l.ChqNo AS Chequeno,
               l.VoucherId,
               acg.GroupName AS AcGroup,
               acb.Address1,
               acb.Address2,
               a.GstIn,
               a.PanNo,
               acb.Email,
               acb.MobileNo,
               acb.PinCode,
               ar.AreaName,
               ct.CityName,
               st.StateName,l.KeyFldValue,bm.Auth As Audit,l.RefId As RefRowID,
               dy.days AS Dys
        FROM dbo.LedgerTrans l
			left outer join BillMain bm on bm.RowId = l.RefId
            LEFT OUTER JOIN dbo.Company c
                ON c.Id = l.CompanyId
            LEFT OUTER JOIN dbo.Acc a
                ON a.Id = l.AccountId
            LEFT OUTER JOIN dbo.AcGroup acg
                ON acg.Id = a.GroupId
            LEFT OUTER JOIN dbo.Acc a1
                ON a1.Id = l.RefAccountId
            LEFT OUTER JOIN dbo.Acc ag
                ON ag.Id = a.AgentId
            LEFT OUTER JOIN dbo.Voucher v
                ON v.Id = l.VoucherId
            LEFT JOIN dbo.AccBal acb
                ON acb.AccId = a.Id
            LEFT OUTER JOIN dbo.Area ar
                ON ar.Id = acb.AreaId
            LEFT OUTER JOIN dbo.City ct
                ON ct.Id = acb.CityId
            LEFT OUTER JOIN dbo.[State] st
                ON st.Id = ct.StateId
			INNER JOIN #mydays dy ON dy.vtypeid = v.VTypeId                
        WHERE acb.CompId = @companyid AND l.CompanyId = @companyid
              AND acb.YearId = @yearid
              AND l.VoucherDate
              BETWEEN @fromdate AND @todate
              AND
              (
                  @branchid = 0
                  OR l.BranchId = @branchid
              )
              
              AND
              (
                  @partygroupid = 0
                  OR a.PGroupId = @partygroupid
              )
              AND l.IsDeleted = 0
              AND l.IsActive = 1
              AND
              (
                  (
                      @partyid = 0
                      OR l.AccountId = @partyid
                  )
				  AND (@party = 'N'
                  OR (EXISTS
				(
            SELECT 1
            FROM dbo.ReportPara rs
            WHERE rs.ReportId = @reportid
                  AND l.AccountId = rs.ParameterValue
                  AND rs.ParameterName = 'party'
        )
                     ))
              )
              AND
              (
                  @acgroup = 'N'
                  OR EXISTS
        (
            SELECT 1
            FROM dbo.ReportPara rs
            WHERE rs.ReportId = @reportid
                  AND a.GroupId = rs.ParameterValue
                  AND rs.ParameterName = 'acgroup'
        )
              )
    ) x
) xx
ORDER BY xx.AccountName,
         xx.flag,
         xx.VoucherDate,xx.Id

GO

