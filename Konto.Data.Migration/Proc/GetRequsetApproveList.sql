IF object_id('[dbo].[GetRequsetApproveList]') IS NULL 
EXEC ('CREATE PROC [dbo].[GetRequsetApproveList] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[GetRequsetApproveList]
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
								  ot.Qty-(isnull(ct.Qty,0.00)) as PendQty,
								  Isnull(ct.Qty,0) RcptQty,em.EmpName RequestBy,
								  dm.DivisionName Division,
								  ot.RequireDate,ot.Priority,ot.CommDescr UsageAt
FROM dbo.OrdTrans ot
LEFT OUTER JOIN dbo.Ord o ON ot.OrdId =o.Id 
LEFT OUTER JOIN dbo.Voucher v on v.Id = o.VoucherId
left outer join Product pr on pr.id=ot.ProductId
LEFT OUTER JOIN dbo.Product de ON de.Id = ot.DesignId
LEFT OUTER JOIN dbo.Emp em on em.id= o.EmpId
left outer join Acc ac on ac.Id=o.AccId
left outer join Color col on col.Id=ot.ColorId
left outer join PayTerms pay on pay.Id=o.PayTermsId
left outer join Acc trans on trans.Id =o.TransportId
left outer join Division dm on dm.id = o.DivisionId
LEFT OUTER JOIN ( SELECT ct.RefId, SUM(ct.Qty) Qty
					FROM dbo.OrdTrans ct
					WHERE  ct.Isdeleted = 0 AND ct.IsActive = 1 AND isnull(ct.RefId,0)<>0  AND isnull(ct.RefVoucherId,0)<>0 
					GROUP BY ct.RefId ) 

ct ON (ct.RefId= ot.Id )
WHERE  ot.IsActive=1 AND OT.IsDeleted=0 
	AND v.VTypeId=@VouchreID and o.CompId=@CompanyId
	and ot.OrdStatus=@OStatus and o.YearId=@YearId
		and (o.VoucherDate between @FromDate and @ToDate)
END

GO

