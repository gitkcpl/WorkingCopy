IF object_id('[dbo].[OutwardprodList]') IS NULL 
EXEC ('CREATE PROC [dbo].[OutwardprodList] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[OutwardprodList]
 	@CompanyId int,
	@VoucherID int,
	@RefId int
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
	  ,p.ModifyDate,p.ModifyUser,p.IsActive,p.IsDeleted,p.LoadingDate as LoadingDate,po.TransId as RefTransId
	  ,po.Id as ProdOutId,(po.Qty *-1) Qty,a.AccName Weaver,c.VoucherNo ChallanNo
from Prodout po
left outer join Prod p on p.Id=po.ProdId
left outer join Voucher v on po.VoucherId=v.Id
LEFT OUTER JOIN challan c ON c.Id = p.RefId
LEFT OUTER JOIN acc a ON a.Id =c.AccId
where p.IsActive = 1 and p.IsDeleted=0 
and po.IsActive = 1 and po.IsDeleted=0 
and po.RefId=@RefId
	--and v.VTypeId=@VoucherID
	and p.CompId=@CompanyId --and (p.VoucherDate between @FromDate and @ToDate)

END
GO
