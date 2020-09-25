IF object_id('[dbo].[TakaprodList]') IS NULL 
EXEC ('CREATE PROC [dbo].[TakaprodList] AS SELECT 1 AS Id') 
GO


 ALTER PROCEDURE [dbo].[TakaprodList]
 	@CompanyId int,
	@VoucherID int,
	@FromDate int,
	@ToDate int
AS
BEGIN

select  p.Id,p.RowId,p.TransId,p.SrNo,p.ProductId,p.GradeId,
p.ColorId,p.PackId,p.MacId,p.SubGradeId,p.TwistType,p.CompId,
      p.YearId,p.VoucherId,p.VoucherDate,p.VoucherNo,p.RefId
      ,p.Ply  ,p.Cops ,p.CopsWt,p.BoxWt,p.CartnWt,p.GrossWt,p.TareWt
      ,p.NetWt,p.DivId,p.BranchId,p.JobId,p.CopsProductId
      ,p.CopsRate,p.BoxProductId,p.BoxRate,p.PackEmpId
      ,p.CheckEmpId,p.PalletProductId,p.PlyProductId,p.DrawingDate
      ,p.WarpingDate,p.CloseDate,p.ProdStatus,p.Tops,p.Pallet
      ,p.CurrQty,p.FinQty,p.IssueRefId,p.IssueRefVoucherId
      ,p.Remark,p.IsClose,p.CreateDate,p.CreateUser,p.IpAddress
	  ,p.ModifyDate,p.ModifyUser,p.IsActive,p.IsDeleted,p.LoadingDate as LoadingDate
	  ,m.MachineName,pd.ProductName,c.ColorName YarnName,g.GradeName as GrayName,
	  F.EmpName as DrawerName,che.EmpName as warperName,mendor.EmpName as MendorName
from Prod p
left outer join Voucher v on p.VoucherId=v.Id
left outer join MachineMaster m on m.Id=p.MacId
left outer join Product pd on pd.Id=p.ProductId
left outer join Color c on c.Id=p.ColorId
left outer join Grade g on g.Id=p.GradeId
left outer join Emp F on p.PackEmpId =F.Id
left outer join Emp che on p.CheckEmpId =che.Id
left outer join Emp mendor on p.JobId =mendor.Id


where p.IsActive = 1 and p.IsDeleted=0 
	and p.IsClose=0  
	and v.VTypeId=@VoucherID
	and p.CompId=@CompanyId and (p.VoucherDate between @FromDate and @ToDate)

 
END
GO

