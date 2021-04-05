IF object_id('[dbo].[pending_gate_entry_for_grn]') IS NULL 
EXEC ('CREATE PROC [dbo].[pending_gate_entry_for_grn] AS SELECT 1 AS Id') 
GO


ALTER  PROCEDURE [dbo].[pending_gate_entry_for_grn]
 	@CompanyId int,
	@AccountId int,
	@VoucherTypeID int=53
AS
BEGIN
	select o.Id,o.VoucherId, o.VoucherNo SrNo,
	ISNULL(CONVERT(Date,Convert(varchar(8),o.VoucherDate),112),'') EntryDate
	from Ord o
	left outer join Voucher v on v.Id=o.VoucherId 
	WHERE
	  o.CompId = @CompanyId
	 AND  (v.VTypeId=@VoucherTypeID) and o.AccId=@AccountId
	AND o.IsActive =1 AND o.IsDeleted = 0
	and (NOT EXISTS(SELECT 1 FROM Challan ct where ct.RefId =o .Id 
	and ct.RefVoucherId = o.VoucherId))
END



GO

