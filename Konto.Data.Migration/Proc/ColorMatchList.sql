IF object_id('[dbo].[ColorMatchList]') IS NULL 
EXEC ('CREATE PROC [dbo].[ColorMatchList] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[ColorMatchList]
 	@CompanyId int
	
AS
BEGIN

SELECT   CONVERT(DATETIME2, CONVERT(VARCHAR(8), isnull(o.VoucherDate,20190401)), 112) Date,
	cl.ColorName,o.MColorId ColorId,o.TypeId Id , pd.ProductName ItemName, o.ItemId
	FROM  dbo.WeftItem o 
	LEFT OUTER JOIN dbo.Acc acc ON acc.Id = o.AccId 	
	LEFT OUTER JOIN dbo.Color cl ON cl.Id = o.MColorId
	LEFT OUTER JOIN dbo.Product pd ON pd.Id = o.ItemId
	WHERE o.IsActive =1 AND o.IsDeleted = 0 AND o.RefId IS NULL AND o.TypeId >1
	
	GROUP BY o.ItemId, pd.ProductName, o.MColorId, cl.ColorName, o.TypeId, o.VoucherDate 
	ORDER BY o.VoucherDate DESC			
	
END
GO

