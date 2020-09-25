create PROCEDURE [dbo].[PACKINGPRINT]
@VoucherID INT = 0,
  @FrmBill varchar (50),
      @ToBill varchar(50)
AS
BEGIN
select p.id,p.VoucherNo,p.GrossWt,g.GradeName,p.TareWt,p.NetWt,p.TwistType,u.Unitcode,pd.ProductName,
           cm.PrintName PrintCompany,
           cm.Address1 CAddress,
           cm.Address2 CAddress2,
		cm.Pincode CPin,
           cm.Mobile CMobile,
           cm.Phone CPhone,
           cm.Email CEmail,
           cm.Website CWebsite,
           cm.GstIn CGstIn,
           cm.PanNo CPanNo,
           cm.AadharNo CAadharNo,
           cm.TdsAcNo CTdsAcNo,
           cm.AcNo CAcNo,
           cm.BankName CBankName,
           cm.IfsCode CIfsCode,
           cm.Remark CRemark,
		   C.CityName AS CCity,
		   S.StateName as CStateName,P.Remark,
 B.VoucherNo AS  BatchNO from prod  p
	left outer join Voucher v
		on v.Id = p.VoucherId 
	left outer join Grade g
		on g.Id = p.GradeId 
	left outer join Product pd
		on pd.id = p.ProductId
	left outer join Uom u
		on u.Id = pd.UomId
	left outer join Batch b
		on b.Id = p.BatchId
	left outer join Company cm
		on cm.id = p.CompId		
	LEFT OUTER JOIN City C
		ON C.Id=CM.CityId
	LEFT OUTER JOIN [State]  S
		ON S.Id = C.StateId
	where P.VoucherId =@VoucherID and P.VoucherNo between @FrmBill and @ToBill
	END