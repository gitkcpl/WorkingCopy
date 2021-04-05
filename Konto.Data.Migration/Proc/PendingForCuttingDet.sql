IF object_id('[dbo].[PendingForCuttingDet]') IS NULL 
EXEC ('CREATE PROC [dbo].[PendingForCuttingDet] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[PendingForCuttingDet]
    @CompanyId int,
	@FromDate int,
	@ToDate int,
	@ChallanId int
	
AS
BEGIN

	SELECT c.VoucherId,ct.LotNo, ct.RefNo, c.VoucherNo AS ChallanNo,p.ProductName AS Product, ct.ProductId, ct.ColorId,ct.GradeId,
	ct.DesignId, de.ProductName AS DesignNo,
	cl.ColorName AS Color ,ISNULL(po.SrNo,0) AS Takano,po.VoucherNo TakaVNo, po.GrayMtr AS GreyMtr,  
	po.FinMrt AS FinMtr, 
	ct.ChallanId Id, ct.Id AS TransId, po.ProdId
	FROM  dbo.ChallanTrans ct
	LEFT OUTER JOIN dbo.ProdOut po ON ct.Id = po.TransId
	LEFT OUTER JOIN dbo.Challan c ON c.Id = ct.ChallanId
	LEFT OUTER JOIN dbo.Product p ON p.Id = ct.ProductId
    LEFT OUTER JOIN dbo.Acc ac ON ac.Id = c.AccId
	LEFT OUTER JOIN dbo.Color cl ON cl.Id = ct.ColorId
	LEFT OUTER JOIN dbo.Product de ON de.Id = ct.DesignId
	WHERE ct.IsActive=1 and ct.IsDeleted=0 AND ct.Id = @ChallanId AND NOT EXISTS ( SELECT 1 FROM dbo.ChallanTrans ctt WHERE ctt.MiscId = po.Id AND ctt.IsDeleted = 0 AND ctt.IsActive = 1 )
	 
END
GO

