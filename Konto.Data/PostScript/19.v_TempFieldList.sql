SET IDENTITY_INSERT dbo.TempField ON

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=1)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (1,'SaleTypeId')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=2)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (2,'VoucherId')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=3)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (3,'OrderDate')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=4)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (4,'InvoiceNo')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=5)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (5,'BookId')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=6)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (6,'BillAmount')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=7)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (7,'OrderNo')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=8)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (8,'Remark')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=9)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (9,'StateName')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=10)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (10,'Item')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=11)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (11,'Qty')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=12)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (12,'Unit')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=13)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (13,'Rate')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=14)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (14,'Gross')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=15)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (15,'Disc%')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=16)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (16,'Discount')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=17)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (17,'Agent')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=18)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (18,'OtherAdd')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=19)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (19,'OtherLess')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=20)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (20,'Cgst%')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=21)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (21,'CgstAmt')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=22)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (22,'Sgst%')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=23)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (23,'SgstAmt')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=24)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (24,'Igst%')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=25)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (25,'IgstAmt')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=26)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (26,'TotalAmt')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=27)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (27,'Color')


IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=28)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (28,'Design')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=29)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (29,'EwayBillNo')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=30)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (30,'LrNo')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=31)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (31,'LrDate')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=32)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (32,'Transport')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=33)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (33,'PortCode')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=34)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (34,'DueDays')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=35)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (35,'VehicleNo')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=36)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (36,'ItemRemark')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=37)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (37,'Grade')


IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=38)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (38,'Party')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=39)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (39,'FreightRate')


IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=40)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (40,'Freight')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=41)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (41,'TcsPer')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=42)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (42,'Cess')

IF NOT EXISTS (select 1 from dbo.TempField em where em.Id=43)
INSERT INTO dbo.TempField
(
   Id,FieldName
)values (43,'CessAmt')
 
SET IDENTITY_INSERT dbo.tempField OFF