SET IDENTITY_INSERT dbo.Uom ON

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=1)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,RateOn,Nod,GSTUnit,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(1,'BAG','BAGS','Q',0,'BAG-BAGS',GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=2)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,RateOn,Nod,GSTUnit,UnitName,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(2,'BAL','Q',0,'BAL-BALE','BALE',GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=3)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,RateOn,Nod,GSTUnit,UnitName,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(3,'BDL','Q',0,'BDL-BUNDLES','BUNDLES',GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=4)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,RateOn,Nod,GSTUnit,UnitName,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(4,'BKL','Q',0,'BKL-BUCKLES','BUCKLES',GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=5)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(5,'BOU','BILLIONS OF UNITS','BOU-BILLIONS OF UNITS','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=6)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(6,'BOX','BOX','BOX-BOX','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=7)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(7,'BTL','BOTTLES','BTL-BOTTLES','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=8)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(8,'BUN','BUNCHES','BUN-BUNCHES','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=9)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(9,'CAN','CANS','CAN-CANS','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=10)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(10,'CBM','CUBIC METERS','CBM-CUBIC METERS','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=11)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(11,'CCM','CUBIC CENTIMETERS','CBM-CUBIC CENTIMETERS','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=12)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(12,'CMS','CENTIMETERS','CMS-CENTIMETERS','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=13)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(13,'CTN','CARTONS','CTN-CARTONS','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=14)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(14,'DOZ','DOZENS','DOZ-DOZENS','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=15)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(15,'DRM','DRUMS','DRM-DRUMS','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=16)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(16,'GGK','GREAT GROSS','GGK-GREAT GROSS','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=17)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(17,'GMS','GRAMMES','GMS-GRAMMES','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=18)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(18,'GRS','GROSS','GRS-GROSS','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=19)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(19,'GYD','GROSS YARD','GYD-GROSS YARD','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=20)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(20,'KGS','KILOGRAMS','GYD-GROSS YARD','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=21)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(21,'KLR','KILOLIIRE','KLR-KILOLITRE','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=22)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(22,'KME','KILOMETRE','KME-KILOMITRE','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=23)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(23,'MLT','MILILITRE','MLT-MILILITRE','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=24)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(24,'MTR','METERS','MTR-METERS','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=25)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(25,'MTS','METRIC TON','MTS-METRIC TON','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=26)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(26,'NOS','NUMBERS','NOS-NUMBERS','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=27)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(27,'PAC','PACKS','PAC-PACKS','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=28)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(28,'PCS','PIECES','PCS-PIECES','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=29)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(29,'PRS','PAIRS','PRS-PAIRS','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=30)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(30,'QTL','QUINTAL','QTL-QUINTAL','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=31)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(31,'ROL','ROLLS','ROL-ROLS','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=32)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(32,'SET','SETS','SET-SETS','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=33)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(33,'SQF','SQUARE FEET','SQF-SQUARE FEET','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=34)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(34,'SQM','SQUARE METERS','SQM-SQUARE METERS','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=35)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(35,'SQY','SQUARE YARDS','SQY-SQUARE YARDS','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=36)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(36,'TBS','TABLETS','TBS-TABLETS','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=37)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(37,'TGM','TEN GROSS','TGM-TEN GROSS','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=38)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(38,'THD','THOUSANDS','THS-THOUSANDS','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=39)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(39,'TON','TONNES','TON-TONNES','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=40)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(40,'TUB','TUBES','TUB-TUBES','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=41)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(41,'UGS','US GALLONS','UGS-US GALLONS','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=42)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(42,'UNT','UNITS','UNT-UNITS','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=43)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(43,'YDS','YARDS','YDS-YARDS','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.Uom WHERE Id=44)
BEGIN
	INSERT INTO dbo.Uom(Id,UnitCode,UnitName,GSTUnit,RateOn,Nod,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(44,'OTH','OTHERS','OTH-OTHERS','Q',0,GETDATE(),'Admin',1,0,NEWID())
END

SET IDENTITY_INSERT dbo.Uom OFF

