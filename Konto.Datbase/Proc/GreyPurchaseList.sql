
Create PROCEDURE [dbo].[GreyPurchaseList]
 	@CompanyId int,
	@VoucherTypeID int,
	@YearId int,
	@FromDate int,
	@ToDate int
AS
BEGIN
	select  bm.AccId,ac.AccName,bm.AddrId,bm.AgentId,bm.Auth,bm.BillNo,bm.BillType,bm.BookAcId
	,bm.BranchId,bm.CompId,bm.Currency,bm.CustomA,bm.CustomP,bm.DelvAccId,bm.DelvAdrId,
	bm.DivisionId,bm.DName,bm.DocDate,bm.DocNo,bm.Duedays,bm.EmpId,bm.EwayBillNo,
	bm.ExchRate,bm.Extra1,bm.Extra2,bm.HasteId,bm.Id, bm.IpAddress,bm.IsActive,bm.IsDeleted
	,bm.GrossAmount,bm.Itc,bm.ModeofTrans,bm.OceanFrtA,bm.OceanFrtP,bm.PortCode,bm.RcdDate,
	bm.Rcm,bm.RefNo,bm.Remarks,bm.RequireDate,bm.RowId,bm.SpecialNotes,bm.StateId,bm.StoreId,bm.TcsAmt
	,bm.TcsPer,bm.TdsAmt,bm.TdsPer,bm.TotalAmount,bm.TotalPcs,bm.TotalQty,bm.TransId,bm.TypeId,
	bm.VDate,bm.VehicleNo,bm.VoucherDate,bm.VoucherId,bm.YearId
	from BillMain bm
	left outer join Acc ac on ac.Id=bm.AccId
	left outer join Voucher v on v.Id=bm.VoucherId
	where bm.CompId=@CompanyId and bm.YearId=@YearId 
	and v.VTypeId=@VoucherTypeID
	and (bm.VoucherDate between @FromDate and @ToDate)

END
