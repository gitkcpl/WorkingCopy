Delete From dbo.ReportType

SET IDENTITY_INSERT dbo.ReportType ON
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=1)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(1,'BillPrintTextilesF1','BILL', 12,'reg\doc\BillPrintTextilesF1.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.BillPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=2)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(2,'BillPrintTextilesF2','BILL', 12,'reg\doc\BillPrintTextilesF2.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.BillPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=3)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(3,'SalesChallan','CHALLAN', 6,'reg\doc\SalesChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=4)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(4,'TakaChallan','CHALLAN', 6,'reg\doc\TakaSalesChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')
 
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=5)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(5,'YarnChallan','CHALLAN', 6,'reg\doc\YarnSalesChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=6)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(6,'BeamChallan','CHALLAN', 6,'reg\doc\BeamSalesChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=7)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(7,'PoRep','ORD', 3,'reg\doc\PoRep.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=8)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(8,'SoRep','ORD', 4,'reg\doc\SoRep.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=9)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(9,'PurRetPrintTextilesF1','PURRET', 18,'reg\doc\PurRetPrintTextilesF1.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.BillPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=10)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(10,'SaleRetPrintTextilesF1','SALERET', 19,'reg\doc\SaleRetPrintTextilesF1.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.BillPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=11)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(11,'NotePrintTextilesF1','DRCR', 24,'reg\doc\NotePrintTextilesF1.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.BillPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=12)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(12,'Details','SALE', 12,'Reg\\Details.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=13)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(13,'Summary','SALE', 12,'Reg\\Summary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=14)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(14,'Monthly Summary','SALE', 12,'Reg\\Summary_monthly.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=15)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(15,'BillNo Wise','SALE', 12,'Reg\\BillWise.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=16)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(16,'Details','PURCHASE', 13,'Reg\\Details.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=17)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(17,'Summary','PURCHASE', 13,'Reg\\Summary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=18)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(18,'Monthly Summary','PURCHASE', 13,'Reg\\Summary_monthly.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=19)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(19,'BillNo Wise','PURCHASE', 13,'Reg\\BillWise.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=20)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(20,'Details','SRETURN', 19,'Reg\\Details.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=21)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(21,'Summary','SRETURN', 19,'Reg\\Summary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=22)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(22,'Monthly Summary','SRETURN', 19,'Reg\\Summary_monthly.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=23)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(23,'BillNo Wise','SRETURN', 19,'Reg\\BillWise.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=24)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(24,'Details','PRETURN', 18,'Reg\\Details.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=25)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(25,'Summary','PRETURN', 18,'Reg\\Summary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=26)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(26,'Monthly Summary','PRETURN', 18,'Reg\\Summary_monthly.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=27)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(27,'BillNo Wise','PRETURN', 18,'Reg\\BillWise.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=28)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(28,'Details','DEBIT', 24,'Reg\\DrCr_Details.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=29)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(29,'Summary','DEBIT', 24,'Reg\\Summary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=30)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(30,'Monthly Summary','DEBIT', 24,'Reg\\Summary_monthly.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=31)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(31,'BillNo Wise','DEBIT', 24,'Reg\\BillWise.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=32)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(32,'Details','GEXPENSE', 23,'Reg\\GenExp_Details.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=33)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(33,'Summary','GEXPENSE', 23,'Reg\\Summary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=34)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(34,'Monthly Summary','GEXPENSE', 23,'Reg\\Summary_monthly.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=35)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(35,'BillNo Wise','GEXPENSE', 23,'Reg\\GenExp_BillWise.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=36)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(36,'Details','RCM', 23,'Reg\\Details.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=37)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(37,'Summary','RCM', 23,'Reg\\Summary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=38)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(38,'Monthly Summary','RCM', 23,'Reg\\Summary_monthly.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=39)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(39,'BillNo Wise Details','RCM', 23 ,'Reg\\BillWise.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=40)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(40,'ChallanNo Wise','SCHALLAN', 6,'Reg\\Challan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.challan_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=41)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(41,'Details','SCHALLAN', 6,'Reg\\Challan_Details.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.challan_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=42)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(42,'Summary','SCHALLAN', 6,'Reg\\Challan_Summary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.challan_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=43)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(43,'ChallanNo Wise','PCHALLAN', 5,'Reg\\Challan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.challan_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=44)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(44,'Details','PCHALLAN', 5,'Reg\\Challan_Details.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.challan_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=45)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(45,'Summary','PCHALLAN', 5,'Reg\\Challan_Summary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.challan_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=46)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(46,'Taka Production Detail','TP', 17,'Reg\\TakaProduction\\TakaProductionDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.YarnProd_Reg')
 
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=47)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(47,'Taka Production Summary','TP', 17,'Reg\\TakaProduction\\TakaProductionSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.YarnProd_Reg')
 
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=48)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(48,'Beam Production Detail','BP', 11,'Reg\\BeamProduction\\BeamProductionDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.BeamprodReport')
 
 -- Outstanding report
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=83)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(83,'Outstanding Details Report','RECEIVABLE', 12,'Outs\\outs_ar.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OutstandingReport')
update ReportType set ReportName = 'Outstanding Details Report' where id = 83
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=49)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(49,'Outstanding Summary Report','RECEIVABLE', 12,'Outs\\outs_summary_ar.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OutstandingReport')

update ReportType set ReportName = 'Outstanding Summary Report' where id = 49
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=53)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(53,'Outstanding Details Report','PAYABLE', 13,'Outs\\outs_ar.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OutstandingReport')
update ReportType set ReportName = 'Outstanding Details Report' where id = 53
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=54)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(54,'Outstanding Summary Report','PAYABLE', 13,'Outs\\outs_summary_ar.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OutstandingReport')
update ReportType set ReportName = 'Outstanding Summary Report' where id = 54
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=57)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(57,'Outstanding FIFO Ageing Details Report','PAYABLE', 13,'Outs\\outs_detail_ageing_ar.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Outstanding_Ageing')

--Mill  Issue
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=59)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(59,'Issue Details','MillIssue', 37,'Reg\\MillJobIssue\\IssueDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Challan_Reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=60)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(60,'Issue Details Summary','MillIssue', 37,'Reg\\MillJobIssue\\IssueSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Challan_Reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=65)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(65,'Pending Mill Issue Detail','MillIssue', 37,'Reg\\MillJobIssue\\PendingMillIssueDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.MillJobIssue_Reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=66)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(66,'Pending Mill Issue Summary','MillIssue', 37,'Reg\\MillJobIssue\\PendingMillIssueSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.MillJobIssue_Reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=117)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(117,'Pending Mill Issue Taka wise','MillIssue', 37,'Reg\\MillJobIssue\\PendingMillIssueDetailsTakawise.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.MillJobIssue_Reg')


--Job Issue
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=61)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(61,'Job Issue Details','JobIssue', 38,'Reg\\MillJobIssue\\IssueDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Challan_Reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=62)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(62,'Job Issue Detail Summary','JobIssue', 38,'Reg\\MillJobIssue\\IssueSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Challan_Reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=63)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(63,'Pending Job Issue Detail','JobIssue', 38,'Reg\\MillJobIssue\\PendingJobIssueDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.JobIssuePending_Reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=64)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(64,'Pending Job Issue Summary','JobIssue', 38,'Reg\\MillJobIssue\\PendingJobIssueSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.JobIssuePending_Reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=67)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(67,'Pending Taka Production','TP', 17,'Reg\\TakaProduction\\PendingTakaProductionDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.PendingTakaprod_reg')

--Yarn
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=68)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(68,'Yarn Production Details','YP', 21,'Reg\\YarnProd\\YarnProductionDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.YarnProd_Reg')
 
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=69)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(69,'Yarn Production Summary','YP', 21,'Reg\\YarnProd\\YarnProductionSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.YarnProd_Reg')
 
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=72)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(72,'Pending Yarn Production','YP', 21,'Reg\\YarnProd\\PendingYarnProduction.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.PendingTakaprod_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=73)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(73,'Pending Yarn Production Summary','YP', 21,'Reg\\YarnProd\\PendingYarnProdSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.PendingTakaprod_reg')


 --Ledger
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=70)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(70,'Ledger Details','LEDGER', 12,'Outs\\ledger_details.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Ledger_Reports')
 
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=71)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(71,'Ledger Summary','LEDGER', 12,'Outs\\ledger_summary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Ledger_Reports')

--Taka Production
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=74)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(74,'Pending Taka Production Summary','TP', 17,'Reg\\TakaProduction\\PendingTakaProdSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.PendingTakaprod_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=75)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(75,'WIP Report','YP', 21,'Reg\\YarnProd\\WIPReport.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.YarnWIP')

--Sales Order
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=78)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(78,'Sales Order Summary','SalesOrder', 4,'Reg\\Ord\\OrderSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderDetailsReport')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=79)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(79,'Sales Order Details','SalesOrder', 4,'Reg\\Ord\\OrderDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderDetailsReport')

--Purchase Order
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=80)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(80,'Purchase Order Summary','PurchaseOrder', 3,'Reg\\Ord\\OrderSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderDetailsReport')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=81)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(81,'Purchase Order Details','PurchaseOrder', 3,'Reg\\Ord\\OrderDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderDetailsReport')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=82)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(82,'Purchase Order Monthly Summary','PurchaseOrder', 3,'Reg\\Ord\\OrderSummary_monthly.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderDetailsReport')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=84)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(84,'Mill Issue','CHALLAN', 6,'reg\doc\GrayIssueToMillChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=85)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(85,'Packing List','CHALLAN', 6,'reg\doc\TSCPackingList.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=86)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(86,'Sales Challan A5','CHALLAN', 6,'reg\doc\SalesChallanA5.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=87)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(87,'PO A5 Size','ORD', 6,'reg\doc\PoRepA5.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=88)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(88,'SO A5 Size','ORD', 6,'reg\doc\SoRepA5.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=89)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(89,'Journal','JV', 14,'reg\doc\JVPrint.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.JVPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=90)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(90,'Yarn Challan A5','CHALLAN', 6,'reg\doc\YarnSalesChallanA5.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')
 
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=91)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(91,'Order Vs Dispatch','SalesOrder', 4,'Reg\\Ord\\OrderAgainstChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderAgainstchallanReport')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=92)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(92,'Order Vs Receipt','PurchaseOrder', 3,'Reg\\Ord\\OrderAgainstChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderAgainstchallanReport')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=93)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(93,'Account Confirmation','LEDGER', 12,'Outs\ledger_confirmation.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Ledger_Reports')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=94)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(94,'Account Confirmation2','LEDGER', 12,'Outs\ledger_confirmation_f2.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Ledger_Reports')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=95)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(95,'Machine Wise Beam','BP', 11,'Reg\\BeamProduction\\MachinewiseProduction.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.MachineBeamprodReport')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=96)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(96,'Pcs Wise A5','ORD', 6,'reg\doc\SoRepA5PcsWise.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=97)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(97,'Rate on Pcs Wise A4','ORD', 6,'reg\doc\SoRepPcsWise.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=124)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(124,'Pcs Wise A4','ORD', 6,'reg\doc\SoRepA4PcsWise.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderPrint')

update ReportType Set ReportName = 'Rate on Pcs Wise A4' where id = 97

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=98)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(98,'Outward Job Challan','JOBCHALLAN', 46,'reg\doc\OutwardJobChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=99)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(99,'Party Wise Stock Ledger T Format','SCHALLAN', 6,'Reg\\InwardOutwardDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.InwardOutwardStock')

--Job Receipt 
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=100)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(100,'Receipt Details','JobRec', 8,'Reg\\MillJobIssue\\RecDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Challan_Reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=101)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(101,'Receipt Detail Summary','JobRec', 8,'Reg\\MillJobIssue\\RecSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Challan_Reg')

--Mill Receipt
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=102)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(102,'Receipt Details','MillRec', 7,'Reg\\MillJobIssue\\RecDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Challan_Reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=103)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(103,'Receipt Details Summary','MillRec', 7,'Reg\\MillJobIssue\\RecSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Challan_Reg')

--Mill Return
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=120)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(120,'Return Details','MillRet', 49,'Reg\\MillJobIssue\\RecDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Challan_Reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=121)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(121,'Return Details Summary','MillRet', 49,'Reg\\MillJobIssue\\RecSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Challan_Reg')


--Outward
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=104)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(104,'Party Wise Stock ledger Horizontal','SCHALLAN', 6,'Reg\\InwardOutwardDetailsnew.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.InwardOutwardStock')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=105)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(105,'Color Wise Stock','Stock', 6,'Reg\\ColorStock.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.InwardOutwardStock')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=106)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(106,'Issue Beam Stock','JobIssue', 38,'Reg\\MillJobIssue\\JobIssueBeamStock.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.JobIssuePending_Reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=107)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(107,'Issue Yarn Stock','JobIssue', 38,'Reg\\MillJobIssue\\JobIssueYarnStock.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.JobIssuePending_Reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=108)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values(108,'Job Card Print','JobCard', 33,'Reg\JobCardPrint.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=109)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(109,'In Out Summary','SCHALLAN', 6,'Reg\\InwardOutwardSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.InwardOutwardStock')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=110)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(110,'Multi Yarn Challan','CHALLAN', 6,'reg\doc\YarnSalesChallanMulty.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=111)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(111,'Taka Wise Mill Issue','CHALLAN', 6,'reg\doc\GrayIssueToMillChallanTaka.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=113)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(113,'Month Wise Stock Report','Stock', 6,'Reg\\MonthwiseStock.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.MonthlyStockReportTextile')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=114)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(114,'Beam With Taka Details','BP', 11,'Reg\\BeamProduction\\BeamWithTakaDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.BeamwithtakaReport')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=115)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(115,'Quality Wise Stock Report','Stock', 7,'Reg\\QualitywiseStock.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.MonthlyStockReportTextile')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=116)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(116,'Taka Challan A5','CHALLAN', 6,'reg\doc\TakaSalesChallanA5.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

-- Payment Register

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=118)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(118,'Payment Summary','RECEIVABLE', 12,'Reg\Receipt\PaymentSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Payment_Reg')
update ReportType set ReportName = 'Payment Summary' where id = 118

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=119)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(119,'Payment Details','RECEIVABLE', 12,'Reg\Receipt\PaymentDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Payment_Reg')
update ReportType set ReportName = 'Payment Details' where id = 119

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=122)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(122,'Taka Challan A5 Duplicate Copy','CHALLAN', 6,'reg\doc\TakaSalesChallanA5DuplicateCopy.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')
 
 IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=123)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(123,'BillPrintTextilesF3','BILL', 12,'reg\doc\BillPrintTextilesF3.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.BillPrint')

 IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=125)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(125,'PurBillPrintTextilesF1','PURBILL', 13,'reg\doc\PurBillPrintTextilesF1.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.BillPrint')

 IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=126)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(126,'Inward Challan','CHALLAN', 5,'reg\doc\InwardChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

UPDATE dbo.ReportType SET VoucherTypeId = 37 WHERE id = 84
UPDATE dbo.ReportType SET VoucherTypeId = 3 WHERE id = 87
UPDATE dbo.ReportType SET VoucherTypeId = 4 WHERE id = 88

UPDATE dbo.ReportType SET VoucherTypeId = 4 WHERE id = 96
UPDATE dbo.ReportType SET VoucherTypeId = 4 WHERE id = 97
UPDATE dbo.ReportType SET VoucherTypeId =38 WHERE id = 116
UPDATE dbo.ReportType SET VoucherTypeId = 4 WHERE id = 124

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=127)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(127,'DeliveryChallan','CHALLAN', 38,'reg\doc\SalesChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=128)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(128,'TakaChallan','CHALLAN', 38,'reg\doc\TakaSalesChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')
 
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=129)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(129,'YarnChallan','CHALLAN', 38,'reg\doc\YarnSalesChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=130)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(130,'BeamChallan','CHALLAN', 38,'reg\doc\BeamSalesChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=131)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(131,'Packing List','CHALLAN', 38,'reg\doc\TSCPackingList.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=132)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(132,'Sales Challan A5','CHALLAN', 38,'reg\doc\SalesChallanA5.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=133)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(133,'Yarn Challan A5','CHALLAN', 38,'reg\doc\YarnSalesChallanA5.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=134)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(134,'Multi Yarn Challan','CHALLAN', 38,'reg\doc\YarnSalesChallanMulty.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=135)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(135,'Taka Wise Mill Issue','CHALLAN', 38,'reg\doc\GrayIssueToMillChallanTaka.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=136)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(136,'Taka Challan A5 Duplicate Copy','CHALLAN', 38,'reg\doc\TakaSalesChallanA5DuplicateCopy.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

 IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=137)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(137,'ChallanNo Wise','GPPUR', 32,'Reg\\Challan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.challan_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=138)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(138,'Details','GPPUR', 32,'Reg\\Challan_Details.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.challan_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=139)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(139,'Summary','GPPUR', 32,'Reg\\Challan_Summary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.challan_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=140)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(140,'Details','GPURCHASE', 36,'Reg\\Details.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=141)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(141,'Summary','GPURCHASE', 36,'Reg\\Summary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=142)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(142,'Monthly Summary','GPURCHASE', 36,'Reg\\Summary_monthly.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=143)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(143,'BillNo Wise','GPURCHASE', 36,'Reg\\BillWise.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=144)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(144,'Purchase Order Summary','GreyOrder', 39,'Reg\\Ord\\greyOrderSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderDetailsReport')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=145)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(145,'Purchase Order Details','GreyOrder', 39,'Reg\\Ord\\GreyOrderDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderDetailsReport')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=146)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(146,'Purchase Order Monthly Summary','GreyOrder', 39,'Reg\\Ord\\GreyOrderSummary_monthly.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderDetailsReport')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE Id=147)
INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values(147,'Order Vs Receipt','GreyOrder', 39,'Reg\\Ord\\greyOrderAgainstChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderAgainstchallanReport')

SET IDENTITY_INSERT dbo.ReportType OFF 