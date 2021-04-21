--Delete From dbo.ReportType

--SET IDENTITY_INSERT dbo.ReportType ON
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\BillPrintTextilesF1.rdlx' and VoucherTypeId=12)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('BillPrintTextilesF1','BILL', 12,'reg\doc\BillPrintTextilesF1.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.BillPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\BillPrintTextilesF2.rdlx' and VoucherTypeId=12)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('BillPrintTextilesF2','BILL', 12,'reg\doc\BillPrintTextilesF2.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.BillPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\SalesChallan.rdlx' and VoucherTypeId=6)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('SalesChallan','CHALLAN', 6,'reg\doc\SalesChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\TakaSalesChallan.rdlx' and VoucherTypeId=6)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('TakaChallan','CHALLAN', 6,'reg\doc\TakaSalesChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')
 
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\YarnSalesChallan.rdlx' and VoucherTypeId=6)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('YarnChallan','CHALLAN', 6,'reg\doc\YarnSalesChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\BeamSalesChallan.rdlx' and VoucherTypeId=6)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('BeamChallan','CHALLAN', 6,'reg\doc\BeamSalesChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\PoRep.rdlx' and VoucherTypeId=3)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('PoRep','PORD', 3,'reg\doc\PoRep.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\SoRep.rdlx' and VoucherTypeId=4)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('SoRep','ORD', 4,'reg\doc\SoRep.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\PurRetPrintTextilesF1.rdlx' and VoucherTypeId=18)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Debit Note F1','PURRET', 18,'reg\doc\PurRetPrintTextilesF1.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.BillPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\SaleRetPrintTextilesF1.rdlx' and VoucherTypeId=19)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('CREDIT NOTE F1','SALERET', 19,'reg\doc\SaleRetPrintTextilesF1.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.BillPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\NotePrintTextilesF1.rdlx' and VoucherTypeId=24)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('NotePrintTextilesF1','DRCR', 24,'reg\doc\NotePrintTextilesF1.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.BillPrint')


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Details.rdlx' AND ReportTypes='SALE')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName,
remarks)
Values('Details','SALE', 12,'Reg\\Details.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg','None-Book-Party-Agent-Item')
update dbo.ReportType set remarks='None-Book-Party-Agent-Item' where FileName='Reg\\Details.rdlx' AND ReportTypes='SALE'


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Summary.rdlx' AND ReportTypes='SALE')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, 
IsDeleted,RowId,SpName,remarks)
Values('Summary','SALE', 12,'Reg\\Summary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg',
'None-Book-Party-Agent-Item-Party+Item-Item+Party-Agent+Party-Book+Party')
update dbo.ReportType set remarks='None-Book-Party-Agent-Item-Party+Item-Item+Party-Agent+Party-Book+Party' where FileName='Reg\\Summary.rdlx' AND ReportTypes='SALE'



IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Summary_monthly.rdlx' AND ReportTypes='SALE')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, 
IsDeleted,RowId,SpName,remarks)
Values('Monthly Summary','SALE', 12,'Reg\\Summary_monthly.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg',
'None-Book-Party-Agent-Item')
update dbo.ReportType set remarks='None-Book-Party-Agent-Item' where FileName='Reg\\Summary_monthly.rdlx' AND ReportTypes='SALE'



IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\BillWise.rdlx' AND ReportTypes='SALE')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, 
	IsActive, IsDeleted,RowId,SpName,remarks)
Values('BillNo Wise','SALE', 12,'Reg\\BillWise.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg','None-Book-Party-Agent')
update dbo.ReportType set remarks='None-Book-Party-Agent' where FileName='Reg\\BillWise.rdlx' AND ReportTypes='SALE'


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Details.rdlx' AND ReportTypes='PURCHASE')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive,
IsDeleted,RowId,SpName,remarks)
Values('Details','PURCHASE', 13,'Reg\\Details.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg','None-Book-Party-Agent-Item')
update dbo.ReportType set remarks='None-Book-Party-Agent-Item' where FileName='Reg\\Details.rdlx' AND ReportTypes='PURCHASE'


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Summary.rdlx' AND ReportTypes='PURCHASE')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser,
IsActive, IsDeleted,RowId,SpName,Remarks)
Values('Summary','PURCHASE', 13,'Reg\\Summary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg',
'None-Book-Party-Agent-Item-Party+Item-Item+Party-Agent+Party-Book+Party')
update dbo.ReportType set remarks='None-Book-Party-Agent-Item-Party+Item-Item+Party-Agent+Party-Book+Party' where FileName='Reg\\Summary.rdlx' AND ReportTypes='PURCHASE'


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Summary_monthly.rdlx' AND ReportTypes='PURCHASE')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, 
	CreateUser, IsActive, IsDeleted,RowId,SpName,remarks)
Values('Monthly Summary','PURCHASE', 13,'Reg\\Summary_monthly.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg',
'None-Book-Party-Agent-Item')
update dbo.ReportType set remarks='None-Book-Party-Agent-Item' where FileName='Reg\\Summary_monthly.rdlx' AND ReportTypes='PURCHASE'


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\BillWise.rdlx' AND ReportTypes='PURCHASE')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive,
IsDeleted,RowId,SpName,Remarks)
Values('BillNo Wise','PURCHASE', 13,'Reg\\BillWise.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg','None-Book-Party-Agent')
update dbo.ReportType set remarks='None-Book-Party-Agent' where FileName='Reg\\BillWise.rdlx' AND ReportTypes='PURCHASE'


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Details.rdlx' AND ReportTypes='SRETURN')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser,
IsActive, IsDeleted,RowId,SpName,remarks)
Values('Details','SRETURN', 19,'Reg\\Details.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg','None-Book-Party-Agent-Item')
update dbo.ReportType set remarks='None-Book-Party-Agent-Item' where FileName='Reg\\Details.rdlx' AND ReportTypes='SRETURN'


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Summary.rdlx' AND ReportTypes='SRETURN')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, 
	CreateUser, IsActive, IsDeleted,RowId,SpName,remarks)
Values('Summary','SRETURN', 19,'Reg\\Summary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg',
'None-Book-Party-Agent-Item-Party+Item-Item+Party-Agent+Party-Book+Party')
update dbo.ReportType set remarks='None-Book-Party-Agent-Item-Party+Item-Item+Party-Agent+Party-Book+Party' where FileName='Reg\\Summary.rdlx' AND ReportTypes='SRETURN'


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Summary_monthly.rdlx' AND ReportTypes='SRETURN')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive,
	IsDeleted,RowId,SpName,remarks)
Values('Monthly Summary','SRETURN', 19,'Reg\\Summary_monthly.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg',
'None-Book-Party-Agent-Item')
update dbo.ReportType set remarks='None-Book-Party-Agent-Item' where FileName='Reg\\Summary_monthly.rdlx' AND ReportTypes='SRETURN'


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\BillWise.rdlx' AND ReportTypes='SRETURN')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser,
	IsActive, IsDeleted,RowId,SpName,remarks)
Values('BillNo Wise','SRETURN', 19,'Reg\\BillWise.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg','None-Book-Party-Agent')
update dbo.ReportType set remarks='None-Book-Party-Agent' where FileName='Reg\\BillWise.rdlx' AND ReportTypes='SRETURN'


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Details.rdlx' AND ReportTypes='PRETURN')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Details','PRETURN', 18,'Reg\\Details.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')
update dbo.ReportType set remarks='None-Book-Party-Agent-Item' where FileName='Reg\\Details.rdlx' AND ReportTypes='PRETURN'


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Summary.rdlx' AND ReportTypes='PRETURN')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Summary','PRETURN', 18,'Reg\\Summary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')
update dbo.ReportType set remarks='None-Book-Party-Agent-Item-Party+Item-Item+Party-Agent+Party-Book+Party' where FileName='Reg\\Summary.rdlx' AND ReportTypes='PRETURN'

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Summary_monthly.rdlx' AND ReportTypes='PRETURN')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Monthly Summary','PRETURN', 18,'Reg\\Summary_monthly.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')
update dbo.ReportType set remarks='None-Book-Party-Agent-Item' where FileName='Reg\\Summary_monthly.rdlx' AND ReportTypes='PRETURN'

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\BillWise.rdlx' AND ReportTypes='PRETURN')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('BillNo Wise','PRETURN', 18,'Reg\\BillWise.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')
update dbo.ReportType set remarks='None-Book-Party-Agent' where FileName='Reg\\BillWise.rdlx' AND ReportTypes='PRETURN'


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\DrCr_Details.rdlx' AND ReportTypes='DEBIT')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Details','DEBIT', 24,'Reg\\DrCr_Details.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')
update dbo.ReportType set remarks='None-Book-Party-Agent' where FileName='Reg\\DrCr_Details.rdlx' AND ReportTypes='DEBIT'



IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Summary.rdlx' AND ReportTypes='DEBIT')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Summary','DEBIT', 24,'Reg\\Summary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')
update dbo.ReportType set remarks='None-Book-Party-Agent-Agent+Party-Book+Party' where FileName='Reg\\Summary.rdlx' AND ReportTypes='DEBIT'


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Summary_monthly.rdlx' AND ReportTypes='DEBIT')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Monthly Summary','DEBIT', 24,'Reg\\Summary_monthly.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')
update dbo.ReportType set remarks='None-Book-Party-Agent' where FileName='Reg\\Summary_monthly.rdlx' AND ReportTypes='DEBIT'


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\BillWise.rdlx' AND ReportTypes='DEBIT')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('BillNo Wise','DEBIT', 24,'Reg\\BillWise.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')
update dbo.ReportType set remarks='None-Book-Party-Agent' where FileName='Reg\\BillWise.rdlx' AND ReportTypes='DEBIT'


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\GenExp_Details.rdlx' AND ReportTypes='GEXPENSE')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Details','GEXPENSE', 23,'Reg\\GenExp_Details.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')
update dbo.ReportType set remarks='None-Book-Party-Agent' where FileName='Reg\\GenExp_Details.rdlx' AND ReportTypes='GEXPENSE'


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Summary.rdlx' AND ReportTypes='GEXPENSE')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Summary','GEXPENSE', 23,'Reg\\Summary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')
update dbo.ReportType set remarks='None-Book-Party-Agent-Agent+Party-Book+Party' where FileName='Reg\\Summary.rdlx' AND ReportTypes='GEXPENSE'

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Summary_monthly.rdlx' AND ReportTypes='GEXPENSE')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Monthly Summary','GEXPENSE', 23,'Reg\\Summary_monthly.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')
update dbo.ReportType set remarks='None-Book-Party-Agent' where FileName='Reg\\Summary_monthly.rdlx' AND ReportTypes='GEXPENSE'


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\GenExp_BillWise.rdlx' AND ReportTypes='GEXPENSE')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('BillNo Wise','GEXPENSE', 23,'Reg\\GenExp_BillWise.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')
update dbo.ReportType set remarks='None-Book-Party-Agent' where FileName='Reg\\GenExp_BillWise.rdlx' AND ReportTypes='GEXPENSE'


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Details.rdlx' AND ReportTypes='RCM')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Details','RCM', 23,'Reg\\Details.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')
update dbo.ReportType set remarks='None-Book-Party-Agent-Item' where FileName='Reg\\GenExp_Details.rdlx' AND ReportTypes='RCM'


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Summary.rdlx' AND ReportTypes='RCM')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Summary','RCM', 23,'Reg\\Summary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')
update dbo.ReportType set remarks='None-Book-Party-Agent-Agent+Party-Book+Party-Party+Item-Item+Party' where FileName='Reg\\Summary.rdlx' AND ReportTypes='RCM'


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Summary_monthly.rdlx' AND ReportTypes='RCM')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Monthly Summary','RCM', 23,'Reg\\Summary_monthly.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')
update dbo.ReportType set remarks='None-Book-Party-Agent-Item' where FileName='Reg\\Summary_monthly.rdlx' AND ReportTypes='RCM'


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\BillWise.rdlx' AND ReportTypes='RCM')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('BillNo Wise Details','RCM', 23 ,'Reg\\BillWise.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')
update dbo.ReportType set remarks='None-Book-Party-Agent-Item' where FileName='Reg\\GenExp_BillWise.rdlx' AND ReportTypes='RCM'


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Challan.rdlx' AND ReportTypes='SCHALLAN')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('ChallanNo Wise','SCHALLAN', 6,'Reg\\Challan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.challan_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Challan_Details.rdlx' AND ReportTypes='SCHALLAN')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Details','SCHALLAN', 6,'Reg\\Challan_Details.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.challan_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Challan_Summary.rdlx' AND ReportTypes='SCHALLAN')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Summary','SCHALLAN', 6,'Reg\\Challan_Summary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.challan_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Challan.rdlx' AND ReportTypes='PCHALLAN')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('ChallanNo Wise','PCHALLAN', 5,'Reg\\Challan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.challan_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Challan_Details.rdlx' AND ReportTypes='PCHALLAN')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Details','PCHALLAN', 5,'Reg\\Challan_Details.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.challan_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Challan_Summary.rdlx' AND ReportTypes='PCHALLAN')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Summary','PCHALLAN', 5,'Reg\\Challan_Summary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.challan_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\TakaProduction\\TakaProductionDetails.rdlx' AND ReportTypes='TP')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Taka Production Detail','TP', 17,'Reg\\TakaProduction\\TakaProductionDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.YarnProd_Reg')
 
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\TakaProduction\\TakaProductionSummary.rdlx' AND ReportTypes='TP')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Taka Production Summary','TP', 17,'Reg\\TakaProduction\\TakaProductionSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.YarnProd_Reg')
 
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\BeamProduction\\BeamProductionDetails.rdlx' AND ReportTypes='BP')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Beam Production Detail','BP', 11,'Reg\\BeamProduction\\BeamProductionDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.BeamprodReport')
 
 -- Outstanding report
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Outs\\outs_ar.rdlx' AND ReportTypes='RECEIVABLE')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Outstanding Details Report','RECEIVABLE', 12,'Outs\\outs_ar.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OutstandingReport')

--update ReportType set ReportName = 'Outstanding Details Report' where id = 83

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Outs\\outs_summary_ar.rdlx' AND ReportTypes='RECEIVABLE')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Outstanding Summary Report','RECEIVABLE', 12,'Outs\\outs_summary_ar.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OutstandingReport')

--update ReportType set ReportName = 'Outstanding Summary Report' where id = 49

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Outs\\outs_ar.rdlx' AND ReportTypes='PAYABLE')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Outstanding Details Report','PAYABLE', 13,'Outs\\outs_ar.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OutstandingReport')
--update ReportType set ReportName = 'Outstanding Details Report' where id = 53

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Outs\\outs_summary_ar.rdlx' AND ReportTypes='PAYABLE')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Outstanding Summary Report','PAYABLE', 13,'Outs\\outs_summary_ar.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OutstandingReport')
--update ReportType set ReportName = 'Outstanding Summary Report' where id = 54

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Outs\\outs_detail_ageing_ar.rdlx' AND ReportTypes='PAYABLE')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Outstanding FIFO Ageing Details Report','PAYABLE', 13,'Outs\\outs_detail_ageing_ar.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Outstanding_Ageing')

--Mill  Issue
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\MillJobIssue\\IssueDetails.rdlx' AND ReportTypes='MillIssue')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Issue Details','MillIssue', 37,'Reg\\MillJobIssue\\IssueDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Challan_Reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\MillJobIssue\\IssueSummary.rdlx' AND ReportTypes='MillIssue')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Issue Details Summary','MillIssue', 37,'Reg\\MillJobIssue\\IssueSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Challan_Reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\MillJobIssue\\PendingMillIssueDetails.rdlx' AND ReportTypes='MillIssue')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Pending Mill Issue Detail','MillIssue', 37,'Reg\\MillJobIssue\\PendingMillIssueDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.MillJobIssue_Reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\MillJobIssue\\PendingMillIssueSummary.rdlx' AND ReportTypes='MillIssue')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Pending Mill Issue Summary','MillIssue', 37,'Reg\\MillJobIssue\\PendingMillIssueSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.MillJobIssue_Reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\MillJobIssue\\PendingMillIssueDetailsTakawise.rdlx' AND ReportTypes='MillIssue')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Pending Mill Issue Taka wise','MillIssue', 37,'Reg\\MillJobIssue\\PendingMillIssueDetailsTakawise.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.MillJobIssue_Reg')


--Job Issue
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\MillJobIssue\\IssueDetails.rdlx' AND ReportTypes='JobIssue')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Job Issue Details','JobIssue', 38,'Reg\\MillJobIssue\\IssueDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Challan_Reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\MillJobIssue\\IssueSummary.rdlx' AND ReportTypes='JobIssue')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Job Issue Detail Summary','JobIssue', 38,'Reg\\MillJobIssue\\IssueSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Challan_Reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\MillJobIssue\\PendingJobIssueDetails.rdlx' AND ReportTypes='JobIssue')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Pending Job Issue Detail','JobIssue', 38,'Reg\\MillJobIssue\\PendingJobIssueDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.JobIssuePending_Reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\MillJobIssue\\PendingJobIssueSummary.rdlx' AND ReportTypes='JobIssue')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Pending Job Issue Summary','JobIssue', 38,'Reg\\MillJobIssue\\PendingJobIssueSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.JobIssuePending_Reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\TakaProduction\\PendingTakaProductionDetails.rdlx' AND ReportTypes='TP')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Pending Taka Production','TP', 17,'Reg\\TakaProduction\\PendingTakaProductionDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.PendingTakaprod_reg')

--Yarn
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\YarnProd\\YarnProductionDetails.rdlx' AND ReportTypes='YP')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Yarn Production Details','YP', 21,'Reg\\YarnProd\\YarnProductionDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.YarnProd_Reg')
 
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\YarnProd\\YarnProductionSummary.rdlx' AND ReportTypes='YP')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Yarn Production Summary','YP', 21,'Reg\\YarnProd\\YarnProductionSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.YarnProd_Reg')
 
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\YarnProd\\PendingYarnProduction.rdlx' AND ReportTypes='YP')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Pending Yarn Production','YP', 21,'Reg\\YarnProd\\PendingYarnProduction.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.PendingTakaprod_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\YarnProd\\PendingYarnProdSummary.rdlx' AND ReportTypes='YP')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Pending Yarn Production Summary','YP', 21,'Reg\\YarnProd\\PendingYarnProdSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.PendingTakaprod_reg')


 --Ledger
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Outs\\ledger_details.rdlx' AND ReportTypes='LEDGER')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Ledger Details','LEDGER', 12,'Outs\\ledger_details.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Ledger_Reports')
 
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Outs\\ledger_summary.rdlx' AND ReportTypes='LEDGER')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Ledger Summary','LEDGER', 12,'Outs\\ledger_summary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Ledger_Reports')

--Taka Production
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\TakaProduction\\PendingTakaProdSummary.rdlx' AND ReportTypes='TP')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Pending Taka Production Summary','TP', 17,'Reg\\TakaProduction\\PendingTakaProdSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.PendingTakaprod_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\YarnProd\\WIPReport.rdlx' AND ReportTypes='YP')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('WIP Report','YP', 21,'Reg\\YarnProd\\WIPReport.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.YarnWIP')

--Sales Order
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Ord\\OrderSummary.rdlx' AND ReportTypes='SalesOrder')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Sales Order Summary','SalesOrder', 4,'Reg\\Ord\\OrderSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderDetailsReport')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Ord\\OrderDetails.rdlx' AND ReportTypes='SalesOrder')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Sales Order Details','SalesOrder', 4,'Reg\\Ord\\OrderDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderDetailsReport')

--Purchase Order
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Ord\\OrderSummary.rdlx' AND ReportTypes='PurchaseOrder')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Purchase Order Summary','PurchaseOrder', 3,'Reg\\Ord\\OrderSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderDetailsReport')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Ord\\OrderDetails.rdlx' AND ReportTypes='PurchaseOrder')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Purchase Order Details','PurchaseOrder', 3,'Reg\\Ord\\OrderDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderDetailsReport')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Ord\\OrderSummary_monthly.rdlx' AND ReportTypes='PurchaseOrder')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Purchase Order Monthly Summary','PurchaseOrder', 3,'Reg\\Ord\\OrderSummary_monthly.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderDetailsReport')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\GrayIssueToMillChallan.rdlx' AND ReportTypes='CHALLAN')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Mill Issue','CHALLAN', 6,'reg\doc\GrayIssueToMillChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\TSCPackingList.rdlx' AND ReportTypes='CHALLAN')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Packing List','CHALLAN', 6,'reg\doc\TSCPackingList.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\SalesChallanA5.rdlx' AND ReportTypes='CHALLAN')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Sales Challan A5','CHALLAN', 6,'reg\doc\SalesChallanA5.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\PoRepA5.rdlx' AND ReportTypes='ORD')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('PO A5 Size','ORD', 6,'reg\doc\PoRepA5.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\SoRepA5.rdlx' AND ReportTypes='ORD')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('SO A5 Size','ORD', 6,'reg\doc\SoRepA5.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\JVPrint.rdlx' AND ReportTypes='JV')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Journal','JV', 14,'reg\doc\JVPrint.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.JVPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\YarnSalesChallanA5.rdlx' AND ReportTypes='CHALLAN')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Yarn Challan A5','CHALLAN', 6,'reg\doc\YarnSalesChallanA5.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')
 
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Ord\\OrderAgainstChallan.rdlx' AND ReportTypes='SalesOrder')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Order Vs Dispatch','SalesOrder', 4,'Reg\\Ord\\OrderAgainstChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderAgainstchallanReport')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Ord\\OrderAgainstChallan.rdlx' AND ReportTypes='PurchaseOrder')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Order Vs Receipt','PurchaseOrder', 3,'Reg\\Ord\\OrderAgainstChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderAgainstchallanReport')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Outs\ledger_confirmation.rdlx' AND ReportTypes='LEDGER')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Account Confirmation','LEDGER', 12,'Outs\ledger_confirmation.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Ledger_Reports')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Outs\ledger_confirmation_f2.rdlx' AND ReportTypes='LEDGER')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Account Confirmation2','LEDGER', 12,'Outs\ledger_confirmation_f2.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Ledger_Reports')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\BeamProduction\\MachinewiseProduction.rdlx' AND ReportTypes='BP')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Machine Wise Beam','BP', 11,'Reg\\BeamProduction\\MachinewiseProduction.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.MachineBeamprodReport')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\SoRepA5PcsWise.rdlx' AND ReportTypes='ORD')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Pcs Wise A5','ORD', 4,'reg\doc\SoRepA5PcsWise.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\SoRepPcsWise.rdlx' AND ReportTypes='ORD')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Rate on Pcs Wise A4','ORD', 4,'reg\doc\SoRepPcsWise.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\SoRepA4PcsWise.rdlx' AND ReportTypes='ORD')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Pcs Wise A4','ORD', 4,'reg\doc\SoRepA4PcsWise.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderPrint')

--update ReportType Set ReportName = 'Rate on Pcs Wise A4' where id = 97

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\OutwardJobChallan.rdlx' AND ReportTypes='JOBCHALLAN')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Outward Job Challan','JOBCHALLAN', 46,'reg\doc\OutwardJobChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\InwardOutwardDetails.rdlx' AND ReportTypes='SCHALLAN')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Party Wise Stock Ledger T Format','SCHALLAN', 6,'Reg\\InwardOutwardDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.InwardOutwardStock')

--Job Receipt 
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\MillJobIssue\\RecDetails.rdlx' AND ReportTypes='JobRec')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Receipt Details','JobRec', 8,'Reg\\MillJobIssue\\RecDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Challan_Reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\MillJobIssue\\RecSummary.rdlx' AND ReportTypes='JobRec')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Receipt Detail Summary','JobRec', 8,'Reg\\MillJobIssue\\RecSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Challan_Reg')

--Mill Receipt
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\MillJobIssue\\RecDetails.rdlx' AND ReportTypes='MillRec')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Receipt Details','MillRec', 7,'Reg\\MillJobIssue\\RecDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Challan_Reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\MillJobIssue\\RecSummary.rdlx' AND ReportTypes='MillRec')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Receipt Details Summary','MillRec', 7,'Reg\\MillJobIssue\\RecSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Challan_Reg')

--Mill Return
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\MillJobIssue\\RecDetails.rdlx' AND ReportTypes='MillRet')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Return Details','MillRet', 49,'Reg\\MillJobIssue\\RecDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Challan_Reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\MillJobIssue\\RecSummary.rdlx' AND ReportTypes='MillRet')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Return Details Summary','MillRet', 49,'Reg\\MillJobIssue\\RecSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Challan_Reg')


--Outward
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\InwardOutwardDetailsnew.rdlx' AND ReportTypes='SCHALLAN')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Party Wise Stock ledger Horizontal','SCHALLAN', 6,'Reg\\InwardOutwardDetailsnew.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.InwardOutwardStock')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\ColorStock.rdlx' AND ReportTypes='Stock')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Color Wise Stock','Stock', 6,'Reg\\ColorStock.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.InwardOutwardStock')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\MillJobIssue\\JobIssueBeamStock.rdlx' AND ReportTypes='JobIssue')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Issue Beam Stock','JobIssue', 38,'Reg\\MillJobIssue\\JobIssueBeamStock.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.JobIssuePending_Reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\MillJobIssue\\JobIssueYarnStock.rdlx' AND ReportTypes='JobIssue')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Issue Yarn Stock','JobIssue', 38,'Reg\\MillJobIssue\\JobIssueYarnStock.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.JobIssuePending_Reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\JobCardPrint.rdlx' AND ReportTypes='JobCard')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId)
Values('Job Card Print','JobCard', 33,'Reg\JobCardPrint.rdlx',GETDATE(),'Admin',1,0,NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\InwardOutwardSummary.rdlx' AND ReportTypes='SCHALLAN')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('In Out Summary','SCHALLAN', 6,'Reg\\InwardOutwardSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.InwardOutwardStock')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\YarnSalesChallanMulty.rdlx' AND ReportTypes='CHALLAN')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Multi Yarn Challan','CHALLAN', 6,'reg\doc\YarnSalesChallanMulty.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

--IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\GrayIssueToMillChallanTaka.rdlx' AND ReportTypes='CHALLAN')
--INSERT INTO dbo.ReportType(Id,ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
--Values('Taka Wise Mill Issue','CHALLAN', 6,'reg\doc\GrayIssueToMillChallanTaka.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\MonthwiseStock.rdlx' AND ReportTypes='Stock')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Month Wise Stock Report','Stock', 6,'Reg\\MonthwiseStock.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.MonthlyStockReportTextile')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\BeamProduction\\BeamWithTakaDetails.rdlx' AND ReportTypes='BP')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Beam With Taka Details','BP', 11,'Reg\\BeamProduction\\BeamWithTakaDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.BeamwithtakaReport')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\QualitywiseStock.rdlx' AND ReportTypes='Stock')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Quality Wise Stock Report','Stock', 7,'Reg\\QualitywiseStock.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.MonthlyStockReportTextile')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\TakaSalesChallanA5.rdlx' AND ReportTypes='CHALLAN')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Taka Challan A5','CHALLAN', 6,'reg\doc\TakaSalesChallanA5.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

-- Payment Register

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\Receipt\PaymentSummary.rdlx' AND ReportTypes='RECEIVABLE')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Payment Summary','RECEIVABLE', 12,'Reg\Receipt\PaymentSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Payment_Reg')
--update ReportType set ReportName = 'Payment Summary' where id = 118

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\Receipt\PaymentDetails.rdlx' AND ReportTypes='RECEIVABLE')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Payment Details','RECEIVABLE', 12,'Reg\Receipt\PaymentDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.Payment_Reg')
--update ReportType set ReportName = 'Payment Details' where id = 119

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\TakaSalesChallanA5DuplicateCopy.rdlx' AND ReportTypes='CHALLAN')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Taka Challan A5 Duplicate Copy','CHALLAN', 6,'reg\doc\TakaSalesChallanA5DuplicateCopy.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')
 
 IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\BillPrintTextilesF3.rdlx' AND ReportTypes='BILL')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('BillPrintTextilesF3','BILL', 12,'reg\doc\BillPrintTextilesF3.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.BillPrint')

 IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\PurBillPrintTextilesF1.rdlx' AND ReportTypes='PURBILL')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('PurBillPrintTextilesF1','PURBILL', 13,'reg\doc\PurBillPrintTextilesF1.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.BillPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\InwardChallan.rdlx' AND ReportTypes='CHALLAN')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Inward Challan','CHALLAN', 5,'reg\doc\InwardChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')



IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\SalesChallan.rdlx' and VoucherTypeId=38)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('DeliveryChallan','CHALLAN', 38,'reg\doc\SalesChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\TakaSalesChallan.rdlx' and VoucherTypeId=38)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('TakaChallan','CHALLAN', 38,'reg\doc\TakaSalesChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')
 
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\YarnSalesChallan.rdlx' and VoucherTypeId=38)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('YarnChallan','CHALLAN', 38,'reg\doc\YarnSalesChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\BeamSalesChallan.rdlx' and VoucherTypeId=38)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('BeamChallan','CHALLAN', 38,'reg\doc\BeamSalesChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\TSCPackingList.rdlx' and VoucherTypeId=38)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Packing List','CHALLAN', 38,'reg\doc\TSCPackingList.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\SalesChallanA5.rdlx' and VoucherTypeId=38)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Sales Challan A5','CHALLAN', 38,'reg\doc\SalesChallanA5.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\YarnSalesChallanA5.rdlx' and VoucherTypeId=38)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Yarn Challan A5','CHALLAN', 38,'reg\doc\YarnSalesChallanA5.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\YarnSalesChallanMulty.rdlx' and VoucherTypeId=38)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Multi Yarn Challan','CHALLAN', 38,'reg\doc\YarnSalesChallanMulty.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\GrayIssueToMillChallanTaka.rdlx' and VoucherTypeId=38)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Taka Wise Mill Issue','CHALLAN', 38,'reg\doc\GrayIssueToMillChallanTaka.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Challan.rdlx' and VoucherTypeId=32)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('ChallanNo Wise','GPPUR', 32,'Reg\\Challan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.challan_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Challan_Details.rdlx' and VoucherTypeId=32)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Details','GPPUR', 32,'Reg\\Challan_Details.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.challan_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Challan_Summary.rdlx' and VoucherTypeId=32)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Summary','GPPUR', 32,'Reg\\Challan_Summary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.challan_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Details.rdlx' and VoucherTypeId=36)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Details','GPURCHASE', 36,'Reg\\Details.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Summary.rdlx' and VoucherTypeId=36)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Summary','GPURCHASE', 36,'Reg\\Summary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Summary_monthly.rdlx' and VoucherTypeId=36)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Monthly Summary','GPURCHASE', 36,'Reg\\Summary_monthly.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\BillWise.rdlx' and VoucherTypeId=36)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('BillNo Wise','GPURCHASE', 36,'Reg\\BillWise.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.sales_reg')


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Ord\\greyOrderSummary.rdlx' and VoucherTypeId=39)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Purchase Order Summary','GreyOrder', 39,'Reg\\Ord\\greyOrderSummary.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderDetailsReport')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Ord\\GreyOrderDetails.rdlx' and VoucherTypeId=39)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Purchase Order Details','GreyOrder', 39,'Reg\\Ord\\GreyOrderDetails.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderDetailsReport')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Ord\\GreyOrderSummary_monthly.rdlx' and VoucherTypeId=39)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Purchase Order Monthly Summary','GreyOrder', 39,'Reg\\Ord\\GreyOrderSummary_monthly.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderDetailsReport')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Reg\\Ord\\greyOrderAgainstChallan.rdlx' and VoucherTypeId=39)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Order Vs Receipt','GreyOrder', 39,'Reg\\Ord\\greyOrderAgainstChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderAgainstchallanReport')


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\GrayIssueToMillChallan.rdlx' and VoucherTypeId=37)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Mill Challan F1','MI', 37,'reg\doc\GrayIssueToMillChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\jobChallan.rdlx' and VoucherTypeId=38)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Job Challan F1','JIB', 38,'reg\doc\jobChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\TakaJobSalesChallan.rdlx' and VoucherTypeId=46)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Job Challan F1','JSC', 46,'reg\doc\TakaJobSalesChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

--SET IDENTITY_INSERT dbo.ReportType OFF 


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\GrnInwardChallan.rdlx' and VoucherTypeId=5)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Grn Print','GRN', 5,'reg\doc\GrnInwardChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\GRNBarcode.rdlx' and VoucherTypeId=5)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Barcode','GRN', 5,'reg\doc\GRNBarcode.rdlx',GETDATE(),'Admin',1,0,NEWID(),'query')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\JobReceiptChallan.rdlx' and VoucherTypeId=8)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Job Receipt Challan','JRB', 8,'reg\doc\JobReceiptChallan.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.ChallanPrint')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\GRNBarcode.rdlx' and VoucherTypeId=8)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Barcode','JRB', 8,'reg\doc\GRNBarcode.rdlx',GETDATE(),'Admin',1,0,NEWID(),'query')


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\tds\tds_details.mrt')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Tds Details','Tds', 0,'reg\tds\tds_details.mrt',GETDATE(),'Admin',1,0,NEWID(),'dbo.tds')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\tds\tds_summary.mrt')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Tds Summary','Tds', 0,'reg\tds\tds_summary.mrt',GETDATE(),'Admin',1,0,NEWID(),'dbo.tds')


-- Purchase Request/indent
update dbo.ReportType set VoucherTypeId=2  WHERE FileName='reg\doc\ReqRep.rdlx'

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\ReqRep.rdlx' and VoucherTypeId=2)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Request Document','REQDOC', 2,'reg\doc\ReqRep.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.OrderPrint')


--Journall Voucher Print
IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='reg\doc\JvPrint.rdlx' and VoucherTypeId=14)
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Journal Voucher','JVP', 14,'reg\doc\JvPrint.rdlx',GETDATE(),'Admin',1,0,NEWID(),'dbo.JvPrint')


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Konto.Reporting.XReport.XJobInc.JobIncomeXRep' AND ReportTypes='Job_Income')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Inward','Job_Income', 5,'Konto.Reporting.XReport.XJobInc.JobIncomeXRep',GETDATE(),'Admin',1,0,NEWID(),'dbo.job_work_income')

IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Konto.Reporting.XReport.XJobInc.JobIncomeDetailsXRep' AND ReportTypes='Job_Income')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Inward Details','Job_Income', 5,'Konto.Reporting.XReport.XJobInc.JobIncomeDetailsXRep',GETDATE(),'Admin',1,0,NEWID(),'dbo.job_work_income')


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Konto.Reporting.XReport.XJobInc.JobInwardVsOutwardXRep' AND ReportTypes='Job_Income')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Inward Vs Outward Details','Job_Income', 5,'Konto.Reporting.XReport.XJobInc.JobInwardVsOutwardXRep',GETDATE(),'Admin',1,0,NEWID(),'dbo.job_income_inwardVsOutward')


IF NOT EXISTS(SELECT 1 FROM dbo.ReportType WHERE FileName='Konto.Reporting.XReport.XGate.GateEntryXRep' AND ReportTypes='Gate_Inward')
INSERT INTO dbo.ReportType(ReportName,ReportTypes, VoucherTypeId,FileName,CreateDate, CreateUser, IsActive, IsDeleted,RowId,SpName)
Values('Gate Entry','Gate_Inward', 53,'Konto.Reporting.XReport.XGate.GateEntryXRep',GETDATE(),'Admin',1,0,NEWID(),'dbo.dbo.gate_entry_rep')

--SET IDENTITY_INSERT dbo.ReportType OFF 

Go
