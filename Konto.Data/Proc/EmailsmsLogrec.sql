IF object_id('[dbo].[EmailsmsLogrec]') IS NULL 
EXEC ('CREATE PROC [dbo].[EmailsmsLogrec] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[EmailsmsLogrec]
  	@CompanyId  INT,
	@VoucherTypeID INT = 0
 
AS
BEGIN
    SELECT vs.SmsAfterSave , vs.EmailAfterSave , vt.SmsToParty AS ToParty,vt.SmsToAgent AS ToAgent, 
vt.SmsToUser AS ToUser,vt.OtherMobile AS ToOther, vt.EmailSub AS EmailSub, vt.EmailBody AS EmailBody, 
'None' AS EmailHead, 'None' AS EmailFoot, vt.SmsTemplates AS Sms,vt.EmailToParty,vt.EmailToAgent,
vt.EmailToUser
FROM  dbo.Voucher vc 
LEFT OUTER JOIN dbo.VoucherType vt ON vt.Id = vc.VTypeId
LEFT OUTER JOIN dbo.VchSetup vs ON vs.VoucherId = vc.Id
WHERE vt.Id = @VoucherTypeID AND vs.CompId = @CompanyId
	
END
GO

