IF object_id('[dbo].[PendingJobCard]') IS NULL 
EXEC ('CREATE PROC [dbo].[PendingJobCard] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[PendingJobCard]
 	@CompanyId int,
	@VoucherTypeID int
AS
BEGIN
	SELECT ot.Id TransId,o.VoucherNo, o.RefNo,
	CONVERT(Date,Convert(varchar(8),o.VoucherDate),112)as VouchDate
	 ,acc.AccName Party,acc.Id as PartyId,p.Id ProductId,p.ProductName,d.Id DesignId,d.ProductName Design,c.Id ColorId ,c.ColorName ,ot.Qty TotalQty, ot.Qty - ISNULL(ct.Qty,0) PendingQty,ot.Remark,o.Id 
	from dbo.Ord o 
	LEFT OUTER JOIN dbo.OrdTrans ot ON ot.OrdId = o.Id
	LEFT OUTER JOIN dbo.Product p ON p.Id = ot.ProductId
	LEFT OUTER JOIN dbo.Product d ON d.Id = ot.DesignId
	LEFT OUTER JOIN dbo.Color c ON c.Id = ot.ColorId
	LEFT OUTER JOIN dbo.Acc acc ON acc.Id = o.AccId 	
	LEFT OUTER JOIN dbo.Voucher v on v.Id = o.VoucherId
	LEFT OUTER JOIN (select SUM(jc.ConsumeQty) Qty,jc.RefId  from dbo.JobCardTrans jc
			where jc.IsActive=1 and jc.IsDeleted=0 and jc.RefId is not NULL
            GROUP BY jc.RefId
	)ct on ct.RefId=ot.Id
	WHERE o.CompId = @CompanyId AND  v.VTypeId=4 AND ((ot.Qty-isnull(ct.Qty,0)) >0)
	AND o.IsActive =1 AND o.IsDeleted = 0 
	
END
GO

