CREATE  PROCEDURE [dbo].[PendingForCutting]
    @CompanyId INT,
	@VoucherTypeId INT = 0,
	@AccountId INT = 0
	
AS
BEGIN
	SELECT
	ct.LotNo, c.VoucherNo AS ChallanNo, ct.ProductId, pd.ProductName AS Product
	,ISNULL(ct.Pcs,0) AS TPcs, ct.Qty AS TMeter,c.Id ,ct.Id TransId, po.VoucherNo TakaNo, p.NetWt GrayMeter, po.FinMrt FinMeter, po.Id TakaId
	FROM dbo.ChallanTrans ct 	
	LEFT OUTER JOIN  dbo.Challan c ON c.Id = ct.ChallanId
	LEFT OUTER JOIN dbo.ProdOut po ON ct.Id = po.TransId
	LEFT OUTER JOIN dbo.Prod p ON p.Id = po.ProdId
	LEFT OUTER JOIN dbo.Product pd ON pd.Id = ct.ProductId
	LEFT OUTER JOIN dbo.Voucher v ON v.Id = c.VoucherId
	WHERE ct.IsActive=1 and ct.IsDeleted=0 
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

