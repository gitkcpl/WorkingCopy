--dedctee master
SET IDENTITY_INSERT dbo.Deductee ON
IF NOT EXISTS(SELECT 1 FROM dbo.Deductee WHERE Id=1)
INSERT INTO dbo.Deductee(Id,Descr,RowId, CreateDate,CreateUser,IpAddress)
VALUES(1,'NA',NEWID(),GETDATE(),'Admin','NA')

IF NOT EXISTS(SELECT 1 FROM dbo.Deductee WHERE Id=2)
INSERT INTO dbo.Deductee(Id,Descr,RowId, CreateDate,CreateUser,IpAddress)
VALUES(2,'Artificial Judicial Person',NEWID(),GETDATE(),'Admin','NA')

IF NOT EXISTS(SELECT 1 FROM dbo.Deductee WHERE Id=3)
INSERT INTO dbo.Deductee(Id,Descr,RowId, CreateDate,CreateUser,IpAddress)
VALUES(3,'Association of Person',NEWID(),GETDATE(),'Admin','NA')

IF NOT EXISTS(SELECT 1 FROM dbo.Deductee WHERE Id=4)
INSERT INTO dbo.Deductee(Id,Descr,RowId, CreateDate,CreateUser,IpAddress)
VALUES(4,'Body of Individuals',NEWID(),GETDATE(),'Admin','NA')

IF NOT EXISTS(SELECT 1 FROM dbo.Deductee WHERE Id=5)
INSERT INTO dbo.Deductee(Id,Descr,RowId, CreateDate,CreateUser,IpAddress)
VALUES(5,'Co-operative Society',NEWID(),GETDATE(),'Admin','NA')

IF NOT EXISTS(SELECT 1 FROM dbo.Deductee WHERE Id=6)
INSERT INTO dbo.Deductee(Id,Descr,RowId, CreateDate,CreateUser,IpAddress)
VALUES(6,'Company Non-Resident',NEWID(),GETDATE(),'Admin','NA')

IF NOT EXISTS(SELECT 1 FROM dbo.Deductee WHERE Id=7)
INSERT INTO dbo.Deductee(Id,Descr,RowId, CreateDate,CreateUser,IpAddress)
VALUES(7,'Company Resident',NEWID(),GETDATE(),'Admin','NA')

IF NOT EXISTS(SELECT 1 FROM dbo.Deductee WHERE Id=9)
INSERT INTO dbo.Deductee(Id,Descr,RowId, CreateDate,CreateUser,IpAddress)
VALUES(9,'Individual/HUF-Non Resident',NEWID(),GETDATE(),'Admin','NA')


IF NOT EXISTS(SELECT 1 FROM dbo.Deductee WHERE Id=10)
INSERT INTO dbo.Deductee(Id,Descr,RowId, CreateDate,CreateUser,IpAddress)
VALUES(10,'Local Authority',NEWID(),GETDATE(),'Admin','NA')

IF NOT EXISTS(SELECT 1 FROM dbo.Deductee WHERE Id=11)
INSERT INTO dbo.Deductee(Id,Descr,RowId, CreateDate,CreateUser,IpAddress)
VALUES(11,'Partnership Firm',NEWID(),GETDATE(),'Admin','NA')

SET IDENTITY_INSERT dbo.Deductee OFF

-- Nature of payments

SET IDENTITY_INSERT dbo.Nop ON
IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=1)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress)
VALUES(1,'NA',NEWID(),'92A','192',GETDATE(),'Admin','NA','TDS')
UPDATE Nop SET Extra1='TDS' WHERE ID=1

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=2)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress,Extra1)
VALUES(2,'Payment to Govt. Employees other than Union Government Employees',NEWID(),'92A','192',GETDATE(),'Admin','NA')
UPDATE Nop SET Extra1='TDS' WHERE ID=2

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=3)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress)
VALUES(3,'Payment of Employees other than Govt. Employee',NEWID(),'92B','192',GETDATE(),'Admin','NA')
UPDATE Nop SET Extra1='TDS' WHERE ID=3

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=4)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress)
VALUES(4,'Interest on Securities',NEWID(),'193','193',GETDATE(),'Admin','NA')
UPDATE Nop SET Extra1='TDS' WHERE ID=4

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=5)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress)
VALUES(5,'Divident',NEWID(),'194','194',GETDATE(),'Admin','NA')
UPDATE Nop SET Extra1='TDS' WHERE ID=5

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=6)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress)
VALUES(6,'Interest other than interest on securities',NEWID(),'194A','194A',GETDATE(),'Admin','NA')
UPDATE Nop SET Extra1='TDS' WHERE ID=6

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=7)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress)
VALUES(7,'Winnings from lotteries and crossword puzzles',NEWID(),'94B','194B',GETDATE(),'Admin','NA')
UPDATE Nop SET Extra1='TDS' WHERE ID=7

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=8)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress)
VALUES(8,'Winnings from horse race',NEWID(),'4BB','194BB',GETDATE(),'Admin','NA')
UPDATE Nop SET Extra1='TDS' WHERE ID=8

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=9)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress)
VALUES(9,'Payment of contractors and sub-contractors',NEWID(),'94C','194C',GETDATE(),'Admin','NA')
UPDATE Nop SET Extra1='TDS' WHERE ID=9

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=10)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress)
VALUES(10,'Insurance Commission',NEWID(),'94D','194D',GETDATE(),'Admin','NA')
UPDATE Nop SET Extra1='TDS' WHERE ID=10

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=11)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress)
VALUES(11,'Payments to non-resident Sportsmen/Sport Associations',NEWID(),'94E','194E',GETDATE(),'Admin','NA')
UPDATE Nop SET Extra1='TDS' WHERE ID=11

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=12)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress)
VALUES(12,'Payments in respect of Deposits under National Savings Schemes',NEWID(),'4EE','194EE',GETDATE(),'Admin','NA')
UPDATE Nop SET Extra1='TDS' WHERE ID=12

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=13)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress)
VALUES(13,'Payments on account of Re-purchase of Units by Mutual Funds or UTI',NEWID(),'94F','194F',GETDATE(),'Admin','NA')
UPDATE Nop SET Extra1='TDS' WHERE ID=13

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=14)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress)
VALUES(14,'Commission, prize etc., on sale of Lottery tickets',NEWID(),'94G','194G',GETDATE(),'Admin','NA')
UPDATE Nop SET Extra1='TDS' WHERE ID=14

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=15)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress)
VALUES(15,'Commission or Brokerage',NEWID(),'94H','194H',GETDATE(),'Admin','NA')
UPDATE Nop SET Extra1='TDS' WHERE ID=15

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=16)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress)
VALUES(16,'Rent',NEWID(),'94I','194I',GETDATE(),'Admin','NA')
UPDATE Nop SET Extra1='TDS' WHERE ID=16

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=17)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress)
VALUES(17,'Fees for Professional or Technical Services',NEWID(),'94J','194J',GETDATE(),'Admin','NA')
UPDATE Nop SET Extra1='TDS' WHERE ID=17

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=18)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress)
VALUES(18,'Income payable to a resident assessee in respect of Units of a specified Mutual Fund or of the units of the UTI',NEWID(),'94K','194K',GETDATE(),'Admin','NA')
UPDATE Nop SET Extra1='TDS' WHERE ID=18

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=19)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress)
VALUES(19,'Payment of Compensation on acquisition of certain immovable property',NEWID(),'4LA','194LA',GETDATE(),'Admin','NA')
UPDATE Nop SET Extra1='TDS' WHERE ID=19

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=20)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress)
VALUES(20,'Income by way of Interest from Infrastructure Debt fund',NEWID(),'4LB','194LB',GETDATE(),'Admin','NA')
UPDATE Nop SET Extra1='TDS' WHERE ID=20

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=21)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress)
VALUES(21,'Income by way of interest from Indian company engaged in certain business',NEWID(),'4LC','194LC',GETDATE(),'Admin','NA')
UPDATE Nop SET Extra1='TDS' WHERE ID=21

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=22)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress)
VALUES(22,'Other sums payable to a non-resident',NEWID(),'195','195',GETDATE(),'Admin','NA')
UPDATE Nop SET Extra1='TDS' WHERE ID=22

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=23)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress)
VALUES(23,'Income in respect of units of Non-Residents',NEWID(),'96A','196A',GETDATE(),'Admin','NA')
UPDATE Nop SET Extra1='TDS' WHERE ID=23

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=24)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress)
VALUES(24,'Payments in respect of Units to an Offshore Fund',NEWID(),'96B','196B',GETDATE(),'Admin','NA')
UPDATE Nop SET Extra1='TDS' WHERE ID=24

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=25)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress)
VALUES(25,'Income from foreign Currency Bonds or shares of Indian Company payable to Non-Resident',NEWID(),'96C','196C',GETDATE(),'Admin','NA')
UPDATE Nop SET Extra1='TDS' WHERE ID=25

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=26)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress)
VALUES(26,'Income of foreign institutional investors from securities',NEWID(),'96D','196D',GETDATE(),'Admin','NA')
UPDATE Nop SET Extra1='TDS' WHERE ID=26

-- TCS
IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=27)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress,Extra1)
VALUES(27,'Liquor of alcoholic nature, made for consumption by humans',NEWID(),'6CA','206C',GETDATE(),'Admin','NA','TCS')

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=28)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress,Extra1)
VALUES(28,'Timber obtained under Forest lease',NEWID(),'6CB','206C',GETDATE(),'Admin','NA','TCS')

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=29)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress,Extra1)
VALUES(29,'Timber obtained by any mode other than under a forest lease',NEWID(),'6CC','206C',GETDATE(),'Admin','NA','TCS')

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=30)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress,Extra1)
VALUES(30,'Any other forest produce not being timber or tendu leaves',NEWID(),'6CD','206C',GETDATE(),'Admin','NA','TCS')

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=31)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress,Extra1)
VALUES(31,'Scrap',NEWID(),'6CE','206C',GETDATE(),'Admin','NA','TCS')

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=32)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress,Extra1)
VALUES(32,'Tendu Leaves',NEWID(),'6CI','206C',GETDATE(),'Admin','NA','TCS')

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=33)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress,Extra1)
VALUES(33,'Minerals, being coal or lignite or iron ore',NEWID(),'6CJ','206C',GETDATE(),'Admin','NA','TCS')

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=34)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress,Extra1)
VALUES(34,'Parking Lot',NEWID(),'6CF','206C',GETDATE(),'Admin','NA','TCS')

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=35)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress,Extra1)
VALUES(35,'Toll Plaza',NEWID(),'6CG','206C',GETDATE(),'Admin','NA','TCS')

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=36)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress,Extra1)
VALUES(36,'Mining & Quarrying',NEWID(),'6CH','206C',GETDATE(),'Admin','NA','TCS')

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=37)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress,Extra1)
VALUES(37,'Motor Vehicle (any mode of payment)',NEWID(),'6CL','206C',GETDATE(),'Admin','NA','TCS')

IF NOT EXISTS(SELECT 1 FROM dbo.Nop WHERE Id=38)
INSERT INTO dbo.Nop(Id,Descr,RowId,SecCode,SecNo, CreateDate,CreateUser,IpAddress,Extra1)
VALUES(38,'Sale of Goods',NEWID(),'6CR','206C',GETDATE(),'Admin','NA','TCS')

SET IDENTITY_INSERT dbo.Nop OFF