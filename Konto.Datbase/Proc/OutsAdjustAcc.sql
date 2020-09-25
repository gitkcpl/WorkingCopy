CREATE  procedure [dbo].[OutsAdjustAcc]
@CompanyId int,
@branchId int,
@fromdate int ,
@todate int,
@AccId int,
@YearId int
as Begin

select btob.Id,bm.VoucherNo BillNo,convert(date,convert(varchar(8), bm.VoucherDate),112) as Date,
bm.TotalAmount BillAmt,convert(numeric(18,2),btob.Amount) PendingAmt
from btob btob 
left outer join BillMain bm on bm.Id=btob.RefId
left outer join BillTrans bt on bt.BillId=bm.Id
where btob.IsActive=1 and btob.IsDeleted=0
and VoucherDate between @fromdate and @todate 
and bt.ToAccId=@AccId
and btob.CompanyId=@CompanyId
and bm.BranchId=@branchId and bm.YearId=@YearId
End