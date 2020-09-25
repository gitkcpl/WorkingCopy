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

--IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=56)
--INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category)
--Values(56,'Ask for Issue in Job receipt purchase','N','Y for Yes N for No','Outward')

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

IF NOT EXISTS(SELECT 1 FROM dbo.SysPara WHERE Id=140)
INSERT INTO dbo.SysPara(Id,Descr,DefaultValue,ValueDescr,Category,FreezePass)
Values(140,null,null,null,null,'1')
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