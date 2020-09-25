IF object_id('[dbo].[SJList]') IS NULL 
EXEC ('CREATE PROC [dbo].[SJList] AS SELECT 1 AS Id') 
GO 

ALTER PROCEDURE [dbo].[SJList]
@VTypeId int,
 @CompanyId int,
 @YearId int,
 @FromDate int,
 @ToDate int,
 @Type int =1, 
 @BranchId int,
 @Deleted INT = 0
AS
BEGIN 
	 SELECT 
	 c.VoucherNo,
	 v.VoucherName,
	 ISNULL(CONVERT(Date,Convert(varchar(8),c.VoucherDate),112),'')  as ChallanDate, 
	  p.ProcessName,
	 CAST(ct.Qty AS NUMERIC(18,3)) AS TotalQty,
	 ct.IssueQty as IssueQty,
	 CAST(ct.Total AS NUMERIC(18,2) ) AS TotalAmount,
	 c.Remark,
	 c.VoucherId,c.Id, c.IsDeleted  
	FROM Challan c
	LEFT OUTER JOIN Voucher v on c.VoucherId =v.Id
	left outer join process p on p.Id=c.ProcessId
    LEFT OUTER JOIN (	
					SELECT ct.ChallanId, SUM(ct.Qty) Qty, SUM(ct.IssueQty) IssueQty,sum(ct.Total) Total FROM dbo.ChallanTrans ct
					 WHERE ct.Isdeleted = 0 AND ct.IsActive = 1
					GROUP BY ct.ChallanId
	) ct ON ct.ChallanId = c.Id
   WHERE c.IsActive=1 and c.IsDeleted=@Deleted AND c.CompId = @CompanyId AND c.YearId = @YearId
   AND (c.VoucherDate  between @FromDate and @ToDate) and v.VTypeId=@VTypeId
   ORDER BY  c.VoucherDate desc, c.VoucherNo desc

END

GO

