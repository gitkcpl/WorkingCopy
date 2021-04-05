
SET IDENTITY_INSERT dbo.Country on
GO

if NOT exists (select 1 from Country em where em.Id=1)
BEGIN
INSERT INTO dbo.Country
(
   Id, CountryName, IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (1, 'INDIA',1,0,GetDate(),'Admin',NEWID())
END
go
SET IDENTITY_INSERT dbo.Country OFF
Go

SET IDENTITY_INSERT dbo.State on
GO

if NOT exists (select 1 from State em where em.Id=1)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (1, 'JAMMU AND KASHMIR',1,'01',1,0,GetDate(),'Admin',NEWID())
END
go

if NOT exists (select 1 from State em where em.Id=2)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (2, 'HIMACHAL PRADESH',1,'02',1,0,GetDate(),'Admin',NEWID())
END
GO

if NOT exists (select 1 from State em where em.Id=3)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (3, 'PUNJAB',1,'03',1,0,GetDate(),'Admin',NEWID())
END
GO


if NOT exists (select 1 from State em where em.Id=4)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (4, 'CHANDIGARH',1,'04',1,0,GetDate(),'Admin',NEWID())
END
GO

if NOT exists (select 1 from State em where em.Id=5)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (5, 'UTTARAKHAND',1,'05',1,0,GetDate(),'Admin',NEWID())
END
GO

if NOT exists (select 1 from State em where em.Id=6)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (6, 'HARYANA',1,'06',1,0,GetDate(),'Admin',NEWID())
END
GO

if NOT exists (select 1 from State em where em.Id=7)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (7, 'DELHI',1,'07',1,0,GetDate(),'Admin',NEWID())
END
GO

if NOT exists (select 1 from State em where em.Id=8)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (8, 'RAJASTHAN',1,'08',1,0,GetDate(),'Admin',NEWID())
END
GO

if NOT exists (select 1 from State em where em.Id=9)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,
   RowId
)values (9, 'UTTAR  PRADESH',1,'09',1,0,GetDate(),'Admin',NEWID())
END
GO

if NOT exists (select 1 from State em where em.Id=10)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (10, 'BIHAR',1,'10',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=11)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (11, 'SIKKIM',1,'11',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=12)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (12, 'ARUNACHAL PRADESH',1,'12',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=13)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (13, 'NAGALAND',1,'13',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=14)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (14, 'MANIPUR',1,'14',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=15)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (15, 'MIZORAM',1,'15',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=16)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (16, 'TRIPURA',1,'16',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=17)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (17, 'MEGHLAYA',1,'17',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=18)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (18, 'ASSAM',1,'18',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=19)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (19, 'WEST BENGAL',1,'19',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=20)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (20, 'JHARKHAND',1,'20',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=21)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (21, 'ODISHA',1,'21',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=22)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (22, 'CHATTISGARH',1,'22',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=23)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (23, 'MADHYA PRADESH',1,'23',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=24)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (24, 'GUJARAT',1,'24',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=25)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (25, 'DAMAN AND DIU',1,'25',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=26)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (26, 'DADRA AND NAGAR HAVELI',1,'26',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=27)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (27, 'MAHARASHTRA',1,'27',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=28)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (28, 'ANDHRA PRADESH(BEFORE DIVISION)',1,'28',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=29)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (29, 'KARNATAKA',1,'29',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=30)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (30, 'GOA',1,'30',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=31)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (31, 'LAKSHWADEEP',1,'31',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=32)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (32, 'KERALA',1,'32',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=33)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (33, 'TAMIL NADU',1,'33',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=34)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (34, 'PUDUCHERRY',1,'34',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=35)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (35, 'ANDAMAN AND NICOBAR ISLANDS',1,'35',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=36)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (36, 'TELANGANA',1,'36',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=37)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (37, 'TELANGANA',1,'37',1,0,GetDate(),'Admin',NEWID())
END
GO

IF NOT exists (select 1 from State em where em.Id=38)
BEGIN
INSERT INTO dbo.State
(
   Id, StateName, CountryId,GstCode,IsActive,IsDeleted,CreateDate,CreateUser,RowId
)values (38, 'ANDHRA PRADESH (NEW)',1,'38',1,0,GetDate(),'Admin',NEWID())
END
GO

SET IDENTITY_INSERT dbo.State OFF