SET IDENTITY_INSERT dbo.Voucher ON

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=1)
begin
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Opening Balance','OPB',1,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),1) 
 

 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(1,1,'OPB',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
end

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=2)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Indent','IND',2,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),2)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(2,1,'IND',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
end

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=3)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Purchase Order Voucher','PO',3,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),3)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(3,1,'PURCHASE ORDER',0,0,1,1,'{#}/{YY}',0,0,1,1,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
end


IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=4)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Sales Order Voucher','SO',4,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),4)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(4,1,'IND',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
end


IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=5)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Inward Voucher','IND',5,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),5)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(5,1,'IND',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
end


IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=6)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Sales Challan','SC',6,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),6)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(6,1,'DESPATCH CHALLAN',0,0,1,1,'{#}/{YY}',0,0,1,1,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())

end

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=7)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Mill Receipt Voucher','MR',7,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),7)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(7,1,'MR',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
end


IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=8)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Job Receipt','JR',8,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),8)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(8,1,'JR',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
end


IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=9)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Store Issue','STI',9,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),9)
 
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(9,1,'Store Issue',0,0,1,1,'{#}/{YY}',0,0,1,1,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
end



IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=10)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Design Mapping Voucher','DM',10,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),10)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(10,1,'DM',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
end


IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=11)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Beam Production Voucher','BP',11,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),11)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(11,1,'BP',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
end

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=12)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Sales Invoice','SINV',12,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),12)
  
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	     Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(12,1,'Tax Invoice',0,0,1,1,'{#}/{YY}',0,0,1,1,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
 
end

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=13)
BEGIN

INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Purchase Invoice','PINV',13,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),13)
 
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(13,1,'Tax Invoice',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
 
end

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=14)
BEGIN

INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Journal Voucher','JV',14,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),14)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	     Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(14,1,'Journal',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())

end

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=15)
BEGIN

INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Receipt Voucher','RCPT',15,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),15)
 

 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	     Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(15,1,'Receipt Voucher',0,0,1,1,'{#}/{YY}',0,1,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())

end

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=16)
BEGIN 
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Payment Voucher','PMT',16,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),16)


 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(16,1,'Payment Voucher',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())

end
 

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=17)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Taka Production Voucher','TP',17,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),17)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(17,1,'TP',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
 
end
IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=18)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Purchase Return Voucher','PRT',18,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),18)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(18,1,'DEBIT NOTE',0,0,1,1,'{#}/{YY}',0,0,1,1,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
 
end
IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=19)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Sales Return Voucher','SRT',19,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),19)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(19,1,'CREDIT NOTE',0,0,1,1,'{#}/{YY}',0,0,1,1,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
 
end

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=20)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Batch','BM',20,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),20)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(20,1,'BM',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
 
end

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=21)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Yarn Production','YP',21,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),21)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(21,1,'YP',0,0,1,1,'{#}/{YY}',0,0,1,1,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
 
end


IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=22)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Taka Folding','CF',22,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),22)

 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(22,1,'Folding',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())

end

update Voucher set VoucherName = 'Taka Folding' where Id =22;

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=23)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('General Expense','EXP',23,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),23)

 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(23,1,'EXP',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID()) 
end

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=24)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Debit/Credit Note','DRCR',24,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),24)

 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(24,1,'DRCR',0,0,1,1,'{#}/{YY}',0,0,1,1,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
 
 end

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=26)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Opening Bill Voucher','OPB',26,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),26)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(26,1,'OpB',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
 end 
 
 IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=27)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Color Recipe Voucher','CRV',27,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),27)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(27,1,'CRV',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
 end 
 UPDATE voucher SET VTypeId = 27 WHERE id = 27

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=28)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Chemical Formula Voucher','CFV',28,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),28)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(28,1,'CFV',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
end 

UPDATE voucher SET VTypeId = 28 WHERE id = 28

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=29)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Taka Opening Voucher','TOV',29,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),29)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(29,1,'TOV',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
 end 
  IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=30)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Job Process Invoice','JPI',30,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),30)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(30,1,'JPI',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
 end 

--IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=31)
--BEGIN
--INSERT INTO dbo.Voucher
--(
--    VoucherName,SortName,VTypeId,RefVoucherId,
--    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
--) Values('Mill Process Invoice','MPI',31,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),31)
 
-- INSERT INTO dbo.VchSetup
--	(
--	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
--	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
--	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
--	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
--	)
--VALUES(31,1,'TOV',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
-- end 

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=32)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Gray Purchase','GP',32,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),32)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(32,1,'GP',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
 
end

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=33)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Job Card','JC',33,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),33)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(33,1,'JC',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
 
end

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=34)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('BOM','BOMV',34,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),34)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(34,1,'BOMV',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
 
end


IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=35)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Store  Return','SIRV',35,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),35)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(35,1,'SIRV',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
 
end

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=36)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Gray Purchase Invoice','GPIV',36,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),36)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(36,1,'GPIV',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
 
end


IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=37)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Mill Issue Voucher','MI',37,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),37)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(37,1,'MI',0,0,1,1,'{#}/{YY}',0,0,1,1,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
 
end

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=38)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Job Issue Voucher','JI',38,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),38)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(38,1,'MI',0,0,1,1,'{#}/{YY}',0,0,1,1,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
 
end


IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=39)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Grey Order Voucher','GO',39,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),39)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(39,1,'GO',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID()) 
end

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=40)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Mill receipt Voucher','MRV',40,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),40)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(40,1,'MRV',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
end

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=41)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Job receipt Voucher','JRV',41,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),41)
 
 INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(41,1,'JRV',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
end


IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=42)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Taka Conv Voucher','TCV',42,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),42)

INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(42,1,'TCV',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
END

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=43)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Taka Cutting Voucher','TCut',43,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),43)

INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(43,1,'TCut',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
END

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=44)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Opening Mill Issue','OPMI',44,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),44)

INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(44,1,'OPMI',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
END

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=45)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Opening Job Issue','OPJI',45,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),45)

INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(45,1,'OPJI',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
END

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=46)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Outward Job Challan','OJC',46,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),46)

INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(46,1,'OJC',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
END

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=47)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Job Against Po','JPO',47,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),47)

INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(47,1,'JPO',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
END

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=48)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Gen Exp Ret','GER',48,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),48)

INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(48,1,'GER',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
END

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=49)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Mill Return','MR',49,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),49)

INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(49,1,'MR',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
END

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=50)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Brokerage Voucher','BV',50,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),50)

INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(50,1,'BV',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
END

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=51)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Taka Wise Job Receipt','TJC',51,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),51)

INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(51,1,'Taka Job Receipt',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
END

IF NOT EXISTS(SELECT 1 FROM dbo.Voucher WHERE Id=52)
BEGIN
INSERT INTO dbo.Voucher
(
    VoucherName,SortName,VTypeId,RefVoucherId,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Id
) Values('Stock Journal','SJ',52,0,1,0,SYSDATETIME(),'Admin','NA',NEWID(),52)

INSERT INTO dbo.VchSetup
	(
	    VoucherId,CompId,InvoiceHeading,VchWidth,PreFillZero,StartFrom,
	    Increment,Serial_Mask,Max_Value,Last_Serial,FyReset,
	    PrintAfterSave,EmailAfterSave,SmsAfterSave,BookFix,AccId,
	    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
	)
VALUES(52,1,'Stock Journal',0,0,1,1,'{#}/{YY}',0,0,1,0,0,0,0,0,1,0,SYSDATETIME(),'Admin','NA',NEWID())
END

SET IDENTITY_INSERT dbo.Voucher OFF