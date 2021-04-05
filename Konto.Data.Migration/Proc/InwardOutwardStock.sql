IF object_id('[dbo].[InwardOutwardStock]') IS NULL 
EXEC ('CREATE PROC [dbo].[InwardOutwardStock] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[InwardOutwardStock]
    @fromdate INT,
    @todate INT,
    @companyid INT = 1,
	@yearid INT = 1,
    @reportid INT = 0,
    @party VARCHAR(1) = 'N',
    @item VARCHAR(1) = 'N',
	@color VARCHAR(1) = 'N',
	@itemtype INT = 0
  	
AS
BEGIN

SELECT	pd.ProductName AS Product, 
        ISNULL(c.ColorName,cl.ColorName) ColorName,
		v.VoucherName, 
		ch.VoucherNo, 
		ISNULL(CONVERT(DATE, CONVERT(VARCHAR(8), st.VoucherDate), 112), '') TransDate ,
		ch.ChallanNo,
		ISNULL(ac.AccName,'Production') AS Particular, 
		sum(st.RcptNos) AS InwPcs, 
		sum(st.RcptQty) AS InwQty, 
		sum(st.IssueNos) AS OutPcs,
		sum(st.IssueQty) AS OutQty,
		sum(st.Qty) as Qty,
		sum(ISNULL(st.Pcs,0)) AS Pcs
FROM dbo.StockTrans st
LEFT OUTER JOIN dbo.Product pd ON pd.Id = st.ItemId
LEFT OUTER JOIN dbo.ProdOut po ON po.RowId = st.RefId
LEFT OUTER JOIN dbo.Color c ON c.Id = po.ColorId
LEFT OUTER JOIN dbo.Color cl ON cl.Id = st.ColorId
LEFT OUTER JOIN dbo.Voucher v ON v.Id = st.VoucherId
LEFT OUTER JOIN dbo.Challan ch ON ch.Id = st.KeyFldValue
LEFT OUTER JOIN dbo.Acc ac ON ac.Id = st.AccountId

WHERE st.IsActive = 1 AND st.IsDeleted = 0 AND st.VoucherDate BETWEEN @fromdate AND @todate AND (@itemtype =0 OR pd.PTypeId = @itemtype)
      AND st.CompanyId = @companyid
	 AND 
      (
          @item = 'N'
          OR EXIsts
		  (
    SELECT 1
    FROM dbo.ReportPara rs
    WHERE rs.ReportId = @reportid
          AND st.ItemId = rs.ParameterValue
          AND rs.ParameterName = 'product'
)
      
      )
	  	 AND 
      (
          @party = 'N'
          OR EXIsts
		  (
    SELECT 1
    FROM dbo.ReportPara rs
    WHERE rs.ReportId = @reportid
          AND st.AccountId = rs.ParameterValue
          AND rs.ParameterName = 'party'
)
      
      )
	  	  	  AND 
      (
          @color = 'N'
          OR EXIsts
		  (
    SELECT 1
    FROM dbo.ReportPara rs
    WHERE rs.ReportId = @reportid
          AND st.ColorId = rs.ParameterValue
          AND rs.ParameterName = 'color'
)
      
      )
	  group by pd.ProductName,c.ColorName,cl.ColorName,v.VoucherName,ch.VoucherNo, st.VoucherDate, ch.ChallanNo,ac.AccName
	  ORDER BY st.VoucherDate
END
GO

