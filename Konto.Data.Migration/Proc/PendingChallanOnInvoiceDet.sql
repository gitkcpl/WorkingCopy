IF object_id('[dbo].[PendingChallanOnInvoiceDet]') IS NULL 
EXEC ('CREATE PROC [dbo].[PendingChallanOnInvoiceDet] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[PendingChallanOnInvoiceDet]
 	@CompanyId INT,
	@ChallanId INT 
AS
BEGIN
	select CASE WHEN v.vtypeid=6 or v.vtypeid=46 THEN ch.VoucherNo ELSE ch.ChallanNo END AS ChallanNo,o.Id AS OrdId,ch.VoucherDate AS ChlnDate, 
	ch.BillNo, ch.RcdDate, ch.TransId AS TransportId,CAST(ch.TotalQty AS numeric(18,2)) AS TotalQty,
	p.ProductName AS Product,o.VoucherNo AS OrderNO, o.VoucherDate AS OrdDate,cht.ProductId,	
	cht.ColorId,cht.GradeId,
	ISNULL(cht.DiscPer,0) AS Disc,
	ISNULL(cht.Disc,0) AS DiscAmt,
	ISNULL(cht.CgstPer,0) AS Cgst,
	ISNULL(cht.Cgst,0) AS CgstAmt,
	ISNULL(cht.SgstPer,0) AS Sgst,
	ISNULL(cht.Sgst,0) AS SgstAmt,
	ISNULL(cht.IgstPer,0) AS Igst,
	ISNULL(cht.Igst,0) AS IgstAmt,
	ISNULL(cht.FreightRate,0) AS FreightRate,
	ISNULL(cht.Freight,0) AS Freight,
	ISNULL(cht.CessPer,0) AS Cess,
	ISNULL(cht.Cess,0) AS CessAmt,
	ISNULL(cht.OtherAdd,0) AS OtherAdd,
	ISNULL(cht.OtherLess,0) AS OtherLess,
	cht.UomId,cht.Pcs,cast(cht.Gross  as numeric(18,2)) as Total,
	CAST(cht.Total  as numeric(18,2)) as NetTotal,cht.LotNo,
	CAST(cht.Qty  as numeric(18,2)) as Qty,
	CAST(cht.rate as numeric(18,2)) as Rate,cht.Id as TransId,ch.Id ,ch.VoucherId, 
	ch.DocNo, ch.DocDate,ch.VehicleNo, cht.Remark AS ItemRemark, ch.Remark AS Remark 
	,design.ProductCode as DesignNo,grd.GradeName,clr.ColorName,cht.DesignId, cht.LotNo
	from dbo.Challan ch
	LEFT OUTER JOIN dbo.ChallanTrans cht on cht.ChallanId=ch.Id
	LEFT OUTER JOIN dbo.Product p on p.Id=cht.ProductId 
	LEFT OUTER JOIN dbo.Voucher v on v.Id=ch.VoucherId 
	LEFT OUTER JOIN dbo.OrdTrans ot ON ot.Id = cht.RefId
	LEFT OUTER JOIN dbo.Ord o ON o.Id = ot.OrdId 
	LEFT OUTER JOIN dbo.Product design ON design.Id=cht.DesignId
	LEFT OUTER JOIN dbo.Grade grd ON grd.Id=cht.GradeId
	LEFT OUTER JOIN dbo.Color clr ON clr.Id=cht.ColorId
	where ch.CompId=@CompanyId AND cht.ChallanId =  @ChallanId
	AND cht.IsActive = 1 AND cht.IsDeleted = 0 
	--and (o.VoucherDate between @FromDate and @ToDate)
END
GO

