SET IDENTITY_INSERT dbo.VoucherType ON

IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=1)
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)

VALUES(1,'Opening Balance','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=2)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(2,'Indent','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(21,2,NEWID(),SYSDATETIME(),'Admin')


END



IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=3)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(3,'Purchase Order','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(21,3,NEWID(),SYSDATETIME(),'Admin')

end




--sales order
IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=4)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(4,'Sales Order','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(25,4,NEWID(),SYSDATETIME(),'Admin')

end



----Inward

IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=5)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(5,'Inward','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(21,5,NEWID(),SYSDATETIME(),'Admin')
end



--sales challan
IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=6)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(6,'Sales Challan','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(25,6,NEWID(),SYSDATETIME(),'Admin')


end



--Mill Receipts
IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=7)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(7,'Mill Receipt','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Book(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(29,7,NEWID(),SYSDATETIME(),'Admin')

INSERT INTO dbo.Voucher_Book(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(10,7,NEWID(),SYSDATETIME(),'Admin')

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(21,7,NEWID(),SYSDATETIME(),'Admin')


end


--Job Receipt
IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=8)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(8,'Job Receipt','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(21,8,NEWID(),SYSDATETIME(),'Admin')

end


--store issue
IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=9)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(9,'Store Issue','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

end

-- design mapping
IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=10)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(10,'Design Mapping','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

end

--beam production
IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=11)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(11,'Beam Production','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

end

--sales invoice

IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=12)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(12,'Sales Voucher','',0,'',0,0,0,'',0,'','',1,1,1,'','','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(25,12,NEWID(),SYSDATETIME(),'Admin')


INSERT INTO dbo.Voucher_Book(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(9,12,NEWID(),SYSDATETIME(),'Admin')
end




--Purchase Invoice
IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=13)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(13,'Purchase Voucher','',0,'',0,0,0,'',0,'','',1,1,1,'','','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(21,13,NEWID(),SYSDATETIME(),'Admin')

INSERT INTO dbo.Voucher_Book(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(10,13,NEWID(),SYSDATETIME(),'Admin')

end



--journal voucher

IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=14)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(14,'Journal Voucher','',0,'',0,0,0,'',0,'','',1,1,1,'','','',1,0,SYSDATETIME(),'Admin','NA',NEWID())
end

--receipt voucher

IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=15)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(15,'Receipt Voucher','',0,'',0,0,0,'',0,'','',1,1,1,'','','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Book(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(26,15,NEWID(),SYSDATETIME(),'Admin')

INSERT INTO dbo.Voucher_Book(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(27,15,NEWID(),SYSDATETIME(),'Admin')

end



-- payment voucher
IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=16)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(16,'Payment Voucher','',0,'',0,0,0,'',0,'','',1,1,1,'','','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Book(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(26,16,NEWID(),SYSDATETIME(),'Admin')

INSERT INTO dbo.Voucher_Book(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(27,16,NEWID(),SYSDATETIME(),'Admin')

end



--taka production

IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=17)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(17,'Taka Production','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())
end

IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=18)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(18,'Purchase Return','',0,'',0,0,0,'',
0,'','',1,1,1,'','','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(21,18,NEWID(),SYSDATETIME(),'Admin')

INSERT INTO dbo.Voucher_Book(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(10,18,NEWID(),SYSDATETIME(),'Admin')

end


--sales return
IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=19)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(19,'Sales Return','',0,'',0,0,0,'',0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(25,19,NEWID(),SYSDATETIME(),'Admin')

INSERT INTO dbo.Voucher_Book(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(9,19,NEWID(),SYSDATETIME(),'Admin')

end




IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=20)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(20,'Batch Voucher','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

end

IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=21)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(21,'Yarn Production','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

end

IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=22)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(22,'Taka Folding ','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())


INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(21,22,NEWID(),SYSDATETIME(),'Admin')
end


update VoucherType set TypeName = 'Taka Folding ' where id = 21;


IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=23)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(23,'General Expense ','',0,'',0,0,0,'',0,'','',1,1,1,'','','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

end

IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=24)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(24,'Debit/Credit Note ','',0,'',0,0,0,'',0,'','',1,1,1,'','','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

end

IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=26)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(26,'Sale/Purchase Opening Bill','',0,'',0,0,0,'',0,'','',1,1,1,'','','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(21,26,NEWID(),SYSDATETIME(),'Admin')


INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(25,26,NEWID(),SYSDATETIME(),'Admin')
end




IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=27)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(27,'Color Recipe','',0,'',0,0,0,'',0,'','',1,1,1,'','','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

end
IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=28)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(28,'Chemical Formula','',0,'',0,0,0,'',0,'','',1,1,1,'','','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

end


IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=29)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(29,'Taka Opening','',0,'',0,0,0,'',0,'','',1,1,1,'','','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

end

IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=30)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(30,'Job  Process Voucher','',0,'',0,0,0,'',0,'','',1,1,1,'','','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(21,30,NEWID(),SYSDATETIME(),'Admin')

INSERT INTO dbo.Voucher_Book(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(29,30,NEWID(),SYSDATETIME(),'Admin')

end



--IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=31)
--BEGIN
--INSERT INTO dbo.VoucherType
--(
--    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
--    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
--    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
--)
--VALUES(31,'Mill Process Voucher','',0,'',0,0,0,'',0,'','',1,1,1,'','','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

--end

--INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
--VALUES(21,31,NEWID(),SYSDATETIME(),'Admin')


--INSERT INTO dbo.Voucher_Book(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
--VALUES(29,31,NEWID(),SYSDATETIME(),'Admin')

IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=32)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(32,'Gray Purchase challan','',0,'',0,0,0,'',0,'','',1,1,1,'','','',1,0,SYSDATETIME(),'Admin','NA',NEWID())


INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(21,32,NEWID(),SYSDATETIME(),'Admin')

INSERT INTO dbo.Voucher_Book(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(10,32,NEWID(),SYSDATETIME(),'Admin')

end


IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=33)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(33,'Job Card Voucher','',0,'',0,0,0,'',0,'','',1,1,1,'','','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

end

IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=34)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(34,'Bill Off Managememnt','',0,'',0,0,0,'',0,'','',1,1,1,'','','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

end

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(20,34,NEWID(),SYSDATETIME(),'Admin')

IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=35)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(35,'Store Issue Return','',0,'',0,0,0,'',0,'','',1,1,1,'','','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

end

IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=36)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(36,'Gray Purchase Invoice','',0,'',0,0,0,'',0,'','',1,1,1,'','','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(21,36,NEWID(),SYSDATETIME(),'Admin')

INSERT INTO dbo.Voucher_Book(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(10,36,NEWID(),SYSDATETIME(),'Admin')
end




IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=37)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(37,'Mill Issue','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(21,37,NEWID(),SYSDATETIME(),'Admin')

end




IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=38)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(38,'Job Issue','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())


INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(21,38,NEWID(),SYSDATETIME(),'Admin')
end


IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=39)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(39,'Grey Order','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())
end

IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=40)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(40,'Mill Receipt Voucher','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(21,40,NEWID(),SYSDATETIME(),'Admin')

INSERT INTO dbo.Voucher_Book(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(10,40,NEWID(),SYSDATETIME(),'Admin')

end



IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=41)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(41,'Job Receipt Voucher','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(21,41,NEWID(),SYSDATETIME(),'Admin')


INSERT INTO dbo.Voucher_Book(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(10,41,NEWID(),SYSDATETIME(),'Admin')

END



IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=42)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(42,'Taka Conversion','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

END

IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=43)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(43,'Taka Cutting','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

END

IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=44)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(44,'Opening Mill Issue','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(21,44,NEWID(),SYSDATETIME(),'Admin')
END



IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=45)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(45,'Opening Job Issue','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(21,45,NEWID(),SYSDATETIME(),'Admin')
END



IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=46)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(46,'Outward Job Challan','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(21,46,NEWID(),SYSDATETIME(),'Admin')

END 



IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=47)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(47,'Job Against Purchase Order','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(21,47,NEWID(),SYSDATETIME(),'Admin')


END



IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=48)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(48,'General Expense Return','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(21,48,NEWID(),SYSDATETIME(),'Admin')
END



IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=49)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(49,'Mill Return','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(21,49,NEWID(),SYSDATETIME(),'Admin')
END




IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=50)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(50,'Brokerage Voucher','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(21,50,NEWID(),SYSDATETIME(),'Admin')
 
END



IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=51)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(51,'Taka Wise Job Receipt','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())


INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(21,51,NEWID(),SYSDATETIME(),'Admin')
END


IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=52)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(52,'Stock Journal','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(21,52,NEWID(),SYSDATETIME(),'Admin')

END

-- Gate Inward

IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=53)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(53,'Gate Inward','',0,'',0,0,0,'',
0,'','',1,1,1,'',
'','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(21,53,NEWID(),SYSDATETIME(),'Admin')

END
Go


--POS Purchase Invoice
IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=55)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(55,'Pos Purchase Voucher','',0,'',0,0,0,'',0,'','',1,1,1,'','','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(21,55,NEWID(),SYSDATETIME(),'Admin')

INSERT INTO dbo.Voucher_Book(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(10,55,NEWID(),SYSDATETIME(),'Admin')

end
Go

-- Pos Invoice
IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=56)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(56,'Pos Sales Invoice','',0,'',0,0,0,'',0,'','',1,1,1,'','','',1,0,SYSDATETIME(),'Admin','NA',NEWID())

INSERT INTO dbo.Voucher_Party(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(25,56,NEWID(),SYSDATETIME(),'Admin')


INSERT INTO dbo.Voucher_Book(GroupId,VoucherTypeId,RowId,CreateDate,CreateUser)
VALUES(9,56,NEWID(),SYSDATETIME(),'Admin')
END
Go

-- Stock Transfer

IF NOT EXISTS(SELECT 1 FROM dbo.VoucherType WHERE Id=57)
BEGIN
INSERT INTO dbo.VoucherType
(
    Id,TypeName,Terms,SendSms,SmsTemplates,SmsToParty,SmsToAgent,SmsToUser,OtherMobile,
    SendMail,EmailSub,EmailBody,EmailToParty,EmailToAgent,EmailToUser,OtherEmail,
    Extra1,Extra2,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
VALUES(57,'Stock Transfer','',0,'',0,0,0,'',0,'','',1,1,1,'','','',1,0,SYSDATETIME(),'Admin','NA',NEWID())
END
Go


SET IDENTITY_INSERT dbo.VoucherType OFF