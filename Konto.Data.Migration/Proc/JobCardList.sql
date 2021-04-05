IF object_id('[dbo].[JobCardList]') IS NULL 
EXEC ('CREATE PROC [dbo].[JobCardList] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[JobCardList]
 	@CompanyId int,
	@VTypeId INT,
	@fromDate INT=0,
	@ToDate INT=0,
	@yearid INT=0,
	@deleted INT=0
AS
BEGIN
	SELECT o.VoucherNo,v.VoucherName,o.ProgramNo OrderNo, o.OrdDate AS OrderDate, o.CarrierNo BeamNo,
	CONVERT(Date,Convert(varchar(8),o.VoucherDate),112)as VouchDate, em.EmpName IssueBy
	 ,acc.AccName Party,p.ProductName ,o.Qty TotalQty, o.VoucherId	,o.Id 
	from dbo.JobCard o 
	LEFT OUTER JOIN dbo.Acc acc ON acc.Id = o.AccountId 	
	LEFT OUTER JOIN Voucher v on v.Id = o.VoucherId
	LEFT OUTER JOIN dbo.Product p ON p.Id = o.ProductId
	LEFT OUTER JOIN dbo.Color cl ON cl.Id = o.ColorId
	LEFT OUTER JOIN dbo.Emp em ON em.Id = o.BatchId     
	WHERE  v.VTypeId=@vtypeid AND o.CompanyId = @CompanyId
	AND ( @fromdate=0 or o.VoucherDate BETWEEN @fromdate AND @ToDate)
	AND o.IsActive =1 AND o.IsDeleted = 0 
	ORDER BY o.VoucherDate desc
END
go
