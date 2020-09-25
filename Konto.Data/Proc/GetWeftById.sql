IF object_id('[dbo].[GetWeftById]') IS NULL 
EXEC ('CREATE PROC [dbo].[GetWeftById] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[GetWeftById]
    @Id INT 

AS
BEGIN
SELECT ot.Id, ot.OrdId,ot.AvgWt,ot.CancelReason, (wi.Qty/100)*ot.Qty Qty,wi.AccId as DeptId,ot.CommDescr, pd.ProductName, ot.WarpItemId, wi.ProductId,wi.ColorId, 
ot.DesignId,ot.GradeId,ot.Cut,pd.PurUomId UomId,ot.Width,ot.NoOfLot,ot.LotPcs, ot.Rate, ot.Total, ot.Disc,ot.DiscAmt,
ot.Sgst, ot.SgstAmt, ot.Cgst, ot.CgstAmt, ot.Igst, ot.IgstAmt, ot.Cess, ot.CessAmt,ot.NetTotal,ot.Remark, ot.DivisionId, ot.Priority,
ot.OrdStatus, ot.RefId,ot.RefVoucherId, ot.CreateDate, ot.ModifyDate, ot.IpAddress,ot.CreateUser, ot.ModifyUser, ot.IsActive,ot.IsDeleted,
ot.RowId FROM dbo.OrdTrans ot
LEFT OUTER JOIN dbo.Color cl ON cl.Id = ot.ColorId
INNER JOIN dbo.WeftItem wi ON wi.ItemId = ot.ProductId AND wi.MColorId = ot.ColorId AND wi.IsDeleted =0
LEFT OUTER JOIN dbo.Product pd ON pd.Id = wi.ProductId
LEFT OUTER JOIN dbo.TaxMaster td ON td.Id = pd.TaxId
WHERE ot.IsActive =1 AND ot.IsDeleted =0 AND  ot.OrdId = @id
END

GO

