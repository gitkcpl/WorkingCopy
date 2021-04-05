IF object_id('[dbo].[BeamprodList]') IS NULL 
EXEC ('CREATE PROC [dbo].[BeamprodList] AS SELECT 1 AS Id') 
GO 
 
ALTER PROCEDURE [dbo].[BeamprodList]
	@fromDate int,
	@ToDate int,
	@CompanyId int,
	@BranchId int ,
	@YearId int,
	@VTypeId int,
	@IsLoading int=0,
	@Deleted int=0

AS
BEGIN

select  d.DivisionName , p.TwistType,
		convert(date,convert(varchar(8), p.VoucherDate),112) VoucherDate
		,p.VoucherNo,v.VoucherName,m.MachineName,p.TwistType as BeamPosition,p.LoadingDate
		,beam.ProductName,yarn.ProductName as YarnName,gray.ProductName as GrayName
		,p.CopsWt as Taka,p.Pallet Denier,p.CartnWt WastagePer,p.Cops NoOfTaka,
		p.ply Length,p.TareWt Pick,p.GrossWt Width,p.Tops Ends,p.NetWt as NetWeight
		 ,p.Id,p.VoucherId
from Prod p
left outer join Division d on d.Id=p.DivId
left outer join Voucher v on p.VoucherId=v.Id
 left outer join Product beam on p.ProductId = beam.Id
left outer join Product yarn on p.CopsProductId =yarn.Id
left outer join Product gray on p.BoxProductId =gray.Id 
left outer join MachineMaster m on m.Id=p.MacId
where p.IsActive = 1 and p.IsDeleted=0 
and(@IsLoading=0 or p.MacId>0)
	--and p.IsClose=0 
	-- and (p.MacId is null)
	and v.VTypeId=@VTypeId
	and p.CompId=@CompanyId 
	and (p.VoucherDate between @FromDate and @ToDate)
	and p.BranchId=@BranchId
	and p.YearId=@YearId
	and p.IsDeleted=@Deleted 
	order by id desc 
END

GO
