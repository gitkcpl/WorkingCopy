IF object_id('[dbo].[PendingForCutting]') IS NULL 
EXEC ('CREATE PROC [dbo].[PendingForCutting] AS SELECT 1 AS Id') 
GO

ALTER  PROCEDURE [dbo].[PendingForCutting]
    @CompanyId INT,
	@VoucherTypeId INT = 0,
	@AccountId INT = 0
	
AS
BEGIN
	SELECT
	ct.LotNo, c.VoucherNo AS ChallanNo, ct.ProductId, pd.ProductName AS Product,
	c.Id ,ct.Id TransId,ISNULL(CONVERT(Date,Convert(varchar(8),c.VoucherDate),112),'') ChallanDate,
	 p.VoucherNo TakaNo, po.GrayMtr GrayMeter, po.FinMrt FinMeter,
	 po.Id TakaId,ct.ColorId,ct.DesignId,dm.ProductCode DesignNo,
	 cl.ColorName,ct.GradeId,gd.GradeName,c.VoucherId
	FROM dbo.ChallanTrans ct 	
	LEFT OUTER JOIN  dbo.Challan c ON c.Id = ct.ChallanId
	LEFT OUTER JOIN dbo.Product dm on ct.DesignId = dm.Id
	LEFT OUTER JOIN dbo.Color cl on ct.ColorId = cl.Id
	LEFT OUTER JOIN dbo.ProdOut po ON ct.Id = po.TransId
	LEFT OUTER JOIN dbo.Prod p ON p.Id = po.ProdId
	LEFT OUTER JOIN dbo.Product pd ON pd.Id = ct.ProductId
	LEFT OUTER JOIN dbo.Grade gd on ct.GradeId = gd.Id
	LEFT OUTER JOIN dbo.Voucher v ON v.Id = c.VoucherId
	WHERE ct.IsActive=1 and ct.IsDeleted=0  and po.IsDeleted=0
	AND NOT EXISTS (
				SELECT 1 FROM dbo.ChallanTrans ct1 
				LEFT OUTER JOIN dbo.Challan ch ON ch.Id = ct1.ChallanId 
				LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = ch.VoucherId
				WHERE ct1.IsActive=1 and ct1.IsDeleted=0 AND vc.VTypeId = 22 AND po.Id = ct1.BatchId )
	AND c.AccId = @AccountId
	AND c.CompId= @CompanyId
	AND v.VTypeId = @VoucherTypeId
	 
END

GO

