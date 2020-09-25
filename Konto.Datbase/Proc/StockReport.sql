CREATE PROCEDURE [dbo].[StockReport]
  	@CompanyId  INT,
	@DivId INT = 0,
	@BranchId INT = 0,
	@PTypeId  INT ,
	@FromDate  INT,
	@ToDate INT ,
	@ptype VARCHAR(1) = 'N'

AS
BEGIN
    SELECT p.ProductName Product,p.Id AS ItemId,
	CAST(ISNULL(z.StockQty,0)  as numeric(18,2)) + ISNULL(y.OpQty,0) AS OpQty,	
	CAST(ISNULL(y.StockQty,0) + ISNULL(z.StockQty,0) as numeric(18,2)) AS StockQty,
	y.OpPcs+ISNULL(z.StockPcs,0) OpPcs,
	y.StockPcs+ISNULL(z.StockPcs,0) AS StockPcs,
	ISNULL(y.BranchId,0) BranchId,
	ISNULL(y.PurQty,0) PurQty,
	ISNULL(y.InForJobQty,0) InForJobQty,
	ISNULL(y.TransInQty,0) TransInQty,
	ISNULL(y.MillRec,0) MillRec, 
	ISNULL(y.JobRec,0) JobRec,
	ISNULL(y.ProdQty,0) ProdQty,
	ISNULL(y.SRetQty,0) SRetQty,
	ISNULL(y.StoreIssRetQty,0) StoreIssRetQty,
	ISNULL(y.InFromJobQty,0) InFromJobQty,

	ISNULL(y.SaleQty,0) SaleQty,
	ISNULL(y.MillIsQty,0) MillIsQty,
	ISNULL(y.IsForJobQty,0) IsForJobQty,
	ISNULL(y.SaleJobQty,0) SaleJobQty,
	ISNULL(y.TransOutQty,0) TransOutQty,
	ISNULL(y.RefIssQty,0) RefIssQty,
	ISNULL(y.StoreIssQty,0) StoreIssQty,
	ISNULL(y.InwQty,0) InwQty,
	ISNULL(y.InwPcs,0) InwPcs, 
	ISNULL(y.OutQty,0) OutQty,
	ISNULL(y.OutPcs,0) OutPcs, 

	--InPcs
	y.PurPcs,
	y.InForJobPcs,
	y.TransInPcs,
	y.MillPcs,
	y.JobPcs,
	y.ProdPcs,
	y.SRetPcs,
	y.StoreIssRetPcs,
	y.InFromJobPcs,
	--Out Pcs
	y.MillIsPcs,
	y.SalePcs,
	y.StoreIssPcs,
	y.PRetPcs,
	y.IsForJobPcs, 
	y.SaleJobPcs , 
	y.TransOutPcs , 
	y.RefIssPcs,
	y.InwPcs,
	y.OutPcs

	FROM dbo.Product p 
	LEFT OUTER JOIN (
	--In Quantities
    SELECT x.BranchId,x.ItemId, x.OpQty AS OpQty ,CAST(x.PurQty  as numeric(18,2)) AS PurQty,
	CAST(x.InForJobQty  as numeric(18,2)) AS InForJobQty,
	CAST(x.TransInQty  as numeric(18,2)) AS TransInQty,
	CAST(x.MillRec  as numeric(18,2)) AS MillRec, 
	CAST(x.JobRec  as numeric(18,2)) AS JobRec, 
	CAST(x.ProdQty  as numeric(18,2)) AS ProdQty, 
	CAST(x.SRetQty  as numeric(18,2)) AS SRetQty, 
	CAST(x.StoreIssRetQty  as numeric(18,2)) AS StoreIssRetQty , 
	CAST(x.InFromJobQty  as numeric(18,2)) AS InFromJobQty,
	--Out Quantaties
	CAST(x.SaleQty  as numeric(18,2)) AS SaleQty, 
	CAST(x.MillIsQty  as numeric(18,2)) AS MillIsQty,
	CAST(x.IsForJobQty  as numeric(18,2)) AS IsForJobQty,
	CAST(x.SaleJobQty  as numeric(18,2)) AS SaleJobQty, 
	CAST(x.TransOutQty  as numeric(18,2)) TransOutQty,
	CAST(x.RefIssQty  as numeric(18,2)) RefIssQty,
	CAST(x.StoreIssQty  as numeric(18,2)) StoreIssQty ,

	CAST(x.PurQty + x.InForJobQty + x.TransInQty + x.MillRec + x.JobRec +x.ProdQty + x.SRetQty + x.StoreIssRetQty + x.InFromJobQty  as numeric(18,2)) AS InwQty,
	CAST(x.SaleQty + x.MillIsQty + x.IsForJobQty + x.SaleJobQty + x.TransOutQty +x.RefIssQty + x.StoreIssQty + x.PRetPcs as numeric(18,2)) AS OutQty,
	
	CAST(x.StockQty as numeric(18,2)) AS StockQty,
	ISNULL(x.OpPcs,0) AS OpPcs, 
	-- Inw Pcs
	x.PurPcs, 
	x.InForJobPcs , 
	x.TransInPcs, 
	x.MillPcs,	
	x.JobPcs, 
	x.ProdPcs, 
	x.SRetPcs , 
	x.StoreIssRetPcs , 
	x.InFromJobPcs , 	
	-- Outward Pcs
	x.MillIsPcs, 
	x.SalePcs, 
	x.StoreIssPcs, 
	x.PRetPcs, 
	x.IsForJobPcs, 
	x.SaleJobPcs , 
	x.TransOutPcs , 
	x.RefIssPcs,
	x.StockPcs  AS StockPcs ,
	x.PurPcs + x.InForJobPcs + x.TransInPcs + x.MillPcs + x.JobPcs + x.ProdPcs + x.SRetPcs + x.StoreIssRetPcs + x.InFromJobPcs AS InwPcs,

	x.MillIsPcs + x.SalePcs + x.StoreIssPcs + x.PRetPcs + x.IsForJobPcs + x.SaleJobPcs + x.TransOutPcs + x.RefIssPcs AS OutPcs
	FROM (
	SELECT st.BranchId ,pd.ProductName AS Product, ISNULL(st.ItemId,0) AS ItemId,
	SUM(CASE WHEN vch.VTypeId = 29 THEN st.RcptQty ELSE 0 END ) AS OpQty,
	SUM(CASE WHEN ch.ChallanType = 1 OR vch.VTypeId = 13 OR vch.VTypeId = 32 THEN st.RcptQty ELSE 0 END ) AS PurQty,
	SUM(CASE WHEN ch.ChallanType = 2 THEN st.RcptQty ELSE 0 END ) AS InForJobQty,
	SUM( CASE WHEN ch.ChallanType = 4 THEN st.RcptQty ELSE 0 END ) AS TransInQty,
	SUM(CASE WHEN vch.VTypeId = 7 OR vch.VTypeId = 31 THEN st.RcptQty ELSE 0 END) AS MillRec,
	SUM(CASE WHEN vch.VTypeId = 8 OR vch.VTypeId = 30 THEN st.RcptQty ELSE 0 END) AS JobRec ,
	SUM(CASE WHEN vch.VTypeId = 17 OR vch.VTypeId = 11 OR vch.VTypeId = 21 OR vch.VTypeId = 43 THEN st.RcptQty ELSE 0 END)  AS ProdQty,
	SUM(CASE WHEN vch.VTypeId = 19 OR ch.ChallanType = 3 THEN st.RcptQty ELSE 0 END) AS SRetQty,
	SUM(CASE WHEN vch.VTypeId = 35 THEN st.RcptQty ELSE 0 END) AS StoreIssRetQty,
	SUM(CASE WHEN ch.ChallanType = 13 THEN st.RcptQty ELSE 0 END) AS InFromJobQty,

	SUM( CASE WHEN vch.VTypeId = 29 THEN st.RcptNos ELSE 0 END ) AS OpPcs, 
	SUM(CASE WHEN ch.ChallanType = 1 OR vch.VTypeId = 13 OR vch.VTypeId = 32 THEN st.RcptNos ELSE 0 END) AS PurPcs,
	SUM(CASE WHEN ch.ChallanType = 2 THEN st.RcptNos ELSE 0 END ) AS InForJobPcs,
	SUM( CASE WHEN ch.ChallanType = 4 THEN st.RcptNos ELSE 0 END ) AS TransInPcs, 
	SUM(CASE WHEN vch.VTypeId = 7 OR vch.VTypeId = 31 THEN st.RcptNos ELSE 0 END) AS MillPcs,
	SUM(CASE WHEN vch.VTypeId = 8 OR vch.VTypeId = 30 THEN st.RcptNos ELSE 0 END) AS JobPcs , 
	SUM(CASE WHEN vch.VTypeId = 17 OR vch.VTypeId = 11 OR vch.VTypeId = 21 OR vch.VTypeId = 43 THEN st.RcptNos ELSE 0 END) AS ProdPcs, 
	SUM(CASE WHEN vch.VTypeId = 19 OR ch.ChallanType = 3 THEN st.RcptNos ELSE 0 END) AS SRetPcs,	
	SUM(CASE WHEN vch.VTypeId = 35 THEN st.RcptNos ELSE 0 END) AS StoreIssRetPcs,
	SUM(CASE WHEN ch.ChallanType = 13 THEN st.RcptNos ELSE 0 END) AS InFromJobPcs,

	SUM(CASE WHEN ch.ChallanType = 6 OR vch.VTypeId = 12 THEN st.IssueQty ELSE 0 END) AS SaleQty,
	SUM(CASE WHEN ch.ChallanType = 7 THEN st.IssueQty ELSE 0 END) AS MillIsQty,
	SUM(CASE WHEN ch.ChallanType = 8 THEN st.IssueQty ELSE 0 END) AS IsForJobQty,
	SUM(CASE WHEN ch.ChallanType = 9 THEN st.IssueQty ELSE 0 END) AS SaleJobQty,
	SUM(CASE WHEN ch.ChallanType = 10 THEN st.IssueQty ELSE 0 END) AS TransOutQty,
	SUM(CASE WHEN ch.ChallanType = 11 THEN st.IssueQty ELSE 0 END) AS RefIssQty,
	SUM(CASE WHEN vch.VTypeId = 9 OR vch.VTypeId = 21 OR vch.VTypeId = 11 OR vch.VTypeId = 17 OR vch.VTypeId = 43 THEN st.IssueQty ELSE 0 END) AS StoreIssQty,
	SUM(CASE WHEN vch.VTypeId = 18 OR ch.ChallanType = 12 THEN st.IssueQty ELSE 0 END) AS PRetQty,

	SUM(CASE WHEN vch.VTypeId = 6 OR vch.VTypeId = 12 THEN st.IssueNos ELSE 0 END) AS SalePcs,
	SUM(CASE WHEN ch.ChallanType = 7 THEN st.IssueNos ELSE 0 END) AS MillIsPcs,
	SUM(CASE WHEN ch.ChallanType = 8 THEN st.IssueNos ELSE 0 END) AS IsForJobPcs,
	SUM(CASE WHEN ch.ChallanType = 9 THEN st.IssueNos ELSE 0 END) AS SaleJobPcs,
	SUM(CASE WHEN ch.ChallanType = 10 THEN st.IssueNos ELSE 0 END) AS TransOutPcs,
	SUM(CASE WHEN ch.ChallanType = 11 THEN st.IssueNos ELSE 0 END) AS RefIssPcs, 
	SUM(CASE WHEN vch.VTypeId = 9 OR vch.VTypeId = 21 OR vch.VTypeId = 11 OR vch.VtypeId = 17 OR vch.VTypeId =43 THEN st.IssueNos ELSE 0 END) AS StoreIssPcs,
	SUM(CASE WHEN vch.VTypeId = 18 OR ch.ChallanType = 12 THEN st.IssueNos ELSE 0 END) AS PRetPcs,
	
    SUM(st.RcptQty) - SUM(st.IssueQty) AS StockQty, 
    SUM(st.RcptNos) - SUM(st.IssueNos) AS StockPcs    
	FROM dbo.StockTrans st
	LEFT OUTER JOIN dbo.Voucher vch ON vch.Id = st.VoucherId
	LEFT OUTER JOIN dbo.Product pd ON pd.Id = st.ItemId
	LEFT OUTER JOIN dbo.Challan ch ON ch.RowId = st.MasterRefId
	WHERE (pd.PTypeId = @PTypeId OR @ptype = 'N') AND st.CompanyId = @CompanyId AND (st.DivId = @DivId OR @DivId = 0)
	AND ( @BranchId = 0 OR st.BranchId = @BranchId )     
	AND  (st.VoucherDate between @FromDate and @ToDate) AND st.IsActive = 1 AND st.IsDeleted = 0	
	GROUP BY st.ItemId,pd.ProductName, st.BranchId ) x
	 ) y ON y.ItemId = p.Id
	LEFT OUTER JOIN (
	SELECT ISNULL(st.ItemId,0) AS ItemId,			
    SUM(st.RcptQty) - SUM(st.IssueQty) AS StockQty, 
    SUM(st.RcptNos) - SUM(st.IssueNos) AS StockPcs    
	FROM dbo.StockTrans st
	LEFT OUTER JOIN dbo.Voucher vch ON vch.Id = st.VoucherId
	LEFT OUTER JOIN dbo.Product pd ON pd.Id = st.ItemId
	WHERE (pd.PTypeId = @PTypeId OR @ptype = 'N') AND st.CompanyId = @CompanyId -- AND st.YearId = 1 
		AND (st.BranchId = @BranchId OR  @BranchId = 0) AND (st.DivId = @DivId OR @DivId = 0)
		AND  st.VoucherDate < @FromDate AND st.IsActive = 1 AND st.IsDeleted = 00	GROUP BY st.ItemId
	) z ON z.ItemId = p.Id
	WHERE (p.PTypeId = @PTypeId OR @ptype = 'N') AND p.IsDeleted = 0
	ORDER BY p.ProductName
END
GO

