IF object_id('[dbo].[POList]') IS NULL 
EXEC ('CREATE PROC [dbo].[POList] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[POList]
	@fromDate int ,
	@ToDate int,
	@CompanyId int,
	@YearId int,
 	@VTypeId int,
	@BranchId int,
	@Deleted INT = 0
AS
BEGIN
 SELECT o.OrderType,
		o.VoucherNo OrderNo, 
		v.VoucherName,
		CASE when o.VoucherDate>10101 then ISNULL(CONVERT(Date,Convert(varchar(8),o.VoucherDate),112),'')  end as OrdDate,                               
        ac.AccName Party ,
        o.RefNo,
		o.TotalPcs,
		o.TotalPcs - ISNULL(ot.Pcs,0) PendingPcs,
        o.TotalQty,
		o.TotalQty - ISNULL(ot.Qty,0) PendingQty,
        o.TotalAmount TotalAmt,
		Trans.PrintName Transport,
        Pay.PayDescr PayTerm,
		o.Remarks,
		o.SpecialNotes,
		o.Currency,
		ep.EmpName Checker,	
		ch.EmpName OutChecker,
		o.CheckRate CheckerRate,
		o.EmpRate OutCheckerRate,
		um.UnitName CheckUnit,
		umc.UnitName OutCheckUnit,
		o.Id,
		o.VoucherId,o.IsDeleted
	from Ord o
	LEFT OUTER JOIN Acc ac on ac.Id=o.AccId 
	LEFT OUTER JOIN Acc Trans on Trans.Id=o.TransportId
	LEFT OUTER JOIN dbo.Acc ag ON ag.Id = ac.AgentId
	LEFT OUTER JOIN PayTerms Pay on Pay.Id=o.PayTermsId
	LEFT OUTER JOIN Voucher v on v.id=o.VoucherId
	LEFT OUTER JOIN dbo.Emp ep ON ep.Id = o.CheckerId
	LEFT OUTER JOIN dbo.Emp ch ON ch.Id = o.CheckerOutId
	LEFT OUTER JOIN dbo.Uom um ON um.Id = o.CheckUomId
	LEFT OUTER JOIN dbo.Uom umc ON umc.Id = o.EmpUomId	 
	LEFT OUTER JOIN (
					SELECT ct.MiscId,ct.RefVoucherId, SUM(ct.Qty) Qty, SUM(Pcs) Pcs 
					 FROM dbo.ChallanTrans ct
					WHERE  ct.Isdeleted = 0 AND ct.IsActive = 1 AND ct.MiscId IS NOT NULL AND ct.RefVoucherId IS NOT NULL
					GROUP BY ct.MiscId,ct.RefVoucherId
	) ot ON ot.MiscId = o.Id AND ot.RefVoucherId = o.VoucherId
	WHERE o.IsActive=1 and o.IsDeleted=@Deleted
	AND v.VTypeId=@VTypeId
	AND o.CompId=@CompanyId and o.YearId=@YearId
	AND (o.VoucherDate between @fromDate and @ToDate)
	ORDER BY o.VoucherDate DESC, o.VoucherNo desc
END
GO

