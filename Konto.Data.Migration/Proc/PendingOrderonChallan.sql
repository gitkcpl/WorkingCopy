IF object_id('[dbo].[PendingOrderonChallan]') IS NULL 
EXEC ('CREATE PROC [dbo].[PendingOrderonChallan] AS SELECT 1 AS Id') 
GO

ALTER  PROCEDURE [dbo].[PendingOrderonChallan]
 	@CompanyId int,
	@AccountId int,
	@VoucherTypeID int,
	@ordStatus varchar(100)='APPROVED' ,
	@ordType INT = 0
AS
BEGIN
	select o.VoucherNo,o.VoucherDate, 
	 ISNULL(CONVERT(Date,Convert(varchar(8),o.VoucherDate),112),'') VouchDate,
	ISNULL(o.AccId,0) AS DelvAccId, ad.Address1,ad.Address2,
	ISNULL(ad.Id,0) AS DelvAdrId,c.CityName,ISNULL(o.TransportId,0) AS TransportId ,
	ot.LotPcs AS TotalPcs,CAST(ot.Qty as numeric(18,2)) as TotalQty,
	cast((ot.Qty-(isnull(ct.Qty,0) + ISNULL(btt.qty,0)) )  as numeric(18,2)) as PendingQty,
	p.ProductName as Product,ot.ProductId,ot.ColorId,ot.Cut,ot.DivisionId,ot.DesignId,ot.GradeId,
	ot.UomId,ot.LotPcs,cast(ot.NetTotal  as numeric(18,2)) as NetTotal,ot.NoOfLot
	,cast(ot.rate as numeric(18,2)) as rate,ot.Sgst,ot.SgstAmt,isnull(ot.Cgst,0) AS Cgst,
	ISNULL(ot.CgstAmt,0) AS CgstAmt,isnull(ot.Igst,0) AS Igst,isnull(ot.IgstAmt,0) AS IgstAmt,ot.Total,
	ot.Disc,ot.DiscAmt,ot.Id as TransId,o.Id ,o.VoucherId , o.Remarks
	,clr.ColorName,design.ProductCode as DesignNo,grd.GradeName, o.RefNo
	from Ord o
	left outer join OrdTrans ot on ot.OrdId=o.Id
	left outer join Product p on p.Id=ot.ProductId
	left outer join Division d on d.Id=ot.DivisionId 
	LEFT OUTER JOIN dbo.AccAddress ad ON ad.AccId = o.AccId AND ad.IsDefault = 1
	LEFT OUTER JOIN Acc ac on ac.id=o.AccId
	LEFT OUTER JOIN dbo.City c ON c.Id = ad.CityId
	LEFT OUTER JOIN dbo.Color clr ON clr.Id =ot.ColorId
	LEFT OUTER JOIN dbo.Product design ON design.Id =ot.DesignId
	LEFT OUTER JOIN dbo.Grade grd ON grd.Id =ot.GradeId
	left outer join (select sum(Qty)as Qty , ISNULL(ct.RefId,0) AS RefId, ISNULL(ct.RefVoucherId,0) AS RefVoucherId  from ChallanTrans ct
			INNER JOIN Challan ch on ch.Id = ct.ChallanId
			inner join Voucher v on v.Id = ch.VoucherId
			where ct.IsActive=1 and ct.IsDeleted=0 and (v.VTypeId=5 or v.VTypeId=6) and ch.IsDeleted=0
			group by ct.RefId, ISNULL(ct.RefVoucherId,0)
	)ct on ct.RefId=ot.Id AND ct.RefVoucherId = o.VoucherId
	left outer join Voucher v on v.Id=o.VoucherId 
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
	WHERE ot.OrdStatus = @ordStatus --AND o.OrderStatusId =@ordType--'APPROVED' 
	--AND  o.CompId = 1
	-- AND  v.VTypeId=3 and (o.AccId=68 or (ac.PGroupId in(select a.PGroupId from Acc a where a.Id=68)))
	AND  o.CompId = @CompanyId
	 AND  (v.VTypeId=@VoucherTypeID or V.VTypeId =39) and (o.AccId=@AccountId or (ac.PGroupId in(select a.PGroupId from Acc a where a.Id=@AccountId AND a.PGroupId<>1)))
	and ((ot.Qty-(isnull(ct.Qty,0) + ISNULL(btt.Qty,0)) ) >0) AND o.IsActive =1 AND o.IsDeleted = 0
	--and (o.VoucherDate between @FromDate and @ToDate)
END
GO

