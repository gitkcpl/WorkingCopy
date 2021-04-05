IF object_id('[dbo].[Challan_Reg]') IS NULL 
EXEC ('CREATE PROC [dbo].[Challan_Reg] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[Challan_Reg]
    @fromdate INT = 20190401,
    @todate INT = 20200331,
    @companyid INT = 22,
    @reportid INT = 0,
    @party VARCHAR(1) = 'N',
    @agent INT = 0,
    @city VARCHAR(1) = 'N',
    @area VARCHAR(1) = 'N',
    @partygroup INT = 0,
    @item VARCHAR(1) = 'N',
    @color VARCHAR(1) = 'N',
	@design VARCHAR(1) = 'N',
    @voucherid INT = 0,
    @unitid INT = 0,
    @transid INT = 0,
    @divid INT = 0,
    @itemgroupid INT = 0,
	@itemtype varchar(1) ='N',
	@vtypeid INT = 0,
	@challantype INT = 0,
	@BranchId int =0 
AS
SELECT sc.ID AS SCID,
        CONVERT(DATE, CONVERT(VARCHAR(8), sc.VoucherDate), 112) ScDate,
    --   ISNULL(sc.ChallanNo,sc.VoucherNo) VoucherNo,
	   CASE WHEN ISNULL(sc.ChallanNo,sc.VoucherNo) = 'NA' THEN sc.VoucherNo ELSE sc.ChallanNo END AS VoucherNo,
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
       ISNULL(st.ProductId,st.NProductId) ItemID,
       ISNULL( i.ProductName,p.ProductName) Item,
       ISNULL(st.qty, 0) Qty,
       ISNULL(st.Cops,0) Cops,
       ISNULL(st.Pcs, 1) Box,
	   ISNULL(st.IssuePcs,1) GrayPcs,
	   ISNULL(st.IssueQty,0) GrayMeter,
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
	LEFT OUTER JOIN dbo.Product p
		ON p.Id = st.NProductId
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
WHERE sc.IsActive = 1 AND sc.IsDeleted = 0 AND st.IsActive = 1 AND st.IsDeleted = 0 AND sc.VoucherDate
      BETWEEN @fromdate AND @todate
      AND sc.CompId = @companyid 
	  and (@BranchId=0 or sc.BranchId=@BranchId)
	  AND 
	  (
			@vtypeid=0  
			OR v.VTypeId=@vtypeid
		)
		  AND 
	  (
			@challantype=0  
			OR sc.ChallanType=@challantype
		)
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
)
      
      )
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
)
      )
	        AND
      (
          @design = 'N'
          OR EXISTS
(
    SELECT 1
    FROM dbo.ReportPara rs
    WHERE rs.ReportId = @reportid
          AND st.DesignId = rs.ParameterValue
          AND rs.ParameterName = 'design'
)
      )
	        AND
      (
          @color = 'N'
          OR EXISTS
(
    SELECT 1
    FROM dbo.ReportPara rs
    WHERE rs.ReportId = @reportid
          AND st.ColorId = rs.ParameterValue
          AND rs.ParameterName = 'color'
)
      )
GO

