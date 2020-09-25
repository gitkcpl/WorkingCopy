 
GO

/*-- =============================================
-- Author:		<Author,,>
  Create date: <Create Date,,>
  Description:	<Description,,>
  exec PendingChallanonInvoice 1,21,4
-- =============================================*/
CREATE PROCEDURE [dbo].[PendingChallanOnReturn]
 	@CompanyId int,
	@AccountId int,
	@ChallanTypeID int
AS
BEGIN
	SELECT ch.VoucherNo AS ChallanNo,o.Id AS OrdId,ch.VoucherDate AS ChlnDate,
	CAST(ch.TotalQty AS numeric(18,2)) AS TotalQty,p.ProductName AS Product,
	o.VoucherNo AS OrderNO, o.VoucherDate AS OrdDate,cht.ProductId,isnull(cht.CgstPer,0) AS CgstPer,
	ISNULL(cht.Cgst,0) AS Cgst,cht.ColorId,cht.GradeId,isnull(cht.IgstPer,0) AS IgstPer,isnull(cht.IgstPer,0) AS IgstPer,
	cht.UomId,cht.Pcs,cast(cht.Total  as numeric(18,2)) as NetTotal,cht.LotNo,
	CAST(cht.Qty  as numeric(18,2)) as Qty,cast(cht.rate as numeric(18,2)) as rate,cht.Sgst
	,cht.Sgst,cht.Total,cht.Id as TransId,ch.Id ,ch.VoucherId 
	from dbo.Challan ch
	LEFT OUTER JOIN dbo.ChallanTrans cht on cht.ChallanId=ch.Id
	LEFT OUTER JOIN dbo.Product p on p.Id=cht.ProductId 
	LEFT OUTER JOIN dbo.Voucher v on v.Id=ch.VoucherId 
	LEFT OUTER JOIN dbo.OrdTrans ot ON ot.Id = cht.RefId
	LEFT OUTER JOIN dbo.Ord o ON o.Id = ot.OrdId 
	WHERE ch.ChallanType = @ChallanTypeID and ch.AccId=@AccountId AND cht.Id NOT IN ( SELECT ISNULL(RefId,0) AS RefId FROM dbo.BillTrans ) 
	--and (o.VoucherDate between @FromDate and @ToDate)
END
GO

