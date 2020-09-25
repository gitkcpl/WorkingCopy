IF object_id('[dbo].[PaymemtReceiptList]') IS NULL 
EXEC ('CREATE PROC [dbo].[PaymemtReceiptList] AS SELECT 1 AS Id') 
GO

ALTER  PROCEDURE [dbo].[PaymemtReceiptList]
 @VTypeId int,
 @CompanyId int,
 @BranchId int,
 @YearId int,
 @FromDate int,
 @ToDate int,
 @Deleted INT = 0
AS
BEGIN 
	  SELECT  v.VoucherName,
	  CASE WHEN bm.VoucherDate>10101 
	  then ISNULL(CONVERT(Date,Convert(varchar(8),bm.VoucherDate),112),'')  end as VoucherDate,
	  bm.VoucherNo,ac.AccName Cash_Bank,
	  bm.TotalAmount,
	  bm.Remarks,
	  bm.Id,bm.VoucherId,bm.IsDeleted  
 	 FROM BillMain bm
	 LEFT OUTER JOIN Acc ac on bm.AccId =ac.Id   
     LEFT OUTER JOIN Voucher v on v.Id=bm.VoucherId
     WHERE bm.IsActive=1 and bm.IsDeleted=@Deleted
     AND (bm.VoucherDate  between @FromDate and @ToDate) 
     AND v.VTypeId=@VTypeId AND bm.CompId = @CompanyId AND bm.YearId = @YearId
 --select * from vouchertype
    ORDER BY bm.VoucherDate desc, bm.VoucherNo desc
    
END
GO

