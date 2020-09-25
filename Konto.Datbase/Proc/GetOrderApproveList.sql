CREATE PROCEDURE [dbo].[GetOrderApproveList]
 	@VouchreID int,
	@CompanyId int,
	@YearId int,
	@FromDate int,
	@ToDate int,
	@OStatus varchar(50)
AS
BEGIN

	select 
	 o.VoucherNo OrderNo , o.VoucherDate OrdDate ,
                                   pr.ProductName Product,
                                   ac.AccName Supplier,
                                  col.ColorName Color,
								  de.ProductName DesignNo,
                                   ot.Total Gross,
                                    o.Id Id,
                                   ot.Id TransId,
                                  ot.Qty,
                                  ot.Rate,
                                   ot.AvgWt,
                                   ot.Cess,
                                   ot.CessAmt,
                                   ot.Cgst,
                                   ot.CgstAmt,
                                   ot.Cut,
                                   ot.DesignId,
                                   ot.Disc,
                                   ot.DiscAmt,
                                   ot.Igst,
                                   ot.IgstAmt,
                                   ot.GradeId,
                                   ot.Remark ItemRemark ,
                                   ot.LotPcs,
                                   ot.NetTotal,
                                   ot.NoOfLot,
                                   Pay.PayDescr PayTerm ,
                                   o.RefNo,
                                   ot.Sgst,
                                   ot.SgstAmt,
                                   ot.Total,
                                   Trans.PrintName Transport ,
                                  Width = ot.Width ,
								  ot.Qty-(isnull(ct.RcptQty,0.00)) as PendQty,
								  ct.RcptQty
FROM dbo.OrdTrans ot
LEFT OUTER JOIN dbo.Ord o ON ot.OrdId =o.Id 
LEFT OUTER JOIN dbo.Voucher v on v.Id = o.VoucherId
left outer join Product pr on pr.id=ot.ProductId
LEFT OUTER JOIN dbo.Product de ON de.Id = ot.DesignId
left outer join Acc ac on ac.Id=o.AccId
left outer join Color col on col.Id=ot.ColorId
left outer join PayTerms pay on pay.Id=o.PayTermsId
left outer join Acc trans on trans.Id =o.TransportId
LEFT OUTER JOIN ( SELECT SUM(b.Qty) AS RcptQty,b.RefId,B.RefVoucherId FROM dbo.ChallanTrans b
				WHERE B.IsActive=1 AND B.IsDeleted=0 GROUP BY b.RefId,B.RefVoucherId) 
ct ON (ct.RefId= ot.Id AND ct.RefVoucherId=O.VoucherId)
WHERE  ot.IsActive=1 AND OT.IsDeleted=0 
	AND v.VTypeId=@VouchreID and o.CompId=@CompanyId
	and ot.OrdStatus=@OStatus and o.YearId=@YearId
		and (o.VoucherDate between @FromDate and @ToDate)
END
GO

