
SET IDENTITY_INSERT dbo.SysPara On

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=1)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue, ValueDescr,Category)
Values(1,'Dual Stock Required','N', 'Y for Yes And N for No','Product Master')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=2)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(2,'Product Search On Product Code','N', 'Y for Yes And N for No for search on Product Name','Product Master')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=3)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(3,'Product Popup Details Top List','ProductName|ProductCode','ProductName|ProductCode|BarCodeNo|','Product Master')
 
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=4)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue, ValueDescr,Category)
Values(4,'Product Popup Details Bottom','DealerPrice|SaleRate|Mrp|ClosingStock|HsnCode|Gst%',
'DealerPrice|SaleRate|Mrp|ClosingStock|HsnCode|Gst%|GroupName|CatName','Product Master')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=5)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue, ValueDescr,Category)
Values(5,'Product Group Required','N', 'Y for Yes And N for No.','Product Master')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=6)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue, ValueDescr,Category)
Values(6,'Product Sub Group Required','N', 'Y for Yes And N for No.','Product Master')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=7)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(7,'Product Category Required','N', 'Y for Yes And N for No.','Product Master')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=8)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(8,'Color Required','N','Y for Yes And N for No.','Product Master')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=9)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(9,'Size Required','N', 'Y for Yes And N for No.','Product Master')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=10)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(10,'Brand Required','N', 'Y for Yes And N for No.','Product Master')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=11)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(11,'Style Required','N', 'Y for Yes And N for No.','Product Master')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=12)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(12,'Vendor Required','N','Y for Yes And N for No.','Product Master')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=13)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(13,'Closing Stock A/c Required','N', 'Y for Yes And N for No.','Product Master')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=14)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(14,'Batch Required','N','Y for Yes And N for No.','Product Master')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=15)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(15,'Serial Required','Y','Y for Yes And N for No.','Product Master')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=16)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(16,'Decimal Point For Unit-1','2','0-4 vale for decimal','Product Master')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=17)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(17,'Decimal Point For Unit-2','0','0-4 vale for decimal','Product Master')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=18)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(18,'Decimal Point For Rate','2','0-4 vale for decimal','Product Master')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=58)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(58,'Weaving Detail','N','Y for Yes N for No','Product Master')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=141)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(141,'ColorSet Required','N','Y for Yes N for No','Product Master')

update dbo.SysPara set Descr = 'ColorSet Required' where id = 141
--PO
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=25)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(25,'Color Name Required','N','Y for Yes N for No','Purchase Order')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=26)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(26,'Design No. Required','N','Y for Yes N for No','Purchase Order')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=27)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(27,'Grade Required','N','Y for Yes N for No','Purchase Order')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=28)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(28,'Gray Details Required','N','Y for Yes N for No','Purchase Order')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=29)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(29,'In Side Checker Required','N','Y for Yes N for No','Purchase Order')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=30)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(30,'Out Side Checker Required','N','Y for Yes N for No','Purchase Order')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=31)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(31,'Default Order Status','PENDING','PENDING for PENDING APPROVED for APPROVE','Purchase Order')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=57)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(57,'Currency Required','N','Y for Yes N for No','Purchase Order')

--Inward GRN

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=32)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(32,'Color Required','Y','Y for Yes N for No','GRN')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=33)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(33,'Batch Required','N','Y for Yes N for No','GRN')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=34)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(34,'Lot No Required','Y','Y for Yes N for No','GRN')
 
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=35)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(35,'Design Required','N','Y for Yes N for No','GRN')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=36)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(36,'Grade Required','N','Y for Yes N for No','GRN')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=37)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(37,'Cut Required','N','Y for Yes N for No','GRN')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=38)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(38,'Freight Required','N','Y for Yes N for No','GRN')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=39)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(39,'Auto Book','0','Auto Book','GRN')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=40)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(40,'Auto Voucher','0','Auto Selected Voucher','GRN')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=41)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(41,'Taka Detail','Y','Y for Yes N for No','GRN')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=42)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(42,'Generate Auto Bill','N','Y for Yes N for No','GRN')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=54)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(54,'Direct Issue From purchase','N','Y for Yes N for No','GRN')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=55)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(55,'Direct Sales From purchase','N','Y for Yes N for No','GRN')

--IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=142)
--INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
--Values(142,'Color Set Applicable','N','Y for Yes N for No','GRN')
--Outward Sales Challan

--Outward

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=43)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(43,'Color Required','Y','Y for Yes N for No','Outward')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=44)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(44,'Batch Required','N','Y for Yes N for No','Outward')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=45)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(45,'Lot No Required','Y','Y for Yes N for No','Outward')
 
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=47)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(47,'Design Required','N','Y for Yes N for No','Outward')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=46)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(46,'Grade Required','N','Y for Yes N for No','Outward')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=48)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(48,'Cut Required','Y','Y for Yes N for No','Outward')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=49)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(49,'Freight Required','Y','Y for Yes N for No','Outward')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=50)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(50,'Auto Book','0','Auto Book','Outward')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=51)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(51,'Auto Voucher','0','Auto Selected Voucher','Outward')

--IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=52)
--INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
--Values(52,'Taka Detail','Y','Y for Yes N for No','Outward')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=53)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(53,'Generate Auto Bill','N','Y for Yes N for No','Outward')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=56)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(56,'Common Stock','N','Y for Yes N for No','Outward')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=71)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(71,'Disable TakaQty In Issue','Y','Y for Yes N for No','Outward')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=95)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(95,'Taka From Stock','Y','Y for Yes N for No','Outward')

--Mill Issue 

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=112)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(112,'Color Required','Y','Y for Yes N for No','MillIssue')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=113)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(113,'Batch Required','N','Y for Yes N for No','MillIssue')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=114)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(114,'Lot No Required','Y','Y for Yes N for No','MillIssue')
 
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=115)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(115,'Grade Required','N','Y for Yes N for No','MillIssue')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=116)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(116,'Design Required','N','Y for Yes N for No','MillIssue')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=117)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(117,'Cut Required','N','Y for Yes N for No','MillIssue')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=118)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(118,'Freight Required','N','Y for Yes N for No','MillIssue')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=119)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(119,'Auto Book','0','Auto Book','MillIssue')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=120)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(120,'Auto Voucher','0','Auto Selected Voucher','MillIssue')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=121)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(121,'Taka From Stock','Y','Y for Yes N for No','MillIssue')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=146)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(146,'Pending Mill Issue Open on Load','N','Y for Yes N for No','MillIssue')

--Job Issue

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=122)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(122,'Color Required','Y','Y for Yes N for No','JobIssue')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=123)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(123,'Batch Required','N','Y for Yes N for No','JobIssue')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=124)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(124,'Lot No Required','Y','Y for Yes N for No','JobIssue')
 
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=125)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(125,'Grade Required','N','Y for Yes N for No','JobIssue')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=126)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(126,'Design Required','N','Y for Yes N for No','JobIssue')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=127)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(127,'Cut Required','N','Y for Yes N for No','JobIssue')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=128)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(128,'Freight Required','N','Y for Yes N for No','JobIssue')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=129)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(129,'Auto Book','0','Auto Book','JobIssue')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=130)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(130,'Auto Voucher','0','Auto Selected Voucher','JobIssue')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=135)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(135,'Finish Product Required','N','Y for Yes N for No','JobIssue')

update dbo.SysPara set Descr = 'Finish Product Required' where id = 135

--Sales Invoice

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=59)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(59,'Color Required','N','Y for Yes N for No','SaleInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=60)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(60,'Grade Required','N','Y for Yes N for No','SaleInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=61)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(61,'Design Required','N','Y for Yes N for No','SaleInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=62)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(62,'Cut Required','N','Y for Yes N for No','SaleInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=144)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(144,'LotNo Required','N','Y for Yes N for No','SaleInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=63)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(63,'Freight Required','N','Y for Yes N for No','SaleInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=64)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(64,'Tcs Required','N','Y for Yes N for No','SaleInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=136)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(136,'Default Print Set','BillPrintTextilesF1.rdlx','Name of Default print after save','SaleInvoice')

update SysPara set Category = 'SaleInvoice' where Id = 136

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=174)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(174,'Allow Duplicate Order for ecommerce','N','Y for Yes N for No','SaleInvoice')


--IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=140)
--INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category,FreezePass)
--Values(140,null,null,null,null,'1')
--Sales Return

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=165)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(165,'Color Required','N','Y for Yes N for No','SaleRet')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=166)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(166,'Grade Required','N','Y for Yes N for No','SaleRet')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=167)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(167,'Design Required','N','Y for Yes N for No','SaleRet')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=168)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(168,'Cut Required','N','Y for Yes N for No','SaleRet')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=169)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(169,'Freight Required','N','Y for Yes N for No','SaleRet')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=170)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(170,'Tcs Required','N','Y for Yes N for No','SaleRet')


--Purchase Invoice

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=65)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(65,'Color Required','N','Y for Yes N for No','PurchaseBill')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=66)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(66,'Grade Required','N','Y for Yes N for No','PurchaseBill')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=67)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(67,'Design Required','N','Y for Yes N for No','PurchaseBill')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=68)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(68,'Cut Required','N','Y for Yes N for No','PurchaseBill')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=69)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(69,'Freight Required','N','Y for Yes N for No','PurchaseBill')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=70)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(70,'Tcs Required','N','Y for Yes N for No','PurchaseBill')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=137) -- Purchase Bill
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(137,'Tds Round Off','N','Y for Yes N for No','PurchaseBill')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=138) -- Purchase Bill
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(138,'Tax Round Off','N','Y for Yes N for No','PurchaseBill')



--Purchase Return

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=157)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(157,'Color Required','N','Y for Yes N for No','PurchaseRet')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=158)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(158,'Grade Required','N','Y for Yes N for No','PurchaseRet')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=159)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(159,'Design Required','N','Y for Yes N for No','PurchaseRet')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=160)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(160,'Cut Required','N','Y for Yes N for No','PurchaseRet')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=161)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(161,'Freight Required','N','Y for Yes N for No','PurchaseRet')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=162)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(162,'Tcs Required','N','Y for Yes N for No','PurchaseRet')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=163) -- Purchase Return
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(163,'Tds Round Off','N','Y for Yes N for No','PurchaseRet')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=164) -- Purchase Return
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(164,'Tax Round Off','N','Y for Yes N for No','PurchaseRet')

--General Expense

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=147)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(147,'Freight Required','N','Y for Yes N for No','GenExp')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=148)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(148,'Tcs Required','N','Y for Yes N for No','GenExp')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=149)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(149,'Tds Required','N','Y for Yes N for No','GenExp')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=150) -- Purchase Bill
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(150,'Tds Round Off','N','Y for Yes N for No','GenExp')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=151) -- Purchase Bill
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(151,'Tax Round Off','N','Y for Yes N for No','GenExp')

--General Expense Return

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=152)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(152,'Freight Required','N','Y for Yes N for No','GenExpRet')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=153)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(153,'Tcs Required','N','Y for Yes N for No','GenExpRet')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=154)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(154,'Tds Required','N','Y for Yes N for No','GenExpRet')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=155) -- Purchase Bill
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(155,'Tds Round Off','N','Y for Yes N for No','GenExpRet')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=156) -- Purchase Bill
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(156,'Tax Round Off','N','Y for Yes N for No','GenExpRet')

--SO
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=72)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(72,'Color Name Required','N','Y for Yes N for No','Sales Order')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=73)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(73,'Design No. Required','N','Y for Yes N for No','Sales Order')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=74)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(74,'Grade Required','N','Y for Yes N for No','Sales Order')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=75)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(75,'Grade Required','N','Y for Yes N for No','Sales Order')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=76)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(76,'Gray Details Required','N','Y for Yes N for No','Sales Order')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=77)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(77,'In Side Checker Required','N','Y for Yes N for No','Sales Order')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=78)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(78,'Out Side Checker Required','N','Y for Yes N for No','Sales Order')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=79)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(79,'Default Order Status','PENDING','PENDING for PENDING APPROVED for APPROVE','Sales Order')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=80)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(80,'Min Odr Qty',5,'Minimun Qty for Order','Sales Order')
 
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=81)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(81,'Generate SO to PO','N','Y for Yes N for No','Sales Order')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=82)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(82,'Currency Required','N','Y for Yes N for No','Sales Order')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=83)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(83,'Cut Required','N','Y for Yes N for No','Sales Order')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=84)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(84,'Pcs Required','N','Y for Yes N for No','Sales Order')

--IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=171)
--INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
--Values(171,'Color Set Applicable','N','Y for Yes N for No','Sales Order')

-- Mill Receipt Challan/Voucher
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=85) -- Mill Receipt Voucher
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(85,'Tds Round Off','Y','Y for Yes N for No','Mrv')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=86)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(86,'Color Required','N','Y for Yes N for No','Mrv')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=87)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(87,'Design Required','N','Y for Yes N for No','Mrv')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=88)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(88,'Freight Required','N','Y for Yes N for No','Mrv')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=89)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(89,'Other Add Required','N','Y for Yes N for No','Mrv')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=90)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(90,'Other Less Required','N','Y for Yes N for No','Mrv')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=91)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(91,'Taka Details Required','N','Y for Yes N for No','Mrv')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=111)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(111,'FinishMeter more than GrayMeter','N','Y for Yes N for No','Mrv')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=171)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(171,'Challan Required','N','Y for Yes N for No','Mrv')

--Production
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=92)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(92,'Decimal Point Weight','3','0-4 vale for decimal','TakaProd')

update SysPara set Category = 'TakaProd' where id = 92

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=139)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(139,'Taka Production With Beam','Y','Y for Yes N for No','TakaProd')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=93)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(93,'Auto Yarn Consumption','Y','Y for Yes N for No','BeamProd')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=94)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(94,'RS in Inch','Y','Y for Yes N for No','Product Master')

--Job Receipt 
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=96) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(96,'Tds Round Off','Y','Y for Yes N for No','JobRec')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=97)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(97,'Color Required','N','Y for Yes N for No','JobRec')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=98)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(98,'Design Required','N','Y for Yes N for No','JobRec')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=99)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(99,'Freight Required','N','Y for Yes N for No','JobRec')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=100)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(100,'Other Add Required','N','Y for Yes N for No','JobRec')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=101)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(101,'Other Less Required','N','Y for Yes N for No','JobRec')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=102)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(102,'Taka Details Required','Y','Y for Yes N for No','JobRec')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=103)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(103,'ShortPcs Required','N','Y for Yes N for No','JobRec')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=104)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(104,'ShortMtr Required','N','Y for Yes N for No','JobRec')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=105)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(105,'Cut Required','N','Y for Yes N for No','JobRec')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=106)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(106,'Job Receipt Against Po','N','Y for Yes N for No','JobRec')

--IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=145)
--INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
--Values(145,'Color Set Applicable','N','Y for Yes N for No','JobRec')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=107)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(107,'Calculate Fold On ','METER','RATE for RATE, METER for METER','GrayPurchase')

---- Payment
--IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=137)
--INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
--Values(137,'Auto Credit Note','N','Y for Yes N for No','Pay')

---- Receipt
--IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=138)
--INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
--Values(138,'Auto Debit Note','N','Y for Yes N for No','Rec')



IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=173)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(173,'Challan Required','N','Y for Yes N for No','JobRec')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=175)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(175,'Generate Barcode For Taka/Box','N','Y for Yes N for No','GRN')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=176)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(176,'Generate Barcode For Taka','N','Y for Yes N for No','Mrv')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=177)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(177,'Issued By Barcode','N','Y for Yes N for No','JobIssue')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=178)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(178,'Ask For Voucher Selection','N','Y for Yes N for No','SaleInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=179)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(179,'Ask For Voucher Selection','N','Y for Yes N for No','PurchaseBill')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=180)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(180,'Ask For Voucher Selection','N','Y for Yes N for No','SaleRet')

update dbo.syspara set category = 'SaleRet' where id=180

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=181)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(181,'Ask For Voucher Selection','N','Y for Yes N for No','PurchaseRet')

--Taka Wise Job Receipt

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=182)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(182,'Color Required','Y','Y for Yes N for No','TakaJR')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=183)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(183,'Batch Required','N','Y for Yes N for No','TakaJR')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=184)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(184,'Lot No Required','Y','Y for Yes N for No','TakaJR')
 
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=185)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(185,'Grade Required','N','Y for Yes N for No','TakaJR')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=186)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(186,'Design Required','N','Y for Yes N for No','TakaJR')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=187)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(187,'Cut Required','N','Y for Yes N for No','TakaJR')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=188)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(188,'Freight Required','N','Y for Yes N for No','TakaJR')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=189)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(189,'OtherAdd Required','N','Y for Yes N for No','TakaJR')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=190)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(190,'OtherLess Required','N','Y for Yes N for No','TakaJR')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=191)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(191,'Challan Required','N','Y for Yes N for No','TakaJR')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=192)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(192,'TDS RoundOff','N','Y for Yes N for No','TakaJR')
 
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=193)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(193,'Taka Detail Reqired','Y','Y for Yes N for No','TakaJR')


--Store Return
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=194)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(194,'Color Required','Y','Y for Yes N for No','StoreReturn')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=195)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(195,'Batch Required','N','Y for Yes N for No','StoreReturn')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=196)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(196,'Lot No Required','Y','Y for Yes N for No','StoreReturn')
 
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=197)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(197,'Design Required','N','Y for Yes N for No','StoreReturn')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=198)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(198,'Grade Required','N','Y for Yes N for No','StoreReturn')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=199)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(199,'Cut Required','N','Y for Yes N for No','StoreReturn')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=200)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(200,'Freight Required','N','Y for Yes N for No','StoreReturn')
 
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=201)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(201,'Taka Detail','Y','Y for Yes N for No','StoreReturn')
 
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=202)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(202,'Other Add Required','N','Y for Yes N for No','StoreReturn')
 
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=203)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(203,'Other Less Detail','Y','Y for Yes N for No','StoreReturn')
 
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=204)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(204,'Cess Detail','Y','Y for Yes N for No','StoreReturn')
 
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=205)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(205,'Generate Barcode For Taka/Box','N','Y for Yes N for No','StoreReturn')



IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=206)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(206,'Job Issue Against Order','N','Y for Yes N for No','JobIssue')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=207)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(207,'Store Issue Against Order','N','Y for Yes N for No','StoreIssue')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=208)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(208,'Issue By Barcode','N','Y for Yes N for No','StoreIssue')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=209)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(209,'Taka From Stock','N','Y for Yes N for No','StoreIssue')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=210) -- Purchase Bill
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(210,'No of Decimal For Rate','2','Enter Range 2 to 4','PurchaseBill')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=211) -- Purchase Bill
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(211,'No of Decimal For Qty','2','Enter Range 2 to 3','PurchaseBill')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=212) -- Expense Bill
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(212,'No of Decimal For Rate','2','Enter Range 2 to 4','GenExp')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=213) -- Expense Bill
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(213,'No of Decimal For Qty','2','Enter Range 2 to 3','GenExp')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=214) -- Expense Bill
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(214,'No of Decimal For Rate','2','Enter Range 2 to 4','PurchaseRet')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=215) -- Expense Bill
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(215,'No of Decimal For Qty','2','Enter Range 2 to 3','PurchaseRet')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=216) -- SaleInvoice
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(216,'No of Decimal For Rate','2','Enter Range 2 to 4','SaleInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=217) -- SaleInvoice
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(217,'No of Decimal For Qty','2','Enter Range 2 to 3','SaleInvoice')

---SaleRet
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=218) -- SaleInvoice
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(218,'No of Decimal For Rate','2','Enter Range 2 to 4','SaleRet')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=219) -- SaleInvoice
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(219,'No of Decimal For Qty','2','Enter Range 2 to 3','SaleRet')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=220)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(220,'Editable Grey Meters','N','Y for Yes N for No','MillIssue')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=221)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(221,'Auto Bill Adjust Receipt','N','Y for Yes N for No','Receipt')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=222)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(222,'Auto Bill Adjust Payment','N','Y for Yes N for No','Payment')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=223)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(223,'Generate Barcode','N','Y for Yes N for No','JobRec')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=224) -- general expense
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(224,'Tds On Item Line Level','N','Y for Yes N for No','GenExp')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=225) -- invoice
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(225,'Tcs Round Off','Y','Y for Yes N for No','SaleInvoice')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=226) -- store issue
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(226,'Editable Details Qty','Y','Y for Yes N for No','StoreIssue')

 
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=227)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue, ValueDescr,Category)
Values(227,'Cost Rate Include Gst','N', 'Y for Yes And N for No.','Product Master')




-- purchase Request/Indent
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=228)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(228,'Color Name Required','N','Y for Yes N for No','Purchase Indent')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=229)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(229,'Design No. Required','N','Y for Yes N for No','Purchase Indent')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=230)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(230,'Grade Required','N','Y for Yes N for No','Purchase Indent')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=231)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(231,'Default Request Status','PENDING','PENDING for PENDING APPROVED for APPROVE','Purchase Indent')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=232) -- purchase invoice
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(232,'Tcs Round Off','Y','Y for Yes N for No','PurchaseBill')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=233) -- General Expense
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(233,'Tcs Round Off','Y','Y for Yes N for No','GenExp')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=234) -- Gray Purchase
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(234,'Tcs Required ?','N','Y for Yes N for No','GrayPurchase')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=235) -- Gray Purchase
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(235,'Tcs Round Off ?','N','Y for Yes N for No','GrayPurchase')



IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=236) -- SaleInvoice
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(236,'Order Required For Invoice ?','N','Y for Yes N for No','SaleInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=237) -- purchase invoice
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(237,'Order Required For Invoice ?','N','Y for Yes N for No','PurchaseBill')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=238) -- purchase invoice
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(238,'Hsn Code Required ?','N','Y for Yes N for No','PurchaseBill')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=239) -- SaleInvoice
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(239,'Hsn Code Required ?','N','Y for Yes N for No','SaleInvoice')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=240) -- Expense Bill
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(240,'Hsn Code Required ?','N','Y for Yes N for No','PurchaseRet')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=241) -- Expense Bill
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(241,'Hsn Code Required ?','N','Y for Yes N for No','SaleRet')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=242)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(242,'Agent Required','N','Y for Yes N for No','Purchase Order')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=243)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(243,'Transport Required','N','Y for Yes N for No','Purchase Order')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=244)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(244,'Division Required','N','Y for Yes N for No','Purchase Order')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=245)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(245,'Order Assigned By Required','N','Y for Yes N for No','Purchase Order')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=246)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(246,'Pay Terms Required','N','Y for Yes N for No','Purchase Order')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=247)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(247,'Party Group Required','N','Y for Yes N for No','Purchase Order')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=248) -- SaleInvoice
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(248,'Use Other Less as Rate Diff ?','N','Y for Yes N for No','SaleInvoice')




-- Point of sale paramenter

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=249)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(249,'Cut Required','N','Y for Yes N for No','PosInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=250)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(250,'Freight Required','N','Y for Yes N for No','PosInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=251)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(251,'Default Print Set','BillPrintTextilesF1.rdlx','Name of Default print after save','PosInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=252)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(252,'Other Add Required','N','Y for Yes N for No','PosInvoice')
 
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=253)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(253,'Other Less Detail','N','Y for Yes N for No','PosInvoice')
 
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=254)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(254,'Cess Detail','N','Y for Yes N for No','PosInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=255) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(255,'Tax Round Off','N','Y for Yes N for No','PosInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=256)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(256,'Ask For Voucher Selection','N','Y for Yes N for No','PosInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=257) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(257,'Order Required For Invoice ?','N','Y for Yes N for No','PosInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=259) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(259,'Hsn Code Required ?','N','Y for Yes N for No','PosInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=260)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(260,'Decimal Point For Rate','2','0-4 vale for decimal','PosInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=261)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(261,'Decimal Point For Qty','2','0-3 vale for decimal','PosInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=262) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(262,'Pcs Required ?','N','Y for Yes N for No','PosInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=263) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(263,'Merge Qty For Same Barcode ?','Y','Y for Yes N for No','PosInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=258) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(258,'Move To Next Barcode After Scanning?','Y','Y for Yes N for No','PosInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=264) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(264,'Rate Required?','N','Y for Yes N for No','PosInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=265) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(265,'Tax Required?','N','Y for Yes N for No','PosInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=266) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(266,'Tax Editable','N','Y for Yes N for No','PosInvoice')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=267) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(267,'Pos Sale Discount Account','0','Select Account','PosInvoice')


--BOM
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=268) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(268,'Stock effect','N','Y for Yes N for No','Bom')




-- Pos Purchase Return
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=269)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(269,'Cut Required','N','Y for Yes N for No','Pos_Pr')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=270)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(270,'Freight Required','N','Y for Yes N for No','Pos_Pr')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=271)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(271,'Other Add Required','N','Y for Yes N for No','Pos_Pr')
 
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=272)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(272,'Other Less Detail','N','Y for Yes N for No','Pos_Pr')
 
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=273)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(273,'Cess Detail','N','Y for Yes N for No','Pos_Pr')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=274) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(274,'Tax Round Off','N','Y for Yes N for No','Pos_Pr')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=275)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(275,'Ask For Voucher Selection','N','Y for Yes N for No','Pos_Pr')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=276) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(276,'Hsn Code Required ?','N','Y for Yes N for No','Pos_Pr')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=277)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(277,'Decimal Point For Rate','2','0-4 vale for decimal','Pos_Pr')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=278)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(278,'Decimal Point For Qty','2','0-3 vale for decimal','Pos_Pr')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=279)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(279,'Validate Barcode For Party','Y','Y for Yes N for No','Pos_Pr')

--IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=280)
--INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
--Values(280,'Load Bill Details','N','Y for Yes N for No','Pos_Pr')


-- POS RET PARA

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=280)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(280,'Cut Required','N','Y for Yes N for No','Pos_Sr')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=281)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(281,'Freight Required','N','Y for Yes N for No','Pos_Sr')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=282)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(282,'Other Add Required','N','Y for Yes N for No','Pos_Sr')
 
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=283)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(283,'Other Less Detail','N','Y for Yes N for No','Pos_Sr')
 
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=284)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(284,'Cess Detail','N','Y for Yes N for No','Pos_Sr')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=285) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(285,'Tax Round Off','N','Y for Yes N for No','Pos_Sr')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=286)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(286,'Ask For Voucher Selection','N','Y for Yes N for No','Pos_Sr')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=287) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(287,'Hsn Code Required ?','N','Y for Yes N for No','Pos_Sr')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=288)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(288,'Decimal Point For Rate','2','0-4 vale for decimal','Pos_Sr')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=289)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(289,'Decimal Point For Qty','2','0-3 vale for decimal','Pos_Sr')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=290) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(290,'Pcs Required ?','N','Y for Yes N for No','Pos_Sr')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=291) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(291,'Merge Qty For Same Barcode ?','Y','Y for Yes N for No','Pos_Sr')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=292) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(292,'Move To Next Barcode After Scanning?','Y','Y for Yes N for No','Pos_Sr')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=293) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(293,'Rate Required?','N','Y for Yes N for No','Pos_Sr')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=294) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(294,'Tax Required?','N','Y for Yes N for No','Pos_Sr')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=295) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(295,'Tax Editable','N','Y for Yes N for No','Pos_Sr')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=297) -- SaleInvoice
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(297,'Party Wise Challan','Y','Y for Yes N for No','SaleInvoice')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=298) -- SaleInvoice
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(298,'Barcode Required','N','Y for Yes N for No','SaleInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=299) -- Purchase Invoice
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(299,'Barcode Required','N','Y for Yes N for No','PurchaseBill')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=300) -- Sales Return
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(300,'Barcode Required','N','Y for Yes N for No','SaleRet')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=301) -- Purchase Return
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(301,'Barcode Required','N','Y for Yes N for No','PurchaseRet')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=302) -- Grn/Inward
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(302,'Lock If Move Next','Y','Y for Yes N for No','GRN')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=303)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(303,'Repeat Product','N','Y for Yes N for No','Outward')



IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=304)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(304,'Other Add Required','N','Y for Yes N for No','SaleInvoice')
 
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=305)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(305,'Other Less Detail','N','Y for Yes N for No','SaleInvoice')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=306)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(306,'Other Add Required','N','Y for Yes N for No','SaleRet')
 
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=307)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(307,'Other Less Detail','N','Y for Yes N for No','SaleRet')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=308)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(308,'Other Add Required','N','Y for Yes N for No','PurchaseBill')
 
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=309)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(309,'Other Less Detail','N','Y for Yes N for No','PurchaseBill')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=310)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(310,'Other Add Required','N','Y for Yes N for No','PurchaseRet')
 

 IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=312)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(312,'Freight Calculate On Qty','Y','Y for Yes N for No','SaleInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=313)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(313,'Default Freight Rate','0','Default Rate For Freight','SaleInvoice')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=314)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(314,'Sale Rate Required','N','Y for Yes N for No','SaleInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=315)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(315,'Cess Required','N','Y for Yes N for No','PurchaseBill')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=316)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(316,'Cess Requires','N','Y for Yes N for No','PurchaseRet')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=317)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(317,'Rate Editable','N','Y for Yes N for No','PosInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=318)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(318,'Clear Data After Save','Y','Y for Yes N for No','Product Master')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=319)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(319,'Barcode Required','N','Y for Yes N for No','JobIssue')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=320)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(320,'Rate Type Required','N','Y for Yes N for No','PosInvoice')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=321)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(321,'Gate Entry Required','N','Y for Yes N for No','GRN')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=322)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(322,'Accept Zero Value','N','Y for Yes N for No','Mrv')


-- for system level
IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=500) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(500,'Asp User Id','1602351118','User Id for Gst Api','sys')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=501) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(501,'Asp Password','taxpro*199','password for Gst Api','sys')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=502) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(502,'Gsp Name Testing','TaxPro_Sandbox','Gsp Name','sys')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=503) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(503,'Gsp Name Production','TaxPro_Production','Gsp Name','sys')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=504) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(504,'Api Base Url','https://api.taxprogsp.co.in/','Api Base Url','sys')


IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=505) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(505,'Common Order For All Company','N','Y For Yes/N For No','sys')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=506) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(506,'Common Stock For All Company','N','Y For Yes/N For No','sys')

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=507) 
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
Values(507,'Branch Wise Voucher Generation','Y','Y For Yes/N For No','sys')


--IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=505) 
--INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
--Values(505,'Multi Company Outstanding','N','Y for Yes,N For No','sys')


-- For Company Level
--IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=600) 
--INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
--Values(600,'Asp User Id','1602351118','User Id for Gst Api','sys')



SET IDENTITY_INSERT dbo.SysPara OFF