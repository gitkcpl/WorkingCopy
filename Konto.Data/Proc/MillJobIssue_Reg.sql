IF object_id('[dbo].[MillJobIssue_Reg]') IS NULL 
EXEC ('CREATE PROC [dbo].[MillJobIssue_Reg] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[MillJobIssue_Reg]
    @fromdate INT,
    @todate INT,
    @companyid INT = 22,
    @reportid INT = 0,
    @party VARCHAR(1) = 'N',
    @agent INT = 0,
    @city VARCHAR(1) = 'N',
    @area VARCHAR(1) = 'N',
    @partygroup INT = 0,
    @item VARCHAR(1) = 'N',
    @color VARCHAR(1) = 'N',
    @voucherid INT = 0,
    @unitid INT = 0,
    @transid INT = 0,
    @divid INT = 0,
    @itemgroupid INT = 0,
	@itemtype varchar(1) ='N',
	@vtypeid INT = 0,
	@challanType varchar(50)=0,
	@FilterType varchar(50)='All'
AS
SELECT  sc.ID AS SCID,
      case when sc.VoucherDate>10101 then  CONVERT(DATE, CONVERT(VARCHAR(8), sc.VoucherDate), 112) end ScDate,
       sc.VoucherNo,
       '' AS SoDate,	 
       sc.AccID,
       a.AccName Party,
       ag.AgentId,
       ag.AccName Agent,
       tr.AccName Trransport,
       sc.TotalPcs,
       sc.TotalQty,
       sc.TotalAmount,
       sc.Remark,
       sc.DivID,
       dm.DivisionName,
       ISNULL(st.ProductId,0) ItemID,
       ISNULL( i.ProductName,0) Item,
       CAST( ISNULL(st.qty, 0) as numeric(18,2)) Qty,
       ISNULL(st.Cops,0) Cops, 
       ISNULL(st.Pcs, 1) Pcs,
       ISNULL(mr.RcptPcs, 0) RcptPcs,
	  CAST( ISNULL(mr.RcptQty, 0) as numeric(18,2)) RcptQty,
	  CAST(isnull(st.Pcs,0)- ISNULL(mr.RcptPcs, 0)  as numeric(18,2)) PendingPcs,
	  CAST(isnull(st.Qty,0)- (ISNULL(mr.RcptQty, 0) +ISNULL(mr.ShQty, 0))  as numeric(18,2)) PendingQty,
      CAST( isnull(mr.ShQty,0) as numeric(18,2)) ShQty,
	  CAST(isnull((isnull(mr.ShQty,1) /isnull(mr.RcptQty,1)) ,0)as  numeric(18,2)) ShPer,
       isnull(st.LotNo,0) Batch,
       pg.AccName DelvParty,
       ic.ColorName Color,
       gt.GradeName Grade,
       0 MainRate,
       st.Rate,
	   st.LotNo AS MergeNo,
       st.CessPer AS CessRate,
       st.Cess AS CessAmount,
       0 AS VatPer,
       0 AS VatAmount,
       0 AS AdVatPer,
       0 as AdVatAmount,
       st.Gross AS Total,
       st.Total AS NetTotal,v.VoucherName Voucher, bm.BranchName 
FROM dbo.Challan Sc
    LEFT OUTER JOIN dbo.ChallanTrans st
        ON sc.ID = st.ChallanId
	LEFT OUTER JOIN(select mr.RefId,sum(Qty)RcptQty,sum(Pcs)RcptPcs,
	 ( sum(IssueQty) -  sum(Qty)) ShQty
	 from dbo.ChallanTrans MR
	 group by mr.RefId
        )mr ON mr.RefId = st.Id
    LEFT OUTER JOIN dbo.acc a
        ON a.Id = sc.AccID
    LEFT OUTER JOIN dbo.acc ag
        ON ag.Id = a.AgentId
    LEFT OUTER JOIN dbo.Acc pg
        ON pg.Id = sc.DelvAccId
    LEFT OUTER JOIN dbo.Acc tr
        ON tr.Id = sc.TransId
    LEFT OUTER JOIN dbo.Product i
        ON i.Id = st.ProductId
		LEFT OUTER JOIN dbo.PGroup ig ON ig.Id = i.GroupId
    LEFT OUTER JOIN dbo.Color ic
        ON ic.Id = st.ColorID
    LEFT OUTER JOIN dbo.Grade gt
        ON gt.ID = st.GradeID
    LEFT OUTER JOIN dbo.Division dm
        ON dm.ID = sc.DivID
    LEFT OUTER JOIN dbo.Branch bm
        ON bm.ID = sc.BranchId
	LEFT OUTER JOIN dbo.voucher v 
		ON v.Id = sc.VoucherId
WHERE st.IsActive=1 and st.IsDeleted=0 and
 	  (CAST(isnull(st.Qty,0)- (ISNULL(mr.RcptQty, 0) +ISNULL(mr.ShQty, 0))  as numeric(18,2)) >0) AND
sc.VoucherDate
      BETWEEN @fromdate AND @todate
      AND sc.CompId = @companyid 
      and (v.VTypeId = 37 or v.VTypeId=44)
	 -- AND 
	 -- (
		--	@vtypeid=0  
		--	OR v.VTypeId=@vtypeid
		--)
		AND 
	  (
			@voucherid=0  
			OR sc.VoucherId=@voucherid
		)
	  AND 
	  (
			@itemgroupid=0 
			OR ig.Id=@itemgroupid
	  )
      AND
      (
          @transid = 0
          OR sc.TransID = @transid
      )
      AND
      (
          @divid = 0
          OR sc.DivID = @divid
      )
    
      AND
      (
          @agent = 0
          OR ag.Id = @agent
      )
      AND
      (
          @partygroup = 0
          OR a.PGroupId =@partygroup
      )
      AND
      (
          @party = 'N'
          OR EXIsts
		  (
    SELECT 1
    FROM dbo.ReportPara rs
    WHERE rs.ReportId = @reportid
          AND a.Id = rs.ParameterValue
          AND rs.ParameterName = 'party'
))
      AND
      (
          @item = 'N'
          OR EXISTS
(

 
    SELECT 1
    FROM dbo.ReportPara rs
    WHERE rs.ReportId = @reportid
          AND i.Id = rs.ParameterValue
          AND rs.ParameterName = 'product'
))
  AND
    (
         @challanType = 0 or sc.ChallanType=@challantype
	)
GO

