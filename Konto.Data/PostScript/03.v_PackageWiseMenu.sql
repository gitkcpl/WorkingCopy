SET IDENTITY_INSERT dbo.Menu_Package ON
------------------------------------------------------------
---------------Taxtile Trading Package -1 ---------------------
------------------------------------------------------------

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=1)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (1,1,100)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=2)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (2,1,101)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=3)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (3,1,103)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=4)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (4,1,104)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=5)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (5,1,105)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=6)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (6,1,106)

  IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=19)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (19,1,1051)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=20)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (20,1,1050)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=21)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (21,1,130)

--Product

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=7)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (7,1,107)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=8)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (8,1,108)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=9)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (9,1,109)


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=11)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (11,1,111)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=12)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (12,1,112)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=13)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (13,1,113)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=14)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (14,1,114)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=16)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (16,1,116)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=17)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (17,1,117)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=18)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (18,1,118)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=22)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (22,1,132)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=23)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (23,1,133)


--Account in master
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=24)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (24,1,140)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=25)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (25,1,122)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=26)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (26,1,123)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=27)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (27,1,124)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=28)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (28,1,120)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=29)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (29,1,131)

--Opening Balances

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=30)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (30,1,139)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=31)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (31,1,141)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=32)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (32,1,142)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=33)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (33,1,143)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=34)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (34,1,144)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=35)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (35,1,145)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=36)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (36,1,146)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=37)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (37,1,147)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=38)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (38,1,148)

--Master
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=39)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (39,1,126)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=40)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (40,1,127)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=41)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (41,1,129)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=643)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (643,1,366)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=642)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (642,1,1064)

--Transaction
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=42)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (42,1,300)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=794)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (794,1,337) -- Stock Journal

--Purchase
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=43)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (43,1,301)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=44)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (44,1,373)--grayOrder

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=45)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (45,1,312) --GreyPurchase

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=46)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (46,1,333)  

  IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=53)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (53,1,334)


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=47)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (47,1,306) 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=48)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (48,1,307)  

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=49)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (49,1,308)  

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=50)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (50,1,309)  
 

 IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=51)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (51,1,324)  
 
  IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=52)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (52,1,363)

 IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=513)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (513,1,329) -- General Expense Return

--Mill 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=54)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (54,1,303)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=55)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (55,1,310) --Mill Issue

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=56)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (56,1,314) --Mill programm

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=57)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (57,1,356) --Mill Receipt

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=58)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (58,1,361) --Mill Return

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=133)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (133,1,377) -- Mill Receipt Voucher

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=646)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (646,1,318) -- Lot Allocation

--Job
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=132)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (132,1,304)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=59)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (59,1,317) -- Mill Folding

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=60)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (60,1,359)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=61)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (61,1,319) -- job receipt

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=62)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (62,1,360) -- Job Bill

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=653)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (653,1,1066) -- Job Sales

--Sales
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=63)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (63,1,351)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=64)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (64,1,352)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=65)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (65,1,353)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=66)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (66,1,355)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=67)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (67,1,358)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=68)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (68,1,362)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=69)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (69,1,375) 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=640)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (640,1,1063)  --Brokerage Sale 

--Account
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=70)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (70,1,302)


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=71)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (71,1,321)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=72)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (72,1,322)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=73)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (73,1,325)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=74)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (74,1,328)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=75)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (75,1,376)

------------------
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=76)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (76,1,365)--Design Mapping

--Reports
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=77)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (77,1,800)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=78)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (78,1,801)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=79)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (79,1,802)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=80)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (80,1,1058)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=81)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (81,1,803)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=82)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (82,1,804)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=83)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (83,1,805)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=84)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (84,1,806)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=85)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (85,1,807)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=86)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (86,1,808)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=87)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (87,1,809)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=88)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (88,1,810)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=89)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (89,1,811)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=90)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (90,1,812)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=91)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (91,1,813)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=92)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (92,1,814)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=93)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (93,1,815)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=94)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (94,1,816)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=95)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (95,1,817)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=96)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (96,1,818)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=97)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (97,1,819)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=98)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (98,1,820)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=99)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (99,1,821)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=100)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (100,1,822)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=101)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (101,1,823)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=102)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (102,1,824)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=103)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (103,1,825)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=104)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (104,1,826)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=105)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (105,1,827)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=106)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (106,1,828)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=107)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (107,1,829)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=108)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (108,1,830)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=109)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (109,1,831)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=110)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (110,1,832)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=111)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (111,1,833)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=112)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (112,1,834)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=113)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (113,1,835)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=114)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (114,1,836)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=115)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (115,1,837)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=644)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (644,1,846)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=657)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (657,1,1067)

--setup
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=116)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (116,1,900)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=117)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (117,1,901)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=118)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (118,1,902)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=119)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (119,1,138)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=263)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (263,1,904)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=639)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (639,1,908) -- Payment Receipt Setup

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=648)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (648,1,911) --Report setup

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=804)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (804,1,912) --Template setup

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=805)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (805,1,913) -- Database Group

--Tools
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=120)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (120,1,700)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=121)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (121,1,701)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=122)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (122,1,702)


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=123)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (123,1,703)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=124)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (124,1,704)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=125)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (125,1,705)

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=126)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (126,1,706) -- Voucher ReIndex

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=127)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (127,1,707)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=128)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (128,1,708)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=129)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (129,1,709)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=130)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (130,1,710)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=131)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (131,1,711)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=132)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (132,1,1060)


-----------------------------------------------------------------------------
--------------- Accounts Packaged - 2 -------------------------------------------
-----------------------------------------------------------------------------

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=427)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (427,2,100) 
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=428)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (428,2,101)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=429)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (429,2,103)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=430)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (430,2,104)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=431)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (431,2,105)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=432)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (432,2,106)

  IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=433)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (433,2,1051)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=434)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (434,2,1050)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=435)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (435,2,130)

--Product

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=436)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (436,2,107)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=437)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (437,2,108)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=438)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (438,2,109)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=439)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (439,2,111)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=440)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (440,2,112)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=441)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (441,2,113)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=442)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (442,2,114)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=443)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (443,2,116)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=444)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (444,2,117)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=445)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (445,2,118)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=446)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (446,2,132)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=447)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (447,2,133)


--Account in master
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=448)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (448,2,140)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=449)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (449,2,122)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=450)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (450,2,123)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=451)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (451,2,124)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=452)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (452,2,120)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=453)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (453,2,131)

--Opening
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=454)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (454,2,139) --Sale Purchase Opening Bill

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=638)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (638,2,141) -- Opening

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=455)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (455,2,142) -- Opening Account Balance

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=456)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (456,2,147)  -- Part Payment

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=457)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (457,2,148) -- Opening Cheque


--Master
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=458)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (458,2,126)   -- Voucher type
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=459)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (459,2,127) -- Voucher
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=460)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (460,2,128)  -- 

--Transaction
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=461)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (461,2,300) -- Transaction 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=797)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (797,2,337)


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=514)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (514,2,351) -- Sales

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=462)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (462,2,301) -- Purchase

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=463)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (463,2,302) -- Account

--Purchase

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=464)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (464,2,309)  -- Purchase Invoice
 
 IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=465)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (465,2,324)    -- General Expense
 
  IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=466)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (466,2,363)  -- Purchase Return

 IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=515)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (515,2,329)  -- General expense Return

--Sales

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=467)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (467,2,358)   -- Sales Invoice

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=468)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (468,2,362)    -- Sales Return

--Account
 
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=470)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (470,2,321)    -- Journal Voucher

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=471)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (471,2,322)   -- Receipt voucher

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=472)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (472,2,325)   -- Cr/Dr Note

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=473)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (473,2,328)  -- Payment Voucher

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=474)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (474,2,376)  -- BRS

--Reports

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=475)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (475,2,800) -- Reports

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=476)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (476,2,801)  -- Account Ledger 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=477)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (477,2,802)  -- Outstanding 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=478)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (478,2,803)  -- Trial Balance

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=479)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (479,2,804)  -- Balance Sheet

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=517)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (517,2,805)  -- Inventory Register 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=480)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (480,2,807)  -- Purchase Invoice
 
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=481)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (481,2,811)  -- Purchase Return

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=482)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (482,2,812)  --

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=483)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (483,2,814)   -- Sales Invoice

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=484)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (484,2,816)  -- Sales Return


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=485)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (485,2,822)  ---- 


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=486)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (486,2,827)  -- Cr/Dr Note


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=487)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (487,2,829)  -- Tax Register

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=488)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (488,2,830)  -- Gstr1

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=489)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (489,2,831)  -- Gstr2

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=490)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (490,2,832)  -- Gstr3

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=491)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId 
)values (491,2,833)   -- Gst job Work

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=492)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (492,2,834)  -- Gstr2 Reconcile

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=493)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (493,2,835)  ---

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=494)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (494,2,836)   --- Tds

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=495)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (495,2,837)  -- Tcs

--setup
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=496)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (496,2,900)   -- Setup

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=497)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (497,2,901)   -- User Role

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=498)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (498,2,902)  -- User Master

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=499)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (499,2,138)  -- Company
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=500)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (500,2,904) -- Menu Setup

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=651)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (651,2,911) --Report setup

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=802)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (802,2,912) --Template setup

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=806)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (806,2,913) -- Database Group

--Tools
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=501)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (501,2,700)  -- Tools

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=502)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (502,2,701)  -- Backup/ Restore

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=503)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (503,2,702)  -- Change Password


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=504)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (504,2,703)  -- 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=505)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (505,2,704)  -- Data Fredge

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=506)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (506,2,705)  -- Account Merge

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=507)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (507,2,706) -- Voucher Reindex

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=508)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (508,2,707)  -- 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=509)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (509,2,708) -- Depreciation Posting 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=510)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId 
)values (510,2,709)  -- Cash Adjustment

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=511)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (511,2,710)  -- 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=512)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (512,2,711) -- Bulk Delete

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=655)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (655,2,1066)


------------------------------------------------------------------------------------------------------------
-----------------------Looms (Weaving) - 3-------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------

-- Master

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=134)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (134,3,100)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=135)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (135,3,101)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=136)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (136,3,103)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=137)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (137,3,104)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=138)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (138,3,105)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=139)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (139,3,106)

  IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=140)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (140,3,1051)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=141)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (141,3,1050)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=142)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (142,3,130)

--Product
 
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=143)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (143,3,107)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=144)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (144,3,108)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=145)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (145,3,109)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=146)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (146,3,111)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=147)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (147,3,112)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=148)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (148,3,113)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=149)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (149,3,114)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=150)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (150,3,116)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=151)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (151,3,117)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=152)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (152,3,118)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=153)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (153,3,132)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=154)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (154,3,133)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=266)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (266,3,153)

--Account in master
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=155)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (155,3,140)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=156)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (156,3,122)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=157)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (157,3,123)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=158)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (158,3,124)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=159)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (159,3,120)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=160)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (160,3,131)

--Opening
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=161)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (161,3,139)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=162)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (162,3,141)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=163)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (163,3,142)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=260)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (260,3,143)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=261)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (261,3,144)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=262)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (262,3,145)

---
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=164)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (164,3,146)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=165)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (165,3,147)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=166)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (166,3,148)

--Master
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=167)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (167,3,126)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=168)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (168,3,127)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=169)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (169,3,129)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=170)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (170,3,366)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=641)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (641,3,1064)
 
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=265)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (265,3,134)

--Transaction
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=171)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (171,3,300)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=795)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (795,3,337) -- Stock Journal

--Purchase
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=172)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (172,3,301)   

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=173)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (173,3,306) 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=174)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (174,3,307)  

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=175)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (175,3,308)  

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=176)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (176,3,309)  
 

 IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=177)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (177,3,324)  
 
  IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=178)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (178,3,363)

 IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=516)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (516,3,329) -- General expense return

/*
  IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=179)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (179,3,334)
*/

--Production

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=180)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (180,3,378)--Production


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=181)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (181,3,364)--StoreIssue


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=182)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (182,3,374)--StoreIssueReturn

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=183)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (183,3,379)--seprator

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=184)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (184,3,367)--Beam Production

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=185)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (185,3,368)--Beam Loading

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=186)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (186,3,369)--Taka Production
 
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=423)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (423,3,381)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=518)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (518,3,1061) -- Color Matching

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=520)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (520,3,1062) -- Job Card

--Mill 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=417)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (417,3,303)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=418)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (418,3,310)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=419)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (419,3,314)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=420)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (420,3,356)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=421)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (421,3,361)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=422)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (422,3,377)

--Job
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=187)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (187,3,304)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=188)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (188,3,317)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=189)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (189,3,359)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=190)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (190,3,319)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=191)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (191,3,360)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=637)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (637,3,336) --Job Against Po

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=647)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (647,3,1065) --Taka Wise Job Receipt Challan

--Sales
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=192)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (192,3,351)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=193)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (193,3,352)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=194)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (194,3,353)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=195)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (195,3,355)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=196)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (196,3,358)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=197)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (197,3,362)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=198)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (198,3,375)

--Account
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=199)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (199,3,302)


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=200)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (200,3,321)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=201)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (201,3,322)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=202)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (202,3,325)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=203)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (203,3,328)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=204)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (204,3,376)
 
 IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=416)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (416,3,380)

------------------
 
--Reports
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=206)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (206,3,800)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=207)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (207,3,801)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=208)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (208,3,802)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=209)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (209,3,1058)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=210)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (210,3,803)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=211)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (211,3,804)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=212)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (212,3,805)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=213)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (213,3,806)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=214)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (214,3,807)
  

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=215)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (215,3,810)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=216)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (216,3,811)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=217)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (217,3,812)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=218)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (218,3,813)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=219)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (219,3,814)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=220)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (220,3,815)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=221)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (221,3,816)
  
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=222)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (222,3,823)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=223)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (223,3,824)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=224)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (224,3,825)
 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=225)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (225,3,827)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=226)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (226,3,828)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=227)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (227,3,829)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=228)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (228,3,830)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=229)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (229,3,831)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=230)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (230,3,832)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=231)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (231,3,833)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=232)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (232,3,834)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=234)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (234,3,835)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=235)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (235,3,836)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=236)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (236,3,837)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=237)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (237,3,838)


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=238)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (238,3,839)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=239)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (239,3,840)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=240)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (240,3,841)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=241)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (241,3,842)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=242)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (242,3,843)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=243)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (243,3,844)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=372)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (372,3,819)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=645)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (645,3,846) -- Interest Ledger

--setup
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=244)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (244,3,900)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=245)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (245,3,901)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=246)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (246,3,902) 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=247)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (247,3,138)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=426)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (426,3,908) --recpay setting

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=649)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (649,3,911) --Report setup

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=800)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (800,3,912) --Template setup

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=807)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (807,3,913) -- Database Group

--Tools

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=248)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (248,3,700)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=249)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (249,3,701)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=250)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (250,3,702)


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=251)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (251,3,703)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=252)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (252,3,704)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=253)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (253,3,705)

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=254)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (254,3,706) -- Voucher ReIndex

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=255)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (255,3,707)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=256)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (256,3,708)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=257)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (257,3,709)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=258)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (258,3,710)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=259)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (259,3,711)
 
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=264)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (264,3,904)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=653)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (653,3,1066)


-----------------------------------------------------------------------
---------------------------------------4 Yarn Production --------------
-----------------------------------------------------------------------

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=660)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (660,4,100)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=661)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (661,4,101) --Location

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=662)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (662,4,103) --State

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=663)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (663,4,104) --City

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=664)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (664,4,105) --Area

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=665)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (665,4,106) -- Product Section

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=273)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (273,4,1051) -- Opening Beam Stock

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=274)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (274,4,1050) --Opening Taka Stock

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=666)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (666,4,130) --Store

--Product

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=667)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (667,4,107)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=668)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (668,4,108)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=669)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (669,4,109)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=670)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (670,4,111) --Sub Group

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=671)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (671,4,112)  --Size Master

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=672)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (672,4,113) --Color Master

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=673)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (673,4,114) --Category Master

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=674)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (674,4,116) --separator

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=675)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (675,4,117) -- Gst Slab

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=676)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (676,4,118) --Product Master

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=677)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (677,4,132) -- Design master

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=678)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (678,4,133) --Catalog

--Account in master

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=679)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (679,4,140) --Account Section

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=680)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (680,4,122) --Ledger Group

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=681)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (681,4,123) --Party Group

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=682)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (682,4,124) -- Account Master

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=683)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (683,4,120) -- SalesMan

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=684)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (684,4,131)  --Common Master

--Opening

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=685)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (685,4,141) --Opening

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=686)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (686,4,139) --Sale Purchase Opening bill

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=687)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (687,4,142) --Opening Account Balance

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=688)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (688,4,143) --Opening Gray Stock

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=689)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (689,4,144) -- Opening Finish stock

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=299)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (299,4,145) --Opening Mill Issue

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=690)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (690,4,146) --Opening Job Issue

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=691)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (691,4,147) --Part Payment

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=692)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (692,4,148)  --Opening Cheque

--Master
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=693)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (693,4,126) --Voucher Type

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=694)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (694,4,127) --Voucher

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=695)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (695,4,129) -- Narration

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=696)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (696,4,134) --Machine Master

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=697)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (697,4,135) --Grade master

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=793)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (793,4,1071) --SubGrade master

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=698)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (698,4,136) --Packing Type Master

--Transaction
 
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=699)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (699,4,300) -- Transaction

--Purchase

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=700)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (700,4,301)    --Purchase

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=701)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (701,4,306) --Purchse Order

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=702)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (702,4,307)   --PO Approval

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=703)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (703,4,308)  --Inward-GRN

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=704)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (704,4,309)  -- Purchase Bill
  
 IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=705)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (705,4,324)   --General Expense
 
  IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=706)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (706,4,363)  --Purchase Return

 IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=707)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (707,4,329) --General Expense Return

/*
  IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=317)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (317,7,334)
*/

--Production

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=708)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (708,4,378)--Production


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=709)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (709,4,364)--StoreIssue
 
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=710)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (710,4,374)--StoreIssueReturn

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=711)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (711,4,379)--seprator

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=322)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (322,4,367)--Beam Production

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=323)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (323,4,368)--Beam Loading

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=324)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (324,4,369)--Taka Production
 
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=712)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (712,4,370)--Batch master
 
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=713)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (713,4,371)--Yarn Production

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=425)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (425,4,381)--Taka Cutting

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=714)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (714,4,1061) -- Jacquard weaving details

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=715)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (715,4,1070) --Weaving Job Card

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=716)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (716,4,1068) --Color Recipe

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=717)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (717,4,1069) --Color Formula

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=718)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (718,4,1070) --Yarn Job card
  
--Mill 

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=328)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (328,4,303) --Mill 

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=329)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (329,4,310)  -- Mill Issue

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=330)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (330,4,314) --Mill Program

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=331)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (331,4,356) --Mill Receipt

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=332)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (332,4,361) --Mill Return

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=333)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (333,4,377)  -- Mill Receipt Voucher


--Job
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=719)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (719,4,304)  --Job

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=335)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (335,4,317) -- Taka Folding

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=720)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (720,4,359)  --Job Issue

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=721)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (721,4,319) --Job Receipt

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=722)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (722,4,360)  -- Job Bill

--Sales

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=723)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (723,4,351)  --Sales

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=724)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (724,4,352) --Sales Order

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=725)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId 
)values (725,4,353) --Sales Approval

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=726)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (726,4,355) --Sales Challan

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=727)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (727,4,358) -- Sales Invoice

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=728)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (728,4,362)  -- Sales Return

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=729)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (729,4,375)  -- Return Challan

--Account
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=730)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (730,4,302)  --Account

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=731)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (731,4,321)   --jv

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=732)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (732,4,322)  --Receipt Voucher

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=733)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (733,4,325) --Cr/Dr Note

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=734)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (734,4,328) -- Payment Voucher

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=735)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (735,4,376) --Bank Reconciliation

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=423)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (423,4,381)  --Taka Cutting


--Reports
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=736)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (736,4,800)  --Reports

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=737)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (737,4,801)  -- Account Ledger

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=738)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (738,4,802)  --Outstanding

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=739)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (739,4,1058) --Stock Report

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=740)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (740,4,803)  --Trial Balance

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=741)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (741,4,804)  -- Balance Sheet

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=742)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (742,4,805)  --Inventory Register

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=743)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (743,4,806)  --GRN

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=744)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (744,4,807)  --Purchase

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=361)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (361,4,808)  -- Gray Purchase 

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=362)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (362,4,809)  -- Gray Purchase Return

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=745)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (745,4,810)  --Purchase Return challan

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=746)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (746,4,811) --Purchase Return

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=747)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (747,4,812)  -- 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=748)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (748,4,813)  --Sales Challan

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=749)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (749,4,814)  --Sales

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=750)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (750,4,815) --Sales Return Challan

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=751)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (751,4,816)  --Sales Return

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=752)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (752,4,817) ---

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=371)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (371,4,818)  --Gray Issue

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=372)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (372,4,819) -- Mill Receipt Challan

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=373)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (373,4,820)  --Mill Receipt Bill

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=374)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (374,4,821)  --Mill Return

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=753)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (753,4,822) ---

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=754)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (754,4,823)  --Job Issue Report

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=755)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (755,4,824)  --Job Receipt Challan Report

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=756)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (756,4,825)  --Job Receipt Bill Report

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=757)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (757,4,826)  -- Folding Report

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=758)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (758,4,827)  -- Credit/Debit Note Report

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=759)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (759,4,828) --Order Register

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=760)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (760,4,829)  -- Tax Register

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=761)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (761,4,830) ---Gstr-1

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=762)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (762,4,831)  -- Gstr2

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=763)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (763,4,832) --Gstr3-B

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=764)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (764,4,833) -- Gstr4-A

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=765)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (765,4,834) -- Gstr2- Reconcile

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=766)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (766,4,835)  -- 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=767)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (767,4,836) -- Tds Report

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=768)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (768,4,837)  -- Tcs Report

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=769)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (769,4,838) --Production

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=391)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (391,4,839)  --Beam Register


--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=392)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (392,4,840)   -- Taka prod register

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=393)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (393,4,841)   --Salary/patiya Register

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=770)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (770,4,842)  --Store Issue

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=771)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (771,4,843)  -- Store Return

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=396)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (396,4,844) --Taka Tracker

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=772)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (772,4,845) -- Yarn Prod Reg


--setup
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=773)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (773,4,900) --Setup

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=774)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (774,4,901) -- User Role

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=775)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (775,4,902)  --User Master

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=776)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (776,4,138) --Company

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=777)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (777,4,904) --Menu Setup

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=778)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (778,4,908)  --Payment/Receipt Setup

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=779)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (779,4,911) --Report setup

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=799)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (799,4,912) --Template setup

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=808)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (808,4,913) -- Database Group

--Tools
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=780)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (780,4,700)  --Tools

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=781)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (781,4,701) --Backup/Restore

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=782)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (782,4,702)  --Change Password

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=783)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (783,4,703)  --

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=784)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (784,4,704)  --Data Fredge

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=785)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (785,4,705)  --Account Merge

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=786)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (786,4,706) -- Voucher ReIndex

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=787)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (787,4,707)  ---

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=788)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (788,4,708) --Depreciation Posting

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=789)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (789,4,709) --Cash Adjustment

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=790)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (790,4,710) --

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=791)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (791,4,711) -- Bulk Delete
 
--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=415)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (415,4,380) --Taka Conversion

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=792)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (792,4,1066)  -- Balance Carry forward


--------------------------------------------------------------------
---------------------------------------7 Customize    --------------
--------------------------------------------------------------------

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=267)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (267,7,100)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=268)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (268,7,101)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=269)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (269,7,103)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=270)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (270,7,104)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=271)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (271,7,105)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=272)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (272,7,106)

  IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=273)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (273,7,1051)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=274)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (274,7,1050)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=275)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (275,7,130)

--Product

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=276)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (276,7,107)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=277)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (277,7,108)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=278)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (278,7,109)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=279)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (279,7,111)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=280)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (280,7,112)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=281)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (281,7,113)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=282)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (282,7,114)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=283)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (283,7,116)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=284)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (284,7,117)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=285)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (285,7,118)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=286)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (286,7,132)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=287)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (287,7,133)


--Account in master
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=288)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (288,7,140)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=289)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (289,7,122)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=290)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (290,7,123)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=291)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (291,7,124)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=292)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (292,7,120)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=293)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (293,7,131)

--Opening
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=294)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (294,7,139)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=295)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (295,7,141)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=296)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (296,7,142)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=297)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (297,7,143)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=298)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (298,7,144)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=299)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (299,7,145)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=300)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (300,7,146)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=301)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (301,7,147)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=302)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (302,7,148)

--Master
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=303)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (303,7,126)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=304)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (304,7,127)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=305)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (305,7,129)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=306)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (306,7,134)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=307)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (307,7,135)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=308)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (308,7,136)

--Transaction
 
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=309)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (309,7,300)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=796)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (796,7,337)

--Purchase

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=310)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (310,7,301)    

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=311)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (311,7,306) 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=312)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (312,7,307)  

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=313)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (313,7,308)  

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=314)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (314,7,309)  
  
 IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=315)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (315,7,324)  
 
  IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=316)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (316,7,363)

 IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=518)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (518,7,329)

/*
  IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=317)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (317,7,334)
*/

--Production

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=318)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (318,7,378)--Production


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=319)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (319,7,364)--StoreIssue
 
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=320)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (320,7,374)--StoreIssueReturn

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=321)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (321,7,379)--seprator

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=322)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (322,7,367)--Beam Production

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=323)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (323,7,368)--Beam Loading

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=324)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (324,7,369)--Taka Production
 
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=325)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (325,7,370)--Taka Production
 
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=326)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (326,7,371)--Taka Production

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=425)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (425,7,381)--Taka Cutting

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=519)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (519,7,1061) -- Color Matching

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=521)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (521,7,1062) --Job Card

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=658)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (658,7,1068) --Color Recipe

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=659)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (659,7,1069) --Color Formula
  
--Mill 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=328)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (328,7,303)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=329)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (329,7,310)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=330)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (330,7,314)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=331)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (331,7,356)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=332)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (332,7,361)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=333)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (333,7,377)


--Job
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=334)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (334,7,304)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=335)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (335,7,317)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=336)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (336,7,359)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=337)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (337,7,319)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=338)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (338,7,360)

--Sales
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=339)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (339,7,351)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=340)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (340,7,352)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=341)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (341,7,353)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=342)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (342,7,355)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=343)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (343,7,358)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=344)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (344,7,362)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=345)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (345,7,375)

--Account
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=346)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (346,7,302)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=347)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (347,7,321)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=348)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (348,7,322)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=349)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (349,7,325)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=350)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (350,7,328)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=351)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (351,7,376)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=423)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (423,7,381)


--Reports
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=352)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (352,7,800)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=353)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (353,7,801)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=354)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (354,7,802)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=355)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (355,7,1058)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=356)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (356,7,803)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=357)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (357,7,804)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=358)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (358,7,805)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=359)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (359,7,806)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=360)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (360,7,807)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=361)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (361,7,808)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=362)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (362,7,809)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=363)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (363,7,810)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=364)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (364,7,811)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=365)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (365,7,812)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=366)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (366,7,813)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=367)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (367,7,814)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=368)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (368,7,815)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=369)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (369,7,816)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=370)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (370,7,817)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=371)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (371,7,818)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=372)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (372,7,819)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=373)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (373,7,820)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=374)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (374,7,821)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=375)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (375,7,822)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=376)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (376,7,823)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=376)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (376,7,824)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=377)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (377,7,825)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=378)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (378,7,826)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=379)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (379,7,827)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=380)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (380,7,828)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=381)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (381,7,829)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=382)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (382,7,830)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=383)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (383,7,831)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=384)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (384,7,832)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=385)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (385,7,833)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=386)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (386,7,834)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=387)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (387,7,835)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=388)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (388,7,836)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=389)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (389,7,837)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=390)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (390,7,838)


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=391)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (391,7,839)


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=392)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (392,7,840)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=393)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (393,7,841)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=394)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (394,7,842)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=395)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (395,7,843)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=396)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (396,7,844)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=397)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (397,7,845)

--setup
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=398)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (398,7,900)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=399)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (399,7,901)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=400)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (400,7,902)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=401)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (401,7,138)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=402)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (402,7,904)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=424)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (424,7,908)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=650)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (650,7,911) --Report setup

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=809)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (809,7,913) -- Database Group

--Tools
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=801)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (801,7,912) --Template setup

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=403)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (403,7,700)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=404)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (404,7,701)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=405)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (405,7,702)


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=406)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (406,7,703)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=407)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (407,7,704)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=408)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (408,7,705)

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=409)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (409,7,706) -- Voucher ReIndex

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=410)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (410,7,707)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=411)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (411,7,708)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=412)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (412,7,709)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=413)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (413,7,710)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=414)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (414,7,711)
 
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=415)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (415,7,380)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=654)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (654,7,1066)


------------------------------------------------------------------------------------
--------------------------Process House - 10 -----------------------------------------------
------------------------------------------------------------------------------------

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=811)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (811,10,100) 
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=812)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (812,10,101)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=813)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (813,10,103)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=814)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (814,10,104)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=815)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (815,10,105)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=816)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (816,10,106)

  IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=817)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (817,10,1051)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=819)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (819,10,1050)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=820)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (820,10,130)

--Product

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=821)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (821,10,107)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=822)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (822,10,108)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=823)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (823,10,109)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=824)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (824,10,111)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=825)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (825,10,112)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=826)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (826,10,113)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=827)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (827,10,114)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=828)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (828,10,116)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=829)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (829,10,117)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=830)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (830,10,118)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=831)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (831,10,132)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=832)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (832,10,133)


--Account in master
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=833)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (833,10,140)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=834)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (834,10,122)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=835)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (835,10,123)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=836)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (836,10,124)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=837)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (837,10,120)
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=838)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (838,10,131)

--Opening
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=839)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (839,10,139) --Sale Purchase Opening Bill

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=840)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (840,10,141) -- Opening

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=841)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (841,10,142) -- Opening Account Balance

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=842)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (842,10,147)  -- Part Payment

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=843)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (843,10,148) -- Opening Cheque


--Master
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=844)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (844,10,126)   -- Voucher type
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=845)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (845,10,127) -- Voucher
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=846)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (846,10,128)  -- 

--Transaction
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=847)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (847,10,300) -- Transaction 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=848)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (848,10,337)


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=849)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (849,10,351) -- Sales

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=850)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (850,10,301) -- Purchase

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=851)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (851,10,302) -- Account

--Purchase

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=852)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (852,10,309)  -- Purchase Invoice
 
 IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=853)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (853,10,324)    -- General Expense
 
  IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=854)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (854,10,363)  -- Purchase Return

 IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=855)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (855,10,329)  -- General expense Return

--Sales

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=856)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (856,10,358)   -- Sales Invoice

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=857)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (857,10,362)    -- Sales Return

--Account
 
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=858)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (858,10,321)    -- Journal Voucher

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=859)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (859,10,322)   -- Receipt voucher

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=860)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (860,10,325)   -- Cr/Dr Note

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=861)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (861,10,328)  -- Payment Voucher

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=862)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (862,10,376)  -- BRS

--Reports

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=863)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (863,10,800) -- Reports

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=864)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (864,10,801)  -- Account Ledger 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=865)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (865,10,802)  -- Outstanding 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=866)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (866,10,803)  -- Trial Balance

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=867)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (867,10,804)  -- Balance Sheet

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=868)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (868,10,805)  -- Inventory Register 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=869)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (869,10,807)  -- Purchase Invoice
 
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=870)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (870,10,811)  -- Purchase Return

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=871)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (871,10,812)  --

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=872)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (872,10,814)   -- Sales Invoice

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=873)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (873,10,816)  -- Sales Return


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=874)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (874,10,822)  ---- 


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=875)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (875,10,827)  -- Cr/Dr Note


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=876)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (876,10,829)  -- Tax Register

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=877)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (877,10,830)  -- Gstr1

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=878)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (878,10,831)  -- Gstr2

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=879)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (879,10,832)  -- Gstr3

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=880)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId 
)values (880,10,833)   -- Gst job Work

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=881)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (881,10,834)  -- Gstr2 Reconcile

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=882)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (882,10,835)  ---

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=883)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (883,10,836)   --- Tds

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=884)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (884,10,837)  -- Tcs

--setup
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=885)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (885,10,900)   -- Setup

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=886)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (886,10,901)   -- User Role

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=887)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (887,10,902)  -- User Master

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=888)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (888,10,138)  -- Company
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=889)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (889,10,904) -- Menu Setup

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=890)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (890,10,911) --Report setup

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=891)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (891,10,912) --Template setup

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=892)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (892,10,913) -- Database Group

--Tools
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=893)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (893,10,700)  -- Tools

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=894)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (894,10,701)  -- Backup/ Restore

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=895)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (895,10,702)  -- Change Password


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=896)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (896,10,703)  -- 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=897)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (897,10,704)  -- Data Fredge

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=898)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (898,10,705)  -- Account Merge

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=507)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (507,10,706) -- Voucher Reindex

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=899)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (899,10,707)  -- 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=900)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (900,10,708) -- Depreciation Posting 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=901)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId 
)values (901,10,709)  -- Cash Adjustment

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=902)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (902,10,710)  -- 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=903)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (903,10,711) -- Bulk Delete

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=904)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (904,10,1066)



------------------------------------------------------------------------------------
--------------------------Online - 12 -----------------------------------------------
------------------------------------------------------------------------------------

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=524)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (524,12,100) -- Masters

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=525)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (525,12,101) --Location
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=526)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (526,12,103) -- State
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=527)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (527,12,104) -- City
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=528)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (528,12,105) --Area
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=529)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (529,12,106) --Product

  IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=530)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (530,12,1051) -- Division

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=531)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (531,12,1050) -- Branch 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=532)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (532,12,130) -- Store

--Product
 
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=533)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (533,12,107) -- Product Type

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=534)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (534,12,108) -- Brand

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=535)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (535,12,109) --Unit

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=536)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (536,12,111) -- Sub Group

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=537)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (537,12,112) --Size Master

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=538)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (538,12,113) --Color Master

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=539)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (539,12,114) -- Category Master

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=540)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (540,12,116) --

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=541)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (541,12,117) -- Gst Slab

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=542)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (542,12,118) -- Product Master

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=543)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (543,12,132) --Design Master

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=544)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (544,12,133)  --Catalog

 
--Account in master
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=545)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (545,12,140)  --Account Section

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=546)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (546,12,122) -- Ledger Group

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=547)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (547,12,123) -- Party Group 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=548)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (548,12,124) --Account Master

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=549)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (549,12,120) --SalesMan

--Opening
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=550)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (550,12,139) --Sale/Purchase Opening Bill

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=551)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (551,12,141) -- Opening

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=552)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (552,12,142) --Opening Account Balance

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=553)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (553,12,143) -- Opening Grey Stock 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=554)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (554,12,144) -- Opening Finish Stock

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=555)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (555,12,145) -- Opening Mill Stock


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=556)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (556,12,146) --Opening Job Issue

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=557)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (557,12,147) -- Part Payment

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=558)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (558,12,148) -- Opening Cheque


--Master
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=559)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (559,12,126) --Voucher Type
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=560)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (560,12,127) --Voucher

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=561)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (561,12,129)  -- Narration

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=562)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (562,12,366) --RefBank 
 

--Transaction
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=563)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (563,12,300) ----Transacton

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=798)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (798,12,337) -- Stock Journal

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=564)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (564,12,301) --   Purchase 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=565)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (565,12,306) --Purchase Order 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=566)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (566,12,307) --Purchase Approval  

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=567)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (567,12,308) --Inward(GRN)  

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=568)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (568,12,309)  --Purchase Bill
 

 IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=569)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (569,12,324) --General Expense 
 
  IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=570)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (570,12,363) --Purchase Return

 IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=571)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (571,12,329) -- General expense eturn

--Mill 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=572)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (572,12,303)  -- Mill

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=573)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (573,12,310) -- Mill Issue


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=574)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (574,12,356) -- Mill Receipt Challan

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=575)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (575,12,361) --Mill Return


--Job
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=576)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (576,12,304) --Job

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=577)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (577,12,359) -- Job Issue

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=578)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (578,12,319) --Job Receipt Challan

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=579)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (579,12,335) -- Job Outward Challan

--Sales
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=580)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (580,12,351) -- Sales

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=581)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (581,12,352) -- Sales Order

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=582)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (582,12,353) -- Sale Approval

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=583)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (583,12,355) -- Sales Challan

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=584)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (584,12,358) -- Sale Invoice

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=585)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (585,12,362) -- Sales Return 


--Account
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=586)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (586,12,302) --Account 


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=587)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (587,12,321) -- Journal Voucher

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=588)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (588,12,322) -- Receipt Voucher

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=589)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (589,12,325) -- Cr/Dr Note

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=590)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (590,12,328) -- Payment Voucher

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=591)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (591,12,376) -- BRS
 

--------------------Reports


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=592)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (592,12,800) --Reports

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=593)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (593,12,801) --Account Ledger

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=594)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (594,12,802) --Outstanding

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=595)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (595,12,1058)  --Stock Report

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=596)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (596,12,803) --Trial Balance 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=597)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (597,12,804) -- Balance sheet

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=598)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (598,12,805) --Inventory Register 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=599)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (599,12,806) --GRN Report

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=600)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (600,12,807) -- Purchase
  
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=601)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (601,12,811)  --Purchase Return

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=602)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (602,12,812) -- 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=603)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (603,12,813) -- Sales Challan

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=604)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (604,12,814) -- Sales


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=605)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (605,12,816) -- Sales Return
  
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=606)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (606,12,823) -- Job Issue

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=607)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (607,12,824) -- JobReceipt Challan

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=608)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (608,12,827)  -- Cr/Dr Note

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=609)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (609,12,828) -- Order Register

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=610)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (610,12,829) -- Tax Register

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=611)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (611,12,830)  --Gstr-1

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=612)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (612,12,831)  --Gstr-2

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=613)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (613,12,832)  --Gstr-3b

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=614)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (614,12,833) -- Gstr-4A 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=615)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (615,12,834)  -- Gstr-2Reconcile

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=616)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (616,12,835) --  

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=617)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (617,12,836) --TDS

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=618)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (618,12,837) -- TCS

--setup

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=619)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (619,12,900) -- Setup

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=620)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (620,12,901) --User Role

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=621)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (621,12,902) --User Master 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=622)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (622,12,138) --Company

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=623)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (623,12,908) -- Payment Receipt Setup

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=652)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (652,12,911) --Report setup

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=803)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (803,12,912) --Template setup

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=810)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (810,12,913) -- Database Group

--Tools
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=624)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (624,12,700) --Tools

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=625)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (625,12,701) --Backup/Restore

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=626)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (626,12,702) --Change Password


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=627)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (627,12,703)  -- 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=628)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (628,12,704) --Data Fredge

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=629)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (629,12,705) -- Account Merge 

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=630)
--INSERT INTO dbo.Menu_Package
--(
--   Id,PackageId,MenuId
--)values (630,12,706) --Voucher ReIndex 

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=631)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (631,12,707) --

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=632)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (632,12,708) --Depreciation Posting

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=633)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (633,12,709) --Cash Adjustment

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=634)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (634,12,710) --

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=635)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (635,12,711) --Bulk Delete
 
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=636)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (636,12,904) --Balance Sheet

 IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=656)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (656,12,1066) -- Balance Transfer
 
 IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=905)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (905,1,110)

 IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=906)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (906,2,110)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=907)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (907,3,110)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=908)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (908,4,110)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=909)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (909,7,110)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=910)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (910,10,110)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=911)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (911,12,110)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=912)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (912,1,335) -- Job Bill


IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=913)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (913,10,335) -- Job Bill

-- TAKA WISE JOB RECEIPT
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=914)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (914,1,1065)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=915)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (915,4,1065)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=916)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (916,11,1065)

--STORE ISSUE
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=917)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (917,1,378)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=918)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (918,1,364)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=919)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (919,1,374)



SET IDENTITY_INSERT dbo.Menu_Package OFF
