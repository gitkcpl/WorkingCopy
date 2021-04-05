
SET IDENTITY_INSERT dbo.Nob ON
--Nob
IF NOT EXISTS(SELECT 1 FROM dbo.Nob WHERE Id=1)
INSERT INTO dbo.Nob(Id,BusinessType)
VALUES(1,'Textiles Trading')

IF NOT EXISTS(SELECT 1 FROM dbo.Nob WHERE Id=2)
INSERT INTO dbo.Nob(Id,BusinessType)
VALUES(2,'Accounts')


IF NOT EXISTS(SELECT 1 FROM dbo.Nob WHERE Id=3)
INSERT INTO dbo.Nob(Id,BusinessType)
VALUES(3,'Weaving')


IF NOT EXISTS(SELECT 1 FROM dbo.Nob WHERE Id=4)
INSERT INTO dbo.Nob(Id,BusinessType)
VALUES(4,'Yarn Manufacturer')


--IF NOT EXISTS(SELECT 1 FROM dbo.Nob WHERE Id=5)
--INSERT INTO dbo.Nob(Id,BusinessType)
--VALUES(5,'Grament Manufacturer')


IF NOT EXISTS(SELECT 1 FROM dbo.Nob WHERE Id=6)
INSERT INTO dbo.Nob(Id,BusinessType)
VALUES(6,'Point of Sale')


IF NOT EXISTS(SELECT 1 FROM dbo.Nob WHERE Id=7)
INSERT INTO dbo.Nob(Id,BusinessType)
VALUES(7,'Customized')


--IF NOT EXISTS(SELECT 1 FROM dbo.Nob WHERE Id=8)
--INSERT INTO dbo.Nob(Id,BusinessType)
--VALUES(8,'Ecommerce')


IF NOT EXISTS(SELECT 1 FROM dbo.Nob WHERE Id=9)
INSERT INTO dbo.Nob(Id,BusinessType)
VALUES(9,'Inventory')


IF NOT EXISTS(SELECT 1 FROM dbo.Nob WHERE Id=10)
INSERT INTO dbo.Nob(Id,BusinessType)
VALUES(10,'Process House')

IF NOT EXISTS(SELECT 1 FROM dbo.Nob WHERE Id=11)
INSERT INTO dbo.Nob(Id,BusinessType)
VALUES(11,'Yarn Mfg + Weaving')


--IF NOT EXISTS(SELECT 1 FROM dbo.Nob WHERE Id=12)
--INSERT INTO dbo.Nob(Id,BusinessType)
--VALUES(12,'Online')


SET IDENTITY_INSERT dbo.Nob Off