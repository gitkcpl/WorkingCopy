IF object_id('[dbo].[PendingOrderonYarnJobCard]') IS NULL 
EXEC ('CREATE PROC [dbo].[PendingOrderonYarnJobCard] AS SELECT 1 AS Id') 
GO

ALTER  PROCEDURE [dbo].[PendingOrderonYarnJobCard]
 	@CompanyId int,
	@VoucherTypeID INT
   -- @Id INT 
AS
BEGIN
	select o.VoucherNo OrderNo,
	o.Id OrderId,
	ot.Id TransId,
	CONVERT(Date,Convert(varchar(8),o.VoucherDate),112)as OrderDate,
	p.ProductName as ProductName,
	c.ColorName ,
	p.ProductName Product,
	ot.DesignId ,
	ot.ProductId,
	acc.AccName ,
	c.ColorCode,acc.Id as AccountId,
	c.Id as ColorId,
	CAST(ot.Qty as numeric(18,2)) as Qty,
	cast((ot.Qty-isnull(ct.OrderQty,0))  as numeric(18,2)) as Balance,
	cast(ot.rate as numeric(18,2)) as Rate,
	cast(ot.NetTotal  as numeric(18,2)) as NetTotal,
	ot.Remark
	from Ord o
	LEFT OUTER JOIN OrdTrans ot on ot.OrdId=o.Id
	LEFT OUTER JOIN Product p on p.Id=ot.ProductId
	LEFT OUTER JOIN Color c on c.Id = ot.ColorId
	LEFT OUTER JOIN dbo.Product pd ON pd.Id = ot.DesignId
	LEFT OUTER JOIN Division d on d.Id=ot.DivisionId 
	LEFT OUTER JOIN dbo.Acc acc ON acc.Id = o.AccId 
	LEFT OUTER JOIN (select SUM(ISNULL(jc.ConsumeQty,0)) OrderQty,jc.RefId  from dbo.JobCardTrans jc
			where jc.IsActive=1 and jc.IsDeleted=0 and jc.RefId is not NULL
            GROUP BY jc.RefId
	)ct on ct.RefId=ot.Id
	left outer join Voucher v on v.Id = o.VoucherId 
	WHERE o.CompId = @CompanyId AND v.VTypeId = @VoucherTypeID
	AND ((ot.Qty-isnull(ct.OrderQty,0)) >0) AND o.IsActive =1 AND o.IsDeleted = 0 and ot.OrdStatus = 'Approved'
END
GO

