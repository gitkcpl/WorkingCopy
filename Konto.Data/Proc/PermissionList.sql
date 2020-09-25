IF object_id('[dbo].[PermissionList]') IS NULL 
EXEC ('CREATE PROC [dbo].[PermissionList] AS SELECT 1 AS Id') 
GO

ALTER  PROCEDURE [dbo].[PermissionList]	
@ptype VARCHAR(50),
@roleid int
AS
BEGIN
     SELECT *
	 FROM dbo.Permissions p     
	WHERE NOT EXISTS (SELECT 1 FROM dbo.Role_Permissions WHERE PermissionId = p.Id and RoleId=@roleid) AND p.PermissionType = @ptype AND p.Id>1
END
GO

