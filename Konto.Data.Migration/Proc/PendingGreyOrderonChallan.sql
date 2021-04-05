IF object_id('[dbo].[PendingGreyOrderonChallan]') IS NULL 
EXEC ('CREATE PROC [dbo].[PendingGreyOrderonChallan] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[PendingGreyOrderonChallan]
 	@CompanyId int,
	@AccountId int,
	@VoucherTypeID int,
	@ordStatus varchar(100)='APPROVED'
AS
BEGIN
	select o.VoucherNo OrderNo,o.VoucherDate, 
     CONVERT(DATETIME2, CONVERT(VARCHAR(8), o.VoucherDate), 112) OrderDate,
    p.ProductName as Product,
	CAST(ot.Qty as numeric(18,2)) as OrderQty,
	cast((ot.Qty-isnull(ct.Pcs,0))  as numeric(18,2)) as PendingQty,
	ot.ProductId,ot.ColorId,ot.Cut,ot.DesignId,ot.GradeId,
	ot.UomId,ot.NoOfLot 
	,cast(ot.rate as numeric(18,2)) as rate,
	ot.Disc,ot.Id as TransId,o.Id,o.VoucherId, o.Remarks,
  tm.Sgst,tm.Cgst,tm.Igst
  
	from Ord o
	left outer join OrdTrans ot on ot.OrdId=o.Id
	left outer join Product p on p.Id=ot.ProductId
  LEFT OUTER JOIN TaxMaster tm  ON tm.Id = p.TaxId
	left outer join (select sum(Qty)as Qty , SUM(ct.Pcs) Pcs, ISNULL(ct.RefId,0) AS RefId, ISNULL(ct.RefVoucherId,0) AS RefVoucherId  from ChallanTrans ct
			where ct.IsActive=1 and ct.IsDeleted=0 AND ct.RefId IS NOT NULL
			group by ct.RefId, ISNULL(ct.RefVoucherId,0)
	)ct on ct.RefId=ot.Id AND ct.RefVoucherId = o.VoucherId
	left outer join Voucher v on v.Id=o.VoucherId 
	WHERE ot.OrdStatus = @ordStatus--'APPROVED' 

	AND  o.CompId = @CompanyId
	 AND  v.VTypeId=@VoucherTypeID AND EXISTS (SELECT 1 FROM Acc ac WHERE ac.PGroupId = o.PGroupId AND ac.Id=@AccountId)
	and ((ot.Qty-isnull(ct.Pcs,0)) >0) AND o.IsActive =1 AND o.IsDeleted = 0 AND ot.IsDeleted=0
	--and (o.VoucherDate between @FromDate and @ToDate)
END
GO
