IF object_id('[dbo].[MonthlyStockReportTextile]') IS NULL 
EXEC ('CREATE PROC [dbo].[MonthlyStockReportTextile] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[MonthlyStockReportTextile]
  	@CompanyId  INT,
	@DivId INT = 0,
	@BranchId INT = 0,
	@PTypeId  INT ,
	@FromDate  INT,
	@ToDate INT ,
	@ptype VARCHAR(1) = 'N'

AS
BEGIN
 SELECT z.ItemId IId, z.OpQty,ISNULL(z.OpQty,0)+x.PurQty+x.InForJobQty+x.TransInQty +x.MillRec + x.JobRec + x.SRetQty + x.MRetQty - x.SaleQty - x.MillIsQty - x.IsForJobQty - x.PRetQty StockQty,x.* FROM (
 SELECT 
       st.VoucherDate,
	   pd.id ItemId,
	   pd.ProductName,
       CONVERT(DATE, CONVERT(VARCHAR(8), st.VoucherDate), 112) BillDate,
       'Q-'
       + CAST(FLOOR(((12 + MONTH(CONVERT(DATE, CONVERT(VARCHAR(8), st.VoucherDate))) - 4) % 12) / 3) + 1 AS VARCHAR(1)) Qtr,
	DATENAME(MONTH, CONVERT(DATE, CONVERT(VARCHAR(8), st.VoucherDate), 112)) AS [Month],
	       CASE
           WHEN MONTH(CONVERT(DATE, CONVERT(VARCHAR(8), st.VoucherDate), 112)) <= 3 THEN
               CAST(YEAR(CONVERT(DATE, CONVERT(VARCHAR(8), st.VoucherDate), 112)) - 1 AS VARCHAR(4)) + '-'
               + CAST(YEAR(CONVERT(DATE, CONVERT(VARCHAR(8), st.VoucherDate), 112)) AS VARCHAR(4))
           ELSE
               CAST(YEAR(CONVERT(DATE, CONVERT(VARCHAR(8), st.VoucherDate), 112)) AS VARCHAR(4)) + '-'
               + CAST(YEAR(CONVERT(DATE, CONVERT(VARCHAR(8), st.VoucherDate), 112)) + 1 AS VARCHAR(4))
       END AS FY,
	--CASE WHEN vch.VTypeId = 29 THEN st.RcptQty ELSE 0 END  AS OpQty,
	CASE WHEN ch.ChallanType = 1 OR vch.VTypeId = 13 OR vch.VTypeId = 32 THEN st.RcptQty ELSE 0 END  AS PurQty,
	CASE WHEN ch.ChallanType = 2 THEN st.RcptQty ELSE 0 END  AS InForJobQty,
    CASE WHEN ch.ChallanType = 4 THEN st.RcptQty ELSE 0 END  AS TransInQty,
	CASE WHEN vch.VTypeId = 7 OR vch.VTypeId = 31 THEN st.RcptQty ELSE 0 END AS MillRec,
	CASE WHEN vch.VTypeId = 8 OR vch.VTypeId = 30 THEN st.RcptQty ELSE 0 END AS JobRec ,
	CASE WHEN vch.VTypeId = 19 OR ch.ChallanType = 3 THEN st.RcptQty ELSE 0 END AS SRetQty,
	CASE WHEN vch.VTypeId = 49 OR ch.ChallanType = 49 THEN st.RcptQty ELSE 0 END AS MRetQty,

	CASE WHEN ch.ChallanType = 6 OR vch.VTypeId = 12 THEN st.IssueQty ELSE 0 END AS SaleQty,
	CASE WHEN ch.ChallanType = 7 THEN st.IssueQty ELSE 0 END AS MillIsQty,
	CASE WHEN ch.ChallanType = 8 THEN st.IssueQty ELSE 0 END AS IsForJobQty,
	CASE WHEN vch.VTypeId = 18 OR ch.ChallanType = 12 THEN st.IssueQty ELSE 0 END AS PRetQty
	
 --   st.RcptQty - st.IssueQty AS StockQty
	FROM dbo.StockTrans st
	LEFT OUTER JOIN dbo.Voucher vch ON vch.Id = st.VoucherId
	LEFT OUTER JOIN dbo.Product pd ON pd.Id = st.ItemId
	LEFT OUTER JOIN dbo.Challan ch ON ch.RowId = st.MasterRefId
	WHERE st.IsDeleted =0 AND st.IsActive = 1 AND  st.VoucherDate
      BETWEEN @FromDate AND @ToDate AND st.CompanyId = @CompanyId AND pd.PTypeId = 2 ) x
	  	LEFT OUTER JOIN (
	SELECT ISNULL(st.ItemId,0) AS ItemId,			
    CASE WHEN vch.VTypeId = 5 OR vch.VTypeId = 13 OR vch.VTypeId = 32 THEN st.RcptQty ELSE 0 END  AS OpQty  
	FROM dbo.StockTrans st
	LEFT OUTER JOIN dbo.Voucher vch ON vch.Id = st.VoucherId
	LEFT OUTER JOIN dbo.Product pd ON pd.Id = st.ItemId
	WHERE st.IsDeleted = 0 AND st.IsActive = 1 AND st.CompanyId = @CompanyId -- AND st.YearId = 1 
		AND  st.VoucherDate < @FromDate ) z ON z.ItemId = x.ItemId
   


END
GO

