create procedure [dbo].[DataFreeze]
@companyid int = 0
as 
begin 
SELECT     data_freeze.Id, data_freeze.FromDate, data_freeze.ToDate, data_freeze.VoucherTypeID, data_freeze.Freeze, data_freeze.Pass, 
                      vouchertype.TypeName AS Voucher, data_freeze.CompanyID
FROM         data_freeze INNER JOIN
                      vouchertype ON data_freeze.VoucherTypeID = vouchertype.id
WHERE     (data_freeze.CompanyID = @companyid)
end