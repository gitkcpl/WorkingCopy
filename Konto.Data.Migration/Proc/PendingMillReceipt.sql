IF object_id('[dbo].[PendingMillReceipt]') IS NULL 
EXEC ('CREATE PROC [dbo].[PendingMillReceipt] AS SELECT 1 AS Id') 
GO

ALTER  PROCEDURE [dbo].[PendingMillReceipt]
 		@CompanyId int,
	@AccountId int=0,
	@VoucherTypeID int
AS
BEGIN
	select c.VoucherNo ,
	ISNULL(CONVERT(Date,Convert(varchar(8),c.VoucherDate),112),'') VoucherDate,
	ct.LotNo,
	ct.RefNo,
	p.ProductName AS GreyQuality,
	np.ProductName FinishQuality,
	case when ct.NProductId is null or ct.NProductId=0 then ct.ProductId else ct.NProductId end ProductId,

	ct.DesignId,
	ct.ColorId,
	CAST((ISNULL(ct.Qty,0))  AS numeric(18,2)) as IssueQty,
	ISNULL(ct.Pcs,0) AS IssuePcs,
	CAST((ISNULL(ct.Qty,0)- ISNULL(outwrd.Qty,0))  AS numeric(18,2)) AS PendingQty,
	CAST(ISNULL(ct.Pcs,0) - ISNULL(outwrd.Pcs,0) AS INT) AS PendingPcs,
	CAST(( ISNULL(outwrd.Qty,0))  AS numeric(18,2)) as ReceiptQty,
	CAST(ISNULL(outwrd.Pcs,0) AS NUMERIC(18,2)) AS ReceiptPcs,
	ct.rate AS Rate,
	ct.Gross AS  Total,
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
	ct.UomId,
	ct.Id TransId ,c.Id,c.VoucherId,ct.ProductId as OrgProductId
	from Challan c
	left outer join ChallanTrans ct on ct.ChallanId=c.Id
	left outer join Product p on p.Id=ct.ProductId
	LEFT OUTER JOIN dbo.Product np ON np.Id = ct.NProductId
	LEFT OUTER JOIN dbo.TransType tp ON tp.Id = c.ChallanType
	left outer join (select SUM(ct.IssueQty)as Qty,SUM(ct.IssuePcs) as Pcs , ct.RefId, ct.RefVoucherId
					 from ChallanTrans ct
					 left outer join Challan c on c.Id=ct.ChallanId 
					 left outer join Voucher v on v.Id=c.VoucherId
			WHERE ct.IsActive=1 and ct.IsDeleted=0  AND ct.RefId IS NOT NULL
			GROUP BY ct.RefId , ct.RefVoucherId
	)outwrd ON outwrd.RefId=ct.Id AND outwrd.RefVoucherId = c.VoucherId
	left outer join Voucher v on v.Id=c.VoucherId 
	where c.IsActive=1 and c.IsDeleted=0  and (c.AccId=@AccountId or @AccountId =0)
	and ((ct.Qty-isnull(outwrd.Qty,0)) >0)
--	and (ISNULL(ct.Pcs,0)- ISNULL(outwrd.Pcs,0)>0)
	and ((v.VTypeId = @VoucherTypeID OR (v.VtypeId = 44 and @VoucherTypeID=37) or(v.VtypeId = 45 and @VoucherTypeID=38)))
	and (c.ChallanType IN (7,8,11,2) )
	
END
GO

