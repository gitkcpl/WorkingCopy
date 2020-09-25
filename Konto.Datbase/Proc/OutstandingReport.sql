CREATE PROCEDURE [dbo].[OutstandingReport]
 	@CompanyId INT = 1,
	@YearId INT = 1,
	@fromdate INT = 20180401 ,
    @todate INT = 20190331 ,
    @payfromdate INT = 20180401 ,
	@paytodate INT = 20190331 ,
    @reportid INT = 0 ,
    @party VARCHAR(100) = 'N' ,
    @agent VARCHAR(1) = 'N' ,
    @cityid int =0 ,
    @areaid int = 0 ,
    @partygroupid int = 0,
   -- @book VARCHAR(1) = 'N' ,
	@branchid int = 0,
	--@division VARCHAR(1) = 'N',
    @company VARCHAR(1) = 'N' ,
    @partyid INT =0,
    @acgroupid int=0,
    @paid VARCHAR(5) = 'ALL',
    @nature VARCHAR(50)='R',
	@duedays INT = 0,
	@salesid INT = 0

AS
BEGIN
	SELECT vc.SortName AS DrCr,
	br.VoucherNo AS SrNo, 
	ISNULL(br.BillNo,br.VoucherNo) as BillNo, 
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
	CASE WHEN br.RefType = 'Debit' THEN CAST(br.BillAmt - ISNULL(adj.Pay,0) - ISNULL(Selfadj.SelfPay,0) - ISNULL(adj.Rg,0) -ISNULL(br.TdsAmt,0) + ISNULL(br.TcsAmt,0) - ISNULL(br.RetAmt,0) - ISNULL(br.AdjustAmt,0) AS NUMERIC(18,2))
	ELSE -1*CAST(br.BillAmt - ISNULL(adj.Pay,0) - ISNULL(Selfadj.SelfPay,0) - ISNULL(adj.Rg,0) -ISNULL(br.TdsAmt,0) + ISNULL(br.TcsAmt,0) - ISNULL(br.RetAmt,0) - ISNULL(br.AdjustAmt,0) AS NUMERIC(18,2))
	END AS PendingAmt ,

	CASE WHEN br.RefType = 'Debit' THEN CAST(CAST(br.BillAmt - ISNULL(adj.Pay,0) - ISNULL(Selfadj.SelfPay,0)  - ISNULL(adj.Rg,0) -ISNULL(br.TdsAmt,0) + ISNULL(br.TcsAmt,0) - ISNULL(br.RetAmt,0) - ISNULL(br.AdjustAmt,0) AS NUMERIC(18,2) ) AS VARCHAR(25)) + ' Dr'
    ELSE CAST(CAST(br.BillAmt - ISNULL(adj.Pay,0) - ISNULL(Selfadj.SelfPay,0)  - ISNULL(adj.Rg,0) -ISNULL(br.TdsAmt,0) + ISNULL(br.TcsAmt,0) - ISNULL(br.RetAmt,0) - ISNULL(br.AdjustAmt,0) AS NUMERIC(18,2)) AS VARCHAR(25))  + ' Cr' END Pending,                   

	ISNULL(selfAdj.SelfPay, 0) SelfPay,
	 DATEDIFF(D, bm.VDate, GETDATE()) AS Days, 
	  DATEADD(D, ac.CrDays, bm.VDate) AS DueDate, CASE when acb.Bal <0 THEN STR(-1*acb.bal) + ' cr' ELSE STR(acb.bal) + ' Dr' END Bal,
	  br.BillId Id, vc.VTypeId
	FROM dbo.BillRef br
	LEFT JOIN dbo.BillMain bm ON bm.Id = br.BillId
	LEFT JOIN dbo.Acc ac ON ac.Id = br.AccountId
	LEFT JOIN dbo.Acc bk ON bk.Id = bm.BookAcId
	LEFT JOIN dbo.Voucher vc ON vc.Id = br.BillVoucherId
	LEFT JOIN dbo.AcGroup acg ON acg.Id = ac.GroupId
	LEFT JOIN dbo.AccBal acb ON acb.AccId = ac.Id AND acb.CompId = br.CompanyId AND acb.YearId = br.YearId
	LEFT JOIN dbo.City ct ON ct.Id = acb.CityId
	LEFT JOIN dbo.Area ar ON ar.Id = acb.AreaId
	LEFT JOIN dbo.State st ON st.Id = ct.StateId
	LEFT JOIN dbo.Acc ag ON ag.Id = ac.AgentId
	--LEFT OUTER JOIN dbo.Acc agb ON agb.Id = bm.AgentId
	LEFT JOIN dbo.Company co ON co.Id = br.CompanyId
	LEFT JOIN (SELECT SUM(CASE WHEN b.TransType = 'Payment' THEN b.Amount + ISNULL(b.Adla1,0) + ISNULL(b.Adla2,0) + ISNULL(b.Adla3,0) + ISNULL(b.Adla4,0) + ISNULL(b.Adla5,0) + ISNULL(b.Adla6,0) + ISNULL(b.Adla7,0) + ISNULL(b.Adla8,0) + ISNULL(b.Adla9,0) + ISNULL(b.Adla10,0) ELSE 0 END) Pay , 
	SUM(CASE WHEN b.TransType = 'Return' THEN b.Amount ELSE 0 END) Rg ,
                      b.RefCode
					              FROM dbo.BtoB b
						
								  WHERE b.IsActive = 1 AND b.IsDeleted = 0  
								    GROUP BY  b.RefCode
	) adj ON adj.RefCode = br.RowId 

	LEFT JOIN ( SELECT SUM(b.Amount) SelfPay, b.RefVoucherId,b.RefId
                    FROM dbo.BtoB b
					WHERE b.IsActive = 1 AND b.IsDeleted = 0  
                    GROUP BY  b.RefVoucherId,b.RefId
                ) selfAdj ON bm.VoucherID = selfAdj.RefVoucherId AND bm.ID = selfAdj.RefId   
                                
	WHERE  br.IsActive = 1 AND br.IsDeleted = 0 AND bm.IsActive = 1 AND bm.IsDeleted = 0 AND br.CompanyId = @CompanyId
	      AND ( @paid = 'PAID'
                      OR ( CAST(br.BillAmt - ISNULL(adj.Pay,0) - ISNULL(Selfadj.SelfPay,0) - ISNULL(adj.Rg,0) -ISNULL(br.TdsAmt,0) + ISNULL(br.TcsAmt,0) - ISNULL(br.RetAmt,0) - ISNULL(br.AdjustAmt,0) AS NUMERIC(18,2)) <> 0 )
                    )
	AND (@duedays = 0 OR DATEDIFF(D, bm.VDate, GETDATE()) > @duedays ) 
	AND  (br.VoucherDate BETWEEN @fromdate AND @todate) 
	AND isnull(acg.Extra1,'N') = @nature
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
                AND ( @cityid=0 OR acb.CityId=@cityid)
                      --OR EXISTS ( SELECT    1
                      --            FROM      dbo.ReportPara rs
                      --            WHERE     rs.ReportId = @reportid
                      --                      AND acb.CityId = rs.ParameterValue
                      --                      AND rs.ParameterName = 'city' )
                    --)
                AND ( @areaid=0 OR acb.AreaId=@areaid)
                    --  OR EXISTS ( SELECT    1
                    --              FROM      dbo.ReportPara rs
                    --              WHERE     rs.ReportId = @reportid
                    --                        AND acb.AreaId = rs.ParameterValue
                    --                        AND rs.ParameterName = 'area' )
                    --)
                AND ( @partygroupid = 0 OR ac.PGroupId = @partygroupid)
                    --  OR EXISTS ( SELECT    1
                    --              FROM      dbo.ReportPara rs
                    --              WHERE     rs.ReportId = @reportid
                    --                        AND ac.PGroupId = rs.ParameterValue
                    --                        AND rs.ParameterName = 'pgroup' )
                    --)
				AND ( @acgroupid=0 OR ac.GroupId=@acgroupid)
                    --  OR EXISTS ( SELECT    1
                    --              FROM      dbo.ReportPara rs
                    --              WHERE     rs.ReportId = @reportid
                    --                        AND ac.GroupId = rs.ParameterValue
                    --                        AND rs.ParameterName = 'acgroup' )
                    --)
				AND ( @salesid=0 OR ac.EmpId=@salesid)
                --AND ( @book = 'N'
                --      OR EXISTS ( SELECT    1
                --                  FROM      dbo.ReportPara rs
                --                  WHERE     rs.ReportId = @reportid
                --                            AND bm.BookAcId = rs.ParameterValue
                --                            AND rs.ParameterName = 'book' )
                --    )
				--AND ( @branch = 'N'
    --                  OR EXISTS ( SELECT    1
    --                              FROM      dbo.ReportPara rs
    --                              WHERE     rs.ReportId = @reportid
    --                                        AND bm.BookAcId = rs.ParameterValue
    --                                        AND rs.ParameterName = 'branch' )
    --                )
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

