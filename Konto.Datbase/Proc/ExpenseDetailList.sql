CREATE PROCEDURE [dbo].[ExpenseDetailList]
@VTypeId int,
 @CompanyId int,
  @BranchId int,
 @YearId int,
 @FromDate int,
 @ToDate int
AS
BEGIN 
	   SELECT bm.BillType, bm.Rcm, bm.Itc,
	   CASE WHEN bm.VoucherDate>10101 THEN ISNULL(CONVERT(Date,Convert(varchar(8),bm.VoucherDate),112),'')  END as VoucherDate,
	   bm.VoucherNo,
	   bm.BillNo,
	   ac.AccName Party,
	   ToAc.AccName as ExpenseBook,
	   bt.Remark Narration,
	   bt.HsnCode,
	   bt.Qty,
	   bt.Rate,
	   bt.Total Gross,
	   bt.Disc DiscPer,
       bt.DiscAmt,
	   bt.FreightRate,
	   bt.Freight FreightAmt,
	   bt.OtherAdd,
	   bt.OtherLess,
	   bt.TcsPer,
	   bt.TcsAmt,
	   bt.NetTotal - bt.Cgst -bt.Sgst -bt.Igst TaxableAmt,
	   bt.CgstPer,
	   bt.Cgst CgstAmt,
	   bt.SgstPer,
	   bt.Sgst SgstAmt,
	   bt.IgstPer,
	   bt.Igst IgstAmt,
	   bt.NetTotal ,
	   bm.Remarks,
	   bm.TotalAmount BillAmount,
	  ISNULL(bl.Pay,0) PaidAmt,
	  ISNULL(bl.Rg,0) RetAmt,
	   bm.VoucherId,bm.Id, bm.IsDeleted
 	   FROM BillTrans bt 	
       LEFT OUTER JOIN BillMain bm on bt.BillId=bm.Id
       LEFT OUTER JOIN Acc ac on bm.AccId =ac.Id  
       LEFT OUTER JOIN  Voucher v on bm.VoucherId =v.Id
       LEFT OUTER JOIN Acc ToAc on bt.ToAccId=ToAc.Id   
	   	  	 LEFT OUTER JOIN (SELECT BillId, 
				SUM(CASE WHEN TransType = 'Payment' THEN Amount + ISNULL(Adla1,0) + ISNULL(Adla2,0) +ISNULL(Adla3,0) +ISNULL(Adla4,0) +ISNULL(Adla5,0) +ISNULL(Adla6,0) +ISNULL(Adla7,0) +ISNULL(Adla8,0) +ISNULL(Adla9,0) +ISNULL(Adla10,0) ELSE 0 END) Pay , 
				SUM(CASE WHEN TransType = 'Return' THEN Amount + ISNULL(Adla1,0) + ISNULL(Adla2,0) +ISNULL(Adla3,0) +ISNULL(Adla4,0) +ISNULL(Adla5,0) +ISNULL(Adla6,0) +ISNULL(Adla7,0) +ISNULL(Adla8,0) +ISNULL(Adla9,0) +ISNULL(Adla10,0) ELSE 0 END) Rg  FROM dbo.BtoB GROUP BY BillId) bl ON bl.BillId = bm.Id
 
       WHERE bm.IsActive=1 and bm.IsDeleted=0 AND bt.IsDeleted = 0 AND bm.CompId = @CompanyId AND bm.YearId = @YearId
       AND (bm.VoucherDate  between @FromDate and @ToDate) and v.VTypeId=@VTypeId 
       ORDER BY bm.VoucherDate desc
    
END
GO

