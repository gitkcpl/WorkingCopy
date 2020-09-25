IF object_id('[dbo].[PendingOJC]') IS NULL 
EXEC ('CREATE PROC [dbo].[PendingOJC] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[PendingOJC]
    @CompanyId int,
	@AccountId int,
	@VoucherTypeID int
AS
BEGIN
	select c.VoucherNo, c.AccId PartyId, ac.AccName Party,
	ISNULL(CONVERT(Date,Convert(varchar(8),c.VoucherDate),112),'') VoucherDate,
	c.VoucherNo LotNo,
	c.ChallanNo,
	p.ProductName AS Product,
	cl.ColorName,
	g.GradeName,
	ct.ProductId,
	ct.DesignId,
	ct.ColorId,
	ct.GradeId,
	CAST((ISNULL(ct.Qty,0))  AS numeric(18,2)) as IssueQty,
	ISNULL(ct.Pcs,0) AS IssuePcs,
	CAST((ISNULL(ct.Qty,0)- ISNULL(outwrd.Qty,0))  AS numeric(18,2)) AS PendingQty,
	CAST(ISNULL(ct.Pcs,0)- ISNULL(outwrd.Pcs,0) AS INT) AS PendingPcs,
	CAST(( ISNULL(outwrd.Qty,0))  AS numeric(18,2)) as ReceiptQty,
	ISNULL(outwrd.Pcs,0) AS ReceiptPcs,
	ct.rate AS Rate,
	ct.Gross AS  Total,
	ISNULL(ct.DiscPer,0) AS Disc,
	ISNULL(ct.Disc,0) AS DiscAmt,
	ISNULL(ct.FreightRate,0) AS FreightRate,
	ISNULL(ct.Freight,0) AS Freight,
	isnull(tx.Cgst,0) Cgst,
	ISNULL(ct.Cgst,0) CgstAmt,
	isnull(ct.Igst,0) IgstAmt,
	ISNULL(tx.Igst,0) Igst,
	ISNULL(ct.CessPer,0) AS Cess,
	ISNULL(ct.Cess,0) AS CessAmt,
	ISNULL(tx.Sgst,0) Sgst,
	ISNULL(ct.Sgst,0) SgstAmt,
	p.UomId,
	ct.Id TransId ,c.Id,c.VoucherId
	from dbo.Challan c
	left outer join dbo.ChallanTrans ct on ct.ChallanId=c.Id
	left outer join dbo.Product p on p.Id=ct.ProductId
	left outer join dbo.TaxMaster tx on tx.Id = p.TaxId
	LEFT OUTER JOIN dbo.Acc ac ON ac.Id = c.AccId
	LEFT OUTER JOIN dbo.Color cl ON cl.Id = ct.ColorId
	LEFT OUTER JOIN dbo.Grade g ON g.Id = ct.GradeId
	LEFT OUTER JOIN dbo.TransType tp ON tp.Id = c.ChallanType
	left outer join (select SUM(ct.IssueQty)as Qty,SUM(ct.IssuePcs) as Pcs , ct.RefId, ct.RefVoucherId 
					 from ChallanTrans ct
					 left outer join Challan c on c.Id=ct.ChallanId 
					 left outer join Voucher v on v.Id=c.VoucherId
			WHERE ct.IsActive=1 and ct.IsDeleted=0  AND ct.RefId IS NOT NULL 
			GROUP BY ct.RefId , ct.RefVoucherId
	)outwrd ON outwrd.RefId=ct.Id AND outwrd.RefVoucherId = c.VoucherId
	left outer join Voucher v on v.Id=c.VoucherId 
	where c.IsActive=1 AND c.IsDeleted=0 and (c.AccId=@AccountId or @AccountId =0)
   AND c.ChallanType=2 AND  ((ct.Qty-isnull(outwrd.Qty,0)) >0) AND c.CompId=@CompanyId
   and (v.VTypeId = @VoucherTypeID) AND ct.IsDeleted=0 AND ct.IsActive=1
	
END
GO

