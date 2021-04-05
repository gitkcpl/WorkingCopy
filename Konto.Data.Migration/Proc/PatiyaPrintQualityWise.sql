IF object_id('[dbo].[PatiyaPrintQualityWise]') IS NULL 
EXEC ('CREATE PROC [dbo].[PatiyaPrintQualityWise] AS SELECT 1 AS Id') 
GO


ALTER PROCEDURE [dbo].[PatiyaPrintQualityWise]
 	@EmpId int,
	@FromDate int,
	@ToDate int

AS
BEGIN

SELECT c.PrintName as PrintCompany,e.EmpName,p.ProductName, pe.TotalMtrs, pe.ProdDate  FROM dbo.prod_emp pe
LEFT OUTER JOIN dbo.prod pd ON pd.Id = pe.ProdId 
LEFT OUTER JOIN dbo.Product p ON p.Id = pd.ProductId
LEFT OUTER JOIN dbo.Emp e ON e.Id = pe.EmpId
LEFT OUTER JOIN dbo.Company c ON c.Id = pd.CompId
WHERE pe.EmpId = @EmpId AND pe.ProdDate BETWEEN CONVERT(date,Convert(varchar(8),@FromDate),112) and CONVERT(date,Convert(varchar(8),@ToDate),112)
AND pe.TotalMtrs > 0
 
END
GO

