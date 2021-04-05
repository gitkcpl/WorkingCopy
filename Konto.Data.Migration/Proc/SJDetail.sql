IF object_id('[dbo].[SJDetailList]') IS NULL 
EXEC ('CREATE PROC [dbo].[SJDetailList] AS SELECT 1 AS Id') 
GO 

ALTER PROCEDURE [dbo].[SJDetailList]
@VTypeId int,
 @CompanyId int, 
 @YearId int,
 @FromDate int,
 @ToDate int,
 @BranchId int
AS
BEGIN 
	 SELECT 
	 ISNULL(CONVERT(Date,Convert(varchar(8),c.VoucherDate),112),'')  as ChallanDate, 
	 c.VoucherNo VoucherNo,
	  v.VoucherName VoucherName,
	  prs.ProcessName,
	 p.ProductName Product,	 
	 ct.Qty,
	 ct.IssueQty ,
	 ct.Rate, 
	 ct.Gross,
	 ct.Total,
	 ct.Remark, c.Id,ct.Id as TransId,
	 c.VoucherId,
	 c.IsDeleted
	 FROM Challan c
     LEFT OUTER JOIN Voucher v ON c.VoucherId =v.Id
	 left outer join process prs on prs.Id=c.ProcessId
     LEFT OUTER JOIN ChallanTrans ct ON ct.ChallanId=c.Id
     LEFT OUTER JOIN Product p ON p.Id=ct.ProductId
      WHERE c.IsActive=1 and c.IsDeleted=0 AND ct.IsDeleted = 0
      AND (c.VoucherDate  between @FromDate and @ToDate) and v.VTypeId=@VTypeId
      ORDER BY c.VoucherDate desc
    
END

GO

