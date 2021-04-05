IF object_id('[dbo].[req_det_list]') IS NULL 
EXEC ('CREATE PROC [dbo].[req_det_list] AS SELECT 1 AS Id') 
GO


ALTER PROCEDURE [dbo].[req_det_list]
	@fromDate int ,
	@ToDate int,
	@CompanyId int,
	@YearId int,
 	@VTypeId int,
	@BranchId int,
	@Deleted INT = 0
AS
BEGIN
 SELECT 
		d.DivisionName Div,
		o.VoucherNo RequestNo, 
		v.VoucherName,
		CASE when o.VoucherDate>10101 then ISNULL(CONVERT(Date,Convert(varchar(8),o.VoucherDate),112),'')  end as ReqDate,                               
        o.RefNo,
		pd.ProductName,
	    ot.Qty,
		ot.Qty - ISNULL(ot1.Qty,0) PendingQty,
		um.UnitName Unit,
        ot.Rate,
		ot.Total,
		e.EmpName RequestBy,
		ot.CommDescr UsageAt,
		ot.RequireDate,
		ot.[Priority],
		ot.Remark,
		o.Id,
		o.VoucherId,o.IsDeleted,
		o.CreateDate,o.CreateUser,o.ModifyDate,o.ModifyUser
	from Ord o
	left outer join dbo.OrdTrans ot on ot.OrdId =o.Id
	LEFT OUTER JOIN  dbo.Uom um on ot.UomId = um.Id
	LEFT OUTER JOIN dbo.Product pd on ot.ProductId = pd.Id
	LEFT OUTER JOIN dbo.Voucher v on v.id=o.VoucherId
	LEFT OUTER JOIN dbo.Division d on d.Id = o.DivisionId
	LEFT OUTER JOIN dbo.Emp e on e.Id = o.EmpId
	LEFT OUTER JOIN (
					SELECT ct.RefId, SUM(ct.Qty) Qty
					FROM dbo.OrdTrans ct
					
					WHERE  ct.Isdeleted = 0 AND ct.IsActive = 1 AND isnull(ct.RefId,0)<>0  AND isnull(ct.RefVoucherId,0)<>0 
					GROUP BY ct.RefId
	) ot1 ON ot1.RefId = ot.id
	WHERE o.IsActive=1 and o.IsDeleted=@Deleted
	AND v.VTypeId=@VTypeId
	AND o.CompId=@CompanyId and o.YearId=@YearId
	AND (o.VoucherDate between @fromDate and @ToDate)
	ORDER BY o.VoucherDate DESC, o.VoucherNo desc
END


GO

