IF object_id('[dbo].[PendingBeamLoading]') IS NULL 
EXEC ('CREATE PROC [dbo].[PendingBeamLoading] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[PendingBeamLoading]
 	@CompanyId int,
	@BLVoucherTypeID int,
	@INVoucherTypeID int

AS
BEGIN

SELECT p.Id,p.RowId,p.TransId,p.SrNo,p.ProductId,p.GradeId,
p.ColorId,p.PackId,p.MacId,p.SubGradeId,p.TwistType,p.CompId,
      p.YearId,p.VoucherId,p.VoucherDate,p.VoucherNo,p.RefId
      ,CAST(ISNULL(p.Ply,0) - ISNULL(Beam.Meter,0) AS INT) Ply  ,p.Cops ,p.CopsWt,p.CartnWt,p.GrossWt,p.TareWt
      ,p.NetWt,p.DivId,p.BranchId,p.JobId,p.CopsProductId
      ,p.CopsRate,p.BoxProductId,p.BoxRate,p.PackEmpId
      ,p.CheckEmpId,p.PalletProductId,p.PlyProductId,p.DrawingDate
      ,p.WarpingDate,p.CloseDate,p.ProdStatus,p.Tops,p.Pallet
      ,p.CurrQty,p.FinQty,p.IssueRefId,p.IssueRefVoucherId
      ,p.Remark,p.IsClose,p.CreateDate,p.CreateUser,p.IpAddress
	  ,p.ModifyDate,p.ModifyUser,p.IsActive,p.IsDeleted,GETDATE() as LoadingDate
from Prod p
left outer join Voucher v on p.VoucherId=v.Id
LEFT OUTER JOIN ( SELECT BeamId, SUM(Mtr) Meter FROM dbo.TakaBeam GROUP BY BeamId) Beam ON Beam.BeamId = p.Id
WHERE  p.IsActive = 1 and p.IsDeleted=0 
	and p.IsClose=0 and (p.MacId is null) and v.VTypeId=@BLVoucherTypeID


 union all

 select  p.Id,p.RowId,p.TransId,p.SrNo,p.ProductId,p.GradeId,
p.ColorId,p.PackId,p.MacId,p.SubGradeId,p.TwistType,p.CompId,
      p.YearId,p.VoucherId,p.VoucherDate,p.VoucherNo,p.RefId
      ,CAST(ISNULL(p.Ply,0) - ISNULL(Beam.Meter,0) AS INT) Ply  ,p.Cops ,p.CopsWt,p.CartnWt,p.GrossWt,p.TareWt
      ,p.NetWt,p.DivId,p.BranchId,p.JobId,p.CopsProductId
      ,p.CopsRate,p.BoxProductId,p.BoxRate,p.PackEmpId
      ,p.CheckEmpId,p.PalletProductId,p.PlyProductId,p.DrawingDate
      ,p.WarpingDate,p.CloseDate,p.ProdStatus,p.Tops,p.Pallet
      ,p.CurrQty,p.FinQty,p.IssueRefId,p.IssueRefVoucherId
      ,p.Remark,p.IsClose,p.CreateDate,p.CreateUser,p.IpAddress
	  ,p.ModifyDate,p.ModifyUser,p.IsActive,p.IsDeleted,GETDATE() as LoadingDate
 from Challan c
 left outer join prod p on p.RefId=c.Id
 left outer join Product pd on pd.Id=p.ProductId
 left outer join ProductType pt on pt.Id=pd.PTypeId
 left outer join Voucher v on v.Id=c.VoucherId
 LEFT OUTER JOIN ( SELECT BeamId, SUM(Mtr) Meter FROM dbo.TakaBeam GROUP BY BeamId) Beam ON Beam.BeamId = p.Id
 where c.IsActive=1 and c.IsDeleted=0 and v.VTypeId=@INVoucherTypeID
 and pd.PTypeId=5 and p.IsActive = 1 and p.IsDeleted=0 
	and p.IsClose=0 and (p.MacId is null) anD p.ProdStatus LIKE 'STOCK' 
	END
GO

