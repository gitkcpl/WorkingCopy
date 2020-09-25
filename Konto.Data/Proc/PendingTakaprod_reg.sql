IF object_id('[dbo].[PendingTakaprod_reg]') IS NULL 
EXEC ('CREATE PROC [dbo].[PendingTakaprod_reg] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[PendingTakaprod_reg]
	@CompanyId int=1,
	@VoucherID int=0,
	@FromDate int=0,
	@ToDate int=0, 
	@ReportId int=0,
	@BranchId int=0,
	@DivId int=0,
	@PGroupId int=0,
	@PTypeId int=0,
	@Product Varchar(1)='N',
	@Grade Varchar(1)='N', 
	@color varchar(1)='N',
	@Machine Varchar(1)='N'
AS
BEGIN
select   p.Id ,
  case when p.VoucherDate>10101 then ISNULL(CONVERT(date,Convert(varchar(8),p.VoucherDate),112),'')  end as VoucherDate
,p.VoucherNo as TakaNo
       ,cast(p.GrossWt as numeric(18,3)) as TWeight,cast(p.TareWt as numeric(18,3))TareWt,
	   cast(p.NetWt as numeric(18,3)) as TakaMeter
	   ,cast(p.BoxWt as numeric(18,3)) BoxWt
	   ,isnull(po.Rcptqty,0)RcptQty
	,isnull(p.netwt,0)- isnull(po.Rcptqty,0) PendQty
	   , p.CheckEmpId,p.DrawingDate 
      ,p.WarpingDate,p.CloseDate,p.ProdStatus,p.Tops,p.Pallet
      ,p.CurrQty,p.FinQty,p.Cops,p.IssueRefId,p.IssueRefVoucherId, 
	  p.Remark,p.IsClose,p.LoadingDate as LoadingDate 
	,m.MachineName,pd.ProductName,c.ColorName ,g.GradeName , 
	  design.ProductName DesignName,b.VoucherNo BatchNo
from Prod p
left outer join Voucher v on p.VoucherId=v.Id
LEft Outer join (select po.ProdId,(sum(qty)*-1)Rcptqty
		from prodout po
		where po.IsActive =1 and po.IsDeleted=0
		group by po.ProdId
		)po on  po.ProdId=p.Id
left outer join MachineMaster m on m.Id=p.MacId
left outer join Product pd on pd.Id=p.ProductId
left outer join Color c on c.Id=p.ColorId
left outer join product design on design.Id=p.PlyProductId 
left outer join Grade g on g.Id=p.GradeId  
left outer join Batch b on b.Id=p.BatchId
where p.IsActive = 1 and p.IsDeleted=0
	and p.IsClose=0
	and ((isnull(p.netwt,0)- isnull(po.Rcptqty,0) )>0)
	and p.CompId=@CompanyId 
	and (pd.PTypeId=@PTypeId or @PTypeId=0)
	and (p.VoucherId=@VoucherID or @VoucherID=0)
	and (p.VoucherDate between @FromDate and @ToDate)
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
          @color = 'N'
          OR EXISTS
	(
    SELECT 1
    FROM dbo.ReportPara rp
    WHERE rp.ReportId= @reportid
          AND p.ColorId = rp.ParameterValue
          AND rp.ParameterName= 'color'
    ))
	AND
      (@Grade = 'N'
          OR EXISTS (  SELECT 1
    FROM dbo.ReportPara rp
    WHERE rp.ReportId= @reportid
          AND p.GradeId = rp.ParameterValue
          AND rp.ParameterName= 'Grade'  
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

