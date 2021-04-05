IF object_id('[dbo].[DeliveryAddressList]') IS NULL 
EXEC ('CREATE PROC [dbo].[DeliveryAddressList] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[DeliveryAddressList]
 	@AccountId INT=0,
	@addressid INT=0

AS
BEGIN
	SELECT aca.Id,aca.AccId,ISNULL(aca.Address1,'NA') Address1 , ISNULL(aca.Address2,'NA') Address2,
	 ISNULL(aca.CityId,0) CityId,ct.CityName,aca.AreaId,ara.AreaName,
	isnull(aca.[Address1],'NA') + ' ' +isnull(aca.[Address2],'NA') + ' ' + isnull(ara.AreaName,'NA') + ' ' + isnull(ct.CityName,'NA') + ' Pin:' +
    isnull(aca.PinCode,'NA') + ' Mob:' + isnull(aca.MobileNo,'NA') + ' Email:' + isnull(aca.email,'NA') Address,
	aca.PinCode,aca.ContactPerson,aca.MobileNo,aca.MobileNo,aca.Phone, aca.Email, aca.Website,
	ct.StateId,st.StateName
	FROM dbo.AccAddress aca
	LEFT OUTER JOIN dbo.City ct ON ct.Id = aca.CityId
	LEFT OUTER JOIN dbo.Area ara ON ara.Id = aca.AreaId
	LEFT OUTER JOIN dbo.State st ON st.id = ct.StateId
	WHERE (@AccountId=0 or aca.AccId=@AccountId) 
	AND (@addressid=0 OR aca.Id=@addressid)
	AND aca.IsDeleted = 0 
	--and (o.VoucherDate between @FromDate and @ToDate)
END

GO
