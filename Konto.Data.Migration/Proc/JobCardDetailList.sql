IF object_id('[dbo].[JobCardDetailList]') IS NULL 
EXEC ('CREATE PROC [dbo].[JobCardDetailList] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[JobCardDetailList]
 	@CompanyId int,
	@VoucherTypeID INT,
	@FromdDate INT,
	@ToDate INT
AS
BEGIN
	SELECT o.VoucherNo,v.VoucherName,
	o.ProgramNo OrderNo, 
	d.DivisionName,
	m.MachineName,
	acc.AccName,
	o.Type,
	CASE WHEN o.VoucherDate>10101 THEN ISNULL(CONVERT(Date,Convert(varchar(8),o.VoucherDate),112),'') END AS VoucherDate,
	o.OrdDate AS OrderDate, o.CarrierNo,
	o.ProgramNo,
	CONVERT(Date,Convert(varchar(8),o.VoucherDate),112)as VouchDate, em.EmpName IssueBy,
	acc.AccName Party,p.ProductName ,
	cl.ColorName,
	o.Remark,
	o.Qty TotalQty,
	o.OrderQty, 
	o.VoucherId	,o.Id 
	from dbo.JobCard o 
	LEFT OUTER JOIN dbo.Acc acc ON acc.Id = o.AccountId 	
	LEFT OUTER JOIN Voucher v on v.Id = o.VoucherId
	LEFT OUTER JOIN dbo.Product p ON p.Id = o.ProductId
	LEFT OUTER JOIN dbo.Color cl ON cl.Id = o.ColorId
	LEFT OUTER JOIN dbo.Emp em ON em.Id = o.BatchId    
	LEFT OUTER JOIN dbo.Division d ON d.Id = o.DivId 
	LEFT OUTER JOIN dbo.MachineMaster m ON m.Id = o.MachineId
	WHERE  v.VTypeId=33 AND o.CompanyId = 1
	AND o.IsActive =1 AND o.IsDeleted = 0 AND ( o.VoucherDate BETWEEN @FromdDate AND @ToDate)
	
END

GO

