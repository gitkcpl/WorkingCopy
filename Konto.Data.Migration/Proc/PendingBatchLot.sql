IF object_id('[dbo].[PendingBatchLot]') IS NULL 
EXEC ('CREATE PROC [dbo].[PendingBatchLot] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[PendingBatchLot]
 	@CompanyId int,
	@VoucherTypeID INT,
	@ChallanType INT = 1
AS
BEGIN
	select o.VoucherNo,o.VoucherDate, ISNULL(CONVERT(Date,Convert(varchar(8),o.VoucherDate),112),'') VouchDate,
    ot.RefNo AS LotNo, o.ChallanNo, o.AccId, ac.AccName, ot.ProductId, p.ProductName , ot.Pcs, ot.Qty, o.Remark, 
	ot.UomId, o.Id, ot.Id TransId
	from dbo.Challan o
	left outer join dbo.ChallanTrans ot on ot.ChallanId = o.Id
	left outer join dbo.Product p on p.Id=ot.ProductId
	LEFT OUTER JOIN dbo.Acc ac on ac.id=o.AccId
	LEFT  OUTER  JOIN dbo.Voucher v on v.Id=o.VoucherId 
	WHERE o.CompId = @CompanyId AND o.ChallanType = @ChallanType
	 AND  v.VTypeId=@VoucherTypeID 
	AND o.IsActive =1 AND o.IsDeleted = 0 
END
GO