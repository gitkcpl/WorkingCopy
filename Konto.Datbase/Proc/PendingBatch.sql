CREATE PROCEDURE PendingBatch
	@CompanyID int,
	@VoucherId int,
	@FromDate int,
	@ToDate int
AS
BEGIN
	  select  b.Id,b.BranchId,b.CompId,
	  b.Cross_Section,
	  b.DivId,b.ItemId,b.Remark,b.RowId,b.ShadeId,b.VoucherDate,
	  b.VoucherId,b.VoucherNo,b.YearId
  from Batch b
  where b.Id not in(
   select BatchId Id from Prod p where p.IsActive=1 and p.IsDeleted=0 and
     p.CompId=@CompanyID and p.VoucherId=@VoucherId)
and b.IsActive=1 and b.IsDeleted=0 and b.CompId=@CompanyID
END