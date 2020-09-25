IF object_id('[dbo].[PendingJRProd]') IS NULL 
EXEC ('CREATE PROC [dbo].[PendingJRProd] AS SELECT 1 AS Id') 
GO

ALTER  PROCEDURE [dbo].[PendingJRProd]
		@CompanyId int,
	@AccountId int,
	@VoucherTypeID int,
	@ChallanType int,
	@AddEdit int=0,
	@RefId int =0,
	@ProcessId INT =0
AS
BEGIN
if(@AddEdit=0)
begin
	SELECT (-1 * ct.Id) as Id ,ct.Id as TransId,c.Id as RefId ,c.VoucherId ,c.VoucherNo as ChallanNo,
    c.VoucherDate ChlnDate,ISNULL(np.ProductName,p.ProductName) as Product,col.ColorName as Color,ISNULL(ct.NProductId,ct.ProductId) ProductId,ct.ColorId, 
    0.00 Qty,um.Id UnitId,um.UnitName,ct.DesignId , de.ProductName Design, ct.Rate, ct.CgstPer, ct.Cgst, ct.SgstPer, ct.Sgst, ct.IgstPer, ct.Igst,
	cast((ct.Qty-isnull(pen.Qty,0.00))  as numeric(18,2)) as PendingQty,
	cast((ct.Pcs-isnull(pen.Pcs,0))  as numeric(18,2)) as PendingPcs,	 
	0.00 Pcs,	
	isnull(pen.Qty,0.00) as IssueQty,
	isnull(pen.Pcs,0.00) as IssuePcs,
	cast(isnull(ct.Pcs,0)  as numeric(18,2))  CurrPendingPcs,
	cast(isnull(ct.Qty,0.00)  as numeric(18,2)) CurrPendingQty,
	isnull(np.PTypeId,p.PTypeId) PTypeId --, ISNULL(pen.IsClear,0) IsClear
	FROM dbo.Challan c
	LEFT OUTER JOIN dbo.ChallanTrans ct on ct.ChallanId=c.Id
	LEFT OUTER JOIN dbo.Product p on p.Id=ct.ProductId
	LEFT OUTER JOIN dbo.Product np ON np.Id = ct.NProductId
	LEFT OUTER JOIN dbo.Uom um ON um.Id = ISNULL(np.UomId,p.UomId)
	LEFT OUTER JOIN dbo.Color col on col.Id=ct.ColorId
	LEFT OUTER JOIN dbo.Product de ON de.Id = ct.DesignId
	LEFT OUTER JOIN dbo.Division d on d.Id=c.DivId 
	LEFT OUTER JOIN dbo.Voucher v on v.Id=c.VoucherId
	LEFT OUTER JOIN (SELECT sum(CASE WHEN jr.IsClear = 1 THEN 9999999999999 ELSE jr.Qty END)as Qty,sum(Pcs) as Pcs,RefTransId FROM dbo.JobReceipt jr
			         WHERE jr.IsActive=1 and jr.IsDeleted=0 
			         GROUP BY jr.RefTransId
	)pen on pen.RefTransId=ct.Id 
	 
	WHERE ct.IsActive=1 and ct.IsDeleted=0  AND  (v.VTypeId=@VoucherTypeID OR v.VTypeId =45) and c.ChallanType=@ChallanType
	 and c.AccId=@AccountId AND c.ProcessId = @ProcessId -- AND ISNULL(pen.IsClear,0) <> 1 
	and ((isnull(ct.Qty,0)-isnull(pen.Qty,0)) >0) 
END
ELSE
BEGIN
	SELECT jr.Id Id ,ct.Id as TransId,c.Id as RefId,jr.ChallanId as ChallanId ,c.VoucherId ,c.VoucherNo as ChallanNo,
    c.VoucherDate ChlnDate,ISNULL(np.ProductName,p.ProductName) as Product,col.ColorName as Color,ISNULL(ct.NproductId,ct.ProductId) ProductId,ct.ColorId, 
    jr.Qty Qty,um.Id UnitId,um.UnitName, ct.DesignId, de.ProductName Design ,jr.PendingQty as PendingQty, ct.Rate,
	jr.IssueQty as IssueQty, jr.Pcs Pcs, ct.CgstPer, ct.Cgst, ct.SgstPer, ct.Sgst, ct.IgstPer, ct.Igst,
	jr.PendingPcs as PendingPcs,
	jr.IssuePcs as IssuePcs,
	cast((ct.Pcs-isnull(pen.Pcs,0.00))  as numeric(18,2))  CurrPendingPcs,
	cast((ct.Qty-isnull(pen.Qty,0))  as numeric(18,2)) CurrPendingQty,
	isnull(np.PTypeId,p.PTypeId) PTypeId --, ISNULL(pen.IsClear,0) IsClear
	from dbo.JobReceipt jr 
	left outer join dbo.Challan c on jr.RefId=c.Id
	left outer join dbo.ChallanTrans ct on ct.Id=jr.RefTransId
	left outer join dbo.Product p on p.Id=ct.ProductId
	LEFT OUTER JOIN dbo.Product np ON np.Id = ct.NProductId
	LEFT OUTER JOIN dbo.Uom um ON um.Id = ct.UomId
	left outer join dbo.Color col on col.Id=ct.ColorId
	LEFT OUTER JOIN dbo.Product de ON de.Id = ct.DesignId
	left outer join dbo.Division d on d.Id=c.DivId 
	left outer join dbo.Voucher v on v.Id=c.VoucherId
	left outer join (select sum(Qty)as Qty,sum(Pcs) as Pcs,RefTransId FROM dbo.JobReceipt jr
			WHERE jr.IsActive=1 and jr.IsDeleted=0
			GROUP BY jr.RefTransId
	)pen ON pen.RefTransId=ct.Id
	 
	WHERE jr.IsActive=1 and jr.IsDeleted=0 AND jr.ChallanId=@RefId
	END
	
END
GO

