CREATE PROCEDURE [dbo].[YarnWIP]
 	@CompanyId int=1,
	@FromDate int=0,
	@ToDate int=0,
	@ReportId int=0,
	@BranchId int=1,
	@DivId int=0,
	@YearId INT ,
	@VTypeID int=0,
	@Product Varchar(1)='N',
	@Grade Varchar(1)='N', 
	@color varchar(1)='N'
	
AS
BEGIN

SELECT x.ProductName, SUM(x.OpQty) AS Opening, 
SUM(x.IssQty) AS IssQty, 0 AS WipAdjustp, 
SUM(x.OpQty) + SUM(x.IssQty) AS SubTotal ,
SUM(x.IssRetQty) AS IssRetQty, 
SUM(x.Production) AS Production,0 AS WipAdjustn, 
SUM(x.OpQty)+SUM(x.IssQty)-SUM(x.IssRetQty)-SUM(x.Production) AS Closing, 
SUM(x.Wastage) AS Wastage FROM (
			SELECT pd.ProductName, 
			SUM(CASE WHEN c.VoucherDate < @FromDate AND vc.VTypeId = 9 THEN ct.Qty ELSE 0 END)  AS OpQty ,
			SUM(CASE WHEN vc.VTypeId = 9 THEN ct.Qty ELSE 0 END)  AS IssQty ,
			SUM(CASE WHEN vc.VTypeId = 35 THEN ct.Qty ELSE 0 END)  AS IssRetQty ,
			0 AS Production , 0 AS Wastage 
			FROM dbo.ChallanTrans ct
			LEFT OUTER JOIN dbo.Challan c ON c.Id = ct.ChallanId
			LEFT OUTER JOIN dbo.Product pd ON pd.Id = ct.ProductId
			LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = c.VoucherId
			WHERE vc.VTypeId = 35 OR vc.VTypeId = 9 AND ct.IsDeleted = 0 AND c.IsDeleted = 0  AND c.CompId = @CompanyId AND c.VoucherDate BETWEEN @FromDate AND @ToDate
			GROUP BY pd.ProductName
			UNION  ALL
			SELECT pd.ProductName,0, 0,0,SUM(p.NetWt) AS Production, SUM(p.NetWt*(100-bt.Per)/100) AS Wastage FROM  dbo.BatchTrans bt 
			LEFT OUTER JOIN dbo.Product pd ON pd.Id = bt.ItemId
			LEFT OUTER JOIN dbo.Batch b ON b.Id = bt.BatchId
			LEFT OUTER JOIN (    SELECT p.BatchId, SUM(p.NetWt) AS NetWt FROM dbo.Prod p
								WHERE p.IsDeleted = 0 
								GROUP BY p.BatchId
							) p ON p.BatchId = b.Id
							WHERE bt.IsDeleted = 0  AND b.CompId = @CompanyId AND b.VoucherDate BETWEEN @FromDate AND @ToDate
			GROUP BY pd.ProductName
			 ) x
 GROUP BY x.ProductName
 
END
GO

