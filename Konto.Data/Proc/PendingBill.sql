IF object_id('[dbo].[PendingBill]') IS NULL 
EXEC ('CREATE PROC [dbo].[PendingBill] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[PendingBill]
 	@CompanyId  INT = 1,
	@AccountId INT,
	@FromDate INT =20180401,
	@ToDate INT = 20190331,
	@VoucherTypeId INT,
	@BillType VARCHAR(25),
	@RefId INT,
	@RefVoucherId INT = 0,
	@RefTransId INT = 0
  
AS
BEGIN
	 SELECT  br.BillId,
	 ISNULL(br.BillTransId,0) AS BillTransId,
	 br.BillVoucherId,CASE WHEN v.VTypeId = 13 or v.VTypeId=26 THEN br.BillNo else br.VoucherNo END AS BillNo,
	 br.RowId AS RefCode, br.AccountId AS ToAccId,
	 br.BillNo  ChallanNo, 
	 br.VoucherDate AS ChlnDate,@RefTransId RefTransId,@RefVoucherId RefVoucherId,
	 v.VoucherName,
	 ac.AccName ,
	 ISNULL(br.GrossAmt,0.00) AS Total,
	 ISNULL(br.BillAmt,0.00) AS NetTotal,
	 CAST(ISNULL(adj.Pay,0.00) + ISNULL(selfa.Amount,0) - ISNULL(bb.Pay,0) AS NUMERIC(18,2)) + ISNULL(br.AdjustAmt,0) AS PaidAmt,
	 ISNULL(br.TdsAmt,0.00) AS TdsAmt,
	 CAST(ISNULL(adj.Rg,0.00)- ISNULL(bb.Rg,0) AS NUMERIC(18,2)) + ISNULL(br.RetAmt,0) AS RetAmt ,	
	 ROUND(br.BillAmt-ISNULL(br.TdsAmt,0) + ISNULL(br.TcsAmt,0) - ISNULL(selfa.Amount,0) - ISNULL(adj.Rg,0) -ISNULL(adj.Pay,0)  + ISNULL(bb.Rg,0) ,0) - ISNULL(br.RetAmt,0) - ISNULL(br.AdjustAmt,0) AS DueAmt, 
	 CAST(ISNULL(bb.Amount,0) AS NUMERIC(18,2)) AS Amount,
	 ISNULL(bb.Adla1,0.00) AS Adla1,  ISNULL(bb.Adla2,0.00) AS Adla2,  ISNULL(bb.Adla3,0.00) AS Adla3,  ISNULL(bb.Adla4,0.00) AS Adla4 , ISNULL(bb.Adla5,0.00) AS Adla5 , ISNULL(bb.Adla6,0.00) AS Adla6,
	 ISNULL(bb.Adla7,0.00) AS Adla7, ISNULL(bb.Adla8,0.00) AS Adla8, ISNULL(bb.Adla9,0.00) AS Adla9, ISNULL(bb.Adla10,0.00) AS Adla10         
FROM dbo.BillRef br
LEFT OUTER JOIN dbo.Acc ac ON ac.Id   = br.AccountId
LEFT OUTER JOIN dbo.Voucher v ON v.Id = br.BillVoucherId 
LEFT OUTER JOIN ( 
				SELECT SUM(bb.Amount) AS Amount,SUM(ISNULL(bb.Adla1,0)) AS Adla1,SUM(ISNULL(bb.Adla2,0)) AS Adla2,SUM(ISNULL(bb.Adla3,0)) AS Adla3,SUM(ISNULL(bb.Adla4,0)) AS Adla4,
				SUM(ISNULL(bb.Adla5,0)) AS Adla5,SUM(ISNULL(bb.Adla6,0)) AS Adla6,SUM(ISNULL(bb.Adla7,0)) AS Adla7,SUM(ISNULL(bb.Adla8,0)) AS Adla8,SUM(ISNULL(bb.Adla9,0)) AS Adla9,
				SUM(ISNULL(bb.Adla10,0)) AS Adla10,
				SUM(CASE WHEN bb.TransType = 'Payment' THEN bb.Amount + ISNULL(bb.Adla1,0) + ISNULL(bb.Adla2,0) + ISNULL(bb.Adla3,0) + ISNULL(bb.Adla4,0) + ISNULL(bb.Adla5,0) + ISNULL(bb.Adla6,0) + ISNULL(bb.Adla7,0) + ISNULL(bb.Adla8,0) + ISNULL(bb.Adla9,0) + ISNULL(bb.Adla10,0) ELSE 0 END) Pay , 
				SUM(CASE WHEN bb.TransType = 'Return' THEN bb.Amount ELSE 0 END) Rg ,
				bb.RefCode
				FROM dbo.BtoB bb 
				WHERE bb.IsActive =1 AND bb.IsDeleted = 0 AND bb.RefId = @RefId AND bb.RefVoucherId = @RefVoucherId AND bb.RefTransId = @RefTransId
				GROUP BY bb.RefCode) bb ON bb.RefCode = br.RowId 
LEFT JOIN (
			SELECT SUM(CASE WHEN b.TransType = 'Payment' THEN b.Amount + ISNULL(b.Adla1,0) + ISNULL(b.Adla2,0) + ISNULL(b.Adla3,0) + ISNULL(b.Adla4,0) + ISNULL(b.Adla5,0) + ISNULL(b.Adla6,0) + ISNULL(b.Adla7,0) + ISNULL(b.Adla8,0) + ISNULL(b.Adla9,0) + ISNULL(b.Adla10,0) ELSE 0 END) Pay , SUM(CASE WHEN b.TransType = 'Return' THEN b.Amount ELSE 0 END) Rg ,
                      b.RefCode
					              FROM dbo.BtoB b
								  WHERE b.IsActive = 1 AND b.IsDeleted = 0  
								    GROUP BY  b.RefCode
	) adj ON adj.RefCode = br.RowId 

LEFT OUTER JOIN ( 
					SELECT SUM(selfa.Amount) + SUM(ISNULL(selfa.Adla1,0)) + SUM(ISNULL(selfa.Adla2,0)) + SUM(ISNULL(selfa.Adla3,0)) + SUM(ISNULL(selfa.Adla4,0)) + SUM(ISNULL(selfa.Adla5,0)) + SUM(ISNULL(selfa.Adla6,0)) + SUM(ISNULL(selfa.Adla7,0)) + SUM(ISNULL(selfa.Adla8,0)) + SUM(ISNULL(selfa.Adla9,0)) + SUM(ISNULL(selfa.Adla10,0)) AS Amount,selfa.RefId, selfa.RefVoucherId FROM dbo.BtoB selfa 
					WHERE selfa.IsActive =1 AND selfa.IsDeleted = 0 
					GROUP BY selfa.RefId,selfa.RefVoucherId ) selfa ON selfa.RefId = br.BillId AND selfa.RefVoucherId = br.BillVoucherId				
WHERE br.AccountId=@AccountId AND (ROUND(br.BillAmt -ISNULL(selfa.Amount,0)-ISNULL(br.TdsAmt,0) + ISNULL(br.TcsAmt,0) - ISNULL(adj.Rg,0) -ISNULL(adj.Pay,0) +ISNULL(bb.Pay,0) + ISNULL(bb.Rg,0) ,0) - ISNULL(br.RetAmt,0) - ISNULL(br.AdjustAmt,0) > 0 OR CAST(bb.Amount AS NUMERIC(18))> 0)
AND br.CompanyId = @CompanyId AND br.IsActive = 1 AND br.IsDeleted = 0 AND br.RefType = @BillType		
ORDER BY br.VoucherDate, br.BillNo
END
GO

