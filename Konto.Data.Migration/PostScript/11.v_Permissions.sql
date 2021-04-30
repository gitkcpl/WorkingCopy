SET IDENTITY_INSERT dbo.Permissions ON

-- Country -102
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=1)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(1,'None',NEWID(),102,'Master',0)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=2)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(2,'Country-Create',NEWID(),102,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=3)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(3,'Country-Modify',NEWID(),102,'Master',2)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=4)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(4,'Country-Delete',NEWID(),102,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=5)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(5,'Country-View',NEWID(),102,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=6)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(6,'Country-Export',NEWID(),102,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=7)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(7,'Country-Print',NEWID(),102,'Master',6)
END

---state-103

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=8)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(8,'State-Create',NEWID(),103,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=9)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(9,'State-Modify',NEWID(),103,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=10)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(10,'State-Delete',NEWID(),103,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=11)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(11,'State-View',NEWID(),103,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=12)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(12,'State-Export',NEWID(),103,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=13)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(13,'State-Print',NEWID(),103,'Master',6)
END

--- City 104


IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=14)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(14,'City-Create',NEWID(),104,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=15)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(15,'City-Modify',NEWID(),104,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=16)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(16,'City-Delete',NEWID(),104,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=17)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(17,'City-View',NEWID(),104,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=18)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(18,'City-Export',NEWID(),104,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=19)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(19,'City-Print',NEWID(),104,'Master',6)
END


-- area 105


IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=20)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(20,'Area-Create',NEWID(),105,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=21)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(21,'Area-Modify',NEWID(),105,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=22)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(22,'Area-Delete',NEWID(),105,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=23)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(23,'Area-View',NEWID(),105,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=24)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(24,'Area-Export',NEWID(),105,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=25)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(25,'Area-Print',NEWID(),105,'Master',6)
END


-- product Type 107


IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=26)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(26,'Product Type-Create',NEWID(),107,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=27)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(27,'Product Type-Modify',NEWID(),107,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=28)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(28,'Product Type-Delete',NEWID(),107,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=29)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(29,'Product Type-View',NEWID(),107,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=30)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(30,'Product Type-Export',NEWID(),107,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=31)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(31,'Product Type-Print',NEWID(),107,'Master',6)
END

-- Branch -1050
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=32)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(32,'Branch-Create',NEWID(),1050,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=33)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(33,'Branch-Modify',NEWID(),1050,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=34)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(34,'Branch-Delete',NEWID(),1050,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=35)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(35,'Branch-View',NEWID(),1050,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=36)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(36,'Branch-Export',NEWID(),1050,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=37)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(37,'Branch-Print',NEWID(),1050,'Master',6)
END

-- Division -1051
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=38)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(38,'Division-Create',NEWID(),1051,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=39)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(39,'Division-Modify',NEWID(),1051,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=40)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(40,'Division-Delete',NEWID(),1051,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=41)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(41,'Division-View',NEWID(),1051,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=42)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(42,'Division-Export',NEWID(),1051,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=43)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(43,'Division-Print',NEWID(),1051,'Master',6)
END

-- Brand -108
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=56)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(56,'Brand-Create',NEWID(),108,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=57)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(57,'Brand-Modify',NEWID(),108,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=58)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(58,'Brand-Delete',NEWID(),108,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=59)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(59,'Brand-View',NEWID(),108,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=60)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(60,'Brand-Export',NEWID(),108,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=61)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(61,'Brand-Print',NEWID(),108,'Master',6)
END

-- Unit - 109
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=62)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(62,'Unit-Create',NEWID(),109,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=63)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(63,'Unit-Modify',NEWID(),109,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=64)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(64,'Unit-Delete',NEWID(),109,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=65)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(65,'Unit-View',NEWID(),109,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=66)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(66,'Unit-Export',NEWID(),109,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=67)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(67,'Unit-Print',NEWID(),109,'Master',6)
END


--Product Group -110
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=68)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(68,'Group-Create',NEWID(),110,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=69)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(69,'Group-Modify',NEWID(),110,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=70)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(70,'Group-Delete',NEWID(),110,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=71)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(71,'Group-View',NEWID(),110,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=72)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(72,'Group-Export',NEWID(),110,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=73)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(73,'Group-Print',NEWID(),110,'Master',6)
END

--Sub Group -111
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=74)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(74,'Sub Group-Create',NEWID(),111,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=75)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(75,'Sub Group-Modify',NEWID(),111,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=76)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(76,'Sub Group-Delete',NEWID(),111,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=77)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(77,'Sub Group-View',NEWID(),111,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=78)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(78,'Sub Group-Export',NEWID(),111,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=79)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(79,'Sub Group-Print',NEWID(),111,'Master',6)
END


--Size Master-112
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=80)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(80,'Size Master-Create',NEWID(),112,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=81)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(81,'Size Master-Modify',NEWID(),112,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=82)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(82,'Size Master-Delete',NEWID(),112,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=83)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(83,'Size Master-View',NEWID(),112,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=84)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(84,'Size Master-Export',NEWID(),112,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=85)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(85,'Size Master-Print',NEWID(),112,'Master',6)
END


--Color Master -113
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=86)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(86,'Color Master-Create',NEWID(),113,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=87)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(87,'Color Master-Modify',NEWID(),113,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=88)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(88,'Color Master-Delete',NEWID(),113,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=89)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(89,'Color Master-View',NEWID(),113,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=90)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(90,'Color Master-Export',NEWID(),113,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=91)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(91,'Color Master-Print',NEWID(),113,'Master',6)
END



--Category Master-114
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=92)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(92,'Category Master-Create',NEWID(),114,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=93)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(93,'Category Master-Modify',NEWID(),114,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=94)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(94,'Category Master-Delete',NEWID(),114,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=95)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(95,'Category Master-View',NEWID(),114,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=96)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(96,'Category Master-Export',NEWID(),114,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=97)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(97,'Category Master-Print',NEWID(),114,'Master',6)
END


--Style Master -115
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=98)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(98,'Style Master-Create',NEWID(),115,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=99)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(99,'Style Master-Modify',NEWID(),115,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=100)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(100,'Style Master-Delete',NEWID(),115,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=101)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(101,'Style Master-View',NEWID(),115,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=102)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(102,'Style Master-Export',NEWID(),115,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=103)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(103,'Style Master-Print',NEWID(),115,'Master',6)
END

--GST Slab -117
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=104)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(104,'GST Slab-Create',NEWID(),117,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=105)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(105,'GST Slab-Modify',NEWID(),117,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=106)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(106,'GST Slab-Delete',NEWID(),117,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=107)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(107,'GST Slab-View',NEWID(),117,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=108)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(108,'GST Slab-Export',NEWID(),117,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=109)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(109,'GST Slab-Print',NEWID(),117,'Master',6)
END


--Product -118
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=110)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(110,'Product-Create',NEWID(),118,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=111)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(111,'Product-Modify',NEWID(),118,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=112)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(112,'Product-Delete',NEWID(),118,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=113)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(113,'Product-View',NEWID(),118,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=114)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(114,'Product-Export',NEWID(),118,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=115)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(115,'Product-Print',NEWID(),118,'Master',6)
END

--SalesMan -120
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=116)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(116,'SalesMan-Create',NEWID(),120,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=117)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(117,'SalesMan-Modify',NEWID(),120,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=118)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(118,'SalesMan-Delete',NEWID(),120,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=119)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(119,'SalesMan-View',NEWID(),120,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=120)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(120,'SalesMan-Export',NEWID(),120,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=121)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(121,'SalesMan-Print',NEWID(),120,'Master',6)
END

--Ledger Group -122
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=122)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(122,'Ledger Group-Create',NEWID(),122,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=123)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(123,'Ledger Group-Modify',NEWID(),122,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=124)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(124,'Ledger Group-Delete',NEWID(),122,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=125)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(125,'Ledger Group-View',NEWID(),122,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=126)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(126,'Ledger Group-Export',NEWID(),122,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=127)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(127,'Ledger Group-Print',NEWID(),122,'Master',6)
END


--Party Group -123
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=128)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(128,'Party Group-Create',NEWID(),123,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=129)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(129,'Party Group-Modify',NEWID(),123,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=130)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(130,'Party Group-Delete',NEWID(),123,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=131)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(131,'Party Group-View',NEWID(),123,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=132)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(132,'Party Group-Export',NEWID(),123,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=133)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(133,'Ledger Group-Print',NEWID(),123,'Master',6)
END

--Account Master-124
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=134)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(134,'Account Master-Create',NEWID(),124,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=135)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(135,'Account Master-Modify',NEWID(),124,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=136)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(136,'Account Master-Delete',NEWID(),124,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=137)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(137,'Account Master-View',NEWID(),124,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=138)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(138,'Account Master-Export',NEWID(),124,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=139)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(139,'Account Master-Print',NEWID(),124,'Master',6)
END


--Voucher Type-126
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=140)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(140,'Voucher Type-Create',NEWID(),126,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=141)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(141,'Voucher Type-Modify',NEWID(),126,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=142)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(142,'Voucher Type-Delete',NEWID(),126,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=143)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(143,'Voucher Type-View',NEWID(),126,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=144)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(144,'Voucher Type-Export',NEWID(),126,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=145)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(145,'Voucher Type-Print',NEWID(),126,'Master',6)
END


-- Voucher -127
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=50)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(50,'Voucher-Create',NEWID(),127,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=51)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(51,'Voucher-Modify',NEWID(),127,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=52)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(52,'Voucher-Delete',NEWID(),127,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=53)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(53,'Voucher-View',NEWID(),127,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=54)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(54,'Voucher-Export',NEWID(),127,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=55)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(55,'Voucher-Print',NEWID(),127,'Master',6)
END


-- Narration -129
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=44)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(44,'Narration-Create',NEWID(),129,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=45)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(45,'Narration-Modify',NEWID(),129,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=46)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(46,'Narration-Delete',NEWID(),129,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=47)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(47,'Narration-View',NEWID(),129,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=48)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(48,'Narration-Export',NEWID(),129,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=49)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(49,'Narration-Print',NEWID(),129,'Master',6)
END


--Store - 130

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=158)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(158,'Store-Create',NEWID(),130,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=159)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(159,'Store-Modify',NEWID(),130,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=160)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(160,'Store-Delete',NEWID(),130,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=161)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(161,'Store-View',NEWID(),130,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=162)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(162,'Store-Export',NEWID(),130,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=163)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(163,'Store-Print',NEWID(),130,'Master',6)
END

--Haste - 131

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=164)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(164,'Haste-Create',NEWID(),131,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=165)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(165,'Haste-Modify',NEWID(),131,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=166)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(166,'Haste-Delete',NEWID(),131,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=167)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(167,'Haste-View',NEWID(),131,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=168)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(168,'Haste-Export',NEWID(),131,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=169)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(169,'Haste-Print',NEWID(),131,'Master',6)
END

--DesignMaster - 132

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=170)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(170,'DesignMaster-Create',NEWID(),132,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=171)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(171,'DesignMaster-Modify',NEWID(),132,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=172)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(172,'DesignMaster-Delete',NEWID(),132,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=173)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(173,'DesignMaster-View',NEWID(),132,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=174)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(174,'DesignMaster-Export',NEWID(),132,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=175)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(175,'DesignMaster-Print',NEWID(),132,'Master',6)
END


--Catalog - 133

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=176)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(176,'Catalog-Create',NEWID(),133,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=177)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(177,'Catalog-Modify',NEWID(),133,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=178)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(178,'Catalog-Delete',NEWID(),133,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=179)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(179,'Catalog-View',NEWID(),133,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=180)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(180,'Catalog-Export',NEWID(),133,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=181)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(181,'Catalog-Print',NEWID(),133,'Master',6)
END



--MachineMaster - 134

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=182)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(182,'MachineMaster-Create',NEWID(),134,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=183)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(183,'MachineMaster-Modify',NEWID(),134,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=184)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(184,'MachineMaster-Delete',NEWID(),134,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=185)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(185,'MachineMaster-View',NEWID(),134,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=186)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(186,'MachineMaster-Export',NEWID(),134,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=187)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(187,'MachineMaster-Print',NEWID(),134,'Master',6)
END


--GradeMaster - 135

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=188)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(188,'GradeMaster-Create',NEWID(),135,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=189)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(189,'GradeMaster-Modify',NEWID(),135,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=190)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(190,'GradeMaster-Delete',NEWID(),135,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=191)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(191,'GradeMaster-View',NEWID(),135,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=192)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(192,'GradeMaster-Export',NEWID(),135,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=193)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(193,'GradeMaster-Print',NEWID(),135,'Master',6)
END


--PackingType - 136

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=194)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(194,'PackingType-Create',NEWID(),136,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=195)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(195,'PackingType-Modify',NEWID(),136,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=196)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(196,'PackingType-Delete',NEWID(),136,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=197)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(197,'PackingType-View',NEWID(),136,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=198)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(198,'PackingType-Export',NEWID(),136,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=199)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(199,'PackingType-Print',NEWID(),136,'Master',6)
END

 
--Sale Purchase Opening Bill -139

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=399)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(399,'OpBill-Create',NEWID(),139,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=400)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(400,'OpBill-Modify',NEWID(),139,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=401)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(401,'OpBill-Delete',NEWID(),139,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=402)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(402,'OpBill-View',NEWID(),139,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=403)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(403,'OpBill-Export',NEWID(),139,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=404)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(404,'OpBill-Print',NEWID(),139,'Master',6)
END

--Opening Account Balance 142

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=393)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(393,'OpBalance-Create',NEWID(),142,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=394)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(394,'OpBalance-Modify',NEWID(),142,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=395)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(395,'OpBalance-Delete',NEWID(),142,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=396)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(396,'OpBalance-View',NEWID(),142,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=397)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(397,'OpBalance-Export',NEWID(),142,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=398)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(398,'OpBalance-Print',NEWID(),142,'Master',6)
END

--Opening Grey Stock -143 

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=405)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(405,'OpGreyStock-Create',NEWID(),143,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=406)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(406,'OpGreyStock-Modify',NEWID(),143,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=407)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(407,'OpGreyStock-Delete',NEWID(),143,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=408)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(408,'OpGreyStock-View',NEWID(),143,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=409)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(409,'OpGreyStock-Export',NEWID(),143,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=410)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(410,'OpGreyStock-Print',NEWID(),143,'Master',6)
END

--Opening Finish Stock - 144

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=411)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(411,'OpFinishStock-Create',NEWID(),144,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=412)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(412,'OpFinishStock-Modify',NEWID(),144,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=413)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(413,'OpFinishStock-Delete',NEWID(),144,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=414)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(414,'OpFinishStock-View',NEWID(),144,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=415)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(415,'OpFinishStock-Export',NEWID(),144,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=416)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(416,'OpFinishStock-Print',NEWID(),144,'Master',6)
END

--Opening Mill Stock -145

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=417)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(417,'OpMillStock-Create',NEWID(),145,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=418)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(418,'OpMillStock-Modify',NEWID(),145,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=419)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(419,'OpMillStock-Delete',NEWID(),145,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=420)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(420,'OpMillStock-View',NEWID(),145,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=421)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(421,'OpMillStock-Export',NEWID(),145,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=422)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(422,'OpMillStock-Print',NEWID(),145,'Master',6)
END

--Opening Job Issue -146

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=423)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(423,'OpJobStock-Create',NEWID(),146,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=424)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(424,'OpJobStock-Modify',NEWID(),146,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=425)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(425,'OpJobStock-Delete',NEWID(),146,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=426)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(426,'OpJobStock-View',NEWID(),146,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=427)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(427,'OpJobStock-Export',NEWID(),146,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=428)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(428,'OpJobStock-Print',NEWID(),146,'Master',6)
END

--Warp item -153

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=429)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(429,'WarpItem-Create',NEWID(),153,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=430)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(430,'WarpItem-Modify',NEWID(),153,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=431)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(431,'WarpItem-Delete',NEWID(),153,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=432)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(432,'Warpitem-View',NEWID(),153,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=433)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(433,'WarpItem-Export',NEWID(),153,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=434)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(434,'WarpItem-Print',NEWID(),153,'Master',6)
END


--Purchase Order-306
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=146)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(146,'Purchase Order-Create',NEWID(),306,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=147)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(147,'Purchase Order-Modify',NEWID(),306,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=148)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(148,'Purchase Order-Delete',NEWID(),306,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=149)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(149,'Purchase Order-View',NEWID(),306,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=150)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(150,'Purchase Order-Export',NEWID(),306,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=151)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(151,'Purchase Order-Print',NEWID(),306,'Transaction',6)
END
 
 -- Po Approval - 307

 IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=435)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(435,'PoApproval-Create',NEWID(),307,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=436)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(436,'PoApproval-Modify',NEWID(),307,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=437)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(437,'PoApproval-Delete',NEWID(),307,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=438)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(438,'PoApproval-View',NEWID(),307,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=439)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(439,'PoApproval-Export',NEWID(),307,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=440)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(440,'PoApproval-Print',NEWID(),307,'Transaction',6)
END

--Inward - 308

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=212)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(212,'Inward-Create',NEWID(),308,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=213)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(213,'Inward-Modify',NEWID(),308,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=214)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(214,'Inward-Delete',NEWID(),308,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=215)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(215,'Inward-View',NEWID(),308,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=216)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(216,'Inward-Export',NEWID(),308,'Transaction',5)
END


IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=217)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(217,'Inward-Print',NEWID(),308,'Transaction',6)
END


--PurchaseBill - 309

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=218)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(218,'PurchaseBill-Create',NEWID(),309,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=219)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(219,'PurchaseBill-Modify',NEWID(),309,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=220)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(220,'PurchaseBill-Delete',NEWID(),309,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=221)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(221,'PurchaseBill-View',NEWID(),309,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=222)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(222,'PurchaseBill-Export',NEWID(),309,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=223)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(223,'PurchaseBill-Print',NEWID(),309,'Transaction',6)
END


--MillIssue - 310

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=224)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(224,'MillIssue-Create',NEWID(),310,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=225)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(225,'MillIssue-Modify',NEWID(),310,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=226)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(226,'MillIssue-Delete',NEWID(),310,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=227)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(227,'MillIssue-View',NEWID(),310,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=228)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(228,'MillIssue-Export',NEWID(),310,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=229)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(229,'MillIssue-Print',NEWID(),310,'Transaction',6)
END


--Cutting/ Folding - 317

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=230)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(230,'Cutting-Create',NEWID(),317,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=231)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(231,'Cutting-Modify',NEWID(),317,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=232)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(232,'Cutting-Delete',NEWID(),317,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=233)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(233,'Cutting-View',NEWID(),317,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=234)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(234,'Cutting-Export',NEWID(),317,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=235)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(235,'Cutting-Print',NEWID(),317,'Transaction',6)
END


--JobReceipt - 319

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=236)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(236,'JobReceipt-Create',NEWID(),319,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=237)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(237,'JobReceipt-Modify',NEWID(),319,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=238)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(238,'JobReceipt-Delete',NEWID(),319,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=239)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(239,'JobReceipt-View',NEWID(),319,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=240)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(240,'JobReceipt-Export',NEWID(),319,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=241)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(241,'JobReceipt-Print',NEWID(),319,'Transaction',6)
END



--JournalVoucher - 321

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=242)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(242,'JournalVoucher-Create',NEWID(),321,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=243)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(243,'JournalVoucher-Modify',NEWID(),321,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=244)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(244,'JournalVoucher-Delete',NEWID(),321,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=245)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(245,'JournalVoucher-View',NEWID(),321,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=246)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(246,'JournalVoucher-Export',NEWID(),321,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=247)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(247,'JournalVoucher-Print',NEWID(),321,'Transaction',6)
END



--ReceiptVoucher - 322

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=248)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(248,'ReceiptVoucher-Create',NEWID(),322,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=249)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(249,'ReceiptVoucher-Modify',NEWID(),322,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=250)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(250,'ReceiptVoucher-Delete',NEWID(),322,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=251)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(251,'ReceiptVoucher-View',NEWID(),322,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=252)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(252,'ReceiptVoucher-Export',NEWID(),322,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=253)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(253,'ReceiptVoucher-Print',NEWID(),322,'Transaction',6)
END



--GeneralExpense - 324

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=254)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(254,'GeneralExpense-Create',NEWID(),324,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=255)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(255,'GeneralExpense-Modify',NEWID(),324,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=256)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(256,'GeneralExpense-Delete',NEWID(),324,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=257)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(257,'GeneralExpense-View',NEWID(),324,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=258)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(258,'GeneralExpense-Export',NEWID(),324,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=259)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(259,'GeneralExpense-Print',NEWID(),324,'Transaction',6)
END


--CreditNote - 325

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=260)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(260,'CreditNote-Create',NEWID(),325,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=261)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(261,'CreditNote-Modify',NEWID(),325,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=262)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(262,'CreditNote-Delete',NEWID(),325,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=263)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(263,'CreditNote-View',NEWID(),325,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=264)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(264,'CreditNote-Export',NEWID(),325,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=265)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(265,'CreditNote-Print',NEWID(),325,'Transaction',6)
END


--DebitNote - 326

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=266)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(266,'DebitNote-Create',NEWID(),326,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=267)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(267,'DebitNote-Modify',NEWID(),326,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=268)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(268,'DebitNote-Delete',NEWID(),326,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=269)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(269,'DebitNote-View',NEWID(),326,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=270)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(270,'DebitNote-Export',NEWID(),326,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=271)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(271,'DebitNote-Print',NEWID(),326,'Transaction',6)
END


--PaymentVoucher - 328

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=272)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(272,'PaymentVoucher-Create',NEWID(),328,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=273)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(273,'PaymentVoucher-Modify',NEWID(),328,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=274)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(274,'PaymentVoucher-Delete',NEWID(),328,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=275)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(275,'PaymentVoucher-View',NEWID(),328,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=276)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(276,'PaymentVoucher-Export',NEWID(),328,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=277)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(277,'PaymentVoucher-Print',NEWID(),328,'Transaction',6)
END

--Outward Job challan - 335

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=386)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(386,'OutwardJobChallan-Create',NEWID(),335,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=387)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(387,'OutwardJobChallan-Modify',NEWID(),335,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=388)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(388,'OutwardJobChallan-Delete',NEWID(),335,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=389)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(389,'OutwardJobChallan-View',NEWID(),335,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=390)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(390,'OutwardJobChallan-Export',NEWID(),335,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=391)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(391,'OutwardJobChallan-Print',NEWID(),335,'Transaction',6)
END

--Sales Order-352

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=152)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(152,'Sales Order-Create',NEWID(),352,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=153)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(153,'Sales Order-Modify',NEWID(),352,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=154)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(154,'Sales Order-Delete',NEWID(),352,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=155)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(155,'Sales Order-View',NEWID(),352,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=156)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(156,'Sales Order-Export',NEWID(),352,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=157)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(157,'Sales Order-Print',NEWID(),352,'Transaction',6)
END

--So Approval -353

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=441)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(441,'SoApproval-Create',NEWID(),353,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=442)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(442,'SoApproval-Modify',NEWID(),353,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=443)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(443,'SoApproval-Delete',NEWID(),353,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=444)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(444,'SoApproval-View',NEWID(),353,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=445)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(445,'SoApproval-Export',NEWID(),353,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=446)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(446,'SoApproval-Print',NEWID(),353,'Transaction',6)
END

--Outward - 355

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=278)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(278,'Outward-Create',NEWID(),355,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=279)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(279,'Outward-Modify',NEWID(),355,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=280)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(280,'Outward-Delete',NEWID(),355,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=281)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(281,'Outward-View',NEWID(),355,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=282)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(282,'Outward-Export',NEWID(),355,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=283)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(283,'Outward-Print',NEWID(),355,'Transaction',6)
END

--MillReceipt - 356

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=284)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(284,'MillReceipt-Create',NEWID(),356,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=285)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(285,'MillReceipt-Modify',NEWID(),356,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=286)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(286,'MillReceipt-Delete',NEWID(),356,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=287)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(287,'MillReceipt-View',NEWID(),356,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=288)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(288,'MillReceipt-Export',NEWID(),356,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=289)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(289,'MillReceipt-Print',NEWID(),356,'Transaction',6)
END


Update dbo.Permissions set ModuleId=377 where ModuleId=356

--SaleInvoice - 358

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=290)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(290,'SaleInvoice-Create',NEWID(),358,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=291)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(291,'SaleInvoice-Modify',NEWID(),358,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=292)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(292,'SaleInvoice-Delete',NEWID(),358,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=293)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(293,'SaleInvoice-View',NEWID(),358,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=294)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(294,'SaleInvoice-Export',NEWID(),358,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=295)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(295,'SaleInvoice-Print',NEWID(),358,'Transaction',6)
END


--JobIssue - 359

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=296)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(296,'JobIssue-Create',NEWID(),359,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=297)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(297,'JobIssue-Modify',NEWID(),359,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=298)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(298,'JobIssue-Delete',NEWID(),359,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=299)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(299,'JobIssue-View',NEWID(),359,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=300)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(300,'JobIssue-Export',NEWID(),359,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=301)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(301,'JobIssue-Print',NEWID(),359,'Transaction',6)
END


--JobBill - 360

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=302)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(302,'JobBill-Create',NEWID(),360,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=303)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(303,'JobBill-Modify',NEWID(),360,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=304)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(304,'JobBill-Delete',NEWID(),360,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=305)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(305,'JobBill-View',NEWID(),360,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=306)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(306,'JobBill-Export',NEWID(),360,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=307)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(307,'JobBill-Print',NEWID(),360,'Transaction',6)
END



--MillReturn - 361

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=308)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(308,'MillReturn-Create',NEWID(),361,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=309)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(309,'MillReturn-Modify',NEWID(),361,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=310)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(310,'MillReturn-Delete',NEWID(),361,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=311)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(311,'MillReturn-View',NEWID(),361,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=312)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(312,'MillReturn-Export',NEWID(),361,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=313)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(313,'MillReturn-Print',NEWID(),361,'Transaction',6)
END


--SalesReturn - 362

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=314)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(314,'SalesReturn-Create',NEWID(),362,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=315)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(315,'SalesReturn-Modify',NEWID(),362,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=316)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(316,'SalesReturn-Delete',NEWID(),362,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=317)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(317,'SalesReturn-View',NEWID(),362,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=318)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(318,'SalesReturn-Export',NEWID(),362,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=319)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(319,'SalesReturn-Print',NEWID(),362,'Transaction',6)
END


--PurchaseReturn - 363

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=320)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(320,'PurchaseReturn-Create',NEWID(),363,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=321)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(321,'PurchaseReturn-Modify',NEWID(),363,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=322)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(322,'PurchaseReturn-Delete',NEWID(),363,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=323)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(323,'PurchaseReturn-View',NEWID(),363,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=324)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(324,'PurchaseReturn-Export',NEWID(),363,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=325)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(325,'PurchaseReturn-Print',NEWID(),363,'Transaction',6)
END

--StoreIssue - 364

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=326)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(326,'StoreIssue-Create',NEWID(),364,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=327)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(327,'StoreIssue-Modify',NEWID(),364,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=328)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(328,'StoreIssue-Delete',NEWID(),364,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=329)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(329,'StoreIssue-View',NEWID(),364,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=330)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(330,'StoreIssue-Export',NEWID(),364,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=331)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(331,'StoreIssue-Print',NEWID(),364,'Transaction',6)
END


--DesignMapping - 365

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=332)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(332,'DesignMapping-Create',NEWID(),365,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=333)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(333,'DesignMapping-Modify',NEWID(),365,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=334)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(334,'DesignMapping-Delete',NEWID(),365,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=335)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(335,'DesignMapping-View',NEWID(),365,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=336)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(336,'DesignMapping-Export',NEWID(),365,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=337)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(337,'DesignMapping-Print',NEWID(),365,'Transaction',6)
END


--RefBank - 366

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=338)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(338,'RefBank-Create',NEWID(),366,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=339)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(339,'RefBank-Modify',NEWID(),366,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=340)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(340,'RefBank-Delete',NEWID(),366,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=341)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(341,'RefBank-View',NEWID(),366,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=342)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(342,'RefBank-Export',NEWID(),366,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=343)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(343,'RefBank-Print',NEWID(),366,'Master',6)
END


--BeamProduction - 367

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=344)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(344,'BeamProduction-Create',NEWID(),367,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=345)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(345,'BeamProduction-Modify',NEWID(),367,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=346)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(346,'BeamProduction-Delete',NEWID(),367,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=347)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(347,'BeamProduction-View',NEWID(),367,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=348)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(348,'BeamProduction-Export',NEWID(),367,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=349)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(349,'BeamProduction-Print',NEWID(),367,'Transaction',6)
END


--BeamLoading - 368

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=350)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(350,'BeamLoading-Create',NEWID(),368,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=351)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(351,'BeamLoading-Modify',NEWID(),368,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=352)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(352,'BeamLoading-Delete',NEWID(),368,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=353)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(353,'BeamLoading-View',NEWID(),368,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=354)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(354,'BeamLoading-Export',NEWID(),368,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=355)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(355,'BeamLoading-Print',NEWID(),368,'Transaction',6)
END


--TakaProduction - 369

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=356)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(356,'TakaProduction-Create',NEWID(),369,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=357)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(357,'TakaProduction-Modify',NEWID(),369,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=358)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(358,'TakaProduction-Delete',NEWID(),369,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=359)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(359,'TakaProduction-View',NEWID(),369,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=360)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(360,'TakaProduction-Export',NEWID(),369,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=361)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(361,'TakaProduction-Print',NEWID(),369,'Transaction',6)
END


--BatchMaster - 370

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=362)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(362,'BatchMaster-Create',NEWID(),370,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=363)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(363,'BatchMaster-Modify',NEWID(),370,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=364)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(364,'BatchMaster-Delete',NEWID(),370,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=365)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(365,'BatchMaster-View',NEWID(),370,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=366)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(366,'BatchMaster-Export',NEWID(),370,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=367)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(367,'BatchMaster-Print',NEWID(),370,'Transaction',6)
END


--YarnProduction - 371

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=368)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(368,'YarnProduction-Create',NEWID(),371,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=369)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(369,'YarnProduction-Modify',NEWID(),371,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=370)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(370,'YarnProduction-Delete',NEWID(),371,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=371)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(371,'YarnProduction-View',NEWID(),371,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=372)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(372,'YarnProduction-Export',NEWID(),371,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=373)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(373,'YarnProduction-Print',NEWID(),371,'Transaction',6)
END

-- BRS - 376


IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=515)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(515,'Bank_Reconcile-View',NEWID(),376,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=516)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(516,'Bank_Reconcile-Export',NEWID(),376,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=517)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(517,'Bank_Reconcile-Print',NEWID(),376,'Transaction',6)
END


-- Account Ledger -801

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=448)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(448,'AccountLedger-View',NEWID(),801,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=449)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(449,'AccountLedger-Export',NEWID(),801,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=450)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(450,'AccountLedger-Print',NEWID(),801,'Report',6)
END

--Outstanding Report - 802

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=458)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(458,'Outstanding-View',NEWID(),802,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=459)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(459,'Outstanding-Export',NEWID(),802,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=460)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(460,'Outstanding-Print',NEWID(),802,'Report',6)
END

--Trial Balance - 803

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=461)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(461,'TrialBalance-View',NEWID(),803,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=462)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(462,'TrialBalance-Export',NEWID(),803,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=463)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(463,'TrialBalance-Print',NEWID(),803,'Report',6)
END

--Balance Sheet - 804

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=464)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(464,'BalanceSheet-View',NEWID(),804,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=465)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(465,'BalanceSheet-Export',NEWID(),804,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=466)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(466,'BalanceSheet-Print',NEWID(),804,'Report',6)
END


--GRN Report-- 806

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=470)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(470,'GRN_Report-View',NEWID(),806,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=471)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(471,'GRN_Report-Export',NEWID(),806,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=472)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(472,'GRN_Report-Print',NEWID(),806,'Report',6)
END

--Purchase Bill Report - 807

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=473)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(473,'Purchase_Report-View',NEWID(),807,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=474)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(474,'Purchase_Report-Export',NEWID(),807,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=475)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(475,'Purchase_Report-Print',NEWID(),807,'Report',6)
END

--Grey Purchse -808

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=476)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(476,'GreyPur_Report-View',NEWID(),808,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=477)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(477,'GreyPur_Report-Export',NEWID(),808,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=478)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(478,'GreyPur_Report-Print',NEWID(),808,'Report',6)
END

--Grey Purchase Return -809

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=479)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(479,'GreyPurchaseRet_Report-View',NEWID(),809,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=480)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(480,'GreyPurchaseRet_Report-Export',NEWID(),809,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=481)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(481,'GreyPurchaseRet_Report-Print',NEWID(),809,'Report',6)
END

--Purchase Return -811

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=482)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(482,'Purchase_Return-View',NEWID(),811,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=483)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(483,'Purchase_Return-Export',NEWID(),811,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=484)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(484,'Purchase_Return-Print',NEWID(),811,'Report',6)
END


--Sales Challan -813

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=485)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(485,'Sales_Challan-View',NEWID(),813,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=486)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(486,'Sales_Challan-Export',NEWID(),813,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=487)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(487,'Sales_Challan-Print',NEWID(),813,'Report',6)
END


-- Sales -814

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=488)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(488,'Sales-View',NEWID(),814,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=489)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(489,'Sales-Export',NEWID(),814,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=490)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(490,'Sales-Print',NEWID(),814,'Report',6)
END

--Sales Return --816

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=491)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(491,'Sales_Return-View',NEWID(),816,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=492)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(492,'Sales_Return-Export',NEWID(),816,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=493)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(493,'Sales_Return-Print',NEWID(),816,'Report',6)
END


--Grey Issue --818

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=494)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(494,'Grey_Issue-View',NEWID(),818,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=495)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(495,'Grey_Issue-Export',NEWID(),818,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=496)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(496,'Grey_Issue-Print',NEWID(),818,'Report',6)
END


--Mill Receipt - 819

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=497)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(497,'MillRec_Challan-View',NEWID(),819,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=498)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(498,'MillRec_Challan-Export',NEWID(),819,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=499)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(499,'MillRec_Challan-Print',NEWID(),819,'Report',6)
END

--Mill Receipt Invoice --820

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=500)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(500,'MillRec_Invoice-View',NEWID(),820,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=501)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(501,'MillRec_Invoice-Export',NEWID(),820,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=502)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(502,'MillRec_Invoice-Print',NEWID(),820,'Report',6)
END

--Mill Return -821

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=503)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(503,'Mill_Return-View',NEWID(),821,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=504)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(504,'Mill_Return-Export',NEWID(),821,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=505)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(505,'Mill_Return-Print',NEWID(),821,'Report',6)
END


--Job Issue -823

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=506)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(506,'Job_Issue-View',NEWID(),823,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=507)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(507,'Job_Issue-Export',NEWID(),823,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=508)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(508,'Job_Issue-Print',NEWID(),823,'Report',6)
END

--Job Receipt Challan -824

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=509)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(509,'JobReceipt_Challan-View',NEWID(),824,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=510)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(510,'JobReceipt_Challan-Export',NEWID(),824,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=511)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(511,'JobReceipt_Challan-Print',NEWID(),824,'Report',6)
END

--Job Receipt Invoice --825

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=512)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(512,'JobReceipt_Invoice-View',NEWID(),825,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=513)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(513,'JobReceipt_Invoice-Export',NEWID(),825,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=514)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(514,'JobReceipt_Invoice-Print',NEWID(),825,'Report',6)
END

--Cr/Dr Note -827

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=524)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(524,'Cr/Dr_Note-View',NEWID(),827,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=525)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(525,'Cr/Dr_Note-Export',NEWID(),827,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=526)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(526,'Cr/Dr_Note-Print',NEWID(),827,'Report',6)
END

--Order Register -828

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=527)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(527,'Order_Register-View',NEWID(),828,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=528)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(528,'Order_Register-Export',NEWID(),828,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=529)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(529,'Order_Register-Print',NEWID(),828,'Report',6)
END

--Gstr1 -830

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=531)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(531,'Gstr1-View',NEWID(),830,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=532)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(532,'Gstr1-Export',NEWID(),830,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=533)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(533,'Gstr1-Print',NEWID(),828,'Report',6)
END

--Gstr2 -831

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=534)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(534,'Gstr2-View',NEWID(),831,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=535)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(535,'Gstr2-Export',NEWID(),831,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=536)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(536,'Gstr2-Print',NEWID(),831,'Report',6)
END

--Gstr 3B -832

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=537)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(537,'Gstr_3B-View',NEWID(),832,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=538)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(538,'Gstr_3B-Export',NEWID(),832,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=539)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(539,'Gstr_3B-Print',NEWID(),832,'Report',6)
END

--Gstr 4A -833

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=540)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(540,'Gstr_4A-View',NEWID(),833,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=541)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(541,'Gstr_4A-Export',NEWID(),833,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=542)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(542,'Gstr_4A-Print',NEWID(),833,'Report',6)
END

--Gstr2 Reconcile -834

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=543)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(543,'Gstr2_Reconcile-View',NEWID(),834,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=544)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(544,'Gstr2_Reconcile-Export',NEWID(),834,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=545)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(545,'Gstr2_Reconcile-Print',NEWID(),834,'Report',6)
END

--Tds -836

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=547)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(547,'Tds-View',NEWID(),836,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=548)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(548,'Tds-Export',NEWID(),836,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=549)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(549,'Tds-Print',NEWID(),836,'Report',6)
END

--Tcs -837

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=550)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(550,'Tcs-View',NEWID(),837,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=551)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(551,'Tcs-Export',NEWID(),837,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=552)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(552,'Tcs-Print',NEWID(),837,'Report',6)
END


--Beam Register -839

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=554)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(554,'Beam_Register-View',NEWID(),839,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=555)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(555,'Beam_Register-Export',NEWID(),839,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=556)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(556,'Beam_Register-Print',NEWID(),839,'Report',6)
END

--Taka Register -840

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=557)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(557,'Taka_Register-View',NEWID(),840,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=558)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(558,'Taka_Register-Export',NEWID(),840,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=559)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(559,'Taka_Register-Print',NEWID(),840,'Report',6)
END

--Yarn Register -845

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=560)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(560,'Yarn_Register-View',NEWID(),845,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=561)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(561,'Yarn_Register-Export',NEWID(),845,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=562)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(562,'Yarn_Register-Print',NEWID(),845,'Report',6)
END

--Company - 138

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=206)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(206,'Company-Create',NEWID(),138,'Setup',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=207)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(207,'Company-Modify',NEWID(),138,'Setup',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=208)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(208,'Company-Delete',NEWID(),138,'Setup',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=209)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(209,'Company-View',NEWID(),138,'Setup',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=210)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(210,'Company-Export',NEWID(),138,'Setup',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=211)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(211,'Company-Print',NEWID(),138,'Setup',6)
END


--Role Master - 901

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=380)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(380,'Role-Create',NEWID(),901,'Setup',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=381)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(381,'Role-Modify',NEWID(),901,'Setup',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=382)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(382,'Role-Delete',NEWID(),901,'Setup',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=383)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(383,'Role-View',NEWID(),901,'Setup',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=384)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(384,'Role-Export',NEWID(),901,'Setup',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=385)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(385,'Role-Print',NEWID(),901,'Setup',6)
END 

--UserMaster - 902

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=
)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(374,'UserMaster-Create',NEWID(),902,'Setup',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=375)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(375,'UserMaster-Modify',NEWID(),902,'Setup',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=376)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(376,'UserMaster-Delete',NEWID(),902,'Setup',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=377)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(377,'UserMaster-View',NEWID(),902,'Setup',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=378)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(378,'UserMaster-Export',NEWID(),902,'Setup',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=379)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(379,'UserMaster-Print',NEWID(),902,'Setup',6)
END 


--Payment/Receipt Setup - 908

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=452)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(452,'Pay/Rec_Setup-Create',NEWID(),908,'Setup',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=453)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(453,'Pay/Rec_Setup-Modify',NEWID(),908,'Setup',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=454)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(454,'Pay/Rec_Setup-Delete',NEWID(),908,'Setup',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=455)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(455,'Pay/Rec_Setup-View',NEWID(),908,'Setup',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=456)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(456,'Pay/Rec_Setup-Export',NEWID(),908,'Setup',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=457)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(457,'Pay/Rec_Setup-Print',NEWID(),908,'Setup',6)
END 

 --Stock Report --1058

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=518)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(518,'Stock_Report-View',NEWID(),1058,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=519)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(519,'Stock_Report-Export',NEWID(),1058,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=520)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(520,'Stock_Report-Print',NEWID(),1058,'Report',6)
END

 --Stock Report --1059

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=521)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(521,'Gstr1_Report-View',NEWID(),1059,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=522)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(522,'Gstr1_Report-Export',NEWID(),1059,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=523)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(523,'Gstr1_Report-Print',NEWID(),1059,'Report',6)
END


--Grey-Purchase - 312

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=563)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(563,'Grey-Purchase-Create',NEWID(),312,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=564)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(564,'Grey-Purchase-Modify',NEWID(),312,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=565)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(565,'Grey-Purchase-Delete',NEWID(),312,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=567)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(567,'Grey-Purchase-View',NEWID(),312,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=568)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(568,'Grey-Purchase-Export',NEWID(),312,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=569)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(569,'Grey-Purchase-Print',NEWID(),312,'Transaction',6)
END

-- grey order - 373
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=570)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(570,'Grey-Order-Create',NEWID(),373,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=571)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(571,'Grey-Order-Modify',NEWID(),373,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=572)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(572,'Grey-Order-Delete',NEWID(),373,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=573)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(573,'Grey-Order-View',NEWID(),373,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=574)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(574,'Grey-Order-Export',NEWID(),373,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=575)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(575,'Grey-Order-Print',NEWID(),373,'Transaction',6)
END

-- Purchase Indent/Request --305
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=576)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(576,'Indent-Request-Create',NEWID(),305,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=577)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(577,'Indent-Request-Modify',NEWID(),305,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=578)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(578,'Indent-Request-Delete',NEWID(),305,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=579)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(579,'Indent-Request-View',NEWID(),305,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=580)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(580,'Indent-Request-Export',NEWID(),305,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=581)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(581,'Indent-Request-Print',NEWID(),305,'Transaction',6)
END

-- INDENT APROVAL--382
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=582)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(582,'Indent-Approval-Create',NEWID(),382,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=583)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(583,'Indent-Approval-Modify',NEWID(),382,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=584)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(584,'Indent-Approval-Delete',NEWID(),382,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=585)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(585,'Indent-Approval-View',NEWID(),382,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=586)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(586,'Indent-Approval-Export',NEWID(),382,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=587)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(587,'Indent-Approval-Print',NEWID(),382,'Transaction',6)
END

-- gate ENTRY 383---
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=588)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(588,'Gate-Inward-Create',NEWID(),383,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=589)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(589,'Gate-Inward-Modify',NEWID(),383,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=590)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(590,'Gate-Inward-Delete',NEWID(),383,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=591)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(591,'Gate-Inward-View',NEWID(),383,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=592)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(592,'Gate-Inward-Export',NEWID(),383,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=593)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(593,'Gate-Inward-Print',NEWID(),383,'Transaction',6)
END

--cost center 155------
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=594)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(594,'Cost-Head-Create',NEWID(),155,'Master',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=595)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(595,'Cost-Head-Modify',NEWID(),155,'Master',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=596)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(596,'Cost-Head-Delete',NEWID(),155,'Master',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=597)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(597,'Cost-Head-View',NEWID(),155,'Master',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=598)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(598,'Cost-Head-Export',NEWID(),155,'Master',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=599)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(599,'Cost-Head-Print',NEWID(),155,'Transaction',6)
END



--BOM-------------
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=596)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(596,'BOM-Create',NEWID(),1101,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=597)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(597,'BOM-Modify',NEWID(),1101,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=598)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(598,'BOM-Delete',NEWID(),1101,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=599)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(599,'BOM-View',NEWID(),1101,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=600)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(600,'BOM-Export',NEWID(),1101,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=601)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(601,'BOM-Print',NEWID(),1101,'Transaction',6)
END


--APAREL INWARD------------------
--1102
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=602)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(602,'Apparel-Inward-Create',NEWID(),1102,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=603)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(603,'Apparel-Inward-Modify',NEWID(),1102,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=604)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(604,'Apparel-Inward-Delete',NEWID(),1102,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=605)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(605,'Apparel-Inward-View',NEWID(),1102,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=606)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(606,'Apparel-Inward-Export',NEWID(),1102,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=607)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(607,'Apparel-Inward-Print',NEWID(),1102,'Transaction',6)
END


--APAREL QC------------------
--1103
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=608)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(608,'Apparel-Qc-Create',NEWID(),1103,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=609)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(609,'Apparel-Qc-Modify',NEWID(),1103,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=610)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(610,'Apparel-Qc-Delete',NEWID(),1103,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=611)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(611,'Apparel-Qc-View',NEWID(),1103,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=612)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(612,'Apparel-Qc-Export',NEWID(),1103,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=613)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(613,'Apparel-Qc-Print',NEWID(),1103,'Transaction',6)
END

----APAREL BARCODE------------------
--1104
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=614)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(614,'Apparel-Barcode-Create',NEWID(),1104,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=615)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(615,'Apparel-Barcode-Modify',NEWID(),1104,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=616)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(616,'Apparel-Barcode-Delete',NEWID(),1104,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=617)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(617,'Apparel-Barcode-View',NEWID(),1104,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=618)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(618,'Apparel-Barcode-Export',NEWID(),1104,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=619)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(619,'Apparel-Barcode-Print',NEWID(),1104,'Transaction',6)
END

----APAREL OUTWARD------------------
--1105
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=620)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(620,'Apparel-Outward-Create',NEWID(),1105,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=621)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(621,'Apparel-Outward-Modify',NEWID(),1105,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=622)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(622,'Apparel-Outward-Delete',NEWID(),1105,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=623)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(623,'Apparel-Outward-View',NEWID(),1105,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=624)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(624,'Apparel-Outward-Export',NEWID(),1105,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=625)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(625,'Apparel-Outward-Print',NEWID(),1105,'Transaction',6)
END



--BOM-------------
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=626)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(626,'BOM-Create',NEWID(),1101,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=627)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(627,'BOM-Modify',NEWID(),1101,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=628)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(628,'BOM-Delete',NEWID(),1101,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=629)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(629,'BOM-View',NEWID(),1101,'Transaction',4)
END


-- job work income register

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=630)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(630,'Job_Income_Register-View',NEWID(),860,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=631)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(631,'Job_Income_Register-Export',NEWID(),860,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=632)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(632,'Job_Income_Register-Print',NEWID(),860,'Report',6)
END


-- Gate Entry
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=633)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(633,'Gate_Inward_Register-View',NEWID(),861,'Report',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=634)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(634,'Gate_Inward_Register-Export',NEWID(),861,'Report',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=635)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(635,'Gate_Inward_Register-Print',NEWID(),861,'Report',6)
END


-- store return

--374
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=636)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(636,'Store-Return-Create',NEWID(),374,'Transaction',1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=637)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(637,'Store-Return-Modify',NEWID(),374,'Transaction',2)
END                       

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=638)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(638,'Store-Return-Delete',NEWID(),374,'Transaction',3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=639)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(639,'Store-Return-View',NEWID(),374,'Transaction',4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=640)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(640,'Store-Return-Export',NEWID(),374,'Transaction',5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Id=641)
BEGIN
	INSERT INTO dbo.Permissions(Id,PermissionDescription,RowId,ModuleId,PermissionType,PermissionTypeId)
	values(641,'Store-Return-Print',NEWID(),374,'Transaction',6)
END

SET IDENTITY_INSERT dbo.Permissions OFF