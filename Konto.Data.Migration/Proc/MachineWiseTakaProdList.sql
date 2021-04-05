IF object_id('[dbo].[MachineWiseTakaProdList]') IS NULL 
EXEC ('CREATE PROC [dbo].[MachineWiseTakaProdList] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[MachineWiseTakaProdList]
 	@CompanyId int,
	@VoucherID int,
	@Status varchar(50),
--	@FromDate int,
--	@ToDate int,
	@MachineId int
AS
BEGIN

select p.Id,p.RowId,p.TransId,p.SrNo,p.ProductId,p.GradeId,
       p.ColorId,p.PackId,p.MacId,p.SubGradeId,p.TwistType,p.CompId,
       p.YearId,p.VoucherId,p.VoucherDate,p.VoucherNo,p.RefId
      ,p.Ply  ,tp.Taka Cops ,p.CartnWt,p.GrossWt,p.TareWt
      ,p.NetWt,p.DivId,p.BranchId,p.JobId,p.CopsProductId
      ,p.BoxProductId,p.BoxRate,p.PackEmpId
      ,p.CheckEmpId,p.PalletProductId,p.PlyProductId,p.DrawingDate
      ,p.WarpingDate,p.CloseDate,p.ProdStatus,p.Tops,p.IssueRefId,p.IssueRefVoucherId
      ,p.Remark,p.IsClose,p.CreateDate,p.CreateUser,p.IpAddress
	  ,p.ModifyDate,p.ModifyUser,p.IsActive,p.IsDeleted,p.LoadingDate as LoadingDate
	  ,m.MachineName,pd.ProductName,c.ColorName YarnName,g.GradeName as GrayName,
	  F.EmpName as DrawerName,che.EmpName as warperName,mendor.EmpName as MendorName
	  ,tp.TakaQty as TakaProd, ISNULL(tp.TakaMtr,0) AS CopsWt,p.GrossWt-isnull(tp.TakaQty,0) as BalQty
      ,p.Ply-isnull(tp.TakaMtr,0)  as CopsRate ,0 Pallet,p.ProdStatus
from Prod p
left outer join Voucher v on p.VoucherId=v.Id
left outer join MachineMaster m on m.Id=p.MacId
left outer join Product pd on pd.Id=p.ProductId
left outer join Color c on c.Id=p.ColorId
left outer join Grade g on g.Id=p.GradeId
left outer join Emp F on p.PackEmpId =F.Id
left outer join Emp che on p.CheckEmpId =che.Id
left outer join Emp mendor on p.JobId =mendor.Id
left outer join (SELECT COUNT(tb.BeamId) Taka, sum(Qty)TakaQty,sum(pd.NetWt) TakaMtr,BeamId from TakaBeam tb
                LEFT OUTER JOIN dbo.Prod pd ON pd.Id =tb.ProdId
				where tb.IsActive=1 and tb.IsDeleted=0 
				group by BeamId)tp on tp.BeamId=p.Id

where p.IsActive = 1 and p.IsDeleted=0 
	and p.IsClose=0  
	--and v.VTypeId=@VoucherID
	and p.ProdStatus=@Status
	and p.CompId=@CompanyId -- and (p.VoucherDate between @FromDate and @ToDate)
	and p.MacId=@MachineId

	  
END
GO

