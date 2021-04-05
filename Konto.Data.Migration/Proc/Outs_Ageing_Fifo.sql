IF object_id('[dbo].[Outs_Ageing_Fifo]') IS NULL 
EXEC ('CREATE PROC [dbo].[Outs_Ageing_Fifo] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[Outs_Ageing_Fifo]
    @companyid BIGINT = 0,
    @fromdate INT = 0,
    @todate INT = 0,
    @payfromdate INT = 0,
	@paytodate INT = 0,
    @reportid INT = 0,
    @party VARCHAR(1) = 'N',
    @agent VARCHAR(1) = 'N',
    @city VARCHAR(1) = 'N',
    @area VARCHAR(1) = 'N', 
    @partygroup VARCHAR(1) = 'N',
    @partyid INT = 0,
    @acgroup VARCHAR(1) = 'N',
    @nature VARCHAR(50) = 'R',
    @range1 INT = 0,
    @range2 INT = 30,
    @range3 INT = 60,
    @range4 INT = 90,
	@range5 INT =120
AS
BEGIN

    -- ********************************* GENERATING outs************************************************
    IF EXISTS (SELECT * FROM sys.objects WHERE name = 'tmp_in')
        DROP TABLE tmp_in;


    SELECT *
    INTO tmp_in
    FROM
    (
              SELECT ROW_NUMBER() OVER (ORDER BY l.VoucherDate, l.AccountID) AS SrNo,
               ac.AccName AS Party,
			   l.AccountID,
               DATEDIFF(D, CONVERT(Date,Convert(varchar(8),l.VoucherDate),112),GETDATE()) AS Days,
               l.BillNo,
               l.VoucherDate,
			   CONVERT(Date,Convert(varchar(8),l.VoucherDate),112) TransDate,
			   l.VoucherNo,
               v.VoucherName voucher_name,
               v.Id voucher_id,
               acb.MobileNo mobile,
               l.Id AS LedgerID,
               c.CompName CompName,
               c.ID CompID,
               pg.Id AS party_id,
               pg.GroupName PartyGroup,
               ISNULL(acb.Address1,'') + ISNULL(acb.Address2,'')[address],
               ct.Id AS city_id,
               ct.CityName city_name,
               ar.Id AS area_id,
               ar.AreaName area_name,
               acb.Email PartyEmail,
               l.BillAmt BillAmount,
               acg.GroupName GroupName,
               AG.AccName Agent,ac.PanNo,
               L.BillAmt  DebitAmt,
			    acb.Bal
        FROM dbo.BillRef l
            LEFT OUTER JOIN dbo.acc AS ac
                ON ac.Id = l.AccountId
            LEFT OUTER JOIN dbo.voucher v
                ON v.Id = l.BillVoucherId
            LEFT OUTER JOIN dbo.VoucherType vt
                ON vt.Id = v.VTypeId
            LEFT OUTER JOIN dbo.Acc AG
                ON AG.Id = ac.AgentId
            LEFT OUTER JOIN dbo.company c
                ON c.ID = l.CompanyID
            LEFT OUTER JOIN dbo.PartyGroup pg
                ON pg.Id = ac.PGroupId
			LEFT OUTER JOIN dbo.AccBal acb ON acb.AccId = l.AccountId AND acb.CompId = l.CompanyId AND acb.YearId = l.YearId
			
            LEFT OUTER JOIN dbo.acgroup acg
                ON acg.Id = acb.GroupId

            LEFT OUTER JOIN dbo.city ct
                ON ct.Id = acb.CityId
            LEFT OUTER JOIN dbo.area ar
                ON ar.Id = acb.AreaId
		WHERE l.IsDeleted = 0 AND ISNULL(acg.Extra1,'N') = @nature AND l.CompanyID = @companyid
			  AND ((@nature='R' AND l.RefType='DEBIT') OR (@nature='P' AND l.RefType='CREDIT'))
              AND (l.VoucherDate BETWEEN @fromdate AND @todate )
			  AND ( (@partyid=0 OR l.AccountID=@partyid) AND (@party='N'
                      OR EXISTS ( SELECT    1
                                  FROM      dbo.ReportPara rs
                                  WHERE     rs.ReportId = @reportid
                                            AND l.AccountId = rs.ParameterValue
                                            AND rs.ParameterName = 'party' )
                    ))
                AND ( @agent = 'N'
                      OR EXISTS ( SELECT    1
                                  FROM      dbo.ReportPara rs
                                  WHERE     rs.ReportId = @reportid
                                            AND ag.Id = rs.ParameterValue
                                            AND rs.ParameterName = 'agent' )
                    )
                AND ( @city = 'N'
                      OR EXISTS ( SELECT    1
                                  FROM      dbo.ReportPara rs
                                  WHERE     rs.ReportId = @reportid
                                            AND acb.CityId = rs.ParameterValue
                                            AND rs.ParameterName = 'city' )
                    )
                AND ( @area = 'N'
                      OR EXISTS ( SELECT    1
                                  FROM      dbo.ReportPara rs
                                  WHERE     rs.ReportId = @reportid
                                            AND acb.AreaId = rs.ParameterValue
                                            AND rs.ParameterName = 'area' )
                    )
                AND ( @partygroup = 'N'
                      OR EXISTS ( SELECT    1
                                  FROM      dbo.ReportPara rs
                                  WHERE     rs.ReportId = @reportid
                                            AND ac.PGroupId = rs.ParameterValue
                                            AND rs.ParameterName = 'pgroup' )
                    )
				AND ( @acgroup = 'N'
                      OR EXISTS ( SELECT    1
                                  FROM      dbo.ReportPara rs
                                  WHERE     rs.ReportId = @reportid
                                            AND ac.GroupId = rs.ParameterValue
                                            AND rs.ParameterName = 'acgroup' )
                    )
    ) p


	IF EXISTS (SELECT * FROM sys.objects WHERE name = 'tmp_out')
        DROP TABLE dbo.tmp_out;

SELECT *
    INTO tmp_out
    FROM
    (
              SELECT ROW_NUMBER() OVER (ORDER BY l.VoucherDate, l.AccountID) AS SrNo,
               ac.AccName AS Party,
			   l.AccountId,
               DATEDIFF(D, CONVERT(Date,Convert(varchar(8),l.VoucherDate),112), GETDATE()) AS Days,
               l.BillNo,
               l.VoucherDate,
               v.VoucherName voucher_name,
               v.Id voucher_id,
               acb.MobileNo mobile,
               l.Id AS LedgerID,
               c.CompName CompName,
               c.ID CompID,
               pg.Id AS party_id,
               pg.GroupName PartyGroup,
               acb.Address1 + acb.Address2 [address],
               ct.Id AS city_id,
               ct.CityName city_name,
               ar.Id AS area_id,
               ar.AreaName area_name,
               acb.Email PartyEmail,
               l.BillAmt BilllAmount,
               acg.GroupName GroupName,
               AG.AccName Agent,
               l.BillAmt + isnull(b.amt,0) Amt
        FROM dbo.BillRef l
            LEFT OUTER JOIN dbo.acc AS ac
                ON ac.Id = l.AccountId
            
            LEFT OUTER JOIN dbo.voucher v
                ON v.Id = l.BillVoucherId
            LEFT OUTER JOIN dbo.VoucherType vt
                ON vt.Id = v.VTypeId
            LEFT OUTER JOIN dbo.Acc AG
                ON AG.Id = ac.AgentId
            LEFT OUTER JOIN dbo.company c
                ON c.ID = l.CompanyID
            LEFT OUTER JOIN dbo.PartyGroup pg
                ON pg.Id = ac.PGroupId
			LEFT OUTER JOIN dbo.AccBal acb ON acb.AccId = l.AccountId AND acb.CompId = l.CompanyId AND acb.YearId = l.YearId
			LEFT OUTER JOIN dbo.acgroup acg
                ON acg.Id = acb.GroupId
            LEFT OUTER JOIN dbo.city ct
                ON ct.Id = acb.CityId
            LEFT OUTER JOIN dbo.area ar
                ON ar.Id = acb.AreaId
                left outer join (select b.RefId,b.RefTransId, sum(ISNULL(b.Adla1,0) + 
	ISNULL(b.Adla2,0) + ISNULL(b.Adla3,0) + ISNULL(b.Adla4,0) + ISNULL(b.Adla5,0) +
	ISNULL(b.Adla6,0) + ISNULL(b.Adla7,0) + ISNULL(b.Adla8,0) + ISNULL(b.Adla9,0) + ISNULL(b.Adla10,0)) as Amt FROM
			 dbo.BtoB b group by b.RefId,b.RefTransId )b
			 on b.RefTransId = l.BillTransId
			and b.RefId = l.BillId

		WHERE l.IsDeleted = 0  AND l.CompanyID = @companyid
              AND (l.VoucherDate BETWEEN @payfromdate AND @paytodate )  
			  AND ((@nature='R' AND l.RefType='CREDIT') OR (@nature='P' AND l.RefType='DEBIT'))    
            
			AND ( (@partyid=0 OR l.AccountID=@partyid) AND (@party='N'
                      OR EXISTS ( SELECT    1
                                  FROM      dbo.ReportPara rs
                                  WHERE     rs.ReportId = @reportid
                                            AND l.AccountId = rs.ParameterValue
                                            AND rs.ParameterName = 'party' )
                    ))
                AND ( @agent = 'N'
                      OR EXISTS ( SELECT    1
                                  FROM      dbo.ReportPara rs
                                  WHERE     rs.ReportId = @reportid
                                            AND ag.Id = rs.ParameterValue
                                            AND rs.ParameterName = 'agent' )
                    )
                AND ( @city = 'N'
                      OR EXISTS ( SELECT    1
                                  FROM      dbo.ReportPara rs
                                  WHERE     rs.ReportId = @reportid
                                            AND acb.CityId = rs.ParameterValue
                                            AND rs.ParameterName = 'city' )
                    )
                AND ( @area = 'N'
                      OR EXISTS ( SELECT    1
                                  FROM      dbo.ReportPara rs
                                  WHERE     rs.ReportId = @reportid
                                            AND acb.AreaId = rs.ParameterValue
                                            AND rs.ParameterName = 'area' )
                    )
                AND ( @partygroup = 'N'
                      OR EXISTS ( SELECT    1
                                  FROM      dbo.ReportPara rs
                                  WHERE     rs.ReportId = @reportid
                                            AND ac.PGroupId = rs.ParameterValue
                                            AND rs.ParameterName = 'pgroup' )
                    )
				AND ( @acgroup = 'N'
                      OR EXISTS ( SELECT    1
                                  FROM      dbo.ReportPara rs
                                  WHERE     rs.ReportId = @reportid
                                            AND ac.GroupId = rs.ParameterValue
                                            AND rs.ParameterName = 'acgroup' )
                    )

    ) p



    
    ----------------------- caculting fifo based outstandig--------------------------------------------------------------------------------------------------

    IF EXISTS (SELECT * FROM sys.objects WHERE name = 'curr_fifo')
        DROP TABLE curr_fifo;


    WITH s
    AS (SELECT *,
               PurchaseUpToNow =
               (
                   SELECT SUM(DebitAmt)
                   FROM tmp_in
                   WHERE Accountid = s.AccountID
                         AND SrNo <= s.SrNo
                         AND DebitAmt > 0
               )
        FROM tmp_in s),
         p
    AS (SELECT Accountid,
               SUM(Amt) AS TotalSales
        FROM tmp_out
        WHERE Amt > 0
        GROUP BY accountid)
    SELECT *,
           LeftAmt AS Amount
    INTO curr_fifo
    FROM
    (
        SELECT s.*,
               p.TotalSales,
               CASE
                   WHEN s.PurchaseUpToNow - ISNULL(p.TotalSales, 0) < 0 THEN
                       0
                   WHEN (s.PurchaseUpToNow - ISNULL(p.TotalSales, 0)) > s.DebitAmt THEN
                       s.DebitAmt
                   ELSE
                       s.PurchaseUpToNow - ISNULL(p.TotalSales, 0)
               END AS LeftAmt
        FROM s
            LEFT JOIN p
                ON s.Accountid = p.Accountid
        WHERE s.DebitAmt > 0
    ) fifo
    WHERE LeftAmt <> 0;

	
	--- Generate Ageing Details------------------------------------------

    SET NOCOUNT ON;
    SELECT p.Party AS Account,
	p.AccountId,p.BillNo,p.voucher_name,p.TransDate,p.voucher_id,p.VoucherNo,
           (p.LeftAmt) Pending,
              CASE
                      WHEN p.[Days] >= @range1
                           AND p.[Days] <= @range2 THEN
                          p.LeftAmt
                      ELSE
                          0
                  END
               AS Range1Value,
              CASE
                      WHEN p.[Days] > @range2
                           AND p.[Days] <= @range3 THEN
                          p.LeftAmt
                      ELSE
                          0
                  END
               AS Range2Value,
             CASE
                      WHEN p.[Days] > @range3
                           AND p.[Days] <= @range4 THEN
                          p.LeftAmt
                      ELSE
                          0
                  END
               AS Range3Value,
			    CASE
                      WHEN p.[Days] > @range4
                           AND p.[Days] <= @range5 THEN
                          p.LeftAmt
                      ELSE
                          0
                  END
               AS Range4Value,
              CASE
                      WHEN p.[Days] > @range5 THEN
                          p.LeftAmt
                      ELSE
                          0
                  END
               AS AboveRangeValue,
           p.PartyEmail,
           p.area_name,
           p.area_id,
           p.GroupName,
           p.PanNo,
           p.city_name,
           p.city_id,
           p.[Address],
           p.PartyGroup,
           p.party_id,
           p.CompID,
           p.CompName,
           p.mobile,
           p.Agent,
		   p.Bal
    FROM dbo.curr_fifo p
   ORDER BY p.party
END

GO


