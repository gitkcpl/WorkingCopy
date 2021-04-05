IF object_id('[dbo].[OrderDetailsReport]') IS NULL 
EXEC ('CREATE PROC [dbo].[OrderDetailsReport] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[OrderDetailsReport]
 	@CompanyId int = 1,
	@ordStatus varchar(100)='All',
	@VoucherID int=0,
	@FromDate int=0,
	@ToDate int=0,
	@ReportId int=0,
	@BranchId int=1,
	@DivId int=0,
	@PGroupId int=0,
	@AgentId int=0,
	@TransportId int=0,
	@VoucherTypeID int=0,
	@VTypeID int=0,
	@ProductGroup int=0,
	@Product Varchar(1)='N',
	@Party Varchar(1)='N', 
	@Grade Varchar(1)='N', 
	@color varchar(1)='N',
	@Design Varchar(1)='N',
	@IsPending int=0
AS
BEGIN
	select o.VoucherNo,
	ISNULL(CONVERT(Date,Convert(varchar(8),o.VoucherDate),112),'')  as VoucherDate,
 
	ac.AccName as PartyName, ISNULL(o.AccId,0) AS DelvAccId, ad.Address1,ad.Address2,
	ISNULL(ad.Id,0) AS DelvAdrId,c.CityName,ISNULL(o.TransportId,0) AS TransportId ,ot.LotPcs AS TotalPcs,CAST(ot.Qty as numeric(18,2)) as TotalQty,
	cast((ot.Qty-(isnull(ct.RcptQty,0)+ isnull(btt.qty,0)))  as numeric(18,2)) as PendingQty,
	cast(isnull(ct.RcptQty,0)+isnull(btt.qty,0) as numeric(18,2)) as RcptQty,
	p.ProductName as Product,ot.ProductId,co.ColorName,ot.ColorId,ot.Cut,ot.DivisionId,ot.DesignId,
	gr.GradeName,ot.GradeId,uo.UnitName,
	ot.UomId,ot.LotPcs,cast(ot.NetTotal  as numeric(18,2)) as NetTotal,ot.NoOfLot
	,cast(ot.rate as numeric(18,2)) as rate,ot.Sgst,ot.SgstAmt,isnull(ot.Cgst,0) AS Cgst,
	ISNULL(ot.CgstAmt,0) AS CgstAmt,isnull(ot.Igst,0) AS Igst,isnull(ot.IgstAmt,0) AS IgstAmt,ot.Total,
	ot.Disc,ot.DiscAmt,ot.Id as TransId,o.Id ,o.VoucherId , o.Remarks
	from dbo.Ord o
	left outer join dbo.OrdTrans ot on ot.OrdId=o.Id
	left outer join dbo.Product p on p.Id=ot.ProductId
	Left Outer Join dbo.Color co on co.id=ot.ColorId
	Left Outer join dbo.Grade gr on gr.id=ot.GradeId
	Left outer Join dbo.Uom uo on uo.Id=ot.UomId
	left outer join dbo.Division d on d.Id=ot.DivisionId 
	left outer join dbo.AccBal acb on acb.AccId = o.AccId and acb.CompId = o.CompId and acb.YearId = o.YearId
	LEFT OUTER JOIN dbo.AccAddress ad ON ad.Id = acb.AddressId 
	LEFT OUTER JOIN dbo.Acc ac on ac.id=o.AccId
	LEFT OUTER JOIN dbo.City c ON c.Id = ad.CityId
	left outer join (SELECT ct.ProductId,sum(ct.Qty)as RcptQty , ISNULL(ct.RefId,0) AS RefId,	ISNULL(ct.RefVoucherId,0) AS RefVoucherId  
					 FROM dbo.ChallanTrans ct
			         WHERE ct.IsActive=1 and ct.IsDeleted=0
			         GROUP BY ct.RefId, ISNULL(ct.RefVoucherId,0), ct.ProductId
	)ct on ct.RefId=ot.Id AND ct.RefVoucherId = o.VoucherId and ct.ProductId = ot.ProductId

	left outer join (select sum(Qty)as Qty , ISNULL(bt.RefTransId,0) AS RefId, 
	ISNULL(bt.RefVoucherId,0) AS RefVoucherId  from BillTrans bt
			INNER JOIN BillMain bm on bm.Id = bt.BillId
			inner join Voucher v on v.Id = bt.RefVoucherId
			where bt.IsActive=1 and bt.IsDeleted=0 and (v.VTypeId= 4 or v.VTypeId=3)
			 and bm.IsDeleted=0
			and bt.RefId is not null and bt.RefVoucherId is not null 
			and bt.RefTransId is not null
			group by bt.RefTransId, ISNULL(bt.RefVoucherId,0)
	)btt on btt.RefId=ot.Id AND btt.RefVoucherId = o.VoucherId


	left outer join dbo.Voucher v on v.Id=o.VoucherId 
	WHERE  o.CompId = @CompanyId   
	AND (o.VoucherDate between @FromDate and @ToDate)
	AND  (v.VTypeId=@VTypeID or @VTypeID=0)
	AND o.IsActive =1 AND o.IsDeleted = 0 AND ot.IsDeleted=0
		and (o.VoucherId=@VoucherID or @VoucherID=0)
	and  (ot.OrdStatus = @ordStatus or @ordStatus='All')
	and (  @IsPending=0  or (isnull(ot.Qty,0)-(isnull(ct.RcptQty,0) +isnull(btt.qty,0)) > 0 and  ot.OrdStatus not in ('CANCELED','CLOSED')))
		AND
      (
          @Party = 'N'
          OR EXISTS
	(
    SELECT 1
    FROM dbo.ReportPara rp
    WHERE rp.ReportId= @ReportId
          AND o.AccId = rp.ParameterValue
          AND rp.ParameterName= 'party'
    ))
		AND
      (
          @Product = 'N'
          OR EXISTS
	(
    SELECT 1
    FROM dbo.ReportPara rp
    WHERE rp.ReportId= @ReportId
          AND ot.ProductId = rp.ParameterValue
          AND rp.ParameterName= 'product'
    ))
			AND
      (
          @color = 'N'
          OR EXISTS
	(
    SELECT 1
    FROM dbo.ReportPara rp
    WHERE rp.ReportId= @ReportId
          AND ot.ColorId = rp.ParameterValue
          AND rp.ParameterName= 'color'
    ))
	AND
      (  @Grade = 'N'
          OR EXISTS (  SELECT 1
    FROM dbo.ReportPara rp
    WHERE rp.ReportId= @ReportId
          AND ot.GradeId = rp.ParameterValue
          AND rp.ParameterName= 'Grade'  
	  )) 
	  
	   AND 
		  (
          @BranchId = 0
          OR o.BranchId =@BranchId
      )
	      AND 
		  (
          @PGroupId = 0
          OR ac.PGroupId =@PGroupId
      )
	     AND 
		  (
          @AgentId = 0
          OR o.AgentId=@AgentId
      ) AND 
		  (
          @TransportId = 0
          OR o.TransportId =@TransportId
      ) 
	  AND 
		  (
          @ProductGroup = 0
          OR p.GroupId =@ProductGroup
      )

END
GO

