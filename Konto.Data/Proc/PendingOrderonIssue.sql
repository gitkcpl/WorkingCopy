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
	  ot.LotPcs AS TotalPcs
	,CAST(ot.Qty as numeric(18,2)) as TotalQty,
	cast((ot.Qty-isnull(ct.Qty,0))  as numeric(18,2)) as PendingQty,
	   cast(ot.NetTotal  as numeric(18,2)) as NetTotal,ot.NoOfLot
	, ot.SgstAmt, 
	ISNULL(ot.CgstAmt,0) AS CgstAmt,isnull(ot.IgstAmt,0) AS IgstAmt,ot.Total,
	 ot.DiscAmt, o.Id ,o.VoucherId , o.Remarks 
	from Ord o
	left outer join(select sum(ot1.Qty) Qty,sum(ot1.LotPcs) LotPcs ,
	sum(ot1.NetTotal) NetTotal,sum(ot1.NoOfLot) NoOfLot,
	sum(ot1.SgstAmt) SgstAmt,sum(ISNULL(ot1.CgstAmt,0) ) CgstAmt
	,sum(isnull(ot1.IgstAmt,0)) AS IgstAmt,sum(ot1.Total) Total
	,sum(ot1.DiscAmt) DiscAmt
	,ot1.OrdId,ot1.OrdStatus
			from OrdTrans ot1
	group by ot1.OrdId,ot1.OrdStatus
	)ot on ot.OrdId=o.Id  
	LEFT OUTER JOIN dbo.AccAddress ad ON ad.AccId = o.AccId AND ad.IsDefault = 1
	LEFT OUTER JOIN Acc ac on ac.id=o.AccId
	LEFT OUTER JOIN dbo.City c ON c.Id = ad.CityId 
	left outer join (select sum(Qty)as Qty , ISNULL(ct.MiscId,0) AS MiscId, ISNULL(ct.RefVoucherId,0) AS RefVoucherId
		  from ChallanTrans ct
		  left outer join Challan c on c.Id=ct.ChallanId
			where ct.IsActive=1 and ct.IsDeleted=0 and c.VoucherId=@IssueVoucher
			group by ct.MiscId, ISNULL(ct.RefVoucherId,0)
	)ct on (ct.MiscId=o.Id AND ct.RefVoucherId = o.VoucherId)
	left outer join Voucher v on v.Id=o.VoucherId 
	WHERE ot.OrdStatus = @ordStatus   
	AND  o.CompId = @CompanyId
	 AND  v.VTypeId=@VoucherTypeID --4 for Sales c 
	 and ((o.AccId=@AccountId or (ac.PGroupId in(select a.PGroupId from Acc a where a.Id=@AccountId AND a.PGroupId<>1))) or @AccountId=0)
	and ((ot.Qty-isnull(ct.Qty,0)) >0) AND o.IsActive =1 AND o.IsDeleted = 0 
END
GO

