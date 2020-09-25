CREATE PROCEDURE [dbo].[GetColorForProductId]
	@id INT = 0
AS
BEGIN
    SELECT cs.ColorId,cs.ItemId, c.ColorName, p.ProductName
    FROM dbo.ColorSet cs 
    LEFT OUTER JOIN dbo.Color c ON c.Id = cs.ColorId
	LEFT OUTER JOIN dbo.Product p ON p.Id = cs.ItemId
    WHERE (cs.ItemId = @id OR 0 = @id )
          AND c.IsDeleted = 0;

END;
GO

