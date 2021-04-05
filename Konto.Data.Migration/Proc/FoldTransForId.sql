IF object_id('[dbo].[FoldTransForId]') IS NULL 
EXEC ('CREATE PROC [dbo].[FoldTransForId] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[FoldTransForId]
	@id INT = 0
AS
BEGIN
    SELECT ct.ChallanId,
           ct.ProductId,
		   ct.BatchId,
           ct.Id,
		   p.VoucherNo TakaVNo,
		   p.ShPer,
		   p.ShMtr ShQty,
		   po.GrayMtr PendingQty,
		   po.VoucherNo ChallanNo,
           pd.ProductName ,
           ct.Sgst,
           ct.Qty,
           ct.LotNo,
		   ct.RefNo,		   
           ct.Pcs,
           ct.Cops,
           ct.Rate,
           ct.UomId,
           ct.Gross,
           ct.Disc,
           ct.DiscPer,
           ct.FreightRate,
           ct.Freight,
           ct.Igst,
           ct.IgstPer,
           ct.Cgst,
           ct.CgstPer,
           ct.SgstPer,
		   	ct.Weight,
		   ct.CessPer,
		   ct.Cess,
           ct.Total,
           ct.IsActive,
		   ct.ColorId,
		   ct.GradeId,
		   ct.DesignId,
           cl.ColorName,
           ct.CreateDate,
           ct.CreateUser,
           ct.IpAddress,
           ct.IsDeleted,
           ct.ModifyDate,
           ct.ModifyUser,
           ct.RefId,
           ct.RefVoucherId,
           ct.Remark,
           ct.RowId,
           ct.OtherAdd,
           ct.OtherLess,
           ct.IssuePcs,
           ct.IssueQty,
		   ct.MiscId, ct.RefId, ct.RefVoucherId
    FROM dbo.Challan c 
    LEFT OUTER JOIN dbo.ChallanTrans ct ON ct.ChallanId = c.Id
	LEFT OUTER JOIN dbo.ProdOut p ON p.TransId = ct.Id AND p.Qty> 0
    LEFT OUTER JOIN dbo.Product pd ON pd.Id = ct.ProductId
    LEFT OUTER JOIN dbo.Color cl ON cl.Id = ct.ColorId
    LEFT OUTER JOIN dbo.Product dm ON dm.Id = ct.DesignId
    LEFT OUTER JOIN dbo.Grade gd ON gd.Id = ct.GradeId
    LEFT OUTER JOIN dbo.ProdOut po ON po.Id = ct.BatchId
    WHERE ct.ChallanId = @id
          AND ct.IsDeleted = 0;

END;

GO

