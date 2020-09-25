IF object_id('[dbo].[PaymemtReceiptDetailList]') IS NULL 
EXEC ('CREATE PROC [dbo].[PaymemtReceiptDetailList] AS SELECT 1 AS Id') 
GO

ALTER  PROCEDURE [dbo].[PaymemtReceiptDetailList]
@VTypeId int,
 @CompanyId int,
  @BranchId int,
 @YearId int,
 @FromDate int,
 @ToDate int
AS
BEGIN 
	  SELECT   v.VoucherName,
	  CASE WHEN bm.VoucherDate>10101 THEN ISNULL(CONVERT(Date,Convert(varchar(8),bm.VoucherDate),112),'')  END as VoucherDate,
	  bm.VoucherNo,
	  ac.AccName Cash_Bank,
	  bt.Remark Narration ,
	  bt.RPType,
	  Toacc.AccName as Particulars,
	  bt.NetTotal,bt.ChequeNo,
	  bt.ChequeDate,
	  RBank.AccName RefBank,
	  bt.Sgst,bt.Cgst,bt.Igst,
	  bm.Remarks,
	  bm.Id,bm.VoucherId, bm.IsDeleted, bt.Id TransId	  
 	 FROM BillMain bm
	 LEFT OUTER JOIN Acc ac on bm.AccId =ac.Id 
     LEFT OUTER JOIN BillTrans bt on bt.BillId=bm.Id
     LEFT OUTER JOIN Acc ToAcc on ToAcc.Id=bt.ToAccId
     LEFT OUTER JOIN Acc RBank on RBank.Id=bt.RefBankId 
     LEFT OUTER JOIN Voucher v on v.Id=bm.VoucherId
     WHERE bm.IsActive=1 and bm.IsDeleted=0 AND bt.IsDeleted = 0
     AND (bm.VoucherDate  between @FromDate and @ToDate) 
     AND v.VTypeId=@VTypeId AND bm.CompId = @CompanyId AND bm.YearId = @YearId
    --select * from vouchertype
     ORDER by bm.VoucherDate desc, bm.VoucherNo desc
    
END
GO

