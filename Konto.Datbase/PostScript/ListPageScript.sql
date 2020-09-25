--po
if NOT exists (select 1 from ListPage em where em.Id=1)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (1, 'Normal','dbo.POList','PO\NormalList.xml',3,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=2)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (2, 'Detail List','dbo.PODetailList','PO\DetailView.xml',3,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=3)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (3, 'Party Wise List','dbo.PoSummaryList','PO\PartyWiseView.xml',3,'ac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=5)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (5, 'Product Wise List','dbo.PoSummaryList','PO\ProductSummaryView.xml',3,'pd.ProductName',null)
END

 if NOT exists (select 1 from ListPage em where em.Id=157)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (157, 'Deleted List','dbo.POList','PO\DeletedView.xml',3,null,null,'Deleted')
END

--SO
if NOT exists (select 1 from ListPage em where em.Id=6)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (6, 'Normal List','dbo.POList','SO\NormalListView.xml',4,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=39)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (39, 'Detail List','dbo.PODetailList','SO\DetailView.xml',4,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=40)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (40, 'Party Wise List','dbo.PoSummaryList','SO\PartyWiseView.xml',4,'ac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=42)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (42, 'Product Wise List','dbo.PoSummaryList','SO\ProductSummaryView.xml',4,'pd.ProductName',null)
END

 if NOT exists (select 1 from ListPage em where em.Id=158)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (158, 'Deleted List','dbo.POList','SO\DeletedView.xml',4,null,null,'Deleted')
END

--grn
if NOT exists (select 1 from ListPage em where em.Id=7)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (7, 'Normal List','dbo.ChallanList','GRN\NormalListView.xml',5,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=43)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (43, 'Detail List','dbo.ChallanDetailList','GRN\DetailView.xml',5,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=44)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (44, 'Party Wise List','dbo.ChallanSummaryList','GRN\PartyWiseView.xml',5,'ac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=46)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (46, 'Product Wise List','dbo.ChallaSummaryList','GRN\ProductSummaryView.xml',5,'pd.ProductName',null)
END

 if NOT exists (select 1 from ListPage em where em.Id=159)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (159, 'Deleted List','dbo.ChallanList','GRN\DeletedView.xml',5,null,null,'Deleted')
END

--Sales Challan
if NOT exists (select 1 from ListPage em where em.Id=8)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (8, 'Normal List','dbo.ChallanList','SC\NormalListView.xml',6,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=47)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (47, 'Detail List','dbo.ChallanDetailList','SC\DetailView.xml',6,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=48)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (48, 'Party Wise List','dbo.ChallanSummaryList','SC\PartyWiseView.xml',6,'ac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=50)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (50, 'Product Wise List','dbo.ChallaSummaryList','SC\ProductSummaryView.xml',6,'pd.ProductName',null)
END

 if NOT exists (select 1 from ListPage em where em.Id=160)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (160, 'Deleted List','dbo.ChallanList','SC\DeletedView.xml',6,null,null,'Deleted')
END

--MillReceipt
if NOT exists (select 1 from ListPage em where em.Id=9)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (9, 'Normal List','dbo.ChallanList','MR\NormalListView.xml',7,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=51)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (51, 'Detail List','dbo.ChallanDetailList','MR\DetailView.xml',7,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=52)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (52, 'Party Wise List','dbo.ChallanSummaryList','MR\PartyWiseView.xml',7,'ac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=54)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (54, 'Product Wise List','dbo.ChallanSummaryList','MR\ProductSummaryView.xml',7,'pd.ProductName',null)
END

 if NOT exists (select 1 from ListPage em where em.Id=161)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (161, 'Deleted List','dbo.ChallanList','MR\DeletedView.xml',7,null,null,'Deleted')
END

--JobReceipt
if NOT exists (select 1 from ListPage em where em.Id=10)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (10, 'Normal List','dbo.ChallanList','JR\NormalListView.xml',8,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=55)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (55, 'Detail List','dbo.ChallanDetailList','JR\DetailView.xml',8,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=56)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (56, 'Party Wise List','dbo.ChallanSummaryList','JR\PartyWiseView.xml',8,'ac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=58)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (58, 'Product Wise List','dbo.ChallaSummaryList','JR\ProductSummaryView.xml',8,'pd.ProductName',null)
END

 if NOT exists (select 1 from ListPage em where em.Id=162)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (162, 'Deleted List','dbo.ChallanList','JR\DeletedView.xml',8,null,null,'Deleted')
END

--Store Issue
if NOT exists (select 1 from ListPage em where em.Id=11)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (11, 'Normal List','dbo.ChallanList','SIS\NormalListView.xml',9,null,null)
END

 
if NOT exists (select 1 from ListPage em where em.Id=59)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (59, 'Detail List','dbo.ChallanDetailList','SIS\DetailView.xml',9,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=60)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (60, 'Party Wise List','dbo.ChallanSummaryList','SIS\PartyWiseView.xml',9,'ac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=62)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (62, 'Product Wise List','dbo.ChallaSummaryList','SIS\ProductSummaryView.xml',9,'pd.ProductName',null)
END

 if NOT exists (select 1 from ListPage em where em.Id=163)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (163, 'Deleted List','dbo.ChallanList','SIS\DeletedView.xml',9,null,null,'Deleted')
END

--DesignMapping
if NOT exists (select 1 from ListPage em where em.Id=12)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (12, 'Normal List','dbo.DesignMappingList','DM\NormalListView.xml',10,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=63)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (63, 'Detail List','dbo.ChallanDetailList','DM\DetailView.xml',10,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=64)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (64, 'Party Wise List','dbo.ChallanSummaryList','DM\PartyWiseView.xml',10,'ac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=66)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (66, 'Product Wise List','dbo.ChallaSummaryList','DM\ProductSummaryView.xml',10,'pd.ProductName',null)
END

--MillIssue
if NOT exists (select 1 from ListPage em where em.Id=13)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (13, 'Normal List','dbo.ChallanList','MI\NormalListView.xml',37,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=67)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (67, 'Detail List','dbo.ChallanDetailList','MI\DetailView.xml',37,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=68)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (68, 'Party Wise List','dbo.ChallanSummaryList','MI\PartyWiseView.xml',37,'ac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=70)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (70, 'Product Wise List','dbo.ChallanSummaryList','MI\ProductSummaryView.xml',37,'pd.ProductName',null)
END

 if NOT exists (select 1 from ListPage em where em.Id=164)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (164, 'Deleted List','dbo.ChallanList','MI\DeletedView.xml',37,null,null,'Deleted')
END

--JobIssue
if NOT exists (select 1 from ListPage em where em.Id=14)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (14, 'Normal List','dbo.ChallanList','JI\NormalListView.xml',38,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=71)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (71, 'Detail List','dbo.ChallanDetailList','JI\DetailView.xml',38,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=72)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (72, 'Party Wise List','dbo.ChallanSummaryList','JI\PartyWiseView.xml',38,'ac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=74)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (74, 'Product Wise List','dbo.ChallaSummaryList','JI\ProductSummaryView.xml',38,'pd.ProductName',null)
END

 if NOT exists (select 1 from ListPage em where em.Id=165)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (165, 'Deleted List','dbo.ChallanList','JI\DeletedView.xml',38,null,null,'Deleted')
END

--gray Order
if NOT exists (select 1 from ListPage em where em.Id=15)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (15, 'Normal List','dbo.GOList','GO\NormalListView.xml',39,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=75)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (75, 'Detail List','dbo.PODetailList','GO\DetailView.xml',39,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=76)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (76, 'Party Wise List','dbo.POSummaryList','GO\PartyWiseView.xml',39,'ac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=78)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (78, 'Product Wise List','dbo.POSummaryList','GO\ProductSummaryView.xml',39,'pd.ProductName',null)
END

--Mill Receipt Voucher
if NOT exists (select 1 from ListPage em where em.Id=16)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (16, 'Normal List','dbo.mrv_bill_list','MRV\NormalListView.xml',40,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=79)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (79, 'Detail List','dbo.mrv_bill_detail_list','MRV\DetailView.xml',40,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=80)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (80, 'Party Wise List','dbo.BillSummaryList','MRV\PartyWiseView.xml',40,'ac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=82)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (82, 'Product Wise List','dbo.BillSummaryList','MRV\ProductSummaryView.xml',40,'pd.ProductName',null)
END

 if NOT exists (select 1 from ListPage em where em.Id=166)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (166, 'Deleted List','dbo.BillList','MRV\DeletedView.xml',40,null,null,'Deleted')
END

--Job Receipt Voucher
if NOT exists (select 1 from ListPage em where em.Id=17)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (17, 'Normal List','dbo.BillList','JRV\NormalListView.xml',41,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=83)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (83, 'Detail List','dbo.BillDetailList','JRV\DetailView.xml',41,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=84)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (84, 'Party Wise List','dbo.BillSummaryList','JRV\PartyWiseView.xml',41,'ac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=86)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (86, 'Product Wise List','dbo.BillSummaryList','JRV\ProductSummaryView.xml',41,'pd.ProductName',null)
END

 if NOT exists (select 1 from ListPage em where em.Id=167)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (167, 'Deleted List','dbo.BillList','JRV\DeletedView.xml',41,null,null,'Deleted')
END

--Grey Purchase
if NOT exists (select 1 from ListPage em where em.Id=18)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (18, 'Normal List','dbo.GPList','GP\NormalListView.xml',36,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=87)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (87, 'Detail List','dbo.ChallanDetailList','GP\DetailView.xml',36,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=88)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (88, 'Party Wise List','dbo.ChallanSummaryList','GP\PartyWiseView.xml',36,'ac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=90)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (90, 'Product Wise List','dbo.ChallaSummaryList','GP\ProductSummaryView.xml',36,'pd.ProductName',null)
END
 
if NOT exists (select 1 from ListPage em where em.Id=168)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (168, 'Deleted List','dbo.BillList','GP\DeletedView.xml',36,null,null,'Deleted')
END

--Sales Invoice
if NOT exists (select 1 from ListPage em where em.Id=19)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (19, 'Normal List','dbo.BillList','SI\NormalListView.xml',12,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=20)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (20, 'Detail List','dbo.BillDetailList','SI\DetailListView.xml',12,null,null)
END
 
if NOT exists (select 1 from ListPage em where em.Id=91)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (91, 'Party Wise List','dbo.BillSummaryList','SI\PartyWiseView.xml',12,'ac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=93)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (93, 'Product Wise List','dbo.BillSummaryList','SI\ProductSummaryView.xml',12,'pd.ProductName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=145)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (145, 'Cancelled Bill List','dbo.BillList','SI\CancelListView.xml',12,null,null,'Cancelled')
END

if NOT exists (select 1 from ListPage em where em.Id=146)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (146, 'Deleted Bill List','dbo.BillList','SI\DeletedListView.xml',12,null,null,'Deleted')
END

if NOT exists (select 1 from ListPage em where em.Id=174)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (174, 'Revised List','dbo.BillList','SI\RevisedListView.xml',12,null,null,'Revised')
END

--Purchase Invoice
if NOT exists (select 1 from ListPage em where em.Id=21)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (21, 'Normal List','dbo.BillList','PI\NormalListView.xml',13,null,null)
END
 
if NOT exists (select 1 from ListPage em where em.Id=22)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (22, 'Detail List','dbo.BillDetailList','PI\DetailListView.xml',13,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=94)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (94, 'Party Wise List','dbo.BillSummaryList','PI\PartyWiseView.xml',13,'ac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=96)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (96, 'Product Wise List','dbo.BillSummaryList','PI\ProductSummaryView.xml',13,'pd.ProductName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=147)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (147, 'Cancelled Bill List','dbo.BillList','SI\NormalListView.xml',13,null,null,'Cancelled')
END

if NOT exists (select 1 from ListPage em where em.Id=148)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (148, 'Deleted Bill List','dbo.BillList','SI\NormalListView.xml',13,null,null,'Deleted')
END

--Purchase Return
if NOT exists (select 1 from ListPage em where em.Id=23)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (23, 'Normal List','dbo.BillList','PR\NormalListView.xml',18,null,null)
END
 
if NOT exists (select 1 from ListPage em where em.Id=24)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (24, 'Detail List','dbo.BillDetailList','PR\DetailListView.xml',18,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=97)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (97, 'Party Wise List','dbo.BillSummaryList','PR\PartyWiseView.xml',18,'ac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=99)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (99, 'Product Wise List','dbo.BillSummaryList','PR\ProductSummaryView.xml',18,'pd.ProductName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=169)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (169, 'Deleted List','dbo.BillList','PR\DeletedView.xml',18,null,null,'Deleted')
END

--Sales Return
if NOT exists (select 1 from ListPage em where em.Id=25)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (25, 'Normal List','dbo.BillList','SR\NormalListView.xml',19,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=26)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (26, 'Detail List','dbo.BillDetailList','SR\DetailListView.xml',19,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=100)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (100, 'Party Wise List','dbo.BillSummaryList','SR\PartyWiseView.xml',19,'ac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=102)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (102, 'Product Wise List','dbo.BillSummaryList','SR\ProductSummaryView.xml',19,'pd.ProductName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=149)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (149, 'Deleted List','dbo.BillList','SR\DeletedListView.xml',19,null,null,'Deleted')
END

if NOT exists (select 1 from ListPage em where em.Id=176)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (176, 'Revised List','dbo.BillList','SR\RevisedListView.xml',19,null,null,'Revised')
END

--DrCrNote 
if NOT exists (select 1 from ListPage em where em.Id=27)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (27, 'Normal List','dbo.BillList','DRCR\NormalListView.xml',24,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=28)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (28, 'Detail List','dbo.CRDRDetailList','DRCR\DetailListView.xml',24,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=170)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (170, 'Deleted List','dbo.BillList','DRCR\DeletedListView.xml',24,null,null,'Deleted')
END
 
if NOT exists (select 1 from ListPage em where em.Id=175)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (175, 'Revised List','dbo.BillList','DRCR\RevisedListView.xml',24,null,null,'Revised')
END

--GenExpense
if NOT exists (select 1 from ListPage em where em.Id=29)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (29, 'Normal List','dbo.GenExpenseList','EXP\NormalListView.xml',23,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=30)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (30, 'Detail List','dbo.GenExpenseList','EXP\DetailListView.xml',23,null,null)
END


if NOT exists (select 1 from ListPage em where em.Id=106)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (106, 'Party Wise List','dbo.BillSummaryList','EXP\PartyWiseView.xml',23,'ac.AccName',null)
END


if NOT exists (select 1 from ListPage em where em.Id=108)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (108, 'Exp Book Wise List','dbo.BillSummaryList','EXP\ProductSummaryView.xml',23,'Toac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=150)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (150, 'Deleted List','dbo.GenExpenseList','EXP\DeletedListView.xml',23,null,null,'Deleted')
END

--GenExpense Return
if NOT exists (select 1 from ListPage em where em.Id=132)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (132, 'Normal List','dbo.GenExpenseList','EXPR\NormalListView.xml',48,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=133)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (133, 'Detail List','dbo.GenExpenseList','EXPR\DetailListView.xml',48,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=134)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (134, 'Party Wise List','dbo.BillSummaryList','EXPR\PartyWiseView.xml',48,'ac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=135)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (135, 'Exp Book Wise List','dbo.BillSummaryList','EXPR\ProductSummaryView.xml',48,'Toac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=151)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (151, 'Deleted List','dbo.GenExpenseList','EXPR\DeletedListView.xml',48,null,null,'Deleted')
END

--Payment
if NOT exists (select 1 from ListPage em where em.Id=33)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (33, 'Normal List','dbo.PaymemtReceiptDetailList','PV\NormalListView.xml',16,null,null)
END

update dbo.ListPage set SpName = 'dbo.PaymemtReceiptDetailList' where id = 33

if NOT exists (select 1 from ListPage em where em.Id=34)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (34, 'Detail List','dbo.PaymemtReceiptDetailList','PV\DetailListView.xml',16,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=109)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (109, 'Party Wise List','dbo.PRSummaryList','PV\PartyWiseView.xml',16,'ac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=111)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (111, 'Particular Wise List','dbo.PRSummaryList','PV\ProductSummaryView.xml',16,'Toac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=152)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (152, 'Deleted List','dbo.PaymemtReceiptList','PV\DeletedListView.xml',16,null,null,'Deleted')
END

--Receipt
if NOT exists (select 1 from ListPage em where em.Id=35)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (35, 'Normal List','dbo.PaymemtReceiptDetailList','RV\NormalListView.xml',15,null,null)
END

update dbo.ListPage set SpName = 'dbo.PaymemtReceiptDetailList' where id = 35

if NOT exists (select 1 from ListPage em where em.Id=36)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (36, 'Detail List','dbo.PaymemtReceiptDetailList','RV\DetailListView.xml',15,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=112)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (112, 'Party Wise List','dbo.PRSummaryList','RV\PartyWiseView.xml',15,'ac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=114)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (114, 'Particular Wise List','dbo.PRSummaryList','RV\ProductSummaryView.xml',15,'Toac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=153)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (153, 'Deleted List','dbo.PaymemtReceiptList','RV\DeletedListView.xml',15,null,null,'Deleted')
END

--Store Issue Return
if NOT exists (select 1 from ListPage em where em.Id=37)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (37, 'Normal List','dbo.ChallanList','SIR\NormalListView.xml',35,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=119)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (119, 'Detail List','dbo.ChallanDetailList','SIR\DetailView.xml',35,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=120)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (120, 'Party Wise List','dbo.ChallanSummaryList','SIR\PartyWiseView.xml',35,'ac.AccName' ,null)
END


if NOT exists (select 1 from ListPage em where em.Id=122)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (122, 'Product Wise List','dbo.ChallaSummaryList','SIR\ProductSummaryView.xml',35,'pd.ProductName' ,null)
END

if NOT exists (select 1 from ListPage em where em.Id=154)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (154, 'Deleted List','dbo.ChallanList','SIR\NormalListView.xml',35,null,null,'Deleted')
END
 
--JV
if NOT exists (select 1 from ListPage em where em.Id=38)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (38, 'Normal List','dbo.JVDetailList','JV\DetailWiseView.xml',14,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=155)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (155, 'Deleted List','dbo.JVDetailList','JV\DeletedView.xml',14,null,null,'Deleted')
END
--Outward Job Challan
if NOT exists (select 1 from ListPage em where em.Id=123)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (123, 'Normal List','dbo.ChallanList','OJC\NormalListView.xml',46,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=124)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (124, 'Detail List','dbo.ChallanDetailList','OJC\DetailView.xml',46,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=125)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (125, 'Party Wise List','dbo.ChallanSummaryList','OJC\PartyWiseView.xml',46,'ac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=127)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (127, 'Product Wise List','dbo.ChallaSummaryList','OJC\ProductSummaryView.xml',46,'pd.ProductName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=171)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (171, 'Deleted List','dbo.ChallanList','OJC\DeletedListView.xml',46,null,null,'Deleted')
END

--Job Against Po
if NOT exists (select 1 from ListPage em where em.Id=136)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (136, 'Normal List','dbo.ChallanList','JAP\NormalListView.xml',47,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=137)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (137, 'Detail List','dbo.ChallanDetailList','JAP\DetailView.xml',47,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=138)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (138, 'Party Wise List','dbo.ChallanSummaryList','JAP\PartyWiseView.xml',47,'ac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=139)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (139, 'Product Wise List','dbo.ChallaSummaryList','JAP\ProductSummaryView.xml',47,'pd.ProductName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=172)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (172, 'Deleted List','dbo.ChallanList','JAP\DeletedListView.xml',47,null,null,'Deleted')
END

--Mill Return
if NOT exists (select 1 from ListPage em where em.Id=140)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (140, 'Normal List','dbo.ChallanList','MR\NormalListView.xml',49,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=141)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (141, 'Detail List','dbo.ChallanDetailList','MR\DetailView.xml',49,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=142)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (142, 'Party Wise List','dbo.ChallanSummaryList','MR\PartyWiseView.xml',49,'ac.AccName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=143)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (143, 'Product Wise List','dbo.ChallaSummaryList','MR\ProductSummaryView.xml',49,'pd.ProductName',null)
END

if NOT exists (select 1 from ListPage em where em.Id=173)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (173, 'Deleted List','dbo.ChallanList','MR\DeletedListView.xml',49,null,null,'Deleted')
END

--Brokerage Module

if NOT exists (select 1 from ListPage em where em.Id=144)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol
)values (144, 'Normal List','dbo.BrokerageDetailList','BR\NormalListView.xml',50,null,null)
END

if NOT exists (select 1 from ListPage em where em.Id=156)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (156, 'Deleted List','dbo.BrokerageDetailList','BR\DeletedView.xml',50,null,null,'Deleted')
END

/* for analysis page */

if NOT exists (select 1 from ListPage em where em.Id=177)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (177, 'Party_Monthly','dbo.gp_analysis','analysis\gp\party_monthly.olapx',36,null,null,'analysis')
END

if NOT exists (select 1 from ListPage em where em.Id=178)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (178, 'Party_Item_Monthly','dbo.gp_analysis','analysis\gp\party_item_monthly.olapx',36,null,null,'analysis')
END

if NOT exists (select 1 from ListPage em where em.Id=179)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (179, 'Item_Monthly','dbo.gp_analysis','analysis\gp\item_monthly.olapx',36,null,null,'analysis')
END

if NOT exists (select 1 from ListPage em where em.Id=180)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (180, 'Item_Quarterly','dbo.gp_analysis','analysis\gp\item_qtr.olapx',36,null,null,'analysis')
END

if NOT exists (select 1 from ListPage em where em.Id=181)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (181, 'Quarterly_Totals','dbo.gp_analysis','analysis\gp\quarterly_total.olapx',36,null,null,'analysis')
END

if NOT exists (select 1 from ListPage em where em.Id=182)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (182, 'Party_Quartelry_Ratio','dbo.gp_analysis','analysis\gp\party_qtr_per.olapx',36,null,null,'analysis')
END

if NOT exists (select 1 from ListPage em where em.Id=183)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (183, 'Party_Quarterly','dbo.gp_analysis','analysis\gp\party_qtr.olapx',36,null,null,'analysis')
END


if NOT exists (select 1 from ListPage em where em.Id=184)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (184, 'Party_Monthly_Ratio','dbo.bill_analysis','analysis\pur\party_month_per.olapx',36,null,null,'analysis')
END


/*Purchase Invoice */
if NOT exists (select 1 from ListPage em where em.Id=185)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (185, 'Party_Monthly','dbo.gp_analysis','analysis\pur\party_monthly.olapx',13,null,null,'analysis')
END

if NOT exists (select 1 from ListPage em where em.Id=186)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (186, 'Party_Item_Monthly','dbo.gp_analysis','analysis\pur\party_item_monthly.olapx',13,null,null,'analysis')
END

if NOT exists (select 1 from ListPage em where em.Id=187)
BEGIN
INSERT INTO dbo.ListPage
(
   Id, Descr,SpName,LayoutFile,VTypeId,GroupCol,SumCol,Extra1
)values (187, 'Item_Monthly','dbo.gp_analysis','analysis\pur\item_monthly.olapx',13,null,null,'analysis')
END
