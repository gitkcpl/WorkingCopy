IF object_id('[dbo].[PendingReceiptOnInvoice]') IS NULL 
EXEC ('CREATE PROC [dbo].[PendingReceiptOnInvoice] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[PendingReceiptOnInvoice]
 	@CompanyId INT,
	@AccountId INT,
	@VoucherTypeID INT,
	@VoucherTypeID1 INT
AS
BEGIN
	select ch.VoucherNo AS ChallanNo,ch.VoucherDate AS ChlnDate, 
	ch.BillNo, ch.RcdDate, ch.TransId AS TransportId,
	CAST(ch.TotalQty AS numeric(18,2)) AS TotalQty,
	CAST(ch.TotalAmount AS numeric(18,2)) AS NetTotal,
	ch.Id ,ch.VoucherId, 
	ch.DocNo, ch.DocDate,ch.VehicleNo,ch.Remark  
	from dbo.Challan ch
	LEFT OUTER JOIN dbo.Voucher v on v.Id=ch.VoucherId 
	where ch.AccId=@AccountId AND NOT EXISTS ( SELECT 1 FROM dbo.BillTrans bt WHERE bt.RefId = ch.Id AND IsActive = 1 AND IsDeleted = 0 ) 
	AND ch.IsActive = 1 AND ch.IsDeleted = 0 AND (v.VTypeId=@VoucherTypeID OR v.VTypeId = @VoucherTypeID1)
	AND ch.CompId=@CompanyId
END
GO

