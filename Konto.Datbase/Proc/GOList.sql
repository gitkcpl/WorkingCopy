GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GOList]
	@fromDate int ,
	@ToDate int,
	@CompanyId int,
	@YearId int,
    @VTypeId int, 
	@BranchId int
AS
BEGIN
	select o.VoucherNo OrderNo,
                                 ISNULL(CONVERT(Date,Convert(varchar(8),o.VoucherDate),112),'') 
								   OrdDate,
								   v.VoucherName,
								   pg.GroupName PartyGroup,
                                  ac.AccName Party ,
                                  o.RefNo,
								  p.ProductName Quality,
								  ot.NoOfLot,
								  ot.LotPcs,
								  ot.Cut PerLotMtrs,
								  ot.Rate,
								  ot.NetTotal,
                                  o.AccId PartyId,
								  o.VoucherId,
                                  o.Id,
    ISNULL(ct.Qty,0) RcptQty,ISNULL(ct.Pcs,0) RcptTaka, ot.Qty - ISNULL(ct.Pcs,0) PendPcs,
     (ot.Qty - ISNULL(ct.Pcs,0))/12 PendLot
                                  
	from Ord o
	left outer join Acc ac on ac.Id=o.AccId 
	left outer join Acc Trans on Trans.Id=o.TransportId
	left outer join PayTerms Pay on Pay.Id=o.PayTermsId
	LEFT OUTER JOIN dbo.Voucher  v ON v.Id = o.VoucherId
	LEFT OUTER JOIN dbo.OrdTrans ot ON ot.OrdId = o.Id
	LEFT OUTER JOIN dbo.Product p ON p.Id = ot.ProductId
	LEFT OUTER JOIN dbo.PartyGroup pg ON pg.Id = o.PGroupId
left outer join (select sum(Qty)as Qty , SUM(ct.Pcs) Pcs, ISNULL(ct.RefId,0) AS RefId, ISNULL(ct.RefVoucherId,0) AS RefVoucherId  from ChallanTrans ct
			where ct.IsActive=1 and ct.IsDeleted=0 AND ct.RefId IS NOT NULL
			group by ct.RefId, ISNULL(ct.RefVoucherId,0)
	)ct on ct.RefId=ot.Id AND ct.RefVoucherId = o.VoucherId
	where o.IsActive=1 and o.IsDeleted=0
	and o.CompId=@CompanyId and o.YearId=@YearId
	 and (@BranchId=0 or o.BranchId=@BranchId)
	 AND v.VTypeId=@VTypeId
	
END