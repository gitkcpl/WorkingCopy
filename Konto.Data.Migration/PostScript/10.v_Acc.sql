SET IDENTITY_INSERT dbo.Acc ON

IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=1)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (1,'NA','NA',1,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','OTH','NA',1,1,0,0,'NO',1,1,1,0)

-- sales account
IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=2)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (2,'SALES A/C','SALES',9,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','GST','NA',1,1,0,0,'NO',1,1,1,0)

--IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=3)
--INSERT INTO dbo.Acc
--(
--   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
--   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
--)values (3,'SALES A/C (IGST)','SALES A/C (IGST)',9,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','IGST.','NA',1,1,0,0,'NO',1,1,1,0)


--IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=4)
--INSERT INTO dbo.Acc
--(
--   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
--   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
--)values (4,'SALES A/C (NON-GST)','SALES A/C (NON-GST)',9,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NGST','NA',1,1,0,0,'NO',1,1,1,0)


--IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=5)
--INSERT INTO dbo.Acc
--(
--   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
--   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
--)values (5,'SALES A/C (NIL RATED)','SALES A/C (NIL RATED)',9,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NLGST','NA',1,1,0,0,'NO',1,1,1,0)

--IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=6)
--INSERT INTO dbo.Acc
--(
--   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
--   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
--)values (6,'SALES A/C (OTHER)','SALES A/C (OTHER)',9,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','OTH','NA',1,1,0,0,'NO',1,1,1,0)


-- purchase account
IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=7)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (7,'PURCHASE A/C','PURCHASE A/C',10,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','GST','NA',1,1,0,0,'NO',1,1,1,0)

IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=8)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (8,'JOB A/C','JOB A/C',10,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','IGST.','NA',1,1,0,0,'NO',1,1,1,0)


--IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=9)
--INSERT INTO dbo.Acc
--(
--   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
--   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
--)values (9,'PURCHASE A/C (NON-GST)','PURCHASE A/C (NON-GST)',10,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NGST','NA',1,1,0,0,'NO',1,1,1,0)


--IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=10)
--INSERT INTO dbo.Acc
--(
--   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
--   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
--)values (10,'PURCHASE A/C (NIL RATED)','PURCHASE A/C (NIL RATED)',10,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NLGST','NA',1,1,0,0,'NO',1,1,1,0)

--IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=11)
--INSERT INTO dbo.Acc
--(
--   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
--   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
--)values (11,'PURCHASE A/C (OTHER)','PURCHASE A/C (OTHER)',10,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','OTH','NA',1,1,0,0,'NO',1,1,1,0)


-- GST INPUT Accounts

IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=12)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (12,'SGST A/C (INPUT)','SGST A/C (INPUT)',19,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','SGST','Input',1,1,0,0,'NO',1,1,1,0)


IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=13)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (13,'CGST A/C (INPUT)','CGST A/C (INPUT)',19,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','CGST','Input',1,1,0,0,'NO',1,1,1,0)

IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=14)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (14,'IGST A/C (INPUT)','IGST A/C (INPUT)',19,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','IGST','Input',1,1,0,0,'NO',1,1,1,0)


IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=15)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (15,'CESS A/C (INPUT)','CESS A/C (INPUT)',19,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','CESS','Input',1,1,0,0,'NO',1,1,1,0)

-- GST OUTPUT

IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=16)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (16,'SGST A/C (OUTPUT)','SGST A/C (OUTPUT)',19,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','SGST','Output',1,1,0,0,'NO',1,1,1,0)


IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=17)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (17,'CGST A/C (OUTPUT)','CGST A/C (OUTPUT)',19,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','CGST','Output',1,1,0,0,'NO',1,1,1,0)

IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=18)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (18,'IGST A/C (OUTPUT)','IGST A/C (OUTPUT)',19,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','IGST','Output',1,1,0,0,'NO',1,1,1,0)


IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=19)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (19,'CESS A/C (OUTPUT)','CESS A/C (OUTPUT)',19,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','CESS','Output',1,1,0,0,'NO',1,1,1,0)

--Freight Account

IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=20)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (20,'FREIGHT A/C (INPUT)','FREIGHT A/C (INPUT)',19,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NA','NA',1,1,0,0,'NO',1,1,1,0)

IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=21)
INSERT INTO dbo.Acc 
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (21,'FREIGHT A/C (OUTPUT)','FREIGHT A/C (OUTPUT)',19,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NA','NA',1,1,0,0,'NO',1,1,1,0)

--Receivable

IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=22)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (22,'IGST RECEIVABLE A/C','IGST RECEIVABLE A/C',19,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NA','NA',1,1,0,0,'NO',1,1,1,0)

--Payable

IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=23)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (23,'CUSTOM DUTY PAYABLE A/C','CUSTOM DUTY PAYABLE A/C',19,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NA','NA',1,1,0,0,'NO',1,1,1,0)

--RCM Account

IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=24)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (24,'SGST RCM PAYBLE A/C','SGST RCM PAYBLE A/C',19,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NA','NA',1,1,0,0,'NO',1,1,1,0)

IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=25)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (25,'CGST RCM PAYBLE A/C','CGST RCM PAYBLE A/C',19,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NA','NA',1,1,0,0,'NO',1,1,1,0)

IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=26)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (26,'IGST RCM PAYBLE A/C','IGST RCM PAYBLE A/C',19,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NA','NA',1,1,0,0,'NO',1,1,1,0)

--Tds

IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=27)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (27,'TDS RECEIVABLE A/C','TDS RECEIVABLE A/C',19,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NA','NA',1,1,0,0,'NO',1,1,1,0)

IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=28)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (28,'TDS PAYABLE A/C','TDS PAYABLE A/C',19,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NA','NA',1,1,0,0,'NO',1,1,1,0)

 
IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=29)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (29,'ROUND OFF A/C','ROUND OFF A/C',13,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NA','NA',1,1,0,0,'NO',1,1,1,0)

IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=30)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (30,'PARTICULAR DETAILS A/C','PARTICULAR DETAILS A/C',13,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NA','NA',1,1,0,0,'NO',1,1,1,0)


IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=31)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (31,'TCS Payable A/C','TCS Payable A/C',19,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NA','NA',1,1,0,0,'NO',1,1,1,0)

IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=32)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (32,'TCS Receivable A/C','TCS Rceceivable A/C',19,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NA','NA',1,1,0,0,'NO',1,1,1,0)


IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=33)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (33,'IGST PAYABLE A/C','IGST PAYABLE A/C',19,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NA','NA',1,1,0,0,'NO',1,1,1,0)

IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=34)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (34,'CESS RECEIVABLE A/C','CESS RECEIVABLE A/C',19,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NA','NA',1,1,0,0,'NO',1,1,1,0)

IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=35)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (35,'Profit & Loss A/C','Profit & Loss A/C',1,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NA','NA',1,1,0,0,'NO',1,1,1,0)

IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=36)
INSERT INTO dbo.Acc
(
   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values (36,'PROCESS A/C','PROCESS A/C',29,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NA','NA',1,1,0,0,'NO',1,1,1,0)

Go


SET IDENTITY_INSERT dbo.Acc OFF



--IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=37)
--INSERT INTO dbo.Acc
--(
--   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
--   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
--)values (37,'Creditors For Expenses ','Creditors For Expenses ',36,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NA','NA',1,1,0,0,'NO',1,1,1,0)

--IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=38)
--INSERT INTO dbo.Acc
--(
--   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
--   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
--)values (38,'Creditors For Process ','Creditors For Process ',36,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NA','NA',1,1,0,0,'NO',1,1,1,0)

--IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=39)
--INSERT INTO dbo.Acc
--(
--   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
--   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
--)values (39,'Creditors For Others ','Creditors For Others ',36,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NA','NA',1,1,0,0,'NO',1,1,1,0)

--IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=40)
--INSERT INTO dbo.Acc
--(
--   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
--   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
--)values (40,'Taxation ','Taxation ',4,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NA','NA',1,1,0,0,'NO',1,1,1,0)

--IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=41)
--INSERT INTO dbo.Acc
--(
--   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
--   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
--)values (41,'STAFF ','STAFF ',36,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NA','NA',1,1,0,0,'NO',1,1,1,0)

--IF NOT EXISTS (select 1 from dbo.Acc em where em.Id=42)
--INSERT INTO dbo.Acc
--(
--   Id,AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
--   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
--)values (42,'Telephone Exp ','Telephone Exp ',12,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NA','NA',1,1,0,0,'NO',1,1,1,0)




SET IDENTITY_INSERT dbo.AccBal ON

IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=1 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(1,1,0,0,1,1,0,0,0,
0,1,1,1,1)

IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=2 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(2,2,0,0,1,9,0,0,0,
0,1,1,1,1)

--IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=3 and CompId=1 and YearId=1)
--INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
--Share,Yearid,CityId,AreaId,RouteId)
--Values(3,3,0,0,1,9,0,0,0,
--0,1,1,1,1)

--IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=4 and CompId=1 and YearId=1)
--INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
--Share,YearId,CityId,AreaId,RouteId)
--Values(4,4,0,0,1,9,0,0,0,
--0,1,1,1,1)

--IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=5 and CompId=1 and YearId=1)
--INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
--Share,YearId,CityId,AreaId,RouteId)
--Values(5,5,0,0,1,9,0,0,0,
--0,1,1,1,1)


--IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=6 and CompId=1 and YearId=1)
--INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
--Share,YearId,CityId,AreaId,RouteId)
--Values(6,6,0,0,1,9,0,0,0,
--0,1,1,1,1)

IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=7 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(7,7,0,0,1,10,0,0,0,
0,1,1,1,1)

IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=8 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(8,8,0,0,1,10,0,0,0,
0,1,1,1,1)

--IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=9 and CompId=1 and YearId=1)
--INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
--Share,YearId,CityId,AreaId,RouteId)
--Values(9,9,0,0,1,10,0,0,0,
--0,1,1,1,1)

--IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=10 and CompId=1 and YearId=1)
--INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
--Share,YearId,CityId,AreaId,RouteId)
--Values(10,10,0,0,1,10,0,0,0,
--0,1,1,1,1)

--IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=11 and CompId=1 and YearId=1)
--INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
--Share,YearId,CityId,AreaId,RouteId)
--Values(11,11,0,0,1,10,0,0,0,
--0,1,1,1,1)

IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=12 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(12,12,0,0,1,19,0,0,0,
0,1,1,1,1)

IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=13 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(13,13,0,0,1,19,0,0,0,
0,1,1,1,1)

IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=14 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(14,14,0,0,1,19,0,0,0,
0,1,1,1,1)

IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=15 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(15,15,0,0,1,19,0,0,0,
0,1,1,1,1)

IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=16 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(16,16,0,0,1,19,0,0,0,
0,1,1,1,1)

IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=17 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(17,17,0,0,1,19,0,0,0,
0,1,1,1,1)

IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=18 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(18,18,0,0,1,19,0,0,0,
0,1,1,1,1)

IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=19 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(19,19,0,0,1,19,0,0,0,
0,1,1,1,1)


IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=20 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(20,20,0,0,1,19,0,0,0,
0,1,1,1,1)

IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=21 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(21,21,0,0,1,19,0,0,0,
0,1,1,1,1)
IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=22 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(22,22,0,0,1,19,0,0,0,
0,1,1,1,1)
IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=23 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(23,23,0,0,1,19,0,0,0,
0,1,1,1,1)
IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=24 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(24,24,0,0,1,19,0,0,0,
0,1,1,1,1)
IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=25 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(25,25,0,0,1,19,0,0,0,
0,1,1,1,1)
IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=26 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(26,26,0,0,1,19,0,0,0,
0,1,1,1,1)
IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=27 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(27,27,0,0,1,19,0,0,0,
0,1,1,1,1)
IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=28 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(28,28,0,0,1,19,0,0,0,
0,1,1,1,1)
IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=29 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(29,29,0,0,1,19,0,0,0,
0,1,1,1,1)

IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=30 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(30,30,0,0,1,19,0,0,0,
0,1,1,1,1)

IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=31 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(31,31,0,0,1,19,0,0,0,
0,1,1,1,1)

IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=32 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(32,32,0,0,1,19,0,0,0,
0,1,1,1,1)

IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=33 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(Id,AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(33,33,0,0,1,19,0,0,0,
0,1,1,1,1)



SET IDENTITY_INSERT dbo.AccBal OFF

IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=35 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(35,0,0,1,1,0,0,0,
0,1,1,1,1)

IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=36 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(36,0,0,1,29,0,0,0,
0,1,1,1,1)

IF NOT EXISTS(select 1 from dbo.AccBal em WHERE AccId=34 and CompId=1 and YearId=1)
INSERT INTO dbo.AccBal(AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId)
Values(34,0,0,1,19,0,0,0,
0,1,1,1,1)




IF NOT EXISTS (select 1 from dbo.accbal em where em.mobileno='0000000000')
BEGIN
INSERT INTO dbo.Acc
(
   AccName,PrintName,GroupId,PGroupId, IsActive, IsDeleted,CreateDate,CreateUser,
   RowId,TdsReq,TcsReq,VatTds,IoTax,DeducteeId,NopId,CrDays,CrLimit,BToB,AgentId,TransportId,EmpId,AddressId
)values ('WALK-IN-CUSTOMER','WALK-IN-CUSTOMER',25,1,1,0,GetDate(),'Admin',NEWID(),'NO','NO','NA','NA',1,1,0,0,'NO',1,1,1,0)

declare @id int

select @id = @@IDENTITY

INSERT INTO dbo.AccBal(AccId,AddressId,Bal,CompId,GroupId,OpBal,OpCredit,OpDebit,
Share,YearId,CityId,AreaId,RouteId,MobileNo)
Values(@id,0,0,1,25,0,0,0,
0,1,1,1,1,'0000000000')
END
Go




-- update runing balance
UPDATE ab SET ab.Bal = ab.OpBal + ISNULL(lt.bal,0) FROM dbo.AccBal ab
LEFT OUTER JOIN (SELECT l.AccountId,l.CompanyId,
l.YearId, SUM(Debit)-SUM(Credit) bal FROM dbo.LedgerTrans l
GROUP BY l.CompanyId,l.YearId,l.AccountId)Lt
ON AB.CompId = lt.CompanyId AND ab.YearId = lt.YearId
 AND ab.AccId = lt.AccountId
 
 
 ----update input output gst wrong posting
 --sgst output
UPDATE lt SET lt.AccountId = 16 FROM dbo.LedgerTrans lt
INNER JOIN dbo.BillMain bm ON lt.RefId = bm.RowId
WHERE (bm.TypeId IN (12,19) OR (bm.TypeId=24 AND ISNULL(bm.Extra1,'X')='SALE')) 
AND lt.AccountId=12

--cgst output
UPDATE lt SET lt.AccountId = 17 FROM dbo.LedgerTrans lt
INNER JOIN dbo.BillMain bm ON lt.RefId = bm.RowId
WHERE (bm.TypeId IN (12,19) OR (bm.TypeId=24 AND ISNULL(bm.Extra1,'X')='SALE')) 
AND lt.AccountId=13

--igst output
UPDATE lt SET lt.AccountId = 18 FROM dbo.LedgerTrans lt
INNER JOIN dbo.BillMain bm ON lt.RefId = bm.RowId
WHERE (bm.TypeId IN (12,19) OR (bm.TypeId=24 AND ISNULL(bm.Extra1,'X')='SALE')) 
AND lt.AccountId=14



-- input
--sgst input
UPDATE lt SET lt.AccountId = 12 FROM dbo.LedgerTrans lt
INNER JOIN dbo.BillMain bm ON lt.RefId = bm.RowId
WHERE (bm.TypeId IN (13,18,23) OR (bm.TypeId=24 AND ISNULL(bm.Extra1,'X')='PURCHASE')) 
AND lt.AccountId=16

--cgst output
UPDATE lt SET lt.AccountId = 13 FROM dbo.LedgerTrans lt
INNER JOIN dbo.BillMain bm ON lt.RefId = bm.RowId
WHERE (bm.TypeId IN (13,18,23) OR (bm.TypeId=24 AND ISNULL(bm.Extra1,'X')='PURCHASE')) 
AND lt.AccountId=17

--igst output
UPDATE lt SET lt.AccountId = 14 FROM dbo.LedgerTrans lt
INNER JOIN dbo.BillMain bm ON lt.RefId = bm.RowId
WHERE (bm.TypeId IN (13,18,23) OR (bm.TypeId=24 AND ISNULL(bm.Extra1,'X')='PURCHASE')) 
AND lt.AccountId=18


