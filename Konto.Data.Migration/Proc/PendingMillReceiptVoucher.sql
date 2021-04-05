IF object_id('[dbo].[PendingMillReceiptVoucher]') IS NULL 
EXEC ('CREATE PROC [dbo].[PendingMillReceiptVoucher] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[PendingMillReceiptVoucher]
 	@CompanyId int,
	@AccountId int,
	@VoucherTypeID int,
	@FromDate int,
	@ToDate int,
	@IssueVoucherId int,
	@challanType1 int,
	@ChallanType2 int

AS
BEGIN
	select   c.VoucherNo ,
	c.VoucherDate , 
	ct.LotNo,p.ProductName AS Product,
	ct.ProductId,
	ct.DesignId,ct.GradeId,
	ct.ColorId,
	CAST(ct.Qty AS numeric(18,2)) as TotalQty,
	CAST((ISNULL(ct.Qty,0))  AS numeric(18,2)) as IssueQty,
	CAST((ISNULL(ct.Qty,0)- ISNULL(bill.Qty,0))  AS numeric(18,2)) AS PendingQty,
	ISNULL(ct.Pcs,0)- ISNULL(bill.Pcs,0) AS PendingPcs,
--ISNULL(ct.Pcs,0)  AS PendingPcs,
	CAST(( ISNULL(bill.Qty,0))  AS numeric(18,2)) as ReceiptQty,
	ct.rate AS Rate,ct.Gross AS  Total,
	ISNULL(ct.DiscPer,0) AS Disc,
	ISNULL(ct.Disc,0) AS DiscAmt,
	ISNULL(ct.FreightRate,0) AS FreightRate,
	ISNULL(ct.Freight,0) AS Freight,
	isnull(ct.CgstPer,0) Cgst,
	ISNULL(ct.Cgst,0) CgstAmt,
	isnull(ct.Igst,0) IgstAmt,
	ISNULL(ct.IgstPer,0) Igst,
	ISNULL(ct.CessPer,0) AS Cess,
	ISNULL(ct.Cess,0) AS CessAmt,
	ct.SgstPer Sgst,
	ct.Sgst SgstAmt,
	ct.Total AS NetTotal,
	ct.UomId,
	ct.Total AS Total,
	CAST(ct.Qty  as numeric(18,2)) as Qty,
	ct.LotNo,	
	ct.Cops,
	ct.Id TransId ,c.Id,c.VoucherId	,ct.BatchId
	from Challan c
	left outer join ChallanTrans ct on ct.ChallanId=c.Id
	left outer join Product p on p.Id=ct.ProductId
	LEFT OUTER JOIN dbo.TransType tp ON tp.Id = c.ChallanType
	left outer join (select SUM(Qty)as Qty,SUM(Pcs) as Pcs , bt.RefId, bt.RefVoucherId
					 from BillTrans bt
			WHERE bt.IsActive=1 and bt.IsDeleted=0 
			GROUP BY bt.RefId , bt.RefVoucherId
	)bill ON bill.RefId=c.Id AND bill.RefVoucherId = c.VoucherId
	left outer join Voucher v on v.Id=c.VoucherId 
	where c.IsActive=1 and c.IsDeleted=0 and (v.VTypeId=@VoucherTypeID )
	 and c.AccId=@AccountId
	and ((ct.Qty-isnull(bill.Qty,0)) >0)
	and (ISNULL(ct.Pcs,0)- ISNULL(bill.Pcs,0)>0) 
	and (c.VoucherDate between @FromDate and @ToDate)

	Union All

	select   c.VoucherNo ,
	c.VoucherDate , 
	ct.LotNo,p.ProductName AS Product,
	ct.ProductId,
	ct.DesignId,ct.GradeId,
	ct.ColorId,
	CAST(ct.Qty AS numeric(18,2)) as TotalQty,
	CAST((ISNULL(ct.Qty,0))  AS numeric(18,2)) as IssueQty,
	CAST((ISNULL(ct.Qty,0)- ISNULL(outwrd.Qty,0))  AS numeric(18,2)) AS PendingQty,
	ISNULL(ct.Pcs,0)- ISNULL(outwrd.Pcs,0) AS PendingPcs,
--ISNULL(ct.Pcs,0)  AS PendingPcs,
	CAST(( ISNULL(outwrd.Qty,0))  AS numeric(18,2)) as ReceiptQty,
	ct.rate AS Rate,ct.Gross AS  Total,
	ISNULL(ct.DiscPer,0) AS Disc,
	ISNULL(ct.Disc,0) AS DiscAmt,
	ISNULL(ct.FreightRate,0) AS FreightRate,
	ISNULL(ct.Freight,0) AS Freight,
	isnull(ct.CgstPer,0) Cgst,
	ISNULL(ct.Cgst,0) CgstAmt,
	isnull(ct.Igst,0) IgstAmt,
	ISNULL(ct.IgstPer,0) Igst,
	ISNULL(ct.CessPer,0) AS Cess,
	ISNULL(ct.Cess,0) AS CessAmt,
	ct.SgstPer Sgst,
	ct.Sgst SgstAmt,
	ct.Total AS NetTotal,
	ct.UomId,
	ct.Total AS Total,
	CAST(ct.Qty  as numeric(18,2)) as Qty,
	ct.LotNo,	
	ct.Cops,
	ct.Id TransId ,c.Id,c.VoucherId	,ct.BatchId
	from Challan c
	left outer join ChallanTrans ct on ct.ChallanId=c.Id
	left outer join Product p on p.Id=ct.ProductId
	LEFT OUTER JOIN dbo.TransType tp ON tp.Id = c.ChallanType
	left outer join (select SUM(Qty)as Qty,SUM(Pcs) as Pcs , ct.RefId, ct.RefVoucherId
					 from ChallanTrans ct
			WHERE ct.IsActive=1 and ct.IsDeleted=0 
			GROUP BY ct.RefId , ct.RefVoucherId
	)outwrd ON outwrd.RefId=ct.Id AND outwrd.RefVoucherId = c.VoucherId
	left outer join Voucher v on v.Id=c.VoucherId 
	where c.IsActive=1 and c.IsDeleted=0 and v.VTypeId=@IssueVoucherId 
	and c.AccId=@AccountId 
	and ((ct.Qty-isnull(outwrd.Qty,0)) >0)
	and (ISNULL(ct.Pcs,0)- ISNULL(outwrd.Pcs,0)>0)
	and c.ChallanType IN (@challanType1,@ChallanType2) 
	and (c.VoucherDate between @FromDate and @ToDate)
END
GO