IF object_id('[dbo].[TakaprodList]') IS NULL 
EXEC ('CREATE PROC [dbo].[TakaprodList] AS SELECT 1 AS Id') 
GO

 
ALTER PROCEDURE [dbo].[TakaprodList]
 	 @VTypeId int,
 @CompanyId int,
 @YearId int,
 @fromDate int,
 @ToDate int,
 @Deleted INT = 0,
 @BranchId int =0
AS
BEGIN

select 
d.DivisionName,p.VoucherNo ,v.VoucherName, convert(date,convert(varchar(8),p.voucherdate),112) VoucherDate,
m.MachineName,pd.ProductName as Quality,design.ProductName as DesignName,p.LoadingDate as FoldDate,
g.GradeName ,p.NetWt Meter,p.GrossWt Weight,p.Cops TotalPcs,
 F.EmpName as Folder,p.CopsRate as FolderRate,
 mendor.EmpName as MendorName,p.BoxRate as MendorRate
 ,che.EmpName as CheckerName,p.FinQty as CheckerRate,p.Remark,

 p.Extra1,b.VoucherNo as BatchNo,c.ColorName,boxitem.ProductName as BoxProduct
 ,copitem.ProductName as CopsProduct,palletitem.ProductName as PalletProduct,p.TwistType,pt.TypeName as PackingType,
 p.Ply,p.BoxWt,p.CartnWt,p.CopsWt,p.CurrQty,p.FinQty,p.LotNo,p.Pallet
 ,p.TareWt,p.Tops,

 p.Id,p.VoucherId
from Prod p
left outer join Division d on d.Id=p.DivId
left outer join Voucher v on p.VoucherId=v.Id
left outer join MachineMaster m on m.Id=p.MacId
left outer join Product pd on pd.Id=p.ProductId
left outer join Product design on design.id =p.PlyProductId
left outer join Product boxitem on boxitem.id =p.BoxProductId
left outer join Product copitem on copitem.id =p.CopsProductId
left outer join Product palletitem on palletitem.id =p.PalletProductId
left outer join Color c on c.Id=p.ColorId
left outer join Grade g on g.Id=p.GradeId
left outer join Emp F on p.PackEmpId =F.Id
left outer join Emp che on p.CheckEmpId =che.Id
left outer join Emp mendor on p.JobId =mendor.Id
left outer join JobCard b on b.Id=p.BatchId
left outer join PackingType pt on pt.Id=p.PackId
where p.IsActive = 1 and p.IsDeleted=@Deleted
	and p.IsClose=0  
	and v.VTypeId=@VTypeId
	and p.CompId=@CompanyId 
	and (p.VoucherDate between @FromDate and @ToDate)
	and p.BranchId=@BranchId
 order by p.VoucherDate desc,p.id desc
END

GO

