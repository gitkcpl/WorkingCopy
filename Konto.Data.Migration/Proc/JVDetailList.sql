IF object_id('[dbo].[JVDetailList]') IS NULL 
EXEC ('CREATE PROC [dbo].[JVDetailList] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[JVDetailList]
@VTypeId int,
 @CompanyId int,
  @BranchId int,
 @YearId int,
 @FromDate int,
 @ToDate int
AS
BEGIN 
	   SELECT 
	    CASE WHEN bm.VoucherDate>10101 THEN ISNULL(CONVERT(Date,Convert(varchar(8),bm.VoucherDate),112),'')  END as VoucherDate,
	   bm.VoucherNo,
	   ToAc.AccName as Particulars,
	   bt.Remark Narration,
	   bt.Total as CrAmt,
	   bt.NetTotal DrAmt,
	   bt.RPType,
	   bm.Remarks,
	   bm.VoucherId,bm.Id,bm.IsDeleted
 	   FROM BillTrans bt 	
       LEFT OUTER JOIN BillMain bm on bt.BillId=bm.Id
       LEFT OUTER JOIN Acc ac on bm.AccId =ac.Id  
       LEFT OUTER JOIN  Voucher v on bm.VoucherId =v.Id
       LEFT OUTER JOIN Acc ToAc on bt.ToAccId=ToAc.Id   
       WHERE bm.IsActive=1 and bm.IsDeleted=0 AND bt.IsDeleted = 0 AND bm.CompId = @CompanyId AND bm.YearId = @YearId
       AND (bm.VoucherDate  between @FromDate and @ToDate) and v.VTypeId=@VTypeId
       ORDER BY bm.VoucherDate desc
    
END
GO

