IF object_id('[dbo].[ufnGetNumeric]') IS NULL 
EXEC ('CREATE FUNCTION [dbo].[ufnGetNumeric]() RETURNS BIGINT AS BEGIN RETURN 1 END') 
GO 

ALTER FUNCTION dbo.ufnGetNumeric
(
	@strAlphaNumeric VARCHAR(256)
)
RETURNS BIGINT
AS
BEGIN
	DECLARE @intAlpha BIGINT
	SET @intAlpha = PATINDEX('%[^0-9]%', @strAlphaNumeric)
	BEGIN
		WHILE @intAlpha > 0
		BEGIN
		    SET @strAlphaNumeric = STUFF(@strAlphaNumeric, @intAlpha, 1, '')
		    SET @intAlpha = PATINDEX('%[^0-9]%', @strAlphaNumeric)
		END
	END
	RETURN ISNULL(@strAlphaNumeric, 0)
END
GO
