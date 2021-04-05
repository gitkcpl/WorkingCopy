IF object_id('[dbo].[ProductOpBalSp]') IS NULL 
EXEC ('CREATE PROC [dbo].[ProductOpBalSp] AS SELECT 1 AS Id') 
GO

ALTER PROC [dbo].[ProductOpBalSp]
@companyid INT=1,
@BranchId INT = 1
AS
SELECT  pb.Id as BalId,p.Id,p.ProductName,p.UomId,p.GroupId,p.PTypeId
		,pb.OpQty,pb.OpNos,pb.StockValue,
		u.UnitName,pg.GroupName,pt.TypeName,ps.SizeName
FROM    dbo.Product p
        LEFT OUTER JOIN dbo.ProductBal pb ON pb.ProductId= p.Id
        LEFT OUTER JOIN dbo.PGroup pg ON pg.Id = p.GroupId
        LEFT OUTER JOIN dbo.Uom u ON u.Id= p.UomId
        LEFT OUTER JOIN dbo.ProductType pt ON pt.Id = p.PTypeId
        LEFT OUTER JOIN dbo.PSize ps on ps.Id = p.SizeId
        WHERE (p.ItemType IN ('I'))
        AND pb.CompanyId=@companyid and p.IsActive=1 and p.IsDeleted=0 AND pb.BranchId = @BranchId		
        ORDER BY p.ProductName
GO

