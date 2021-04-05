IF object_id('[dbo].[ReqList]') IS NULL 
EXEC ('CREATE PROC [dbo].[ReqList] AS SELECT 1 AS Id') 
GO


ALTER PROCEDURE [dbo].[ReqList]
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
	    o.TotalQty,
		o.TotalQty - ISNULL(ot.Qty,0) PendingQty,
        o.TotalAmount TotalAmt,
		e.EmpName RequestBy,
		o.Remarks,
		o.Id,
		o.VoucherId,o.IsDeleted,
		o.CreateDate,o.CreateUser,o.ModifyDate,o.ModifyUser
	from Ord o
	LEFT OUTER JOIN dbo.Voucher v on v.id=o.VoucherId
	LEFT OUTER JOIN dbo.Division d on d.Id = o.DivisionId
	LEFT OUTER JOIN dbo.Emp e on e.Id = o.EmpId
	LEFT OUTER JOIN (
					SELECT od.Id, SUM(ct.Qty) Qty
					FROM dbo.OrdTrans ct
					left outer join ord od on ct.OrdId = od.Id
					WHERE  ct.Isdeleted = 0 AND ct.IsActive = 1 AND isnull(ct.RefId,0)<>0  AND isnull(ct.RefVoucherId,0)<>0 
					GROUP BY od.Id
	) ot ON ot.Id = o.Id
	WHERE o.IsActive=1 and o.IsDeleted=@Deleted
	AND v.VTypeId=@VTypeId
	AND o.CompId=@CompanyId and o.YearId=@YearId
	AND (o.VoucherDate between @fromDate and @ToDate)
	ORDER BY o.VoucherDate DESC, o.VoucherNo desc
END

GO

