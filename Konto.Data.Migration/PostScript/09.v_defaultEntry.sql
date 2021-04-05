
SET IDENTITY_INSERT dbo.City on
GO

if NOT exists (select 1 from City em where em.Id=1)
BEGIN
INSERT INTO dbo.City
(
   Id, CityName, StateId,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (1, 'NA',24,1,0,GetDate(),'Admin',NEWID())
END
go
if NOT exists (select 1 from City em where em.Id=2)
BEGIN
INSERT INTO dbo.City
(
   Id, CityName, StateId,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (2, 'SURAT',24,1,0,GetDate(),'Admin',NEWID())
END
go

SET IDENTITY_INSERT dbo.City OFF
GO

SET IDENTITY_INSERT dbo.Area ON
if NOT exists (select 1 from Area em where em.Id=1)
BEGIN
INSERT INTO dbo.Area
(
   Id, AreaName, CityId,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (1, 'NA',1,1,0,GetDate(),'Admin',NEWID())
END
go
 
SET IDENTITY_INSERT dbo.Area OFF
GO

-- route
SET IDENTITY_INSERT dbo.Route ON
if NOT exists (select 1 from Route em where em.Id=1)
BEGIN
INSERT INTO dbo.Route
(
   Id, RouteName, CityId,AreaId, IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (1, 'NA',1,1,1,0,GetDate(),'Admin',NEWID())
END
go
SET IDENTITY_INSERT dbo.Route OFF
GO

-- Company
SET IDENTITY_INSERT dbo.Company ON
IF NOT EXIStS(select 1 from dbo.Company em WHERE em.Id=1)
INSERT INTO dbo.Company(Id,CompName,PrintName,CityId,StateId,Pincode,Para,RowId,
IsActive,IsDeleted,CreateUser,CreateDate,NobId)
Values(1,'Demo','Demo',1,24,'395002','demo',NEWID(),1,0,'Admin',GETDATE(),1)
SET IDENTITY_INSERT dbo.Company OFF

--- Branch 

SET IDENTITY_INSERT dbo.Branch on
GO

if NOT exists (select 1 from Branch em where em.Id=1)
BEGIN
INSERT INTO dbo.Branch
(
   Id, BranchName, CompId,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (1, 'NA',1,1,0,GetDate(),'Admin',NEWID())
END
go

SET IDENTITY_INSERT dbo.Branch OFF
GO

-- Brand
SET IDENTITY_INSERT dbo.Brand ON
if NOT exists (select 1 from Brand em where em.Id=1)
BEGIN
INSERT INTO dbo.Brand
(
   Id, BrandName, BrandCode,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (1, 'NA','NA',1,0,GetDate(),'Admin',NEWID())
END
go

SET IDENTITY_INSERT dbo.Brand OFF
Go

--- Product Group
SET IDENTITY_INSERT dbo.PGroup ON
if NOT exists (select 1 from dbo.PGroup em where em.Id=1)
BEGIN
INSERT INTO dbo.PGroup
(
   Id, GroupName, GroupCode,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (1, 'NA','NA',1,0,GetDate(),'Admin',NEWID())
END

SET IDENTITY_INSERT dbo.PGroup OFF
GO

--- Product Sub Group
SET IDENTITY_INSERT dbo.PSubGroup ON
if NOT exists (select 1 from dbo.PSubGroup em where em.Id=1)
BEGIN
INSERT INTO dbo.PSubGroup
(
   Id, SubName, SubCode,PGroupId, IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (1, 'NA','NA',1,1,0,GetDate(),'Admin',NEWID())
END

SET IDENTITY_INSERT dbo.PSubGroup OFF
GO

--- Product Sub Group
SET IDENTITY_INSERT dbo.Roles ON
if NOT exists (select 1 from dbo.Roles em where em.Id=1)
BEGIN
INSERT INTO dbo.Roles
(
   Id,IsSysAdmin,CreateDate,RoleDescription,RoleName,
   RowId,IsActive,IsDeleted
)values (1, 1,GETDATE(),'Allows system administration of Users/Roles/Permissions','System Administrator',NEWID(),1,0)
END

if NOT exists (select 1 from dbo.Roles em where em.Id=2)
BEGIN
INSERT INTO dbo.Roles
(
   Id,IsSysAdmin,CreateDate,RoleDescription,RoleName,
   RowId,IsActive,IsDeleted
)values (2, 0,GETDATE(),'Default role with limited permissions','Default User',NEWID(),1,0)
END
SET IDENTITY_INSERT dbo.Roles OFF
GO

-- Create Default User --1
SET IDENTITY_INSERT dbo.UserMaster ON
if NOT exists (select 1 from dbo.UserMaster em where em.Id=1)
BEGIN
	INSERT INTO UserMaster(Id,CreateDate,EmpId,IsActive,RoleId,RowId,UserName,UserPass,IsDeleted)
	values(1,GETDATE(),1,1,1,NEWID(),'KEYSOFT','N8WqBhvsRTrv7C71OZxNXQ==',0)
END

UPDATE dbo.UserMaster set UserPass = 'N8WqBhvsRTrv7C71OZxNXQ==' WHERE Id = 1

if NOT exists (select 1 from dbo.UserMaster em where em.Id=2)
BEGIN
	INSERT INTO UserMaster(Id,CreateDate,EmpId,IsActive,RoleId,RowId,UserName,UserPass,IsDeleted)
	values(2,GETDATE(),1,1,1,NEWID(),'Admin','iqdT6mfVNJs=',0)
END
SET IDENTITY_INSERT dbo.UserMaster OFF
go

-- Product Typpe
SET IDENTITY_INSERT dbo.ProductType ON
if NOT exists (select 1 from dbo.ProductType em where em.Id=1)
BEGIN
INSERT INTO dbo.ProductType
(
   Id, TypeName, TypeCode,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (1, 'NA','NA',1,0,GetDate(),'Admin',NEWID())
END
go

if NOT exists (select 1 from dbo.ProductType em where em.Id=2)
BEGIN
INSERT INTO dbo.ProductType
(
   Id, TypeName, TypeCode,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (2, 'GREY','GREY',1,0,GetDate(),'Admin',NEWID())
END
go

if NOT exists (select 1 from dbo.ProductType em where em.Id=3)
BEGIN
INSERT INTO dbo.ProductType
(
   Id, TypeName, TypeCode,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (3, 'FINISH','FINISH',1,0,GetDate(),'Admin',NEWID())
END
go

if NOT exists (select 1 from dbo.ProductType em where em.Id=4)
BEGIN
INSERT INTO dbo.ProductType
(
   Id, TypeName, TypeCode,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (4, 'SEMIFINISH','SEMIFINISH',1,0,GetDate(),'Admin',NEWID())
END
go

if NOT exists (select 1 from dbo.ProductType em where em.Id=5)
BEGIN
INSERT INTO dbo.ProductType
(
   Id, TypeName, TypeCode,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (5, 'BEAM','BEAM',1,0,GetDate(),'Admin',NEWID())
END
go

if NOT exists (select 1 from dbo.ProductType em where em.Id=6)
BEGIN
INSERT INTO dbo.ProductType
(
   Id, TypeName, TypeCode,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (6, 'YARN','YARN',1,0,GetDate(),'Admin',NEWID())
END
go
   
if NOT exists (select 1 from dbo.ProductType em where em.Id=7)
BEGIN
INSERT INTO dbo.ProductType
(
   Id, TypeName, TypeCode,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (7, 'POY','POY',1,0,GetDate(),'Admin',NEWID())
END
go
   
if NOT exists (select 1 from dbo.ProductType em where em.Id=8)
BEGIN
INSERT INTO dbo.ProductType
(
   Id, TypeName, TypeCode,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (8, 'RAW MATERIAL','RAW MATERIAL',1,0,GetDate(),'Admin',NEWID())
END
go
    
if NOT exists (select 1 from dbo.ProductType em where em.Id=9)
BEGIN
INSERT INTO dbo.ProductType
(
   Id, TypeName, TypeCode,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (9, 'STORE ITEM','STORE ITEM',1,0,GetDate(),'Admin',NEWID())
END
go
    
if NOT exists (select 1 from dbo.ProductType em where em.Id=10)
BEGIN
INSERT INTO dbo.ProductType
(
   Id, TypeName, TypeCode,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (10, 'PACKING ITEM','PACKING ITEM',1,0,GetDate(),'Admin',NEWID())
END
go
    
if NOT exists (select 1 from dbo.ProductType em where em.Id=11)
BEGIN
INSERT INTO dbo.ProductType
(
   Id, TypeName, TypeCode,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (11, 'OTHERS','OTHERS',1,0,GetDate(),'Admin',NEWID())
END
go
   
if NOT exists (select 1 from dbo.ProductType em where em.Id=12)
BEGIN
INSERT INTO dbo.ProductType
(
   Id, TypeName, TypeCode,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (12, 'COLOR','COLOR',1,0,GetDate(),'Admin',NEWID())
END
go
   
if NOT exists (select 1 from dbo.ProductType em where em.Id=13)
BEGIN
INSERT INTO dbo.ProductType
(
   Id, TypeName, TypeCode,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (13, 'CHEMICAL','CHEMICAL',1,0,GetDate(),'Admin',NEWID())
END
go
   
SET IDENTITY_INSERT dbo.ProductType OFF
Go


-- Size Master
SET IDENTITY_INSERT dbo.PSize ON
if NOT exists (select 1 from dbo.PSize em where em.Id=1)
BEGIN
INSERT INTO dbo.PSize
(
   Id, SizeName, SizeCode,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (1, 'NA','NA',1,0,GetDate(),'Admin',NEWID())
END
go
SET IDENTITY_INSERT dbo.PSize OFF
Go

-- Color
SET IDENTITY_INSERT dbo.Color ON
if NOT exists (select 1 from dbo.Color em where em.Id=1)
BEGIN
INSERT INTO dbo.Color
(
   Id, ColorName, ColorCode,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (1, 'NA','NA',1,0,GetDate(),'Admin',NEWID())
END
go
SET IDENTITY_INSERT dbo.Color OFF
Go

-- Item Category
SET IDENTITY_INSERT dbo.PCategory ON
IF NOT EXISTS (select 1 from dbo.PCategory em where em.Id=1)
BEGIN
INSERT INTO dbo.PCategory
(
   Id,CatCode,CatName,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (1, 'NA','NA',1,0,GetDate(),'Admin',NEWID())
END
go
SET IDENTITY_INSERT dbo.PCategory OFF
Go

-- Catalog
SET IDENTITY_INSERT dbo.Catalog ON
IF NOT EXISTS (select 1 from dbo.Catalog em where em.Id=1)
BEGIN
INSERT INTO dbo.Catalog
(Id,CatalogNo,CatalogName,IsActive,IsDeleted,CreateDate,CreateUser
 ,RowId
 )values (1, 'NA','NA',1,0,GetDate(),'Admin',NEWID())
END
go
SET IDENTITY_INSERT dbo.Catalog OFF
Go
 
-- style
SET IDENTITY_INSERT dbo.Style ON
IF NOT EXISTS (select 1 from dbo.Style em where em.Id=1)
BEGIN
INSERT INTO dbo.Style
(
   Id,StyleCode,StyleName,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (1, 'NA','NA',1,0,GetDate(),'Admin',NEWID())
END
go
SET IDENTITY_INSERT dbo.Style OFF
Go

-- PartyGroup
SET IDENTITY_INSERT dbo.PartyGroup ON
IF NOT EXISTS (select 1 from dbo.PartyGroup em where em.Id=1)
BEGIN
INSERT INTO dbo.PartyGroup
(
   Id,GroupName,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (1,'NA',1,0,GetDate(),'Admin',NEWID())
END
go
SET IDENTITY_INSERT dbo.PartyGroup OFF
Go

---emp
SET IDENTITY_INSERT dbo.Emp ON
IF NOT EXISTS(select 1 from dbo.Emp em where em.Id=1)
INSERT INTO dbo.Emp(Id,RowId,EmpName,IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,CompId)
Values(1,NEWID(),'NA',1,0,GETDATE(),'Admin','NA',1)
SEt IDENTITY_INSERT dbo.Emp OFF

update dbo.Emp set CompId =1 where id=1

--Fin Year
SET IDENTITY_INSERT dbo.FinYear ON
IF NOT EXISTS(select 1 from dbo.FinYear em where em.Id=1)
INSERT INTO dbo.FinYear(Id,YearCode,FromDate,ToDate,FDate,TDate,
IsActive,IsDeleted,CreateUser,CreateDate,RowId)
Values(1,datename(YEAR,  DATEADD(M,-3,GETDATE())) + '-' + cast((datepart(YEAR,  DATEADD(M,-3,GETDATE())) + 1)%100  as varchar(2)),
DATENAME(YEAR,  DATEADD(M,-3,GETDATE())) + '0401',
cast((datepart(YEAR,  DATEADD(M,-3,GETDATE())) + 1) as varchar(4)) +'0331',
cast('01-APR-' + DATENAME(YEAR,  DATEADD(M,-3,GETDATE())) as datetime2),
cast('31-MAR-' + cast((datepart(YEAR,  DATEADD(M,-3,GETDATE())) + 1)%100  as varchar(2)) as datetime2),
1,0,'Admin',GETDATE(),NEWID())
SET IDENTITY_INSERT dbo.FinYear OFF

-- Tax Master
SET IDENTITY_INSERT dbo.TaxMaster ON
IF NOT EXISTS(SELECT 1 FROM dbo.TaxMaster em where em.Id=1)
INSERT INTO dbo.TaxMaster(Id,RowId,TaxName,TaxType,Cess,CessRate,CessType,Cgst,
Sgst,CreateDate,CreateUser,Igst,IpAddress,IsActive,IsDeleted)
VALUES(1,NEWID(),'5% GST','GST',0,0,'None',2.5,
2.5,GETDATE(),'Admin',5,'NA',1,0)

IF NOT EXISTS(SELECT 1 FROM dbo.TaxMaster em where em.Id=2)
INSERT INTO dbo.TaxMaster(Id,RowId,TaxName,TaxType,Cess,CessRate,CessType,Cgst,
Sgst,CreateDate,CreateUser,Igst,IpAddress,IsActive,IsDeleted)
VALUES(2,NEWID(),'12% GST','GST',0,0,'None',6,
6,GETDATE(),'Admin',12,'NA',1,0)

IF NOT EXISTS(SELECT 1 FROM dbo.TaxMaster em where em.Id=3)
INSERT INTO dbo.TaxMaster(Id,RowId,TaxName,TaxType,Cess,CessRate,CessType,Cgst,
Sgst,CreateDate,CreateUser,Igst,IpAddress,IsActive,IsDeleted)
VALUES(3,NEWID(),'18% GST','GST',0,0,'None',9,
9,GETDATE(),'Admin',18,'NA',1,0)

IF NOT EXISTS(SELECT 1 FROM dbo.TaxMaster em where em.Id=4)
INSERT INTO dbo.TaxMaster(Id,RowId,TaxName,TaxType,Cess,CessRate,CessType,Cgst,
Sgst,CreateDate,CreateUser,Igst,IpAddress,IsActive,IsDeleted)
VALUES(4,NEWID(),'28% GST','GST',0,0,'None',14,
14,GETDATE(),'Admin',28,'NA',1,0)

IF NOT EXISTS(SELECT 1 FROM dbo.TaxMaster em where em.Id=5)
INSERT INTO dbo.TaxMaster(Id,RowId,TaxName,TaxType,Cess,CessRate,CessType,Cgst,
Sgst,CreateDate,CreateUser,Igst,IpAddress,IsActive,IsDeleted)
VALUES(5,NEWID(),'3% GST','GST',0,0,'None',1.5,
1.5,GETDATE(),'Admin',3,'NA',1,0)

IF NOT EXISTS(SELECT 1 FROM dbo.TaxMaster em where em.Id=6)
INSERT INTO dbo.TaxMaster(Id,RowId,TaxName,TaxType,Cess,CessRate,CessType,Cgst,
Sgst,CreateDate,CreateUser,Igst,IpAddress,IsActive,IsDeleted)
VALUES(6,NEWID(),'Nil Rated','Nil Rated',0,0,'None',0,
0,GETDATE(),'Admin',0,'NA',1,0)


IF NOT EXISTS(SELECT 1 FROM dbo.TaxMaster em where em.Id=7)
INSERT INTO dbo.TaxMaster(Id,RowId,TaxName,TaxType,Cess,CessRate,CessType,Cgst,
Sgst,CreateDate,CreateUser,Igst,IpAddress,IsActive,IsDeleted)
VALUES(7,NEWID(),'Non GST','Non GST',0,0,'None',0,
0,GETDATE(),'Admin',0,'NA',1,0)

SET IDENTITY_INSERT dbo.TaxMaster OFF

---- Default Branch-------------
SET IDENTITY_INSERT dbo.Branch ON
IF NOT EXISTS(SELECT 1 FROM dbo.Branch em where Id=1)
INSERT INTO dbo.Branch(Id,RowId,BranchCode,BranchName,CityId,AreaId,CompId,CreateDate,CreateUser,
IsActive,IsDeleted,IpAddress) Values(1,NEWID(),'NA','NA',1,1,1,GETDATE(),'Admin',
1,0,'NA')
SET IDENTITY_INSERT dbo.Branch OFF

---- Default Division-------------
SET IDENTITY_INSERT dbo.Division ON
IF NOT EXISTS(SELECT 1 FROM dbo.Division em where Id=1)
INSERT INTO dbo.Division(Id,RowId,DivisionName,BranchId,CreateDate,CreateUser,
IsActive,IsDeleted,IpAddress) Values(1,NEWID(),'NA',1,GETDATE(),'Admin',
1,0,'NA')
SET IDENTITY_INSERT dbo.Division OFF

---- Default Store-------------
SET IDENTITY_INSERT dbo.Store ON
IF NOT EXISTS(SELECT 1 FROM dbo.Store em where Id=1)
INSERT INTO dbo.Store(
Id,StoreName,BranchId,Address,Extra1,Extra2,IsActive,
IsDeleted,CreateDate,CreateUser,IpAddress,RowId)
Values(1,'NA',1,'NA',NULL,NULL,1,0,GETDATE(),'Admin',
'NA',NEWID())
SET IDENTITY_INSERT dbo.Store OFF
 
--- default Product---------------------
SET IDENTITY_INSERT dbo.Product ON
IF NOT EXISTS(SELECT 1 FROM dbo.Product em where Id=1)
INSERT INTO dbo.Product(Id,RowId,ProductName,ProductCode,BarCode,HsnCode,ProductDesc,
TaxId,PTypeId,PurUomId,UomId,PurDisc,PurSpDisc,SaleDisc,SaleSpDisc,
ActualCost,StockReq,BatchReq,SerialReq,MinLevel,MaxLevel,
Rol,MinOrdQty,MaxOrdQty,LeadDays,CheckNegative,SaleRateTaxInc,
Sales,Purchase,Inventory,FixedAsset,WorkOrder,StdWt,
Price1,Price2,GroupId,SubGroupId,SizeId,CategoryId,StyleId,ColorId,BrandId,
IsActive,IsDeleted,CreateDate,CreateUser,IpAddress)
VALUES(1,NEWID(),'NA','NA','NA','NA','NA',
1,1,28,28,0,0,0,0,
0,'Yes','No','No',0,0,
0,0,0,0,0,0,
1,1,1,0,0,0,
0,0,1,1,1,1,1,1,1,
1,0,GETDATE(),'Admin','NA')
SET IDENTITY_INSERT dbo.Product OFF

SET IDENTITY_INSERT dbo.ProductPrice ON
IF NOT EXISTS(SELECT 1 FROM dbo.ProductPrice em where Id=1)
INSERT INTO ProductPrice(Id,ProductId,BatchNo,BranchId,CreateDate,CreateUser,
DealerPrice,IpAddress,IssueQty,Mrp,Qty,RowId,SaleRate,IsActive,IsDeleted)
VALUES(1,1,'NA',1,GETDATE(),'Admin',
0,'NA',0,0,0,NEWID(),0,1,0)
SET IDENTITY_INSERT dbo.ProductPrice OFF

SET IDENTITY_INSERT dbo.productBal ON
IF NOT EXISTS(SELECT 1 FROM dbo.productBal em where Id=1)
insert into ProductBal(Id,CompanyId,YearId,ProductId,GodownId,BranchId,
	OpNos,OpQty,IssueNo,IssueQty,RcptNos,
	RcptQty,BalNos ,BalQty ,Rate ,StockValue, 
    RowId) Values(1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,NEWID())
SET IDENTITY_INSERT dbo.productBal OFF

----Dept
--SET IDENTITY_INSERT dbo.Dept ON
--IF NOT EXISTS(SELECT 1 FROM dbo.Dept em where Id=1)
--INSERT Into dbo.Dept(Id,DeptName,IsActive,IsDeleted,CreateDate,CreateUser,
--IpAddress,RowId)
--Values(1,'NA',1,0,SYSDATETIME(),'Admin','NA',NEWID())
--SET IDENTITY_INSERT dbo.Dept OFF


--Grade
SET IDENTITY_INSERT dbo.Grade ON
IF NOT EXISTS(SELECT 1 FROM dbo.Grade em where Id=1)
INSERT Into dbo.Grade( 
Id,GradeName,IsActive,IsDeleted,CreateDate,CreateUser,
IpAddress,RowId,StartWt,EndWt,RateDiff)
Values(1,'NA',1,0,SYSDATETIME(),'Admin','NA',NEWID(),0,0,0)
SET IDENTITY_INSERT dbo.Grade OFF


----Nar
--SET IDENTITY_INSERT dbo.Nar ON
--IF NOT EXISTS(SELECT 1 FROM dbo.Nar em where Id=1)
--INSERT Into dbo.Nar( 
--Id,Narration,IsActive,IsDeleted,CreateDate,CreateUser,
--IpAddress,RowId)
--Values(1,'NA',1,0,SYSDATETIME(),'Admin','NA',NEWID())
--SET IDENTITY_INSERT dbo.Nar OFF


----MachineMaster
--SET IDENTITY_INSERT dbo.MachineMaster ON
--IF NOT EXISTS(SELECT 1 FROM dbo.MachineMaster em where Id=1)
--INSERT Into dbo.MachineMaster(
--Id,MachineName,CompanyID,DivId,IsActive,IsDeleted,CreateDate,CreateUser,
--IpAddress,RowId)
--Values(1,'NA',1,1,1,0,SYSDATETIME(),'Admin','NA',NEWID())
--SET IDENTITY_INSERT dbo.MachineMaster OFF

----PackingType
--SET IDENTITY_INSERT dbo.PackingType ON
--IF NOT EXISTS(SELECT 1 FROM dbo.PackingType em where Id=1)
--INSERT Into dbo.PackingType(
--Id,TypeName,IsActive,IsDeleted,CreateDate,CreateUser,
--IpAddress,RowId)
--Values(1,'NA',1,0,SYSDATETIME(),'Admin','NA',NEWID())
--SET IDENTITY_INSERT dbo.PackingType OFF

--pay terms
SET IDENTITY_INSERT dbo.PayTerms ON
IF NOT EXISTS(SELECT 1 FROM dbo.PayTerms em where Id=0)
INSERT Into dbo.PayTerms(Id,PayDescr,[Days],IsActive,IsDeleted,CreateDate,CreateUser,
IpAddress,RowId)
Values(0,'N/A',0,1,0,SYSDATETIME(),'Admin','NA',NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.PayTerms em where Id=1)
INSERT Into dbo.PayTerms(Id,PayDescr,[Days],IsActive,IsDeleted,CreateDate,CreateUser,
IpAddress,RowId)
Values(1,'COD',0,1,0,SYSDATETIME(),'Admin','NA',NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.PayTerms em where Id=2)
INSERT Into dbo.PayTerms(Id,PayDescr,[Days],IsActive,IsDeleted,CreateDate,CreateUser,
IpAddress,RowId)
Values(2,'Due On Receipt',0,1,0,SYSDATETIME(),'Admin','NA',NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.PayTerms em where Id=3)
INSERT Into dbo.PayTerms(Id,PayDescr,[Days],IsActive,IsDeleted,CreateDate,CreateUser,
IpAddress,RowId)
Values(3,'Net 7 Days',7,1,0,SYSDATETIME(),'Admin','NA',NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.PayTerms em where Id=4)
INSERT Into dbo.PayTerms(Id,PayDescr,[Days],IsActive,IsDeleted,CreateDate,CreateUser,
IpAddress,RowId)
Values(4,'Net 15 Days',15,1,0,SYSDATETIME(),'Admin','NA',NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.PayTerms em where Id=5)
INSERT Into dbo.PayTerms(Id,PayDescr,[Days],IsActive,IsDeleted,CreateDate,CreateUser,
IpAddress,RowId)
Values(5,'Net 30 Days',30,1,0,SYSDATETIME(),'Admin','NA',NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.PayTerms em where Id=6)
INSERT Into dbo.PayTerms(Id,PayDescr,[Days],IsActive,IsDeleted,CreateDate,CreateUser,
IpAddress,RowId)
Values(6,'Net 45 Days',45,1,0,SYSDATETIME(),'Admin','NA',NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.PayTerms em where Id=7)
INSERT Into dbo.PayTerms(Id,PayDescr,[Days],IsActive,IsDeleted,CreateDate,CreateUser,
IpAddress,RowId)
Values(7,'Net 60 Days',60,1,0,SYSDATETIME(),'Admin','NA',NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.PayTerms em where Id=8)
INSERT Into dbo.PayTerms(Id,PayDescr,[Days],IsActive,IsDeleted,CreateDate,CreateUser,
IpAddress,RowId)
Values(8,'Net 75 Days',30,1,0,SYSDATETIME(),'Admin','NA',NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.PayTerms em where Id=9)
INSERT Into dbo.PayTerms(Id,PayDescr,[Days],IsActive,IsDeleted,CreateDate,CreateUser,
IpAddress,RowId)
Values(9,'Net 90 Days',30,1,0,SYSDATETIME(),'Admin','NA',NEWID())

SET IDENTITY_INSERT dbo.PayTerms OFF

--IF NOT EXISTS(SELECT 1 FROM dbo.MiscList em where Id=0)
--INSERT INTO MiscList(Id,Descr,Category) Values(0,'Draft','PO')

--IF NOT EXISTS(SELECT 1 FROM dbo.MiscList em where Id=1)
--INSERT INTO MiscList(Id,Descr,Category) Values(1,'Ordered','PO')

--IF NOT EXISTS(SELECT 1 FROM dbo.MiscList em where Id=2)
--INSERT INTO MiscList(Id,Descr,Category) Values(2,'Partial Shipment','PO')

--IF NOT EXISTS(SELECT 1 FROM dbo.MiscList em where Id=3)
--INSERT INTO MiscList(Id,Descr,Category) Values(3,'Received','PO')

--IF NOT EXISTS(SELECT 1 FROM dbo.MiscList em where Id=4)
--INSERT INTO MiscList(Id,Descr,Category) Values(4,'Orders','SO')

--IF NOT EXISTS(SELECT 1 FROM dbo.MiscList em where Id=5)
--INSERT INTO MiscList(Id,Descr,Category) Values(5,'In Manufacturing','SO')

--IF NOT EXISTS(SELECT 1 FROM dbo.MiscList em where Id=6)
--INSERT INTO MiscList(Id,Descr,Category) Values(6,'Partial Shipped & Invoiced','So')

--IF NOT EXISTS(SELECT 1 FROM dbo.MiscList em where Id=7)
--INSERT INTO MiscList(Id,Descr,Category) Values(7,'Partial Shipped-Not Invoiced','SO')

--IF NOT EXISTS(SELECT 1 FROM dbo.MiscList em where Id=8)
--INSERT INTO MiscList(Id,Descr,Category) Values(8,'Closed-Shipped & Invoiced','SO')

--Set Process master by default 'NA'
SET IDENTITY_INSERT dbo.Process ON
if NOT exists (select 1 from Process em where em.Id=1)
BEGIN
INSERT INTO dbo.Process
(
   Id, ProcessName, HsnCode,Priority,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (1, 'NA','NA',0,1,0,GetDate(),'Admin',NEWID())
END
go

SET IDENTITY_INSERT dbo.Process OFF



--Insert value in sppara table
SET IDENTITY_INSERT dbo.SpPara on
GO

if NOT exists (select 1 from SpPara sp where sp.Id=1)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (1,'OrderDetailsReport','CompanyId', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=2)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (2,'OrderDetailsReport','ordStatus', 'varchar(100)','All')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=3)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (3,'OrderDetailsReport','VoucherID', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=4)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (4,'OrderDetailsReport','FromDate', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=5)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (5,'OrderDetailsReport','ToDate', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=6)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (6,'OrderDetailsReport','ReportId', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=7)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (7,'OrderDetailsReport','BranchId', 'int','1')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=8)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (8,'OrderDetailsReport','DivId', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=9)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (9,'OrderDetailsReport','PGroupId', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=10)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (10,'OrderDetailsReport','AgentId', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=11)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (11,'OrderDetailsReport','TransportId', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=12)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (12,'OrderDetailsReport','VoucherTypeID', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=13)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (13,'OrderDetailsReport','VTypeID', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=14)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (14,'OrderDetailsReport','ProductGroup', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=15)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (15,'OrderDetailsReport','Product', 'Varchar(1)','N')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=16)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (16,'OrderDetailsReport','Party', 'Varchar(1)','N')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=17)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (17,'OrderDetailsReport','Grade', 'Varchar(1)','N')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=18)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (18,'OrderDetailsReport','color', 'Varchar(1)','N')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=19)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (19,'OrderDetailsReport','Design', 'Varchar(1)','N')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=20)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (20,'OrderDetailsReport','IsPending', 'int','0')
END
go

--for challan 
if NOT exists (select 1 from SpPara sp where sp.Id=21)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (21,'Challan_Reg','fromdate', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=22)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (22,'Challan_Reg','todate', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=23)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (23,'Challan_Reg','companyid', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=24)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (24,'Challan_Reg','reportid', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=25)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (25,'Challan_Reg','party', 'varchar(1)','N')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=26)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (26,'Challan_Reg','agent', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=27)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (27,'Challan_Reg','city', 'varchar(1)','N')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=28)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (28,'Challan_Reg','area', 'varchar(1)','N')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=29)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (29,'Challan_Reg','partygroup', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=30)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (30,'Challan_Reg','item', 'varchar(1)','N')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=31)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (31,'Challan_Reg','color', 'varchar(1)','N')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=32)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (32,'Challan_Reg','design', 'varchar(1)','N')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=33)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (33,'Challan_Reg','voucherid', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=34)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (34,'Challan_Reg','unitid', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=35)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (35,'Challan_Reg','transid', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=36)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (36,'Challan_Reg','divid', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=37)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (37,'Challan_Reg','itemgroupid', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=38)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (38,'Challan_Reg','itemtype', 'varchar(1)','N')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=40)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (40,'Challan_Reg','challantype', 'int','0')
END
go


--for bill print
if NOT exists (select 1 from SpPara sp where sp.Id=41)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (41,'sales_Reg','fromdate', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=42)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (42,'sales_Reg','todate', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=43)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (43,'sales_Reg','companyid', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=44)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (44,'sales_Reg','reportid', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=45)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (45,'sales_Reg','party', 'varchar(1)','N')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=46)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (46,'sales_Reg','agent', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=47)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (47,'sales_reg','city', 'varchar(1)','N')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=48)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (48,'sales_reg','area', 'varchar(1)','N')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=49)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (49,'sales_reg','partygroup', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=50)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (50,'sales_reg','item', 'varchar(1)','N')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=51)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (51,'sales_reg','color', 'varchar(1)','N')
END
go
 

if NOT exists (select 1 from SpPara sp where sp.Id=52)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (52,'sales_reg','voucherid', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=53)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (53,'sales_reg','bookid', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=54)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (54,'sales_reg','transid', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=55)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (55,'sales_reg','branchid', 'int','0')
END
go
if NOT exists (select 1 from SpPara sp where sp.Id=56)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (56,'sales_reg','divid', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=57)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (57,'sales_reg','groupid', 'int','0')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=58)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (58,'sales_reg','book', 'varchar(1)','N')
END
go

if NOT exists (select 1 from SpPara sp where sp.Id=59)
BEGIN
INSERT INTO dbo.SpPara
(
  Id,SpName, ParaName,ParaType,DefaultValue
)values (59,'sales_reg','RCM', 'varchar(3)','NO')
END
go


SET IDENTITY_INSERT dbo.SpPara off
Go

-- Update prod table for current product id if null
update prod set cProductId = ProductId where Cproductid is null


---- Default Cost head-------------
SET IDENTITY_INSERT dbo.cost_heads ON
IF NOT EXISTS(SELECT 1 FROM dbo.cost_heads em where Id=1)
INSERT INTO dbo.cost_heads(Id,RowId,HeadName,BranchId,CreateDate,CreateUser,
IsActive,IsDeleted,IpAddress) Values(1,NEWID(),'NA',1,GETDATE(),'Admin',
1,0,'NA')
SET IDENTITY_INSERT dbo.cost_heads OFF