SET IDENTITY_INSERT dbo.SPCollection ON
IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=1)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(1,'AccLookup','For Open Account Master','' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=2)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(2,'BeamprodList','For opening Beam Production List','Used In Outward' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=3)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(3,'BillPrint','SaleInvoicePrint','' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=4)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(4,'BOMList','Bill of Material table','' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=5)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(5,'ChallanList','Challan Details','not clear' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=6)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(6,'ChallanPrint','For Challan Printing','' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=7)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(7,'DeliveryAddressList','Used in Sale challan and sale bill','For Showing Delivery Address' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=8)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(8,'FreeMachineList','Taka Production','For Showing Free Machine which is not alloted in production' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=9)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(9,'GenerateSerialNumberSp','Generating VoucherNo','Used in Every Module' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=10)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(10,'GetOrderApproveList','Sale Approve/Purchase Approve','Pending For Approval List' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=11)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(11,'GstReport','Gst Report','Used in B2B and CDNR/CDNUR' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=12)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(12,'GstReportBtoc','Gst Report B2cl','Used in B2CL' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=13)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(13,'GstReportBtocs','Gst Report B2cs','Used in B2CS' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=14)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(14,'HsnSummary','Gst Report','Used in Gst Report' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=15)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(15,'inv_reg','Not clear','Not Clear' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=16)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(16,'JobCardList','JobCard','Used in job card module' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=17)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(17,'LedgerShow','Ledger Report','Used in Ledger Report' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=18)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(18,'ListofBill','PendingBill','Used in open pending bill' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=19)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(19,'MachineWiseTakaProdList','Taka Production','Used in Opening machine wise production list' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=20)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(20,'OrderPrint','Order Print','Used in Order Printing' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=21)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(21,'OutwardBeamProd','Beam Production','Used in Opening Beam Production List' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=22)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(22,'OutwardprodList','Outward','Used in Edit Outward' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=23)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(23,'OutwardRefinishTaka','MillIssue','Used to open Refinish taka for reissue' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=24)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(24,'PendingBatch','YarnProduction','Used to open pending batch for yarnproduction' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=25)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(25,'PendingBeamLoading','BeamLoading','Used to open Pending beam for load' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=26)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(26,'PendingBill','ShowPendingBill','Used in payment receipt drcrnote jv expense bill return' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=27)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(27,'PendingChallanOnInvoice','SaleInvoicePurchaseInvoice','Used to open pending summary challan in purchaseinvoice saleinvoice' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=28)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(28,'PendingChallanOnInvoiceDet','SaleInvoicepurchaseinvoice','Used to open pending detail challan in purchaseinvoice saleinvoice' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=29)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(29,'PendingForCutting','CuttingModule','Used to open pending taka summary in cutting module' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=30)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(30,'PendingForCuttingDet','CuttingModule','Used to open pending taka details in cutting module' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=31)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(31,'PendingJOBonChallan','Outward','Used to open pending job challan to outward' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=32)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(32,'PendingJOBReceipt','Inward','Used to open pending job for receipt' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=33)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(33,'PendingJRProd','JobReceipt','For used to open pending job receipt' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=34)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(34,'PendingMillReceipt','MillReceipt','Used to open pending Mill issue to receipt' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=35)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(35,'PendingMRProd','MillReceipt','Used to open Taka details in millreceipt' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=36)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(36,'PendingOrderonChallan','Challan','Used to open pending order to challan' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=37)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(37,'PendingOrderonJobCard','JobCard','Used to open pending order to JobCard' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=38)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(38,'PendingReceiptOnInvoice','JobBill/processbill','Used to open pending receipt to Invoice' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=39)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(39,'PendingTransferOut','TransferOut','Used to open pending transfer out list' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=40)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(40,'RCPUIList','NotClear','Not Clear' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=41)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(41,'RoleWiseMenu','UserSetup','Used to Module wise role assign' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=42)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(42,'StockReport','StockReport','Used to show pending stock' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=43)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(43,'TakaprodList','TakaProductionlistviewmodel','Used to open taka production list' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=44)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(44,'GreyPurchaseList','GPViewModel','Used to open Grey purchase list' )
 
IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=45)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(45,'PendingGreyOrderonChallan','GPViewModel','Used to open Grey purchase pending list' )
 
 IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=46)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(46,'PendingOJC','OJCMainView','Used to Pending OJC' )
  
 IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=47)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(47,'PendingOJCProd','OJCMainView','Used to Pending OJC taka detail' )
 
 IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=48)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(48,'ColorMatchList','ColorMListViewModel','Used to Color list view' )
 
 IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=49)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(49,'PendingJobCard','JobCardMainView','Used to Pending Job card' )
 
IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=50)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(50,'JobCardList','JobCardListViewModel','Used to Job card List' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=51)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(51,'GreyPurchaseList','Graypurchase','Used to Gray purchase List' )

IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=51)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(52,'GetWeftById','Productviewmodel','Used to Product List' )
 
 IF NOT EXISTS(SELECT 1 FROM dbo.SPCollection WHERE Id=53)
INSERT INTO dbo.SPCollection(Id,Name,Section, Remark)
Values(53,'PendingMillIssue','MillIssue','Used to show Pending Mill Issue' )
SET IDENTITY_INSERT dbo.SPCollection OFF