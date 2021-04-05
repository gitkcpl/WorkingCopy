IF object_id('[dbo].[PendingOrderonIssue]') IS NULL 
EXEC ('CREATE PROC [dbo].[PendingOrderonIssue] AS SELECT 1 AS Id') 
GO


ALTER  PROCEDURE [dbo].[PendingOrderonIssue]
 	@CompanyId int,
	@AccountId int=0,
	@VoucherTypeID int,
	@ordStatus varchar(100)='APPROVED' ,
	@IssueVoucher INT = 0
AS
BEGIN
	select o.VoucherNo, 
	 ISNULL(CONVERT(Date,Convert(varchar(8),o.VoucherDate),112),'') VouchDate,
	 ac.AccName as Party,ad.Address1,p.ProductName Product,
	 CAST(ot.Qty as numeric(18,2)) as TotalQty
	 ,cast(( isnull(ct.Qty,0))  as numeric(18,2)) as IssueQty,
	cast((ot.Qty-isnull(ct.Qty,0))  as numeric(18,2)) as PendingQty,
	o.Id ,o.VoucherId  ,ot.ProductId,ot.Id as TransId
	from Ord o
	left outer join  OrdTrans  ot on ot.OrdId=o.Id  
	Left outer join product p on p.id=ot.ProductId
	LEFT OUTER JOIN dbo.AccAddress ad ON ad.AccId = o.AccId AND ad.IsDefault = 1
	LEFT OUTER JOIN Acc ac on ac.id=o.AccId
	LEFT OUTER JOIN dbo.City c ON c.Id = ad.CityId 
	left outer join (select sum(Qty)as Qty , ISNULL(ct.MiscId,0) AS MiscId,isnull(ct.refid,0) as RefId, ISNULL(ct.RefVoucherId,0) AS RefVoucherId
		  from ChallanTrans ct
		  left outer join Challan c on c.Id=ct.ChallanId
			where ct.IsActive=1 and ct.IsDeleted=0 and c.VoucherId=@IssueVoucher AND c.IsDeleted=0
			group by ct.MiscId, ISNULL(ct.RefVoucherId,0),ct.RefId
	)ct on (ct.RefId=ot.Id AND ct.RefVoucherId = o.VoucherId)
	left outer join Voucher v on v.Id=o.VoucherId 
	WHERE ot.OrdStatus = @ordStatus   
	AND  o.CompId = @CompanyId
	AND  v.VTypeId=@VoucherTypeID --4 for Sales c 
  	and ((ot.Qty-isnull(ct.Qty,0)) >0) AND o.IsActive =1 AND o.IsDeleted = 0 AND ot.IsDeleted=0
END
GO

