CREATE  PROC [dbo].[sales_reg]
    @fromdate INT = 20190401,
    @todate INT = 20200331,
    @companyid INT = 1,
    @reportid INT = 0,
    @party VARCHAR(1) = 'N',
	@book VARCHAR(1) = 'N',
    @agent INT = 0,
    @city VARCHAR(1) = 'N',
    @area VARCHAR(1) = 'N',
    @partygroup INT = 0,
    @bookid INT = 0,
    @item VARCHAR(1) = 'N',
    @color VARCHAR(1) = 'N',
	@RCM VARCHAR(3) = 'NO',
    @voucherid INT = 0,
    @branchid INT = 0,
    @transid INT = 0,
    @divid INT = 0,
	@vtypeid INT=0,
	@groupid INT =0
AS

SELECT cm.Id AS CompanyId,bm.Id AS SalesID,
       bm.VoucherId,
       bm.VoucherDate,
       CONVERT(DATE, CONVERT(VARCHAR(8), bm.VoucherDate), 112) BillDate,
       'Q-'
       + CAST(FLOOR(((12 + MONTH(CONVERT(DATE, CONVERT(VARCHAR(8), bm.VoucherDate))) - 4) % 12) / 3) + 1 AS VARCHAR(1)) Qtr,
       DATENAME(MONTH, CONVERT(DATE, CONVERT(VARCHAR(8), bm.VoucherDate), 112)) AS [Month],
       CASE
           WHEN MONTH(CONVERT(DATE, CONVERT(VARCHAR(8), bm.VoucherDate), 112)) <= 3 THEN
               CAST(YEAR(CONVERT(DATE, CONVERT(VARCHAR(8), bm.VoucherDate), 112)) - 1 AS VARCHAR(4)) + '-'
               + CAST(YEAR(CONVERT(DATE, CONVERT(VARCHAR(8), bm.VoucherDate), 112)) AS VARCHAR(4))
           ELSE
               CAST(YEAR(CONVERT(DATE, CONVERT(VARCHAR(8), bm.VoucherDate), 112)) AS VARCHAR(4)) + '-'
               + CAST(YEAR(CONVERT(DATE, CONVERT(VARCHAR(8), bm.VoucherDate), 112)) + 1 AS VARCHAR(4))
       END AS FY,
	        CASE
           WHEN bm.BillType = 'DEBIT NOTE' THEN
               'D'
           ELSE
		  'C'
       END AS NoteType,
	   bm.SpecialNotes AS NoteReason,
	   bm.Extra1 AS Against,
       ISNULL(bm.BillNo,bm.VoucherNo) AS BillNo,
       bm.RefNo ChallanNo,
       CAST(CAST(bm.BillNo AS VARBINARY(MAX)) AS BIGINT) [SortBillNo],
	   bm.Extra1 AS OrderNo,
       bm.AccId AS AccountId,
       ac.AccName Party,
       ISNULL(bm.BookAcId,bt.ToAccId) AS SalesAcId,
       ISNULL(bk.AccName,toac.AccName) SalesBook,
       v.VoucherName Voucher,
	   bm.AgentId,ag.AccName Agent,
       bm.TotalPcs,
       bm.TotalQty,
	   bm.GrossAmount AS TotalAmount,
       bm.TotalAmount + bm.TcsAmt AS BillAmount,
       bm.EwayBillNo,
       bm.Remarks AS SalesRemark,
       bm.DocNo AS LrNo,
	   bm.DocDate AS LrDate,
       tr.AccName trans_name,
	   bm.TransId AS TransportID,
	   tr.RowId AS RowID,
       bt.ProductId AS ItemID,
       p.ProductName AS Item,
       ISNULL(p.HsnCode,bt.HsnCode) AS HsnCode,
       bt.ColorId AS ColorID,
       cl.ColorName AS Color,
       cl.ColorCode,
       p.ProductCode,
       p.BarCode,
       bt.GradeId AS GradeID,
       gd.GradeName,
       bt.DesignId,
       dm.ProductName DesignNo,
       bt.Cut,
       bt.Pcs,
       bt.Qty,
       bt.UomId AS UnitID,
	   bt.Remark ItemRemark,
       um.UnitCode,
       bt.Rate,
       bt.Total,
       bt.Disc AS DiscPer,
       bt.DiscAmt AS DiscAmount,
       bt.FreightRate,
	   bt.Freight,	 
       bt.TcsPer,
       bt.TcsAmt,
       bt.OtherAdd AddAmt,
       bt.OtherLess AS LessAmt,
       bt.NetTotal - ISNULL(bt.Sgst, 0) - ISNULL(bt.Cgst, 0) - ISNULL(bt.Igst, 0) - ISNULL(bt.Cess, 0) AS TaxableValue,
       bt.CgstPer AS Cgst,
       bt.Cgst AS CgstAmt,
       bt.SgstPer AS Sgst,
       bt.Sgst AS SgstAmt,
       bt.IgstPer AS Igst,
       bt.Igst AS IgstAmt,
       bt.CessPer AS Cess,
       bt.Cess AS CessAmt,
       bt.NetTotal,
	   ac.GstIn,
       bt.LotNo,
	   cm.CompName,
	   cm.PrintName CompanyPrint,
	   acb.Address1+acb.Address2 AS PartyAddress,
	   pct.CityName AS City,
	   pa.AreaName AS Area,
	   pst.StateName AS State_Name,
	   pst.GstCode AS StatCode,
	   acb.MobileNo AS PartyMobile,
	   acb.Email AS PartyEmail,
	   cm.Address1 CAddress1,
	   cm.Address2 CAddress2,
	   cm.Pincode CPinCode,
	   c.CityName CCity,
	   st.StateName CState,
	   cm.Mobile CMobile,cm.Phone CPhone,cm.GstIn CGstin,cm.PanNo,cm.Email CEmail
FROM dbo.BillMain bm
    LEFT OUTER JOIN dbo.BillTrans bt
        ON bm.Id = bt.BillId
    LEFT OUTER JOIN dbo.Product p
        ON p.Id = bt.ProductId
	LEFT OUTER JOIN dbo.Acc toac
		ON toac.Id = bt.ToAccId
    LEFT OUTER JOIN dbo.Product dm
        ON dm.Id = bt.DesignId
    LEFT OUTER JOIN dbo.Color cl
        ON cl.Id = bt.ColorId
    LEFT OUTER JOIN dbo.Grade gd
        ON gd.Id = bt.Id
    LEFT OUTER JOIN dbo.Acc ac
        ON ac.Id = bm.AccId
	LEFT OUTER JOIN dbo.AccBal acb
        ON acb.AccId = ac.Id AND acb.CompId = @companyid
    LEFT OUTER JOIN dbo.City pct
	    ON pct.Id = acb.CityId
    LEFT OUTER JOIN dbo.Area pa
	    ON pa.Id = acb.AreaId
    LEFT OUTER JOIN dbo.State pst
	ON pst.Id = pct.StateId
    LEFT OUTER JOIN dbo.Acc bk
        ON bk.Id = bm.BookAcId
    LEFT OUTER JOIN dbo.Acc ag
        ON ag.Id = ac.AgentId
    LEFT OUTER JOIN dbo.Acc tr
        ON tr.Id = bm.TransId
    LEFT OUTER JOIN dbo.Voucher v
        ON v.Id = bm.VoucherId
    LEFT OUTER JOIN dbo.Uom um
        ON um.Id = bt.UomId
    LEFT OUTER JOIN dbo.Company cm
        ON cm.Id = bm.CompId
    LEFT OUTER JOIN dbo.City c
        ON cm.CityId = c.Id
    LEFT OUTER JOIN dbo.[State] st
        ON st.Id = c.StateId
    LEFT OUTER JOIN dbo.Challan ch
        ON ch.Id = bt.RefId
           AND ch.VoucherId = bt.RefVoucherId

WHERE bm.IsActive = 1 AND bm.IsDeleted = 0 AND bt.IsActive = 1 AND bt.IsDeleted = 0 AND bm.VoucherDate
      BETWEEN @fromdate AND @todate
      AND bm.CompId = @companyid AND (@vtypeid=0 or v.VTypeId=@vtypeid)
      AND
      (
          @transid = 0
          OR tr.Id = @transid
      )
	  AND
	  (
	    @groupid = 0
		OR dm.GroupId = @groupid
	  )
      AND
      (
          @divid = 0
          OR bm.DivisionId = @divid
      )
      AND
      (
          @agent = 0
          OR bm.AgentId = @agent
      )
      AND
      (
          @partygroup = 0
          OR ac.PGroupId = @partygroup
      )
	      AND
      (
          @RCM = 'NO'
          OR bm.Rcm = @RCM
      )
      AND
      (
          @voucherid = 0
          OR bm.VoucherId = @voucherid
      )
      AND
      (
          @branchid = 0
          OR bm.BranchId = @branchid
      )
      AND
      (
          @party = 'N'
          OR EXISTS
(
    SELECT 1
    FROM dbo.ReportPara rs
    WHERE rs.ReportId = @reportid
          AND ac.Id = rs.ParameterValue
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
          AND p.Id = rs.ParameterValue
          AND rs.ParameterName = 'product'
)
      )
      AND
      (
          @book = 'N'
          OR (EXISTS
(
    SELECT 1
    FROM dbo.ReportPara rs
    WHERE rs.ReportId = @reportid
          AND bm.BookAcId = rs.ParameterValue
          AND rs.ParameterName = 'book'
)
OR EXISTS
(
    SELECT 1
    FROM dbo.ReportPara rs
    WHERE rs.ReportId = @reportid
          AND bt.ToAccId = rs.ParameterValue
          AND rs.ParameterName = 'book'
)

)

      );