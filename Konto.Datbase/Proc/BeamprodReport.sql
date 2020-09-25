CREATE PROCEDURE [dbo].[BeamprodReport]
	@CompanyId int=1,
	@VoucherID int=0,
	@FromDate int=0,
	@ToDate int=0,
	@Product Varchar(1)='N',
 	@ReportId int=0,
	@Machine Varchar(1)='N',
	@BranchId int=0,
	@DivId int=0,
	@PGroupId int=0,
	@Emp Varchar(1)='N',
 
	@FilterType varchar(50)='All',
	@VTypeId int=0 
AS
BEGIN

select     
   p.Id, case when p.VoucherDate>10101 then ISNULL(CONVERT(date,Convert(varchar(8),p.VoucherDate),112),'')  end as VoucherDate,
p.LoadingDate as LoadingDate,p.warpingdate,p.VoucherNo as BeamNo
,p.Ply as BLength,tb.Taka Cops ,p.CopsWt,p.CartnWt,p.GrossWt,p.TareWt
      ,p.NetWt,p.CopsRate,p.BoxRate,p.DrawingDate,p.CloseDate,p.ProdStatus,p.Tops as Ends
      ,p.Remark,p.IsClose,p.CreateDate,pd.ProductName,yarn.ProductName as YarnName,gray.ProductName as GrayName,
	  w.EmpName as  warperName ,d.EmpName as DrawerName,p.CloseDate as BeamCloseDate,
	  MM.MachineName as MachineNo,tb.TakaQty as TakaProd,p.NetWt-isnull(tb.TakaQty,0) as BalQty,(p.Ply-isnull(tb.TakaMtr,0))/p.Ply*100 as balper,0 as beamdays
	  , p.Ply-isnull(tb.TakaMtr,0)  as BalMtr,isnull(tb.TakaMtr,0)  as TakaMtr
from Prod p
left outer join Voucher v on p.VoucherId=v.Id
 left outer join Product pd on p.ProductId = pd.Id
left outer join Product yarn on p.CopsProductId =yarn.Id
left outer join Product gray on p.BoxProductId =gray.Id
LEFT OUTER JOIN MachineMaster MM ON p.MacId=MM.Id
left outer join Emp w on p.CheckEmpId =w.Id
left outer join Emp d on p.PackEmpId =d.Id
left outer join Company comp on p.CompId= comp.Id
left outer join (SELECT COUNT(tb.BeamId) Taka, sum(Qty)TakaQty,sum(pd.NetWt) TakaMtr,BeamId from TakaBeam tb
                LEFT OUTER JOIN dbo.Prod pd ON pd.Id =tb.ProdId
				where tb.IsActive=1 and tb.IsDeleted=0 
				group by BeamId)tb on tb.BeamId=p.Id
where p.IsActive = 1 and p.IsDeleted=0 AND pd.PTypeId = 5
	 
	and (v.VTypeId=5 OR  v.VTypeId = 11)
		and (@VoucherID=0 or  p.VoucherId=@VoucherID)
	and p.CompId=@CompanyId and (p.VoucherDate between @FromDate and @ToDate)
	AND
	(
	 ( @FilterType='PendingLoading' and p.MacId is null)
	 or
	 (@FilterType='LoadedBeam' and p.ProdStatus='LOADED')
	 or
	 (@FilterType='CloseBeam' and p.IsClose=1)
	 or
	 ((@FilterType='All' or @FilterType is NULL ) and 1=1)
	 )
	AND
      (
          @Product = 'N'
          OR EXISTS
	(
    SELECT 1
    FROM dbo.ReportPara rp
    WHERE rp.ReportId= @reportid
          AND p.ProductId = rp.ParameterValue
          AND rp.ParameterName= 'product'
    ))
		AND
      (
          @Emp = 'N'
          OR EXISTS
	(
    SELECT 1
    FROM dbo.ReportPara rp
    WHERE rp.ReportId= @reportid
          AND p.CheckEmpId = rp.ParameterValue
          AND rp.ParameterName= 'Emp'
    ))
	 	  AND
      (  @Machine = 'N'
          OR EXISTS (  SELECT 1
    FROM dbo.ReportPara rp
    WHERE rp.ReportId= @reportid
          AND p.MacId = rp.ParameterValue
          AND rp.ParameterName= 'machine'  
	  )) 
	   AND 
		  (
          @BranchId = 0
          OR p.BranchId =@BranchId
      )
	    AND 
		  (
          @DivId = 0
          OR p.DivId =@DivId
      )
	   AND 
		  (
          @PGroupId = 0
          OR pd.GroupId =@PGroupId
      )
END
GO

