SET IDENTITY_INSERT dbo.Menu_Package ON
--Taxtile Package

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

--Opening
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

--Transaction
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=42)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (42,1,300)

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

  IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=53)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (53,1,334)

--Mill
IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=54)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (54,1,303)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=54)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (54,1,303)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=55)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (55,1,310)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=56)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (56,1,314)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=57)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (57,1,356)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=58)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (58,1,361)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=133)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (133,1,377)


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
)values (59,1,317)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=60)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (60,1,359)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=61)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (61,1,319)

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=62)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (62,1,360)

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
)values (76,1,365)

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

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=126)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (126,1,706)

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

-----------------------Looms (Weaving)-------------------------------------------------------------------
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

  IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=179)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (179,3,334)


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

IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.Id=254)
INSERT INTO dbo.Menu_Package
(
   Id,PackageId,MenuId
)values (254,3,706)

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
SET IDENTITY_INSERT dbo.Menu_Package OFF