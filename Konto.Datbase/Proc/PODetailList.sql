CREATE PROCEDURE [dbo].[PODetailList]
	@FromDate int ,
	@ToDate int,
	@CompanyId int,
	@YearId int,
	@BranchId INT,
	@VTypeid INT
AS
BEGIN
	SELECT o.VoucherNo OrderNo, v.VoucherName ,
	CONVERT(Date,Convert(varchar(8),o.VoucherDate),112) as OrderDate, 
    ac.AccName Party, 
	ag.AccName Agent,
	o.RefNo, 
    pr.ProductName Product, 
	col.ColorName Color,
	de.ProductName Design,
	ot.LotPcs pcs,
	ot.LotPcs - ISNULL(ct.Pcs,0) PendingPcs,
    ot.Cut,
	ot.Qty,
	ot.Qty - ISNULL(ct.Qty,0) PendingQty,
	--ct.ChallanNo,
	ot.OrdStatus,
	ot.Rate,
    um.UnitName,
	ot.Total Gross, 
	ot.Disc, ot.DiscAmt,
	ot.AvgWt, ot.Cess, ot.CessAmt,
    ot.Cgst, ot.CgstAmt, 
	ot.Igst, ot.IgstAmt,
    ot.Sgst,  ot.SgstAmt, 
	ot.Remark, 
	ot.NetTotal,
    ot.NoOfLot,
	o.Currency,
	o.ExchRate,
	o.Remarks,
	o.SpecialNotes,
	Trans.PrintName Transport,
    Pay.PayDescr PayTerm, 
	ep.EmpName Checker,
	ch.EmpName CheckerOut,
	umt.UnitName CheckerUnit,
	umc.UnitName CheckerOutUnit,
	o.CheckRate,
	o.EmpRate OutCheckerRate,
	ot.Width,o.VoucherId,o.Id, o.IsDeleted
	FROM Ord o
	LEFT OUTER JOIN dbo.Voucher v ON v.Id = o.VoucherId
	LEFT OUTER JOIN OrdTrans ot on ot.OrdId=o.Id
	LEFT OUTER JOIN Acc ac on ac.Id=o.AccId 
	LEFT OUTER JOIN dbo.Acc ag ON ag.Id = ac.AgentId
	LEFT OUTER JOIN Acc Trans on Trans.Id=o.TransportId
	LEFT OUTER JOIN PayTerms Pay on Pay.Id=o.PayTermsId
	LEFT OUTER JOIN Product pr on ot.ProductId =pr.Id
	LEFT OUTER JOIN Color col on ot.ColorId=col.Id
	LEFT OUTER JOIN dbo.Product de on de.Id = ot.DesignId
	LEFT OUTER JOIN dbo.Uom um ON um.Id = ot.UomId
	LEFT OUTER JOIN dbo.Emp ep ON ep.Id = o.CheckerId
	LEFT OUTER JOIN dbo.Emp ch ON ch.Id = o.CheckerOutId
	LEFT OUTER JOIN dbo.Uom umt ON umt.Id = o.CheckUomId
	LEFT OUTER JOIN dbo.Uom umc ON umc.Id = o.EmpUomId	 
	LEFT OUTER JOIN (
					SELECT ct.RefId, SUM(ct.Pcs) Pcs ,SUM(ct.Qty) Qty FROM dbo.ChallanTrans ct
			        WHERE ct.Isdeleted = 0 AND ct.IsActive = 1
					GROUP BY ct.RefId
	) ct ON ct.RefId = ot.Id
	WHERE o.IsActive=1 and o.IsDeleted=0
	and ot.IsActive=1 and ot.IsDeleted=0
	and v.VTypeId=@VTypeId
	and o.CompId=@CompanyId
	AND o.YearId = @YearId
	and (o.VoucherDate between @fromDate and @toDate)
	ORDER BY o.VoucherDate DESC, o.VoucherNo desc
	END
GO

