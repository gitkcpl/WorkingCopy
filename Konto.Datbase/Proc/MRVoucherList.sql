CREATE PROCEDURE [dbo].[MRVoucherList]
	@VoucherId int,
	@YearId int,
	@CompanyId int,
	@BranchId int,
	@FromDate int,
	@ToDate int
AS
BEGIN
	select bm.AccId,ac.AccName,bm.AgentId,ag.AccName as AgentName,
	bm.Auth,bm.BillNo,bm.BillType,bm.BookAcId,bac.AccName as BookName,
	bm.BranchId,bm.CompId,bm.CreateDate,bm.CreateUser,
	bm.Currency,bm.CustomA,bm.CustomP,bm.DelvAccId,bm.DelvAdrId,
	bm.DivisionId,bm.DName,bm.DocDate,bm.DocNo,bm.Duedays,bm.EmpId,
	e.EmpName,bm.EwayBillNo,bm.ExchRate,bm.Extra1,bm.Extra2,bm.HasteId,
	bm.IpAddress,bm.IsActive,bm.IsDeleted,bm.Itc,bm.ModeofTrans,bm.ModifyDate,
	bm.ModifyUser,bm.Rcm,bm.RcdDate,bm.RefNo,bm.Remarks,bm.RequireDate,
	bm.RowId,bm.StoreId,bm.SpecialNotes,bm.StateId,bm.TcsAmt,bm.TcsPer,bm.TdsAmt,
	bm.TdsPer,bm.TotalAmount,bm.TotalPcs,bm.TotalQty,bm.TransId,bm.TypeId
	,bm.VDate,bm.VehicleNo,bm.VoucherDate,bm.VoucherId,bm.VoucherNo
	from BillMain as bm
	left outer join Acc ac on bm.AccId=ac.Id
	left outer join Acc ag on ag.Id=bm.AgentId
	left outer join Acc bac on bac.Id=bm.BookAcId
	left outer join Emp e on e.Id=bm.EmpId
	where bm.VoucherId=@VoucherId
	and bm.YearId=@YearId
	and bm.CompId=@CompanyId
	and bm.BranchId=@BranchId
	and (bm.VoucherDate between @FromDate and @ToDate)
END