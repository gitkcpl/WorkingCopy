SET IDENTITY_INSERT dbo.ReportType ON
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=1)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(1,'BillPrintTextilesF1','BILL', 12,'','Repx\\BillPrintTextilesF1.repx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=2)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(2,'BillPrintTextilesF2','BILL', 12,'','Repx\\BillPrintTextilesF2.repx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=3)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(3,'SalesChallan','CHALLAN', 6,'','Repx\\SalesChallan.repx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=4)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(4,'TakaChallan','CHALLAN', 6,'','Repx\\TakaChallan.repx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=5)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(5,'YarnChallan','CHALLAN', 6,'','Repx\\YarnChallan.repx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=6)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(6,'BeamChallan','CHALLAN', 6,'','Repx\\BeamChallan.repx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=7)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(7,'PoRep','ORD', 3,'','Repx\\PoRep.repx',GETDATE(),'Admin',1,0,NEWID())

update dbo.ReportType set VoucherTypeId=3 where Id=7

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=8)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(8,'SoRep','ORD', 4,'','Repx\\SoRep.repx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=9)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(9,'PurRetPrintTextilesF1','PURRET', 18,'','Repx\\PurRetPrintTextilesF1.repx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=10)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(10,'SaleRetPrintTextilesF1','SALERET', 19,'','Repx\\SaleRetPrintTextilesF1.repx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=11)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(11,'NotePrintTextilesF1','DRCR', 24,'','Repx\\NotePrintTextilesF1.repx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=12)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(12,'Details','SALE', 12,'','Reg\\Details.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=13)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(13,'Summary','SALE', 12,'','Reg\\Summary.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=14)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(14,'Summary_monthly','SALE', 12,'','Reg\\Summary_monthly.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=15)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(15,'BillWiseDetails','SALE', 12,'','Reg\\BillWiseDetails.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=16)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(16,'Details','PURCHASE', 13,'','Reg\\Details.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=17)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(17,'Summary','PURCHASE', 13,'','Reg\\Summary.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=18)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(18,'Summary_monthly','PURCHASE', 13,'','Reg\\Summary_monthly.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=19)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(19,'BillWiseDetails','PURCHASE', 13,'','Reg\\BillWiseDetails.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=20)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(20,'Details','SRETURN', 19,'','Reg\\Details.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=21)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(21,'Summary','SRETURN', 19,'','Reg\\Summary.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=22)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(22,'Summary_monthly','SRETURN', 19,'','Reg\\Summary_monthly.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=23)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(23,'BillWiseDetails','SRETURN', 19,'','Reg\\BillWiseDetails.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=24)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(24,'Details','PRETURN', 18,'','Reg\\Details.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=25)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(25,'Summary','PRETURN', 18,'','Reg\\Summary.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=26)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(26,'Summary_monthly','PRETURN', 18,'','Reg\\Summary_monthly.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=27)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(27,'BillWiseDetails','PRETURN', 18,'','Reg\\BillWiseDetails.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=28)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(28,'Details','DEBIT', 24,'','Reg\\Details.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=29)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(29,'Summary','DEBIT', 24,'','Reg\\Summary.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=30)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(30,'Summary_monthly','DEBIT', 24,'','Reg\\Summary_monthly.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=31)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(31,'BillWiseDetails','DEBIT', 24,'','Reg\\BillWiseDetails.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=32)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(32,'Details','GEXPENSE', 23,'','Reg\\Details.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=33)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(33,'Summary','GEXPENSE', 23,'','Reg\\Summary.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=34)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(34,'Summary_monthly','GEXPENSE', 23,'','Reg\\Summary_monthly.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=35)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(35,'BillWiseDetails','GEXPENSE', 23,'','Reg\\BillWiseDetails.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=36)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(36,'Details','RCM', 23,'','Reg\\Details.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=37)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(37,'Summary','RCM', 23,'','Reg\\Summary.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=38)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(38,'Summary_monthly','RCM', 23,'','Reg\\Summary_monthly.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=39)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(39,'BillWiseDetails','RCM', 23,'','Reg\\BillWiseDetails.rdlx',GETDATE(),'Admin',1,0,NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=40)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(40,'ChallanNo Wise','SCHALLAN', 6,'','Reg\\Challan.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=41)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(41,'Details','SCHALLAN', 6,'','Reg\\Challan_Details.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=42)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(42,'Summary','SCHALLAN', 6,'','Reg\\Challan_Summary.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=43)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(43,'ChallanNo Wise','PCHALLAN', 5,'','Reg\\Challan.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=44)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(44,'Details','PCHALLAN', 5,'','Reg\\Challan_Details.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=45)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(45,'Summary','PCHALLAN', 5,' ','Reg\\Challan_Summary.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=46)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(46,'Taka Production Detail','TP', 17,'','Reg\\TakaProduction\\TakaProductionDetails.rdlx',GETDATE(),'Admin',1,0,NEWID())
 
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=47)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(47,'Taka Production Summary','TPS', 17,'','Reg\\TakaProduction\\TakaProductionSummary.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=48)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(48,'Receipt Outstanding Details Report','RECEIVABLE', 12,'','Outs\\outs_ar.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=49)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(49,'Receipt Outstanding Summary Report','RECEIVABLE', 12,'','Outs\\outs_summary_ar.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=50)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(50,'Receipt Outstanding Ageing Summary Report','RECEIVABLE', 12,'','Outs\\outs_summary_ageing_ar.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=51)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(51,'Receipt Outstanding Ageing Details Report','RECEIVABLE', 12,'','Outs\\outs_detail_ageing_ar.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=52)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(52,'Receipt Outstanding FIFO Ageing Details Report','RECEIVABLE', 12,'','Outs\\outs_detail_ageing_ar.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=53)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(53,'Payment Outstanding Details Report','PAYABLE', 13,'','Outs\\outs_ar.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=54)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(54,'Payment Outstanding Summary Report','PAYABLE', 13,'','Outs\\outs_summary_ar.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=55)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(55,'Payment Outstanding Ageing Summary Report','PAYABLE', 13,'','Outs\\outs_summary_ageing_ar.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=56)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(56,'Payment Outstanding Ageing Details Report','PAYABLE', 13,'','Outs\\outs_detail_ageing_ar.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=57)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,Remarks,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(57,'Payment Outstanding FIFO Ageing Details Report','PAYABLE', 13,'','Outs\\outs_detail_ageing_ar.rdlx',GETDATE(),'Admin',1,0,NEWID())


SET IDENTITY_INSERT dbo.ReportType OFF