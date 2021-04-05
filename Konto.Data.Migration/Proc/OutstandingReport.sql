IF object_id('[dbo].[OutstandingReport]') IS NULL 
EXEC ('CREATE PROC [dbo].[OutstandingReport] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE dbo.OutstandingReport
 	@CompanyId INT = 1,
	@YearId INT = 1,
	@fromdate INT = 20180401 ,
  @todate INT = 20190331 ,
  @payfromdate INT = 20180401 ,
	@paytodate INT = 20190331 ,
  @reportid INT = 0 ,
  @party VARCHAR(100) = 'N' ,
  @agent VARCHAR(1) = 'N' ,
  @city VARCHAR(1) ='N' ,
  @area VARCHAR(1) = 'N' ,
  @partygroup VARCHAR(1) = 'N',
  @book VARCHAR(1) = 'N' ,
	@branchid int = 0,
	--@division VARCHAR(1) = 'N',
    @company VARCHAR(1) = 'N' ,
    @partyid INT =0,
    @acgroup VARCHAR(1)='N',
    @paid VARCHAR(10) = 'UNPAID',
    @nature VARCHAR(50)='R',
	@duedays INT = 0,
	@emp VARCHAR(1) = 'N'

AS
BEGIN
	SELECT vc.SortName AS DrCr,
	br.VoucherNo AS SrNo, 
	CASE WHEN vc.VTypeId =12 THEN br.VoucherNo else br.BillNo END AS BillNo,  
	CONVERT(DATETIME2, CONVERT(VARCHAR(8), 
	ISNULL(br.VoucherDate,0)), 112) AS [Date] ,
	ac.AccName AS Account,
	ac.Id AS AccountId,
	ac.GstIn, 
	vc.VoucherName,
	acg.GroupName,
	Isnull(acb.Address1,'') + ISNULL(acb.Address2,'') AS [Address] , 
	ct.CityName AS City, 
	st.StateName AS [State] , 
	ar.AreaName AS Area, 
	bk.AccName AS BookName, 
	ag.AccName Agent,
	--CASE WHEN  agb.Id IS NULL THEN ag.AccName ELSE agb.AccName end AS Agent, 
	co.CompName AS CompanyName, 
	acb.MobileNo, 
	acb.Email AS PartyEmail,
	ISNULL(bm.TotalPcs,0) AS TotalPcs, 
	ISNULL(bm.TotalQty,0) AS TotalQty,  
	CASE WHEN bm.TotalQTY > 0 THEN CAST(bm.TotalAmount / bm.TotalQTY AS NUMERIC(18,2)) ELSE 0 END AS NetRate ,
	CAST(br.GrossAmt  as numeric(18,2)) AS GrossAmt,
--CAST(br.BillAmt  as numeric(18,2)) AS BillAmt,
	CASE WHEN br.RefType = 'Debit' THEN CAST(br.BillAmt  as numeric(18,2)) ELSE -1*CAST(br.BillAmt  as numeric(18,2)) END AS BillAmt ,
	CAST(ISNULL(adj.Pay,0)  as numeric(18,2)) + ISNULL(br.AdjustAmt,0) AS AdjustAmt, 
	CAST(ISNULL(adj.Rg,0) as numeric(18,2)) + ISNULL(br.RetAmt,0) AS ReturnAmt, 
	CAST(ISNULL(br.TdsAmt,0) as numeric(18,2)) AS TdsAmt, 
	CAST(ISNULL(br.TcsAmt,0) as numeric(18,2)) AS TcsAmt, 
	CASE WHEN br.RefType = 'Debit' THEN CAST(br.BillAmt - ISNULL(adj.Pay,0) - ISNULL(Selfadj.SelfPay,0) - ISNULL(adj.Rg,0) -ISNULL(br.TdsAmt,0)  - ISNULL(br.RetAmt,0) - ISNULL(br.AdjustAmt,0)-ISNULL(bp.rcpt,0) AS NUMERIC(18,2))
	ELSE -1*CAST(br.BillAmt - ISNULL(adj.Pay,0) - ISNULL(Selfadj.SelfPay,0) - ISNULL(adj.Rg,0) -ISNULL(br.TdsAmt,0)  - ISNULL(br.RetAmt,0) - ISNULL(br.AdjustAmt,0)-ISNULL(bp.rcpt,0) AS NUMERIC(18,2))
	END AS PendingAmt ,

	CASE WHEN br.RefType = 'Debit' THEN CAST(CAST(br.BillAmt - ISNULL(adj.Pay,0) - ISNULL(Selfadj.SelfPay,0)  - ISNULL(adj.Rg,0) -ISNULL(br.TdsAmt,0)  - ISNULL(br.RetAmt,0) - ISNULL(br.AdjustAmt,0) -ISNULL(bp.rcpt,0) AS NUMERIC(18,2) ) AS VARCHAR(25)) + ' Dr'
    ELSE CAST(CAST(br.BillAmt - ISNULL(adj.Pay,0) - ISNULL(Selfadj.SelfPay,0)  - ISNULL(adj.Rg,0) -ISNULL(br.TdsAmt,0)  - ISNULL(br.RetAmt,0) - ISNULL(br.AdjustAmt,0) AS NUMERIC(18,2)) -ISNULL(bp.rcpt,0) AS VARCHAR(25))  + ' Cr' END Pending,                   

	ISNULL(selfAdj.SelfPay, 0) SelfPay,
	 DATEDIFF(D, CONVERT(DATETIME2, CONVERT(VARCHAR(8), bm.VoucherDate)), GETDATE()) AS Days, 
	  DATEADD(D, ac.CrDays, CONVERT(DATETIME2, CONVERT(VARCHAR(8), bm.VoucherDate))) AS DueDate, CASE when acb.Bal <0 THEN STR(-1*acb.bal) + ' cr' ELSE STR(acb.bal) + ' Dr' END Bal,
	  br.BillId Id, vc.VTypeId,br.RefType
	FROM dbo.BillRef br
	LEFT JOIN dbo.BillMain bm ON bm.Id = br.BillId
	LEFT JOIN dbo.Acc ac ON ac.Id = br.AccountId
	LEFT JOIN dbo.Acc bk ON bk.Id = bm.BookAcId
	LEFT JOIN dbo.Voucher vc ON vc.Id = br.BillVoucherId
	LEFT OUTER JOIN dbo.AccBal acb ON acb.AccId = br.AccountId AND acb.CompId = br.CompanyId AND acb.YearId = br.YearId
    LEFT OUTER JOIN dbo.AcGroup acg ON acg.Id = acb.GroupId
    LEFT JOIN dbo.City ct ON ct.Id = acb.CityId
	LEFT JOIN dbo.Area ar ON ar.Id = acb.AreaId
	LEFT JOIN dbo.State st ON st.Id = ct.StateId
	LEFT JOIN dbo.Acc ag ON ag.Id = ac.AgentId
	--LEFT OUTER JOIN dbo.Acc agb ON agb.Id = bm.AgentId
	LEFT JOIN dbo.Company co ON co.Id = br.CompanyId
	LEFT JOIN (SELECT SUM(CASE WHEN b.TransType = 'Payment' THEN b.Amount + ISNULL(b.Adla1,0) + 
	ISNULL(b.Adla2,0) + ISNULL(b.Adla3,0) + ISNULL(b.Adla4,0) + ISNULL(b.Adla5,0) +
	ISNULL(b.Adla6,0) + ISNULL(b.Adla7,0) + ISNULL(b.Adla8,0) + ISNULL(b.Adla9,0) + ISNULL(b.Adla10,0) ELSE 0 END) Pay, 
	SUM(CASE WHEN b.TransType = 'Return' THEN b.Amount ELSE 0 END) Rg,b.RefCode	FROM dbo.BtoB b
								  LEFT OUTER JOIN dbo.BillMain bm ON bm.Id = b.RefId
								  WHERE b.IsActive = 1 AND b.IsDeleted = 0  AND bm.IsDeleted=0
								  AND bm.VoucherDate BETWEEN @payfromdate AND @paytodate
								    GROUP BY  b.RefCode
	) adj ON adj.RefCode = br.RowId 

	--LEFT JOIN ( SELECT SUM(b.Amount) SelfPay, b.RefVoucherId,b.RefId
 --                   FROM dbo.BtoB b
	--				WHERE b.IsActive = 1 AND b.IsDeleted = 0  
 --                   GROUP BY  b.RefVoucherId,b.RefId
 --               ) selfAdj ON bm.VoucherID = selfAdj.RefVoucherId AND bm.ID = selfAdj.RefId
 
 LEFT JOIN ( SELECT SUM(b.Amount) SelfPay, b.RefVoucherId,b.RefId,b.RefTransId
                    FROM dbo.BtoB b
					WHERE b.IsActive = 1 AND b.IsDeleted = 0  
                    GROUP BY  b.RefVoucherId,b.RefId,b.RefTransId
                ) selfAdj ON br.BillVoucherId = selfAdj.RefVoucherId AND br.BillId = selfAdj.RefId
				and isnull(br.BillTransId,br.BillId) = selfAdj.RefTransId   

-- for pos receipt
LEFT OUTER JOIN (SELECT bp.BillId,SUM(bp.Pay1Amt + bp.DiscAmt - bp.ChangeAmt+bp.Pay2Amt+bp.Pay3Amt)rcpt
 FROM bill_pays AS bp 
                 WHERE bp.IsActive=1 AND bp.IsDeleted	=0 
                 AND bp.PayDate BETWEEN @payfromdate AND @paytodate
                 GROUP BY bp.BillId
)bp ON br.BillId = bp.BillId

                                
	WHERE  br.IsActive = 1 AND br.IsDeleted = 0 AND bm.IsActive = 1 AND bm.IsDeleted = 0 AND br.CompanyId = @CompanyId
	      --AND ( @paid = 'PAID'
       --               OR ( CAST(br.BillAmt - ISNULL(adj.Pay,0) - ISNULL(Selfadj.SelfPay,0) - ISNULL(adj.Rg,0) -ISNULL(br.TdsAmt,0) + ISNULL(br.TcsAmt,0) - ISNULL(br.RetAmt,0) - ISNULL(br.AdjustAmt,0) AS NUMERIC(18,2)) <> 0 )
       --             )

	     AND ( (@paid = 'PAID' AND 
	      ((@nature='R' AND br.RefType='DEBIT') OR(@nature='P' AND br.RefType='CREDIT'))
	      AND  
	       ( CAST(ABS(br.BillAmt) - ISNULL(adj.Pay,0) - ISNULL(Selfadj.SelfPay,0) -
	        ISNULL(adj.Rg,0) -ISNULL(br.TdsAmt,0)  - 
	        ISNULL(br.RetAmt,0) - ISNULL(br.AdjustAmt,0)-ISNULL(bp.rcpt,0) AS NUMERIC(18,2)) = 0 ))
	      
                      OR (@paid = 'UNPAID' and ( ( ABS(br.BillAmt) - ISNULL(adj.Pay,0) - 
                      ISNULL(Selfadj.SelfPay,0) - ISNULL(adj.Rg,0) 
                      -ISNULL(br.TdsAmt,0) - 
                      ISNULL(br.RetAmt,0) - ISNULL(br.AdjustAmt,0)-ISNULL(bp.rcpt,0)) <> 0 ))
                      
                      OR @paid='ALL'
                    )

	AND (@duedays = 0 OR DATEDIFF(D, CONVERT(DATETIME2, CONVERT(VARCHAR(8), bm.VoucherDate)), GETDATE()) > @duedays ) 
	AND  (br.VoucherDate BETWEEN @fromdate AND @todate) 
	AND isnull(acg.Extra1,'N') = @nature AND (@branchid=0 OR bm.BranchId=@branchid)
	AND ( (@partyid=0 OR br.AccountID=@partyid) AND (@party='N'
                      OR EXISTS ( SELECT    1
                                  FROM      dbo.ReportPara rs
                                  WHERE     rs.ReportId = @reportid
                                            AND br.AccountId = rs.ParameterValue
                                            AND rs.ParameterName = 'party' )
                    ))
                AND ( @agent = 'N'
                      OR EXISTS ( SELECT    1
                                  FROM      dbo.ReportPara rs
                                  WHERE     rs.ReportId = @reportid
                                            AND ag.Id = rs.ParameterValue
                                            AND rs.ParameterName = 'agent' )
                    )
                AND ( @city='N' 
                      OR EXISTS ( SELECT    1
                                  FROM      dbo.ReportPara rs
                                  WHERE     rs.ReportId = @reportid
                                            AND acb.CityId = rs.ParameterValue
                                            AND rs.ParameterName = 'city' )
                    )
                AND ( @area='N'
                      OR EXISTS ( SELECT    1
                                  FROM      dbo.ReportPara rs
                                  WHERE     rs.ReportId = @reportid
                                            AND acb.AreaId = rs.ParameterValue
                                            AND rs.ParameterName = 'area' )
                    )
                AND ( @partygroup='N'
                      OR EXISTS ( SELECT    1
                                  FROM      dbo.ReportPara rs
                                  WHERE     rs.ReportId = @reportid
                                            AND ac.PGroupId = rs.ParameterValue
                                            AND rs.ParameterName = 'pgroup' )
                    )
				AND ( @acgroup='N'
                      OR EXISTS ( SELECT    1
                                  FROM      dbo.ReportPara rs
                                  WHERE     rs.ReportId = @reportid
                                            AND ac.GroupId = rs.ParameterValue
                                            AND rs.ParameterName = 'acgroup' )
                    )
				AND ( @emp='N'
                
                      OR EXISTS ( SELECT    1
                                  FROM      dbo.ReportPara rs
                                  WHERE     rs.ReportId = @reportid
                                            AND bm.EmpId = rs.ParameterValue
                                            AND rs.ParameterName = 'emp' )
                    )
				AND ( @book = 'N'
                      OR EXISTS ( SELECT    1
                                  FROM      dbo.ReportPara rs
                                  WHERE     rs.ReportId = @reportid
                                            AND bm.BookAcId = rs.ParameterValue
                                            AND rs.ParameterName = 'book' )
                    )
				--AND ( @division = 'N'
    --                  OR EXISTS ( SELECT    1
    --                              FROM      dbo.ReportPara rs
    --                              WHERE     rs.ReportId = @reportid
    --                                        AND bm.BookAcId = rs.ParameterValue
    --                                        AND rs.ParameterName = 'division' )
    --                )
	ORDER BY ac.AccName, br.VoucherDate

END
GO
