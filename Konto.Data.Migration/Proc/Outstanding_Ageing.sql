IF object_id('[dbo].[Outstanding_Ageing]') IS NULL 
EXEC ('CREATE PROC [dbo].[Outstanding_Ageing] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[Outstanding_Ageing] 
    -- Add the parameters for the stored procedure here
    @companyid BIGINT = 0 ,
    @fromdate INT = 0 ,
    @todate INT = 0 ,
    @payfromdate INT = 0 ,
	@paytodate INT = 0 ,
    @reportid INT = 0 ,
    @party VARCHAR(1) = 'N' ,
    @agent VARCHAR(1) = 'N' ,
    @city VARCHAR(1) = 'N' ,
    @area VARCHAR(1) = 'N' ,
	@branch VARCHAR(1) = 'N' ,
	@division VARCHAR(1) = 'N' ,
    @partygroup VARCHAR(1) = 'N' ,
    @book VARCHAR(1) = 'N' ,
    @company VARCHAR(1) = 'N' ,
    @partyid INT = 0 ,
    @acgroup VARCHAR(1) = 'N' ,
    @paid VARCHAR(5) = 'ALL' ,
    @nature VARCHAR(50) = 'ASSETS',
	@duedays INT = 0 ,
    @range1 INT=0,
    @range2 INT=30,
    @range3 INT=60,
    @range4 INT=90
   
AS
     SELECT  a.AccName AS Account , l.RefType ,
		        l.VoucherNo AS SrNo,
		        l.BillNo,
				CONVERT(DATETIME2, CONVERT(VARCHAR(8), 
				ISNULL(l.VoucherDate,0)), 112) AS Date,
                DATEDIFF(D, bm.VDate, GETDATE()) AS Days ,
                acb.MobileNo AS mobile,
                l.ID,
				bm.VDate AS TransDate,
              --  AG.account_name AgentName ,
                c.CompName  ,
                c.ID CompID ,
                pg.Id AS party_id ,
                pg.GroupName PartyGroup ,
                acb.Address1 + acb.Address2 address ,
                ct.id AS city_id ,
                ct.CityName  city_name ,
                ar.Id area_id ,
                ar.AreaName area_name ,
                acb.Email PartyEmail ,
				ag.AccName AS AgentName,				
				CASE WHEN DATEDIFF(D, bm.VDate, GETDATE())>@range1 AND DATEDIFF(D, bm.VDate, GETDATE())<=@range2 THEN
				 CASE WHEN l.RefType = 'Debit' THEN CAST(l.BillAmt - ISNULL(adj.Pay,0) - ISNULL(Selfadj.SelfPay,0) - ISNULL(adj.Rg,0) -ISNULL(l.TdsAmt,0)  -ISNULL(bp.rcpt,0) AS NUMERIC(18,2))
	ELSE -1*CAST(l.BillAmt - ISNULL(adj.Pay,0) - ISNULL(Selfadj.SelfPay,0) - ISNULL(adj.Rg,0) -ISNULL(l.TdsAmt,0) -ISNULL(bp.rcpt,0) AS NUMERIC(18,2))
	 END ELSE 0  END AS Range1Value,
				CASE WHEN DATEDIFF(D, bm.VDate, GETDATE())>@range2 AND DATEDIFF(D, bm.VDate, GETDATE())<=@range3 THEN 
				 CASE WHEN l.RefType = 'Debit' THEN CAST(l.BillAmt - ISNULL(adj.Pay,0) - ISNULL(Selfadj.SelfPay,0) - ISNULL(adj.Rg,0) -ISNULL(l.TdsAmt,0) -ISNULL(bp.rcpt,0) AS NUMERIC(18,2))
	ELSE -1*CAST(l.BillAmt - ISNULL(adj.Pay,0) - ISNULL(Selfadj.SelfPay,0) - ISNULL(adj.Rg,0) -ISNULL(l.TdsAmt,0) -ISNULL(bp.rcpt,0) AS NUMERIC(18,2))
	 END ELSE 0 END AS Range2Value,
				CASE WHEN DATEDIFF(D, bm.VDate, GETDATE())>@range3 AND DATEDIFF(D, bm.VDate, GETDATE())<=@range4 THEN 
				 CASE WHEN l.RefType = 'Debit' THEN CAST(l.BillAmt - ISNULL(adj.Pay,0) - ISNULL(Selfadj.SelfPay,0) - ISNULL(adj.Rg,0) -ISNULL(l.TdsAmt,0) -ISNULL(bp.rcpt,0) AS NUMERIC(18,2))
	ELSE -1*CAST(l.BillAmt - ISNULL(adj.Pay,0) - ISNULL(Selfadj.SelfPay,0) - ISNULL(adj.Rg,0) -ISNULL(l.TdsAmt,0) -ISNULL(bp.rcpt,0) AS NUMERIC(18,2))
	 END ELSE 0 END AS Range3Value,
				CASE WHEN DATEDIFF(D, bm.VDate, GETDATE())>@range4  THEN 
				 CASE WHEN l.RefType = 'Debit' THEN CAST(l.BillAmt - ISNULL(adj.Pay,0) - ISNULL(Selfadj.SelfPay,0) - ISNULL(adj.Rg,0) -ISNULL(l.TdsAmt,0) -ISNULL(bp.rcpt,0) AS NUMERIC(18,2))
	ELSE -1*CAST(l.BillAmt - ISNULL(adj.Pay,0) - ISNULL(Selfadj.SelfPay,0) - ISNULL(adj.Rg,0) -ISNULL(l.TdsAmt,0)-ISNULL(bp.rcpt,0) AS NUMERIC(18,2))
	 END ELSE 0 END AS AboveRangeValue,	
				CASE WHEN l.RefType = 'Debit' THEN CAST(l.BillAmt  as numeric(18,2)) ELSE -1*CAST(l.BillAmt  as numeric(18,2)) END AS BillAmt ,
				CASE WHEN l.RefType = 'Debit' THEN CAST(l.BillAmt - ISNULL(adj.Pay,0) - ISNULL(Selfadj.SelfPay,0) - ISNULL(adj.Rg,0) -ISNULL(l.TdsAmt,0) -ISNULL(bp.rcpt,0) AS NUMERIC(18,2))
				ELSE -1*CAST(l.BillAmt - ISNULL(adj.Pay,0) - ISNULL(Selfadj.SelfPay,0) - ISNULL(adj.Rg,0) -ISNULL(l.TdsAmt,0) -ISNULL(bp.rcpt,0) AS NUMERIC(18,2))
				END AS PendingAmt ,
				CASE WHEN l.RefType = 'Debit' THEN CAST(CAST(l.BillAmt - ISNULL(adj.Pay,0) - ISNULL(Selfadj.SelfPay,0)  - ISNULL(adj.Rg,0) -ISNULL(l.TdsAmt,0) -ISNULL(bp.rcpt,0) AS NUMERIC(18,2) ) AS VARCHAR(25)) + ' Dr'
				 ELSE CAST(CAST(l.BillAmt - ISNULL(adj.Pay,0) - ISNULL(Selfadj.SelfPay,0)  - ISNULL(adj.Rg,0) -ISNULL(l.TdsAmt,0) -ISNULL(bp.rcpt,0) AS NUMERIC(18,2)) AS VARCHAR(25)) + ' Cr' END Pending,     
                l.AccountID ,
                acg.GroupName 
				FROM    dbo.BillRef l
				LEFT OUTER JOIN dbo.BillMain bm ON bm.Id = l.BillId
                LEFT OUTER JOIN dbo.acc AS a ON a.Id = l.AccountId
               
				LEFT OUTER JOIN dbo.PartyGroup pg ON pg.Id = a.PGroupId
                LEFT OUTER JOIN dbo.voucher v ON v.Id = l.BillVoucherId
                LEFT OUTER JOIN dbo.VoucherType vt ON vt.Id = v.VTypeId
                LEFT OUTER JOIN dbo.acc AG ON AG.Id = bm.AgentId
                LEFT OUTER JOIN dbo.company c ON c.ID = l.CompanyID
				LEFT OUTER JOIN dbo.AccBal acb ON acb.AccId = l.AccountId AND acb.CompId = l.CompanyId AND acb.YearId = l.YearId
                 LEFT OUTER JOIN dbo.AcGroup acg ON acg.Id = acb.GroupId
                LEFT OUTER JOIN dbo.city ct ON ct.Id = acb.CityId
                LEFT OUTER JOIN dbo.area ar ON ar.Id = acb.AreaId
                LEFT OUTER JOIN dbo.acc bk ON bk.Id = bm.BookAcId
				LEFT JOIN (SELECT SUM(CASE WHEN b.TransType = 'Payment' THEN b.Amount + ISNULL(b.Adla1,0) + ISNULL(b.Adla2,0) + ISNULL(b.Adla3,0) + ISNULL(b.Adla4,0) + ISNULL(b.Adla5,0) + ISNULL(b.Adla6,0) + ISNULL(b.Adla7,0) + ISNULL(b.Adla8,0) + ISNULL(b.Adla9,0) + ISNULL(b.Adla10,0) ELSE 0 END) Pay , SUM(CASE WHEN b.TransType = 'Return' THEN b.Amount ELSE 0 END) Rg ,
                      b.RefCode
					              FROM dbo.BtoB b
								  WHERE b.IsActive = 1 AND b.IsDeleted = 0  
								    GROUP BY  b.RefCode
				) adj ON adj.RefCode = l.RowId 

				LEFT JOIN ( SELECT SUM(b.Amount) SelfPay , b.RefVoucherId,b.RefId
                    FROM dbo.BtoB b
					WHERE b.IsActive = 1 AND b.IsDeleted = 0 
                    GROUP BY  b.RefVoucherId,b.RefId
                ) selfAdj ON bm.VoucherID = selfAdj.RefVoucherId AND  bm.ID = selfAdj.RefId   

                LEFT OUTER JOIN (SELECT bp.BillId,SUM(bp.Pay1Amt + bp.DiscAmt - bp.ChangeAmt+bp.Pay2Amt+bp.Pay3Amt)rcpt
                        FROM bill_pays AS bp 
                        WHERE bp.IsActive=1 AND bp.IsDeleted	=0 
                        AND bp.PayDate BETWEEN @payfromdate AND @paytodate
                        GROUP BY bp.BillId
                      )bp ON l.BillId = bp.BillId

                                
        WHERE   l.IsActive = 1 AND l.IsDeleted = 0 AND bm.IsActive = 1 AND bm.IsDeleted = 0 AND l.VoucherDate BETWEEN @fromdate AND @todate 
                AND ISNULL(acg.Nature, 'N') = @nature AND acg.Extra1 <> 'N'    AND ( ( CAST(l.BillAmt - ISNULL(adj.Pay,0) - ISNULL(Selfadj.SelfPay,0) - ISNULL(adj.Rg,0) -ISNULL(l.TdsAmt,0)  AS NUMERIC(18,2)) <> 0 )
                    )				
       				AND ( (@partyid=0 OR l.AccountID=@partyid) AND(@party='N'
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
                                            AND a.PGroupId = rs.ParameterValue
                                            AND rs.ParameterName = 'pgroup' )
                    )
                AND ( @book = 'N'
                      OR EXISTS ( SELECT    1
                                  FROM      dbo.ReportPara rs
                                  WHERE     rs.ReportId = @reportid
                                            AND bm.BookAcId = rs.ParameterValue
                                            AND rs.ParameterName = 'book' )
                    )
    
GO

