CREATE  PROCEDURE [dbo].[PermissionList]	
@ptype VARCHAR(50)
AS
BEGIN
     SELECT *
	 FROM dbo.Permissions p     
	WHERE NOT EXISTS (SELECT 1 FROM dbo.Role_Permissions WHERE PermissionId = p.Id) AND p.PermissionType = @ptype AND p.Id>1
END
GO

