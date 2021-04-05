IF object_id('[dbo].[PendingMillIssue]') IS NULL 
EXEC ('CREATE PROC [dbo].[PendingMillIssue] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[PendingMillIssue]
 	@CompanyId int
AS
BEGIN
	select o.VoucherNo,o.VoucherDate, ISNULL(CONVERT(Date,Convert(varchar(8),o.VoucherDate),112),'') VouchDate,
    ot.RefNo AS LotNo, o.ChallanNo, o.AccId, ac.AccName, ot.ProductId, p.ProductName , ot.Pcs, ot.Qty, o.Remark, 
	ot.UomId, o.Id, ot.Id TransId, o.VoucherId
	from dbo.Challan o
	left outer join dbo.ChallanTrans ot on ot.ChallanId = o.Id
	left outer join dbo.Product p on p.Id=ot.ProductId
	LEFT OUTER JOIN dbo.Acc ac on ac.id=o.AccId
	left outer join (select sum(Qty)as Qty , ISNULL(ct.RefId,0) AS RefId, ISNULL(ct.RefVoucherId,0) AS RefVoucherId  from dbo.ChallanTrans ct
			where ct.IsActive=1 and ct.IsDeleted=0
			group by ct.RefId, ISNULL(ct.RefVoucherId,0)
	)ct on ct.RefId=ot.Id AND ct.RefVoucherId = o.VoucherId
	left outer join dbo.Voucher v on v.Id=o.VoucherId 
	WHERE o.CompId = @CompanyId 
	 AND  (v.VTypeId=5 OR v.VTypeId = 32)
	and ((ot.Qty-isnull(ct.Qty,0)) >0) AND o.IsActive =1 AND o.IsDeleted = 0 AND p.PTypeId =2
	--and (o.VoucherDate between @FromDate and @ToDate)
END

GO

