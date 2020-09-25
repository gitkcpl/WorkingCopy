IF object_id('[dbo].[YarnProd_Reg]') IS NULL 
EXEC ('CREATE PROC [dbo].[YarnProd_Reg] AS SELECT 1 AS Id') 
GO

ALTER  PROCEDURE [dbo].[YarnProd_Reg]
 	@CompanyId int=1,
	@VoucherID int=0,
	@FromDate int=0,
	@ToDate int=0,
	@ReportId int=0,
	@BranchId int=1,
	@DivId int=0,
	@PGroupId int=0,
	@PTypeId int=0,
	@VTypeID int=0,
	@Product Varchar(1)='N',
	@Grade Varchar(1)='N', 
	@color varchar(1)='N',
	@Batch Varchar(1)='N',
	@Machine Varchar(1)='N'
	
AS
BEGIN
select  p.Id,
  case when p.VoucherDate>10101 then ISNULL(CONVERT(date,Convert(varchar(8),p.VoucherDate),112),'')  end as VoucherDate
	  ,p.VoucherNo as TakaNo 
	  ,m.MachineName,pd.ProductName,c.ColorName ,g.GradeName,b.VoucherNo as BatchNo
	  --,ply.ProductName PlyProduct
	  --,box.ProductName as BoxName,cops.ProductName as CopsProduct
	  ,p.Ply,p.Cops,cast(p.CopsWt as numeric(18,2)) CopsWt
	  
	  ,cast(p.GrossWt as numeric(18,3)) as TWeight
	 , cast(p.BoxWt  as numeric(18,3)) BoxWt,
	  cast(p.TareWt  as numeric(18,3))TareWt 
	  
	   ,cast(p.NetWt as numeric(18,3)) as TakaMeter 
	  
	   ,p.Tops,p.Pallet 
      ,p.CurrQty,p.FinQty
	  ,p.DrawingDate,de.ProductName as DesignName
      ,p.WarpingDate,p.CloseDate,p.ProdStatus 
	  ,CASE WHEN CAST(p.GrossWt AS numeric(10, 3)) > 0.000
   THEN cast(((CAST(p.GrossWt AS numeric(10, 3)) / CAST(p.GrossWt AS numeric(10, 3))) * 100) as numeric(18,2)) END AS AverageWeight
	,p.Remark,p.IsClose,p.LoadingDate as LoadingDate
	  ,Beam.VoucherNo as BeamNO
from Prod p
left outer join Voucher v on p.VoucherId=v.Id
left outer join Product pd on pd.Id=p.ProductId
left outer join Color c on c.Id=p.ColorId
left outer join MachineMaster m on m.Id=p.MacId 
left outer join Grade g on g.Id=p.GradeId
left outer join Batch b on b.Id=p.BatchId
Left Outer Join Product de on de.id=p.PlyProductId
LEft Outer join Prod Beam On Beam.id=p.RefId
where p.IsActive = 1 and p.IsDeleted=0
	and p.IsClose=0
	and v.VTypeId=@VTypeID
	and p.CompId=@CompanyId 
	--and ( pd.PTypeId=@PTypeId or @PTypeId=0)
	and ( p.VoucherId=@VoucherID or @VoucherID=0)
	and (p.VoucherDate between @FromDate and @ToDate)
	AND
      (
          @Batch= 'N'
          OR EXISTS
	(
    SELECT 1
    FROM dbo.ReportPara rp
    WHERE rp.ReportId= @reportid
          AND p.BatchId = rp.ParameterValue
          AND rp.ParameterName= 'Batch'
    ))
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
      (  @Grade = 'N'
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
          AND p.MacId= rp.ParameterValue
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

