CREATE PROCEDURE [dbo].[ListofBill]
  	@CompanyId  INT,
	@AccountId INT,
	@VoucherTypeId  INT,
	@RefId INT 

AS
BEGIN
	 SELECT  bb.Amount,br.BillNo,br.VoucherNo AS ChallanNo,br.VoucherDate AS ChlnDate,v.VoucherName,
	 ISNULL(br.GrossAmt,0.00) AS Total, ISNULL(br.BillAmt,0.00) AS NetTotal,
	 ISNULL(br.TdsAmt,0.00) AS TdsAmt,ISNULL(br.RetAmt,0.00) AS RetAmt,ISNULL(br.AdjustAmt,0.00) AS PaidAmt,  
	 ROUND(br.BillAmt-ISNULL(br.TdsAmt,00)-ISNULL(br.RetAmt,0.00)-ISNULL(br.AdjustAmt,0.00),0) AS DueAmt
FROM dbo.BtoB bb
LEFT OUTER JOIN dbo.BillRef br ON br.RowId = bb.RefCode
LEFT OUTER JOIN dbo.Voucher v ON v.VTypeId = br.BillVoucherId
WHERE bb.RefId = @RefId AND br.CompanyId = @CompanyId AND br.AccountId = @AccountId AND bb.RefVoucherId = @VoucherTypeId 


		--and (o.VoucherDate between @FromDate and @ToDate)
END
GO

