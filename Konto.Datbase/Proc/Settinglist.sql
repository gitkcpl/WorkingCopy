CREATE PROCEDURE [dbo].[Settingslist]
 	@CompanyId int,
	@Category VARCHAR(50)
AS
BEGIN
SELECT sp.Category, sp.DefaultValue, sp.Descr,ISNULL(cp.ParaValue,sp.DefaultValue) ParaValue,cp.Remark, sp.ValueDescr, sp.Id ParaId, ISNULL(cp.Id,0) Id FROM dbo.SysPara sp
LEFT JOIN dbo.CompPara cp ON cp.ParaId = sp.Id
WHERE  sp.Category = @Category
	
END

GO

