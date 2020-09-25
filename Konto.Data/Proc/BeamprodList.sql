IF object_id('[dbo].[BeamprodList]') IS NULL 
EXEC ('CREATE PROC [dbo].[BeamprodList] AS SELECT 1 AS Id') 
GO 

ALTER PROCEDURE [dbo].[BeamprodList]
 @CompanyId int,
	@VTypeId int,
	@FromDate int,
	@ToDate int
AS
BEGIN

select  p.Id,p.RowId,p.TransId,p.SrNo,p.ProductId,p.GradeId,
p.ColorId,p.PackId,p.MacId,p.SubGradeId,p.TwistType,p.CompId,
      p.YearId,p.VoucherId,p.VoucherDate,p.VoucherNo,p.RefId
      ,p.Ply  ,p.Cops ,p.CopsWt,p.CartnWt,p.GrossWt,p.TareWt
      ,p.NetWt,p.DivId,p.BranchId,p.JobId,p.CopsProductId
      ,p.CopsRate,p.BoxProductId,p.BoxRate,p.PackEmpId
      ,p.CheckEmpId,p.PalletProductId,p.PlyProductId,p.DrawingDate
      ,p.WarpingDate,p.CloseDate,p.ProdStatus,p.Tops,p.Pallet
      ,p.CurrQty,p.FinQty,p.IssueRefId,p.IssueRefVoucherId
      ,p.Remark,p.IsClose,p.CreateDate,p.CreateUser,p.IpAddress
	  ,p.ModifyDate,p.ModifyUser,p.IsActive,p.IsDeleted,p.LoadingDate as LoadingDate
	  ,pd.ProductName,yarn.ProductName as YarnName,
	  gray.ProductName as GrayName,
	  w.EmpName as warperName,d.EmpName as DrawerName
from Prod p
left outer join Voucher v on p.VoucherId=v.Id
 left outer join Product pd on p.ProductId = pd.Id
left outer join Product yarn on p.CopsProductId =yarn.Id
left outer join Product gray on p.BoxProductId =gray.Id
left outer join Emp w on p.CheckEmpId =w.Id
left outer join Emp d on p.PackEmpId =d.Id
where p.IsActive = 1 and p.IsDeleted=0 
	--and p.IsClose=0 -- and (p.MacId is null) 
	and v.VTypeId=@VTypeId
	and p.CompId=@CompanyId and (p.VoucherDate between @FromDate and @ToDate)

 
END
GO
