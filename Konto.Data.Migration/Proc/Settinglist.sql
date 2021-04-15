IF object_id('[dbo].[Settingslist]') IS NULL 
EXEC ('CREATE PROC [dbo].[Settingslist] AS SELECT 1 AS Id') 
GO


ALTER PROCEDURE [dbo].[Settingslist]
 	@CompanyId int,
	@Category VARCHAR(50)
AS
BEGIN
SELECT sp.Category, sp.DefaultValue, sp.Descr,
ISNULL(cp.ParaValue,sp.DefaultValue) ParaValue,
cp.Remark, sp.ValueDescr, sp.Id ParaId, ISNULL(cp.Id,0) Id FROM dbo.SysPara sp
LEFT JOIN dbo.CompPara cp ON cp.ParaId = sp.Id
WHERE  sp.Category = @Category AND cp.CompId = @CompanyId
UNION ALL
SELECT sp.Category, sp.DefaultValue, sp.Descr,
sp.DefaultValue ParaValue,
NULL Remark, sp.ValueDescr, sp.Id ParaId, 0 Id FROM dbo.SysPara sp

WHERE  sp.Category = @Category AND NOT EXISTS (SELECT 1 FROM CompPara AS cp2
                                               WHERE cp2.ParaId = SP.Id
                                               AND CP2.CompId=@CompanyId)

END
GO

