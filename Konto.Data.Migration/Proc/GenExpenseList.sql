IF object_id('[dbo].[GenExpenseList]') IS NULL 
EXEC ('CREATE PROC [dbo].[GenExpenseList] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[GenExpenseList]
@VTypeId int,
 @CompanyId int,
  @BranchId int,
 @YearId int,
 @FromDate int,
 @ToDate int,
 @Deleted INT = 0
AS
BEGIN 
	  SELECT  bm.BillType, bm.Rcm, bm.Itc,
	  CASE WHEN bm.VoucherDate>10101 THEN ISNULL(CONVERT(Date,Convert(varchar(8),bm.VoucherDate),112),'')  END AS Date,
	  bm.VoucherNo,
	  v.VoucherName,
	  bm.BillNo,
	  ac.AccName Party,
	  bk.AccName Book,
	  bm.GrossAmount,
	  bm.GrossAmount -bt.disc-bt.OtherL TaxableAmt,
	  bt.Cgst,
	  bt.Sgst,
	  bt.Igst,
	  bm.TotalAmount BillAmount,
	  ISNULL(bl.Pay,0) PaidAmt,
	  ISNULL(bl.Rg,0) RetAmt,
	 CASE WHEN bm.TotalAmount -ISNULL(bl.Pay,0)-ISNULL(bm.TdsAmt,0)- isnull(bl.Rg,0) = 0 THEN 'PAID' ELSE 'UNPAID' END AS Status,
     ISNULL(bm.TdsAmt,0) TdsAmt,
      bm.TdsPer,
	  bm.TcsPer,
	  bm.TcsAmt,
	  bm.Remarks,
	  ac.VatTds,
	  bm.CreateUser,
	  bm.CreateDate,
	  bm.ModifyUser,
	  bm.ModifyDate,
	  bm.Id,bm.VoucherId,bm.IsDeleted
 	  FROM BillMain bm
	  LEFT OUTER JOIN (SELECT BillId, SUM(DiscAmt) disc, SUM(OtherAdd) AS OtherA, SUM(OtherLess) AS OtherL, SUM(Cgst) Cgst, SUM(Sgst) Sgst, SUM(Igst) Igst FROM dbo.BillTrans GROUP BY BillId) bt ON bt.BillId = bm.Id
	  LEFT OUTER JOIN Acc ac on bm.AccId =ac.Id 
      LEFT OUTER JOIN Voucher v on v.Id=bm.VoucherId
	  LEFT OUTER JOIN dbo.Acc bk ON bm.BookAcId = bk.Id
	  	 LEFT OUTER JOIN (SELECT BillId, 
				SUM(CASE WHEN TransType = 'Payment' THEN Amount + ISNULL(Adla1,0) + ISNULL(Adla2,0) +ISNULL(Adla3,0) +ISNULL(Adla4,0) +ISNULL(Adla5,0) +ISNULL(Adla6,0) +ISNULL(Adla7,0) +ISNULL(Adla8,0) +ISNULL(Adla9,0) +ISNULL(Adla10,0) ELSE 0 END) Pay , 
				SUM(CASE WHEN TransType = 'Return' THEN Amount + ISNULL(Adla1,0) + ISNULL(Adla2,0) +ISNULL(Adla3,0) +ISNULL(Adla4,0) +ISNULL(Adla5,0) +ISNULL(Adla6,0) +ISNULL(Adla7,0) +ISNULL(Adla8,0) +ISNULL(Adla9,0) +ISNULL(Adla10,0) ELSE 0 END) Rg  FROM dbo.BtoB GROUP BY BillId) bl ON bl.BillId = bm.Id
 
      WHERE bm.IsActive=1 and bm.IsDeleted=@Deleted
      AND (bm.VoucherDate  between @FromDate and @ToDate) 
      AND v.VTypeId=@VTypeId AND bm.CompId = @CompanyId
 --select * from vouchertype
     ORDER BY bm.VoucherDate DESC, bm.VoucherNo Desc
    
END
GO

