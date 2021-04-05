IF object_id('[dbo].[pending_request_for_order]') IS NULL 
EXEC ('CREATE PROC [dbo].[pending_request_for_order] AS SELECT 1 AS Id') 
GO


ALTER  PROCEDURE [dbo].[pending_request_for_order]
 	@CompanyId int,
	@VoucherTypeID int,
	@ordStatus varchar(100)='APPROVED' ,
	@ordType INT = 0
AS
BEGIN
	select o.VoucherNo RequestNo,o.VoucherDate, 
	 ISNULL(CONVERT(Date,Convert(varchar(8),o.VoucherDate),112),'') RequestDate,
	 ot.Qty,
	cast((ot.Qty-isnull(ct.Qty,0))  as numeric(18,2)) as PendQty,
	p.ProductName as Product,ot.ProductId,ot.ColorId,ot.DivisionId,ot.DesignId,ot.GradeId,
	ot.UomId,
	cast(ot.rate as numeric(18,2)) as rate,
	ot.Total,um.UnitName Unit,
	ot.Id as TransId,o.Id ,o.VoucherId , o.Remarks
	,clr.ColorName,design.ProductCode as DesignNo,grd.GradeName, o.RefNo,
	em.EmpName RequestBy,ot.RequireDate,d.DivisionName Division,
	tm.Cgst,tm.Sgst,tm.Igst
	from Ord o
	left outer join OrdTrans ot on ot.OrdId=o.Id
	left outer join Product p on p.Id=ot.ProductId
	left outer join Division d on d.Id=ot.DivisionId 
	LEFT OUTER JOIN dbo.Color clr ON clr.Id =ot.ColorId
	LEFT OUTER JOIN dbo.Product design ON design.Id =ot.DesignId
	LEFT OUTER JOIN dbo.Grade grd ON grd.Id =ot.GradeId
	LEFT OUTER JOIN dbo.Emp em on em.Id = o.EmpId
	LEFT OUTER JOIN dbo.Uom um on um.Id  = ot.UomId
	LEFT OUTER JOIN dbo.TaxMaster tm on tm.Id = p.TaxId
	left outer join (SELECT ct.RefId, SUM(ct.Qty) Qty
					FROM dbo.OrdTrans ct
					WHERE  ct.Isdeleted = 0 AND ct.IsActive = 1 AND isnull(ct.RefId,0)<>0  AND isnull(ct.RefVoucherId,0)<>0 
					GROUP BY ct.RefId
	)ct on ct.RefId=ot.Id 
	left outer join Voucher v on v.Id=o.VoucherId 
	WHERE ot.OrdStatus = @ordStatus --AND o.OrderStatusId =@ordType--'APPROVED' 
	AND  o.CompId = @CompanyId
	 AND  (v.VTypeId=@VoucherTypeID ) 
	and ((ot.Qty-isnull(ct.Qty,0)) >0) AND o.IsActive =1 AND o.IsDeleted = 0
	
END


GO

