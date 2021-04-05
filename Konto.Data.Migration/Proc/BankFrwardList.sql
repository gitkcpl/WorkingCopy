
IF object_id('[dbo].[BankFrwardList]') IS NULL 
EXEC ('CREATE PROC [dbo].[BankFrwardList] AS SELECT 1 AS Id') 
GO 

ALTER PROCEDURE   [dbo].[BankFrwardList] 
@id int=29005,
 @reportid int=0,
  @bill varchar(1)='N',
  @vtypeId int=16,
  @fromdate int
AS
BEGIN 
 
	  select   bt.Id as BillTransID ,ac.AccName BankName,
		   ac.GstIn,
		   bm.CompId,
           cm.CompName,
           bm.VoucherId,
           v.VoucherName,
           bm.VoucherDate ,
            bm.VoucherNo,
		   br.BillNo as BillNo,
		      CONVERT(DATETIME2, CONVERT(VARCHAR(8), br.VoucherDate), 112) InvoiceDate, 
		   bt.Remark as Remarks,
		   toacc.AccName as ToAccName, 
		   ISNULL(br.GrossAmt,0.00) AS Total,
	 ISNULL(br.BillAmt,0.00) AS NetTotal, 
	    ISNULL(pbb.Pay,0) AS PaidAmt,
	 ISNULL(br.TdsAmt,0.00) AS TdsAmt,
	 ISNULL(pbb.Rg,0) AS RetAmt , 
	 CAST(ISNULL(bb.Amount,0) AS NUMERIC(18,2)) AS Amount, 
		   isnull(br.BillAmt,0) as BillAmount, 
		   ( ISNULL(bb.Adla1,0) + ISNULL(bb.Adla2,0) + 
				ISNULL(bb.Adla3,0) + ISNULL(bb.Adla4,0) + ISNULL(bb.Adla5,0) + ISNULL(bb.Adla6,0) + ISNULL(bb.Adla7,0) +
				ISNULL(bb.Adla8,0) + ISNULL(bb.Adla9,0) + ISNULL(bb.Adla10,0)) DiscAmt , 
		   Isnull(bb.Amount,0) AdjustAmt,
		   bt.ChequeDate,bt.ChequeNo,
		   cm.PrintName PrintCompany,
           cm.Address1 CAddress,
           cm.Address2 CAddress2, 
		   ccty.CityName as CCityName,
		   cst.StateName as CStateName
from BillMain bm 
left outer join BillTrans bt on bt.BillId=bm.Id
LEFT OUTER JOIN dbo.Company cm
            ON cm.Id = bm.CompId
        LEFT OUTER JOIN dbo.Voucher v
            ON v.Id = bm.VoucherId
        LEFT OUTER JOIN dbo.VchSetup vst
            ON vst.VoucherId = v.Id AND vst.CompId = bm.CompId 
			 LEFT OUTER JOIN dbo.City ccty
            ON ccty.Id = cm.CityId
        LEFT OUTER JOIN dbo.[State] cst
            ON cst.Id = cm.StateId
	 LEFT OUTER JOIN dbo.Acc ac
            ON ac.Id = bm.AccId
	left outer join Acc ToAcc on ToAcc.Id=bt.ToAccId
 left outer join BtoB bb on bb.RefId = bt.BillId AND bb.RefTransId = bt.Id
 left outer join BillRef br on br.RowId = bb.RefCode
 LEFT OUTER JOIN ( 
				SELECT SUM(b1.Amount) AS Amount,
				SUM(CASE WHEN b1.TransType = 'Payment' THEN b1.Amount else 0 end) as  Pay , 
				
				SUM(CASE WHEN b1.TransType = 'Return' THEN b1.Amount ELSE 0 END) Rg ,
				b1.RefCode,b1.RefId,b1.RefTransId 
				FROM dbo.BtoB b1
				left outer join BillMain bm1 on b1.RefId = bm1.Id
				WHERE b1.IsActive =1 AND b1.IsDeleted = 0 
				and bm1.VoucherDate < @fromdate and bm1.Id < @id
				GROUP BY b1.RefCode,b1.RefId,b1.RefTransId 
				)pbb on  pbb.RefId = bt.BillId AND pbb.RefTransId = bt.Id
				 
where bm.IsActive=1 and bm.IsDeleted=0 and bt.IsActive=1 and bt.IsDeleted=0
and v.VTypeId=@vtypeId
	  and (bm.Id=@id or @id=0)
	    	AND (@Bill ='N' OR  EXISTs(SELECT 1
							 FROM dbo.ReportPara RP
							 WHERE RP.ParameterValue = bm.Id
							AND RP.ReportId=@reportid
							AND RP.ParameterName='BillId'))

ORDER BY bm.VoucherDate desc, bm.VoucherNo desc 
END
GO